namespace gizmogeo.Application.Category.Dtos;

public class ServiceRequestDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
