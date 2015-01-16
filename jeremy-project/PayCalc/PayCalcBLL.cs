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
			foreach (ShiftTime shift in shiftList) {
				totalPay += PayCalcDAL.GetPayPerShift (shift);
				fortnightHours += (shift.ShiftEnd - shift.ShiftStart).TotalHours;
			}
			Console.WriteLine ("\n The total pay is ${0} for {1} hours.\n", totalPay, fortnightHours);
		}
	}
}
