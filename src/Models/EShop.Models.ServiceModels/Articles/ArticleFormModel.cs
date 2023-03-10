namespace EShop.Models.ServiceModels.Articles
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class ArticleFormModel : ParametersFormModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [MaxLength(450, ErrorMessage = "Максималната дължина е 450 символа")]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? EcoPrice { get; set; }

        public decimal? DiscountPrice { get; set; }

        public IFormFile Picture { get; set; }

        public bool IsDeleted { get; set; }

        public int  CategoryId { get; set; }

        public int?  SubcategoryId { get; set; }

        public bool IsOnHeadPage { get; set; }
    }
}
