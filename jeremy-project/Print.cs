using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
    class Print
    {
		public static void PrintShiftTimes(Roster roster)
        {
			foreach (Shift day in roster.dayList)
            {
				foreach (ShiftTime shift in day.listOfShifts) {
					Console.WriteLine ("You will make ${0} for {1} ({2} hrs) on {3}, {4}"
						,Math.Round(shift.shiftPay,2)
						,shift.singleShiftText
						,shift.shiftLength
						,shift.ShiftStart.DayOfWeek.ToString()
						,shift.ShiftStart.ToShortDateString());
				}
            }
			Console.WriteLine ("\n    The estimate gross pay is ${0} for {1} hours.\n", Math.Round (roster.rosterPay, 1), roster.rosterHours);
        }
    }
}
