using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Clockcard.Models
{
    public class Clock
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Employee No.")]
        public int EMPREF { get; set; }
        
        [DisplayName("Clocked In")]
        
        public DateTime STARTTIME { get; set; }
        
        [DisplayName("Clocked Out")]
        public DateTime ENDTIME{ get; set; }
    }
}
