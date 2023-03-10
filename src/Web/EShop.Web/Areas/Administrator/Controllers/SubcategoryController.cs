namespace EShop.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using EShop.Services.Subcategory;
    using EShop.Models.ServiceModels.Subcategories;

    using static EShop.Common.GlobalConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AdministratorRoleName)]
    public class SubcategoryController : BaseController
    {
        private readonly ISubcategoryService subcategoryService;

        public SubcategoryController(ISubcategoryService subCategoryService)
        {
            this.subcategoryService = subCategoryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(SubcategoryServiceModel input)
        {
            this.subcategoryService.Create(input);

            return this.RedirectToPreviousPage();
        }

        public IActionResult ChooseToUpdate()
        {
            return this.View();
        }

        public IActionResult Update(int id)
        {
            var viewModel = this.subcategoryService.GetById(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(SubcategoryFormModel input)
        {
            this.subcategoryService.Update(input);

            return this.RedirectToPreviousPage();
        }

        [HttpPost]
        public IActionResult Delete(int subcategoryId)
        {
            this.subcategoryService.Delete(subcategoryId);

            return this.RedirectToPreviousPage();
        }
    }
}
