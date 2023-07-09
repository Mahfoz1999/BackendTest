using BackendTest_Services.StudentService;
using BackEndTest_SharedKernal.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService service;
    public StudentController(IStudentService service)
    {
        this.service = service;
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllStudents()
    {
        var result = await service.GetAllStudents();
        return Ok(result);
    }
    [HttpGet]
    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> GetCurrentStudentImages(ImageType imageType)
    {
        var result = await service.GetCurrentStudentImages(imageType);
        return Ok(result);
    }
}
