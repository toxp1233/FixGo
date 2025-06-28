using MediatR;

namespace gizmogeo.Application.Auth.Commands.TokenCommands.ValidateRefreshToken;

public record ValidateRefreshTokenCommand(Guid userId, string refreshToken) : IRequest<User?>;

