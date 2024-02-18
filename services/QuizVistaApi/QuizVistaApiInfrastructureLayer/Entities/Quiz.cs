using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

[Entity]
public partial class Quiz
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? EditionDate { get; set; }

    public int? CategoryId { get; set; }

    public string? CmsTitleStyle { get; set; }

    public int? AuthorId { get; set; }

    public bool IsActive { get; set; }

    public int AttemptCount { get; set; }

    public bool? PublicAccess { get; set; }

    public virtual User? Author { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
