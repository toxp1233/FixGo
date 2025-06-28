using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.TokenCommands.ValidateRefreshToken;

public class ValidateRefreshTokenCommandHandler(IUserRepository userRepository) : IRequestHandler<ValidateRefreshTokenCommand, User?>
{
    public async Task<User?> Handle(ValidateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId);
        if (user is null || user.RefreshToken != request.refreshToken
            || user.RefreshTokenExpireDate <= DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }
}
