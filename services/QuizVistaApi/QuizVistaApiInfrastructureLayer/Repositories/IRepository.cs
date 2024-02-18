using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiInfrastructureLayer.Repositories
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        Task<TEntity> UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
        Task<TEntity> DeleteAsync(int id);
        Task<TEntity> InsertAsync(TEntity entity);
    }
}
