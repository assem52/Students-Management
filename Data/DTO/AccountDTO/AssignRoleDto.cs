using System.ComponentModel.DataAnnotations;

namespace StudentManagerAPI.Data.DTO.AccountDTO;

public class AssignRoleDto
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string RoleName { get; set; } = string.Empty;
}
