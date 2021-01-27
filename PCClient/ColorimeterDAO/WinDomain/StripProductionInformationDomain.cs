using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ColorimeterDAO.WinDomain
{
    public sealed class StripProductionInformationDomain
    {
        // lazy 单例
        private StripProductionInformationDomain() { }

        public static StripProductionInformationDomain Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested() { }
            internal static readonly StripProductionInformationDomain instance = new StripProductionInformationDomain();
        }

        /// <summary>
        /// 卷生产时间
        /// </summary>
        private volatile string val;
        public string rollStartProductTime { get { return val; } set { val = value; } }
        /// <summary>
        /// 卷结束时间
        /// </summary>
        public string rollEndProductTime { get; set; }
        /// <summary>
        /// 卷号
        /// </summary>
        public string rollNumber { get; set; }
        /// <summary>
        /// 分卷号
        /// </summary>
        public string subRollNumber { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public string length { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 厚度
        /// </summary>
        public string thickness { get; set; }
        /// <summary>
        /// 横向
        /// </summary>
        public string broadwise { get; set; }
        /// <summary>
        /// 颜色代码
        /// </summary>
        public string colorCode { get; set; }

        /// <summary>
        /// 颜色名称
        /// </summary>
        public string colorName { get; set; }
        /// <summary>
        /// 偏差值 ΔL
        /// </summary>
        public string deltaL { get; set; }
        /// <summary>
        /// 偏差值 ΔA
        /// </summary>
        public string deltaA { get; set; }
        /// <summary>
        /// 偏差值 ΔB
        /// </summary>
        public string deltaB { get; set; }
        /// <summary>
        /// 标准差 ΔE
        /// </summary>
        public string deltaE { get; set; }
        /// <summary>
        /// 是否合格
        /// </summary>
        public bool ifGood { get; set; }

        /// <summary>
        /// 标准颜色值L
        /// </summary>
        public string std_L { get; set; }

        /// <summary>
        /// 标准颜色值a
        /// </summary>
        public string std_a { get; set; }

        /// <summary>
        /// 标准颜色值b
        /// </summary>
        public string std_b { get; set; }

        /// <summary>
        /// 卷号是否改变，只有卷号改变了，才查询数据库，得到颜色代码。
        /// 需要在rollNumber赋值之前调用
        /// </summary>
        public bool ifRollNumberChanged(string currentRollNumber)
        {
            bool changed = currentRollNumber.Equals(this.rollNumber);
            return !changed; 
        }

        /// <summary>
        /// 不合格计数
        /// </summary>
        public string notGoodCount { get; set; }

        /// <summary>
        /// 检测数量
        /// </summary>
        public string checkCount { get; set; }

        /// <summary>
        /// 是否合格的填充颜色
        /// </summary>
        public Color fillColor { get; set; }
        /// <summary>
        /// 是否合格的填充文本
        /// </summary>
        public string fillText { get; set; }

        /// <summary>
        /// 不是第一次改变卷号 bool
        /// </summary>
        public bool notfirstChangedRollNumber { get; set; }


    }
}
