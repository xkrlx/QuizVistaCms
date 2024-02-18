using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests.AttemptRequests
{
    public class AttemptRequest
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public List<AnswerRequest> Answers { get; set; }

        public AttemptRequest()
        {
            Answers = new List<AnswerRequest>();
        }

        public AttemptRequest(int id, int userId, List<AnswerRequest> answers)
        {
            Id = id;
            UserId = userId;
            Answers = answers ?? new List<AnswerRequest>();
        }
    }
}
