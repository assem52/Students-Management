using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace StudentManagerAPI.Entities;

public class User : IdentityUser
{
    [Required]
    [Length(5, 100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    public string? Address { get; set; }
    public Department? Department { get; set; }
    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }
    
}