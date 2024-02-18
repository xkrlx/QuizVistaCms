using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class AnswerRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Question ID is required.")]
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Answer text is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Answer text must be between 1 and 100 characters long.")]
        public string AnswerText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }

        public AnswerRequest() { }

        public AnswerRequest(int id,int questionId, string answerText, bool isCorrect)
        {
            Id = id;
            QuestionId = questionId;
            AnswerText = answerText;
            IsCorrect = isCorrect;
        }
    }
}
