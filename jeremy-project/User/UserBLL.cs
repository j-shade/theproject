using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeremy_project
{
    class UserBLL
    {
        public static List<User> GetUserObjects(string FilePath)
        {
            return UserDAL.GetAllUsers(FilePath);
        }
    }
}
