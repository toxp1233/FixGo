using gizmogeo.Application.Users.Dtos;
using MediatR;

namespace gizmogeo.Application.Users.Commands.AssignRole;

public record AssignRoleCommand(Guid UserId, int RoleId) : IRequest<UserDto>;
