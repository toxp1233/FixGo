using gizmogeo.Application.Users.Dtos;
using MediatR;

namespace gizmogeo.Application.Users.Querys.GetUserByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IRequest<UserDto>;
