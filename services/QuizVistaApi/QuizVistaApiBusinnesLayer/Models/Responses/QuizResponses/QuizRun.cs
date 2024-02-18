using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses
{
    public class QuizRun
    {
        public string Name { get; set; }
        public int UserAttemptCount { get; set; }
        public string AuthorName { get; set; }
        public List<QuestionRun> Questions { get; set; }

        public class QuestionRun
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string Type { get; set; }
            public int AdditionalValue { get; set; }
            public int? SubstractionalValue { get; set; }
            public string CmsTitleValue { get; set; }
            public string CmsQuestionsValue { get; set; }
            public List<AnswerRun> Answers { get; set; }

            public class AnswerRun
            {
                public int Id { get; set; }
                public string Text { get; set; }

            }
        }
    }
}
