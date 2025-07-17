namespace gizmogeo.Domain.Entities;

public class Attachment
{
    public Guid Id { get; set; }

    public Guid? ServiceRequestId { get; set; }
    public ServiceRequest? ServiceRequest { get; set; }

    public Guid? CompletedOrderId { get; set; }
    public CompletedOrder? CompletedOrder { get; set; } 

    public Guid? AcceptedRequestId { get; set; }
    public AcceptedRequest? AcceptedRequest { get; set; } 

    public string FileName { get; set; } = default!;        
    public string FilePath { get; set; } = default!;        
    public string ContentType { get; set; } = default!;     
    public string PublicId { get; set; } = default!;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}

