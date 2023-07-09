using BackendTest_DTO.Auth;
using BackendTest_DTO.Student;

namespace BackendTest_Services.AuthenticationService;

public interface IAuthenticationService
{
    public Task<StudentDto> AddStudent(StudentFormDto formDto);
    public Task<TokenModel> LogIn(string id);
}
