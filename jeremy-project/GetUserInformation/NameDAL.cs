using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Excel;
using System.Data;

namespace jeremy_project
{
	class NameDAL
	{
		public string WhatIsTheName()
		{
			bool isValid = false;
			string userName = string.Empty;
			while (isValid == false) {
				Console.WriteLine ("Who are you looking for?");
				userName = Console.ReadLine ();
				if (userName.Contains (" ") && userName != null)
					isValid = true;
				else
					Console.WriteLine ("The input is not valid. Please make sure it is a full name, i.e. John Hancock");
			}
			return userName;
		}
	}
}

