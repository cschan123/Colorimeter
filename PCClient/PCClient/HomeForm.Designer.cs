namespace PCClient
{
    partial class HomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.TopNav = new System.Windows.Forms.ToolStrip();
            this.tsbCheck = new System.Windows.Forms.ToolStripButton();
            this.tslCheck = new System.Windows.Forms.ToolStripLabel();
            this.tsbQuery = new System.Windows.Forms.ToolStripButton();
            this.tslQuery = new System.Windows.Forms.ToolStripLabel();
            this.tsbManagement = new System.Windows.Forms.ToolStripButton();
            this.tslManagement = new System.Windows.Forms.ToolStripLabel();
            this.tsbzhuxiao = new System.Windows.Forms.ToolStripButton();
            this.tslzhuxiao = new System.Windows.Forms.ToolStripLabel();
            this.tsbQuit = new System.Windows.Forms.ToolStripButton();
            this.tslQuit = new System.Windows.Forms.ToolStripLabel();
            this.tslIntroduction = new System.Windows.Forms.ToolStripLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TopNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox23
            // 
            this.textBox23.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textBox23.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox23.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox23.ForeColor = System.Drawing.Color.SteelBlue;
            this.textBox23.Location = new System.Drawing.Point(1546, 6);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(309, 32);
            this.textBox23.TabIndex = 472;
            this.textBox23.Text = "色差仪检测系统 V1.0";
            // 
            // TopNav
            // 
            this.TopNav.AutoSize = false;
            this.TopNav.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.TopNav.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TopNav.ImageScalingSize = new System.Drawing.Size(35, 35);
            this.TopNav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCheck,
            this.tslCheck,
            this.tsbQuery,
            this.tslQuery,
            this.tsbManagement,
            this.tslManagement,
            this.tsbzhuxiao,
            this.tslzhuxiao,
            this.tsbQuit,
            this.tslQuit,
            this.tslIntroduction});
            this.TopNav.Location = new System.Drawing.Point(0, 0);
            this.TopNav.Name = "TopNav";
            this.TopNav.Padding = new System.Windows.Forms.Padding(1);
            this.TopNav.Size = new System.Drawing.Size(1924, 60);
            this.TopNav.Stretch = true;
            this.TopNav.TabIndex = 473;
            this.TopNav.Text = "toolStrip1";
            this.TopNav.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.TopNav_ItemClicked);
            // 
            // tsbCheck
            // 
            this.tsbCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheck.Image")));
            this.tsbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheck.Name = "tsbCheck";
            this.tsbCheck.RightToLeftAutoMirrorImage = true;
            this.tsbCheck.Size = new System.Drawing.Size(39, 55);
            this.tsbCheck.Text = "toolStripButton1";
            this.tsbCheck.ToolTipText = "tsbCheck";
            this.tsbCheck.Click += new System.EventHandler(this.tsbCheck_Click);
            // 
            // tslCheck
            // 
            this.tslCheck.Margin = new System.Windows.Forms.Padding(0, 1, 50, 2);
            this.tslCheck.Name = "tslCheck";
            this.tslCheck.Size = new System.Drawing.Size(90, 55);
            this.tslCheck.Text = "色差仪检测";
            this.tslCheck.Click += new System.EventHandler(this.tsbCheck_Click);
            // 
            // tsbQuery
            // 
            this.tsbQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbQuery.Image = ((System.Drawing.Image)(resources.GetObject("tsbQuery.Image")));
            this.tsbQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbQuery.Name = "tsbQuery";
            this.tsbQuery.Size = new System.Drawing.Size(39, 55);
            this.tsbQuery.Text = "toolStripButton1";
            this.tsbQuery.Click += new System.EventHandler(this.tslQuery_Click);
            // 
            // tslQuery
            // 
            this.tslQuery.Margin = new System.Windows.Forms.Padding(0, 1, 50, 2);
            this.tslQuery.Name = "tslQuery";
            this.tslQuery.Size = new System.Drawing.Size(74, 55);
            this.tslQuery.Text = "数据查询";
            this.tslQuery.Click += new System.EventHandler(this.tslQuery_Click);
            // 
            // tsbManagement
            // 
            this.tsbManagement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbManagement.Image = ((System.Drawing.Image)(resources.GetObject("tsbManagement.Image")));
            this.tsbManagement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManagement.Name = "tsbManagement";
            this.tsbManagement.Size = new System.Drawing.Size(39, 55);
            this.tsbManagement.Text = "toolStripButton1";
            this.tsbManagement.Click += new System.EventHandler(this.tslManagement_Click);
            // 
            // tslManagement
            // 
            this.tslManagement.Margin = new System.Windows.Forms.Padding(0, 1, 50, 2);
            this.tslManagement.Name = "tslManagement";
            this.tslManagement.Size = new System.Drawing.Size(74, 55);
            this.tslManagement.Text = "参数管理";
            this.tslManagement.Click += new System.EventHandler(this.tslManagement_Click);
            // 
            // tsbzhuxiao
            // 
            this.tsbzhuxiao.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbzhuxiao.Image = ((System.Drawing.Image)(resources.GetObject("tsbzhuxiao.Image")));
            this.tsbzhuxiao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbzhuxiao.Name = "tsbzhuxiao";
            this.tsbzhuxiao.Size = new System.Drawing.Size(39, 55);
            this.tsbzhuxiao.Text = "toolStripButton1";
            this.tsbzhuxiao.Click += new System.EventHandler(this.tslzhuxiao_Click);
            // 
            // tslzhuxiao
            // 
            this.tslzhuxiao.Margin = new System.Windows.Forms.Padding(0, 1, 50, 2);
            this.tslzhuxiao.Name = "tslzhuxiao";
            this.tslzhuxiao.Size = new System.Drawing.Size(42, 55);
            this.tslzhuxiao.Text = "注销";
            this.tslzhuxiao.Click += new System.EventHandler(this.tslzhuxiao_Click);
            // 
            // tsbQuit
            // 
            this.tsbQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbQuit.Image = ((System.Drawing.Image)(resources.GetObject("tsbQuit.Image")));
            this.tsbQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbQuit.Name = "tsbQuit";
            this.tsbQuit.Size = new System.Drawing.Size(39, 55);
            this.tsbQuit.Text = "toolStripButton1";
            this.tsbQuit.Click += new System.EventHandler(this.tsbQuit_Click);
            // 
            // tslQuit
            // 
            this.tslQuit.Name = "tslQuit";
            this.tslQuit.Size = new System.Drawing.Size(42, 55);
            this.tslQuit.Text = "退出";
            this.tslQuit.Click += new System.EventHandler(this.tsbQuit_Click);
            // 
            // tslIntroduction
            // 
            this.tslIntroduction.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslIntroduction.Font = new System.Drawing.Font("Microsoft YaHei UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tslIntroduction.ForeColor = System.Drawing.Color.Teal;
            this.tslIntroduction.Margin = new System.Windows.Forms.Padding(0, 1, 100, 1);
            this.tslIntroduction.Name = "tslIntroduction";
            this.tslIntroduction.Size = new System.Drawing.Size(290, 56);
            this.tslIntroduction.Text = "色差仪检测系统 V1.0";
            this.tslIntroduction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Location = new System.Drawing.Point(0, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 1080);
            this.panel1.TabIndex = 474;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TopNav);
            this.Controls.Add(this.textBox23);
            this.Name = "HomeForm";
            this.Text = "HomeForm";
            this.TopNav.ResumeLayout(false);
            this.TopNav.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.ToolStrip TopNav;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.ToolStripLabel tslCheck;
        private System.Windows.Forms.ToolStripButton tsbQuery;
        private System.Windows.Forms.ToolStripLabel tslQuery;
        private System.Windows.Forms.ToolStripButton tsbManagement;
        private System.Windows.Forms.ToolStripLabel tslManagement;
        private System.Windows.Forms.ToolStripButton tsbzhuxiao;
        private System.Windows.Forms.ToolStripLabel tslzhuxiao;
        private System.Windows.Forms.ToolStripButton tsbQuit;
        private System.Windows.Forms.ToolStripLabel tslQuit;
        private System.Windows.Forms.ToolStripLabel tslIntroduction;
        private System.Windows.Forms.Panel panel1;

    }
}