using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests.UserRequests
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string ConfirmPassword { get; set; }

        public ResetPasswordRequest() { }

        public ResetPasswordRequest(string token, string password, string confirmPassword)
        {
            Token = token;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
