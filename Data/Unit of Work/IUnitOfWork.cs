using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StudentManagerAPI.Data.Repository.GenericRepository;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // Generic repository access
        IGenericRepository<T> GetRepository<T>() where T : class;
        
        // Save changes
        Task<int> SaveChangesAsync();
    }
}
