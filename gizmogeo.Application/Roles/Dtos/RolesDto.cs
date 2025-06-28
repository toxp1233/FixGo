namespace gizmogeo.Application.Roles.Dtos;

public class RolesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public ICollection<UserDto>? Users { get; set; }
}
