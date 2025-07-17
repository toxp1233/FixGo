using MediatR;

namespace gizmogeo.Application.Auth.Commands.ResetPhone;

public record ResetPhoneCommand(string OldNumber, string NewNumber, string RecoveryCode) : IRequest<string>;
