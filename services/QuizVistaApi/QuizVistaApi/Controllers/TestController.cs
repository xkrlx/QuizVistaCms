using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Services.Interfaces;

namespace QuizVistaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        

        private readonly ILogger<TestController> _logger;
        private readonly IMailService _mailService;

        public TestController(ILogger<TestController> logger,IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet]
        public Result Get()
        {
            return Result.Ok();
        }

        [HttpGet("error")]
        public Result ForceError(int id)
        {
            throw new Exception("test error occured");
        }

        [HttpGet("mail-test")]
        public async Task<Result> TestMail()
        {
            MailRequest x = new MailRequest();
            x.Subject = "Test";
            x.ToEmail = "hilton.goldner31@ethereal.email";
            x.Body = "Test";
            await _mailService.SendEmailAsync(x);
            return Result.Ok();
        }
    }
}