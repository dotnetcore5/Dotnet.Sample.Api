using Microsoft.EntityFrameworkCore;
using Rest.Api.Datastore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest.Api.Domain
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();

        Task<T> FindAsync(int id);

        Task UpdateAsync(T entity);

        Task AddAsync(T entity);

        Task DeleteAsync(T entity);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDatabase _context;

        public Repository(IDatabase context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity); await _context.SaveAsync();
        }

        public virtual async Task<T> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAsync()
        {
            var result = await _context.Set<T>().AsQueryable().ToListAsync();
            return result;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            await _context.SaveAsync();
        }
    }
}