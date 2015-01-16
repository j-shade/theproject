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
			const double pubMulti = 2.50;

			//define initial shift pay
			shift.shiftPay = 0.0;

//			Console.WriteLine (shift.ShiftStart);
//			Console.WriteLine (shift.ShiftEnd);
//			Console.WriteLine (shift.shiftType);

			shift.shiftLength = (shift.ShiftEnd - shift.ShiftStart).TotalHours;
			if ((shift.shiftLength >= 5) && (shift.shiftLength <= 7))
				shift.shiftLength = shift.shiftLength - 0.5;

			bool isPublic = FindPublicHoliday (shift);

			if (shift.shiftLength != 0.0) {
				switch (shift.shiftDate.DayOfWeek.ToString()) {
				case "Saturday":
					shift.shiftPay = (shift.shiftLength * basePay * satMulti);
					break;
				case "Sunday":
					shift.shiftPay = (shift.shiftLength * basePay * sunMulti);
					break;
				default :
					shift.shiftPay = (shift.shiftLength * basePay);
					break;
				}

				if (isPublic == true)
					shift.shiftPay = (shift.shiftLength * basePay * pubMulti);
				Console.WriteLine ("You will make ${0} for {1} ({2} hrs) on the {3}"
					,Math.Round(shift.shiftPay,2)
					,shift.shiftText
					,shift.shiftLength
					,shift.shiftDate.ToShortDateString());
			}

			return shift.shiftPay;
		}

		public static bool FindPublicHoliday(ShiftTime shift)
		{
			bool isPublic = false;
			if ((shift.shiftDate.ToShortDateString ().Equals ("01/01/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("26/01/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("02/03/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("03/04/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("06/04/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("25/04/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("26/04/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("01/06/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("28/09/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("25/12/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("26/12/2015")) ||
			    (shift.shiftDate.ToShortDateString ().Equals ("28/12/2015")))
				isPublic = true;
			return isPublic;
		}
	}
}

