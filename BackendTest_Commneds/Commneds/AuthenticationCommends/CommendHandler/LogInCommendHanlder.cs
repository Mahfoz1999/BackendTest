using BackendTest_Commneds.Commneds.AuthenticationCommends.Commend;
using BackendTest_Commneds.Commneds.AuthenticationCommends.Query;
using BackendTest_Commneds.Exceptions;
using BackendTest_DTO.Auth;
using BackendTest_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace BackendTest_Commneds.Commneds.AuthenticationCommends.CommendHandler;

public class LogInCommendHanlder : IRequestHandler<LogInCommend, TokenModel>
{
    private readonly UserManager<Student> _userManager;
    private readonly IMediator _mediator;
    public LogInCommendHanlder(UserManager<Student> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }
    public async Task<TokenModel> Handle(LogInCommend request, CancellationToken cancellationToken)
    {
        try
        {
            Student? student = await _userManager.FindByIdAsync(request.id);
            if (student is null) throw new NotFoundException("Student Not Found");
            var token = await _mediator.Send(new GetTokenQuery(student));
            return new TokenModel()
            {
                token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }
        catch (Exception)
        {
            throw;
        }
    }
}
