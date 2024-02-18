using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions.Mappings
{
    public static class AnswerExtensions
    {

        public static AnswerResponse ToResponse(this Answer answer)
        {
            if (answer is null) return null;

            return new AnswerResponse
            (
                answer.Id,
                answer.AnswerText,
                answer.IsCorrect,
                answer.QuestionId,
                answer.Question.ToResponse(),
                answer.Attempts.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<AnswerResponse> ToCollectionResponse(this IEnumerable<Answer> answers)
        {
            if (answers is null || !answers.Any()) return Enumerable.Empty<AnswerResponse>();
            return answers.Select(ToResponse) ?? Enumerable.Empty<AnswerResponse>();
        }

        public static Answer ToEntity(this AnswerRequest answerRequest)
        {
            return new Answer
            {
                AnswerText = answerRequest.AnswerText,
                Id = answerRequest.Id,
                IsCorrect = answerRequest.IsCorrect,
                QuestionId = answerRequest.QuestionId,
            };
        }

    }
}
