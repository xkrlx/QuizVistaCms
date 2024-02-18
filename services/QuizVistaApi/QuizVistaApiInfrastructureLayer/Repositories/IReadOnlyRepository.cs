using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiInfrastructureLayer.Repositories
{

    public interface IReadOnlyRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity?> GetAsync(int id);
    }
}
