namespace EShop.Models.ViewModels
{
    using System.Collections.Generic;

    public class ArticleViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? EcoPrice { get; set; }

        public decimal? DiscountPrice { get; set; }

        public string PictureUrl { get; set; }

        public string Category { get; set; }

        public string Subcategory { get; set; }

        public ICollection<ArticleParameter> Parameters { get; set; }
    }
}
