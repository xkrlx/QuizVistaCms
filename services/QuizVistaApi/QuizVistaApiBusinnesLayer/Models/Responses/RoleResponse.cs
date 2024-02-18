using QuizVistaApiBusinnesLayer.Models.Responses.UserResponses;
using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;

namespace QuizVistaApiBusinnesLayer.Models.Responses;

public class RoleResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<UserResponse> Users { get; set; } = new List<UserResponse>();

    private RoleResponse() { }

    public RoleResponse(int id, string name, List<UserResponse> users)
    {
        Id = id;
        Name = name;
        Users = users;
    }

}
