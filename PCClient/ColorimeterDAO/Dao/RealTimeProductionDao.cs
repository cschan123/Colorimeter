using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorimeterDB;
using ColorimeterDAO.WinDomain;

namespace ColorimeterService.Dao
{
    /// <summary>
    /// 实时生产检测数据表dao接口
    /// </summary>
    public interface RealTimeProductionDao
    {
        /// <summary>
        /// 向RealTimeProduction表中插入一条实时数据
        /// </summary>
        /// <param name="obj"></param>
        void insert(RealTimeProduction obj);

    }
}
