using AutoMapper;
using gizmogeo.Application.ServiceRequests.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Querys.GetServiceById;

public class GetServiceByIdQueryHandler(IServiceRequestRepository serviceRequestRepository, IMapper mapper) : IRequestHandler<GetServiceByIdQuery, ServiceRequestDto>
{
    public async Task<ServiceRequestDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var serviceRequest = await serviceRequestRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(ServiceRequest), request.Id.ToString());
        return mapper.Map<ServiceRequestDto>(serviceRequest);   
    }
}
