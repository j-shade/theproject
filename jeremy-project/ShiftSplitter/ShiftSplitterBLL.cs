using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	class ShiftSplitterBLL
	{
		public static void SplitTheShifts(List<Shift> shiftList)
		{
			ShiftSplitterDAL.OneShiftAtATime(shiftList);
		}
	}
}
