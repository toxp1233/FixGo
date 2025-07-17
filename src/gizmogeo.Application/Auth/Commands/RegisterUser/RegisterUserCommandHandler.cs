using AutoMapper;
using gizmogeo.Application.Auth.Commands.SendPhoneCode;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper, IMediator mediator) : IRequestHandler<RegisterUserCommand, string>
{
    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userCheckName = await userRepository.GetByNameAsync(request.Name);
        var userChechPhoneNumber = await userRepository.GetByPhoneNumberAsync(request.PhoneNumber);

        if (userCheckName is not null)
            throw new AlreadyExistsException(nameof(User), userCheckName.Name);
        else if (userChechPhoneNumber is not null)
            throw new AlreadyExistsException(nameof(User), userChechPhoneNumber.PhoneNumber);

        var user = mapper.Map<User>(request);
        user.RoleId = 3;
        var newUser = await userRepository.CreateAsync(user);
        var codeSend = await mediator.Send(new SendPhoneVerificationCodeCommand(newUser.PhoneNumber), cancellationToken);
        return codeSend;
    }
}
