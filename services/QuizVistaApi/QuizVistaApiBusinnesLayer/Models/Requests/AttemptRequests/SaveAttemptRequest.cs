using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests.AttemptRequests
{
    public class SaveAttemptRequest
    {
        public List<int> AnswerIds { get; set; }
    }
}
