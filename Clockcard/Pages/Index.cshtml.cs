using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace Clockcard.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        private readonly Clockcard.Data.ClockcardContext _context;
        public IndexModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public class InputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Secured");

            var result = _context.EmpDetails
                .Where(q => q.USERNAME == Input.Username && q.PASSWORD == Input.Password).FirstOrDefault();
            if (result != null)
            {
                                                                                    //Session to hold the employees information
                HttpContext.Session.SetString("Empref", result.EMPREF.ToString());
                HttpContext.Session.SetString("Role", result.ROLE.ToString());
                HttpContext.Session.SetString("HasClockedIn", "False");
                TempData["Empref"] = result.EMPREF;
                TempData["Role"] = result.ROLE;
                TempData["PageLoad"] = "True";
                return LocalRedirect(returnUrl);
            }

            else
            {
                TempData["Error"] = "Error - Username or Password is invalid";
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

        }

    }
}
