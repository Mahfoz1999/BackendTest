using BackendTest_DTO.Student;

namespace BackendTest_Services.StudentService;

public interface IStudentService
{
    public Task<IEnumerable<StudentDto>> GetAllStudents();
    public Task<StudentDetailsDto> GetCurrentStudent();
}
