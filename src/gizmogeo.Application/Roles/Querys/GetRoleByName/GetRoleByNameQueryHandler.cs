using AutoMapper;
using gizmogeo.Application.Roles.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Roles.Querys.GetRoleByName;

public class GetRoleByNameQueryHandler(IRoleRepository roleRepository, IMapper mapper) : IRequestHandler<GetRoleByNameQuery, RolesDto>
{
    public async Task<RolesDto> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByNameAsync(request.Name) ?? throw new NotFoundException(nameof(Role), request.Name);
        return mapper.Map<RolesDto>(role);
    }
}
