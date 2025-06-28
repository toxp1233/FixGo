using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.RecoveryCodeHasher;

public class RecoveryCodeHasherCommandHandler(IUserRepository userRepository) : IRequestHandler<RecoveryCodeHasherCommand, string>
{
    public async Task<string> Handle(RecoveryCodeHasherCommand request, CancellationToken cancellationToken)
    {
        var recoveryCode = Random.Shared.Next(100000, 999999).ToString();
        var hashedCode = BCrypt.Net.BCrypt.HashPassword(recoveryCode);

        request.User.RecoveryCodeHash = hashedCode;
        await userRepository.UpdateAsync(request.User);

        return recoveryCode;
    }
}
