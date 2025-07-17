using gizmogeo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Persistance.Seeders;

public static class UserSeeder
{
    public static async Task SeedUsers(FixGoDbContext context)
    {

        if (!await context.Users.AnyAsync())
        {
            var users = new List<User>
            {
            new() {
                Id = Guid.NewGuid(),
                Name = "Admin",
                CreatedAt = DateTime.UtcNow,
                PhoneNumber = "+995511246968",
                IsPhoneNumberConfirmed = true,
                RoleId = 1
                },

            new() {
                Id = Guid.NewGuid(),
                Name = "Manager",
                CreatedAt = DateTime.UtcNow,
                PhoneNumber = "+995511246967",
                IsPhoneNumberConfirmed = true,
                RoleId = 2
                },

            new() {
                Id = Guid.NewGuid(),
                Name = "User",
                CreatedAt = DateTime.UtcNow,
                PhoneNumber = "+995511246966",
                IsPhoneNumberConfirmed = true,
                RoleId = 3
                }

            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
    }

}
