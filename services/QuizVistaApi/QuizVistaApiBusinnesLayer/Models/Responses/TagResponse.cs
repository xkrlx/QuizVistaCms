using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;
using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QuizVistaApiBusinnesLayer.Models.Responses;


public class TagResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual List<QuizResponse> Quizzes { get; set; } = new List<QuizResponse>();

    public TagResponse() { }

    public TagResponse(int id,
        string name, 
        List<QuizResponse> quizzes)
    {
        Id = id;
        Name = name;
        Quizzes = quizzes;
    }

}
