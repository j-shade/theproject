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

		public double rosterPay{ get; set; }

		public double rosterHours{ get; set; }

		private readonly List<Shift> _dayList = new List<Shift>();
		public List<Shift> dayList { get {return _dayList;}}

		private readonly List<User> _userList = new List<User>();
		public List<User> userList { get { return _userList; }}
	}
}

