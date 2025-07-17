using AutoMapper;
using gizmogeo.Application.Users.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Users.Querys.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(User), request.Id.ToString());
        return mapper.Map<UserDto>(user);
    }
}
