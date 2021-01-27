using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorimeterDB;

namespace ColorimeterDAO.Dao
{
    public interface UserDao
    {
         User findFirstUser (string username, string password);
         int countUserName(string username);
    }
}
