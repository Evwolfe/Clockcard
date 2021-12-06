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

namespace Clockcard.Pages.ClockCard
{
    public class CreateModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;


        public Dictionary<int,string> EmployeesDict { get; set; }


        public CreateModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {

            string role = Utils.Enums.getSessionValues("Role", HttpContext);
            //var roleSession = new Byte[20];
            //bool HasRole = HttpContext.Session.TryGetValue("Role", out roleSession);
            //string role = "";
            //if (HasRole)
            //{
            //    role = System.Text.Encoding.UTF8.GetString(roleSession);
            //}
            int empRef = Int32.Parse( Utils.Enums.getSessionValues("Empref", HttpContext));
            //var empRefSession = new Byte[20];
            //bool EmprefOK = HttpContext.Session.TryGetValue("Empref", out empRefSession);
            //int empRef = 0;
            //if (EmprefOK)
            //{
            //    empRef = Int32.Parse(System.Text.Encoding.UTF8.GetString(empRefSession));
            //}

            var data =  _context.EmpDetails.ToListAsync().Result;
            EmployeesDict = new Dictionary<int, string>();
            foreach (var item in data)
            {
                if (role.Equals("1")){
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
        [ChildActionOnly]
        public async Task<IActionResult> HandleClock()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var ClockSession = new Byte[20];
            bool HasClockInSession = HttpContext.Session.TryGetValue("HasClockedIn", out ClockSession);
            string HasClockedIn =  System.Text.Encoding.UTF8.GetString(ClockSession);

            var EmprefSession = new Byte[20];
            bool HasEmpref = HttpContext.Session.TryGetValue("Empref", out EmprefSession);
            int Empref = Int32.Parse(System.Text.Encoding.UTF8.GetString(EmprefSession));


            // if inserting -- clock in
            if (HasClockedIn == "False")
            {
                string isPageLoad = TempData["PageLoad"] as string;
                if (isPageLoad=="False")
                {
                    Clock clock = new Clock();
                    clock.EMPREF = Empref;
                    clock.STARTTIME = DateTime.Now;
                    _context.Clock.Add(clock);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                isPageLoad = "False";
                return Page();
            }
            else
            {
                return Page();
            }


        }
    }
}
