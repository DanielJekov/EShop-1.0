namespace EShop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using EShop.Services.Articles;

    using static EShop.Common.GlobalConstants;
    using EShop.Services.Categories;
    using EShop.Services.Subcategory;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;
        private readonly ISubcategoryService subcategoryService;

        public ArticleController(IArticleService articleService,
            ICategoryService categoryService,
            ISubcategoryService subcategoryService)
        {
            this.articleService = articleService;
            this.categoryService = categoryService;
            this.subcategoryService = subcategoryService;
        }

        [Route("kategoria")]
        public IActionResult GetByCategory(string ime)
        {
            var name = ime;
            var isExistCategory = this.categoryService.IsExistByName(name);
            if (!isExistCategory)
            {
                this.TempData[GlobalMessageWarning] = "Изберете категория от менюто";
                return this.Redirect("/");
            }

            var viewModel = this.articleService.GetByCategory(name);
            this.ViewData["Title"] = name;

            return this.View(viewModel);
        }

        [Route("podkategoria")]
        public IActionResult GetBySubcategory(string ime)
        {
            var name = ime;
            var isExistSubcategory = this.subcategoryService.IsExistByName(name);
            if (!isExistSubcategory)
            {
                this.TempData[GlobalMessageWarning] = "Изберете подкатегория от менюто";
                return this.Redirect("/");
            }
            var viewModel = this.articleService.GetBySubcategory(name);
            this.ViewData["Title"] = name;

            return this.View(viewModel);
        }

        [Route("/artikul/")]
        public IActionResult Specification(string nomer)
        {
            var id = nomer;

            var isExist = this.articleService.IsExistById(id);
            if (!isExist)
            {
                return this.Redirect("/Error404");
            }
            
            var viewModel = this.articleService.Get(id);

            return this.View(viewModel);

        }
    }
}
