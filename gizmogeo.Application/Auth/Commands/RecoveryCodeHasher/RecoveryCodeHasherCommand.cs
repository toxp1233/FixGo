using MediatR;

namespace gizmogeo.Application.Auth.Commands.RecoveryCodeHasher;

public record RecoveryCodeHasherCommand(User User) : IRequest<string>;

