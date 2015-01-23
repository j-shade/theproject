using System;

namespace jeremy_project
{
	public class GetUserName
	{
		public static string GetName ()
		{
			bool nameIsValid = false;
			string userName = string.Empty;
			while (nameIsValid == false) {
				Console.WriteLine ("What is the employee Name?");
				userName = Console.ReadLine ();
				if (userName.Contains (" ") && userName != null) {
					nameIsValid = true;
				}
				else
					Console.WriteLine ("The input is not valid. Please make sure it is a full name, i.e. John Hancock");
			}
			return userName;
		}
	}
}

