using BackendTest_Commneds.Commneds.StudentCommends.Query;
using BackendTest_DTO.Student;
using BackEndTest_SharedKernal.enums;
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
            FullName = e.FullName,
            StudentReg = e.StudentReg,
            RegistrationDate = e.RegistrationDate
        }).ToList();
        return studentDtos;
    }

    public async Task<List<string>> GetCurrentStudentImages(ImageType imageType)
    {
        var urls = await _mediator.Send(new GetCurrentStudentImagesQuery(imageType));
        return urls;
    }
}
