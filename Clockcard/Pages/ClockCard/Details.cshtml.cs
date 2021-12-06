using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clockcard.Data;
using Clockcard.Models;

namespace Clockcard.Pages.ClockCard
{
    public class DetailsModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public DetailsModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }

        public Clock Clock { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Clock = await _context.Clock.FirstOrDefaultAsync(m => m.ID == id);

            if (Clock == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
