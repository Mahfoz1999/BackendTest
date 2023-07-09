namespace BackendTest_DTO.Student;

public record StudentDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string StudentReg { get; set; }
    public long RegistrationDate { get; set; }
}
