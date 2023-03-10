namespace EShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;
    using EShop.Data.Models.Enums;

    public class Purchase : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        [MaxLength(450)]
        public string StatusDescription { get; set; }

        [MaxLength(450)]
        public string UserDescription { get; set; }

        public string ExternalAddress { get; set; }

        [Required]
        public string UserId { get; set; }
        public EShopUser User { get; set; }

        public DateTime CreatedOn { get; set; }
        = DateTime.UtcNow;

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<ArticlesPurchases> Articles { get; set; }
        = new HashSet<ArticlesPurchases>();
    }
}
