using AutoMapper;
using gizmogeo.Application.AcceptedRequests.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Querys.GetAcceptedRequestById;

public class GetAcceptedRequestByIdQueryHandler(IAcceptedRequestsRepository acceptedRequestsRepository, IMapper mapper) : IRequestHandler<GetAcceptedRequestByIdQuery, AcceptedRequestByUserDto>
{
    public async Task<AcceptedRequestByUserDto> Handle(GetAcceptedRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var acceptedRequest = await acceptedRequestsRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(AcceptedRequest), request.Id.ToString());
        return mapper.Map<AcceptedRequestByUserDto>(acceptedRequest);
    }
}
