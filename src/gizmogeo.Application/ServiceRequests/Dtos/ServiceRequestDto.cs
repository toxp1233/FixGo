using gizmogeo.Application.Category.Dtos;

namespace gizmogeo.Application.ServiceRequests.Dtos;

public class ServiceRequestDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public AcceptedRequestDto? AcceptedRequest { get; set; }
    public ICollection<AttachmentsDto> Attachments { get; set; } = [];
}
