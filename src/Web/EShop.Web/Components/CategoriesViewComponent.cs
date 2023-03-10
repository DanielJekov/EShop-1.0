namespace EShop.Web.Components
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EShop.Services.Categories;

    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService categoriesService;

        public CategoriesViewComponent(ICategoryService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = this.categoriesService.AllIncludingSubcategories();

            return this.View(viewModel);
        }
    }
}
