namespace EShop.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using EShop.Services.Bag;

    using static EShop.Common.GlobalConstants;
    using EShop.Models.ServiceModels.Bag;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AdministratorRoleName)]
    public class BagController : BaseController
    {
        private readonly IBagService bagService;

        public BagController(IBagService bagService)
        {
            this.bagService = bagService;
        }

        public IActionResult Purchases()
        {
            var viewModel = this.bagService.Purchases();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult PurchaseProcess(StatusFormModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData[GlobalMessageFailure] = this.ModelStateErrorCollector();
                return this.RedirectToPreviousPage();
            }

            this.bagService.ProcessPurchase(input);

            return this.RedirectToPreviousPage();
        }
    }
}
