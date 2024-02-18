using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests.UserRequests
{
    public class ResetPasswordInitialRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(80, ErrorMessage = "Email cannot be longer than 80 characters.")]
        public string Email { get; set; }

        public ResetPasswordInitialRequest() { }

        public ResetPasswordInitialRequest(string email)
        {
            Email = email;
        }
    }
}
