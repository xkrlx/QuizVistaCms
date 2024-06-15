using System.ComponentModel.DataAnnotations;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class QuizGenerateRequest
    {
        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Category must be between 1 and 100 characters long.")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Questons Amoubt is required.")]
        public int QuestionsAmount { get; set; }
        public int AnswersAmount { get; set; } = 4;

    }
}
