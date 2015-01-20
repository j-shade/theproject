using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
    class UserBLL
    {
		public static void GetUserObjects(Roster roster)
        {
             UserDAL.GetAllUsers(roster);
        }
    }
}
