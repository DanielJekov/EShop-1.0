namespace EShop.Services.Categories
{
    using System.Collections.Generic;
    using System.Linq;

    using EShop.Data;
    using EShop.Data.Models;
    using EShop.Models.ServiceModels;
    using EShop.Models.ServiceModels.Categories;
    using EShop.Models.ViewModels;

    public class CategoryService : ICategoryService
    {
        private readonly EShopDbContext data;

        public CategoryService(EShopDbContext data)
        {
            this.data = data;
        }

        public void Create(CategoryServiceModel input)
        {
            var nextPossition = 1;
            var possition = this.GetCount() + nextPossition;
            var category = new Category
            {
                Name = input.Name,
                Position = possition
            };

            this.data.Categories.Add(category);
            this.data.SaveChanges();
        }

        public int Update(CategoryFormModel input)
        {
            var category = this.data.Categories
                                    .Where(c => c.Id == input.Id)
                                    .FirstOrDefault();

            var category1 = this.data.Categories
                                     .Where(c => c.Position == input.Position)
                                     .FirstOrDefault();

            if (category1 != null)
            {
                category1.Position = category.Position;
            }

            category.Name = input.Name;
            category.Position = input.Position;

            return this.data.SaveChanges();
        }


        public void Delete(int id)
        {
            var category = this.data.Categories
                                    .Where(sc => sc.Id == id)
                                    .ToList();

            this.data.BulkDelete<Category>(category);
        }

        public CategoryFormModel GetById(int id)
        {
            return this.data.Categories
                            .Where(c => c.Id == id)
                            .Select(c => new CategoryFormModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Position = c.Position
                            })
                            .FirstOrDefault();
        }

        public ICollection<CategoryViewModel> All()
        {
            return this.data.Categories
                            .Select(c => new CategoryViewModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Possition = c.Position,
                            })
                            .OrderBy(c => c.Possition)
                            .ToList();
        }

        public ICollection<CategoryViewModel> AllIncludingSubcategories()
        {
            return this.data.Categories
                            .Select(c => new CategoryViewModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Possition = c.Position,
                                Subcategories = c.Subcategories
                                                 .Select(sc => new SubcategoryViewModel
                                                 {
                                                     Id = sc.Id,
                                                     Name = sc.Name,
                                                     Position = sc.Position,
                                                 })
                                                 .OrderBy(sc => sc.Position)
                                                 .ToList()
                            })
                            .OrderBy(c => c.Possition)
                            .ToList();
        }

        public int GetCount()
        {
            return this.data.Categories.Count();
        }

        public bool IsExistById(int categoryId)
        {
            return this.data.Categories.Any(c => c.Id == categoryId);
        }

        public bool IsExistByName(string name)
        {
            return this.data.Categories.Any(c => c.Name == name);
        }
    }
}
