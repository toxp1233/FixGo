using gizmogeo.Application.Users.Dtos;
using MediatR;

namespace gizmogeo.Application.Users.Querys.GetUserByName;

public record GetUserByNameQuery(string Name) : IRequest<UserDto>;
