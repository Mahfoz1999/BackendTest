using BackendTest_DTO.Auth;
using BackendTest_DTO.Student;
using Microsoft.AspNetCore.Identity;

namespace BackendTest_Services.AuthenticationService;

public interface IAuthenticationService
{
    public Task<IdentityResult> AddStudent(StudentFormDto formDto);
    public Task<TokenModel> LogIn(string id);
}
