using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Common;
using TopSpeed.Infrastructure.Data;

namespace TopSpeed.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _dbContext;


        public GenericRepository(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        public async Task Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
             _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<bool> IsRecordExists(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).AnyAsync();
        }

        public async  Task<IEnumerable<T>> Query(Expression<Func<T, bool>> predicate)
        {
            var entities = await  _dbContext.Set<T>().Where(predicate).ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> Query()
        {
            var entities = await _dbContext.Set<T>().ToListAsync();
            return entities;
        }

    
    }
}
