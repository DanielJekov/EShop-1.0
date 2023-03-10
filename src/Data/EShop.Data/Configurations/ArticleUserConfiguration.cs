namespace EShop.Data.Configurations
{
    using EShop.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ArticleUserConfiguration : IEntityTypeConfiguration<ArticlesUsers>
    {
        public void Configure(EntityTypeBuilder<ArticlesUsers> builder)
        {
            builder.HasKey(x => new { x.ArticleId, x.UserId });
        }
    }
}
