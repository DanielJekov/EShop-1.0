namespace EShop.Data
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(EShopDbContext dbContext, IServiceProvider serviceProvider);
    }
}
