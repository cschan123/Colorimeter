using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorimeterDB;
using ColorimeterDAO.WinDomain;

namespace ColorimeterService.Dao.impl
{
    public class RealTimeProductionDaoImpl : RealTimeProductionDao
    {
        public void insert(ColorimeterDB.RealTimeProduction obj)
        {
            using (ColorimeterDBDataContext DBContext = new ColorimeterDBDataContext())
            {

                DBContext.RealTimeProduction.InsertOnSubmit(obj);

                DBContext.SubmitChanges();
            }
        }


    }
}
