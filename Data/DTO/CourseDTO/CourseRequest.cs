using System.ComponentModel.DataAnnotations;

namespace StudentManagerAPI.Data.DTO.CourseDTO;

public class CourseRequest
{
    [Required]
    [Length(4, 40)]
    public string Name { get; set; }
    [Required]
    [Length(20, 200)]
    public string  Description { get; set; }
}