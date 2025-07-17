using gizmogeo.Application.Roles.Dtos;
using MediatR;

namespace gizmogeo.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand(string Name, string Description) : IRequest<RolesDto>;
