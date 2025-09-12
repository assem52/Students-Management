using StudentManagerAPI.API.Seeding.DTO;
using StudentManagerAPI.Data.DTO.Shared;
using StudentManagerAPI.Data.UnitOfWork;
using StudentManagerAPI.Entities;

namespace StudentManagerAPI.API.Services;

public class StudentService(IUnitOfWork unitOfWork) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResultHandler<List<StudentResponse>>> GetAllAsync()
    {
        var studentRepo = _unitOfWork.GetRepository<User>();
        var students = await studentRepo.GetAllAsync(std => std.Department);
        if(!students.Any())
            return ResultHandler<List<StudentResponse>>.Fail("No students found");

        var result = students.Select(std => new StudentResponse
        {
            Name = std.Name,
            Email = std.Email,
            Address = std.Address,
            DepartmentName = std.Department.Name
        }).ToList();
        
        return ResultHandler<List<StudentResponse>>.Ok(result);
    }

    public async Task<ResultHandler<StudentResponse>> GetByIdAsnyc(int id)
    {
        var  studentRepo = _unitOfWork.GetRepository<User>();
        var studnet = await studentRepo.GetByIdAsync(id, std => std.Department);
        
        if(studnet == null)
            return ResultHandler<StudentResponse>.Fail("No student found");
        
        var result = new StudentResponse
        {
            Name = studnet.Name,
            Email = studnet.Email,
            Address = studnet.Address,
            DepartmentName = studnet.Department.Name
        };
        return ResultHandler<StudentResponse>.Ok(result);
    }

    public async Task<ResultHandler<bool>> CreateAsync(StudentRequest dto)
    {
        if(dto == null)
            return  ResultHandler<bool>.Fail("Invalid request");
        var studentRepo = _unitOfWork.GetRepository<User>();
        
        var std = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Address = dto.Addres,
            DepartmentId = dto.DepartmentId
        };
        studentRepo.AddAsync(std);
        _unitOfWork.SaveChangesAsync();
        
        return ResultHandler<bool>.Ok(true);
    }

    public async Task<ResultHandler<bool>> UpdateAsync(int id, StudentRequest dto)
    {
        var studentRepo = _unitOfWork.GetRepository<User>();
        var std = await studentRepo.GetByIdAsync(id, std => std.Department);
        if(std == null)
            return ResultHandler<bool>.Fail("No student found");
        if(dto == null)
            return ResultHandler<bool>.Fail("Invalid request");
        
        std.Name = dto.Name;
        std.Email = dto.Email;
        std.Address =  dto.Addres;
        std.DepartmentId = dto.DepartmentId;
        
        studentRepo.Update(std);
        await _unitOfWork.SaveChangesAsync();
        return ResultHandler<bool>.Ok(true);
    }

    public async Task<ResultHandler<bool>> DeleteAsync(int id)
    {
        var studentRepo = _unitOfWork.GetRepository<User>();
        var std = await studentRepo.GetByIdAsync(id);
        
        if(std == null)
            return ResultHandler<bool>.Fail("No student found");
        studentRepo.Delete(std);
        
        _unitOfWork.SaveChangesAsync();
        return ResultHandler<bool>.Ok(true);
    }
}