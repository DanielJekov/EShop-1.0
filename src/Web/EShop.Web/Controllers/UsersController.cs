namespace EShop.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using EShop.Services.User;
    using EShop.Models.ServiceModels.Users;

    using static EShop.Common.GlobalConstants;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult Edit(UserFormModel input)
        {
            var userId = this.GetUserId();
            input.Id = userId;

            var countOfChanges = this.userService.Edit(input);

            this.TempData[GlobalMessageSuccess] = $"Успешно бяха запаметени промените";

            return this.RedirectToPreviousPage();
        }
    }
}
