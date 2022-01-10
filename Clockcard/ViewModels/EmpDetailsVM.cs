using System.ComponentModel;
using static Clockcard.Utils.Enums;

namespace Clockcard.ViewModels
{
    // Class to help with the SQL join
    public class EmpDetailsVM
    {
        
        public int EMPREF { get; set; }
        [DisplayName("Employee No.")]
        public string USERNAME { get; set; }
        [DisplayName("Password")]
        public string PASSWORD { get; set; }
        [DisplayName("First Name")]
        public string FIRSTNAME { get; set; }
        [DisplayName("Surname")]
        public string SURNAME { get; set; }
        [DisplayName("Phone No.")]
        public string PHONE { get; set; }
        [DisplayName("Email")]
        public string EMAIL { get; set; }
        [DisplayName("Employee Type")]
        public EmployeeRole ROLE { get; set; }
        [DisplayName("Active")]
        public Active ISACTIVE { get; set; }
    }
}
