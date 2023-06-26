using BackendTest_Models.Models;
using MediatR;

namespace BackendTest_Commneds.Commneds.StudentCommends.Query;

public record GetCurrentStudentQuery() : IRequest<Student>;
public record GetAllStudentsQuery() : IRequest<IEnumerable<Student>>;
public record GetAllStudentsPortfoliosUrlsQuery(string id) : IRequest<List<string>>;
