namespace EShop.Models.ServiceModels.Categories
{
    using System.ComponentModel.DataAnnotations;
    public class CategoryFormModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public int  Position { get; set; }
    }
}
