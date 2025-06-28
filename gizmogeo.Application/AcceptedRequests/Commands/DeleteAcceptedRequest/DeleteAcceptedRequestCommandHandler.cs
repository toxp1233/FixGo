using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Commands.DeleteAcceptedRequest;

public class DeleteAcceptedRequestCommandHandler(IAcceptedRequestsRepository acceptedRequestsRepository) : IRequestHandler<DeleteAcceptedRequestCommand>
{
    public async Task Handle(DeleteAcceptedRequestCommand request, CancellationToken cancellationToken)
    {
        var acceptedRequest = await acceptedRequestsRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(AcceptedRequest), request.Id.ToString());
        await acceptedRequestsRepository.DeleteAsync(acceptedRequest);
    }
}
