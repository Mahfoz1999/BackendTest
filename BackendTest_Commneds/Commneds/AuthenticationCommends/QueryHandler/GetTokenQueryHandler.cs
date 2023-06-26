using BackendTest_Commneds.Commneds.AuthenticationCommends.Query;
using BackendTest_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendTest_Commneds.Commneds.AuthenticationCommends.QueryHandler;

public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, JwtSecurityToken>
{
    private readonly IConfiguration configuration;
    private readonly UserManager<Student> _userManager;
    public GetTokenQueryHandler(IConfiguration configuration, UserManager<Student> userManager)
    {
        this.configuration = configuration;
        _userManager = userManager;
    }

    public async Task<JwtSecurityToken> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userRoles = await _userManager.GetRolesAsync(request.Student);

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Student.Id),
                new Claim(ClaimTypes.NameIdentifier, request.Student.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, request.Student.UserName !),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, request.Student.Id));
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:secret"]!));

            var token = new JwtSecurityToken(
                issuer: configuration["JwtConfig:validIssuer"],
                audience: configuration["JwtConfig:validAudience"],
                expires: DateTime.Now.AddMinutes(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
