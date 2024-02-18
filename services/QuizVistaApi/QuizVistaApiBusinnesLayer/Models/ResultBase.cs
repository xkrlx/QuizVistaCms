using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models
{
    public abstract class ResultBase
    {
        public bool IsValid { get; }
        public string ErrorMessage { get; }

        public ResultBase()
        {
            IsValid = true;
            ErrorMessage = string.Empty;
        }

        public ResultBase(string errorMessage)
        {
            IsValid = false;
            ErrorMessage = errorMessage;
        }
    }
}
