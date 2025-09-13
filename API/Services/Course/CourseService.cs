using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using StudentManagerAPI.API.Services.Course;
using StudentManagerAPI.Data.DTO.CourseDTO;
using StudentManagerAPI.Data.DTO.Shared;
using StudentManagerAPI.Data.Repository;
using StudentManagerAPI.Data.UnitOfWork;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Services;

public class CourseService(IUnitOfWork unitOfWork) : ICourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResultHandler<List<CourseResponseDto>>> GetAllCoursesAsync()
    {
        var courseRepository = _unitOfWork.GetRepository<Entities.Course>();
        var courses = await courseRepository.GetAllAsync(c => c.Department);
        
        var result = courses.Select(c => new CourseResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            DepartmentName = c.Department?.Name,
        }).ToList();

        return ResultHandler<List<CourseResponseDto>>.Ok(result);
    }


    public async Task<ResultHandler<CourseResponseDto>> GetCourseByIdAsync(int courseId)
    {
        var courseRepository = _unitOfWork.GetRepository<Entities.Course>();
        var course = await courseRepository.GetByIdAsync(courseId, c => c.Department);

        if (course == null)
        {
            return ResultHandler<CourseResponseDto>.Fail("Course Not Found!");
        }

        var result = new CourseResponseDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            DepartmentName = course.Department?.Name,
        };

        return ResultHandler<CourseResponseDto>.Ok(result);
    }

    public async Task<ResultHandler<bool>> CreateCourseAsync(CourseRequest courseRequest)
    {
        if (courseRequest == null)
        {
            return ResultHandler<bool>.Fail("Invalid Course Input!");
        }
        // Check for Department Existance first
        var deptRepository = _unitOfWork.GetRepository<Department>();
        var exist = await deptRepository.AnyAsync(dept => dept.Id == courseRequest.DeptId);
        if (!exist)
        {
            return ResultHandler<bool>.Fail("Invalid Department Input!");
        }
        
        var courseRepository =  _unitOfWork.GetRepository<Entities.Course>();

        var existed = await courseRepository.AnyAsync(c => c.Name.ToLower() == courseRequest.Name.Trim().ToLower());
        if (existed)
            return ResultHandler<bool>.Fail("Course Already Existed");

        var course = new Entities.Course
        {
            Name = courseRequest.Name,
            Description = courseRequest.Description,
            DepartmentId = courseRequest.DeptId
        };

        await courseRepository.AddAsync(course);
        await _unitOfWork.SaveChangesAsync();

        return ResultHandler<bool>.Ok(true);
    }

    public async Task<ResultHandler<bool>> UpdateCourseAsync(int courseId, CourseRequest courseRequest)
    {
        var courseRepository = _unitOfWork.GetRepository<Entities.Course>();
        var course = await courseRepository.GetByIdAsync(courseId);
        
        if (course == null) 
            return ResultHandler<bool>.Fail("Course Not Found!");

        var existed = await courseRepository.AnyAsync(c => c.Name.ToLower() == courseRequest.Name.Trim().ToLower());
        if (existed)
            return ResultHandler<bool>.Fail("Course Already Existed");
        
         // Check for Department Existance first
        var deptRepository = _unitOfWork.GetRepository<Department>();
        var exist = await deptRepository.AnyAsync(dept => dept.Id == courseRequest.DeptId);
        if (!exist)
        {
            return ResultHandler<bool>.Fail("Invalid Department Input!");
        }
        
        
        course.Name = courseRequest.Name;
        course.Description = courseRequest.Description;
        course.DepartmentId = courseRequest.DeptId;

        courseRepository.Update(course);
        await _unitOfWork.SaveChangesAsync();
        
        return ResultHandler<bool>.Ok(true);
    }

    public async Task<ResultHandler<bool>> DeleteCourseAsync(int courseId)
    {
        var courseRepository = _unitOfWork.GetRepository<Entities.Course>();
        var course = await courseRepository.GetByIdAsync(courseId);
        
        if (course == null)
            return ResultHandler<bool>.Fail("Course Not Found");
        
        courseRepository.Delete(course);
        await _unitOfWork.SaveChangesAsync();
        
        return ResultHandler<bool>.Ok(true);
    }
}