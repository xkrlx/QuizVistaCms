using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<CategoryResponse>>> GetCategories();
        Task<ResultWithModel<CategoryResponse>> GetCategory(int categoryId);

        Task<Result> DeleteCategory(int id);
        Task<Result> CreateCategory(CategoryRequest category);
        Task<Result> UpdateCategory(CategoryRequest category);


        #endregion
    }
}
