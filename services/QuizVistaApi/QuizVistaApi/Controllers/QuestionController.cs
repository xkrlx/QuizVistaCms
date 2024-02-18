using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Services.Implementations;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using QuizVistaApiBusinnesLayer.Models.Requests.QuestionRequests;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<QuestionResponse>>> GetQuestions()
        {
            return await _questionService.GetQuestions();
        }
        
        [HttpPost("create")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> CreateQuestion([FromBody] QuestionRequest questionRequest)
        {
            return await _questionService.CreateQuestionAsync(questionRequest);
        }
        
        [HttpPut("edit")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> EditQuestion([FromBody] QuestionRequest QuestionRequest)
        {
            return await _questionService.UpdateQuestionAsync(QuestionRequest);
        }
      
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> DeleteQuestion(int id)
        {
            return await _questionService.DeleteQuestionAsync(id);
        }
    }
}
