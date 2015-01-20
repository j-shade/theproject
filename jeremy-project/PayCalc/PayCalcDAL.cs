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

			shift.shiftLength = (shift.ShiftEnd - shift.ShiftStart).TotalHours;
			if ((shift.shiftLength >= 5) && (shift.shiftLength <= 7))
				shift.shiftLength = shift.shiftLength - 0.5;

			bool isPublic = FindPublicHoliday (shift);

			if (shift.shiftLength != 0.0) {
				switch (shift.ShiftStart.ToShortDateString()) {
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

				//check if the day is a public holiday 
				if (isPublic == true)
					shift.shiftPay = (shift.shiftLength * basePay * pubMulti);
			}

			return shift.shiftPay;
		}

		public static bool FindPublicHoliday(ShiftTime shift)
		{
			bool isPublic = false;
			if ((shift.ShiftStart.ToShortDateString ().Equals ("01/01/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("26/01/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("02/03/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("03/04/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("06/04/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("25/04/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("26/04/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("01/06/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("28/09/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("25/12/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("26/12/2015")) ||
			    (shift.ShiftStart.ToShortDateString ().Equals ("28/12/2015")))
				isPublic = true;
			return isPublic;
		}
	}
}

