using MediatR;
namespace gizmogeo.Application.Auth.Commands.TokenCommands.GenerateToken;

public record GenerateTokenCommand(User User) : IRequest<string>;
