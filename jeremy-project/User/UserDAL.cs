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
    static class UserDAL
    {
		public static void GetAllUsers(Roster roster)
        {
            User user = new User();

            // path to excel, read in all the users to a user object
			FileStream stream = File.Open(roster.filePath, FileMode.Open, FileAccess.Read);

            // read in file
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            while (excelReader.Read())
            {
                string name = excelReader.GetString(0);
				if (name == null) {
					name = String.Empty;
				}
                // really shitty validation - definitley fix this up
				if ((name.Length > 5) && (name.Length < 20) && (name.Contains("0") != true))
                {
                    // add users to list
					roster.userList.Add(new User { EmployeeName = excelReader.GetString(0) });
                }
            }
            // m management
			excelReader.Close ();
        }
    }
}
