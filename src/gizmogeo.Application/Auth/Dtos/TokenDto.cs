namespace gizmogeo.Application.Auth.Dtos;

public class TokenDto
{
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public string? RecoveryCode { get; set; }
}
