namespace EShop.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Data.Models;

    public class CompanyDetailsSeeder : ISeeder
    {
        public async Task SeedAsync(EShopDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CompanyDetails.Any())
            {
                return;
            }

            var details = new CompanyDetails() {
                Contacts = "Test",
                PhoneNumber = "+3598XXXXXXXXX",
                Privacy = "<p>Use this page to detail your site's privacy policy.</p>",
                About = "Test",
                Location = "<iframe src=\"https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2908.0068056643795!2d27.895076314821342!3d43.20934658903699!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x40a45353bf3bae45%3A0xae236ae956fab99c!2z0K_QndCY0JzQntCdIDIwMTgg0JXQntCe0JQ!5e0!3m2!1sen!2sbg!4v1635011567537!5m2!1sen!2sbg\" width=\"600\" height=\"450\" style=\"border:0;\" allowfullscreen=\"\" loading=\"lazy\"></iframe>",
            };

            await dbContext.CompanyDetails.AddAsync(details);
            await dbContext.SaveChangesAsync();
        }
    }
}
