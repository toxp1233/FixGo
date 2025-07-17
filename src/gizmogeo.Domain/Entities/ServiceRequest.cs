using gizmogeo.Domain.Entities;

public class ServiceRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public ICollection<Attachment> Attachments { get; set; } = [];

    public AcceptedRequest? AcceptedRequest { get; set; } 
}
