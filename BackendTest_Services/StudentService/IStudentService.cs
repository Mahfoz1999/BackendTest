using BackendTest_DTO.Student;
using BackEndTest_SharedKernal.enums;

namespace BackendTest_Services.StudentService;

public interface IStudentService
{
    public Task<IEnumerable<StudentDto>> GetAllStudents();
    public Task<List<string>> GetCurrentStudentImages(ImageType imageType);
}
