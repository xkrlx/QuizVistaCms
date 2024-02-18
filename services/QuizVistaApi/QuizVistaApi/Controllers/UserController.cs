using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Requests.UserRequests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Models.Responses.UserResponses;
using QuizVistaApiBusinnesLayer.Services.Implementations;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using System.Security.Claims;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<Result> Register([FromBody] UserRequest userRequest)
        {
            return await _userService.RegisterUser(userRequest);
        }

        //[EnableCors("_myOrigins")]
        [HttpPost("login")]
        public async Task<ResultWithModel<LoginResponse>> Login([FromBody] UserLoginRequest userRequest)
        {
            return await _userService.LoginUser(userRequest);
        }

        [HttpGet("showusers"), Authorize(Roles = "Admin")]
        public async Task<ResultWithModel<IEnumerable<UserResponse>>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        [HttpGet("showuser/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResultWithModel<UserResponse>> GetUser(int userId)
        {
            return await _userService.GetUser(userId);
        }

        [HttpGet("details")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<UserDetailsResponse>> Details()
        {
            string userName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _userService.GetUserDetails(userName);
        }

        [HttpPut("edit")]
        [Authorize(Roles="Admin")]
        public async Task<Result> Edit([FromBody] UserUpdateRequest userRequest)
        {
            return await _userService.UpdateUser(userRequest);
        }

        [HttpPost("changepassword"), Authorize(Roles = "User")]
        public async Task<Result> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            changePasswordRequest.ValidateUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _userService.ChangePassword(changePasswordRequest);
        }

        [HttpPost("reset-password-init")]
        public async Task<Result> ResetPasswordInit([FromBody] ResetPasswordInitialRequest resetPasswordInitialRequest)
        {
            return await _userService.ResetPasswordInit(resetPasswordInitialRequest);
        }

        [HttpPost("reset-password")]
        public async Task<Result> ResetPassword([FromBody] ResetPasswordRequest resetPassRequest)
        {
            return await _userService.ResetPassword(resetPassRequest);
        }

        [HttpPost("toggle-role")]
        [Authorize(Roles = "Admin")]
        public async Task<Result> ToggleRole([FromBody] ToggleRoleRequest toggleRoleRequest)
        {
            return await _userService.ToggleRole(toggleRoleRequest);
        }
    }
}
