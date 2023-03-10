namespace EShop.Models.ServiceModels.Subcategories
{
    using System.ComponentModel.DataAnnotations;
    public class SubcategoryServiceModel
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Position { get; set; }
    }
}
