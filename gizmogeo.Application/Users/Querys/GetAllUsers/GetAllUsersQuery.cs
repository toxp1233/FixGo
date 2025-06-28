using gizmogeo.Application.Users.Dtos;
using MediatR;

namespace gizmogeo.Application.Users.Querys.GetAllUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;

