using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	class PayCalcBLL
	{
		public static void FindThePay(Roster roster)
		{
			foreach (Shift shift in roster.dayList) {
				foreach (ShiftTime workingShift in shift.listOfShifts) {
					//Calculate the pay
					roster.rosterPay += PayCalcDAL.GetPayPerShift (workingShift);
					//calculate the total hours
					roster.rosterHours += workingShift.shiftLength;
				}
			}
		}
	}
}
