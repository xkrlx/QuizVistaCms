using QuizVistaApiInfrastructureLayer.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiInfrastructureLayer.Repositories
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        protected readonly QuizVistaDbContext _dbContext;

        public ReadOnlyRepository(QuizVistaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.GetDbSet<TEntity>();
        }
    }
}
