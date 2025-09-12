using Microsoft.AspNetCore.Mvc;

namespace StudentManagerAPI.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentController(IUserService studentService) : ControllerBase
{
    private readonly IUserService _studentService = studentService;


}