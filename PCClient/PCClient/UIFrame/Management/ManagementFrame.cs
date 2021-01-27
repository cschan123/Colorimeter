using ColorimeterDB;
using ColorimeterService.Dao;
using ColorimeterService.Dao.impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCClient.UIFrame.Management
{
    public partial class ManagementFrame : Form
    {
        
        public ManagementFrame()
        {
            InitializeComponent();
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            
            
        }
        private void Btn_AllPataSave_Click(object sender, EventArgs e)
        {
            Parameter para = new Parameter()
            {
                HeadDistance = this.text_HeadDistance.Text,
                HanFengDistance = this.Text_HanFengDIstance.Text,
                OperaPorportion = this.txt_ManaOperDistance.Text,
                DirveDistance = this.txt_ManaDriverDistance.Text,
                //SechayiScanTime = DateTime.Parse(this.text_SechayiScanTime.Text),
                Sechayi_timeLength = this.text_Sechayi_timeLength.Text,
                Sechayi_leftDev = this.text_Sechayi_leftDev.Text,
                Sechayi_RightDev = this.text_Sechayi_RightDev.Text,
                Sechayi_Eloutu = this.text_Sechayi_Eloutu.Text

            };
            ParameterDao dao = new ParameterDaoImpl();
            dao.insert(para);
            MessageBox.Show("参数设置成功");
        }

       

       
    }
}
