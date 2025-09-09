using Microsoft.EntityFrameworkCore;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.Data.Repository;

public class StudentRepository : GenericRepository<User>
{
    
private readonly AppDbContext _dbContext;

public StudentRepository(AppDbContext dbContext) : base(dbContext)
{
    _dbContext = dbContext;
}

public async Task<User?> FindByEmail(string email)
{
    return await _dbContext.
        Students
        .Include(s => s.Department)
        .FirstOrDefaultAsync(s => s.Email == email);
}
}