namespace EShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using EShop.Data.Common.Models;

    public class EShopRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public EShopRole()
            : this(null)
        {
        }

        public EShopRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        override public string Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }
        = DateTime.UtcNow;

        public DateTime? ModifiedOn { get; set; }
    }
}
