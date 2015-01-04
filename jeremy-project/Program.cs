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

            // constants
            const string folderPath = "/Users/jeremy/jeremy-project/jeremy-project/sheet.xlsx";            

            // could do this better, like you have is fine
			userName = NameBLL.GetUserName ();

            try
            {
                // instantiate list of users
                var users = new List<User>();
                // populate list of users
                users = UserBLL.GetUserObjects(folderPath);
                // trawl trhough users
                foreach (User user in users)
                {
                    // if one matches happy days
					if (user.EmployeeName == userName && scrPrt < 1)
                    {
						//ShiftBLL.GetShiftObjects(userName, folderPath);
                        // print shifttimes, but first get shift objects based on username
                        Print.PrintShiftTimes(ShiftBLL.GetShiftObjects(userName, folderPath)); 
						scrPrt =+ 1;
                    }                    
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
