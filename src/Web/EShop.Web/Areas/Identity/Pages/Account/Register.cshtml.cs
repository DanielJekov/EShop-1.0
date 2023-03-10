namespace EShop.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using EShop.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;


    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<EShopUser> _signInManager;
        private readonly UserManager<EShopUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        private const string _fieldRequired = "Полето е задължително";

        public RegisterModel(
            UserManager<EShopUser> userManager,
            SignInManager<EShopUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = _fieldRequired)]
            [EmailAddress(ErrorMessage = "Въведете валиден имейл адрес")]
            [Display(Name = "Имейл")]
            public string Email { get; set; }

            [Required(ErrorMessage = _fieldRequired)]
            [MinLength(2, ErrorMessage = "Името трябва да се състои от поне 2 букви.")]
            [MaxLength(20, ErrorMessage = "Максималната дължина за име е 20 букви.")]
            [RegularExpression(@"^\s*[АаБбВвГгДдЕеЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЬьЮюЯя]*\s*$|^\s*[A-z]*\s*$", ErrorMessage = "Полето трябва да съдържа само букви")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = _fieldRequired)]
            [MinLength(2, ErrorMessage = "Името трябва да се състои от поне 2 букви.")]
            [MaxLength(20, ErrorMessage = "Максималната дължина за име е 20 букви.")]
            [RegularExpression(@"^\s*[АаБбВвГгДдЕеЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЬьЮюЯя]*\s*$|^\s*[A-z]*\s*$", ErrorMessage = "Полето трябва да съдържа само букви")]
            public string LastName { get; set; }

            [Required(ErrorMessage = _fieldRequired)]
            [RegularExpression(@"^[0-9]{10}|^\+359[0-9]{9}", ErrorMessage = "Въведете валиден телефонен номер")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = _fieldRequired)]
            public string Address { get; set; }

            [Required(ErrorMessage = _fieldRequired)]
            [MinLength(8, ErrorMessage = "Паролата трябва да е минимум 8 символа.")]
            [MaxLength(20, ErrorMessage = "Паролата трябва да е максимум 20 символа.")]
            [StringLength(100, ErrorMessage = "Паролата трябва да съдържа следните неща: главна буква, малка буква и цифра. Дължината трябва да е между 8-20 символа.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърдете паролата")]
            [Compare("Password", ErrorMessage = "Паролата не съвпада")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new EShopUser { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, Address = Input.Address, PhoneNumber = Input.PhoneNumber };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Потвърдете своя имейл.",
                        $"Моля потвърдете вашия акаунт от следния <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>линк</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
