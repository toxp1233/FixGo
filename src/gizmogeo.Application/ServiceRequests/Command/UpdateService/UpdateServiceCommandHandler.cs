using AutoMapper;
using gizmogeo.Application.ServiceRequests.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Command.UpdateService;

public class UpdateServiceCommandHandler(IServiceRequestRepository serviceRequestRepository, IMapper mapper) : IRequestHandler<UpdateServiceCommand, ServiceRequestDto>
{
    public async Task<ServiceRequestDto> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var serviceRequest = await serviceRequestRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(ServiceRequest), request.Id.ToString());
        mapper.Map(request, serviceRequest);
        await serviceRequestRepository.UpdateAsync(serviceRequest);
        return mapper.Map<ServiceRequestDto>(serviceRequest);
    }
}
