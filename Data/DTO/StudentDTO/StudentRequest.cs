using System.ComponentModel.DataAnnotations;

namespace StudentManagerAPI.API.Seeding.DTO;

public class StudentRequest
{
    [Required]
    [Length(5, 50)]
    public string Name { get; set; }
    [Required]
    [Length(5, 50)]
    public string Email { get; set; }
    [Required]
    [Length(5, 50)]
    public string Addres { get; set; }
    public int  DepartmentId { get; set; }
}