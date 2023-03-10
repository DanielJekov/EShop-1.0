namespace EShop.Models.ViewModels.Bag
{
    using System;
    using System.Collections.Generic;
    using EShop.Models.ServiceModels.Bag;
    using EShop.Models.ViewModels.Articles;

    public class PurchaseViewModel
    {
        public string  Description { get; set; }

        public string ExternalAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Names { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal TotalPrice { get; set; }

        public StatusFormModel StatusForm { get; set; }

        public ICollection<ArticleShortDataViewModel> Articles { get; set; }
    }
}
