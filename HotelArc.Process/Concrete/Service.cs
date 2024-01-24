using HotelArc.Business;
using HotelArc.Process.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelArc.Process.Concrete
{
    public class Service<T> : IService<T> where T : class, new()
    {
        internal DatabaseContext _context;

        internal DbSet<T> _dbSet;

        public Service(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FindAsync(Guid id)
        {

            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            // IsDeleted == false olanları getir

            return await _dbSet.AsNoTracking()
                .Where(x => x.GetType()
                .GetProperty("IsDeleted") == null || (bool)x.GetType().GetProperty("IsDeleted").GetValue(x) == false)
                .ToListAsync();
        }

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            // IsDeleted == false olanları ve expressiona uyanları getir
            return _dbSet.AsNoTracking()
                .Where(x => x.GetType().GetProperty("IsDeleted") == null || (bool)x.GetType().GetProperty("IsDeleted").GetValue(x) == false)
                .Where(expression).ToListAsync();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking()
                .Where(x => x.GetType().GetProperty("IsDeleted") == null || (bool)x.GetType().GetProperty("IsDeleted").GetValue(x) == false)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {

            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(T entity, Guid id)
        {
            var entityToUpdate = await _dbSet.FindAsync(id);
            if (entityToUpdate == null)
            {
                return false;
            }
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
