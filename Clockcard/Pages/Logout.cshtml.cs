using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Clockcard.Pages
{
    public class LogoutModel : PageModel
    {
        public string role = "";
        public void OnGet()
        {
            var roleSession = new Byte[20];
            bool HasRole = HttpContext.Session.TryGetValue("Role", out roleSession);
            if (HasRole)
            {
                role = System.Text.Encoding.UTF8.GetString(roleSession);
            }
        }

        public async Task<IActionResult> OnPost(string returnUrl = null) // Clears the session information for the Logout 
        {
            returnUrl ??= Url.Content("~/Index");
            HttpContext.Session.SetString("Empref", "");
            HttpContext.Session.SetString("Role", "");
            HttpContext.Session.SetString("HasClockedIn", "");
            HttpContext.Session.Clear();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
