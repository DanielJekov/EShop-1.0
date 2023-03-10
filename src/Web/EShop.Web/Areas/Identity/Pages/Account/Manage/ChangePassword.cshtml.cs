namespace EShop.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    using EShop.Data.Models;

    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<EShopUser> _userManager;
        private readonly SignInManager<EShopUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        private const string _fieldRequired = "Полето е задължително";

        public ChangePasswordModel(
            UserManager<EShopUser> userManager,
            SignInManager<EShopUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage ="Полето е задължително")]
            [DataType(DataType.Password)]
            [Display(Name = "Текуща парола")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = _fieldRequired)]
            [MinLength(8, ErrorMessage = "Паролата трябва да е минимум 8 символа.")]
            [MaxLength(20, ErrorMessage = "Паролата трябва да е максимум 20 символа.")]
            [StringLength(100, ErrorMessage = "Паролата трябва да съдържа следните неща: главна буква, малка буква и цифра. Дължината трябва да е между 8-20 символа.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Нова парола")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърдете паролата")]
            [Compare("NewPassword", ErrorMessage = "Паролите не съвпадат")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Вашата парола беше променена.";

            return RedirectToPage();
        }
    }
}
