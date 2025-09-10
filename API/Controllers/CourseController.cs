using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.API.Services;
using StudentManagerAPI.Data.DTO.CourseDTO;
using StudentManagerAPI.Data.DTO.Shared;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Controllers;


[ApiController]
[Route("api/course")]
public class CourseController(CourseService courseService) : ControllerBase
{
    private readonly CourseService _courseService = courseService;

    [HttpGet]
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

    [HttpPost]
    [Authorize("Admin")]
    public async Task<IActionResult> CreateCourse([FromBody] CourseRequest courseRequest)
    {
        if (!ModelState.IsValid)
        {
            return  BadRequest(ModelState);
        }
        var result = await _courseService.CreateCourseAsync(courseRequest);
        if (!result.Success)
        {
            return  BadRequest(result.ErrorMessage);
        }

        return CreatedAtAction(nameof(GetCourse), new { id = result.Data }, result.Data);
    }

    [HttpPut("id")]
    [Authorize("Admin")]
    public async Task<IActionResult> UpdateCourse([FromRoute] int id, [FromBody] CourseRequest courseRequest)
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

    [HttpDelete]
    [Authorize("Admin")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var result = await _courseService.DeleteCourseAsync(id);
        if(!result.Success)
            return  BadRequest(result.ErrorMessage);
        return NoContent();
    }
    
}
