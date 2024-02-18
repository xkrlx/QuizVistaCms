using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions.Mappings
{
    public static class RoleExtensions
    {

        public static RoleResponse ToResponse(this Role role)
        {
            if (role is null) return null;

            return new RoleResponse(
                role.Id,
                role.Name,
                role.Users.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<RoleResponse> ToCollectionResponse(this IEnumerable<Role> roles)
        {
            if (roles is null || !roles.Any()) return Enumerable.Empty<RoleResponse>();
            return roles.Select(ToResponse) ?? Enumerable.Empty<RoleResponse>();
        }

        public static Role ToEntity(this RoleRequest roleRequest)
        {
            return new Role
            {
                Name = roleRequest.Name,
            };
        }

    }
}
