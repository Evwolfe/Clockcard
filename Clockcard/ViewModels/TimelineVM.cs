using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clockcard.ViewModels
{
    // Class to help with the SQL join
    public class TimelineVM
    {
        public int ID { get; set; }
        public int EMPREF { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

    }
}
