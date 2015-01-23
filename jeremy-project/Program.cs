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
			bool haveAsked = false;

            // constants
            const string folderPath = "/Users/jeremy/jeremy-project/jeremy-project/sheet2.xlsx"; 

            try
            {
				//instantiate new roster object
				var roster = new Roster();
				//set the file path for roster object
				roster.filePath = folderPath;
			
                // populate list of users
				var getUsers = new UserBLL(new UserDAL());
				getUsers.GetUserObjects(roster);

				//create name
				userName = GetUserName.GetName();

				//get additional info on the user you want
				foreach (User user in roster.userList)
				{
					bool contains = user.EmployeeName.IndexOf(userName, StringComparison.OrdinalIgnoreCase) >= 0;
					// if one matches happy days
					if (contains == true && haveAsked == false)
					{
						user.EmployeeName = userName;
						GetUserInformation.GetInfo(user);
						roster.currentUser = user;
						haveAsked = true;
					}
				}
					
				// populate list of users for new riteq roster
//				var getUsers = new UserBLL (new UserRiteqDAL ());
//				getUsers.GetUserObjects(roster);

				//create the shift DAL interface
				var shiftFind = new ShiftBLL(new ShiftDAL());

                // trawl through users
				foreach (User user in roster.userList)
                {
					//ignore name case to find variations
					bool contains = user.EmployeeName.IndexOf(userName, StringComparison.OrdinalIgnoreCase) >= 0;
                    // if one matches happy days
					if (contains == true && doesExist == false)
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
