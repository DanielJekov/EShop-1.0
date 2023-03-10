namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Parameter
    {
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
