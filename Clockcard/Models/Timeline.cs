using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Clockcard.Models
{
    public class Timeline
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Employee No.")]
        public int EMPREF { get; set; }

        [DisplayName("Monday Start")]
        public DateTime MONDAYSTARTTIME{ get; set; }

        [DisplayName("Monday End")]
        public DateTime MONDAYENDTIME { get; set; }

        [DisplayName("Tuesday Start")]
        public DateTime TUESDAYSTARTTIME { get; set; }

        [DisplayName("Tuesday End")]
        public DateTime TUESDAYENDTIME { get; set; }

        [DisplayName("Wednesday Start")]
        public DateTime WEDNESDAYSTARTTIME { get; set; }

        [DisplayName("Wednesday End")]
        public DateTime WEDNESDAYENDTIME { get; set; }

        [DisplayName("Thursday Start")]
        public DateTime THURSDAYSTARTTIME { get; set; }

        [DisplayName("Thursday End")]
        public DateTime THURSDAYENDTIME { get; set; }

        [DisplayName("Friday Start")]
        public DateTime FRIDAYSTARTTIME { get; set; }

        [DisplayName("Friday End")]
        public DateTime FRIDAYENDTIME { get; set; }

        [DisplayName("Saturday Start")]
        public DateTime SATURDAYSTARTTIME { get; set; }

        [DisplayName("Saturday End")]
        public DateTime SATURDAYENDTIME { get; set; }

        [DisplayName("Sunday Start")]
        public DateTime SUNDAYSTARTTIME { get; set; }

        [DisplayName("Sunday End")]
        public DateTime SUNDAYENDTIME { get; set; }

        }
}
