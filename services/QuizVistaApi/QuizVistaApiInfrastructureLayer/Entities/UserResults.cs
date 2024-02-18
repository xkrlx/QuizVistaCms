using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiInfrastructureLayer.Entities
{
    [Entity]
    public class UserResults
    {
        public int UserId { get; set; }
        public string QuizName { get; set; }
        public int? QuizId { get; set; }
        public int? AttemptId { get; set; }
        public int? QuestionId { get; set; }
        public DateTime? RegDate { get; set; }
        public string Type { get; set; }
        public int? AdditionalValue { get; set; }
        public int SubstractionalValue { get; set; }
        public int UserCorrectAnswers { get; set; }
        public int UserWrongAnswers { get; set; }
        public int MaxCorrectAnswers { get; set; }
        public int MaxWrongAnswers { get; set; }
    }
}
