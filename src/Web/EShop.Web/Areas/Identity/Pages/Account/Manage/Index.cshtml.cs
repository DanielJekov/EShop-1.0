namespace EShop.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using EShop.Data.Models;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<EShopUser> _userManager;
        private readonly SignInManager<EShopUser> _signInManager;

        private const string _fieldRequired = "Полето е задължително";

        public IndexModel(
            UserManager<EShopUser> userManager,
            SignInManager<EShopUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [RegularExpression(@"^\s*[АаБбВвГгДдЕеЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЬьЮюЯя]*\s*$|^\s*[A-z]*\s*$", ErrorMessage = "Полето трябва да съдържа само букви")]
            public string FirstName { get; set; }

            [RegularExpression(@"^\s*[АаБбВвГгДдЕеЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЬьЮюЯя]*\s*$|^\s*[A-z]*\s*$", ErrorMessage = "Полето трябва да съдържа само букви")]
            public string LastName { get; set; }

            [MaxLength(35)]
            public string Address { get; set; }

            [Display(Name = "Телефонен номер")]
            [RegularExpression(@"^[0-9]{10}|^\+359[0-9]{9}", ErrorMessage = "Въведете валиден телефонен номер")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(EShopUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Потребител с това ИД '{_userManager.GetUserId(User)}' не може да бъде зареден.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Потребител с това ИД '{_userManager.GetUserId(User)}' не може да бъде зареден.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Неочаквана грешка при опита за смяна на телефонен номер.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Промените бяха запаметени";
            return RedirectToPage();
        }
    }
}
