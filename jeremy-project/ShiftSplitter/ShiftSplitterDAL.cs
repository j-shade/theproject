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
	class ShiftSplitterDAL
	{

		public static void OneShiftAtATime(List<ShiftTime> shiftList)
		{
			foreach (ShiftTime shift in shiftList) {
				MakeDateTime(shift);
			}
		}

		public static void MakeDateTime(ShiftTime shiftValue)
		{
			//check to see if there are more than one shift and split it by the pattern
			if (shiftValue.shiftText != null) {
				string pattern = "/";
				string[] multiShifts = Regex.Split (shiftValue.shiftText, pattern);
				//begin the shift count
				int shiftCount = 1;
				foreach (string shift in multiShifts) {
					int wordCount = 1;
					//Console.WriteLine ("\n Shift {0} : \n", shiftCount);
					shiftCount += 1;
					string[] separators = { "-", "(", ")", " ", "Extra" };
					string[] words = shift.Split (separators, StringSplitOptions.RemoveEmptyEntries);
					//for each shift it finds, break it down
					foreach (var word in words) {
						findShiftTimes (word, wordCount, shiftValue);
						wordCount += 1;
					}
				}
			}
		}

		public static void findShiftTimes(string word, int count, ShiftTime shiftValue)
		{
			double dec = 0.00;
			string shiftTime = word;
			//make sure the word doesnt contain any shift type identifiers
			if (Regex.IsMatch (word, @"[GTSF]") != true) {
				int stringLength = word.Length;
				if (stringLength > 0) {
					//if the string has am/pm, remove it
					if (Regex.IsMatch(word, @"[apm]")){
						shiftTime = word.Substring (0, stringLength - 2);
					}
					//if the shiftTime is only 1 or 2 strings (11am, 3pm etc), append :00 for DateTime conventions
					if (shiftTime.Length < 3) {
						shiftTime += ":00";
					}
					//fix roster formatting where a . is used instead of a :
					if (shiftTime.Contains ("."))
						shiftTime = shiftTime.Replace (".", ":");
					//Convert the time into a decimal to allow for DateTime addition later
					dec = Convert.ToDouble (Convert.ToDecimal (TimeSpan.Parse (shiftTime).TotalHours));
				}
			}
			//fix rostering anomalies..
			if (word == "11")
				word = "11:00am";
			if (word == "3:30")
				word = "3:30pm";

			//create the shift vary in 24h time for DateTime
			double shiftVary = dec + 12.0;
			if (dec == 12)
				shiftVary = 12.0;
			//DateTime shift = day;
			if (count == 1) {
				shiftValue.ShiftStart = findShiftStart (shiftValue.shiftDate, word, dec, shiftVary);
			}

			if (count == 2) {
				shiftValue.ShiftEnd = findShiftEnd (shiftValue.shiftDate, word, dec, shiftVary);
			}

			if (count == 3) {
				shiftValue.shiftType = findShiftType (word);
			}
		}

		public static DateTime findShiftStart(DateTime day, string word, double dec, double shiftVary)
		{
			DateTime shiftStart = new DateTime ();
			if (word.Contains ("pm")) {
				shiftStart = day.AddHours (shiftVary);
//				Console.WriteLine ("The shift starts at {0}", shiftStart);
			}
			if (word.Contains ("am")) {
				shiftStart = day.AddHours (dec);
//				Console.WriteLine ("The shift starts at {0}", shiftStart);
			}
			return shiftStart;
		}

		public static DateTime findShiftEnd(DateTime day, string word, double dec, double shiftVary)
		{
			DateTime shiftEnd = new DateTime ();
			if (word.Contains ("pm")) {
				shiftEnd = day.AddHours (shiftVary);
//				Console.WriteLine ("The shift ends at {0}", shiftEnd);
			} 
			if (word.Contains ("am")) {
				shiftEnd = day.AddHours (dec);
//				Console.WriteLine ("The shift ends at {0}", shiftEnd);
			}
			return shiftEnd;
		}

		public static string findShiftType(string word)
		{
			switch (word) {
			case "G":
				word = "gym";
//				Console.WriteLine ("This is a {0} shift", word);
				break;
			case "T":
				word = "training";
//				Console.WriteLine ("This is a {0} shift", word);
				break;
			case "S":
				word = "slide";
//				Console.WriteLine ("This is a {0} shift", word);
				break;
			case "F":
				word = "function";
//				Console.WriteLine ("This is a {0} shift", word);
				break;
			default:
				word = "standard";
//				Console.WriteLine ("This is a {0} shift", word);
				break;
			}
			return word;
		}
	}
}

