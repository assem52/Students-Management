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
    CourseRepository courseRepository) : ICourseService
{
    private readonly CourseRepository _courseRepository = courseRepository;


    public async Task<ResultHandler<List<CourseResponseDto>>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        var result = courses.Select(c => new CourseResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
        }).ToList();
        return ResultHandler<List<CourseResponseDto>>.Ok(result);
    }

    public async Task<ResultHandler<CourseResponseDto>> GetCourseByIdAsync(int courseId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null)
        {
            return ResultHandler<CourseResponseDto>.Fail("Course Not Found!");
        }

        var result = new CourseResponseDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            DepartmentName = course.Department.Name, 
            DepratmentId = course.Department.Id,
        };
        
    return ResultHandler<CourseResponseDto>.Ok(result);
    }

    public async Task<ResultHandler<bool>> CreateCourseAsync(CourseRequest courseRequest)
    {
        if (courseRequest == null)
        {
            return ResultHandler<bool>.Fail("Invalid Course Input!");
        }

        var course = new Entities.Course
        {
            Name = courseRequest.Name,
            Description = courseRequest.Description,
            DepartmentId = courseRequest.DeptId
        };
        return ResultHandler<bool>Ok.(true);
    }

    public async Task<ResultHandler<bool>> UpdateCourseAsync(int courseId, CourseRequest courseRequest)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if(course == null) 
            return ResultHandler<bool>.Fail("Course Not Found!");
        if (courseRequest == null)
            return ResultHandler<bool>.Fail("Invalid Course Input!");

        _courseRepository.Update(course);
        return ResultHandler<bool>.Ok(true);
    }

    public async Task<ResultHandler<bool>> DeleteCourseAsync(int courseId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null)
            return ResultHandler<bool>.Fail("Course Npt Found");
        
        _courseRepository.Delete(course);
        return ResultHandler<bool>.Ok(true);
    }
}

    