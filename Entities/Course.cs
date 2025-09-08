using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerAPI.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string  Description { get; set; }
    public Department Department { get; set; }
    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }
}