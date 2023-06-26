using System.ComponentModel.DataAnnotations;

namespace BackendTest_Models.Models;

public class Portfolio
{
    [Key]
    public Guid Id { get; set; }
    public required string Url { get; set; }
    public required Student Student { get; set; }
}
