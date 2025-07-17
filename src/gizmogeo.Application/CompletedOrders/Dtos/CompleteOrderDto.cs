namespace gizmogeo.Application.CompletedOrders.Dtos;

public class CompletedOrderDto
{
    public Guid Id { get; set; }
    public decimal FinalCost { get; set; }
    public string? Notes { get; set; }
    public DateTime CompletedAt { get; set; }

    public List<string> Attachments { get; set; } = default!;
    public string CompletedByName { get; set; } = default!;
    public string RequestedByName { get; set; } = default!;
}
