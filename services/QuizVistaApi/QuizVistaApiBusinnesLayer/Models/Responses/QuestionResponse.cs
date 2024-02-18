using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;
using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizVistaApiBusinnesLayer.Models.Responses;


public class QuestionResponse
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Text { get; set; } = null!;

    public int AdditionalValue { get; set; }

    public int? SubstractionalValue { get; set; }

    public int? QuizId { get; set; }

    public string? CmsTitleStyle { get; set; }

    public string? CmsQuestionsStyle { get; set; }

    public List<AnswerResponse>? Answers { get; set; } = new List<AnswerResponse>();

    public QuizResponse? Quiz { get; set; }

    private QuestionResponse() { }
    public QuestionResponse(
        int id,
        string type,
        string text,
        int additionalValue,
        int? substractionalValue,
        int? quizId, 
        string? cmsTitleStyle,
        string? cmsQuestionsStyle, 
        List<AnswerResponse>? answers,
        QuizResponse? quiz)
    {
        Id = id;
        Type = type;
        Text = text;
        AdditionalValue = additionalValue;
        SubstractionalValue = substractionalValue;
        QuizId = quizId;
        CmsTitleStyle = cmsTitleStyle;
        CmsQuestionsStyle = cmsQuestionsStyle;
        Answers = answers;
        Quiz = quiz;
    }

    
}
