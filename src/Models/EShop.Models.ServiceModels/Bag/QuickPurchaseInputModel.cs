namespace EShop.Models.ServiceModels.Bag
{
    using System.ComponentModel.DataAnnotations;

    public class QuickPurchaseInputModel
    {
        [Required(ErrorMessage ="Полето е задължително")]
        [MinLength(4, ErrorMessage = "Моля въведете две имена")]
        public string Names { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [RegularExpression(@"^[0-9]{10}|^\+359[0-9]{9}", ErrorMessage = "Въведете валиден телефонен номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [MaxLength(450)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [MaxLength(450)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [EmailAddress(ErrorMessage = "Въведете валиден имейл адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string ArticleId { get; set; }
    }
}
