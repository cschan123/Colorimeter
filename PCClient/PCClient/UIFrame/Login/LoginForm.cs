using ColorimeterDAO.Dao.impl;
using ColorimeterDAO.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColorimeterDAO;
using ColorimeterDB;

namespace PCClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private bool isEmptyInput(string username, string password)
        {
            bool isEmpty = false;
            //检测输入是否为空
            if (username == "" && password == "")
            {
                isEmpty = true;
                MessageBox.Show("用户名和密码输入为空，请重新输入！");
            }
            else if (username == "" && password != "")
            {
                isEmpty = true;
                MessageBox.Show("用户名输入为空，请重新输入！");
            }
            else if (username != "" && password == "")
            {
                isEmpty = true;
                MessageBox.Show("密码输入为空，请重新输入！");
            }
            return isEmpty;
        }

        /// <summary>
        /// [button_login]的点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_login_Click(object sender, EventArgs e)
        {
            userName = this.textBox_userName.Text;
            password = this.textBox_password.Text;
            if (!isEmptyInput(userName, password))  // 如果用户输入不为空，查询用户信息，确认用户名和密码是否正确
            {
                UserDao dao = null;
                try
                {
                    dao = new UserDaoImpl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数据库连接错误！" + ex.Message);
                }
                User user = dao.findFirstUser(userName, password);
                if (user != null)
                {
                    HomeForm mainForm = new HomeForm();
                    mainForm.Show();
                }
                else
                {
                    int nameCount = dao.countUserName(userName);
                    if (nameCount != 0)
                    {
                        MessageBox.Show("密码错误！请重新输入！");
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误！请重新输入！");
                    }
                }

               // this.Close();  // 关闭LoginForm窗体
            }
        }

        /// <summary>
        /// [button_logout]的点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_logout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// [textBox_password]中按下回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==(char) Keys.Enter)
            {
                button_login_Click(sender, e);
            }
        }

        /// <summary>
        /// [textBox_userName]中按下回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_userName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.textBox_password.Focus();
            }
        }


        public string userName { get; set; }
        public string password { get; set; }

    }
}
