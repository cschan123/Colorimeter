using ColorimeterDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorimeterDAO.Dao.impl
{
   public class PassStdDaoImpl : PassStdDao
    {
        public PassStd findFirstPassStd(string _colorCode)
        {
            using (ColorimeterDBDataContext DBContext = new ColorimeterDBDataContext())
            {
                return DBContext.PassStd.FirstOrDefault(p => p.ColorCode == _colorCode);

            }
        }

    }
}
