using BackendTest_Commneds.Commneds.AuthenticationCommends.Commend;
using BackendTest_DTO.Auth;
using BackendTest_DTO.Student;
using BackendTest_Models.Models;
using MediatR;

namespace BackendTest_Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMediator _mediator;
    public AuthenticationService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<StudentDto> AddStudent(StudentFormDto formDto)
    {
        Student student = await _mediator.Send(new AddStudentCommend(formDto));
        StudentDto studentDto = new StudentDto()
        {
            Id = student.Id,
            FullName = student.FullName,
            StudentReg = student.StudentReg,
            RegistrationDate = student.RegistrationDate,
        };
        return studentDto;
    }
    public async Task<TokenModel> LogIn(string id)
    {
        TokenModel tokenModel = await _mediator.Send(new LogInCommend(id));
        return tokenModel;
    }
}
