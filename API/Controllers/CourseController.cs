using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.API.Services;
using StudentManagerAPI.API.Services.Course;
using StudentManagerAPI.Data.DTO.CourseDTO;
using StudentManagerAPI.Data.DTO.Shared;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService courseService) : ControllerBase
{
    private readonly ICourseService _courseService = courseService;

    [HttpGet("Courses")]
    public async Task<IActionResult> GetCourses()
    {
        var result = await _courseService.GetAllCoursesAsync();
        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Data);
    }
    [HttpGet("id")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var result = await _courseService.GetCourseByIdAsync(id);
        if(!result.Success)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }

    [HttpPost("AddCourse")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCourse([FromBody] CourseRequest courseRequest)
    {
        if (!ModelState.IsValid)
        {
            return  BadRequest(ModelState);
        }
        try
        {
            var result = await _courseService.CreateCourseAsync(courseRequest);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // log the error
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseRequest courseRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _courseService.UpdateCourseAsync(id, courseRequest);
        if(!result.Success)
            return BadRequest(result.ErrorMessage);
        return NoContent();
    }

    [HttpDelete("DeleteCourse/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var result = await _courseService.DeleteCourseAsync(id);
        if(!result.Success)
            return  BadRequest(result.ErrorMessage);
        return NoContent();
    }
    
}
