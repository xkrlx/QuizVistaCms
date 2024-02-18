using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;

namespace QuizVistaApiInfrastructureLayer.Entities;

[Entity]
public partial class Question
{
    public int Id { get; set; }

    /// <summary>
    /// 1 - one good question
    /// 2 - true/false
    /// 3 - multi
    /// </summary>
    public string Type { get; set; } = null!;

    public string Text { get; set; } = null!;

    public int AdditionalValue { get; set; }

    public int? SubstractionalValue { get; set; }

    public int? QuizId { get; set; }

    public string? CmsTitleStyle { get; set; }

    public string? CmsQuestionsStyle { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Quiz? Quiz { get; set; }
}
