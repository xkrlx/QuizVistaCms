using QuizVistaApiBusinnesLayer.Models.Responses.UserResponses;
using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;

namespace QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;


public class QuizResponse
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

    public UserResponse? Author { get; set; }

    public CategoryResponse? Category { get; set; }

    public List<QuestionResponse?>? Questions { get; set; } = new List<QuestionResponse?>();


    public List<TagResponse?>? Tags { get; set; } = new List<TagResponse?>();

    public List<UserResponse?>? Users { get; set; } = new List<UserResponse?>();

    private QuizResponse() { }

    public QuizResponse(
        int id,
        string name,
        string? description,
        DateTime creationDate,
        DateTime? editionDate,
        int? categoryId,
        string? cmsTitleStyle,
        int? authorId,
        bool isActive,
        int attemptCount,
        bool? publicAccess,
        UserResponse? author,
        CategoryResponse? category,
        List<QuestionResponse?>? questions,
        List<TagResponse?>? tags,
        List<UserResponse?>? users)
    {
        Id = id;
        Name = name;
        Description = description;
        CreationDate = creationDate;
        EditionDate = editionDate;
        CategoryId = categoryId;
        CmsTitleStyle = cmsTitleStyle;
        AuthorId = authorId;
        IsActive = isActive;
        AttemptCount = attemptCount;
        PublicAccess = publicAccess;
        Author = author;
        Category = category;
        Questions = questions;
        Tags = tags;
        Users = users;
    }


}
