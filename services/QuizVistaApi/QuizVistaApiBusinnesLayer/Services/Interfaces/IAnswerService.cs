using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IAnswerService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswers();
        Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswersForQuestion(int questionId);
        Task<ResultWithModel<AnswerResponse>> GetAnswer(AnswerRequest answerRequest);
        
        Task<Result> CreateAnswerAsync(AnswerRequest answerRequest);
        Task<Result> DeleteAnswerAsync(int answerId);
        Task<Result> UpdateAnswerAsync(AnswerRequest answerRequest);


        #endregion
    }
}
