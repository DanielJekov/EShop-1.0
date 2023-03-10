namespace EShop.Web.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    using EShop.Services.Articles;
    using EShop.Services.Bag;
    using EShop.Services.User;
    using EShop.Models.ServiceModels.Bag;
    using EShop.Models.ServiceModels.Articles;

    using static EShop.Common.GlobalConstants;
   
    [Authorize]
    public class BagController : BaseController
    {
        private readonly IBagService bagService;
        private readonly IArticleService articleService;
        private readonly IUserService userService;

        public BagController(IBagService bagService,
            IArticleService articleService,
            IUserService userService)
        {
            this.bagService = bagService;
            this.articleService = articleService;
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult Add(string articleId)
        {
            var userId = this.GetUserId();

            var isExist = this.articleService.IsExistById(articleId);

            if (!isExist)
            {
                this.TempData[GlobalMessageFailure] = "Неуспешно добавяне на артикул.";

                return this.RedirectToPreviousPage();
            }

            bool isAlreadyAdded = this.bagService.IsAlreadyAdded(articleId, userId);
            if (isAlreadyAdded)
            {
                this.TempData[GlobalMessageWarning] = "Този артикул вече е добавен във вашата кошница.";
                return this.RedirectToPreviousPage();
            }

            this.bagService.Add(articleId, userId);
            this.TempData[GlobalMessageSuccess] = "Успешно добавихте артикул в кошницата.";

            return this.RedirectToPreviousPage();
        }

        public IActionResult Remove(string id)
        {
            var userId = this.GetUserId();
            var isExist = this.articleService.IsExistById(id);
            var isInTheBag = this.bagService.IsAlreadyAdded(id, userId);
            if (!isExist || !isInTheBag)
            {
                this.TempData[GlobalMessageFailure] = "Неуспешно премахване артикул";

                return this.RedirectToPreviousPage();
            }

            this.bagService.Remove(id, userId);
            this.TempData[GlobalMessageSuccess] = "Успешно премахнат артикул";

            return this.RedirectToPreviousPage();
        }

        [Route("kolichka")]
        public IActionResult List()
        {
            var userId = this.GetUserId();
            var viewModel = this.bagService.GetArticlesFromBag(userId);

            return this.View(viewModel);
        }

        [Route("finalizirane-na-porychka")]
        public IActionResult FinishPurchase()
        {
            var input = ProcessRawPurchasedArticlesWithQuantity();

            if (input == null)
            {
                return this.BadRequest();
            }

            var viewModel = new FinishPurchaseInputModel()
            {
                Articles = input
            };

            var userId = this.GetUserId();
            var userData = this.userService.GetShippingData(userId);
            viewModel.Names = userData.FirstName + " " + userData.LastName;
            viewModel.PhoneNumber = userData.PhoneNumber;
            viewModel.Email = userData.Email;
            viewModel.Address = userData.Address;
            viewModel.TotalPrice = this.articleService.CalculateTotalPrice(input);

            this.TempData["Articles"] = JsonConvert.SerializeObject(input);

            return this.View(viewModel);
        }

        [Route("finalizirane-na-porychka")]
        [HttpPost]
        public IActionResult FinishPurchase(FinishPurchaseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData[GlobalMessageFailure] = "Неуспешна поръчка";

                return this.RedirectToPreviousPage();
            }

            input.Articles = JsonConvert.DeserializeObject<IEnumerable<ArticlePurchaseInputModel>>(TempData["Articles"].ToString());

            if (input.Articles == null)
            {
                return this.BadRequest();
            }

            var userId = this.GetUserId();

            foreach (var article in input.Articles)
            {
                var isValidArticle = this.articleService.IsExistById(article.ArticleId);
                if (!isValidArticle)
                {
                    return this.BadRequest();
                }
            }

            var isSuccsesful = this.bagService.Purchase(input, userId);
            if (!isSuccsesful)
            {
                return this.BadRequest();
            }

            this.bagService.ClearBag(userId);
            this.TempData[GlobalMessageSuccess] = "Успешно направена поръчка.";

            return this.Redirect("/");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult QuickPurchase(QuickPurchaseInputModel input)
        {
           
            if (!this.ModelState.IsValid)
            {
                this.TempData[GlobalMessageFailure] = this.ModelStateErrorCollector();

                return this.RedirectToPreviousPage();
            }

            var isValidItem = this.articleService.IsExistById(input.ArticleId);

            if (!isValidItem)
            {
                this.TempData[GlobalMessageFailure] = "Невалиден артикул. Моля опитайте отново.";

                return this.RedirectToPreviousPage();
            }

            this.bagService.QuickPurchase(input);

            this.TempData[GlobalMessageSuccess] = "Успешно направена поръчка.";

            return this.RedirectToPreviousPage();
        }

        private ICollection<ArticlePurchaseInputModel> ProcessRawPurchasedArticlesWithQuantity()
        {
            var input = new List<ArticlePurchaseInputModel>();
            var inputRaw = this.HttpContext.Request.Query;
            ;
            string[] articles;
            string[] quantities;
            for (int i = 0; i < inputRaw["Quantity"].Count; i++)
            {
                articles = inputRaw["articleId"].ToString().Split(",", System.StringSplitOptions.RemoveEmptyEntries);
                quantities = inputRaw["Quantity"].ToString().Split(",", System.StringSplitOptions.RemoveEmptyEntries);

                var isQuantityParsed = int.TryParse(quantities[i], out int parsedQuantity);
                if (!this.articleService.IsExistById(articles[i]) || !isQuantityParsed)
                {
                    return null;
                }

                input.Add(new ArticlePurchaseInputModel { ArticleId = articles[i], Quantity = parsedQuantity });
            }

            return input;
        }
    }
}
