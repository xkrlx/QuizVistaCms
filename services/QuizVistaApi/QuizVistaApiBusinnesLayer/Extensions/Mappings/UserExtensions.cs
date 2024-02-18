using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses.UserResponses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions.Mappings
{
    public static class UserExtensions
    {
        public static UserResponse ToResponse(this User user)
        {
            if (user is null) return null;

            return new UserResponse(
                user.Id,
                user.UserName,
                //user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.Email,
                user.ResetPasswordToken,
                user.Attempts?.ToCollectionResponse().ToList(),
                user.QuizzesNavigation.ToCollectionResponse().ToList(),
                user.Quizzes.ToCollectionResponse().ToList(),
                user.Roles.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<UserResponse> ToCollectionResponse(this IEnumerable<User> users)
        {
            if (users is null || !users.Any()) return Enumerable.Empty<UserResponse>();
            return users.Select(ToResponse) ?? Enumerable.Empty<UserResponse>();
        }

        public static User ToEntity(this UserRequest userRequest)
        {
            return new User
            {
                UserName = userRequest.UserName,
                PasswordHash = userRequest.Password,  //DODAĆ HASHOWANIE
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
            };
        }
    }
}
