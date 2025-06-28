using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.RecoverAccontPhoneCode;

public class RecoverAccountPhoneCodeCommandHandler(IUserRepository userRepository, IPhoneVerificationService phoneVerificationService) : IRequestHandler<RecoverAccountPhoneCodeCommand, string>
{
    public async Task<string> Handle(RecoverAccountPhoneCodeCommand request, CancellationToken cancellationToken)
    {
        var recoverableAccount = await userRepository.GetByPendingPhoneNumberAsync(request.PhoneNumber) ?? throw new NotFoundException(nameof(User), request.PhoneNumber);
        await phoneVerificationService.SendAsync(request.PhoneNumber);
        return "Verification code sent.";
    }
}
