using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorimeterService.Service.impl
{
    /// <summary>
    /// 1 人工电子标样模式
    /// </summary>
    public class ArtificialElectronicsColorStandard : AbstractColorStandard
    {
        public ArtificialElectronicsColorStandard()
        {
            standardSrcMode = 1;// 标准值来源设为1
        }

        /// <summary>
        /// 人工电子标样模式处理逻辑，获得人工输入的代码的标准值。
        /// </summary>
        public override bool process(string colorCode, out float l_star, out float a_star, out float b_star)
        {
            csDomain.Manualinput_colorCode = colorCode;
            if (!verify())
            {
              //  MessageBox.Show("输入不完整！请输入完整颜色标准值！");
                l_star = a_star = b_star = float.NaN;
                flag = false;
                return flag;
            }
            
            l_star = float.Parse(csDomain.Manualinput_l_star);
            a_star = float.Parse(csDomain.Manualinput_a_star);
            b_star = float.Parse(csDomain.Manualinput_b_star);
            flag = true;
            return flag;
        }

        public override bool verify()
        {
            return this.verify(csDomain);
        }

        public override bool verify(ColorimeterDAO.WinDomain.ColorStandardDomain csDomain)
        {
            if (string.IsNullOrEmpty(csDomain.Manualinput_colorCode) ||
                string.IsNullOrEmpty(csDomain.Manualinput_l_star) ||
                string.IsNullOrEmpty(csDomain.Manualinput_a_star) ||
                string.IsNullOrEmpty(csDomain.Manualinput_b_star))
            {
                return false;
            }
            return true;
        }

    }
}
