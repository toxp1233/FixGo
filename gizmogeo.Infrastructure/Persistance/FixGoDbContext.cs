using gizmogeo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Persistance;

public class FixGoDbContext(DbContextOptions<FixGoDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Role> Roles { get; set; }
    internal DbSet<ServiceRequest> ServiceRequests { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Attachment> Attachments { get; set; }
    internal DbSet<AcceptedRequest> AcceptedRequests { get; set; }
    internal DbSet<CompletedOrder> CompletedOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<ServiceRequest>()
            .HasOne(sr => sr.User)
            .WithMany(u => u.ServiceRequests)
            .HasForeignKey(sr => sr.UserId);

        modelBuilder.Entity<ServiceRequest>()
            .HasOne(sr => sr.Category)
            .WithMany(c => c.ServiceRequests)
            .HasForeignKey(sr => sr.CategoryId);

        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.ServiceRequest)
            .WithMany(sr => sr.Attachments)
            .HasForeignKey(a => a.ServiceRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.CompletedOrder)
            .WithMany(co => co.Attachments)
            .HasForeignKey(a => a.CompletedOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.AcceptedRequest)
            .WithMany(ar => ar.Attachments)
            .HasForeignKey(a => a.AcceptedRequestId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AcceptedRequest>()
            .HasOne(ar => ar.User)
            .WithMany(u => u.AcceptedRequestsAsAdmin)
            .HasForeignKey(ar => ar.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AcceptedRequest>()
            .HasOne(ar => ar.Category)
            .WithMany(c => c.AcceptedRequests)
            .HasForeignKey(ar => ar.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CompletedOrder>()
            .HasOne(co => co.CompletedByUser)
            .WithMany(u => u.CompletedOrders)
            .HasForeignKey(co => co.CompletedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ServiceRequest>()
            .HasOne(sr => sr.AcceptedRequest)
            .WithOne(ar => ar.ServiceRequest)
            .HasForeignKey<AcceptedRequest>(ar => ar.ServiceRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AcceptedRequest>()
            .HasOne(ar => ar.CompletedOrder)
            .WithOne(co => co.AcceptedRequest)
            .HasForeignKey<CompletedOrder>(co => co.AcceptedRequestId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}
