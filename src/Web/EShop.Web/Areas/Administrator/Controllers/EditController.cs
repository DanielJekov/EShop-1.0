namespace EShop.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static EShop.Common.GlobalConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AdministratorRoleName)]
    public class EditController : BaseController
    {
        public IActionResult Location()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contacts()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult PhoneNumber()
        {
            return this.View();
        }
    }
}
