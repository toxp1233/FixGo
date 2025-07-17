using MediatR;

namespace gizmogeo.Application.Auth.Commands.RecoverAccontPhoneCode;

public class RecoverAccountPhoneCodeCommand(string phoneNumber) : IRequest<string>
{
    public string PhoneNumber { get; set; } = phoneNumber;
}
