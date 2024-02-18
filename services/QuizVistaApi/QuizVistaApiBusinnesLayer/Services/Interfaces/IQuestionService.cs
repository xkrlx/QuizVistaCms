using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests.QuestionRequests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IQuestionService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<QuestionResponse>>> GetQuestions();
        Task<ResultWithModel<IEnumerable<QuestionResponse>>> GetQuestionsForQuiz(int quizId);
        Task<ResultWithModel<QuestionResponse>> GetQuestion(int questionId);
        Task<ResultWithModel<QuestionResponse>> GetQuestionWithAnswers(int questionId);
        Task<Result> CreateQuestionAsync(QuestionRequest question);
        Task<Result> DeleteQuestionAsync(int questionId);
        Task<Result> UpdateQuestionAsync(QuestionRequest QuestionRequest);
        
        

        #endregion
    }
}
