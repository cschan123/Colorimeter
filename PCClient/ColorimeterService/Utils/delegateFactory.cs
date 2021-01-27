using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunny.UI;
using System.Windows.Forms;
using System.Drawing;

namespace ColorimeterService.Utils
{
    /// <summary>
    /// 委托工厂，多线程访问控件委托
    /// </summary>
    public static class DelegateFactory
    {
        /// <summary>
        /// 改变Label中Text
        /// </summary>
        /// <param name="label"></param>
        /// <param name="data"></param>
        private delegate void changeDataInLabelDelegate(ref Label label, string data);
        public static void changeDataInLabel(ref Label label, string data)
        {
            if (label.InvokeRequired)
            {
                changeDataInLabelDelegate d = changeDataInLabel;
                label.Invoke(d, label, data);
            }
            else
            {
                label.Text = data;
            }
        }

        /// <summary>
        /// 改变Label的foreColor
        /// </summary>
        /// <param name="label"></param>
        /// <param name="color"></param>
        private delegate void changeColorInLabelDelegate(ref Label label, Color color);
        public static void changeColorInLable(ref Label label, Color color)
        {
            if (label.InvokeRequired)
            {
                changeColorInLabelDelegate d = changeColorInLable;
                label.Invoke(d, label, color);
            }
            else
            {
                label.ForeColor = color;
            }
        }

        private delegate void showDataInTextBoxDelegate(ref UITextBox textBox, string data);
        /// <summary>
        /// 多线程访问控件委托，在textBox中显示数据
        /// </summary>
       public static void showDataInTextBox(ref UITextBox textBox, string data)
        {


            if (textBox.InvokeRequired)
            {
                showDataInTextBoxDelegate d = showDataInTextBox;
                textBox.Invoke(d, textBox, data);
            }
            else
            {
                textBox.Text = data;
            }
        }

        
        private delegate void showDataInRichTextBoxDelegate(ref UIRichTextBox textBox, string data);
        /// <summary>
        /// 多线程访问控件委托，在RickTextBox中显示数据
        /// </summary>
        public static void showDataInRichTextBox(ref UIRichTextBox textBox, string data)
        {

            if (textBox.InvokeRequired)
            {
                showDataInRichTextBoxDelegate d = showDataInRichTextBox;
                textBox.Invoke(d, textBox, data);
            }
            else
            {
                textBox.Text = data;
            }
        }

        
        private delegate void changeButtonStatusDelegate(ref Button button, bool status);
        /// <summary>
        /// 多线程访问控件委托，改变button的状态，enabled = true or false
        /// </summary>
        public static void changeButtonStatus(ref Button button, bool status)
        {
            if (button.InvokeRequired)
            {
                changeButtonStatusDelegate d = changeButtonStatus;
                button.Invoke(d, button, status);
            }
            else
            {
                button.Enabled = status;
            }
        }
    }
}