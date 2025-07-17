namespace gizmogeo.Domain.Entities;

public class CompletedOrder
{
    public Guid Id { get; set; }

    public Guid? AcceptedRequestId { get; set; }
    public AcceptedRequest? AcceptedRequest { get; set; }

    public decimal FinalCost { get; set; }
    public string? Notes { get; set; } 

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public Guid CompletedByUserId { get; set; }
    public User CompletedByUser { get; set; } = default!;
    public ICollection<Attachment> Attachments { get; set; } = [];

    public string CompletedByName { get; set; } = default!;
    public string RequestedByName { get; set; } = default!;
}
