using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Responses.AttemptResponses
{
    public class UserResultBriefResponse
    {
        public string UserName { get; set; } = string.Empty;
        public List<QuizResultBrief> Quizzes { get; set; } = new List<QuizResultBrief>();

        public class QuizResultBrief
        {
            public string QuizName { get; set; } = string.Empty;
            public List<AttemptResultBrief> Attempts { get; set; } = new List<AttemptResultBrief>();

            public class AttemptResultBrief
            {
                public DateTime? Attempt_date { get; set; }
                public int PointsScored { get; set; }
                public int PointsTotal { get; set; }
                public string PercentageString { get; set; } = string.Empty;
                public int AnswersCorrect { get; set; }
                public int AnswersWrong { get; set; }
                public int AnswersMixed { get; set; }
            }
        }
    }
}
