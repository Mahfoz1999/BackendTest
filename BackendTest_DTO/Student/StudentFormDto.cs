using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BackendTest_DTO.Student;

public record StudentFormDto
{
    [Required]
    [MaxLength(50)]
    public required string FullName { get; set; }
    [Required]
    [MaxLength(10)]
    public required string StudentReg { get; set; }
    [Required]
    public required IFormFile MainPortfolio { get; set; }
    public IFormFileCollection? Portfolios { get; set; }
}
