namespace EShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;
    using EShop.Data.Models.Enums;

    public class QuickPurchase : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Names { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [MaxLength(450)]
        public string UserDescription { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Status Status { get; set; }

        [MaxLength(450)]
        public string StatusDescription { get; set; }

        [Required]
        [MaxLength(36)]
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public DateTime CreatedOn { get; set; }
        = DateTime.UtcNow;

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
