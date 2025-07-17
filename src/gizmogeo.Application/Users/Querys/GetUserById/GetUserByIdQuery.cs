using gizmogeo.Application.Users.Dtos;
using MediatR;

namespace gizmogeo.Application.Users.Querys.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
