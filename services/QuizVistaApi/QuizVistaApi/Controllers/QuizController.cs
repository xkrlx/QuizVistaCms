using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;
using QuizVistaApiBusinnesLayer.Services.Implementations;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using System.Security.Claims;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<QuizResponse>>> GetQuizez()
        {
            return await _quizService.GetQuizesAsync();
        }

        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizesForUser()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.GetQuizListForUser(userName);
        }

        [HttpGet("category")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizesByCategory(string categoryName)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.GetQuizesByCategory(userName, categoryName);
        }

        [HttpGet("tag")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizesByTag(string tagName)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.GetQuizesByTag(userName, tagName);
        }



        [HttpGet("moderator")]
        [Authorize(Roles = "Moderator")]
        public async Task<ResultWithModel<IEnumerable<QuizListForModResponse>>> GetQuizesForModerator()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.GetQuizListForModerator(userName);
        }

        [HttpGet("details")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<QuizDetailsForUser>> GetQuizDetails(string quizName)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.GetQuizDetailsForUser(quizName, userName);
        }

        [HttpGet("details-mod")]
        [Authorize(Roles = "Moderator")]
        public async Task<ResultWithModel<QuizDetailsForModResponse>> GetQuizDetailsForMod(string quizName)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.GetQuizDetailsForMod(quizName, userName);
        }

        [HttpGet("quiz-run")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<QuizRun>> GetUserQuizRun(string quizName)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            return await _quizService.GetQuizWithQuestionsAsync(quizName, userName);
        }

        [HttpGet("get-questions-mod")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<QuizWithQuestionsModResponse>> GetQuestionsForQuizMod(string quizName)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            return await _quizService.GetQuestionsForQuizMod(quizName, userName);
        }


        [HttpPost("create"), Authorize(Roles = "Moderator")]
        public async Task<Result> CreateQuiz([FromBody] QuizRequest quizRequest)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            return await _quizService.CreateQuizAsync(userId, quizRequest);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> EditQuiz([FromBody] QuizRequest quizRequest)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.UpdateQuizAsync(userId,quizRequest);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> DeleteQuiz(int id)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.DeleteQuizAsync(username, id);
        }


        [HttpPost("assignuser")]
        [Authorize(Roles="Moderator")]
        public async Task<Result> AssignUser([FromBody] AssignUserRequest assignUserRequest)
        {
            return await _quizService.AssignUser(assignUserRequest);
        }

        [HttpPost("unassignuser")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> UnAssignUser([FromBody] AssignUserRequest assignUserRequest)
        {
            return await _quizService.UnAssignUser(assignUserRequest);
        }



        [HttpPost("generate")]
        public async Task<ResultWithModel<QuizGenerateResponse>> GenerateQuiz([FromBody] QuizGenerateRequest quizGenerateRequest)
        {
            //var userId = User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            return await _quizService.GenerateQuizAsync(quizGenerateRequest);
        }


        [HttpPost("evaluateanswer")]
        public async Task<ResultWithModel<QuizOpenQuestionResponse>> EvaluteAnswer([FromBody] QuizOpenQuestionRequest quizOpenQuestionRequest)
        {
            //var userId = User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            return await _quizService.EvaluateAnswer(quizOpenQuestionRequest);
        }

    }
}
