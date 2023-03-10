namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CompanyDetails
    {
        public int Id { get; set; }

        [Required]
        public string Contacts { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string About { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Privacy { get; set; }
    }
}
