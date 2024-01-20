using System.Linq.Expressions;

namespace HotelArc.Process.Abstract
{
    public interface IService<T> where T : class
    {
        Task<T> FindAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression); 
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression); 
        Task AddAsync(T entity);
      
        Task<bool> UpdateAsync(T entity, Guid id);
        Task<bool> SoftDeleteAsync(Guid id);
    }
}
