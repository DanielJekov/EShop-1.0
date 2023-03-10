namespace EShop.Web.Areas.Administrator.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        public string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public RedirectResult RedirectToPreviousPage()
        {
            string prevPage = this.Request.Headers["Referer"].ToString();
            return this.Redirect(prevPage);
        }

        public string ModelStateErrorCollector()
        {
            var sb = new StringBuilder();
            var modelsErrors = this.ModelState.Where(e => e.Value.Errors.Count > 0)
                                              .Select(e => e.Value.Errors.Select(s => s.ErrorMessage)
                                              .FirstOrDefault())
                                              .ToList();

            sb.AppendLine(string.Join(" ", modelsErrors));

            return sb.ToString();
        }
    }
}
