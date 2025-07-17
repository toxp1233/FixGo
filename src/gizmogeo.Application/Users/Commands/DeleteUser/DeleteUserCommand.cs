using MediatR;

namespace gizmogeo.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest;
