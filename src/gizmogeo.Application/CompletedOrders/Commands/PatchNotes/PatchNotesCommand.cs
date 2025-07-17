using gizmogeo.Application.CompletedOrders.Dtos;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Commands.PatchNotes;

public class PatchNotesCommand : IRequest<CompletedOrderDto>
{
    public Guid Id { get; set; }
    public string Notes { get; set; } = default!;
}
