using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorimeterDAO.WinDomain;
using ColorimeterDAO.Dao;

namespace ColorimeterService.Service
{
    /// <summary>
    /// 基础颜色标准抽象类
    /// </summary>
    public abstract class AbstractColorStandard
    {
        /// <summary>
        /// 处理获取 颜色标准, 得到L* a* b*
        /// </summary>
        public abstract bool process(string colorCode, out float l_star, out float a_star, out float b_star);

        /// <summary>
        /// 验证是否填写完整且合法
        /// </summary>
        /// <returns>bool</returns>
        public abstract bool verify();

        /// <summary>
        /// 颜色标准窗体实体类
        /// </summary>
        public static ColorStandardDomain csDomain = ColorStandardDomain.Instance;

        /// <summary>
        /// 验证是否填写完整
        /// </summary>
        /// <param name="csDomain">ColorStandardDomain</param>
        /// <returns>bool</returns>
        public abstract bool verify(ColorStandardDomain csDomain);

        /// <summary>
        /// 是否已经检验过
        /// </summary>
        public volatile bool flag;

        /// <summary>
        /// 标准值来源 1，2，3，4
        /// </summary>
        public float standardSrcMode { get; set; }
    }
}
