namespace EShop.Models.ServiceModels.Bag
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Models.ServiceModels.Articles;

    public class FinishPurchaseInputModel
    {

        public string Names { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [RegularExpression("^[^<>]+$")]
        public string Description { get; set; }

        [RegularExpression("^[^<>]+$")]
        [MinLength(8)]
        [MaxLength(50)]
        public string AdditionalAddress { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryTax { get; set; }
        = 20.00m;

        public IEnumerable<ArticlePurchaseInputModel> Articles { get; set; }
    }
}
