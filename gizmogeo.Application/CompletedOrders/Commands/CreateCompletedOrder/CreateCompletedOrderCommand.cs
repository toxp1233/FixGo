using gizmogeo.Application.CompletedOrders.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace gizmogeo.Application.CompletedOrders.Commands;

public class CreateCompletedOrderCommand : IRequest<CompletedOrderDto>
{
    public Guid AcceptedRequestId { get; set; }
    public decimal FinalCost { get; set; }
    public string? Notes { get; set; }
    public Guid CompletedByUserId { get; set; }
    public List<IFormFile> Attachments { get; set; } = [];
}
