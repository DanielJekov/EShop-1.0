namespace EShop.Services.User
{
    using System.Linq;

    using EShop.Data;
    using EShop.Models.ServiceModels.Users;

    public class UserService : IUserService
    {
        private readonly EShopDbContext data;

        public UserService(EShopDbContext data)
        {
            this.data = data;
        }

        public int Edit(UserFormModel input)
        {
            var user = this.data.Users
                                .Where(u => u.Id == input.Id)
                                .FirstOrDefault();

            if (!string.IsNullOrEmpty(input.FirstName))
            {
                user.FirstName = input.FirstName;
            }

            if (!string.IsNullOrEmpty(input.LastName))
            {
                user.LastName = input.LastName;
            }

            if (!string.IsNullOrEmpty(input.Address))
            {
                user.Address = input.Address;
            }

            if (!string.IsNullOrEmpty(input.PhoneNumber))
            {
                user.PhoneNumber = input.PhoneNumber;
            }

            return this.data.SaveChanges();
        }

        public UserPurchaseViewModel GetShippingData(string userId)
        {
           return this.data.Users
                           .Where(u => u.Id == userId)
                           .Select(u => new UserPurchaseViewModel
                           {
                               Id = u.Id,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               Address = u.Address,
                               Email = u.Email,
                               PhoneNumber = u.PhoneNumber,
                           })
                           .FirstOrDefault();
        }
    }
}
