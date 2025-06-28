using gizmogeo.Application.Users.Dtos;
using MediatR;

namespace gizmogeo.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Name, string PhoneNumber, int RoleId) : IRequest<UserDto>;
