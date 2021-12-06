using static Clockcard.Utils.Enums;

namespace Clockcard.ViewModels
{
    // Class to help with the SQL join
    public class EmpDetailsVM
    {
        public int EMPREF { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string FIRSTNAME { get; set; }
        public string SURNAME { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public EmployeeRole ROLE { get; set; }
        public Active ISACTIVE { get; set; }
    }
}
