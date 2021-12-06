using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clockcard.Data;
using Clockcard.Models;
using Clockcard.Utils;

namespace Clockcard.Pages.Timetable
{
    public class DetailsModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public DetailsModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }

        public string role = "";
        public Timeline Timeline { get; set; }
        public static string converttojsdate(DateTime date)   //Converts C# format to Java Script 
        {
            string datestring = date.Year+"-"+date.Month +"-"+date.Day+"T"+date.Hour+":"+date.Minute+":00";
            return datestring;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Timeline = await _context.Timeline.FirstOrDefaultAsync(m => m.ID == id);

            role = Enums.getSessionValues("Role", HttpContext);
            //var roleSession = new Byte[20];
            //bool HasRole = HttpContext.Session.TryGetValue("Role", out roleSession);
            //if (HasRole)
            //{
            //    role = System.Text.Encoding.UTF8.GetString(roleSession);
            //}

            if (Timeline == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
