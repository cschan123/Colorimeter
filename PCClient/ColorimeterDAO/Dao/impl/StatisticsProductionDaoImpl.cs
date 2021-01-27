using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorimeterDB;

namespace ColorimeterService.Dao.impl
{
    public class StatisticsProductionDaoImpl : StatisticsProductionDao
    {
        
        public void insert(StatisticsProduction obj)
        {
            using (ColorimeterDBDataContext DBContext = new ColorimeterDBDataContext())
            {

                DBContext.StatisticsProduction.InsertOnSubmit(obj);
                DBContext.SubmitChanges();
        
            }
        }
    }
}
