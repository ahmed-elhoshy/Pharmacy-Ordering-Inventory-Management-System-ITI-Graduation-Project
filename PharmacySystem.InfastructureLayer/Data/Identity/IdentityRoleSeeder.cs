using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PharmacySystem.DomainLayer.Entities.Constants;


namespace PharmacySystem.InfastructureLayer.Data.Identity
{
    public static class IdentityRoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "Pharmacy", "WarehouseManager", "Representative" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
