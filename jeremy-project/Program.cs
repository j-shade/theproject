using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;

namespace jeremy_project
{
    class Program
    {
        static void Main(string[] args)
        {
            // vars
            string userName = String.Empty;
			int scrPrt = 0;
			double totalPay = 0.0;
			bool doesExist = false;

            // constants
            const string folderPath = "/Users/jeremy/jeremy-project/jeremy-project/sheet2.xlsx";  

            // could do this better, like you have is fine
			userName = NameBLL.GetUserName ();



            try
            {
                // instantiate list of users
                var users = new List<User>();
				// instantiate list of shifts
				var shiftList = new List<ShiftTime>();
                // populate list of users
                users = UserBLL.GetUserObjects(folderPath);
                // trawl trhough users
                foreach (User user in users)
                {
                    // if one matches happy days
					if (user.EmployeeName == userName && scrPrt < 1)
                    {
						//make the shift list from the userName and folder path
						shiftList = ShiftBLL.GetShiftObjects(userName, folderPath);
						// print shift datetimes, but first get shift objects based on username
						ShiftSplitterBLL.SplitTheShifts(shiftList);
						// find the total pay for each shift
						PayCalcBLL.FindThePay(shiftList);
                        // print shifttimes, but first get shift objects based on username
//                        Print.PrintShiftTimes(ShiftBLL.GetShiftObjects(userName, folderPath)); 
						scrPrt =+ 1;
						doesExist = true;
                    }                    
                }
				if (doesExist == false){
					Console.WriteLine ("This user does not exist in the roster.");
				}
            }

            // generic error handling
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}
