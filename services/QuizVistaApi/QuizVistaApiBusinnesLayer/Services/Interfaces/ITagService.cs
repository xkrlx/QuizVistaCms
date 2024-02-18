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
    public interface ITagService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<TagResponse>>> GetTags();
        Task<ResultWithModel<IEnumerable<TagResponse>>> GetTagsForQuiz(int quizId);
        Task<ResultWithModel<TagResponse>> GetTag(int id);
        Task<Result> CreateTag(TagRequest tag);
        Task<Result> DeleteTag(int id);
        Task<Result> UpdateTag(TagRequest tag);

        #endregion
    }
}
