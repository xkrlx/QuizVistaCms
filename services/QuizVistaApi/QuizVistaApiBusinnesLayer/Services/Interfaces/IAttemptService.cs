using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests.AttemptRequests;
using QuizVistaApiBusinnesLayer.Models.Responses.AttemptResponses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IAttemptService
    {
        Task<ResultWithModel<AttemptResponse>> GetAttempt(int id);
        Task<ResultWithModel<AttemptResponse>> GetAttemptWithAnswers(int id);
        Task<ResultWithModel<IEnumerable<AttemptResponse>>> GetAttemptsOfUser(int userId);
        Task<ResultWithModel<UserResultBriefResponse>> GetUserResults(string userName);
        Task<ResultWithModel<QuizResultBriefResponse>> GetQuizResults(string quizName);
        Task<Result> SaveAttempt(SaveAttemptRequest attempt, string userName);
        Task<Result> DeleteAttempt(int id);
        Task<Result> UpdateAttempt(AttemptRequest attempt);
    }
}
