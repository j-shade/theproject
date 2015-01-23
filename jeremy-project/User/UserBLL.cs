using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	interface IUserDAL {
		void GetAllUsers (Roster roster);
	}

    class UserBLL
    {
		//create the DAL
		private readonly IUserDAL _dal;

		public UserBLL(IUserDAL dal) {
			_dal = dal;
		}

		public void GetUserObjects(Roster roster)
        {
             _dal.GetAllUsers(roster);
        }
    }
}
