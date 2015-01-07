using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace jeremy_project
{
	class SplitTheShift
	{
		public static void breakShiftDown(string value)
		{
			int count = 1;
			string[] separators = { "-", "(", ")", " ", "/", "Extra"};
			string[] words = value.Split (separators,StringSplitOptions.RemoveEmptyEntries);
			foreach (var word in words) {
				findShiftTimes(word, count);
				//				Console.WriteLine (word);
				//				Console.WriteLine (count);
				count += 1;
			}
		}

		public static void findShiftTimes(string word, int count)
		{
			DateTime day = new DateTime ();
			double dec = 0.00;
			decimal tempDec;
			string shiftTime = "";
			if (Regex.IsMatch (word, @"[GTSF]") != true) {
				int stringLength = word.Length;
				if (stringLength > 0) {
					shiftTime = word.Substring (0, stringLength - 2);
					if (shiftTime.Length < 3)
						shiftTime += ":00";
					if (shiftTime.Contains ("."))
						shiftTime = shiftTime.Replace (".", ":");
					tempDec = Convert.ToDecimal (TimeSpan.Parse (shiftTime).TotalHours);
					dec = Convert.ToDouble (tempDec);
				}
			}
			double shiftVary = dec + 12.0;
			DateTime shift = day;
			if (count < 4) {
				//				DateTime shiftStart = day;
				//				DateTime shiftEnd = day;

				if (count == 1) {
					DateTime shiftStart = findShiftStart (shift, word, dec, shiftVary);
				}

				if (count == 2) {
					DateTime shiftEnd = findShiftEnd (shift, word, dec, shiftVary);
				}

				if (count == 3) {
					word = findShiftType (word);
					if (word.Length == 0)
						count += 1;
				}
			}

			if (count > 3) {
				if (count == 4) {
					DateTime shiftTwoStart = findShiftStart (shift, word, dec, shiftVary);
				}

				if (count == 5) {
					DateTime shiftTwoEnd = findShiftEnd (shift, word, dec, shiftVary);
				}

				if (count == 6) {
					word = findShiftType (word);
					if (word.Length == 0)
						count += 1;
				}
			}
		}

		public static void TimeSpanTest()
		{
			TimeSpan timespan = new TimeSpan ();
			DateTime shiftStart = new DateTime ();
			DateTime shiftEnd = new DateTime ();
			shiftStart.AddHours (16.0);
			shiftEnd.AddHours (20.0);
			timespan = shiftEnd - shiftStart;
			Console.WriteLine (timespan.Hours.ToString());
		}

		public static DateTime findShiftStart(DateTime day, string word, double dec, double shiftVary)
		{
			DateTime shiftStart = new DateTime ();
			if (word.Contains ("pm")) {
				shiftStart = day.AddHours (shiftVary);
				Console.WriteLine ("The shift starts at {0}", shiftStart);
			}
			if (word.Contains ("am")) {
				shiftStart = day.AddHours (dec);
				Console.WriteLine ("The shift starts at {0}", shiftStart);
			}
			return shiftStart;
		}

		public static DateTime findShiftEnd(DateTime day, string word, double dec, double shiftVary)
		{
			DateTime shiftEnd = new DateTime ();
			if (word.Contains ("pm")) {
				shiftEnd = day.AddHours (shiftVary);
				Console.WriteLine ("The shift ends at {0}", shiftEnd);
			} 
			if (word.Contains ("am")) {
				shiftEnd = day.AddHours (dec);
				Console.WriteLine ("The shift ends at {0}", shiftEnd);
			}
			return shiftEnd;
		}

		public static string findShiftType(string inWord)
		{
			string word = string.Empty;
			if (Regex.IsMatch (word, @"[GTSF]")) {
				switch (word) {
				case "G":
					word = "gym";
					Console.WriteLine ("This is a {0} shift", word);
					break;
				case "T":
					word = "training";
					Console.WriteLine ("This is a {0} shift", word);
					break;
				case "S":
					word = "slide";
					Console.WriteLine ("This is a {0} shift", word);
					break;
				case "F":
					word = "function";
					Console.WriteLine ("This is a {0} shift", word);
					break;
				default:
					word = "standard";
					Console.WriteLine ("This is a {0} shift", word);
					break;
				}
			}
			return word;
		}
	}
}

