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
        public static List<Shift> GetShiftObjectsForUser(string user, string filePath)
        {
            List<Shift> shifts = new List<Shift>();
            
            // path to excel, read in all the users to a user object
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            // read in file
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            while (excelReader.Read())
            {
                if (excelReader.GetString(0) == user)
                {
					Shift shift = new Shift();
                    shift.Day1 = excelReader.GetString(1);
                    shift.Day2 = excelReader.GetString(2);
                    shift.Day3 = excelReader.GetString(3);
                    shift.Day4 = excelReader.GetString(4);
                    shift.Day5 = excelReader.GetString(5);
                    shift.Day6 = excelReader.GetString(6);
                    shift.Day7 = excelReader.GetString(7);
                    shift.ShiftTotal = excelReader.GetString(8);
                    shifts.Add(shift);
                }
            }
            excelReader.Close();

            // return shift object based on username
            return shifts;
        }
    }
}
