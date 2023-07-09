using BackendTest_Models.Models;
using BackEndTest_SharedKernal.enums;
using MediatR;

namespace BackendTest_Commneds.Commneds.StudentCommends.Query;

public record GetCurrentStudentImagesQuery(ImageType ImageType) : IRequest<List<string>>;
public record GetAllStudentsQuery() : IRequest<IEnumerable<Student>>;
public record GetAllStudentsPortfoliosUrlsQuery(string id) : IRequest<List<string>>;
