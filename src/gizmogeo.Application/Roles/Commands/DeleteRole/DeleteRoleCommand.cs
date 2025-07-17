using MediatR;

namespace gizmogeo.Application.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(int Id) : IRequest;

