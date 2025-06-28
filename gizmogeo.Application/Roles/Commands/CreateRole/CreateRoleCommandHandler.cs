using AutoMapper;
using gizmogeo.Application.Roles.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper) : IRequestHandler<CreateRoleCommand, RolesDto> 
{
    public async Task<RolesDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRole = await roleRepository.GetByNameAsync(request.Name);
        if (existingRole is not null)
        {
            throw new AlreadyExistsException(nameof(Role), existingRole.Name);
        }
        var role = mapper.Map<Role>(request);
        var newRole = await roleRepository.CreateAsync(role);
        return mapper.Map<RolesDto>(newRole);
    }
}
