using AutoMapper;
using gizmogeo.Application.Roles.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Roles.Querys.GetRoleById;

public class GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper) : IRequestHandler<GetRoleByIdQuery, RolesDto>
{
    public async Task<RolesDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Role), request.Id.ToString());
        return mapper.Map<RolesDto>(role);
    }
}
