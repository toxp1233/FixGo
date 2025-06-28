using gizmogeo.Application.AcceptedRequests.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace gizmogeo.Application.AcceptedRequests.Commands.CreateAcceptedRequest;

public class CreateAcceptedRequestCommand : IRequest<AcceptedRequestByUserDto>
{
    public Guid UserId { get; set; }
    public Guid ServiceRequestId { get; set; }
    public string Message { get; set; } = default!;
    public decimal? EstimatedCost { get; set; }
    public List<IFormFile>? Attachments { get; set; } 
}
