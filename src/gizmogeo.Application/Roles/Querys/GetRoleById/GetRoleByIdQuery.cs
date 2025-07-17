using gizmogeo.Application.Roles.Dtos;
using MediatR;

namespace gizmogeo.Application.Roles.Querys.GetRoleById;

public record GetRoleByIdQuery(int Id) : IRequest<RolesDto>;
