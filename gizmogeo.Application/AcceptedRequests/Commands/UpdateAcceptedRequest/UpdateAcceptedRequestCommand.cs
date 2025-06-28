using gizmogeo.Application.AcceptedRequests.Dtos;
using gizmogeo.Domain.Enums;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Commands.UpdateAcceptedRequest;

public class UpdateAcceptedRequestCommand : IRequest<AcceptedRequestByUserDto>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; } = default!;
    public decimal? EstimatedCost { get; set; }
    public RequestStatus Status { get; set; }
}
