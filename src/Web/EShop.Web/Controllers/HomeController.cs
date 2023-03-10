namespace EShop.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using EShop.Models.ViewModels;
    using EShop.Services.Articles;
    using EShop.Models.ViewModels.Articles;

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService articleService;

        public HomeController(
            IArticleService articleService,
            ILogger<HomeController> logger
            )
        {
            this.articleService = articleService;
            this._logger = logger;
        }

        public IActionResult Index(IndexPageViewModel viewModel)
        {
            viewModel.Newer10 = articleService.GetLast10Added();
            viewModel.OnFocus = articleService.GetHeadingArticles();

            return View(viewModel);
        }

        [Route("/poveritelnost")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/dopylnitelni-uslugi")]
        public IActionResult AdditionalServices()
        {
            return View();
        }

        [Route("/kontakti")]
        public IActionResult Contacts()
        {
            return View();
        }

        [Route("/za-nas")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
