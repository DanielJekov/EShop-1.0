namespace EShop.Models.ServiceModels.Bag
{
    using System.ComponentModel.DataAnnotations;

    public class StatusFormModel
    {
        public Status Status { get; set; }

        [MaxLength(450)]
        public string Description { get; set; }

        public int PurchaseId { get; set; }
    }
}
