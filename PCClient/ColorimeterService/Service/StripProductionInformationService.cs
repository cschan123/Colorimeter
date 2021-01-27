using ColorimeterDAO.WinDomain;
using ColorimeterDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ColorimeterService.Service
{
    /// <summary>
    /// 带钢生产信息服务接口
    /// </summary>
    public interface StripProductionInformationService
    {
        /// <summary>
        /// 获取当前值 l a b
        /// 根据当前值Lab和标准色l*a*b*计算得到偏差值ΔL，Δa，Δb和标准差ΔE，并根据放行标准判断是否合格
        /// </summary>
        void currentJudge();

        /// <summary>
        /// 初始化连接400PLC
        /// </summary>
        void initConnectPlc();

        /// <summary>
        /// 读取400PLC实时生产数据，并展示在用户前台界面
        /// </summary>
        void readPLC();
        
        /// <summary>
        /// 根据卷号，在ERP中查询对应的颜色代码ColorCode，及钢卷厚度，宽度
        /// </summary>
        /// <param name="coldCoilNo">卷号</param>
        void getColorCodeAndThickAndWeight(out string colorCode, string coldCoilNo);

        /// <summary>
        /// 根据ColorCode，在PassStd表中查询对应colorcode的 △L,△a,△b 和 L_C,a_C,b_C, 以及颜色名称colorName
        /// </summary>
        /// <param name="colorCode">颜色代码</param>
        ColorimeterService.Service.impl.StripProductionInformationServiceImpl.deltaAndC getDeltaAndC(string colorCode, out string colorName);
        

        /// <summary>
        /// 生成带钢生产信息，包括：卷号、分卷号、厚度、宽度、ColorCode、△L*(delta_l_std)，△a*,△b*,△e*、L_C,a_C,b_C 等。
        /// </summary>
        RealTimeProduction generateRealTimeProductionMessage();

        /// <summary>
        /// 获取标准色l*, a*, b*，4种方式：1.固定板、2.人工输入、3.自校验、4.LIMS
        /// </summary>
        void getStandardColor();


        AbstractColorStandard absColorStd { get; set; }

    }
}
