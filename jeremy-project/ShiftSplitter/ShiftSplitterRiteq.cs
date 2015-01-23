using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using Excel;
using System.Data;

namespace jeremy_project
{
	class ShiftSplitterRiteq
	{
		public static void SplitTheShifts(List<Day> dayList)
		{
			foreach (Day day in dayList) {
				//check to see if there are more than one shift and split it by the pattern
				if (day.dayText != null) {

					ShiftTime shiftOne = new ShiftTime ();
					day.listOfShifts.Add (shiftOne);
					ShiftTime shiftTwo = new ShiftTime ();

					string pattern = "/";
					string[] multiShifts = Regex.Split (day.dayText, pattern);
					if (multiShifts.Length > 1)
						day.listOfShifts.Add (shiftTwo);


					//begin the shift count
					int shiftCount = 1;

					foreach (string shift in multiShifts) {
						int wordCount = 1;
						string[] separators = { "-", "(", ")", " ", "Extra" };
						string[] words = shift.Split (separators, StringSplitOptions.RemoveEmptyEntries);
						//set the single shift text per shift
						if (shiftCount == 1) {
							shiftOne.singleShiftText = To12HourTime (shift);
						} if (shiftCount == 2) {
							shiftTwo.singleShiftText = To12HourTime (shift);
						}
						//for each shift it finds, break it down
						foreach (var word in words) {
							findShiftTimes (word, wordCount, day, shiftCount, shiftOne, shiftTwo);
							wordCount += 1;
						}
						shiftCount += 1;
					}
				}
			}
		}

		public static void findShiftTimes(string word, int count, Day day, int shiftCount, ShiftTime shiftOne, ShiftTime shiftTwo)
		{
			double dec = Convert.ToDouble (Convert.ToDecimal (TimeSpan.Parse (word).TotalHours));
			if (shiftCount == 1) {
				if (count == 1) {
					shiftOne.ShiftStart = findShiftStart (day.dayDate, dec);
				}

				if (count == 2) {
					shiftOne.ShiftEnd = findShiftEnd (day.dayDate, dec);
				}
			}

			if (shiftCount == 2) {
				if (count == 1) {
					shiftTwo.ShiftStart = findShiftStart (day.dayDate, dec);
				}

				if (count == 2) {
					shiftTwo.ShiftEnd = findShiftEnd (day.dayDate, dec);
				}
			}
		}

		public static DateTime findShiftStart(DateTime day, double dec)
		{
			DateTime shiftStart = new DateTime ();
			shiftStart = day.AddHours (dec);
			return shiftStart;
		}

		public static DateTime findShiftEnd(DateTime day, double dec)
		{
			DateTime shiftEnd = new DateTime ();
			shiftEnd = day.AddHours (dec);
			return shiftEnd;
		}

		public static string To12HourTime(string shift)
		{
			string[] separators = { "-" };
			string[] words = shift.Split (separators, StringSplitOptions.RemoveEmptyEntries);
			string fixedTime = string.Empty;
			int shiftCount = 1;

			foreach (string word in words) {
				string amPmPart = "am";
				string fractionHour = string.Empty;
				double dec = Convert.ToDouble (Convert.ToDecimal (TimeSpan.Parse (word).TotalHours));
				if (dec > 12) {
					dec = (dec - 12);
					amPmPart = "pm";
				}
				if (dec % 1.0 > 0) {
					fractionHour = ":" + ((dec % 1.0) * 60).ToString();
					dec = (dec - (dec % 1.0));
				}
				if (shiftCount == 1)
					fixedTime += (dec.ToString () + fractionHour + amPmPart + " - ");
				if (shiftCount == 2)
					fixedTime += (dec.ToString () + fractionHour + amPmPart);
				shiftCount += 1;
			}
			return fixedTime;
		}
	}
}
