using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	class PayCalcBLL
	{
		public static void FindThePay(List<ShiftTime> shiftList)
		{
			double totalPay = 0.0;
			double fortnightHours = 0.0;
			double shiftLength = 0.0;
			foreach (ShiftTime shift in shiftList) {
				//Calculate the pay
				totalPay += PayCalcDAL.GetPayPerShift (shift);
				//calculate the total hours
				shiftLength = (shift.ShiftEnd - shift.ShiftStart).TotalHours;
				if ((shift.shiftLength >= 5) && (shift.shiftLength <= 7))
					shift.shiftLength = shift.shiftLength - 0.5;
				fortnightHours += shiftLength;
			}
			Console.WriteLine ("\n The total pay is ~ ${0} for {1} hours.\n", Math.Round(totalPay,0), fortnightHours);
		}
	}
}
