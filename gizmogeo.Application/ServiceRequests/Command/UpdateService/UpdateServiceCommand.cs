using gizmogeo.Application.ServiceRequests.Dtos;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Command.UpdateService;

public class UpdateServiceCommand : IRequest<ServiceRequestDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
}
