using AutoMapper;
using gizmogeo.Application.AcceptedRequests.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Commands.UpdateAcceptedRequest;

public class UpdateAcceptedRequestCommandHandler(IAcceptedRequestsRepository acceptedRequestsRepository, IMapper mapper) : IRequestHandler<UpdateAcceptedRequestCommand, AcceptedRequestByUserDto>
{
    public async Task<AcceptedRequestByUserDto> Handle(UpdateAcceptedRequestCommand request, CancellationToken cancellationToken)
    {
        var acceptedRequest = await acceptedRequestsRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(AcceptedRequest), request.Id.ToString());
        mapper.Map(request, acceptedRequest);
        await acceptedRequestsRepository.UpdateAsync(acceptedRequest);
        return mapper.Map<AcceptedRequestByUserDto>(acceptedRequest);
    }
}
