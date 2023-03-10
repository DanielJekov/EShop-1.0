namespace EShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class Article : IDeletableEntity, IAuditInfo
    {
        public Article()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(450)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? EcoPrice { get; set; }

        public decimal? DiscountPrice { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }
        = DateTime.UtcNow;

        public bool IsOnHeadPage { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Url]
        public string PictureUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int? SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public ICollection<ArticlesUsers> ArticlesUsers { get; set; }
        = new HashSet<ArticlesUsers>();

        public ICollection<QuickPurchase> QuickPurchases { get; set; }
        = new HashSet<QuickPurchase>();

        public ICollection<ArticlesPurchases> Purchases { get; set; }
        = new HashSet<ArticlesPurchases>();

        public ICollection<Parameter> Parameters { get; set; }
        = new HashSet<Parameter>();
    }
}
