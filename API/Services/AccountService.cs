using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagerAPI.Entities;
using StudentManagerAPI.Data.DTO.AccountDTO;

namespace StudentManagerAPI.API.Services;

public interface IAccountService
{
    Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
    Task<User> GetUserByEmailAsync(string email);
    Task<bool> AssignRoleAsync(string userId, string role);
    Task<UserProfileResult> GetUserProfileAsync(string email);
}

public class UserProfileResult
{
    public bool Success { get; set; }
    public User? User { get; set; }
    public string? ErrorMessage { get; set; }
}
public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Name = registerDto.Name,
            DepartmentID = registerDto.DepartmentId
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        
        if (result.Succeeded)
        {
            // Assign default role
            await _userManager.AddToRoleAsync(user, "Student");
        }

        return result;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> AssignRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || !await _roleManager.RoleExistsAsync(role))
            return false;

        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<UserProfileResult> GetUserProfileAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return new UserProfileResult
            {
                Success = false,
                ErrorMessage = "Email is required"
            };
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new UserProfileResult
            {
                Success = false,
                ErrorMessage = "User not found"
            };
        }

        return new UserProfileResult
        {
            Success = true,
            User = user
        };
    }
}