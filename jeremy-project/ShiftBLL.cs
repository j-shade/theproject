using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
    class ShiftBLL
    {
        public static Shift GetShiftObject(string user, string filePath)
        {
            return ShiftDAL.GetShiftObjectForUser(user, filePath);
        }
    }
}
