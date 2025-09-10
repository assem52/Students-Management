using StudentManagerAPI.Entities;

namespace StudentManagerAPI.Data.Repository;

public class CourseRepository : GenericRepository<Course>
{
    private readonly AppDbContext _appDbContext;

    public CourseRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }  

}