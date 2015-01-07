using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	class ShiftTextBLL
	{
		public static string GetShiftType(string word)
		{
			return ShiftTextDAL.GetShiftType(word);
		}
	}
}
