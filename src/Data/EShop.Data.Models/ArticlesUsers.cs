namespace EShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class ArticlesUsers : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public int ArticleQuantity { get; set; }

        [Required]
        [MaxLength(36)]
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        [Required]
        [MaxLength(36)]
        public string UserId { get; set; }
        public EShopUser User { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }
        = DateTime.UtcNow;

        public DateTime? ModifiedOn { get; set; }
    }
}
