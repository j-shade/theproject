using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
    public class Day
    {
		public string dayText{ get; set; }
		public DateTime dayDate{ get; set; }
		private readonly List<ShiftTime> _listOfShifts = new List<ShiftTime>();
		public List<ShiftTime> listOfShifts { get { return _listOfShifts; }}

    }

    public class ShiftTime
    {
		public string singleShiftText{ get; set; }
		public string shiftType { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
		public double shiftLength{get; set;}
		public double shiftPay{ get; set; }
    }
}
