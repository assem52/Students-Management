using Microsoft.AspNetCore.Mvc;

namespace StudentManagerAPI.Data.Repository.GenericRepository;

public class IGenericRepository
{
    public Task<IActionResult> getAllAsync(TEntity entity);
    public Task<IActionResult> getByIdAsync(int id);
    public Task<IActionResult> add(TEntity entity);
    public Task<IActionResult> update(TEntity entity);
    public Task<IActionResult> delete(TEntity entity);
}