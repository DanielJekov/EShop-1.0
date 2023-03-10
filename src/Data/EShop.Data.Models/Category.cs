namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(36)]
        public string Name { get; set; }

        public int Position { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }
        = new HashSet<Subcategory>();

        public ICollection<Article> Articles { get; set; }
         = new HashSet<Article>();
    }
}
