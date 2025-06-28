using AutoMapper;
using gizmogeo.Application.Users.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userCheckName = await userRepository.GetByNameAsync(request.Name);
        var userChechPhoneNumber = await userRepository.GetByPhoneNumberAsync(request.PhoneNumber);

        if (userCheckName is not null)
            throw new AlreadyExistsException(nameof(User), userCheckName.Name);
        else if (userChechPhoneNumber is not null)
            throw new AlreadyExistsException(nameof(User), userChechPhoneNumber.PhoneNumber);

        var user = mapper.Map<User>(request);
        await userRepository.CreateAsync(user);
        return mapper.Map<UserDto>(user);
    }
}
