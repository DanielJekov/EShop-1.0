namespace EShop.Services.Subcategory
{
    using System.Collections.Generic;
    using System.Linq;

    using EShop.Data;
    using EShop.Data.Models;
    using EShop.Models.ServiceModels.Subcategories;
    using EShop.Models.ViewModels;

    public class SubcategoryService : ISubcategoryService
    {
        private readonly EShopDbContext data;

        public SubcategoryService(EShopDbContext data)
        {
            this.data = data;
        }

        public void Create(SubcategoryServiceModel input)
        {
            var subCategory = new Subcategory()
            {
                Name = input.Name,
                Position = input.Position,
            };

            var category = this.data.Categories.FirstOrDefault(c => c.Id == input.CategoryId);

            category.Subcategories.Add(subCategory);
            this.data.SaveChanges();

        }

        public int Update(SubcategoryFormModel input)
        {
            var subcategory = this.data.Subcategories
                                       .Where(sc => sc.Id == input.Id)
                                       .FirstOrDefault();

            subcategory.Name = input.Name;
            subcategory.Position = input.Position;

            return this.data.SaveChanges();
        }

        public void Delete(int subCategoryId)
        {
            var subcategory = this.data.Subcategories.Where(sc => sc.Id == subCategoryId).ToList();
            this.data.BulkDelete<Subcategory>(subcategory);
        }

        public SubcategoryFormModel GetById(int id)
        {
            return this.data.Subcategories
                            .Where(sc => sc.Id == id)
                            .Select(sc => new SubcategoryFormModel
                            {
                                Id = sc.Id,
                                Name = sc.Name,
                                Position = sc.Position
                            })
                            .FirstOrDefault();
        }

        public ICollection<SubcategoryViewModel> GetAll()
        {
            return this.data.Subcategories
                          .Select(sc => new SubcategoryViewModel
                          {
                              Id = sc.Id,
                              Name = sc.Name,
                              Position = sc.Position,
                          })
                          .ToList();
        }

        public ICollection<SubcategoryViewModel> GetAllByCategoryId(int id)
        {
            return this.data.Subcategories
                            .Where(sb => sb.Category.Id == id)
                            .Select(sc => new SubcategoryViewModel
                            {
                                Id = sc.Id,
                                Name = sc.Name,
                                Position = sc.Position,
                            })
                            .ToList();
        }

        public int GetCount()
        {
            return this.data.Subcategories.Count();
        }

        public bool IsExistById(int subcategoryId)
        {
           return this.data.Subcategories.Any(sc => sc.Id == subcategoryId);
        }

        public bool IsExistByName(string name)
        {
            return this.data.Subcategories.Any(sc => sc.Name == name);
        }
    }
}
