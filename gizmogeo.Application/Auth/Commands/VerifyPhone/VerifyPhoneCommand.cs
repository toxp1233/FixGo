using MediatR;
using gizmogeo.Application.Auth.Dtos;

namespace gizmogeo.Application.Auth.Commands.VerifyPhone;

public class VerifyPhoneCommand : IRequest<TokenDto>
{
    public string PhoneNumber { get; set; } = default!;
    public string Code { get; set; } = default!;
}
