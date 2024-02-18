using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests.QuestionRequests
{
    public class QuestionRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Question type is required.")]
        public string Type { get; set; } = null!;
        [Required(ErrorMessage = "Question text is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Question text must be between 1 and 100 characters long.")]
        public string Text { get; set; } = null!;
        public int AdditionalValue { get; set; }
        public int? SubstractionalValue { get; set; }
        [Required(ErrorMessage = "Quiz ID is required.")]
        public int QuizId { get; set; }
        public string? CmsTitleStyle { get; set; }
        public string? CmsQuestionsStyle { get; set; }
        public List<AnswerRequest>? Answers { get; set; } = new List<AnswerRequest>();

        public QuestionRequest() { }

        public QuestionRequest(int id, string type, string text, int additionalValue, int? substractionalValue, int quizId, string? cmsTitleStyle, string? cmsQuestionsStyle, List<AnswerRequest>? answers)
        {
            Id = id;
            Type = type;
            Text = text;
            AdditionalValue = additionalValue;
            SubstractionalValue = substractionalValue;
            QuizId = quizId;
            CmsTitleStyle = cmsTitleStyle;
            CmsQuestionsStyle = cmsQuestionsStyle;
            Answers = answers;
        }
    }
}
