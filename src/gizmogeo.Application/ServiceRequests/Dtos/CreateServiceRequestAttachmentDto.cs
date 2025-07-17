using Microsoft.AspNetCore.Http;

namespace gizmogeo.Application.ServiceRequests.Dtos;

public class CreateServiceRequestAttachmentDto
{
    public IFormFile File { get; set; } = default!;
}
