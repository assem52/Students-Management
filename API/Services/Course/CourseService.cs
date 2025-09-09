using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using StudentManagerAPI.API.Services.Course;
using StudentManagerAPI.Data.DTO.CourseDTO;
using StudentManagerAPI.Data.Repository;

namespace StudentManagerAPI.API.Services;

public class CourseService(
        CourseRepository courseRepository) : ICourseService
{
    private readonly CourseRepository _courseRepository = courseRepository;

    public async Task CreateCourseAsync(CourseRequest courseRequest)
    {
        if (courseRequest == null)
            throw new ArgumentNullException(nameof(courseRequest));

        var course = new Entities.Course
        {
            Name = courseRequest.Name,
            Description = courseRequest.Description,
            DepartmentId = courseRequest.DeptId
        };
        await _courseRepository.AddAsync(course);
    }

    public async Task DeleteCourseAsync(int courseId)
    {
        var course = _courseRepository.GetByIdAsync(courseId);
        if (course == null)
        {
            
        }

        await _courseRepository.Delete(course);
        
    }

    public async Task<List<CourseResponseDto>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        var response = new List<CourseResponseDto>();
        foreach (var course in courses)
        {
            CourseResponseDto courseResp = new CourseResponseDto();
            courseResp.Id = course.Id;
            courseResp.Name = course.Name;
            
            response.Add(courseResp);
        }

        return response;
    }

    public async Task<CourseResponseDto> GetCourseByIdAsync(int courseId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null)
        {
            return null;
        }

        var courseResponse = new CourseResponseDto()
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description
        };
        return courseResponse;

    }

    public Task UpdateCourseAsync(int courseId, CourseRequest courseRequest)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null)
        {
            return  Task.CompletedTask;
        }

        _courseRepository.Update(course);
    }
}