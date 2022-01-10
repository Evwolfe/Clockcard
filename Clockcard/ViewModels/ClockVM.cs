using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Clockcard.ViewModels
{
    // Class to help with the SQL join
    public class ClockVM
    {
        public int ID { get; set; }
        [DisplayName("Employee No.")]

        public int EMPREF { get; set; }
        [DisplayName("Name")]

        public string FullName { get; set;}
        [DisplayName("Start Time")]

        public DateTime STARTTIME { get; set; }
        [DisplayName("End Time")]

        public DateTime ENDTIME { get; set; }
        [DisplayName("Employee No.")]

        public string Username { get; set; }
    }
}
