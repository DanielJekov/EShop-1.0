namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Subcategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Position { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Article> Articles { get; set; }
        = new HashSet<Article>();
    }
}
