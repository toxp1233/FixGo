using gizmogeo.Application.Roles.Dtos;
using MediatR;

namespace gizmogeo.Application.Roles.Querys.GetAllRoles;

public record GetAllRolesQuery : IRequest<IEnumerable<RolesDto>>;

