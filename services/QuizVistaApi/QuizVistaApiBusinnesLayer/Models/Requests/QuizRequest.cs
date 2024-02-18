using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class QuizRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Quiz name is required.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Quiz name must be between 1 and 40 characters long.")]
        public string Name { get; set; } = null!;
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }
        public string? CmsTitleStyle { get; set; }
        public bool IsActive { get; set; }
        public int AttemptCount { get; set; }
        public bool? PublicAccess { get; set; }
        //public int AuthorId { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();




        public QuizRequest() { }

        public QuizRequest(int id, string name, string? description, int categoryId, string? cmsTitleStyle, bool isActive, int attemptCount, bool? publicAccess,List<int> tagIds)
        {
            Id = id;
            Name = name;
            Description = description;
            CategoryId = categoryId;
            CmsTitleStyle = cmsTitleStyle;
            //AuthorId = authorId;
            IsActive = isActive;
            AttemptCount = attemptCount;
            PublicAccess = publicAccess;
            TagIds = tagIds;
        }
    }
}
