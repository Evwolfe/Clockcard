using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Clockcard.Data;
using Clockcard.Models;
using static Clockcard.Utils.Enums;

namespace Clockcard.Pages.EmployeeDetails
{
    public class CreateModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public Dictionary<int, string> ActiveDict { get; set; }
        public Dictionary<int, string> RoleDict { get; set; }

        public CreateModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ActiveDict = new Dictionary<int, string>();
            ActiveDict.Add((int)Active.No, Active.No.ToString());
            ActiveDict.Add((int)Active.Yes, Active.Yes.ToString());
            RoleDict = new Dictionary<int, string>();
            RoleDict.Add((int)EmployeeRole.Employee, EmployeeRole.Employee.ToString());
            RoleDict.Add((int)EmployeeRole.Manager, EmployeeRole.Manager.ToString());
            RoleDict.Add((int)EmployeeRole.Admin, EmployeeRole.Admin.ToString());
            return Page();
        }

        [BindProperty]
        public EmpDetails EmpDetails { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.EmpDetails.Add(EmpDetails);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
