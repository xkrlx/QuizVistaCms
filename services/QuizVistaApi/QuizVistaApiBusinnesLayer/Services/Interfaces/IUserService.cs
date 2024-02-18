using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Requests.UserRequests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Models.Responses.UserResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterUser(UserRequest request);
        Task<ResultWithModel<LoginResponse>> LoginUser(UserLoginRequest request);
        Task<Result> ChangePassword(ChangePasswordRequest changePasswordRequest);
        Task<Result> UpdateUser(UserUpdateRequest userRequest);
        Task<Result> ResetPasswordInit(ResetPasswordInitialRequest resetPasswordInitialRequest);
        Task<Result> ResetPassword(ResetPasswordRequest resetPasswordRequest);
        Task<Result> ToggleRole(ToggleRoleRequest toggleRoleRequest);
        Task<ResultWithModel<UserResponse>> GetUser(int userId);
        Task<ResultWithModel<UserDetailsResponse>> GetUserDetails(string userName);
        Task<ResultWithModel<IEnumerable<UserResponse>>> GetUsers();
    }
}
