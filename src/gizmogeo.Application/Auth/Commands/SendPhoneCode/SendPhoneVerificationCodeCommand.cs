using MediatR;

namespace gizmogeo.Application.Auth.Commands.SendPhoneCode;

public class SendPhoneVerificationCodeCommand(string phoneNumber) : IRequest<string>
{
    public string PhoneNumber { get; set; } = phoneNumber;
}
