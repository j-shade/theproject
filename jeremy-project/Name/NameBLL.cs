using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	class NameBLL
	{
		public static string GetUserName()
		{
			return NameDAL.WhatIsTheName();
		}
	}
}
