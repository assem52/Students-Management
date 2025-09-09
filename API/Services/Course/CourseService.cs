using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using StudentManagerAPI.API.Services.Course;
using StudentManagerAPI.Data.DTO.CourseDTO;
using StudentManagerAPI.Data.DTO.Shared;
using StudentManagerAPI.Data.Repository;

namespace StudentManagerAPI.API.Services;

public class CourseService(
        CourseRepository courseRepository
        ,ResultHandler<> resultHander) : ICourseService
{
    private readonly CourseRepository _courseRepository = courseRepository;
    private readonly Resopns


    public Task<ResultHandler<List<CourseResponseDto>>> GetAllCoursesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResultHandler<CourseResponseDto>> GetCourseByIdAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<ResultHandler<bool>> CreateCourseAsync(CourseRequest courseRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ResultHandler<bool>> UpdateCourseAsync(int courseId, CourseRequest courseRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ResultHandler<bool>> DeleteCourseAsync(int courseId)
    {
        throw new NotImplementedException();
    }
}