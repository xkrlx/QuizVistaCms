using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class UserUpdateRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(80, ErrorMessage = "Email cannot be longer than 80 characters.")]
        public string Email { get; set; } = string.Empty;

        public UserUpdateRequest() { }

        public UserUpdateRequest(string userName, string password, string firstName, string lastName, string email)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
