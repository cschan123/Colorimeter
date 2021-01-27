using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PCClient.UIFrame.Check;
using PCClient.UIFrame.Management;
using PCClient.UIFrame.Query;


namespace PCClient
{
    public partial class HomeForm : Form
    {
        

        public Checkframe checkf;                       //色差仪检测界面
        public ManagementFrame managementf;            //参数管理页面
        public QueryFrame queryf;                      //数据查询页面
        
        public HomeForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(panel1_Load);

            //全屏，无边框
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            checkf = new Checkframe();
            checkf.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            checkf.TopLevel = false;//指示子窗体非顶级窗体
            //Form checkf = new Form();
            //checkf.Load += new EventHandler(checkf_Load);
            this.panel1.Controls.Add(checkf);//将子窗体载入panel
            checkf.Show();
            

        }
        private void panel1_Load(object sender, EventArgs e)
        {
          //  checkf = new Checkframe();
            managementf = new ManagementFrame();
            queryf = new QueryFrame();
        }


        

     
        //public String panel_sechayiCheck { get; set; }
        private void tsbCheck_Click(object sender, EventArgs e)
        {

            this.panel1.Controls.Clear();
         //   checkf = new Checkframe();
            checkf.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            checkf.TopLevel = false;//指示子窗体非顶级窗体
            //Form checkf = new Form();
            //checkf.Load += new EventHandler(checkf_Load);
            this.panel1.Controls.Add(checkf);//将子窗体载入panel
            //checkf.Height = 520;
            //checkf.Width = 500;
            //checkf.BackColor = Color.ForestGreen;
            checkf.Show();

        }


        private void tslQuery_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
        //    queryf = new QueryFrame();
            queryf.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            queryf.TopLevel = false;//指示子窗体非顶级窗体
            this.panel1.Controls.Add(queryf);//将子窗体载入panel
            queryf.Show();
            
        }

        private void tslManagement_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
       //     managementf = new ManagementFrame();
            managementf.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            managementf.TopLevel = false;//指示子窗体非顶级窗体
            this.panel1.Controls.Add(managementf);//将子窗体载入panel
            managementf.Show();



            
        }
       

        private void tslzhuxiao_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否确定退出注销", "已注销", messButton);
            if (dr == DialogResult.OK)//如果点击“确定”按钮
            {
                Application.Exit();
            }
        }

        private void tsbQuit_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否确定退出系统", "退出系统", messButton);
            if (dr == DialogResult.OK)//如果点击“确定”按钮
            {
                Application.Exit();
            }
            
        }

        private void panel_sechayiCheck_Paint(object sender, PaintEventArgs e)
        {
            //panel_sechayiCheck.Controls.Add(checkf);
            //panel_sechayiCheck.Controls.Add(managementf);
            //panel_sechayiCheck.Controls.Add(queryf);

        }

        private void TopNav_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

     
       

        

        

        

       



        

    }
}
