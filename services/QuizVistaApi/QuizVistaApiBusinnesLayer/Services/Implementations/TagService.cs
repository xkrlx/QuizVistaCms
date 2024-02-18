using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tagRepository;

        public TagService(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Result> CreateTag(TagRequest tag)
        {
            Tag entity = tag.ToEntity();
            entity.Id = default;

            await _tagRepository.InsertAsync(entity);

            return Result.Ok();
        }

        public async Task<Result> DeleteTag(int id)
        {
            await _tagRepository.DeleteAsync(id);

            return Result.Ok();
        }

        public async Task<ResultWithModel<TagResponse>> GetTag(int id)
        {
            var tag = await _tagRepository.GetAsync(id);

            if(tag is null)
                throw new ArgumentNullException($"tag #{id} not found");

            return ResultWithModel<TagResponse>.Ok(tag.ToResponse());
        }

        public async Task<ResultWithModel<IEnumerable<TagResponse>>> GetTags()
        {
            var tags = await _tagRepository.GetAll()
                .OrderBy(x => x.Id)
                .ToListAsync();

            if(tags is null)
                throw new ArgumentNullException(nameof(tags));

            return ResultWithModel<IEnumerable<TagResponse>>.Ok(tags.ToCollectionResponse().ToList());
        }

        public async Task<ResultWithModel<IEnumerable<TagResponse>>> GetTagsForQuiz(int quizId)
        {
            var tags = await _tagRepository.GetAll()
                .Include(x => x.Quizzes)
                .Where(x => x.Id == quizId)
                .OrderBy(x => x.Id)
                .ToListAsync();

            if(tags is null)
                throw new ArgumentNullException($"tags for quiz #{quizId} not found");

            return ResultWithModel<IEnumerable<TagResponse>>.Ok(tags.ToCollectionResponse().ToList());
        }

        public async Task<Result> UpdateTag(TagRequest tag)
        {
            await _tagRepository.UpdateAsync(tag.ToEntity());

            return Result.Ok();
        }
    }
}
