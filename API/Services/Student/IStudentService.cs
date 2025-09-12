using StudentManagerAPI.API.Seeding.DTO;
using StudentManagerAPI.Data.DTO.Shared;

namespace StudentManagerAPI.API.Services;

public interface IStudentService
{
    public Task<ResultHandler<List<StudentResponse>>> GetAllAsync();
    public Task<ResultHandler<StudentResponse>> GetByIdAsnyc(int id);
    public Task<ResultHandler<bool>> CreateAsync(StudentRequest dto);
    public Task<ResultHandler<bool>> UpdateAsync(int id, StudentRequest dto);
    public Task<ResultHandler<bool>> DeleteAsync(int id);
}