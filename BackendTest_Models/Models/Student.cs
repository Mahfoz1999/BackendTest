using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackendTest_Models.Models;

public class Student : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public required string FullName { get; set; }
    [Required]
    [MaxLength(10)]
    public required string StudentReg { get; set; }
    [Required]
    public required string MainPortfolio { get; set; }
    public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    [Required]
    [Range(typeof(long), "0", "9223372036854775807")]
    public long RegistrationDate { get; set; }
}
