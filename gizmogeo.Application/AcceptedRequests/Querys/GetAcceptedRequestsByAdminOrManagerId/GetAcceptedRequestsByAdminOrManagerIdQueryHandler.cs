using AutoMapper;
using gizmogeo.Application.AcceptedRequests.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Querys.GetAcceptedRequestsByAdminOrManagerId;

public class GetAcceptedRequestsByAdminOrManagerIdQueryHandler(IAcceptedRequestsRepository acceptedRequestsRepository, IMapper mapper) : IRequestHandler<GetAcceptedRequestsByAdminOrManagerIdQuery, IEnumerable<AcceptedRequestByUserDto>>
{
    public async Task<IEnumerable<AcceptedRequestByUserDto>> Handle(GetAcceptedRequestsByAdminOrManagerIdQuery request, CancellationToken cancellationToken)
    {
        var acceptedRequests = await acceptedRequestsRepository.GetByUserIdAsync(request.Id) ?? throw new NotFoundException(nameof(AcceptedRequest), request.Id.ToString());
        return mapper.Map<IEnumerable<AcceptedRequestByUserDto>>(acceptedRequests);
    }
}
