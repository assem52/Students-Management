using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.API.Services;
using StudentManagerAPI.Data.DTO.Shared;

namespace StudentManagerAPI.API.Controllers;


[ApiController]
[Route("api/course")]
public class CourseController(CourseService courseService) : ControllerBase
{
    private readonly CourseService _courseService = courseService;

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        return await _courseService.GetAllCoursesAsync();
    }
}