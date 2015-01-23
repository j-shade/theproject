using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace jeremy_project
{    
    public class User
    {        
        public string EmployeeName { get; set; }
		public int EmployeeLevel{get; set;}
		public int EmployeeAge{ get; set; }
    }
}
