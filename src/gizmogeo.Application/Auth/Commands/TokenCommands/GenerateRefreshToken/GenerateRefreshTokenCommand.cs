using MediatR;

namespace gizmogeo.Application.Auth.Commands.TokenCommands.GenerateRefreshToken;

public record GenerateRefreshTokenCommand(User User) : IRequest<string>;

