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
        public IList<Clock> Clock { get;set; }
        public IList<ClockVM> ClockVMList { get; set; }

        public async Task OnGetAsync()
        {
            Clock = await _context.Clock.ToListAsync();

            role = Utils.Enums.getSessionValues("Role", HttpContext);


            string empRef = Utils.Enums.getSessionValues("Empref", HttpContext);
            //var empRefSession = new Byte[20];
            //bool EmprefOK = HttpContext.Session.TryGetValue("Empref", out empRefSession);
            //string empRef = "";
            //if (EmprefOK)
            //{
            //    empRef = System.Text.Encoding.UTF8.GetString(empRefSession);
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
        }
    }
}

