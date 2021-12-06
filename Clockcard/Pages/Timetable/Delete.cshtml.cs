using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clockcard.Data;
using Clockcard.Models;

namespace Clockcard.Pages.Timetable
{
    public class DeleteModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public DeleteModel(Clockcard.Data.ClockcardContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Timeline = await _context.Timeline.FindAsync(id);

            if (Timeline != null)
            {
                _context.Timeline.Remove(Timeline);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
