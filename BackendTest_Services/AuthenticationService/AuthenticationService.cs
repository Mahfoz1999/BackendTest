using BackendTest_Commneds.Commneds.AuthenticationCommends.Commend;
using BackendTest_DTO.Auth;
using BackendTest_DTO.Student;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BackendTest_Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMediator _mediator;
    public AuthenticationService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IdentityResult> AddStudent(StudentFormDto formDto)
    {
        IdentityResult identityResult = await _mediator.Send(new AddStudentCommend(formDto));
        return identityResult;
    }
    public async Task<TokenModel> LogIn(string id)
    {
        TokenModel tokenModel = await _mediator.Send(new LogInCommend(id));
        return tokenModel;
    }
}
