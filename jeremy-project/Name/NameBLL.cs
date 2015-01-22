using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	interface INameDAL {
		string WhatIsTheName ();
	}

	class NameBLL
	{
		//create the DAL
		private readonly INameDAL _dal;

		public NameBLL(INameDAL dal) {
			_dal = dal;
		}

		public string GetUserName()
		{
			return _dal.WhatIsTheName();
		}
	}
}
