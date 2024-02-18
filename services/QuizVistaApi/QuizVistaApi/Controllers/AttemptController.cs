using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests.AttemptRequests;
using QuizVistaApiBusinnesLayer.Models.Responses.AttemptResponses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using System.Security.Claims;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttemptController : ControllerBase
    {

        private readonly IAttemptService _attemptService;

        public AttemptController(IAttemptService attemptService)
        {
            _attemptService = attemptService;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public async Task<ResultWithModel<AttemptResponse>> GetAttempts([FromBody] AttemptRequest attemptRequest)
        {
            return await _attemptService.GetAttempt(attemptRequest.UserId);
        }

        [HttpGet("forUser")]
        [Authorize(Roles = "Moderator")]
        public async Task<ResultWithModel<IEnumerable<AttemptResponse>>> GetAttemptsForUser([FromBody] AttemptRequest attemptRequest)
        {
            return await _attemptService.GetAttemptsOfUser(attemptRequest.UserId);
        }
        
        [HttpGet("userResults")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<UserResultBriefResponse>> GetUserResults()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            return await _attemptService.GetUserResults(userName);
        }

        [HttpGet("quizResults/{quizName}")]
        public async Task<ResultWithModel<QuizResultBriefResponse>> GetQuizResults(string quizName)
        {
            return await _attemptService.GetQuizResults(quizName);
        }

        [HttpPost("create")]
        [Authorize(Roles = "User")]
        public async Task<Result> CreateAttempt([FromBody] SaveAttemptRequest attemptRequest)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            return await _attemptService.SaveAttempt(attemptRequest, userName);
        }

        /*
        [HttpPut("edit")]
        [Authorize(Roles = "User")]
        public async Task<Result> EditAttempt([FromBody] AttemptRequest attemptRequest)
        {
            return await _attemptService.SaveAttempt(attemptRequest);
        }
        */
        [HttpDelete("delete")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> DeleteAttempt([FromBody] AttemptRequest attemptRequest)
        {
            return await _attemptService.DeleteAttempt(attemptRequest.Id);
        }







    }
}
