using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clockcard.Data;
using Clockcard.Models;

namespace Clockcard.Pages.ClockCard
{
    public class EditModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public EditModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Clock Clock { get; set; }

        public Dictionary<int, string> EmployeesDict { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Clock = await _context.Clock.FirstOrDefaultAsync(m => m.ID == id);

            var data = _context.EmpDetails.ToListAsync().Result;
            EmployeesDict = new Dictionary<int, string>();
            foreach (var item in data)
            {
                    EmployeesDict.Add(item.EMPREF, item.FIRSTNAME + " " + item.SURNAME);

            }


            if (Clock == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Clock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClockExists(Clock.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClockExists(int id)
        {
            return _context.Clock.Any(e => e.ID == id);
        }
    }
}
