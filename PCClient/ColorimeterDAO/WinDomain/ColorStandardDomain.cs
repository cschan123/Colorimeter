using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorimeterDAO.WinDomain
{
    /// <summary>
    /// 颜色标准窗体实例
    /// </summary>
    public class ColorStandardDomain
    {
        // lazy 单例
        private ColorStandardDomain() { }

        public static ColorStandardDomain Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested() { }
            internal static readonly ColorStandardDomain instance = new ColorStandardDomain();
        }

        /// <summary>
        /// 放行标准 ΔL*
        /// </summary>
        public string deltaL_std { get; set; }
        /// <summary>
        /// 放行标准 Δa*
        /// </summary>
        public string deltaA_std { get; set; }
        /// <summary>
        /// 放行标准 Δb*
        /// </summary>
        public string deltaB_std { get; set; }
        /// <summary>
        /// 放行标准 ΔE*
        /// </summary>
        public string deltaE_std { get; set; }
        /// <summary>
        /// 放行标准 L_C
        /// </summary>
        public string L_C { get; set; }
        /// <summary>
        /// 放行标准 a_C
        /// </summary>
        public string a_C { get; set; }
        /// <summary>
        /// 放行标准 b_C
        /// </summary>
        public string b_C { get; set; }

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

        /// ===========================人工输入==================================
        /// 颜色代码
        /// </summary>
        public string Manualinput_colorCode { get; set; }
        /// <summary>
        /// l标准值
        /// </summary>
        public string Manualinput_l_star { get; set; }
        /// <summary>
        /// a标准值
        /// </summary>
        public string Manualinput_a_star { get; set; }
        /// <summary>
        /// b标准值
        /// </summary>
        public string Manualinput_b_star { get; set; }
        /// ===========================人工输入==================================

        /// ===========================GDB3==================================

        /// <summary>
        /// 标样板3，2，1
        /// 颜色代码
        /// </summary>
        public string GDB3_colorCode { get; set; }
        /// <summary>
        /// l标准值
        /// </summary>
        public string GDB3_l_star { get; set; }
        /// <summary>
        /// a标准值
        /// </summary>
        public string GDB3_a_star { get; set; }
        /// <summary>
        /// b标准值
        /// </summary>
        public string GDB3_b_star { get; set; }
        /// ===========================GDB3==================================

        /// ===========================GDB2==================================
        public string GDB2_colorCode { get; set; }
        /// <summary>
        /// l标准值
        /// </summary>
        public string GDB2_l_star { get; set; }
        /// <summary>
        /// a标准值
        /// </summary>
        public string GDB2_a_star { get; set; }
        /// <summary>
        /// b标准值
        /// </summary>
        public string GDB2_b_star { get; set; }
        /// ===========================GDB2==================================

        /// ===========================GDB1==================================
        public string GDB1_colorCode { get; set; }
        /// <summary>
        /// l标准值
        /// </summary>
        public string GDB1_l_star { get; set; }
        /// <summary>
        /// a标准值
        /// </summary>
        public string GDB1_a_star { get; set; }
        /// <summary>
        /// b标准值
        /// </summary>
        public string GDB1_b_star { get; set; }
        /// ===========================GDB1==================================

        /// ===========================自对比==================================
        /// /// <summary>
        /// 颜色代码
        /// </summary>
        public string SelfCheck_colorCode { get; set; }
        /// <summary>
        /// l标准值
        /// </summary>
        public string SelfCheck_l_star { get; set; }
        /// <summary>
        /// a标准值
        /// </summary>
        public string SelfCheck_a_star { get; set; }
        /// <summary>
        /// b标准值
        /// </summary>
        public string SelfCheck_b_star { get; set; }
        /// <summary>
        /// 取样位置
        /// </summary>
        public string SelfCheck_sample_index { get; set; }
        /// ===========================自对比==================================

        /// ===========================LIMS==================================
        /// <summary>
        /// 颜色代码
        /// </summary>
        public string LIMS_colorCode { get; set; }
        /// <summary>
        /// l标准值
        /// </summary>
        public string LIMS_l_star { get; set; }
        /// <summary>
        /// a标准值
        /// </summary>
        public string LIMS_a_star { get; set; }
        /// <summary>
        /// b标准值
        /// </summary>
        public string LIMS_b_star { get; set; }
        /// <summary>
        /// 取样位置
        /// </summary>
        public string LIMS_sample_index { get; set; }
        /// <summary>
        /// 卷号
        /// </summary>
        public string LIMS_roll_number { get; set; }
        /// <summary>
        /// 分卷号
        /// </summary>
        public string LIMS_sub_roll_number { get; set; }
        /// <summary>
        /// delta e
        /// </summary>
        public string LIMS_delta_e { get; set; }
        /// ===========================LIMS==================================

        /// <summary>
        /// 是否Lab数据库矫正
        /// </summary>
        public bool ifLabDataBaseCorrect { get; set; }

        /// <summary>
        /// 是否激活自对比模式
        /// </summary>
        public bool ifSelfCheck { get; set; }



        /// ========================== 色差仪控制模块 指示灯 ==================
        /// <summary>
        /// 远程
        /// </summary>
        public Color remote { get { return PlcComm.DataExchange.DataExchange.Remote == true? Color.Lime:Color.Gray; } }
        /// <summary>
        /// 回零完成
        /// </summary>
        public Color homeReached1 { get { return PlcComm.DataExchange.DataExchange.HomeReached1 ? Color.Lime : Color.Gray; } }
        /// <summary>
        /// 伺服运行
        /// </summary>
        public Color motRunning { get { return PlcComm.DataExchange.DataExchange.MotRunning ? Color.Lime : Color.Gray; } }
        /// <summary>
        /// 色差仪状态绿灯，蓝灯，红灯，黄灯
        /// </summary>
        public Color statusGreen { get { return PlcComm.DataExchange.DataExchange.StatusGreen ? Color.Lime : Color.Gray; } }
        public Color statusBlue { get { return PlcComm.DataExchange.DataExchange.StatusBlue ? Color.Blue : Color.Gray; } }
        public Color statusRed { get { return PlcComm.DataExchange.DataExchange.StatusRed ? Color.Red : Color.Gray; } }
        public Color statusYellow { get { return PlcComm.DataExchange.DataExchange.StatusYellow ? Color.Yellow : Color.Gray; } }
        /// <summary>
        /// 过焊缝报警喇叭灯
        /// </summary>
        public Color weldHorn1 { get { return PlcComm.DataExchange.DataExchange.WeldHorn1 ? Color.Red : Color.Gray; } }
        /// <summary>
        /// 故障报警灯
        /// </summary>
        public Color error { get { return PlcComm.DataExchange.DataExchange.Error ? Color.Red : Color.Gray; } }

        /// ========================== 色差仪控制模块 textBox显示 伺服代码 ==================
        /// <summary>
        /// 伺服故障代码
        /// </summary>
        public string servoErrorCode { get; set; }
        /// <summary>
        /// 伺服报警代码
        /// </summary>
        public string servoWarningCode { get; set; }

        /// ============================ 右侧色差仪滑动杆显示
        /// <summary>
        /// 极限开关+
        /// </summary>
        public Color pmax { get { return PlcComm.DataExchange.DataExchange.PMax ? Color.Lime : Color.Gray; } }
        /// <summary>
        /// 极限开关-
        /// </summary>
        public Color pmin { get { return PlcComm.DataExchange.DataExchange.PMin ? Color.Lime : Color.Gray; } }
        /// <summary>
        /// 操作侧移动位置显示
        /// </summary>
        public string db200_dbd178 { get ; set; }
        public string db200_dbd182 { get; set; }
        /// <summary>
        /// 位置设定
        /// </summary>
        public string positionRef { get; set; }
        /// <summary>
        /// 当前位置
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 工艺段速度
        /// </summary>
        public string technicSpeed { get; set; }
        /// <summary>
        /// 出口段速度
        /// </summary>
        public string exportSpeed { get; set; }
        /// <summary>
        /// 卷取长度
        /// </summary>
        public string rollLength { get; set; }
        /// <summary>
        /// 过焊缝6#长度
        /// </summary>
        public string hanFeng6 { get; set; }
        /// <summary>
        /// 过焊缝5#长度
        /// </summary>
        public string hanFeng5 { get; set; }
    }
}
