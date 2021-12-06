using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Clockcard.Data;
using Clockcard.Models;

namespace Clockcard.Pages.Timetable
{
    public class CreateModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;
        public Dictionary<int, string> EmployeesDict { get; set; }
        public CreateModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var data = _context.EmpDetails.ToList();
            EmployeesDict = new Dictionary<int, string>();
            foreach (var item in data)
            {
                EmployeesDict.Add(item.EMPREF, item.FIRSTNAME + " " + item.SURNAME);

            }
            return Page();
        }

        [BindProperty]
        public Timeline Timeline { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Timeline.Add(Timeline);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
