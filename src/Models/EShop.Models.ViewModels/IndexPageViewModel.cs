namespace EShop.Models.ViewModels.Articles
{
    using System.Collections.Generic;
    public class IndexPageViewModel
    {
        public ICollection<ArticleViewModel> Newer10 { get; set; }

        public ICollection<ArticleViewModel> OnFocus { get; set; }
    }
}
