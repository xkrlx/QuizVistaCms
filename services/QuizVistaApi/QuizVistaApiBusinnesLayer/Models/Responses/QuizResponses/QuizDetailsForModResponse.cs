using QuizVistaApiBusinnesLayer.Models.Responses.UserResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses
{
    public class QuizDetailsForModResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string AuthorName { get; set; }
        public int UserAttempts { get; set; }
        public int AttemptCount { get; set; }
        public bool IsActive { get; set; }
        public bool PublicAccess { get; set; }
        public bool HasAttempts { get; set; }
        public List<TagResponse> Tags { get; set; }
        public List<UserDetailsResponse> Users { get; set; }
    }
}
