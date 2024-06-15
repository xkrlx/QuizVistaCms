using QuizVistaApiBusinnesLayer.Models;
using System;
using System.Collections.Generic;
using QuizVistaApiInfrastructureLayer.Entities;
using System.Text;
using System.Threading.Tasks;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IQuizService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<QuizResponse>>> GetQuizesAsync();
        Task<ResultWithModel<QuizResponse>> GetQuizAsync(int id);
        Task<Result>  CreateQuizAsync(string userId,QuizRequest quizToCreate);
        Task<ResultWithModel<QuizGenerateResponse>> GenerateQuizAsync(QuizGenerateRequest quizToGenerate);
        Task<Result> DeleteQuizAsync(string username,int idToDelete);
        Task<Result> UpdateQuizAsync(string userId,QuizRequest quizToUpdate);
        Task<Result> AssignUser(AssignUserRequest assignUserRequest);
        Task<Result> UnAssignUser(AssignUserRequest assignUserRequest);
        //Task<Result> AssignUser(QuizRequest quizToUpdate);

        #endregion
        Task<ResultWithModel<QuizRun>> GetQuizWithQuestionsAsync(string quizName, string userName);

        Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizListForUser(string userName);
        Task<ResultWithModel<IEnumerable<QuizListForModResponse>>> GetQuizListForModerator(string userName);

        Task<ResultWithModel<QuizDetailsForUser>> GetQuizDetailsForUser(string quizName, string userName);
        Task<ResultWithModel<QuizDetailsForModResponse>> GetQuizDetailsForMod(string quizName, string userName);
        Task<ResultWithModel<QuizWithQuestionsModResponse>> GetQuestionsForQuizMod(string quizName, string userName);
        Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizesByCategory(string userName, string categoryName);
        Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizesByTag(string userName, string tagName);
    }
}
