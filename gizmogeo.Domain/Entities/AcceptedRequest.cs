using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Enums;

public class AcceptedRequest
{
    public Guid Id { get; set; }

    public Guid ServiceRequestId { get; set; }
    public ServiceRequest ServiceRequest { get; set; } = default!;

    public string Message { get; set; } = default!;
    public decimal? EstimatedCost { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    public DateTime RespondedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public CompletedOrder? CompletedOrder { get; set; } 
    public ICollection<Attachment>? Attachments { get; set; } = [];
}
