using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.API.Services;
using StudentManagerAPI.Data.DTO.AccountDTO;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IAccountService _accountService;
    private readonly IRoleService _roleService;

    public AccountController(IAuthService authService, IAccountService accountService, IRoleService roleService)
    {
        _authService = authService;
        _accountService = accountService;
        _roleService = roleService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _accountService.RegisterUserAsync(registerDto);
        
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "Registration successful" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var loginResult = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
        
        if (!loginResult.Success)
            return Unauthorized(new { message = loginResult.ErrorMessage });

        return Ok(new 
        { 
            token = loginResult.Token,
            user = new
            {
                loginResult.User!.Id,
                loginResult.User.Email,
                loginResult.User.UserName,
                loginResult.User.Name,
                DepartmentId = loginResult.User.DepartmentID
            }
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _accountService.AssignRoleAsync(model.UserId, model.RoleName);
        if (!success)
            return BadRequest(new { message = "Failed to assign role" });

        return Ok(new { message = $"Assigned {model.RoleName} role to user" });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("roles")]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(new { roles });
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        
        var profileResult = await _accountService.GetUserProfileAsync(email!);
        
        if (!profileResult.Success)
        {
            return profileResult.ErrorMessage == "User not found" 
                ? NotFound(new { message = profileResult.ErrorMessage })
                : BadRequest(new { message = profileResult.ErrorMessage });
        }

        return Ok(new
        {
            profileResult.User!.Id,
            profileResult.User.Email,
            profileResult.User.UserName,
            profileResult.User.Name,
            DepartmentId = profileResult.User.DepartmentID
        });
    }
}