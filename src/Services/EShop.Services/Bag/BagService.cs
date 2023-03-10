namespace EShop.Services.Bag
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EShop.Data;
    using EShop.Data.Models;
    using EShop.Models.ServiceModels.Bag;
    using EShop.Models.ViewModels;
    using EShop.Models.ViewModels.Articles;
    using EShop.Models.ViewModels.Bag;
    using EShop.Services.Articles;

    using static EShop.Common.GlobalConstants;

    public class BagService : IBagService
    {
        private readonly EShopDbContext data;
        private readonly IArticleService articleService;

        public BagService(
            EShopDbContext data,
            IArticleService articleService)
        {
            this.data = data;
            this.articleService = articleService;
        }

        public void Add(string articleId, string userId)
        {
            this.data.ArticlesUsers.Add(new ArticlesUsers { ArticleId = articleId, UserId = userId, ArticleQuantity = 1 });
            this.data.SaveChanges();
        }

        public void Remove(string articleId, string userId)
        {
            var article = this.data.ArticlesUsers.Where(x => x.Article.Id == articleId && x.UserId == userId).ToList();
            this.data.BulkDelete(article);
        }

        public ICollection<ArticleViewModel> GetArticlesFromBag(string userId)
        {
            return this.data.ArticlesUsers
                            .Where(x => x.User.Id == userId)
                            .Select(x => new ArticleViewModel
                            {
                                Id = x.Article.Id,
                                Name = x.Article.Name,
                                Price = Math.Round(x.Article.Price, 2),
                                EcoPrice = x.Article.EcoPrice == null ? null : Math.Round(x.Article.EcoPrice.Value, 2),
                                DiscountPrice = x.Article.DiscountPrice == null ? null : Math.Round(x.Article.DiscountPrice.Value, 2),
                                Category = x.Article.Category.Name,
                                Subcategory = x.Article.Subcategory.Name,
                                PictureUrl = x.Article.PictureUrl,
                                Parameters = x.Article.Parameters
                                                      .Where(p => p.Article.Id == x.Article.Id)
                                                      .Select(p => new ArticleParameter
                                                      {
                                                          Key = p.Key,
                                                          Value = p.Value
                                                      })
                                                      .ToList(),
                            })
                            .ToList();
        }

        public bool IsAlreadyAdded(string articleId, string userId)
        {
            return this.data.ArticlesUsers.Any(x => x.Article.Id == articleId && x.User.Id == userId);
        }

        private int GetOrderIndependentHashCode<T>(IEnumerable<T> source)
        {
            int hash = 0;
            foreach (T element in source)
            {
                hash = hash ^ EqualityComparer<T>.Default.GetHashCode(element);
            }
            return hash;
        }

        public bool Purchase(FinishPurchaseInputModel input, string userId)
        {
            //var userData = this.data.Users.Where(u => u.Id == userId).FirstOrDefault();

            var articlesFromInput = input.Articles.Select(x => x.ArticleId).ToArray();
            var articlesFromBag = this.data.ArticlesUsers
                                        .Where(b => b.User.Id == userId)
                                        .Select(x => x.Article.Id)
                                        .ToArray();

            var articlesFromBagHash = GetOrderIndependentHashCode<string>(articlesFromBag);
            var articlesFromInputHash = GetOrderIndependentHashCode<string>(articlesFromInput);


            if (articlesFromBagHash != articlesFromInputHash)
            {
                return false;
            };

            var totalPriceOfPurchase = this.articleService.CalculateTotalPrice(input.Articles.ToList());

            var purchase = new Purchase()
            {
                UserId = userId,
                UserDescription = input.Description,
                ExternalAddress = input.Address,
                Status = Data.Models.Enums.Status.Process,
                TotalPrice = totalPriceOfPurchase,
            };


            this.data.Purchases.Add(purchase);
            this.data.SaveChanges();

            foreach (var article in input.Articles)
            {
                var item = new ArticlesPurchases()
                {
                    ArticleId = article.ArticleId,
                    PurchaseId = purchase.Id,
                    Quantity = article.Quantity,
                    ConstPrice = this.data.Articles
                                            .Where(x => x.Id == article.ArticleId)
                                            .Select(x => x.DiscountPrice == null ? x.Price : x.DiscountPrice.Value)
                                            .FirstOrDefault()
                };

                purchase.Articles.Add(item);
            }

            this.data.SaveChanges();

            return true;
        }

        public bool QuickPurchase(QuickPurchaseInputModel input)
        {
            var articlePrice = this.data.Articles.Where(a => a.Id == input.ArticleId).Select(x => new { x.Price, x.DiscountPrice }).FirstOrDefault();
            var quickPurchase = new QuickPurchase()
            {
                Names = input.Names,
                PhoneNumber = input.PhoneNumber,
                Address = input.Address,
                UserDescription = input.Description,
                Email = input.Email,
                ArticleId = input.ArticleId,
                Status = Data.Models.Enums.Status.Process,
            };

            if (articlePrice.DiscountPrice == null)
            {
                quickPurchase.Price = articlePrice.Price;
            }
            else
            {
                quickPurchase.Price = articlePrice.DiscountPrice.Value;
            }

            this.data.QuickPurchases.Add(quickPurchase);
            this.data.SaveChanges();

            return true;
        }

        public void ClearBag(string userId)
        {
            var articles = this.data.ArticlesUsers.Where(b => b.User.Id == userId).ToList();
            this.data.ArticlesUsers.BulkDelete(articles);
        }

        public OrdeViewModel Purchases()
        {
            var quickPurchases = this.data.QuickPurchases
                .Select(p => new PurchaseViewModel
                {
                    Description = p.UserDescription,
                    ExternalAddress = p.Address,
                    Email = p.Email,
                    Names = p.Names,
                    CreatedOn = p.CreatedOn,
                    PhoneNumber = p.PhoneNumber,
                    StatusForm = new StatusFormModel
                    {
                        PurchaseId = p.Id,
                        Status = (Status)p.Status,
                        Description = p.StatusDescription,
                    },
                    TotalPrice = p.Price,
                    Articles = new List<ArticleShortDataViewModel>(){
                        new ArticleShortDataViewModel{
                        Name = p.Article.Name,
                        Quantity = 1,
                        Price = p.Article.Price,
                        }
                    }
                })
                .ToList();

            var purchases = this.data.Purchases
                                     .Select(p => new PurchaseViewModel
                                     {
                                         Description = p.UserDescription,
                                         ExternalAddress = p.User.Address,
                                         Email = p.User.Email,
                                         Names = p.User.FirstName + " " + p.User.LastName,
                                         CreatedOn = p.CreatedOn,
                                         PhoneNumber = p.User.PhoneNumber,
                                         StatusForm = new StatusFormModel
                                         {
                                             PurchaseId = p.Id,
                                             Status = (Status)p.Status,
                                             Description = p.StatusDescription,
                                         },
                                         TotalPrice = p.TotalPrice,
                                         Articles = p.Articles
                                                     .Select(x => new ArticleShortDataViewModel()
                                                     {
                                                         Name = x.Article.Name,
                                                         Quantity = x.Quantity,
                                                         Price = x.ConstPrice
                                                     })
                                                     .ToList()
                                     })
                                     .ToList();

            purchases.AddRange(quickPurchases);
            var ordersViewModel = new OrdeViewModel();
            ordersViewModel.Purchases = purchases;
            ordersViewModel.FinishedCount = purchases.Where(x => x.StatusForm.Status == Status.Finished).Count();
            ordersViewModel.ProcessingCount = purchases.Where(x => x.StatusForm.Status == Status.Process).Count();
            ordersViewModel.ShippedCount = purchases.Where(x => x.StatusForm.Status == Status.Sended).Count();
            ordersViewModel.RejectedCount = purchases.Where(x => x.StatusForm.Status == Status.Rejected).Count();

            return ordersViewModel;
        }

        public void ProcessPurchase(StatusFormModel input)
        {
            var purchase = this.data.Purchases.Where(x => x.Id == input.PurchaseId).FirstOrDefault();
            if (purchase != null)
            {
                purchase.Status = (Data.Models.Enums.Status)input.Status;
                purchase.StatusDescription = input.Description;
            }
            else
            {
                var quickPurchase = this.data.QuickPurchases.Where(x => x.Id == input.PurchaseId).FirstOrDefault();

                quickPurchase.Status = (Data.Models.Enums.Status)input.Status;
                quickPurchase.StatusDescription = input.Description;
            }
            this.data.SaveChanges();
        }
    }
}
