using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
	public class Roster
	{
		public String filePath{get; set;}

		public User currentUser{get;set;}

		public double rosterPay{ get; set; }

		public double rosterHours{ get; set; }

		private readonly List<Day> _dayList = new List<Day>();
		public List<Day> dayList { get {return _dayList;}}

		private readonly List<User> _userList = new List<User>();
		public List<User> userList { get { return _userList; }}
	}
}

