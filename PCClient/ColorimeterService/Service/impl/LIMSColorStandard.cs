using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorimeterService.Service.impl
{
    /// <summary>
    /// 4 LIMS 校验模式
    /// </summary>
    public class LIMSColorStandard : AbstractColorStandard
    {
        public LIMSColorStandard()
        {
            standardSrcMode = 4; // 标准值来源设为4
        }

        /// <summary>
        /// LIMS校验模式处理逻辑
        /// </summary>
        public override bool process(string colorCode, out float l_star, out float a_star, out float b_star)
        {
            // TODO
            // 在T0时刻，通过LIMS取样上抛的EXCEL得到：卷号、分卷号、颜色值。ΔL，Δa，Δb
            // 在历史数据库，实时检测数据表中根据 卷号、分卷号、颜色代码后6位、长度（人为设置）、高度（人为设置）查询对应的颜色检测记录。
            // 如果不匹配（没有查询结果）取消LIMS检验模式，否则，第一条匹配值l,a,b + LIMS Δl, Δa, Δb => l*, a*, b*
            l_star = a_star = b_star = float.NaN;
            return false;
        }

        public override bool verify()
        {
            throw new NotImplementedException();
        }



        public override bool verify(ColorimeterDAO.WinDomain.ColorStandardDomain csDomain)
        {
            throw new NotImplementedException();
        }
    }
}
