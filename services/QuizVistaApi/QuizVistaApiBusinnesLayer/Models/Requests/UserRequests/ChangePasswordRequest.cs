using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests.UserRequests
{
    public class ChangePasswordRequest
    {
        public string? ValidateUserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string CurrentPassword { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string NewPassword { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string ConfirmNewPassword { get; set; }

        public ChangePasswordRequest() { }

        public ChangePasswordRequest(string validateUserName, string currentPassword, string newPassword, string confirmNewPassword)
        {
            ValidateUserName = validateUserName;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmNewPassword = confirmNewPassword;
        }
    }

}
