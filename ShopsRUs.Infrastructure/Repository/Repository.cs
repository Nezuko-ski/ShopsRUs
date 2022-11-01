using Microsoft.EntityFrameworkCore;
using ShopsRUs.Core.Interfaces;

namespace ShopsRUs.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShopsRUsDbContext _context;
        private readonly DbSet<T> _db;
        public Repository(ShopsRUsDbContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }
        public async Task DeleteAsync(string id)
        {
            _db.Remove(await _db.FindAsync(id));
        }

        public void DeleteRangeAsync(IEnumerable<T> entities)
        {

            _db.RemoveRange(entities);
        }

        public IQueryable<T> GetAllAsync()
        {
            return _db;
        }

        public async Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            return await _db.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task InsertAsync(T entity)
        {
            try
            {
                await _db.AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public Task<bool> Update(T entity)
        {
            try
            {
                 _db.Update(entity);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
