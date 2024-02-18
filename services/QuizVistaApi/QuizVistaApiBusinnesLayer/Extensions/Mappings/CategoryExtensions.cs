using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions.Mappings
{
    public static class CategoryExtensions
    {


        public static CategoryResponse ToResponse(this Category category)
        {
            if (category is null) return null;

            return new CategoryResponse(
                category.Id,
                category.Name,
                category.Description,
                category.Quizzes.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<CategoryResponse> ToCollectionResponse(this IEnumerable<Category> categories)
        {
            if(categories is null || !categories.Any()) return Enumerable.Empty<CategoryResponse>();
            return categories.Select(ToResponse) ?? Enumerable.Empty<CategoryResponse>();
        }

        public static Category ToEntity(this CategoryRequest categoryRequest)
        {
            return new Category
            {
                Id = categoryRequest.Id,
                Name = categoryRequest.Name,
                Description = categoryRequest.Description,
            };
        }

    }
}
