namespace EShop.Models.ViewModels.Bag
{
    using System.Collections.Generic;

    public class OrdeViewModel
    {
        public int FinishedCount { get; set; }
        public int ProcessingCount { get; set; }
        public int RejectedCount { get; set; }
        public int ShippedCount { get; set; }

        public ICollection<PurchaseViewModel> Purchases { get; set; }
    }
}
