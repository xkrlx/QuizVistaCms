using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class QuizOpenQuestionRequest
    {
        public string QuestionText { get; set; }
        public string UserAnswer { get; set; }
    }
}
