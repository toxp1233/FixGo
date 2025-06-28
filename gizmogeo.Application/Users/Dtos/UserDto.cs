namespace gizmogeo.Application.Users.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public decimal Balance { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public bool IsPhoneNumberConfirmed { get; set; }
    public string RoleName { get; set; } = default!;
}
