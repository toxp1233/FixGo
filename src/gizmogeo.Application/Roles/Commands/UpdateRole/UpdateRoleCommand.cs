using gizmogeo.Application.Roles.Dtos;
using MediatR;

namespace gizmogeo.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<RolesDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
