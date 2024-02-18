using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests.AttemptRequests;
using QuizVistaApiBusinnesLayer.Models.Responses.AttemptResponses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class AttemptService : IAttemptService
    {
        private readonly IRepository<Attempt> _attemptRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<UserResults> _userResultsRepository;

        public AttemptService(
            IRepository<Attempt> attemptRepository,
            IRepository<User> userRepository,
            IRepository<Answer> answerRepository,
            IRepository<UserResults> userResultsRepository
            )
        {
            _attemptRepository = attemptRepository;
            _userRepository = userRepository;
            _answerRepository = answerRepository;
            _userResultsRepository = userResultsRepository;
        }

        public async Task<ResultWithModel<AttemptResponse>> GetAttempt(int id)
        {
            var attempt = await _attemptRepository.GetAsync(id);

            if (attempt is null)
                throw new ArgumentException($"attempt #{id} not found");

            return ResultWithModel<AttemptResponse>.Ok(attempt.ToResponse());

        }

        public async Task<ResultWithModel<IEnumerable<AttemptResponse>>> GetAttemptsOfUser(int userId)
        {
            var attempts = await _attemptRepository.GetAll()
                .Where(x=>x.UserId == userId)
                .ToListAsync();

            if (attempts is null)
                throw new ArgumentException($"attemps of user #{userId} not found");

            return ResultWithModel<IEnumerable<AttemptResponse>>.Ok(attempts.ToCollectionResponse().ToList());
        }

        public async Task<ResultWithModel<AttemptResponse>> GetAttemptWithAnswers(int id)
        {
            var attempt = await _attemptRepository.GetAll()
                .Include(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (attempt is null)
                throw new ArgumentException($"attempt #{id} not found");

            return ResultWithModel<AttemptResponse>.Ok(attempt.ToResponse());
        }

        public async Task<ResultWithModel<UserResultBriefResponse>> GetUserResults(string userName)
        {

            User? user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x=>x.UserName == userName);

            if (user is null) throw new ArgumentNullException($"user {userName} not found");

            IList<UserResults> results = await _userResultsRepository.GetAll()
                .Where(x=>x.UserId == user.Id).ToListAsync();


            //modeling user results response

            var groupedResults = results
                .GroupBy(x => x.QuizName)
                .Select(x => x.GroupBy(y => y.AttemptId))
                .ToImmutableList();

            List<UserResultBriefResponse.QuizResultBrief> quizResultList = new List<UserResultBriefResponse.QuizResultBrief>();  
            if(groupedResults.Count > 0)
            {
                foreach (var quizGroup in groupedResults)
                {
                    string quizName = quizGroup.First()?.First()?.QuizName ?? string.Empty;
                    List<UserResultBriefResponse.QuizResultBrief.AttemptResultBrief> attemptResultBriefs = new List<UserResultBriefResponse.QuizResultBrief.AttemptResultBrief>();

                    foreach (var attemptGroup in quizGroup)
                    {
                        int userPointsSum = 0;
                        int answersCorrectCount = 0, answersWrongCount = 0, answersMixedCount = 0;
                        foreach (var questionItem in attemptGroup)
                        {
                            int questionPointSum = questionItem.Type switch
                            {
                                "1" => (questionItem.UserCorrectAnswers >= 1) ? (questionItem.AdditionalValue ?? 0) : -questionItem.SubstractionalValue, //true, false
                                "2" => (questionItem.UserCorrectAnswers >= 1) ? (questionItem.AdditionalValue ?? 0) : -questionItem.SubstractionalValue, //single good answer
                                "3" => (int)
                                    (
                                        ((float)questionItem.UserCorrectAnswers / (questionItem.MaxCorrectAnswers == 0 ? 1 : questionItem.MaxCorrectAnswers) * (questionItem.AdditionalValue ?? 0)) - //sum of good answers
                                        ((float)questionItem.UserWrongAnswers / (questionItem.MaxWrongAnswers == 0 ? 1 : questionItem.MaxWrongAnswers) * -questionItem.SubstractionalValue) // sum of bad answers
                                    ), //multiple good answers
                                _ => 0
                            };

                            //if multi choice and negative points, then return 0
                            userPointsSum += questionPointSum <= 0 && questionItem.Type == "3" ? 0 : questionPointSum;

                            #region counting answers

                            if(questionItem.Type == "1" || questionItem.Type == "2")
                            {
                                if (questionItem.UserCorrectAnswers > 0) answersCorrectCount++;
                                else answersWrongCount++;
                            }
                            else if(questionItem.Type == "3")
                            {
                                if (questionItem.UserCorrectAnswers == questionItem.MaxCorrectAnswers) answersCorrectCount++;
                                else if (questionItem.UserWrongAnswers == questionItem.MaxWrongAnswers) answersWrongCount++;
                                else answersMixedCount++;
                            }

                            #endregion
                        }

                        int pointsTotal = attemptGroup.Sum(x => x.AdditionalValue) ?? 0;

                        UserResultBriefResponse.QuizResultBrief.AttemptResultBrief attemptResultBrief = new UserResultBriefResponse.QuizResultBrief.AttemptResultBrief
                        {
                            Attempt_date = attemptGroup.Min(x => x.RegDate),
                            PointsTotal = pointsTotal,
                            PointsScored = userPointsSum,
                            PercentageString = string.Format("{0:0.##}%", (float)userPointsSum / pointsTotal * 100),
                            AnswersCorrect = answersCorrectCount,
                            AnswersWrong = answersWrongCount,
                            AnswersMixed = answersMixedCount
                        };
                        attemptResultBriefs.Add(attemptResultBrief);
                    }



                    UserResultBriefResponse.QuizResultBrief quizBriefItem = new UserResultBriefResponse.QuizResultBrief
                    {
                        QuizName = quizName ?? string.Empty,
                        Attempts = attemptResultBriefs.OrderByDescending(x=>x.Attempt_date).ToList()
                    };
                    quizResultList.Add(quizBriefItem);
                }

            }


            UserResultBriefResponse userResultBrief = new UserResultBriefResponse { 
                UserName = user.UserName,
                Quizzes = quizResultList.OrderBy(x=>x.QuizName).ToList()
            };

            return ResultWithModel<UserResultBriefResponse>.Ok(userResultBrief);
        }


        public async Task<ResultWithModel<QuizResultBriefResponse>> GetQuizResults(string quizName)
        {
            IList<UserResults> results = await _userResultsRepository.GetAll()
                .Where(x => x.QuizName == quizName).ToListAsync();

            if (!results.Any()) throw new ArgumentNullException($"No results found for quiz {quizName}");

            var groupedResults = results
                .GroupBy(x => x.QuizName)
                .Select(x => x.GroupBy(y => y.AttemptId))
                .ToImmutableList();

            List<QuizResultBriefResponse.UserResultBrief> quizResultList = new List<QuizResultBriefResponse.UserResultBrief>();
            if (groupedResults.Count > 0)
            {
                foreach (var quizGroup in groupedResults)
                {
                    foreach (var attemptGroup in quizGroup)
                    {
                        int userId = attemptGroup.First().UserId;
                        var user = await _userRepository.GetAsync(attemptGroup.First().UserId);
                        string userName = user?.UserName ?? "Unknown User";
                        string quiz = attemptGroup.First()?.QuizName ?? string.Empty;
                        int userPointsSum = 0;
                        int answersCorrectCount = 0, answersWrongCount = 0, answersMixedCount = 0;

                        foreach (var questionItem in attemptGroup)
                        {
                            int questionPointSum = questionItem.Type switch
                            {
                                "1" => (questionItem.UserCorrectAnswers >= 1) ? (questionItem.AdditionalValue ?? 0) : -questionItem.SubstractionalValue,
                                "2" => (questionItem.UserCorrectAnswers >= 1) ? (questionItem.AdditionalValue ?? 0) : -questionItem.SubstractionalValue,
                                "3" => (int)
                                    (
                                        ((float)questionItem.UserCorrectAnswers / (questionItem.MaxCorrectAnswers == 0 ? 1 : questionItem.MaxCorrectAnswers) * (questionItem.AdditionalValue ?? 0)) -
                                        ((float)questionItem.UserWrongAnswers / (questionItem.MaxWrongAnswers == 0 ? 1 : questionItem.MaxWrongAnswers) * -questionItem.SubstractionalValue)
                                    ),
                                _ => 0
                            };

                            userPointsSum += questionPointSum <= 0 && questionItem.Type == "3" ? 0 : questionPointSum;

                            if (questionItem.Type == "1" || questionItem.Type == "2")
                            {
                                if (questionItem.UserCorrectAnswers > 0) answersCorrectCount++;
                                else answersWrongCount++;
                            }
                            else if (questionItem.Type == "3")
                            {
                                if (questionItem.UserCorrectAnswers == questionItem.MaxCorrectAnswers) answersCorrectCount++;
                                else if (questionItem.UserWrongAnswers == questionItem.MaxWrongAnswers) answersWrongCount++;
                                else answersMixedCount++;
                            }
                        }

                        int pointsTotal = attemptGroup.Sum(x => x.AdditionalValue) ?? 0;

                        QuizResultBriefResponse.UserResultBrief.AttemptResultBrief_ attemptResultBrief = new QuizResultBriefResponse.UserResultBrief.AttemptResultBrief_
                        {
                            Attempt_date = attemptGroup.Min(x => x.RegDate),
                            PointsTotal = pointsTotal,
                            PointsScored = userPointsSum,
                            PercentageString = string.Format("{0:0.##}%", (float)userPointsSum / pointsTotal * 100),
                            AnswersCorrect = answersCorrectCount,
                            AnswersWrong = answersWrongCount,
                            AnswersMixed = answersMixedCount
                        };

                        QuizResultBriefResponse.UserResultBrief quizBriefItem = new QuizResultBriefResponse.UserResultBrief
                        {
                            UserName = userName,
                            Attempts = new List<QuizResultBriefResponse.UserResultBrief.AttemptResultBrief_> { attemptResultBrief }
                        };

                        quizResultList.Add(quizBriefItem);
                    }
                }
            }

            QuizResultBriefResponse response = new QuizResultBriefResponse
            {
                QuizName = quizName,
                Quizzes = quizResultList
            };

            return ResultWithModel<QuizResultBriefResponse>.Ok(response);
        }





        public async Task<Result> SaveAttempt(SaveAttemptRequest attempt, string userName)
        {
            User? user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (user is null) throw new ArgumentNullException($"user {userName} not found");

            var answers = await _answerRepository.GetAll()
                .Where(x => attempt.AnswerIds.Contains(x.Id))
                .ToListAsync();

            if (answers is null) throw new ArgumentNullException($"answers not found");


            Attempt entity = new Attempt
            {
                CreateDate = DateTime.Now,
                EditionDate = DateTime.Now,
                Answers = answers,
                User = user,
                UserId = user.Id
            };


            await _attemptRepository.InsertAsync(entity);


            
            return Result.Ok();
        }

        public async Task<Result> UpdateAttempt(AttemptRequest attempt)
        {
            await _attemptRepository.UpdateAsync(attempt.ToEntity());
            
            return Result.Ok();
        }

        public async Task<Result> DeleteAttempt(int id)
        {
            await _attemptRepository.DeleteAsync(id); 
            
            return Result.Ok();
        }

        
    }
}
