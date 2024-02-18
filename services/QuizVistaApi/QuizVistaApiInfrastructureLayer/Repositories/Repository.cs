using QuizVistaApiInfrastructureLayer.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiInfrastructureLayer.Repositories
{
    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        public Repository(QuizVistaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var itemToDelete = _dbContext.Find<TEntity>(id);
            if (itemToDelete is null)
            {
                throw new ArgumentNullException(nameof(itemToDelete));
            }

            _dbContext.Remove(itemToDelete);

            await SaveChangesAsync();

            return itemToDelete;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            _dbContext.Add(entity);

            await SaveChangesAsync();

            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if(entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbContext.Update(entity);

            await SaveChangesAsync();
            
            return entity;
        }
    }
}
