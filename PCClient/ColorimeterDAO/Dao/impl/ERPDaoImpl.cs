using ColorimeterDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorimeterDAO.Dao.impl
{
    public class ERPDaoImpl : ERPDao
    {
        private static ColorimeterDBDataContext DBContext = new ColorimeterDBDataContext();

        public ERP findERP(string _COLDCOILNO)
        {
            return DBContext.ERP.FirstOrDefault(e => e.COLDCOILNO == _COLDCOILNO);
        }
    }
}
