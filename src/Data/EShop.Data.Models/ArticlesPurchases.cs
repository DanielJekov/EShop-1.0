namespace EShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class ArticlesPurchases : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public int Quantity { get; set; }

        //Current price at the time when purchase is made.
        public decimal ConstPrice { get; set; }

        public DateTime CreatedOn { get; set; }
        = DateTime.UtcNow;

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
