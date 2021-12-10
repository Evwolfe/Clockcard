using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Clockcard.Data;
using Clockcard.Models;
using System.Web.Mvc;
using Clockcard.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Clockcard.Pages.ClockCard
{
    public class CreateModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;


        public Dictionary<int, string> EmployeesDict { get; set; }

        public string hasClockedIn { get; set; }


        public CreateModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {

            string role = Utils.Enums.getSessionValues("Role", HttpContext);
            hasClockedIn = Utils.Enums.getSessionValues("HasClockedIn", HttpContext);
            int empRef = Int32.Parse(Utils.Enums.getSessionValues("Empref", HttpContext));

            var data = _context.EmpDetails.ToListAsync().Result;
            EmployeesDict = new Dictionary<int, string>();
            foreach (var item in data)
            {
                if (role.Equals("1"))
                {
                    if (item.EMPREF == empRef)
                    {
                        EmployeesDict.Add(item.EMPREF, item.FIRSTNAME + " " + item.SURNAME);
                    }
                }
                else
                {
                    EmployeesDict.Add(item.EMPREF, item.FIRSTNAME + " " + item.SURNAME);
                }

            }
            return Page();
        }

        [BindProperty]
        public Clock Clock { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Clock.Add(Clock);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
