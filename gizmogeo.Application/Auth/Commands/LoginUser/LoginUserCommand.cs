using gizmogeo.Application.Auth.Dtos;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.LoginUser;

public record LoginUserCommand(string PhoneNumber) : IRequest<string>;
