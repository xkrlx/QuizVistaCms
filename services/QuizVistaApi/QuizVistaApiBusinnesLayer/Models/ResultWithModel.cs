using System;
using System.Collections.Generic;
using System.Text;

namespace QuizVistaApiBusinnesLayer.Models
{
    public class ResultWithModel<T> : ResultBase where T : class
    {
        public T? Model { get; }


        protected ResultWithModel(T model): base()
        {
            Model = model;
        }

        protected ResultWithModel(string errorMessage): base(errorMessage)
        {
            Model = null;
        }

        public static ResultWithModel<T> Ok(T model)
        {
            return new ResultWithModel<T>(model);
        }

        public static ResultWithModel<T> Failed(string errorMessage)
        {
            return new ResultWithModel<T>(errorMessage);
        }

    }
}
