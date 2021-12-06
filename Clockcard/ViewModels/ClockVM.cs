using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clockcard.ViewModels
{
    // Class to help with the SQL join
    public class ClockVM
    {
        public int ID { get; set; }
        public int EMPREF { get; set; }
        public string FullName { get; set;}
        public DateTime STARTTIME { get; set; }
        public DateTime ENDTIME { get; set; }

        public string Username { get; set; }
    }
}
