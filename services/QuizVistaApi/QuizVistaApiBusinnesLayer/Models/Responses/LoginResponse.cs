using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Responses
{
    public class LoginResponse
    {
        public string Token {  get; set; }

        LoginResponse() { }

        public LoginResponse(string token) { 
            Token = token;
        }
    }
}
