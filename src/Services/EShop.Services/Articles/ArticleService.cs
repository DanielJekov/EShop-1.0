namespace EShop.Services.Articles
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using EShop.Data;
    using EShop.Data.Models;
    using EShop.Models.ServiceModels.Articles;
    using EShop.Models.ViewModels;
    using EShop.Services.Cloudinary;

    public class ArticleService : IArticleService
    {
        private readonly EShopDbContext data;
        private readonly ICloudinaryService cloudinaryService;

        public ArticleService(EShopDbContext data, ICloudinaryService cloudinaryService)
        {
            this.cloudinaryService = cloudinaryService;
            this.data = data;
        }

        public async Task Create(ArticleFormModel input)
        {
            var parameters = new List<Parameter>();

            var maxCountOfKeyValuePairs = 20;
            for (int i = 1; i <= maxCountOfKeyValuePairs; i++)
            {
                var key = input.GetType().GetProperty("Key" + i).GetValue(input);
                var value = input.GetType().GetProperty("Value" + i).GetValue(input);
                if (key != null && value != null)
                {
                    parameters.Add(new Parameter() { Key = key.ToString().Trim(), Value = value.ToString().Trim() });
                }
            }
            var article = new Article()
            {
                Name = input.Name,
                Price = input.Price,
                EcoPrice = input.EcoPrice,
                Description = input.Description,
                DiscountPrice = input.DiscountPrice,
                CategoryId = input.CategoryId,
                SubcategoryId = input.SubcategoryId,
                IsOnHeadPage = input.IsOnHeadPage,
                Parameters = parameters,
            };

            string pictureUrl = string.Empty;
            if (input.Picture != null)
            {
                using var ms = new MemoryStream();
                input.Picture.CopyTo(ms);
                var destinationData = ms.ToArray();


                pictureUrl = await this.cloudinaryService.UploadPictureAsync(destinationData, "Article", "Articles", 350, 350);
                article.PictureUrl = pictureUrl;
            }
            else
            {
                pictureUrl = "/no-image-icon.png";
            }

            article.PictureUrl = pictureUrl;
            this.data.Articles.Add(article);
            this.data.SaveChanges();
        }

        public async Task<int> Update(ArticleFormModel input)
        {
            var article = this.data.Articles.Where(a => a.Id == input.Id)
                                            .Include(x => x.Parameters)
                                            .FirstOrDefault();

            string pictureUrl = string.Empty;
            if (input.Picture != null)
            {
                using var ms = new MemoryStream();
                input.Picture.CopyTo(ms);
                var destinationData = ms.ToArray();


                pictureUrl = await this.cloudinaryService.UploadPictureAsync(destinationData, "Article", "Articles", 350, 350);
                article.PictureUrl = pictureUrl;
            }

            article.Name = input.Name;
            article.Description = input.Description;
            article.Price = input.Price;
            article.EcoPrice = input.EcoPrice;
            article.DiscountPrice = input.DiscountPrice;
            article.IsDeleted = input.IsDeleted;
            article.CategoryId = input.CategoryId;
            article.SubcategoryId = input.SubcategoryId;
            article.IsOnHeadPage = input.IsOnHeadPage;
            if (article.Parameters.Count > 0)
            {
                article.Parameters.ToList().RemoveRange(0, article.Parameters.Count - 1);
            }
          
            this.data.SaveChanges();
            var parameters = new List<Parameter>();

            var maxCountOfKeyValuePairs = 20;
            for (int i = 1; i <= maxCountOfKeyValuePairs; i++)
            {
                var key = input.GetType().GetProperty("Key" + i).GetValue(input);
                var value = input.GetType().GetProperty("Value" + i).GetValue(input);
                if (key != null && value != null)
                {
                    parameters.Add(new Parameter() { Key = key.ToString().Trim(), Value = value.ToString().Trim() });
                }
            }
            article.Parameters = parameters;
            var countOfChanges = this.data.SaveChanges();

            var unusedParams = this.data.Parameters.Where(x => x.ArticleId == null).ToList();
            this.data.Parameters.BulkDelete(unusedParams);

            return countOfChanges;
        }

        public bool IsArticlePurchased(string articleId)
        {
            var isPurchased = this.data.QuickPurchases.Any(p => p.Article.Id == articleId);
            if (isPurchased == true)
            {
                return isPurchased;
            }
            isPurchased = this.data.Purchases.Any(p => p.Articles.Any(x => x.Article.Id == articleId) == true);

            return isPurchased;
        }

        public void Delete(string id)
        {
            var article = this.data.Articles.Where(a => a.Id == id).FirstOrDefault();
            article.IsDeleted = true;

            this.data.SaveChangesAsync();
        }

        public ArticleFormModel GetById(string articleId)
        {
            var article = this.data.Articles
                            .Where(a => a.Id == articleId)
                            .Select(a => new ArticleFormModel
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Description = a.Description,
                                Price = Math.Round(a.Price, 2),
                                EcoPrice = a.EcoPrice == null ? null : Math.Round(a.EcoPrice.Value, 2),
                                DiscountPrice = a.DiscountPrice == null ? null : Math.Round(a.DiscountPrice.Value, 2),
                                CategoryId = a.Category.Id,
                                SubcategoryId = a.Subcategory.Id,
                                IsDeleted = a.IsDeleted,
                            })
                            .FirstOrDefault();

            var parametersRaw = this.data.Parameters
                .Where(a => a.Article.Id == articleId)
                .Select(a => new 
                {
                    Key= a.Key,
                    Value = a.Value,
                })
                .ToList();
            ;
            for (int i = 1; i <= parametersRaw.Count; i++)
            {
                article.GetType().GetProperty("Key" + i).SetValue(article, parametersRaw[i-1].Key);
                article.GetType().GetProperty("Value" + i).SetValue(article, parametersRaw[i-1].Value);
            }
            return article;
        }

        public ArticleViewModel Get(string articleId)
        {
            return this.data.Articles
                            .Where(a => a.Id == articleId)
                            .Select(a => new ArticleViewModel
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Description = a.Description,
                                Price = Math.Round(a.Price, 2),
                                EcoPrice = a.EcoPrice == null ? null : Math.Round(a.EcoPrice.Value, 2),
                                DiscountPrice = a.DiscountPrice == null ? null : Math.Round(a.DiscountPrice.Value, 2),
                                Category = a.Category.Name,
                                Subcategory = a.Subcategory.Name,
                                PictureUrl = a.PictureUrl,
                                Parameters = a.Parameters
                                              .Where(p => p.Article.Id == articleId)
                                              .Select(p => new ArticleParameter
                                              {
                                                  Key = p.Key,
                                                  Value = p.Value
                                              })
                                              .ToList(),
                            })
                            .FirstOrDefault();
        }

        public ICollection<ArticleViewModel> GetAll()
        {
            return this.data.Articles
                            .Select(a => new ArticleViewModel
                            {
                                Id = a.Id,
                                Name = a.Name
                            })
                            .ToList();
        }

        public ICollection<ArticleViewModel> GetByCategory(string name)
        {
            return this.data.Articles
                            .Where(a => a.Category.Name == name)
                            .Select(a => new ArticleViewModel
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = Math.Round(a.Price, 2),
                                EcoPrice = a.EcoPrice == null ? null : Math.Round(a.EcoPrice.Value, 2),
                                DiscountPrice = a.DiscountPrice == null ? null : Math.Round(a.DiscountPrice.Value, 2),
                                PictureUrl = a.PictureUrl,
                                Parameters = a.Parameters
                                              .Select(p => new ArticleParameter
                                              {
                                                  Key = p.Key,
                                                  Value = p.Value
                                              })
                                              .ToList()
                            })
                            .ToList();
        }

        public ICollection<ArticleViewModel> GetLast10Added()
        {
            return this.data.Articles
                            .OrderByDescending(a => a.CreatedOn)
                            .Select(a => new ArticleViewModel
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = Math.Round(a.Price, 2),
                                EcoPrice = a.EcoPrice == null ? null : Math.Round(a.EcoPrice.Value, 2),
                                DiscountPrice = a.DiscountPrice == null ? null : Math.Round(a.DiscountPrice.Value, 2),
                                PictureUrl = a.PictureUrl,
                                Parameters = a.Parameters
                                              .Select(p => new ArticleParameter
                                              {
                                                  Key = p.Key,
                                                  Value = p.Value
                                              })
                                              .ToList()
                            })
                            .Take(4)
                            .ToList();
        }

        public ICollection<ArticleViewModel> GetHeadingArticles()
        {
            return this.data.Articles
                            .Where(a => a.IsOnHeadPage == true)
                            .Select(a => new ArticleViewModel
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = Math.Round(a.Price, 2),
                                EcoPrice = a.EcoPrice == null ? null : Math.Round(a.EcoPrice.Value, 2),
                                DiscountPrice = a.DiscountPrice == null ? null : Math.Round(a.DiscountPrice.Value, 2),
                                PictureUrl = a.PictureUrl,
                                Parameters = a.Parameters
                                              .Select(p => new ArticleParameter
                                              {
                                                  Key = p.Key,
                                                  Value = p.Value
                                              })
                                              .ToList()
                            })
                            .ToList();
        }

        public ICollection<ArticleViewModel> GetBySubcategory(string name)
        {
            return this.data.Articles
                            .Where(a => a.Subcategory.Name == name)
                            .Select(a => new ArticleViewModel
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = Math.Round(a.Price, 2),
                                EcoPrice = a.EcoPrice == null ? null : Math.Round(a.EcoPrice.Value, 2),
                                DiscountPrice = a.DiscountPrice == null ? null : Math.Round(a.DiscountPrice.Value, 2),
                                PictureUrl = a.PictureUrl,
                                Parameters = a.Parameters
                                              .Select(p => new ArticleParameter
                                              {
                                                  Key = p.Key,
                                                  Value = p.Value
                                              })
                                              .ToList()
                            })
                            .ToList();
        }

        public bool IsExistById(string articleId)
        {
            return this.data.Articles.Any(a => a.Id == articleId);
        }

        public bool IsExistByName(string name)
        {
            return this.data.Articles.Any(a => a.Name == name);
        }

        public decimal CalculateTotalPrice(ICollection<ArticlePurchaseInputModel> input)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT id, price, discountprice FROM articles WHERE ");
            foreach (var article in input)
            {
                sb.Append(string.Format(@"ID = '{0}'  OR ", article.ArticleId));
            }
            sb.Remove(sb.Length - 4, 4);
            var query = sb.ToString();

            var articles = this.data.Articles.FromSqlRaw(query)
                .Select(x => new ComputeModel
                {
                    Id = x.Id,
                    Price = x.Price,
                    DiscountPrice = x.DiscountPrice
                })
                .ToList();

            decimal totalPrice = 0m;
            foreach (var article in articles)
            {
                if (article.DiscountPrice != null)
                {
                    totalPrice += (input.Where(x => x.ArticleId == article.Id)
                                        .FirstOrDefault().Quantity * article.DiscountPrice.Value);
                    continue;
                }
                totalPrice += (input.Where(x => x.ArticleId == article.Id).FirstOrDefault().Quantity * article.Price);
            }
            return totalPrice;
        }

        public class ComputeModel
        {
            public string Id { get; set; }

            public decimal Price { get; set; }

            public decimal? DiscountPrice { get; set; }
        }
    }
}
