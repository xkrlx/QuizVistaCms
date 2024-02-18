using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests.UserRequests
{
    public class GetUserRequest
    {
        [Required]
        public string UserId { get; set; }

        public GetUserRequest() { }

        public GetUserRequest(string UserId) {  this.UserId = UserId; }
    }
}
