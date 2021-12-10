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
using Clockcard.Utils;

namespace Clockcard.Pages.Timetable
{
    public class IndexModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public IndexModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }
        public string role = "";
        public IList<Timeline> Timeline { get; set; }
        public IList<TimelineVM> TimelineVMList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            string empRef = Enums.getSessionValues("Empref", HttpContext);

            role = Enums.getSessionValues("Role", HttpContext);

            Timeline = await _context.Timeline.ToListAsync();


            //var roleSession = new Byte[20];
            //bool HasRole = HttpContext.Session.TryGetValue("Role", out roleSession);
            //if (HasRole)
            //{
            //    role = System.Text.Encoding.UTF8.GetString(roleSession);
            //}

            //var empRefSession = new Byte[20];
            //bool EmprefOK = HttpContext.Session.TryGetValue("Empref", out empRefSession);
            //string empRef = "";
            //if (EmprefOK)
            //{
            //    empRef = System.Text.Encoding.UTF8.GetString(empRefSession);
            //}

            var data = _context.Timeline
            .Join(_context.EmpDetails,
                  p => p.EMPREF,
                  e => e.EMPREF,
                  (p, e) => new
                  {
                      ID = p.ID,
                      Empref = p.EMPREF,
                      FirstName = e.FIRSTNAME,
                      LastName = e.SURNAME,
                      Username = e.USERNAME,
                      Role = e.ROLE

                  }
                  ).ToListAsync();

            if (role.Equals("1"))
            {
                var filteredData = data.Result.Where(q => q.Empref.ToString() == empRef).ToList();


                TimelineVMList = new List<TimelineVM>();
                foreach (var item in filteredData)
                {
                    if (item != null)
                    {
                        TimelineVMList.Add(new TimelineVM()
                        {
                            ID = item.ID,
                            EMPREF = item.Empref,
                            Name = item.FirstName + " " + item.LastName,
                            Username = item.Username,
                        });

                    }
                }

                int Timetableid = TimelineVMList[0].ID;
                string returnUrl = "~/Timetable/Details?id=" + Timetableid;
                returnUrl ??= Url.Content(returnUrl);
                return LocalRedirect(returnUrl);


            }
            else
            {
                TimelineVMList = new List<TimelineVM>();
                foreach (var item in data.Result)
                {
                    if (item != null)
                    {
                        TimelineVMList.Add(new TimelineVM()
                        {
                            ID = item.ID,
                            EMPREF = item.Empref,
                            Name = item.FirstName + " " + item.LastName,
                            Username = item.Username,
                        });

                    }
                }
            }
            return Page();

        }

    }
}
