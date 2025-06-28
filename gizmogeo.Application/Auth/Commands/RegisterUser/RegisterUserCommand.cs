using MediatR;

namespace gizmogeo.Application.Auth.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<string>
{
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
};
