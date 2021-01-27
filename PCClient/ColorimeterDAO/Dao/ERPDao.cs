using ColorimeterDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorimeterDAO.Dao
{
    /// <summary>
    /// ERP表DAO
    /// </summary>
    public interface ERPDao
    {
        /// <summary>
        /// 根据 卷号 查询 ERP数据行
        /// </summary>
        /// <param name="COLDCOILNO"></param>
        /// <returns></returns>
        ERP findERP(string _COLDCOILNO);
      
    }
}
