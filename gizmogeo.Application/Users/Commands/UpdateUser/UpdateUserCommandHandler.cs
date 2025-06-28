using AutoMapper;
using gizmogeo.Application.Users.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Users.Commands.UpdateUser;

internal class UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<UpdateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(User), request.Id.ToString());

        mapper.Map(request, user);
        await userRepository.UpdateAsync(user);
        return mapper.Map<UserDto>(user);
    }
}
