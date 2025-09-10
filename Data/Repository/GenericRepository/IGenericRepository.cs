using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagerAPI.Data.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        // Basic CRUD operations
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        
        // Simple query operations
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
