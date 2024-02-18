using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

[Entity]
public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ResetPasswordToken { get; set; }

    public DateTime? PasswordResetExpire { get; set; }

    public virtual ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();

    public virtual ICollection<Quiz> QuizzesNavigation { get; set; } = new List<Quiz>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
