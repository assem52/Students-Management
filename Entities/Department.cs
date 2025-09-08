using System.Reflection.Metadata.Ecma335;

namespace StudentManagerAPI.Entities;

public class Department
{
    public int  Id { get; set; }
    public string Name { get; set; }
    public List<User> Students { get; set; }
    public List<Courses> Courses { get; set; }
    
}