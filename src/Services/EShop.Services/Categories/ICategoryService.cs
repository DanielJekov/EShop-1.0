namespace EShop.Services.Categories
{
    using System.Collections.Generic;

    using EShop.Models.ServiceModels;
    using EShop.Models.ServiceModels.Categories;
    using EShop.Models.ViewModels;

    public interface ICategoryService
    {
        public void Create(CategoryServiceModel input);

        public int Update(CategoryFormModel input);

        public void Delete(int id);

        public CategoryFormModel GetById(int id);

        public ICollection<CategoryViewModel> AllIncludingSubcategories();

        public ICollection<CategoryViewModel> All();

        public int GetCount();

        public bool IsExistById(int categoryId);

        public bool IsExistByName(string name);
    }
}
