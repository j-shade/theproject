using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
    class ShiftBLL
    {
		public static void GetShiftObjects(string user, Roster roster)
        {
            ShiftDAL.GetShiftObjectsForUser(user, roster);
        }
    }
}
