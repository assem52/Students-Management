using System.Threading.Tasks;
using StudentManagerAPI.Entities;
using StudentManagerAPI.Data.Repository.GenericRepository;

namespace StudentManagerAPI.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        // Generic repository access
        IGenericRepository<T> GetRepository<T>() where T : class;
        
        // Save changes
        Task<int> SaveChangesAsync();
    }
}
