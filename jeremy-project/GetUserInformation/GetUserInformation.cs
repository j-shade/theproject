using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	class GetUserInformation
	{
		public static void GetInfo(User user)
		{
			bool levelIsValid = false;
			bool ageIsValid = false;
			int userLevel = 1;
			int userAge = 0;

			while (levelIsValid == false) {
				Console.WriteLine ("What is the level of the employee?");
				userLevel = Convert.ToInt16 (Console.ReadLine ());
				if (userLevel >= 1 && userLevel <= 6) {
					user.EmployeeLevel = userLevel;
					levelIsValid = true;
				} else
					Console.WriteLine ("The level is not valid. It must be between 1 and 6 inclusive.");
			}

			while (ageIsValid == false){
				Console.WriteLine ("What is the age of the employee?");
				userAge = Convert.ToInt16 (Console.ReadLine ());
				if (userAge >= 14 && userAge <= 60) {
					user.EmployeeAge = userAge;
					ageIsValid = true;
				} else
					Console.WriteLine("Please be realistic...");
			}
		}
	}
}
