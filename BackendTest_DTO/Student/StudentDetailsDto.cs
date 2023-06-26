namespace BackendTest_DTO.Student;

public record StudentDetailsDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string StudentReg { get; set; }
    public string MainPortfolioUrl { get; set; }
    public List<string> PortfoliosUrls { get; set; } = new List<string>();
    public long RegistrationDate { get; set; }
}
