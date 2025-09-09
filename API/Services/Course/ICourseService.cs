using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Services.Course;

public interface ICourseService
{
    public Task<IActionResult> GetAllCoursesAsync(Entities.Course course);
    public Task<IActionResult> GetCourseByIdAsync(int courseId);
    public Task<IActionResult> CreateCourseAsync(Entities.Course course);
    public Task<IActionResult> UpdateCourseAsync(Entities.Course course);
    public Task<IActionResult> DeleteCourseAsync(Entities.Course course);
    
}