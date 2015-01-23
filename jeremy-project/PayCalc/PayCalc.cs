using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	class PayCalc
	{
		public static void FindThePay(Roster roster)
		{
			foreach (Day shift in roster.dayList) {
				foreach (ShiftTime workingShift in shift.listOfShifts) {
					//Calculate the pay
					roster.rosterPay += GetPayPerShift (workingShift, roster);
					//calculate the total hours
					roster.rosterHours += workingShift.shiftLength;
				}
			}
		}

		private static double GetPayPerShift(ShiftTime shift, Roster roster)
		{
			//set constants and variables
			const double satMulti = 1.25;
			const double sunMulti = 1.50;
			const double pubMulti = 2.50;
			double basePay = 23.92;
			double ageMulti = 1.00;

			switch (roster.currentUser.EmployeeLevel) {
			case 2:
				basePay = 25.12;
				break;
			case 3:
				basePay = 26.23;
				break;
			case 4:
				basePay = 28.21;
				break;
			case 5:
				basePay = 28.61;
				break;
			case 6:
				basePay = 30.39;
				break;
			}

			//account for new pay upgrades in July, 2015.
			DateTime newPay = new DateTime (2015, 7,1);
			if (shift.ShiftStart.Date > newPay.Date) {
				basePay = 25.12;
				switch (roster.currentUser.EmployeeLevel) {
				case 2:
					basePay = 26.38;
					break;
				case 3:
					basePay = 27.54;
					break;
				case 4:
					basePay = 29.62;
					break;
				case 5:
					basePay = 30.04;
					break;
				case 6:
					basePay = 31.91;
					break;
				
				}
			}
				
			//calculate age multiplier for pay
			int tempAge = roster.currentUser.EmployeeAge;
			if (tempAge < 16)
				ageMulti = 0.55;
			if (tempAge == 16)
				ageMulti = 0.65;
			if (tempAge == 17)
				ageMulti = 0.75;
			if (tempAge == 18)
				ageMulti = 0.85;
			if (tempAge == 19)
				ageMulti = 0.90;

			//define initial shift pay
			shift.shiftPay = 0.0;

			//shorten shift length for half hour break
			shift.shiftLength = (shift.ShiftEnd - shift.ShiftStart).TotalHours;
			if ((shift.shiftLength >= 5) && (shift.shiftLength <= 7))
				shift.shiftLength = shift.shiftLength - 0.5;

			//shorten shift length for early close on weekends
			if ((shift.shiftLength == 4) && (shift.ShiftStart.Hour != 11) && (shift.ShiftStart.Hour != 7)) {
				shift.shiftLength = 3.5;
			}

			bool isPublic = FindPublicHoliday (shift);

			if (shift.shiftLength != 0.0) {
				switch (shift.ShiftStart.DayOfWeek.ToString()) {
				case "Saturday":
					shift.shiftPay = (shift.shiftLength * ageMulti * basePay * satMulti);
					break;
				case "Sunday":
					shift.shiftPay = (shift.shiftLength * ageMulti * basePay * sunMulti);
					break;
				default :
					shift.shiftPay = (shift.shiftLength * ageMulti * basePay);
					break;
				}

				//check if the day is a public holiday 
				if (isPublic == true)
					shift.shiftPay = (shift.shiftLength * ageMulti * basePay * pubMulti);
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
