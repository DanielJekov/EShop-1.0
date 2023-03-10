namespace EShop.Models.ServiceModels.Subcategories
{
    using System.ComponentModel.DataAnnotations;

    public class SubcategoryFormModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Position { get; set; }
    }
}
