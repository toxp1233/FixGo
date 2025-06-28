using AutoMapper;
using gizmogeo.Application.ServiceRequests.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Querys.GetServicesByUserId;

public class GetServicesByUserIdQueryHandler(IServiceRequestRepository serviceRequestRepository, IMapper mapper) : IRequestHandler<GetServicesByUserIdQuery, IEnumerable<ServiceRequestDto>>
{
    public async Task<IEnumerable<ServiceRequestDto>> Handle(GetServicesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var serviceRequest = await serviceRequestRepository.GetByUserId(request.UserId) ?? throw new NotFoundException(nameof(ServiceRequest), request.UserId.ToString());
        return mapper.Map<IEnumerable<ServiceRequestDto>>(serviceRequest);
    }
}
