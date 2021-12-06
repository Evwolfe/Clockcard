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
    public class DetailsModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public string role = "";

        public EmpDetailsVM EmpDetailsVM { get; set; }

        public DetailsModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }

        public EmpDetails EmpDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmpDetails = await _context.EmpDetails.FirstOrDefaultAsync(m => m.EMPREF == id);

            role = Utils.Enums.getSessionValues("Role", HttpContext);

            //var roleSession = new Byte[20];
            //bool HasRole = HttpContext.Session.TryGetValue("Role", out roleSession);
            //if (HasRole)
            //{
            //    role = System.Text.Encoding.UTF8.GetString(roleSession);
            //}

            EmpDetailsVM = new EmpDetailsVM();

            EmpDetailsVM.EMPREF = EmpDetails.EMPREF;
            EmpDetailsVM.FIRSTNAME = EmpDetails.FIRSTNAME;
            EmpDetailsVM.SURNAME = EmpDetails.SURNAME;
            EmpDetailsVM.PASSWORD = EmpDetails.PASSWORD;
            EmpDetailsVM.PHONE = EmpDetails.PHONE;
            EmpDetailsVM.EMAIL = EmpDetails.EMAIL;
            EmpDetailsVM.USERNAME = EmpDetails.USERNAME;
            EmpDetailsVM.ISACTIVE = (Active)EmpDetails.ISACTIVE;
            EmpDetailsVM.ROLE = (EmployeeRole)EmpDetails.ROLE;

            if (EmpDetails == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
