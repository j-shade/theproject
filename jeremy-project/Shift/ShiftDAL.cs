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
    class ShiftDAL
    {
        public static List<ShiftTime> GetShiftObjectsForUser(string user, string filePath)
        {
            List<ShiftTime> shifts = new List<ShiftTime>();
            
            // path to excel, read in all the users to a user object
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            // read in file
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            while (excelReader.Read())
            {
				if (excelReader.GetString (0) == "Senior Centre Assistants") {
					//create new shifts for each week that contains the string above
					ShiftTime Day1 = new ShiftTime ();
					ShiftTime Day2 = new ShiftTime ();
					ShiftTime Day3 = new ShiftTime ();
					ShiftTime Day4 = new ShiftTime ();
					ShiftTime Day5 = new ShiftTime ();
					ShiftTime Day6 = new ShiftTime ();
					ShiftTime Day7 = new ShiftTime ();

					//set the date for each day in the week
					Day1.shiftDate = excelReader.GetDateTime (1);
					Day2.shiftDate = excelReader.GetDateTime (2);
					Day3.shiftDate = excelReader.GetDateTime (3);
					Day4.shiftDate = excelReader.GetDateTime (4);
					Day5.shiftDate = excelReader.GetDateTime (5);
					Day6.shiftDate = excelReader.GetDateTime (6);
					Day7.shiftDate = excelReader.GetDateTime (7);

					//add the 7 days of the week to the list of shifts for this roster
					shifts.Add(Day1);
					shifts.Add(Day2);
					shifts.Add(Day3);
					shifts.Add(Day4);
					shifts.Add(Day5);
					shifts.Add(Day6);
					shifts.Add(Day7);

					bool isFound = false;
					while (isFound == false)
					{
						excelReader.Read ();
						if (excelReader.GetString(0) == user)
						{
							//if a shift is found, set the shift text to the cell value
							Day1.shiftText = excelReader.GetString(1);
							Day2.shiftText = excelReader.GetString(2);
							Day3.shiftText = excelReader.GetString(3);
							Day4.shiftText = excelReader.GetString(4);
							Day5.shiftText = excelReader.GetString(5);
							Day6.shiftText = excelReader.GetString(6);
							Day7.shiftText = excelReader.GetString(7);
							isFound = true;
						}
					}
				}
            }
            excelReader.Close();

            // return shift object based on username
            return shifts;
        }
    }
}
