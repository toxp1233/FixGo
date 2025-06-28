using gizmogeo.Application.Auth.Dtos;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.TokenCommands.RefreshToken;

public record RefreshTokenCommand(Guid UserId, string RefreshToken) : IRequest<TokenDto>;
