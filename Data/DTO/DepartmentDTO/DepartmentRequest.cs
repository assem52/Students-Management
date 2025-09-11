using System.ComponentModel.DataAnnotations;

namespace StudentManagerAPI.API.Seeding.DTO.DepartmentDTO;

public class DepartmentRequest
{
    [Required]
    [Length(5, 50)]
    public string Name { get; set; }
    [Required]
    [Length(5, 200)]
    public string Description { get; set; }

}