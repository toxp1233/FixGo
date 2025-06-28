using AutoMapper;
using gizmogeo.Application.Users.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Users.Querys.GetUserByName;

public class GetUserByNameQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserByNameQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByNameAsync(request.Name) ?? throw new NotFoundException(nameof(User), request.Name);
        return mapper.Map<UserDto>(user);
    }
}
