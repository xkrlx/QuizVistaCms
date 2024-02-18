using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models
{
    public class Result : ResultBase
    {
        protected Result() : base() { }

        protected Result(string error) : base(error) { }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result Failed(string errorMessage) {
            return new Result(errorMessage);
        }


    }
}
