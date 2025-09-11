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
        var result =  await _departmentService.GetAllDepartmentsAsync();
        if(!result.Success)
            return NotFound();
        
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        var result = await _departmentService.GetDepartmentByIdAsync(id);
        if(!result.Success)
            return NotFound();
        
        return Ok(result.Data);
    }   
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateDepartment(DepartmentRequest department)
    {
        var result = await _departmentService.CreateDepartmentAsync(department);
        if(!result.Success)
            return BadRequest();
        
        return Ok(result.Data);
    }

    [HttpPost("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateDepartment(int id, DepartmentRequest department)
    {
        var result = await _departmentService.UpdateDepartmentAsync(id, department);
        
        if(!result.Success)
            return BadRequest();
        
        return Ok();
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var result = await _departmentService.DeleteDepartmentAsync(id);
        
        if(!result.Success)
            return BadRequest();
        
        return Ok();
    }
}