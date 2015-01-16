using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
    public class Shift
    {
		public string shiftText{ get; set; }
		public DateTime shiftDate{ get; set; }
		public double shiftPay{ get; set; }

//        public string ShiftTotal { get; set; }
//        public string[] Days = { "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
//        public string Day1 { get; set; }
//        public string Day2 { get; set; }
//        public string Day3 { get; set; }
//        public string Day4 { get; set; }
//        public string Day5 { get; set; }
//        public string Day6 { get; set; }
//        public string Day7 { get; set; }

    }

    public class ShiftTime : Shift
    {
		public string shiftType { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
    }
}
