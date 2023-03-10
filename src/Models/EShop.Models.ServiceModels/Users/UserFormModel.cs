namespace EShop.Models.ServiceModels.Users
{
    public class UserFormModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //[RegularExpression("/+*[0-9]{5}")]
        public string PhoneNumber { get; set; }

        //[RegularExpression("[A-z]{10}")]
        public string Address { get; set; }
    }
}
