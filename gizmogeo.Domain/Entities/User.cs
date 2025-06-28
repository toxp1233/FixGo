using gizmogeo.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public decimal Balance { get; set; } = 0;
    public string PhoneNumber { get; set; } = default!;
    public bool IsPhoneNumberConfirmed { get; set; } = false;
    public string? RecoveryCodeHash { get; set; }
    public string? PendingPhoneNumber { get; set; }

    public string? RefreshToken { get; set; } 
    public DateTime? RefreshTokenExpireDate { get; set; }

    public int RoleId { get; set; } 
    public Role Role { get; set; } = default!;

    public ICollection<ServiceRequest>? ServiceRequests { get; set; } = [];
    public ICollection<AcceptedRequest>? AcceptedRequestsAsAdmin { get; set; } = [];
    public ICollection<CompletedOrder>? CompletedOrders { get; set; } = [];
}

