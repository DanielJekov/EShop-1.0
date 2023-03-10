namespace EShop.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using EShop.Services.Categories;
    using EShop.Models.ServiceModels.Categories;

    using static EShop.Common.GlobalConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AdministratorRoleName)]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CategoryServiceModel input)
        {
            this.categoryService.Create(input);

            return this.RedirectToPreviousPage();
        }

        public IActionResult ChooseToUpdate()
        {
            return this.View();
        }

        public IActionResult Update(int id)
        {
            var viewModel = this.categoryService.GetById(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(CategoryFormModel input)
        {
            this.categoryService.Update(input);

            return this.RedirectToPreviousPage();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            return this.RedirectToPreviousPage();
        }
    }
}
