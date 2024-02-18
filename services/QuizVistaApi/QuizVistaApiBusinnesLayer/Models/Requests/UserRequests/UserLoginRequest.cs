using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests.UserRequests
{
    public class UserLoginRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; } = string.Empty;

        public UserLoginRequest() { }

        public UserLoginRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
