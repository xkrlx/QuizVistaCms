using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Responses
{
    public class ResetPassTokenResponse
    {
        public string Token { get; set; }

        public ResetPassTokenResponse() { }

        public ResetPassTokenResponse(string token)
        {
            Token = token;
        }
    }
}
