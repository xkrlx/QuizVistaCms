using System.Collections.Generic;

namespace QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses
{
    public class QuizGenerateResponse
    {
        public string Category { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<GeneratedQuestion> Questions { get; set; }
    }

    public class GeneratedQuestion
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
