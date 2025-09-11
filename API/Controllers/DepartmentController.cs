using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.API.Seeding.DTO.DepartmentDTO;
using StudentManagerAPI.API.Services;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DepartmentController(IDepartmentService departmentService) : ControllerBase
{
    private readonly IDepartmentService _departmentService =  departmentService;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetDepartments()
    {
        
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        
    }   
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateDepartment(DepartmentRequest department)
    {
        
    }

    [HttpPost("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateDepartment(int id, DepartmentRequest department)
    {
        
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        
    }
}