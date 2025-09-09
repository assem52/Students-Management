using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.Data.DTO.CourseDTO;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Services.Course;

public interface ICourseService
{
    public Task<IActionResult> GetAllCoursesAsync();
    public Task<IActionResult> GetCourseByIdAsync(int courseId);
    public Task<IActionResult> CreateCourseAsync(int courseId, CourseRequest courseRequest);
    public Task<IActionResult> UpdateCourseAsync(CourseRequest courseRequest);
    public Task<IActionResult> DeleteCourseAsync(int courseId);
    
}