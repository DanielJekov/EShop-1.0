namespace EShop.Services.Subcategory
{
    using System.Collections.Generic;

    using EShop.Models.ServiceModels;
    using EShop.Models.ServiceModels.Subcategories;
    using EShop.Models.ViewModels;

    public interface ISubcategoryService
    {
        public void Create(SubcategoryServiceModel input);

        public int Update(SubcategoryFormModel input);

        public void Delete(int subCategoryId);

        public SubcategoryFormModel GetById(int id);

        public ICollection<SubcategoryViewModel> GetAll();

        public int GetCount();

        public bool IsExistById(int subcategoryId);
        public bool IsExistByName(string name);
    }
}
