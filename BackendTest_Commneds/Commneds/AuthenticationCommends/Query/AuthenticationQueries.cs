using BackendTest_Models.Models;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace BackendTest_Commneds.Commneds.AuthenticationCommends.Query;

public record GetTokenQuery(Student Student) : IRequest<JwtSecurityToken>;