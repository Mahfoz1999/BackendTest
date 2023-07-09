using BackendTest_DTO.Student;
using BackendTest_Services.AuthenticationService;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService service;
    public AuthenticationController(IAuthenticationService service)
    {
        this.service = service;
    }
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> AddStudent([FromForm] StudentFormDto formDto)
    {
        var result = await service.AddStudent(formDto);
        return Ok(result);
    }
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> LogIn(string id)
    {
        return Ok(await service.LogIn(id));

    }
}
