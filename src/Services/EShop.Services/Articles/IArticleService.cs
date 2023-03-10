namespace EShop.Services.Articles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EShop.Models.ServiceModels.Articles;
    using EShop.Models.ViewModels;

    public interface IArticleService
    {
        public Task Create(ArticleFormModel input);

        public Task<int> Update(ArticleFormModel input);

        public void Delete(string id);

        public ICollection<ArticleViewModel> GetAll();

        public ICollection<ArticleViewModel> GetBySubcategory(string name);

        public ICollection<ArticleViewModel> GetByCategory(string name);

        public ICollection<ArticleViewModel> GetLast10Added();

        public ICollection<ArticleViewModel> GetHeadingArticles();

        public ArticleFormModel GetById(string articleId);

        public ArticleViewModel Get(string articleId);

        public bool IsExistById(string articleId);

        public bool IsExistByName(string name);

        public decimal CalculateTotalPrice(ICollection<ArticlePurchaseInputModel> input);
    }
}
