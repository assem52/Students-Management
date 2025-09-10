using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.Data.DTO.CourseDTO;
using StudentManagerAPI.Data.DTO.Shared;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Services.Course;

public interface ICourseService
{
    public Task<ResultHandler<List<CourseResponseDto>>> GetAllCoursesAsync();
    public Task<ResultHandler<CourseResponseDto>> GetCourseByIdAsync(int courseId);
    public Task <ResultHandler<bool>> CreateCourseAsync(CourseRequest courseRequest);
    public Task <ResultHandler<bool>> UpdateCourseAsync(int courseId, CourseRequest courseRequest);
    public Task <ResultHandler<bool>> DeleteCourseAsync(int courseId);
    
}