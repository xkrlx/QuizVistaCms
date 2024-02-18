using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class AssignUserRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        public string UserName { get; set; }
        [Required]
        public string QuizName { get; set; }

        public AssignUserRequest() { }

        public AssignUserRequest(string userName, string quizName)
        {
            UserName = userName;
            QuizName = quizName;
        }
    }
}
