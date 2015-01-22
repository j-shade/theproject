using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	class UserRiteqBLL
	{
		public static void GetUserObjects(Roster roster)
		{
			UserRiteqDAL.GetAllUsers(roster);
		}
	}
}
