using BackendTest_DTO.Auth;
using BackendTest_DTO.Student;
using BackendTest_Models.Models;
using MediatR;

namespace BackendTest_Commneds.Commneds.AuthenticationCommends.Commend;

public record AddStudentCommend(StudentFormDto formDto) : IRequest<Student>;
public record LogInCommend(string id) : IRequest<TokenModel>;