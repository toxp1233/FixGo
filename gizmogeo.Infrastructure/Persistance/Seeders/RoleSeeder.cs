using gizmogeo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Persistance.Seeders;

public static class RoleSeeder
{
    public static async Task SeedAsync(FixGoDbContext context)
    {

        if (!await context.Roles.AnyAsync())
        {
            var roles = new List<Role>
            {
                new() { Name = "Admin", Description = "Administrator, Manages Users and Websites" },
                new() { Name = "Manager", Description = "Manager, Manages Requests and Finalizes the Orders"},
                new() { Name = "User", Description = "Regular User, Uses the website for purposes " }
            };

            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }
    }
}
