using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.API.Seeding.DTO;
using StudentManagerAPI.API.Services;

namespace StudentManagerAPI.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    private readonly IStudentService _studentService = studentService;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetStudents()
    {
        var result = await _studentService.GetAllAsync();
        if(!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var result = await _studentService.GetByIdAsnyc(id);
        if(!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateStudent(StudentRequest std)
    {
        if(!ModelState.IsValid)
            return  BadRequest(ModelState);
        
        var result = await _studentService.CreateAsync(std);
        if(!result.Success)
            return BadRequest(result);
        
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStudent([FromRoute]int id, [FromBody] StudentRequest std)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _studentService.UpdateAsync(id, std);
        if(!result.Success)
            return BadRequest(result);
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var result = await _studentService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }
}