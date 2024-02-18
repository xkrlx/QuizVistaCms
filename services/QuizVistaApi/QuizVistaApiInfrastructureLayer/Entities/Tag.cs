using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

[Entity]
public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
