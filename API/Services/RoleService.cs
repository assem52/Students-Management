using Microsoft.AspNetCore.Identity;

namespace StudentManagerAPI.API.Services;

public interface IRoleService
{
    Task EnsureRolesExistAsync(IEnumerable<string> roles);
    Task<bool> RoleExistsAsync(string roleName);
    Task<IdentityResult> CreateRoleAsync(string roleName);
    Task<IEnumerable<string>> GetAllRolesAsync();
}

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task EnsureRolesExistAsync(IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    public async Task<IdentityResult> CreateRoleAsync(string roleName)
    {
        return await _roleManager.CreateAsync(new IdentityRole(roleName));
    }

    public async Task<IEnumerable<string>> GetAllRolesAsync()
    {
        var roles = await Task.FromResult(_roleManager.Roles.Select(r => r.Name).ToList());
        return roles.Where(r => r != null).Cast<string>();
    }
}