namespace EShop.Models.ServiceModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryServiceModel
    {
        [Required]
        public string Name { get; set; }
    }
}
