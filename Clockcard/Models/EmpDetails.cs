using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Clockcard.Models
{
    public class EmpDetails
    {
        [Key]
        public int EMPREF { get; set; }

        [DisplayName("Employee No.")]
        public string USERNAME { get; set; }
        
        [DisplayName("Password")]
        public string PASSWORD { get; set; }
        
        [DisplayName("First Name")]
        public string FIRSTNAME { get; set; }

        [DisplayName("Surname")]
        public string SURNAME { get; set; }

        [DisplayName("Phone Number")]
        public string PHONE { get; set; }

        [DisplayName("Email")]
        public string EMAIL { get; set; }

        [DisplayName("Role")]
        public int ROLE { get; set; }

        [DisplayName("Active Employee")]
        public int ISACTIVE { get; set; }
    }
}
