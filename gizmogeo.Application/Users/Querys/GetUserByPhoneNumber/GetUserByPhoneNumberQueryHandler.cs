using AutoMapper;
using gizmogeo.Application.Users.Dtos;
using gizmogeo.Application.Users.Querys.GetUserByPhoneNumber;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Users.Querys.GetByPhoneNumber;

public class GetUserByPhoneNumberQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserByPhoneNumberQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByPhoneNumberAsync(request.PhoneNumber) ?? throw new NotFoundException(nameof(User), request.PhoneNumber);
        return mapper.Map<UserDto>(user);
    }
}
