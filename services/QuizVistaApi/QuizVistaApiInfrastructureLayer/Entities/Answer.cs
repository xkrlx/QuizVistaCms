using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

[Entity]
public partial class Answer
{
    public int Id { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int? QuestionId { get; set; }

    public virtual Question? Question { get; set; }

    public virtual ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();
}
