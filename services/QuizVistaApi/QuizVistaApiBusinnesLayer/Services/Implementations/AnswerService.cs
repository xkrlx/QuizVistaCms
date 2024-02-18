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
    public class AnswerService : IAnswerService
    {
        private readonly IRepository<Answer> _answerRepository;

        public AnswerService(IRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Result> CreateAnswerAsync(AnswerRequest answerRequest)
        {
            Answer answer = answerRequest.ToEntity();

            await _answerRepository.InsertAsync(answer);

            return Result.Ok();
        }

        public async Task<Result> DeleteAnswerAsync(int answerId)
        {
            await _answerRepository.DeleteAsync(answerId);

            return Result.Ok();
        }

        public async Task<ResultWithModel<AnswerResponse>> GetAnswer(AnswerRequest answerRequest)
        {
            var answer = await _answerRepository.GetAsync(answerRequest.QuestionId);

            if(answer is null)
                throw new ArgumentNullException($"answer #{answer} not found");

            return ResultWithModel<AnswerResponse>.Ok(answer.ToResponse());
        }

        public Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswers()
        {
            return Task.FromResult(ResultWithModel<IEnumerable<AnswerResponse>>.Ok(_answerRepository.GetAll().ToCollectionResponse().ToList()));
        }

        public async Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswersForQuestion(int questionId)
        {
            var answers = await  _answerRepository
                .GetAll()
                .Where(x=>x.QuestionId == questionId)
                .OrderBy(x=>x.Id)
                .ToListAsync();

            if (answers is null)
                throw new ArgumentNullException($"answers for #{questionId} question not found");

            return await Task.FromResult(ResultWithModel<IEnumerable<AnswerResponse>>.Ok(answers.ToCollectionResponse().ToList()));
            
        }
        
        public async Task<Result> UpdateAnswerAsync(AnswerRequest answerRequest)
        {
            Answer answer = answerRequest.ToEntity();
            await _answerRepository.UpdateAsync(answer);

            return Result.Ok();
        }


    }
}
