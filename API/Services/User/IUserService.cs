using StudentManagerAPI.Data.DTO.Shared;

namespace StudentManagerAPI.API.Services;

public interface IUserService
{
    public Task<ResultHandler<StudentResponse>> GetAllAsync();
    public Task<ResultHandler<StudentResponse>> GetByIdAsnyc(int id);
    public Task<ResultHandler<bool>> CreateAsync(StudentRequest dto);
    public Task<ResultHandler<bool>> UpdateAsync(int id, StduentRequest dto);
    public Task<ResultHandler<bool>> DeleteAsync(int id);
}