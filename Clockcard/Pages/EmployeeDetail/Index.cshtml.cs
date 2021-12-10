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
using static Clockcard.Utils.Enums;

namespace Clockcard.Pages.EmployeeDetails
{
    public class IndexModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public IndexModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }

        public IList<EmpDetails> EmpDetails { get;set; }
        public IList<EmpDetailsVM> EmpDetailsVMList { get; set; }

        public string role = "";

        public async Task<IActionResult> OnGetAsync(Active active)
        {
            EmpDetails = await _context.EmpDetails.ToListAsync();

            role = Utils.Enums.getSessionValues("Role", HttpContext);

            //var roleSession = new Byte[20];
            //bool HasRole = HttpContext.Session.TryGetValue("Role", out roleSession);
            //if (HasRole)
            //{
            //    role = System.Text.Encoding.UTF8.GetString(roleSession);
            //} 

            string empRef =  Utils.Enums.getSessionValues("Empref", HttpContext);
            //var empRefSession = new Byte[20];
            //bool EmprefOK = HttpContext.Session.TryGetValue("Empref", out empRefSession);
            //string empRef = "";
            //if (EmprefOK)
            //{
            //    empRef = System.Text.Encoding.UTF8.GetString(empRefSession);
            //}

            if (role.Equals("1"))
            {
                var filteredData = EmpDetails.Where(q => q.EMPREF.ToString() == empRef);

                EmpDetails = filteredData.ToList();
                string returnUrl = "~/EmployeeDetail/Details?id=" + empRef;
                returnUrl ??= Url.Content(returnUrl);
                return LocalRedirect(returnUrl);

            }
            EmpDetailsVMList = new List<EmpDetailsVM>();
            foreach (var emp in EmpDetails)
            {
                EmpDetailsVMList.Add(new EmpDetailsVM {
                    EMPREF = emp.EMPREF,
                    FIRSTNAME = emp.FIRSTNAME,
                    SURNAME = emp.SURNAME,
                    PASSWORD = emp.PASSWORD,
                    PHONE = emp.PHONE,
                    EMAIL = emp.EMAIL,
                    USERNAME = emp.USERNAME,
                    ISACTIVE = (Active)emp.ISACTIVE,
                    ROLE = (EmployeeRole)emp.ROLE,
                });
            }
            return Page();

        }
    }
}
