using AutoMapper;
using gizmogeo.Application.Roles.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper) : IRequestHandler<UpdateRoleCommand, RolesDto>
{
    public async Task<RolesDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Role), request.Id.ToString());
        mapper.Map(request, role);
        var updatedRole = await roleRepository.UpdateAsync(role);
        return mapper.Map<RolesDto>(updatedRole); 
    }
}
