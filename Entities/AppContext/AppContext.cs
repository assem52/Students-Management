using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentManagerAPI.Entities.AppContext;

public class MyAppContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Course> Courses { get; set; }

    public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
    {
        
    }
    
}