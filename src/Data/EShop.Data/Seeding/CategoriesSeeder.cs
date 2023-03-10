namespace EShop.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(EShopDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            List<Category> categories = new List<Category>()
            {
                new Category() { Name = "Акумулатори" },
                new Category() { Name = "Гуми" },
                new Category() { Name = "Аксесоари" },
            };

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();

            HashSet<Subcategory> subcategories = new HashSet<Subcategory>()
            {
                new Subcategory() { Name = "Стартерни", Position = 1, CategoryId = categories[0].Id },
                new Subcategory() { Name = "Start-stop", Position = 2, CategoryId = categories[0].Id },
                new Subcategory() { Name = "Тягови ", Position = 3, CategoryId = categories[0].Id },
                new Subcategory() { Name = "Мото ", Position = 4, CategoryId = categories[0].Id },
            };

            await dbContext.Subcategories.AddRangeAsync(subcategories);
            await dbContext.SaveChangesAsync();
        }
    }
}
