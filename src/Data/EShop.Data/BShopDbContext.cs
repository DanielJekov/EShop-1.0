namespace EShop.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using EShop.Data.Models;

    public class EShopDbContext : IdentityDbContext<EShopUser, EShopRole, string>
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Subcategory> Subcategories { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Parameter> Parameters { get; set; }

        public DbSet<ArticlesUsers> ArticlesUsers { get; set; }

        public DbSet<ArticlesPurchases> ArticlesPurchases { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<QuickPurchase> QuickPurchases { get; set; }

        public DbSet<CompanyDetails> CompanyDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
