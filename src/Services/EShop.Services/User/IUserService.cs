using EShop.Models.ServiceModels.Users;

namespace EShop.Services.User
{
    public interface IUserService
    {
        public int Edit(UserFormModel input);

        public UserPurchaseViewModel GetShippingData(string userId);
    }
}
