using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	interface IShiftDAL {
		void GetShiftObjectsForUser (string user, Roster roster);
	}

    class ShiftBLL
    {
		//create the DAL
		private readonly IShiftDAL _dal;

		public ShiftBLL(IShiftDAL dal) {
			_dal = dal;
		}

		public void GetShiftObjects(string user, Roster roster)
        {
			_dal.GetShiftObjectsForUser (user, roster);
        }
    }
}
