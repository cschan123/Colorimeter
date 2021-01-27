namespace PCClient
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.button_login = new Sunny.UI.UIButton();
            this.label_username = new Sunny.UI.UILabel();
            this.label_password = new Sunny.UI.UILabel();
            this.botton_logout = new Sunny.UI.UIButton();
            this.textBox_userName = new Sunny.UI.UITextBox();
            this.textBox_password = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // button_login
            // 
            this.button_login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_login.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.button_login.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.button_login.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button_login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.button_login.Location = new System.Drawing.Point(43, 181);
            this.button_login.MinimumSize = new System.Drawing.Size(1, 1);
            this.button_login.Name = "button_login";
            this.button_login.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.button_login.Size = new System.Drawing.Size(171, 46);
            this.button_login.Style = Sunny.UI.UIStyle.LightBlue;
            this.button_login.TabIndex = 0;
            this.button_login.Text = "登录";
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // label_username
            // 
            this.label_username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label_username.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label_username.Location = new System.Drawing.Point(39, 50);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(59, 29);
            this.label_username.TabIndex = 1;
            this.label_username.Text = "用户名";
            this.label_username.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_password
            // 
            this.label_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label_password.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_password.Location = new System.Drawing.Point(39, 113);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(47, 29);
            this.label_password.TabIndex = 2;
            this.label_password.Text = "密码";
            this.label_password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // botton_logout
            // 
            this.botton_logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botton_logout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.botton_logout.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.botton_logout.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.botton_logout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.botton_logout.Location = new System.Drawing.Point(223, 181);
            this.botton_logout.MinimumSize = new System.Drawing.Size(1, 1);
            this.botton_logout.Name = "botton_logout";
            this.botton_logout.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.botton_logout.Size = new System.Drawing.Size(176, 46);
            this.botton_logout.Style = Sunny.UI.UIStyle.LightBlue;
            this.botton_logout.TabIndex = 3;
            this.botton_logout.Text = "退出";
            this.botton_logout.Click += new System.EventHandler(this.button_logout_Click);
            // 
            // textBox_userName
            // 
            this.textBox_userName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_userName.FillColor = System.Drawing.Color.White;
            this.textBox_userName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox_userName.Location = new System.Drawing.Point(114, 50);
            this.textBox_userName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_userName.Maximum = 2147483647D;
            this.textBox_userName.Minimum = -2147483648D;
            this.textBox_userName.MinimumSize = new System.Drawing.Size(1, 1);
            this.textBox_userName.Name = "textBox_userName";
            this.textBox_userName.Padding = new System.Windows.Forms.Padding(5);
            this.textBox_userName.Size = new System.Drawing.Size(285, 29);
            this.textBox_userName.TabIndex = 4;
            this.textBox_userName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_userName_KeyPress);
            // 
            // textBox_password
            // 
            this.textBox_password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_password.FillColor = System.Drawing.Color.White;
            this.textBox_password.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox_password.Location = new System.Drawing.Point(114, 113);
            this.textBox_password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_password.Maximum = 2147483647D;
            this.textBox_password.Minimum = -2147483648D;
            this.textBox_password.MinimumSize = new System.Drawing.Size(1, 1);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Padding = new System.Windows.Forms.Padding(5);
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(285, 29);
            this.textBox_password.TabIndex = 5;
            this.textBox_password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_password_KeyPress);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(446, 275);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_userName);
            this.Controls.Add(this.botton_logout);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.button_login);
            this.Name = "LoginForm";
            this.Text = "色差仪检测系统";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIButton button_login;
        private Sunny.UI.UILabel label_username;
        private Sunny.UI.UILabel label_password;
        private Sunny.UI.UIButton botton_logout;
        private Sunny.UI.UITextBox textBox_userName;
        private Sunny.UI.UITextBox textBox_password;

    }
}