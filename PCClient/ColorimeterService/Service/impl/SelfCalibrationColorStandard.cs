using ColorimeterDAO.WinDomain;
using ColorimeterService.Dao.impl;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorimeterService.Service.impl
{
    /// <summary>
    /// 3 自对比模式（自校验模式）
    /// </summary>
    public class SelfCalibrationColorStandard : AbstractColorStandard
    {
        private StripProductionInformationDomain spiDomain = StripProductionInformationDomain.Instance;

        private const float LENGTH  = 0f;
        private const float UPPER_HEIGHT  = float.MaxValue;
        private const float LOWER_HEIGHT = float.MinValue;

        public UITextBox textBox_CurrentL;
        public UITextBox textBox_CurrentA;
        public UITextBox textBox_CurrentB;

        public SelfCalibrationColorStandard(            
            ref UITextBox textBox_CurrentL,
            ref UITextBox textBox_CurrentA,
            ref UITextBox textBox_CurrentB)
        {
            this.textBox_CurrentL = textBox_CurrentL;
            this.textBox_CurrentA = textBox_CurrentA;
            this.textBox_CurrentB = textBox_CurrentB;

            standardSrcMode = 3; // 标准值来源设为3

        }

        /// <summary>
        /// 自校验模式处理逻辑
        /// </summary>
        public override bool process(string colorCode, out float l_star, out float a_star, out float b_star)
        {
            // 如果检验过，直接返回检验得到的l a b
            if (flag)
            {
                l_star = float.Parse(this.textBox_CurrentL.Text);
                a_star = float.Parse(this.textBox_CurrentA.Text);
                b_star = float.Parse(this.textBox_CurrentB.Text);
                return true;
            }

            // 自检测当前值
            // 长度大于过焊缝距离，高度在有效范围内
            while ((flag = check(colorCode, out l_star, out a_star, out b_star)) == false) { }

            //DialogResult dr = MessageBox.Show("是否激活自对比模式？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (dr == DialogResult.Yes)
            //{
            //    csDomain.ifSelfCheck = true;
            //}
            //else
            //{
            //    csDomain.ifSelfCheck = false;
            //}

            //if (!csDomain.ifSelfCheck)
            //{
            //    MessageBox.Show("还没有实现LIMS...");
            //    return false;
            //   // return new LIMSColorStandard().process(colorCode, out l_star, out a_star, out b_star);
            //}

            return flag;
        }


        private bool check(string colorCode, out float l_star, out float a_star, out float b_star)
        {
            if (condition(float.Parse(spiDomain.length), float.Parse(spiDomain.broadwise)))
            {
                l_star = float.Parse(this.textBox_CurrentL.Text);
                a_star = float.Parse(this.textBox_CurrentA.Text);
                b_star = float.Parse(this.textBox_CurrentB.Text);

                csDomain.SelfCheck_colorCode = colorCode;
                csDomain.SelfCheck_l_star = l_star.ToString();
                csDomain.SelfCheck_a_star = a_star.ToString();
                csDomain.SelfCheck_b_star = b_star.ToString();
                csDomain.SelfCheck_sample_index = spiDomain.broadwise;
                return true;
            }
            l_star = a_star = b_star = float.NaN;
            return false;
        }


        private bool condition(float length, float height)
        {
            return length > LENGTH && height >= LOWER_HEIGHT && height <= UPPER_HEIGHT;
        }

        /// <summary>
        /// 重置检验状态，进行重采样
        /// </summary>
        public void resetStatus()
        {
            this.flag = false;
        }

        public override bool verify()
        {
            throw new NotImplementedException();
        }

        public override bool verify(ColorStandardDomain csDomain)
        {
            throw new NotImplementedException();
        }
    }
}