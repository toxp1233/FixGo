using AutoMapper;
using gizmogeo.Application.AcceptedRequests.Dtos;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Querys.GetAllUsers;

public class GetAllAcceptedRequestsQueryHandler(IAcceptedRequestsRepository acceptedRequestsRepository, IMapper mapper) : IRequestHandler<GetAllAcceptedRequestsQuery, IEnumerable<AcceptedRequestByUserDto>>
{
    public async Task<IEnumerable<AcceptedRequestByUserDto>> Handle(GetAllAcceptedRequestsQuery request, CancellationToken cancellationToken)
    {
        var acceptedRequests = await acceptedRequestsRepository.GetAllAsync();       
        return mapper.Map<IEnumerable<AcceptedRequestByUserDto>>(acceptedRequests); 
    }
}
