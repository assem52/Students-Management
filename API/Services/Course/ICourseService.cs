using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.Data.DTO.CourseDTO;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Services.Course;

public interface ICourseService
{
    public Task<List<CourseResponseDto>> GetAllCoursesAsync();
    public Task<CourseResponseDto> GetCourseByIdAsync(int courseId);
    public Task<bool> CreateCourseAsync(CourseRequest courseRequest);
    public Task<bool> UpdateCourseAsync(int courseId, CourseRequest courseRequest);
    public Task<bool> DeleteCourseAsync(int courseId);
    
}