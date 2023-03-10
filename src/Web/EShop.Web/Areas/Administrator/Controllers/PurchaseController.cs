namespace EShop.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static EShop.Common.GlobalConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AdministratorRoleName)]
    public class PurchaseController : ControllerBase
    {
    }
}
