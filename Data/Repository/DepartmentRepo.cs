using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagerAPI.Data.UnitOfWork;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.Data.Repository;

public class DepartmentRepo : GenericRepository<Department>
{
    private readonly AppDbContext _dbContext;
    public DepartmentRepo(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;

    }
    //Departmetn specific Opertaions 
    public async Task<Department?> GetDepartmentWithCoursesAsync(int id)
    {
        return await _dbContext.Departments
            .Include(d => d.Courses)
            .FirstOrDefaultAsync();
    }
    
}