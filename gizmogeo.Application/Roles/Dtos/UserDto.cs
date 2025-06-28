namespace gizmogeo.Application.Roles.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public bool IsPhoneNumberConfirmed { get; set; } = false;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireDate { get; set; }
    public int RoleId { get; set; }
    public RolesDto Roles { get; set; } = default!;
}
