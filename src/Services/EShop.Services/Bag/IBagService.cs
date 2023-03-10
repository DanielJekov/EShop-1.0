namespace EShop.Services.Bag
{
    using System.Collections.Generic;

    using EShop.Models.ServiceModels.Bag;
    using EShop.Models.ViewModels;
    using EShop.Models.ViewModels.Bag;

    public interface IBagService
    {
        public void Add(string articleId, string userId);

        public void Remove(string articleId, string userId);

        public ICollection<ArticleViewModel> GetArticlesFromBag(string userId);

        public bool IsAlreadyAdded(string articleId, string userId);

        public bool QuickPurchase(QuickPurchaseInputModel input);

        public bool Purchase(FinishPurchaseInputModel input, string userId);

        public void ClearBag(string userId);

        public OrdeViewModel Purchases();

        public void ProcessPurchase(StatusFormModel input);
    }
}
