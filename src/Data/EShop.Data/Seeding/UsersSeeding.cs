namespace EShop.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Identity;

    using EShop.Data.Models;

    using static EShop.Common.GlobalConstants;

    public class UsersSeeding : ISeeder
    {
        public async Task SeedAsync(EShopDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var users = new List<EShopUser>()
            {
                new EShopUser()
                {
                    Email = "admin@mail.com",
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    UserName = "admin@mail.com",
                    NormalizedUserName = "ADMIN@MAIL.COM",
                    FirstName = "Unknown",
                    LastName = "Unknown",
                    Address = "Unknown",
                    LockoutEnabled = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEEpB/M7AvuCJrfxUHqsSX012oMQ161mtL7hobXopkSOxUAzBIqdA4Hgc0HSGKRM7YQ==", // Password = 123456
                },
            };


            users[0].Roles.Add(new IdentityUserRole<string>()
            {
                RoleId = dbContext.Roles
                    .FirstOrDefault(r => r.Name == AdministratorRoleName)?.Id,
            });

            users[1].Roles.Add(new IdentityUserRole<string>()
            {
                RoleId = dbContext.Roles
                   .FirstOrDefault(r => r.Name == AdministratorRoleName)?.Id,
            });

            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }
    }
}
