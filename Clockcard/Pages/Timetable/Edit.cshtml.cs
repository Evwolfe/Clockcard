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

namespace Clockcard.Pages.Timetable
{
    public class EditModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public EditModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Timeline Timeline { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Timeline = await _context.Timeline.FirstOrDefaultAsync(m => m.ID == id);

            if (Timeline == null)
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

            _context.Attach(Timeline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimelineExists(Timeline.ID))
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

        private bool TimelineExists(int id)
        {
            return _context.Timeline.Any(e => e.ID == id);
        }
    }
}
