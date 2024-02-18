using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Responses.UserResponses
{
    public class UserDetailsResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserDetailsResponse() { }
        public UserDetailsResponse(string userName, string email, string firstName, string lastName)
        {
            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
