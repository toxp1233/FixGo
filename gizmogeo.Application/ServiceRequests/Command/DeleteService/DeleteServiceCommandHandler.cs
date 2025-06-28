using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Command.DeleteService;

public class DeleteServiceCommandHandler(IServiceRequestRepository serviceRequestRepository) : IRequestHandler<DeleteServiceCommand>
{
    public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var serviceRequest = await serviceRequestRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(ServiceRequest), request.Id.ToString());
        await serviceRequestRepository.DeleteAsync(serviceRequest);
    }
}
