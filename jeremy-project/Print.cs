using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
    class Print
    {
        public static void PrintShiftTimes(List<Shift> shifts)
        {
            foreach (Shift shift in shifts)
            {
				Console.WriteLine (shift.shiftDate.ToShortDateString() + " , " + shift.shiftText);
                // print any info you need based off the object.
//                Console.WriteLine(shift.Days[0] + " = " + shift.Day1);
//                Console.WriteLine(shift.Days[1] + " = " + shift.Day2);
//                Console.WriteLine(shift.Days[2] + " = " + shift.Day3);
//                Console.WriteLine(shift.Days[3] + " = " + shift.Day4);
//                Console.WriteLine(shift.Days[4] + " = " + shift.Day5);
//                Console.WriteLine(shift.Days[5] + " = " + shift.Day6);
//                Console.WriteLine(shift.Days[6] + " = " + shift.Day7);
//                Console.WriteLine("Total Hours = " + shift.ShiftTotal);
//                Console.WriteLine(String.Empty);
            }
        }
    }
}
