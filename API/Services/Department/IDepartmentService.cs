using StudentManagerAPI.API.Seeding.DTO.DepartmentDTO;
using StudentManagerAPI.Data.DTO.CourseDTO;
using StudentManagerAPI.Data.DTO.Shared;

namespace StudentManagerAPI.API.Services;

public interface IDepartmentService
{
    public Task<ResultHandler<List<DepartmentResponse>>> GetAllDepartmentsAsync();
    public Task<ResultHandler<DepartmentResopnse>>  GetDepartmentByIdAsync(int departmentId);
    public Task<ResultHandler<bool>> CreateDepartmentAsync(DepartmentRequest dept);
    public Task<ResultHandler<bool>> UpdateDepartmentAsync(int id, DepartmentRequest dept);
    public Task<ResultHandler<bool>> DeleteDepartmentAsync(int id);
    
}