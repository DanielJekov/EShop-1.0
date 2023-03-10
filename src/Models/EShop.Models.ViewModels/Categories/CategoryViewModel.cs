namespace EShop.Models.ViewModels
{
    using System.Collections.Generic;

    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Possition { get; set; }

        public ICollection<SubcategoryViewModel> Subcategories { get; set; }
    }
}
