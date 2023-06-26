namespace BackendTest_DTO.Auth;

public record TokenModel
{
    public string token { get; set; } = string.Empty;
    public DateTime expiration { get; set; }
}
