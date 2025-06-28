using gizmogeo.Domain.Enums;

namespace gizmogeo.Application.AcceptedRequests.Dtos;

public class AcceptedRequestByUserDto
{
    public Guid Id { get; set; }
    public string Message { get; set; } = default!;
    public decimal? EstimatedCost { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime RespondedAt { get; set; }
    public string CategoryName { get; set; } = default!;
    public List<string> AttachmentUrls { get; set; } = [];
}
