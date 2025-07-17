using gizmogeo.Domain.Enums;

namespace gizmogeo.Application.ServiceRequests.Dtos;

public class AcceptedRequestsDto
{
    
     public Guid Id { get; set; }
     public string Message { get; set; } = default!;
     public decimal? EstimatedCost { get; set; }
     public RequestStatus Status { get; set; }
     public DateTime RespondedAt { get; set; }

}
