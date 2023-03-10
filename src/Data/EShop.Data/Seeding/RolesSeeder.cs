namespace EShop.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using EShop.Data.Models;

    using static  EShop.Common.GlobalConstants;
    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(EShopDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<EShopRole>>();

            await SeedRoleAsync(roleManager, AdministratorRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<EShopRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new EShopRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
