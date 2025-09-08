using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagerAPI.Entities;

public class Courses
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string  Description { get; set; }
    public Department Demartment { get; set; }
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
}