using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [Authorize(Roles="User")]
        public async Task<ResultWithModel<IEnumerable<TagResponse>>> GetAll()
        {
            return await _tagService.GetTags();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<TagResponse>> GetTag(int id)
        {
            return await _tagService.GetTag(id);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> CreateTag([FromBody] TagRequest tagRequest)
        {
            return await _tagService.CreateTag(tagRequest);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> EditTag([FromBody] TagRequest tagRequest)
        {
            return await _tagService.UpdateTag(tagRequest);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> DeleteTag(int id)
        {
            return await _tagService.DeleteTag(id);
        }

    }
}
