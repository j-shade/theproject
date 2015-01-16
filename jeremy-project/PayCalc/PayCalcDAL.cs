using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Excel;
using System.Data;

namespace jeremy_project
{
	class PayCalcDAL
	{
		public static double GetPayPerShift(ShiftTime shift)
		{
			//set payrate
			const double basePay = 26.23;
			const double satMulti = 1.25;
			const double sunMulti = 1.50;

			//define initial shift pay
			shift.shiftPay = 0.0;

//			Console.WriteLine (shift.ShiftStart);
//			Console.WriteLine (shift.ShiftEnd);
//			Console.WriteLine (shift.shiftType);

			double shiftLength = (shift.ShiftEnd - shift.ShiftStart).TotalHours;

			if (shiftLength != 0.0) {
				switch (shift.shiftDate.DayOfWeek.ToString()) {
				case "Saturday":
					shift.shiftPay = (shiftLength * basePay * satMulti);
					break;
				case "Sunday":
					shift.shiftPay = (shiftLength * basePay * sunMulti);
					break;
				default :
					shift.shiftPay = (shiftLength * basePay);
					break;
				}
				Console.WriteLine ("You will make ${0} for {2} on the {1}"
					, Math.Round(shift.shiftPay,2)
					, shift.shiftDate.ToShortDateString()
					, shift.shiftText);
			}

			return Math.Round(shift.shiftPay, 2);
		}
	}
}

