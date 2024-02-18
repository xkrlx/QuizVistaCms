using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Requests.UserRequests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Models.Responses.UserResponses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;

        public UserService(IRepository<User> userRepository,IRepository<Role> roleRepository, IConfiguration configuration, IMailService mailService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<ResultWithModel<IEnumerable<UserResponse>>> GetUsers()
        {

            var users = await _userRepository.GetAll().OrderBy(x => x.Id).ToListAsync();
            //var users = await _userRepository.GetAll().Include(x=>x.Roles).OrderBy(x => x.Id).ToListAsync();  DLACZEGO TO NIE DZIAŁA??!?!


            foreach (var user in users)
            {
                var roles = await _userRepository.GetAll().Where(x=>x.Id== user.Id).SelectMany(x=>x.Roles).ToListAsync();
                user.Roles = roles; 
            }

            return ResultWithModel<IEnumerable<UserResponse>>.Ok(users.ToCollectionResponse().ToList());
        }

        public async Task<ResultWithModel<UserResponse>> GetUser(int userId)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user is null)
                throw new ArgumentNullException($"User #{userId} not found");


            return ResultWithModel<UserResponse>.Ok(user.ToResponse());
        }

        public async Task<ResultWithModel<UserDetailsResponse>> GetUserDetails(string userName)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);


            if (user is null)
                throw new ArgumentNullException($"User not found");

            var userDetailsResponse = new UserDetailsResponse
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return ResultWithModel<UserDetailsResponse>.Ok(userDetailsResponse);
        }

        public async Task<Result> RegisterUser(UserRequest request)
        {
            List<User> users = await _userRepository.GetAll().ToListAsync();
            bool emailCheck = users.Any(x => x.Email.ToLower() == request.Email.ToLower());
            bool userNameCheck = users.Any(x=>x.UserName.ToLower() == request.UserName.ToLower());
            if (emailCheck)
                throw new ArgumentException("actual email is already registered");
            if (userNameCheck)
                throw new ArgumentException("user with this username is already registered");

            request.Password = HashPassword(request.Password);

            var newUser = request.ToEntity();
            await _userRepository.InsertAsync(newUser);

            var userRole = new Role { Id = 3, Name = "User" };

            newUser.Roles.Add(userRole);
            await _userRepository.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<ResultWithModel<LoginResponse>> LoginUser(UserLoginRequest request)
        {
            request.Password = HashPassword(request.Password);

            var user = await _userRepository.GetAll().Include(x=>x.Roles).Where(x=>x.UserName.ToLower() == request.UserName.ToLower()).FirstOrDefaultAsync();

            if (user is null)
                throw new ArgumentException($"Nazwa użytkownika lub hasło jest nieprawidłowe");


            if (user.PasswordHash != request.Password)
                throw new ArgumentException("Nazwa użytkownika lub hasło jest nieprawidłowe");

           string token = CreateToken(user);

            LoginResponse loginResponse = new LoginResponse(token);
            return ResultWithModel<LoginResponse>.Ok(loginResponse);
        }

        public async Task<Result> ResetPasswordInit(ResetPasswordInitialRequest request)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Result.Failed("Nieprawidłowy adres email");
            }

            var resetCode = GenerateResetCode();
            var hashedToken = HashPassword(resetCode);

            user.ResetPasswordToken = hashedToken;
            user.PasswordResetExpire = DateTime.Now;
            await _userRepository.UpdateAsync(user);

            string reset_password_link = _configuration["AppSettings:Url"]+"/reset-password";

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Resetowanie hasła",
                Body = $"Twój kod do resetu hasła to: {resetCode}. Hasło zresetujesz na stronie {reset_password_link}"
            };

            await _mailService.SendEmailAsync(mailRequest);
            
            return Result.Ok();
        }

        public async Task<Result> UpdateUser(UserUpdateRequest userRequest)
        {
            var user = await _userRepository.GetAll().Where(u=>u.UserName==userRequest.UserName).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Username not found");
            }

            user.FirstName= userRequest.FirstName;
            user.LastName= userRequest.LastName;
            user.Email= userRequest.Email;

            await _userRepository.UpdateAsync(user);
            return Result.Ok();
        }


        public async Task<Result> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            resetPasswordRequest.Token = HashPassword(resetPasswordRequest.Token);
            resetPasswordRequest.Password = HashPassword(resetPasswordRequest.Password);
            resetPasswordRequest.ConfirmPassword = HashPassword(resetPasswordRequest.ConfirmPassword);

            if (string.Compare(resetPasswordRequest.Password, resetPasswordRequest.ConfirmPassword) != 0)
            {
                return Result.Failed("The new password and confirm password does not match.");
            }

            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.ResetPasswordToken == resetPasswordRequest.Token);


            if (user == null || user.PasswordResetExpire < DateTime.Now.AddDays(-1))
            {
                return Result.Failed("Invalid or expired reset token.");
            }


            user.PasswordHash = resetPasswordRequest.Password;
            user.ResetPasswordToken = null;
            user.PasswordResetExpire = null;
            await _userRepository.UpdateAsync(user);

            return Result.Ok();
        }

        public async Task<Result> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            changePasswordRequest.CurrentPassword = HashPassword(changePasswordRequest.CurrentPassword);
            changePasswordRequest.NewPassword = HashPassword(changePasswordRequest.NewPassword);
            changePasswordRequest.ConfirmNewPassword = HashPassword(changePasswordRequest.ConfirmNewPassword);

            if (string.Compare(changePasswordRequest.NewPassword, changePasswordRequest.ConfirmNewPassword) != 0)
            {
                throw new ArgumentException("The new password and confirm password does not match.");
            }

            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName.ToLower() == changePasswordRequest.ValidateUserName.ToLower());

            if (user is null)
                throw new ArgumentNullException($"Error");

            if (user.PasswordHash != changePasswordRequest.CurrentPassword)
            {
                //throw new ArgumentException("Invalid password");
                return Result.Failed("Nieprawidłowe hasło");
            }

            user.PasswordHash = changePasswordRequest.NewPassword;
            await _userRepository.UpdateAsync(user);
            return Result.Ok();
        }

        public async Task<Result> ToggleRole(ToggleRoleRequest toggleRoleRequest)
        {
            var user = await _userRepository.GetAll().Include(x=>x.Roles).FirstOrDefaultAsync(x=>x.UserName== toggleRoleRequest.UserName);
            if(user == null)
            {
                throw new ArgumentException("User not found");
            }

            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x=>x.Name== toggleRoleRequest.RoleName);

            var userRole = user.Roles.FirstOrDefault(r => r.Name == toggleRoleRequest.RoleName);

            if (userRole != null)
            {
                user.Roles.Remove(userRole);
            }
            else
            {

                user.Roles.Add(role);
            }

            await _userRepository.UpdateAsync(user);

            return Result.Ok();
        }
        static string HashPassword(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name,user.UserName),
             };


            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private string GenerateResetCode()
        {
            var random = new Random();
            return random.Next(0, 999999).ToString("D6");
        }
    }
}
