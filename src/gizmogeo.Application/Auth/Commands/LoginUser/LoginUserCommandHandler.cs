using gizmogeo.Application.Auth.Commands.SendPhoneCode;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.LoginUser;

public class LoginUserCommandHandler(IUserRepository userRepository, IMediator mediator)
    : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByPhoneNumberAsync(request.PhoneNumber) ?? throw new NotFoundException(nameof(User), request.PhoneNumber);
        if (!user.IsPhoneNumberConfirmed)
        {
            await mediator.Send(new SendPhoneVerificationCodeCommand(user.PhoneNumber), cancellationToken);
            return "Verification code re-sent. Please verify to log in.";
        }

        var phoneCode = await mediator.Send(new SendPhoneVerificationCodeCommand(user.PhoneNumber), cancellationToken);
        return phoneCode;
    }
}
