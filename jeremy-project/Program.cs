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
			bool doesExist = false;

            // constants
            const string folderPath = "/Users/jeremy/jeremy-project/jeremy-project/sheet2.xlsx";  

            // could do this better, like you have is fine
			userName = NameBLL.GetUserName ();

            try
            {
				//instantiate new roster object
				var roster = new Roster();
				//set the file path for roster object
				roster.filePath = folderPath;
                // populate list of users
				UserBLL.GetUserObjects(roster);
				//create the shift DAL interface
				var shiftFind = new ShiftBLL(new ShiftDAL());
                // trawl through users
				foreach (User user in roster.userList)
                {
                    // if one matches happy days
					if (user.EmployeeName == userName && doesExist == false)
                    {
						//make the shift list from the userName and folder path
						shiftFind.GetShiftObjects(userName, roster);
						// for each shift in the roster, separate and make new shift times
						ShiftSplitter.SplitTheShifts(roster.dayList);
						// find the total pay for each shift
						PayCalc.FindThePay(roster);
                      	//Print the shift, date and estimated income
						Print.PrintShiftTimes(roster);
					
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
