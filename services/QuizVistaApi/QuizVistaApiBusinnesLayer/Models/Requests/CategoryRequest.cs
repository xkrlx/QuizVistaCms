using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class CategoryRequest
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 40 characters long.")]
        public string Name { get; set; } = string.Empty;
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string? Description { get; set; } = string.Empty;

        public CategoryRequest() { }

        public CategoryRequest(int id,string name, string? description) { Id = id;  Name = name; Description = description; }

    }
}
