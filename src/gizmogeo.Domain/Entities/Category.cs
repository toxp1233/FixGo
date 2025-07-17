namespace gizmogeo.Domain.Entities;

public class Category
{   
     public int Id { get; set; }
     public string Name { get; set; } = default!;

     public ICollection<ServiceRequest> ServiceRequests { get; set; } = [];
     public ICollection<AcceptedRequest> AcceptedRequests { get; set; } = [];
}
