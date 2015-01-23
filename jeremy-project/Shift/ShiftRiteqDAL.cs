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
	class ShiftRiteqDAL : IShiftDAL
	{
		public void GetShiftObjectsForUser(string user, Roster roster)
		{    
			// path to excel, read in all the users to a user object
			FileStream stream = File.Open(roster.filePath, FileMode.Open, FileAccess.Read);

			// read in file
			IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
			while (excelReader.Read())
			{
				if (excelReader.GetString (0) == "City of Canning\\Leisure\\Riverton LeisurePlex") {
					//create new shifts for each week that contains the string above
					Day Day1 = new Day ();
					Day Day2 = new Day ();
					Day Day3 = new Day ();
					Day Day4 = new Day ();
					Day Day5 = new Day ();
					Day Day6 = new Day ();
					Day Day7 = new Day ();

					//set the date for each day in the week
					Day1.dayDate = excelReader.GetDateTime (1);
					Day2.dayDate = excelReader.GetDateTime (2);
					Day3.dayDate = excelReader.GetDateTime (3);
					Day4.dayDate = excelReader.GetDateTime (4);
					Day5.dayDate = excelReader.GetDateTime (5);
					Day6.dayDate = excelReader.GetDateTime (6);
					Day7.dayDate = excelReader.GetDateTime (7);

					//add the 7 days of the week to the list of shifts for this roster
					roster.dayList.Add(Day1);
					roster.dayList.Add(Day2);
					roster.dayList.Add(Day3);
					roster.dayList.Add(Day4);
					roster.dayList.Add(Day5);
					roster.dayList.Add(Day6);
					roster.dayList.Add(Day7);

					bool isFound = false;
					while (isFound == false)
					{
						excelReader.Read ();
						if (excelReader.GetString (0) != null) {
							//see if names match while ignoring text case
							bool isUser = user.IndexOf (excelReader.GetString (0), StringComparison.OrdinalIgnoreCase) >= 0;
							if (isUser == true)
							{
								//if a shift is found, set the shift text to the cell value
								Day1.dayText = excelReader.GetString(1);
								Day2.dayText = excelReader.GetString(2);
								Day3.dayText = excelReader.GetString(3);
								Day4.dayText = excelReader.GetString(4);
								Day5.dayText = excelReader.GetString(5);
								Day6.dayText = excelReader.GetString(6);
								Day7.dayText = excelReader.GetString(7);
								isFound = true;
							}
						}
					}
				}
			}
			excelReader.Close();
		}
	}
}
