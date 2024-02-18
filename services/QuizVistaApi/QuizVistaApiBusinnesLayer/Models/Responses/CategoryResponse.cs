using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;
using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;

namespace QuizVistaApiBusinnesLayer.Models.Responses;



public class CategoryResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public List<QuizResponse> Quizzes { get; set; } = new List<QuizResponse>();

    private CategoryResponse() { }

    public CategoryResponse(int id,
        string name,
        string? description,
        List<QuizResponse> quizzes)
    {
        Id = id;
        Name = name; 
        Description = description; 
        Quizzes = quizzes;
    }

}
