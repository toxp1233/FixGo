namespace gizmogeo.Application.Category.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<ServiceRequestDto>? ServiceRequests { get; set; } = [];
    public ICollection<AcceptedRequestDto>? AcceptedRequests { get; set; } = [];
}
