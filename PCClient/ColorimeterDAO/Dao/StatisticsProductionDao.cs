using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorimeterDB;

namespace ColorimeterService.Dao
{
    /// <summary>
    /// 分卷统计数据表
    /// </summary>
    public interface StatisticsProductionDao
    {
        /// <summary>
        /// 插入并提交数据库，记入分卷数据
        /// </summary>
        /// <param name="obj"></param>
        void insert(StatisticsProduction obj);
    }
}
