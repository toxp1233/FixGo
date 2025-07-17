using AutoMapper;
using gizmogeo.Application.Roles.Dtos;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Roles.Querys.GetAllRoles;

public class GetAllRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper) : IRequestHandler<GetAllRolesQuery, IEnumerable<RolesDto>>
{
    public async Task<IEnumerable<RolesDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var Roles = await roleRepository.GetAllAsync();
        return mapper.Map<IEnumerable<RolesDto>>(Roles);
    }
}
