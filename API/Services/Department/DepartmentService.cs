using StudentManagerAPI.API.Seeding.DTO.DepartmentDTO;
using StudentManagerAPI.Data;
using StudentManagerAPI.Data.DTO.Shared;
using StudentManagerAPI.Data.UnitOfWork;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Services;

public class DepartmentService (IUnitOfWork unitOfWork) : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResultHandler<List<DepartmentResponse>>> GetAllDepartmentsAsync()
    {
        var deptRepo = _unitOfWork.GetRepository<Department>();
        var depts = await deptRepo.GetAllAsync();

        var result = depts.Select(d => new DepartmentResponse()
        {
            Name = d.Name,
            Description = d.Description,
            Courses = d.Courses, 
            Students = d.Students,
        }).ToList();
        if(result.Any())    
            return ResultHandler<List<DepartmentResponse>>.Fail("No departments found");
        
        return ResultHandler<List<DepartmentResponse>>.Ok(result);
    }

    public async Task<ResultHandler<DepartmentResponse>> GetDepartmentByIdAsync(int departmentId)
    {
        var  departmentRepo = _unitOfWork.GetRepository<Department>();
        var department = await departmentRepo.GetByIdAsync(departmentId);
        if(department == null)
            return ResultHandler<DepartmentResponse>.Fail("No department found");

        var result = new DepartmentResponse
        {
            Name = department.Name,
            Description = department.Description,
            Courses = department.Courses,
            Students = department.Students,
        };
        
        return ResultHandler<DepartmentResponse>.Ok(result);
    }

    public async Task<ResultHandler<bool>> CreateDepartmentAsync(DepartmentRequest dept)
    {
        var deptRepo = _unitOfWork.GetRepository<Department>();
        
        // var context = _unitOfWork.GetRepository<AppDbContext>();
        // var exist = context.Departments.Contain(d => d.Name = dept.Name);
        var department = new Department()
        {
            Name = dept.Name,
            Description = dept.Description
        };
        await deptRepo.AddAsync(department);
        await _unitOfWork.SaveChangesAsync();
        
        return ResultHandler<bool>.Ok(true);
    }

    public async Task<ResultHandler<bool>> UpdateDepartmentAsync(int id, DepartmentRequest dept)
    {
        var deptRepo = _unitOfWork.GetRepository<Department>();
        var department = await deptRepo.GetByIdAsync(id);
        
        if(department == null)
            return ResultHandler<bool>.Fail("No department found");
        
        department.Name = dept.Name;
        department.Description = dept.Description;
        
        deptRepo.Update(department);
        await _unitOfWork.SaveChangesAsync();
        
        return ResultHandler<bool>.Ok(true);
    }

    public async Task<ResultHandler<bool>> DeleteDepartmentAsync(int id)
    {
        var  departmentRepo = _unitOfWork.GetRepository<Department>();
        var department = await departmentRepo.GetByIdAsync(id);
        
        if(department == null)
            return ResultHandler<bool>.Fail("No Department found");
        
        departmentRepo.Delete(department);
        await _unitOfWork.SaveChangesAsync();
        
        return ResultHandler<bool>.Ok(true);
    }
}