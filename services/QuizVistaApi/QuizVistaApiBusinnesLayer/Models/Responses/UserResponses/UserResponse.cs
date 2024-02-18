using QuizVistaApiBusinnesLayer.Models.Responses.AttemptResponses;
using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;
using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizVistaApiBusinnesLayer.Models.Responses.UserResponses;

public class UserResponse
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    //public string PasswordHash { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ResetPasswordToken { get; set; } = string.Empty;

    public List<AttemptResponse?>? Attempts { get; set; }

    public List<QuizResponse> QuizzesNavigation { get; set; } = new List<QuizResponse>();

    public List<QuizResponse> Quizzes { get; set; } = new List<QuizResponse>();

    public List<RoleResponse> Roles { get; set; } = new List<RoleResponse>();

    private UserResponse() { }

    public UserResponse(
        int id,
        string userName,
        //string passwordHash,
        string firstName,
        string lastName,
        string email,
        string resetPasswordToken,
        List<AttemptResponse?> attempts,
        List<QuizResponse> quizzesNavigation,
        List<QuizResponse> quizzes,
        List<RoleResponse> roles)
    {
        Id = id;
        UserName = userName;
        //PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ResetPasswordToken = resetPasswordToken;
        Attempts = attempts;
        QuizzesNavigation = quizzesNavigation;
        Quizzes = quizzes;
        Roles = roles;
    }


}
