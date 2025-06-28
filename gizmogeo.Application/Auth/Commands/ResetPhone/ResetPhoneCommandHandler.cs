using gizmogeo.Application.Auth.Commands.RecoverAccontPhoneCode;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.ResetPhone;

public class ResetPhoneCommandHandler(IUserRepository userRepository, IMediator mediator) : IRequestHandler<ResetPhoneCommand, string>
{
    public async Task<string> Handle(ResetPhoneCommand request, CancellationToken cancellationToken)
    {
        var resetableUser = await userRepository.GetByPhoneNumberAsync(request.OldNumber) ?? throw new NotFoundException(nameof(User), request.OldNumber);
        if (!BCrypt.Net.BCrypt.Verify(request.RecoveryCode, resetableUser.RecoveryCodeHash))
            throw new UnauthorizedAccessException("Invalid code");
        
        var resetedUser = await mediator.Send(new RecoverAccountPhoneCodeCommand(request.NewNumber));
        resetableUser.PendingPhoneNumber = request.NewNumber;
        await userRepository.UpdateAsync(resetableUser);
        return resetedUser;
    }
}
