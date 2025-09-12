using Microsoft.AspNetCore.Mvc;
using StudentManagerAPI.API.Services;

namespace StudentManagerAPI.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    private readonly IStudentService _studentService = studentService;
    

}