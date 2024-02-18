using QuizVistaApiBusinnesLayer.Models.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class TagRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tag name is required.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Tag name must be between 1 and 20 characters long.")]
        public string Name { get; set; } = string.Empty;

        private TagRequest() { }

        public TagRequest(int id,
            string name)
        {
            Id = id;
            Name = name;
        }


    }
}
