using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;
using QuizVistaApiBusinnesLayer.Models.Responses.UserResponses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class QuizService : IQuizService
    {
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Attempt> _attemptRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<AttemptCount> _attemptCountRepository;
        private readonly IConfiguration _configuration;
        public QuizService(IRepository<Quiz> quizRepository,
            IRepository<User> userRepository,
            IRepository<Tag> tagRepository,
            IRepository<Attempt> attemptRepository,
            IRepository<Answer> answerRepository,
            IRepository<AttemptCount> attemptCountRepository,
            IConfiguration configuration)
        {
            _quizRepository = quizRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _attemptRepository = attemptRepository;
            _answerRepository = answerRepository;
            _attemptCountRepository = attemptCountRepository;
            _configuration = configuration;
        }

        public async Task<Result> CreateQuizAsync(string userId, QuizRequest quizToCreate)
        {
            var entity = quizToCreate.ToEntity();

            var user = await _userRepository.GetAll().Where(x=>x.UserName == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result.Failed("User ID is missing.");
            }

            /*List<User> users = await _userRepository.GetAll().ToListAsync();
            bool emailCheck = users.Any(x => x.Email.ToLower() == request.Email.ToLower());*/

            var quiz_exist = await _quizRepository.GetAll().FirstOrDefaultAsync(x => x.Name == quizToCreate.Name);

            if (quiz_exist != null)
            {
                return Result.Failed("Nazwa quizu jest zajęta");
            }

            

            entity.AuthorId = user.Id;

            entity.CreationDate = DateTime.Now;
            entity.EditionDate = DateTime.Now;

            entity.Tags = new List<Tag>();

            var existingTags = await _tagRepository.GetAll()
                .Where(tag => quizToCreate.TagIds.Contains(tag.Id))
                .ToListAsync();

            foreach (var tag in existingTags)
            {
                entity.Tags.Add(tag);
            }

            await _quizRepository.InsertAsync(entity);

            return Result.Ok();
        }

        public async Task<Result> DeleteQuizAsync(string userName, int idToDelete)
        {
            var quiz = await _quizRepository.GetAll()
                .Include(q => q.Tags) 
                .Include(q=>q.Questions)
                .Include(q=>q.Users)
                .Where(x => x.Id == idToDelete)
                .FirstOrDefaultAsync();

            var user = await _userRepository.GetAll().Where(x => x.UserName == userName).FirstOrDefaultAsync();

            if (quiz == null)
            {
                return Result.Failed("Quiz not found.");
            }

            if (user == null)
            {
                return Result.Failed("User not found.");
            }

            if (user.Id != quiz.AuthorId)
            {
                return Result.Failed("Unauthorized user.");
            }

            quiz.Questions.Clear();
            quiz.Tags.Clear();
            quiz.Users.Clear();

            await _quizRepository.SaveChangesAsync();


            await _quizRepository.DeleteAsync(idToDelete);

            return Result.Ok();
        }

        public async Task<ResultWithModel<QuizResponse>> GetQuizAsync(int id)
        {
            var quiz = await _quizRepository.GetAsync(id);

            if(quiz is null)
                throw new ArgumentNullException($"quiz #{id} not found");

            return ResultWithModel<QuizResponse>.Ok(quiz.ToResponse());
        }

        public async Task<ResultWithModel<IEnumerable<QuizResponse>>> GetQuizesAsync()
        {
            var quizes = await _quizRepository
                .GetAll()
                //.Include(x=>x.Author)
                .OrderBy(x=>x.Id)
                .ToListAsync();

            if(quizes is null)
                throw new ArgumentNullException(nameof(quizes));

            var quizesResponses = quizes.ToCollectionResponse().ToList();

            return ResultWithModel<IEnumerable<QuizResponse>>.Ok(quizesResponses);

        }

        public async Task<ResultWithModel<QuizRun>> GetQuizWithQuestionsAsync(string quizName, string userName)
        {
            var quiz = await _quizRepository.GetAll()
                .Include(x => x.Author)
                .Include(x => x.Questions)
                .FirstOrDefaultAsync( x=> x.Name == quizName);

            if (quiz is null)
                throw new ArgumentNullException($"quiz {quizName} not found");

            foreach(var question in quiz.Questions)
            {
                List<Answer> answers = await _answerRepository.GetAll()
                    .Where(x=>x.QuestionId == question.Id)
                    .ToListAsync();

                question.Answers = answers;
            }

            User? user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);

            if (user is null) throw new ArgumentNullException($"user {userName} does not exist");

            var attemptCount = await _attemptCountRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id && x.QuizId == quiz.Id);



            QuizRun res = new QuizRun
            {
                AuthorName = quiz.Author is null ? "" : $"{quiz.Author.FirstName} {quiz.Author.LastName}",
                Name = quizName,
                UserAttemptCount = attemptCount?.AttemptCountNumber ?? 0,
                Questions = quiz.Questions.Select(x=>new QuizRun.QuestionRun
                {
                    Id = x.Id,
                    Text = x.Text,
                    AdditionalValue = x.AdditionalValue,
                    SubstractionalValue = x.SubstractionalValue,
                    CmsTitleValue = x.CmsTitleStyle,
                    CmsQuestionsValue = x.CmsQuestionsStyle,
                    Type = x.Type,
                    Answers = x.Answers.Select(y=>new QuizRun.QuestionRun.AnswerRun
                    {
                        Id = y.Id,
                        Text = y.AnswerText
                    }).ToList()
                    
                    
                }).ToList()
            };


            return ResultWithModel<QuizRun>.Ok(res);
        }

        public async Task<ResultWithModel<QuizWithQuestionsModResponse>> GetQuestionsForQuizMod(string quizName, string userName)
        {
            var quiz = await _quizRepository.GetAll()
                .Include(x => x.Author)
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(x => x.Name == quizName);

            if (quiz is null)
                throw new ArgumentNullException($"quiz {quizName} not found");

            foreach (var question in quiz.Questions)
            {
                List<Answer> answers = await _answerRepository.GetAll()
                    .Where(x => x.QuestionId == question.Id)
                    .ToListAsync();

                question.Answers = answers;
            }

            User? user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);

            if (user is null) throw new ArgumentNullException($"user {userName} does not exist");

            var attemptCount = await _attemptCountRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id && x.QuizId == quiz.Id);



            QuizWithQuestionsModResponse res = new QuizWithQuestionsModResponse
            {
                AuthorName = quiz.Author is null ? "" : $"{quiz.Author.FirstName} {quiz.Author.LastName}",
                Name = quizName,
                UserAttemptCount = attemptCount?.AttemptCountNumber ?? 0,
                Questions = quiz.Questions.Select(x => new QuizWithQuestionsModResponse.QuestionMod
                {
                    Id = x.Id,
                    Text = x.Text,
                    AdditionalValue = x.AdditionalValue,
                    SubstractionalValue = x.SubstractionalValue,
                    CmsTitleValue = x.CmsTitleStyle,
                    CmsQuestionsValue = x.CmsQuestionsStyle,
                    Type = x.Type,
                    Answers = x.Answers.Select(y => new QuizWithQuestionsModResponse.QuestionMod.AnswerMod
                    {
                        Id = y.Id,
                        Text = y.AnswerText,
                        IsCorrect = y.IsCorrect,
                    }).ToList()


                }).ToList()
            };


            return ResultWithModel<QuizWithQuestionsModResponse>.Ok(res);
        }

        public async Task<Result> UpdateQuizAsync(string userId, QuizRequest quizToUpdate)
        {
            var existingQuiz = await _quizRepository.GetAll().Include(q => q.Tags).FirstOrDefaultAsync(q => q.Id == quizToUpdate.Id);

            if (existingQuiz == null)
            {
                return Result.Failed("Quiz not found.");
            }

            var user = await _userRepository.GetAll().Where(x => x.UserName == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result.Failed("User not found.");
            }

            if (existingQuiz.AuthorId != user.Id)
            {
                return Result.Failed("Unauthorized user.");
            }

            var updatedEntity = quizToUpdate.ToEntity();


            existingQuiz.Name = updatedEntity.Name;
            existingQuiz.Description = updatedEntity.Description;
            existingQuiz.CategoryId = updatedEntity.CategoryId;
            existingQuiz.CmsTitleStyle = updatedEntity.CmsTitleStyle;
            existingQuiz.IsActive = updatedEntity.IsActive;
            existingQuiz.PublicAccess = updatedEntity.PublicAccess;
            existingQuiz.AttemptCount = updatedEntity.AttemptCount;
            existingQuiz.EditionDate = DateTime.Now;

            existingQuiz.Tags.Clear();
            //await _quizRepository.UpdateAsync(existingQuiz);

            var existingTags = await _tagRepository.GetAll()
                .Where(tag => quizToUpdate.TagIds.Contains(tag.Id))
                .ToListAsync();


            foreach (var tag in existingTags)
            {
                existingQuiz.Tags.Add(tag);
            }

            await _quizRepository.UpdateAsync(existingQuiz);

            return Result.Ok();
        }

        public async Task<Result> AssignUser(AssignUserRequest assignUserRequest)
        {
            var quiz = await _quizRepository.GetAll().Include(x=>x.Users).Where(x=>x.Name==assignUserRequest.QuizName).FirstOrDefaultAsync();
            if (quiz == null)
            {
                return Result.Failed("Quiz not found.");
            }


            var user = await _userRepository.GetAll().Where(x => x.UserName.ToLower() == assignUserRequest.UserName.ToLower()).FirstOrDefaultAsync();
            if (user == null)
            {
                return Result.Failed("User not found.");
            }

            //var x = quiz.Users.Where(x=>x.Id==user.Id).FirstOrDefault();

            if (quiz.Users.Any(x=>x.Id==user.Id))
            {
                return Result.Failed("User is already assigned to this quiz.");
            }

            quiz.Users.Add(user);
            await _quizRepository.UpdateAsync(quiz);

            return Result.Ok();
        }

        public async Task<Result> UnAssignUser(AssignUserRequest assignUserRequest)
        {
            var quiz = await _quizRepository.GetAll().Include(x => x.Users).Where(x => x.Name == assignUserRequest.QuizName).FirstOrDefaultAsync();
            if (quiz == null)
            {
                return Result.Failed("Quiz not found.");
            }
            var user = await _userRepository.GetAll().Where(x => x.UserName.ToLower() == assignUserRequest.UserName.ToLower()).FirstOrDefaultAsync();
            if (user == null)
            {
                return Result.Failed("User not found.");
            }

            if (!quiz.Users.Any(x => x.Id == user.Id))
            {
                return Result.Failed("User is not assigned to this quiz.");
            }

            quiz.Users.Remove(user);
            await _quizRepository.UpdateAsync(quiz);

            return Result.Ok();
        }

        public async Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizListForUser(string userName)
        {
            User? loggedUser = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);

            if (loggedUser is null) throw new Exception($"user {userName} cannot find");

            List<Quiz> quizes = await _quizRepository.GetAll()
                .Include(x=>x.Users)
                .Include(x=>x.Category)
                .Include(x=>x.Author)
                .Include(x=>x.Tags)
                .Include(x => x.Questions)
                .ToListAsync();

            //add public quizes
            IEnumerable<Quiz> quizesAssignedToUser = quizes
                .Where(x => (x.PublicAccess == true) || x.Users.Any(y => y.Id == loggedUser.Id))
                .Where(x => x.IsActive)
                .Where(x=>x.Questions.Any())
                .ToList();

            IList<QuizListForUserResponse> quizesResponse = quizesAssignedToUser.Select(x => new QuizListForUserResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description ?? "",
                AuthorName = $"{x.Author.FirstName} {x.Author.LastName}",
                CategoryName = x.Category.Name,
                Tags = x.Tags.Select(y=>new TagResponse
                {
                    Id = y.Id,
                    Name = y.Name,
                    Quizzes = new List<QuizResponse>()
                }).ToList()
            }).ToList();

            return ResultWithModel<IEnumerable<QuizListForUserResponse>>.Ok(quizesResponse);
        }

        public async Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizesByCategory(string userName, string categoryName)
        {
            User? loggedUser = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);

            if (loggedUser is null) throw new Exception($"user {userName} cannot find");

            List<Quiz> quizes = await _quizRepository.GetAll()
                .Include(x => x.Users)
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Include(x => x.Tags)
                .Where(x=>x.Category.Name.ToLower() == categoryName.ToLower())
                .ToListAsync();

            //add public quizes
            IEnumerable<Quiz> quizesAssignedToUser = quizes
                .Where(x => (x.PublicAccess == true) || x.Users.Any(y => y.Id == loggedUser.Id))
                .Where(x => x.IsActive)
                .ToList();

            IList<QuizListForUserResponse> quizesResponse = quizesAssignedToUser.Select(x => new QuizListForUserResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description ?? "",
                AuthorName = $"{x.Author.FirstName} {x.Author.LastName}",
                CategoryName = x.Category.Name,
                Tags = x.Tags.Select(y => new TagResponse
                {
                    Id = y.Id,
                    Name = y.Name,
                    Quizzes = new List<QuizResponse>()
                }).ToList()
            }).ToList();

            return ResultWithModel<IEnumerable<QuizListForUserResponse>>.Ok(quizesResponse);
        }
        public async Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizesByTag(string userName, string tagName)
        {
            User? loggedUser = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);

            if (loggedUser is null) throw new Exception($"user {userName} cannot find");

            List<Quiz> quizes = await _quizRepository.GetAll()
                .Include(x => x.Users)
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Include(x => x.Tags)
                .Where(x => x.Tags.Any(y => y.Name.ToLower() == tagName.ToLower()))
                .ToListAsync();

            //add public quizes
            IEnumerable<Quiz> quizesAssignedToUser = quizes
                .Where(x => (x.PublicAccess == true) || x.Users.Any(y => y.Id == loggedUser.Id))
                .Where(x => x.IsActive)
                .ToList();

            IList<QuizListForUserResponse> quizesResponse = quizesAssignedToUser.Select(x => new QuizListForUserResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description ?? "",
                AuthorName = $"{x.Author.FirstName} {x.Author.LastName}",
                CategoryName = x.Category.Name,
                Tags = x.Tags.Select(y => new TagResponse
                {
                    Id = y.Id,
                    Name = y.Name,
                    Quizzes = new List<QuizResponse>()
                }).ToList()
            }).ToList();

            return ResultWithModel<IEnumerable<QuizListForUserResponse>>.Ok(quizesResponse);
        }

        public async Task<ResultWithModel<IEnumerable<QuizListForModResponse>>> GetQuizListForModerator(string userName)
        {
            User? loggedUser = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);

            if (loggedUser is null) throw new Exception($"user {userName} cannot find");

            List<Quiz> quizes = await _quizRepository.GetAll()
                .Include(x => x.Users)
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Include(x => x.Tags)
                .Include(x=>x.Questions)
                .ThenInclude(x=>x.Answers)
                .ThenInclude(x=>x.Attempts)
                .Where(x=>x.AuthorId==loggedUser.Id)
                .ToListAsync();


            IList<QuizListForModResponse> quizesResponse = quizes.Select(x => new QuizListForModResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description ?? "",
                AuthorName = $"{x.Author.FirstName} {x.Author.LastName}",
                CategoryName = x.Category.Name,
                HasAttempts = x.Questions.Any(q => q.Answers.Any(a => a.Attempts.Any())),
                Tags = x.Tags.Select(y => new TagResponse
                {
                    Id = y.Id,
                    Name = y.Name,
                    Quizzes = new List<QuizResponse>()
                }).ToList()
            }).ToList();



            return ResultWithModel<IEnumerable<QuizListForModResponse>>.Ok(quizesResponse);
        }

        public async Task<ResultWithModel<QuizDetailsForUser>> GetQuizDetailsForUser(string quizName, string userName)
        {
            //getting quiz props
            Quiz? quiz = await _quizRepository.GetAll()
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Name == quizName);

            if (quiz is null) throw new ArgumentException($"quiz {quizName} does not exist");


            //getting user attempts count
            User? user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (user is null) throw new ArgumentException($"user does not exist");
            
            var attemptCount = await _attemptCountRepository.GetAll().FirstOrDefaultAsync(x=>x.UserId == user.Id && x.QuizId == quiz.Id);


            QuizDetailsForUser quizDetails = new QuizDetailsForUser
            {
                Id = quiz.Id,
                Name = quiz.Name,
                Description = quiz.Description ?? "",
                AuthorName = (quiz.Author is null) ? "" : $"{quiz.Author.FirstName} {quiz.Author.LastName}",
                CategoryName = quiz.Category?.Name ?? "",
                AttemptsLimit = quiz.AttemptCount,
                Tags = quiz.Tags.Select(x => new TagResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quizzes = new List<QuizResponse>()
                }).ToList(),
                UserAttempts = attemptCount?.AttemptCountNumber ?? 0,
            };



            return ResultWithModel<QuizDetailsForUser>.Ok(quizDetails);

        }


        public async Task<ResultWithModel<QuizDetailsForModResponse>> GetQuizDetailsForMod(string quizName, string userName)
        {
            //getting quiz props
            Quiz? quiz = await _quizRepository.GetAll()
                .Include(x => x.Author)
                .Include(x => x.Users)
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .ThenInclude(x => x.Attempts)
                .FirstOrDefaultAsync(x => x.Name == quizName);

            if (quiz is null) throw new ArgumentException($"quiz {quizName} does not exist");


            //getting user attempts count
            User? user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (user is null) throw new ArgumentException($"user does not exist");

            var attemptCount = await _attemptCountRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id && x.QuizId == quiz.Id);


            QuizDetailsForModResponse quizDetails = new QuizDetailsForModResponse
            {
                Id = quiz.Id,
                Name = quiz.Name,
                Description = quiz.Description ?? "",
                AuthorName = (quiz.Author is null) ? "" : $"{quiz.Author.FirstName} {quiz.Author.LastName}",
                CategoryId = quiz.Category.Id,
                AttemptCount = quiz.AttemptCount,
                PublicAccess = quiz.PublicAccess ?? false,
                IsActive = quiz.IsActive,
                HasAttempts = quiz.Questions.Any(q => q.Answers.Any(a => a.Attempts.Any())),
                Tags = quiz.Tags.Select(x => new TagResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quizzes = new List<QuizResponse>()
                }).ToList(),
                Users = quiz.Users.Select(x => new UserDetailsResponse
                {
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                }).ToList(),
                UserAttempts = attemptCount?.AttemptCountNumber ?? 0,
            };



            return ResultWithModel<QuizDetailsForModResponse>.Ok(quizDetails);

        }

        public async Task<ResultWithModel<QuizGenerateResponse>> GenerateQuizAsync(QuizGenerateRequest quizToGenerate)
        {
            var token = _configuration.GetSection("ChatGPTApiSettings:Token").Value;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var prompt = GeneratePrompt(quizToGenerate);
                Console.WriteLine("Wygenerowany prompt: " + prompt);

                var requestBody = new
                {
                    model = "gpt-3.5-turbo", 
                    messages = new[]
                    {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt }
            },
                    max_tokens = 1500, 
                    temperature = 0.7 
                };

                var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();

                    if (result != null && result.choices != null && result.choices.Count > 0)
                    {
                        var generatedQuiz = ParseOpenAIResponse(result);
                        if (generatedQuiz != null)
                        {
                            return ResultWithModel<QuizGenerateResponse>.Ok(generatedQuiz);
                        }
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine("Błąd podczas zapytania do OpenAI: " + errorContent);
            }

            return ResultWithModel<QuizGenerateResponse>.Ok(new QuizGenerateResponse
            {
                Category = "NoCategory",
                NumberOfQuestions = 0,
                Questions = new List<GeneratedQuestion>()
            });
        }

        private string GeneratePrompt(QuizGenerateRequest quizToGenerate)
        {
            return $@"
Generate a quiz in the category {quizToGenerate.CategoryName} with {quizToGenerate.QuestionsAmount} questions. Each question should have {quizToGenerate.AnswersAmount} possible answers. Format the response as JSON in the following structure:
{{
  ""model"": {{
    ""category"": ""{quizToGenerate.CategoryName}"",
    ""numberOfQuestions"": {quizToGenerate.QuestionsAmount},
    ""questions"": [
      {{
        ""questionText"": ""<questionText>"",
        ""answers"": [
          ""<answer1>"",
          ""<answer2>"",
          ""<answer3>"",
          ""<answer4>""
        ],
        ""correctAnswer"": ""<correctAnswer>""
      }}
      // more questions...
    ]
  }},
  ""isValid"": true,
  ""errorMessage"": """"
}}
Make sure the JSON is properly formatted.";
        }

        private QuizGenerateResponse ParseOpenAIResponse(OpenAIResponse response)
        {
            try
            {
                var responseContent = response.choices[0].message.content.Trim();
                Console.WriteLine("Odpowiedź OpenAI: " + responseContent);

                // Niestandardowy deserializator
                var jsonDocument = JsonDocument.Parse(responseContent);
                var modelElement = jsonDocument.RootElement.GetProperty("model");

                var category = modelElement.GetProperty("category").GetString();
                var numberOfQuestions = modelElement.GetProperty("numberOfQuestions").GetInt32();

                var questions = new List<GeneratedQuestion>();
                foreach (var questionElement in modelElement.GetProperty("questions").EnumerateArray())
                {
                    var questionText = questionElement.GetProperty("questionText").GetString();
                    var correctAnswer = questionElement.GetProperty("correctAnswer").GetString();

                    var answers = new List<string>();
                    foreach (var answerElement in questionElement.GetProperty("answers").EnumerateArray())
                    {
                        answers.Add(answerElement.GetString());
                    }

                    questions.Add(new GeneratedQuestion
                    {
                        QuestionText = questionText,
                        Answers = answers,
                        CorrectAnswer = correctAnswer
                    });
                }

                return new QuizGenerateResponse
                {
                    Category = category,
                    NumberOfQuestions = numberOfQuestions,
                    Questions = questions
                };
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Błąd podczas przetwarzania odpowiedzi OpenAI: " + ex.Message);
                return new QuizGenerateResponse
                {
                    Category = "NoCategory",
                    NumberOfQuestions = 0,
                    Questions = new List<GeneratedQuestion>()
                };
            }
        }




        public class OpenAIResponse
        {
            public List<Choice> choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }



    }
}
