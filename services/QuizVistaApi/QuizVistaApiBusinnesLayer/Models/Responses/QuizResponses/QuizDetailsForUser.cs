using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses
{
    public class QuizDetailsForUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public int UserAttempts { get; set; }
        public int AttemptsLimit { get; set; }
        public List<TagResponse> Tags { get; set; }

    }
}
