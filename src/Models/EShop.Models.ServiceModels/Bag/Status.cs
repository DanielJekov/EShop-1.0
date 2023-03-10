using System.ComponentModel.DataAnnotations;

namespace EShop.Models.ServiceModels.Bag
{
    public enum Status
    {
        [Display(Name = "Обработва се")]
        Process = 1,

        [Display(Name = "Изпратена")]
        Sended = 2,

        [Display(Name = "Приключена")]
        Finished = 3,

        [Display(Name = "Отказана")]
        Rejected = 4,
    }
}
