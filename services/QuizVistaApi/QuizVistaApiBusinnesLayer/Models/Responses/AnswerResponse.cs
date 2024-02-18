using QuizVistaApiBusinnesLayer.Models.Responses.AttemptResponses;
using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QuizVistaApiBusinnesLayer.Models.Responses;



public class AnswerResponse
{
    public int Id { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int? QuestionId { get; set; }

    public QuestionResponse? Question { get; set; }

    public virtual List<AttemptResponse?>? Attempts { get; set; } = new List<AttemptResponse?>();


    private AnswerResponse() { }

    public AnswerResponse(
        int id,
        string answerText,
        bool isCorrect,
        int? questionId,
        QuestionResponse? question,
        List<AttemptResponse?>? attempts
        )
    {
        Id = id;
        AnswerText = answerText;
        IsCorrect = isCorrect;
        QuestionId = questionId;
        Question = question;
        Attempts = attempts;
    }
    
}
