using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorimeterDB;
namespace ColorimeterDAO.Dao.impl
{
    public class UserDaoImpl : UserDao
    {
        private static ColorimeterDBDataContext DBContext = new ColorimeterDBDataContext();
        public User findFirstUser (string _userName, string _password)
        {
            return DBContext.User.FirstOrDefault(u => u.userName == _userName && u.password == _password);
        }

        public int countUserName(string _userName)
        {
            return DBContext.User.Where(u => u.userName == _userName).Count();
        }

    }
}
