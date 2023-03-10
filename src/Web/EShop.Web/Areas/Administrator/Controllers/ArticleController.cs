namespace EShop.Web.Areas.Administrator.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using EShop.Models.ServiceModels.Articles;
    using EShop.Services.Articles;

    using static EShop.Common.GlobalConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AdministratorRoleName)]
    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleFormModel input)
        {
            await this.articleService.Create(input);

            return this.RedirectToPreviousPage();
        }

        public IActionResult ChooseToUpdate()
        {
            return this.View();
        }

        public IActionResult Update(string id)
        {
            var viewModel = this.articleService.GetById(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(ArticleFormModel input)
        {
            this.articleService.Update(input);

            return this.Redirect("/");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            return this.RedirectToPreviousPage();
        }
    }
}
