using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests.QuestionRequests;
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
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _questionRepository;

        public QuestionService(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Result> CreateQuestionAsync(QuestionRequest question)
        {
            await _questionRepository.InsertAsync(question.ToEntity());

            return Result.Ok();
        }

        public async Task<Result> DeleteQuestionAsync(int questionId)
        {
            await _questionRepository.DeleteAsync(questionId);

            return Result.Ok();
        }

        public async Task<ResultWithModel<QuestionResponse>> GetQuestion(int questionId)
        {
            var question = await _questionRepository.GetAsync(questionId);

            if(question is null) 
                throw new ArgumentNullException($"question #{questionId} not found");

            return ResultWithModel<QuestionResponse>.Ok(question.ToResponse());
        }

        public async Task<ResultWithModel<IEnumerable<QuestionResponse>>> GetQuestions()
        {
            var questions = await _questionRepository.GetAll()
                .OrderBy(x => x.Id)
                .ToListAsync();

            return ResultWithModel<IEnumerable<QuestionResponse>>.Ok(questions.ToCollectionResponse().ToList());
        }

        public async Task<ResultWithModel<IEnumerable<QuestionResponse>>> GetQuestionsForQuiz(int quizId)
        {
            var questions = await _questionRepository.GetAll()
                .Where(x=>x.QuizId == quizId)
                .OrderBy(x=> x.Id)
                .ToListAsync();

            if(questions is null)
                throw new ArgumentNullException($"questions for quiz #{quizId} not found");

            return ResultWithModel<IEnumerable<QuestionResponse>>.Ok(questions.ToCollectionResponse().ToList());

        }

        public Task<ResultWithModel<QuestionResponse>> GetQuestionWithAnswers(int questionId)
        {
            var questionExtended = _questionRepository.GetAll()
                .Include(x => x.Answers)
                .FirstOrDefault(x => x.Id == questionId);

            if(questionExtended is null)
                throw new ArgumentNullException($"question #{questionId} not found");

            return Task.FromResult(ResultWithModel<QuestionResponse>.Ok(questionExtended.ToResponse()));

        }

        public async Task<Result> UpdateQuestionAsync(QuestionRequest question)
        {
            var existingQuestion = await _questionRepository.GetAll().FirstOrDefaultAsync(x=>x.Id==question.Id);

            if (existingQuestion == null)
            {
                return Result.Failed("Pytanie nie istnieje.");
            }

            var updatedQuestion = question.ToEntity();

            existingQuestion.Type = updatedQuestion.Type;
            existingQuestion.Text = updatedQuestion.Text;
            existingQuestion.AdditionalValue = updatedQuestion.AdditionalValue;
            existingQuestion.SubstractionalValue = updatedQuestion.SubstractionalValue;
            existingQuestion.CmsTitleStyle = updatedQuestion.CmsTitleStyle;
            existingQuestion.CmsQuestionsStyle = updatedQuestion.CmsQuestionsStyle;
            existingQuestion.Answers = updatedQuestion.Answers;



            await _questionRepository.UpdateAsync(existingQuestion);

            return Result.Ok();
        }
    }
}
