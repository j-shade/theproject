﻿using System;
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
	class ShiftSplitter
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
							shiftOne.singleShiftText = shift;
						} if (shiftCount == 2) {
							shiftTwo.singleShiftText = shift;
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
			if (shiftCount == 1) {
				if (count == 1) {
					shiftOne.ShiftStart = findShiftStart (day.dayDate, word, dec, shiftVary);
				}

				if (count == 2) {
					shiftOne.ShiftEnd = findShiftEnd (day.dayDate, word, dec, shiftVary);
				}

				if (count == 3) {
					shiftOne.shiftType = findShiftType (word);
				}
			}

			if (shiftCount == 2) {
				if (count == 1) {
					shiftTwo.ShiftStart = findShiftStart (day.dayDate, word, dec, shiftVary);
				}

				if (count == 2) {
					shiftTwo.ShiftEnd = findShiftEnd (day.dayDate, word, dec, shiftVary);
				}

				if (count == 3) {
					shiftTwo.shiftType = findShiftType (word);
				}
			}
		}

		public static DateTime findShiftStart(DateTime day, string word, double dec, double shiftVary)
		{
			DateTime shiftStart = new DateTime ();
			if (word.Contains ("pm")) {
				shiftStart = day.AddHours (shiftVary);
			}
			if (word.Contains ("am")) {
				shiftStart = day.AddHours (dec);
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
