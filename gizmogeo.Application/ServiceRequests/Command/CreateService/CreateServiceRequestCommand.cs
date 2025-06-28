using gizmogeo.Application.ServiceRequests.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace gizmogeo.Application.ServiceRequests.Command.CreateService;

public class CreateServiceRequestCommand : IRequest<ServiceRequestDto>
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid UserId { get; set; }                  
    public int CategoryId { get; set; }
    public List<IFormFile> Attachments { get; set; } = [];
}
