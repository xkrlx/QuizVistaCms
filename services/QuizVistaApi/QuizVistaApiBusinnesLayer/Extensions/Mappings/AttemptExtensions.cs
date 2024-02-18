using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Requests.AttemptRequests;
using QuizVistaApiBusinnesLayer.Models.Responses.AttemptResponses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions.Mappings
{
    public static class AttemptExtensions
    {

        public static AttemptResponse ToResponse(this Attempt attempt)
        {
            if (attempt is null) return null;

            return new AttemptResponse
            (
                attempt.Id,
                attempt.CreateDate,
                attempt.EditionDate,
                attempt.UserId,
                attempt.Answers.ToCollectionResponse().ToList(),
                attempt.User.ToResponse()
                );
        }

        public static IEnumerable<AttemptResponse> ToCollectionResponse(this IEnumerable<Attempt> attempts)
        {
            if(attempts is null || !attempts.Any()) return Enumerable.Empty<AttemptResponse>();
            return attempts.Select(ToResponse) ?? Enumerable.Empty<AttemptResponse>();
        }

        public static Attempt ToEntity(this AttemptRequest request)
        {
            var attempt = new Attempt
            {
                UserId = request.UserId,
                Answers = request.Answers.ConvertCollection().ToList()
            };

            return attempt;
        }

        public static List<Answer> ConvertCollection(this List<AnswerRequest> answerRequests)
        {
            return answerRequests.Select(a => a.ToEntity()).ToList();
        }
    }
}
