using gizmogeo.Application.Users.Dtos;
using MediatR;

namespace gizmogeo.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<UserDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}
