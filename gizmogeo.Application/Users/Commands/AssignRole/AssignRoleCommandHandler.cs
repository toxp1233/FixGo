using AutoMapper;
using gizmogeo.Application.Users.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Users.Commands.AssignRole;
public class AssignRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
    : IRequestHandler<AssignRoleCommand, UserDto>
{
    public async Task<UserDto> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            throw new NotFoundException(nameof(User), request.UserId.ToString());

        var role = await roleRepository.GetByIdAsync(request.RoleId);
        if (role is null)
            throw new NotFoundException(nameof(Role), request.RoleId.ToString());

        user.RoleId = role.Id;
        user.Role = role;

        await userRepository.UpdateAsync(user);
        return mapper.Map<UserDto>(user);
    }
}
