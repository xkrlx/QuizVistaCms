using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Responses.AttemptResponses
{
    public class QuizResultBriefResponse
    {
        public string QuizName { get; set; } = string.Empty;
        public List<UserResultBrief> Quizzes { get; set; } = new List<UserResultBrief>();

        public class UserResultBrief
        {
            public string UserName { get; set; } // Dodane pole UserId
            public List<AttemptResultBrief_> Attempts { get; set; } = new List<AttemptResultBrief_>();

            public class AttemptResultBrief_
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
