using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Requests.QuestionRequests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using System.Collections.Generic;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswers()
        {
            return await _answerService.GetAnswers();
        }

        [HttpGet("answer")]
        [Authorize(Roles = "Moderator")]
        public async Task<ResultWithModel<AnswerResponse>> GetAnswer([FromBody] AnswerRequest answerRequest)
        {
            return await _answerService.GetAnswer(answerRequest);
        }

        [HttpGet("answerForQuestion")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswersForQuestion([FromBody] QuestionRequest questionRequest)
        {
            return await _answerService.GetAnswersForQuestion(questionRequest.Id);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> CreateAnswer([FromBody] AnswerRequest answerRequest)
        {
            var result = await _answerService.CreateAnswerAsync(answerRequest);
            return Ok(result);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> EditAnswer([FromBody] AnswerRequest answerRequest)
        {
            return await _answerService.UpdateAnswerAsync(answerRequest);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> DeleteAnswer(int id)
        {
            return await _answerService.DeleteAnswerAsync(id);
        }

    }
}
