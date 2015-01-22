using System;

namespace jeremy_project
{
	public class CheckCase
	{
		public static bool Contain(this string source, string toCheck, StringComparison comp)
		{
			return source.IndexOf(toCheck, comp) >= 0;
		}
	}
}

