using gizmogeo.Application.Roles.Dtos;
using MediatR;

namespace gizmogeo.Application.Roles.Querys.GetRoleByName;

public record GetRoleByNameQuery(string Name) : IRequest<RolesDto>;
