using ColorimeterDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorimeterService.Dao.impl
{
    public class ParameterDaoImpl : ParameterDao
    {
        private static ColorimeterDBDataContext DBContext = new ColorimeterDBDataContext();
        public void insert(Parameter para)
        {
            DBContext.Parameter.InsertOnSubmit(para);
            DBContext.SubmitChanges();
        }
    }
}
