using gizmogeo.Application.Auth.Commands.SendPhoneCode;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.SendPhoneVerification;

public class SendPhoneVerificationCodeCommandHandler(
    IUserRepository userRepository,
    IPhoneVerificationService phoneVerificationService
) : IRequestHandler<SendPhoneVerificationCodeCommand, string>
{
    public async Task<string> Handle(SendPhoneVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByPhoneNumberAsync(request.PhoneNumber) ?? throw new NotFoundException(nameof(User), request.PhoneNumber);


        await phoneVerificationService.SendAsync(request.PhoneNumber);
        return "Verification code sent.";
    }
}
