namespace EShop.Models.ServiceModels.Articles
{
    using System.ComponentModel.DataAnnotations;

    public class ArticlePurchaseInputModel
    {
        [Required]
        [MinLength(36)]
        [MaxLength(36)]
        public string ArticleId { get; set; }

        public int Quantity { get; set; }
    }
}
