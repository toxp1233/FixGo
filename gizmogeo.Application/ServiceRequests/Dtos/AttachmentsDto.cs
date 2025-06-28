namespace gizmogeo.Application.ServiceRequests.Dtos;

public class AttachmentsDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = default!;        // Original name
    public string FilePath { get; set; } = default!;        // Saved path (e.g., /uploads/...)
    public string ContentType { get; set; } = default!;     // image/jpeg, video/mp4, etc.
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
