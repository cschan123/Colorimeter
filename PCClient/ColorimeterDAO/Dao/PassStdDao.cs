using ColorimeterDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorimeterDAO.Dao
{
    /// <summary>
    /// PassStd放行标准表 dao
    /// </summary>
    public interface PassStdDao
    {
        /// <summary>
        /// 根据ColorCode颜色代码查找数据行
        /// </summary>
        /// <param name="_colorCode"></param>
        /// <returns></returns>
        PassStd findFirstPassStd(string _colorCode);
    }
}
