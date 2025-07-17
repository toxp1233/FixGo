using MediatR;
using System.Security.Cryptography;

namespace gizmogeo.Application.Auth.Commands.TokenCommands.GenerateRefreshToken;

public class GenerateRefreshTokenCommandHandler() : IRequestHandler<GenerateRefreshTokenCommand, string>
{
    public Task<string> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = GenerateRefreshToken();
        request.User.RefreshToken = refreshToken;
        request.User.RefreshTokenExpireDate = GenerateRefreshTokenExpiry();

        return Task.FromResult(refreshToken);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public DateTime GenerateRefreshTokenExpiry()
    {
        return DateTime.UtcNow.AddDays(7);
    }
}
