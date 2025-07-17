using AutoMapper;
using AutoMapper.QueryableExtensions;
using gizmogeo.Application.common;
using gizmogeo.Application.Users.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.Users.Querys.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, PaginatedList<UserDto>>
{
    public async Task<PaginatedList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = userRepository.GetQueryable();

        if (!string.IsNullOrEmpty(request.PaginationParams.Search))
        {
            var search = request.PaginationParams.Search.ToLower();
            query = query.Where(u =>
                u.Name.ToLower().Contains(search) ||
                u.PhoneNumber.ToLower().Contains(search)) ?? throw new NotFoundException(nameof(User), request.PaginationParams.Search);
        }

        var dtoQuery = query.ProjectTo<UserDto>(mapper.ConfigurationProvider);

        return await PaginatedList<UserDto>.CreateAsync(dtoQuery,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize);
    }

}
