using BackendTest_DTO.Auth;
using BackendTest_DTO.Student;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BackendTest_Commneds.Commneds.AuthenticationCommends.Commend;

public record AddStudentCommend(StudentFormDto formDto) : IRequest<IdentityResult>;
public record LogInCommend(string id) : IRequest<TokenModel>;