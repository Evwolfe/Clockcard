using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clockcard.Data;
using Clockcard.Models;
using Clockcard.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Clockcard.Pages.ClockCard
{
    public class IndexModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public IndexModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }
        public string role = "";

        public string hasClockedIn { get; set; }

        [BindProperty]
        public int? selectedEmployee { get; set; }
        public SelectList EmployeesSelectList { get; set; }
        public Dictionary<int, string> EmployeesDict { get; set; }
        public IList<Clock> ClockList { get;set; }
        public IList<ClockVM> ClockVMList { get; set; }

        [BindProperty]
        public Clock Clock { get; set; }

        public async Task OnGetAsync()
        {
            ClockList = await _context.Clock.ToListAsync();

            role = Utils.Enums.getSessionValues("Role", HttpContext);

            hasClockedIn = Utils.Enums.getSessionValues("HasClockedIn", HttpContext);
            string empRef = Utils.Enums.getSessionValues("Empref", HttpContext);

            //if( selectedEmployee == null)
            //{
            //    selectedEmployee =Int32.Parse(empRef);
            //}

            var data = _context.Clock
            .Join(_context.EmpDetails,
                  p => p.EMPREF,
                  e => e.EMPREF,
                  (p, e) => new {
                      ID = p.ID,
                      Empref =  p.EMPREF,
                      FirstName = e.FIRSTNAME,
                      LastName = e.SURNAME,
                      StartTime = p.STARTTIME,
                      EndTime = p.ENDTIME,
                      Username = e.USERNAME
                  }
                  ).Where(q => q.Empref.ToString() == empRef).ToListAsync();
            data.Result.Reverse();
            ClockVMList = new List<ClockVM>();
            foreach ( var item in data.Result)
            {
                if(item!=null)
                {
                    ClockVMList.Add(new ClockVM()
                    {
                        ID = item.ID,
                        EMPREF = item.Empref,
                        FullName = item.FirstName +" "+item.LastName,
                        STARTTIME = item.StartTime,
                        ENDTIME = item.EndTime,
                        Username = item.Username
                    }) ;

                }
            }
            //IList<ClockVM> TempList = new List<ClockVM>();
            //for(int i = 0; i < 10; i++)
            //{
            //    TempList.Add(ClockVMList[i]);
            //}
            //ClockVMList = TempList;
            var Employeedata = _context.EmpDetails.ToListAsync().Result;
            EmployeesDict = new Dictionary<int, string>();
            foreach (var item in Employeedata)
            {
                EmployeesDict.Add(item.EMPREF, item.FIRSTNAME + " " + item.SURNAME);
            }
            EmployeesSelectList = new SelectList(EmployeesDict, "Key", "Value");
        }

        public IActionResult OnPostFilterTable()
        {
            role = Utils.Enums.getSessionValues("Role", HttpContext);
            hasClockedIn = Utils.Enums.getSessionValues("HasClockedIn", HttpContext);

            string empRef = Utils.Enums.getSessionValues("Empref", HttpContext);
            //var yunus = param;
            var data = _context.Clock
            .Join(_context.EmpDetails,
                  p => p.EMPREF,
                  e => e.EMPREF,
                  (p, e) => new {
                      ID = p.ID,
                      Empref = p.EMPREF,
                      FirstName = e.FIRSTNAME,
                      LastName = e.SURNAME,
                      StartTime = p.STARTTIME,
                      EndTime = p.ENDTIME,
                      Username = e.USERNAME
                  }
                  ).Where(q => q.Empref == selectedEmployee).ToListAsync();
            data.Result.Reverse();
            ClockVMList = new List<ClockVM>();
            foreach (var item in data.Result)
            {
                if (item != null)
                {
                    ClockVMList.Add(new ClockVM()
                    {
                        ID = item.ID,
                        EMPREF = item.Empref,
                        FullName = item.FirstName + " " + item.LastName,
                        STARTTIME = item.StartTime,
                        ENDTIME = item.EndTime,
                        Username = item.Username
                    });

                }
            }

            //IList<ClockVM> TempList = new List<ClockVM>();
            //for (int i = 0; i < 10; i++)
            //{
            //    TempList.Add(ClockVMList[i]);
            //}
            //ClockVMList = TempList;

            var Employeedata = _context.EmpDetails.ToListAsync().Result;
            EmployeesDict = new Dictionary<int, string>();
            foreach (var item in Employeedata)
            {
                EmployeesDict.Add(item.EMPREF, item.FIRSTNAME + " " + item.SURNAME);
            }
            EmployeesSelectList = new SelectList(EmployeesDict, "Key", "Value");
            return Page();
        }

        public async Task<IActionResult> OnPostCreateClock()
        {
            hasClockedIn = Utils.Enums.getSessionValues("HasClockedIn", HttpContext);

            int empRef = Int32.Parse(Utils.Enums.getSessionValues("Empref", HttpContext));
            // if inserting -- clock in
            if (hasClockedIn == "False")
            {
                HttpContext.Session.SetString("HasClockedIn", "True");
                Clock clock = new Clock();
                clock.EMPREF = empRef;
                clock.STARTTIME = DateTime.Now;
                clock.ENDTIME = DateTime.Now;
                _context.Clock.Add(clock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");

        }
        public async Task<IActionResult> OnPostUpdateClock()
        {
            hasClockedIn = Utils.Enums.getSessionValues("HasClockedIn", HttpContext);

            int empRef = Int32.Parse(Utils.Enums.getSessionValues("Empref", HttpContext));
            // if inserting -- clock in
            if (hasClockedIn == "True")
            {
                HttpContext.Session.SetString("HasClockedIn", "False");
                Clock = await _context.Clock.OrderBy(x => x.ID).LastOrDefaultAsync(m => m.EMPREF == empRef);
                Clock.ENDTIME = DateTime.Now;

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
            }

            return RedirectToPage("./Index");

        }
        private bool ClockExists(int id)
        {
            return _context.Clock.Any(e => e.ID == id);
        }
    }
}

