namespace EShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using EShop.Data.Common.Models;

    public class EShopUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public EShopUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public override string Id { get; set; }

        [Required]
        [MaxLength(25)]
        [PersonalData]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        [PersonalData]
        public string LastName { get; set; }

        [Required]
        [PersonalData]
        public string Address { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }
        = DateTime.UtcNow;

        public DateTime? ModifiedOn { get; set; }

        public ICollection<ArticlesUsers> ArticlesUsers { get; set; }
        = new HashSet<ArticlesUsers>();

        public ICollection<Purchase> Purchases { get; set; }
         = new HashSet<Purchase>();

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
        = new HashSet<IdentityUserRole<string>>();

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        = new HashSet<IdentityUserClaim<string>>();

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        = new HashSet<IdentityUserLogin<string>>();
    }
}
