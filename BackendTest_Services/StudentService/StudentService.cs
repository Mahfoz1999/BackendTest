using BackendTest_Commneds.Commneds.StudentCommends.Query;
using BackendTest_DTO.Student;
using MediatR;

namespace BackendTest_Services.StudentService;

public class StudentService : IStudentService
{
    private readonly IMediator _mediator;
    public StudentService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IEnumerable<StudentDto>> GetAllStudents()
    {
        var students = await _mediator.Send(new GetAllStudentsQuery());
        List<StudentDto> studentDtos = students.Select(e => new StudentDto()
        {
            Id = e.Id,
            FullName = e.FullName
        }).ToList();
        return studentDtos;
    }

    public async Task<StudentDetailsDto> GetCurrentStudent()
    {
        var student = await _mediator.Send(new GetCurrentStudentQuery());
        List<string> urls = await _mediator.Send(new GetAllStudentsPortfoliosUrlsQuery(student.Id));
        StudentDetailsDto studentDetails = new()
        {
            Id = student.Id,
            FullName = student.FullName,
            RegistrationDate = student.RegistrationDate,
            StudentReg = student.StudentReg,
            MainPortfolioUrl = student.MainPortfolio,
            PortfoliosUrls = urls
        };
        return studentDetails;
    }
}
