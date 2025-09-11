using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Seeding.DTO.DepartmentDTO;

public class DepartmentResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Course>  Courses { get; set; }
    public List<User> Students { get; set; }
}