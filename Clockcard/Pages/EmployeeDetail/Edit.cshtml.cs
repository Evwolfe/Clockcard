using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clockcard.Data;
using Clockcard.Models;
using static Clockcard.Utils.Enums;

namespace Clockcard.Pages.EmployeeDetails
{
    public class EditModel : PageModel
    {
        private readonly Clockcard.Data.ClockcardContext _context;

        public EditModel(Clockcard.Data.ClockcardContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EmpDetails EmpDetails { get; set; }
        public Dictionary<int, string> ActiveDict { get; set; }
        public Dictionary<int, string> RoleDict { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ActiveDict = new Dictionary<int, string>();
            ActiveDict.Add((int)Active.No, Active.No.ToString());
            ActiveDict.Add((int)Active.Yes, Active.Yes.ToString());
            RoleDict = new Dictionary<int, string>();
            RoleDict.Add((int)EmployeeRole.Employee, EmployeeRole.Employee.ToString());
            RoleDict.Add((int)EmployeeRole.Manager, EmployeeRole.Manager.ToString());
            RoleDict.Add((int)EmployeeRole.Admin, EmployeeRole.Admin.ToString());

            EmpDetails = await _context.EmpDetails.FirstOrDefaultAsync(m => m.EMPREF == id);
            TempData["Password"] = EmpDetails.PASSWORD;

            if (EmpDetails == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(EmpDetails.PASSWORD == null || EmpDetails.PASSWORD == "")
            {
                EmpDetails.PASSWORD = (string)TempData["Password"];
            }

            _context.Attach(EmpDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpDetailsExists(EmpDetails.EMPREF))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmpDetailsExists(int id)
        {
            return _context.EmpDetails.Any(e => e.EMPREF == id);
        }
    }
}
