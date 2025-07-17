using gizmogeo.Application.common;
using gizmogeo.Application.Users.Dtos;
using MediatR;

namespace gizmogeo.Application.Users.Querys.GetAllUsers;

public record GetAllUsersQuery(PaginationParams PaginationParams) : IRequest<PaginatedList<UserDto>>;
