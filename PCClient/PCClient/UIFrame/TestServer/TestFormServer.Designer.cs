namespace PCClient
{
    partial class TestFormServer
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
            this.uiTextBox_LocalIPAddress = new Sunny.UI.UITextBox();
            this.uiButton_GetLocalIPAddress = new Sunny.UI.UIButton();
            this.uiTextBox_Port = new Sunny.UI.UITextBox();
            this.uiButton_openServer = new Sunny.UI.UIButton();
            this.uiRichTextBox_RecvDataMessage = new Sunny.UI.UIRichTextBox();
            this.uiListBox_SendDataMessage = new Sunny.UI.UIListBox();
            this.uiRichTextBox_SendData = new Sunny.UI.UIRichTextBox();
            this.uiButton_GenerateAndSendData = new Sunny.UI.UIButton();
            this.uiButton_RecurSendData = new Sunny.UI.UIButton();
            this.uiButton_StopGenerateAndSendData = new Sunny.UI.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTextBox_LocalIPAddress
            // 
            this.uiTextBox_LocalIPAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_LocalIPAddress.FillColor = System.Drawing.Color.White;
            this.uiTextBox_LocalIPAddress.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiTextBox_LocalIPAddress.Location = new System.Drawing.Point(39, 37);
            this.uiTextBox_LocalIPAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox_LocalIPAddress.Maximum = 2147483647D;
            this.uiTextBox_LocalIPAddress.Minimum = -2147483648D;
            this.uiTextBox_LocalIPAddress.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_LocalIPAddress.Name = "uiTextBox_LocalIPAddress";
            this.uiTextBox_LocalIPAddress.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_LocalIPAddress.Size = new System.Drawing.Size(294, 29);
            this.uiTextBox_LocalIPAddress.TabIndex = 2;
            this.uiTextBox_LocalIPAddress.Text = "本地IP地址";
            // 
            // uiButton_GetLocalIPAddress
            // 
            this.uiButton_GetLocalIPAddress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_GetLocalIPAddress.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton_GetLocalIPAddress.Location = new System.Drawing.Point(340, 37);
            this.uiButton_GetLocalIPAddress.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_GetLocalIPAddress.Name = "uiButton_GetLocalIPAddress";
            this.uiButton_GetLocalIPAddress.Size = new System.Drawing.Size(121, 29);
            this.uiButton_GetLocalIPAddress.TabIndex = 3;
            this.uiButton_GetLocalIPAddress.Text = "获取本地IP";
            this.uiButton_GetLocalIPAddress.Click += new System.EventHandler(this.uiButton_GetLocalIPAddress_Click);
            // 
            // uiTextBox_Port
            // 
            this.uiTextBox_Port.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_Port.DoubleValue = 9999D;
            this.uiTextBox_Port.FillColor = System.Drawing.Color.White;
            this.uiTextBox_Port.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiTextBox_Port.IntValue = 9999;
            this.uiTextBox_Port.Location = new System.Drawing.Point(39, 76);
            this.uiTextBox_Port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox_Port.Maximum = 2147483647D;
            this.uiTextBox_Port.Minimum = -2147483648D;
            this.uiTextBox_Port.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox_Port.Name = "uiTextBox_Port";
            this.uiTextBox_Port.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_Port.Size = new System.Drawing.Size(294, 29);
            this.uiTextBox_Port.TabIndex = 3;
            this.uiTextBox_Port.Text = "9999";
            // 
            // uiButton_openServer
            // 
            this.uiButton_openServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_openServer.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton_openServer.Location = new System.Drawing.Point(340, 76);
            this.uiButton_openServer.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_openServer.Name = "uiButton_openServer";
            this.uiButton_openServer.Size = new System.Drawing.Size(121, 29);
            this.uiButton_openServer.TabIndex = 4;
            this.uiButton_openServer.Text = "启动服务器";
            this.uiButton_openServer.Click += new System.EventHandler(this.uiButton_openServer_Click);
            // 
            // uiRichTextBox_RecvDataMessage
            // 
            this.uiRichTextBox_RecvDataMessage.AutoWordSelection = true;
            this.uiRichTextBox_RecvDataMessage.FillColor = System.Drawing.Color.White;
            this.uiRichTextBox_RecvDataMessage.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRichTextBox_RecvDataMessage.Location = new System.Drawing.Point(39, 132);
            this.uiRichTextBox_RecvDataMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRichTextBox_RecvDataMessage.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRichTextBox_RecvDataMessage.Name = "uiRichTextBox_RecvDataMessage";
            this.uiRichTextBox_RecvDataMessage.Padding = new System.Windows.Forms.Padding(2);
            this.uiRichTextBox_RecvDataMessage.Size = new System.Drawing.Size(422, 509);
            this.uiRichTextBox_RecvDataMessage.Style = Sunny.UI.UIStyle.Custom;
            this.uiRichTextBox_RecvDataMessage.TabIndex = 5;
            this.uiRichTextBox_RecvDataMessage.Text = "接收数据";
            // 
            // uiListBox_SendDataMessage
            // 
            this.uiListBox_SendDataMessage.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiListBox_SendDataMessage.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiListBox_SendDataMessage.Location = new System.Drawing.Point(473, 289);
            this.uiListBox_SendDataMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiListBox_SendDataMessage.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBox_SendDataMessage.Name = "uiListBox_SendDataMessage";
            this.uiListBox_SendDataMessage.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBox_SendDataMessage.Size = new System.Drawing.Size(908, 352);
            this.uiListBox_SendDataMessage.TabIndex = 6;
            this.uiListBox_SendDataMessage.Text = "uiListBox1";
            // 
            // uiRichTextBox_SendData
            // 
            this.uiRichTextBox_SendData.AutoWordSelection = true;
            this.uiRichTextBox_SendData.FillColor = System.Drawing.Color.White;
            this.uiRichTextBox_SendData.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRichTextBox_SendData.Location = new System.Drawing.Point(473, 132);
            this.uiRichTextBox_SendData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRichTextBox_SendData.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRichTextBox_SendData.Name = "uiRichTextBox_SendData";
            this.uiRichTextBox_SendData.Padding = new System.Windows.Forms.Padding(2);
            this.uiRichTextBox_SendData.Size = new System.Drawing.Size(895, 147);
            this.uiRichTextBox_SendData.Style = Sunny.UI.UIStyle.Custom;
            this.uiRichTextBox_SendData.TabIndex = 7;
            // 
            // uiButton_GenerateAndSendData
            // 
            this.uiButton_GenerateAndSendData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_GenerateAndSendData.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton_GenerateAndSendData.Location = new System.Drawing.Point(864, 76);
            this.uiButton_GenerateAndSendData.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_GenerateAndSendData.Name = "uiButton_GenerateAndSendData";
            this.uiButton_GenerateAndSendData.Size = new System.Drawing.Size(153, 27);
            this.uiButton_GenerateAndSendData.TabIndex = 8;
            this.uiButton_GenerateAndSendData.Text = "随机生成报文发送";
            this.uiButton_GenerateAndSendData.Click += new System.EventHandler(this.uiButton_GenerateAndSendData_Click);
            // 
            // uiButton_RecurSendData
            // 
            this.uiButton_RecurSendData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_RecurSendData.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton_RecurSendData.Location = new System.Drawing.Point(502, 77);
            this.uiButton_RecurSendData.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_RecurSendData.Name = "uiButton_RecurSendData";
            this.uiButton_RecurSendData.Size = new System.Drawing.Size(153, 28);
            this.uiButton_RecurSendData.TabIndex = 9;
            this.uiButton_RecurSendData.Text = "定时循环发送";
            this.uiButton_RecurSendData.Click += new System.EventHandler(this.uiButton_RecurSendData_Click);
            // 
            // uiButton_StopGenerateAndSendData
            // 
            this.uiButton_StopGenerateAndSendData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_StopGenerateAndSendData.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton_StopGenerateAndSendData.Location = new System.Drawing.Point(670, 77);
            this.uiButton_StopGenerateAndSendData.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_StopGenerateAndSendData.Name = "uiButton_StopGenerateAndSendData";
            this.uiButton_StopGenerateAndSendData.Size = new System.Drawing.Size(156, 28);
            this.uiButton_StopGenerateAndSendData.TabIndex = 10;
            this.uiButton_StopGenerateAndSendData.Text = "停止";
            this.uiButton_StopGenerateAndSendData.Click += new System.EventHandler(this.uiButton_StopGenerateAndSendData_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Location = new System.Drawing.Point(1037, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 95);
            this.panel1.TabIndex = 11;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(33, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(257, 52);
            this.listBox1.TabIndex = 0;
            // 
            // TestFormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 655);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uiButton_StopGenerateAndSendData);
            this.Controls.Add(this.uiButton_RecurSendData);
            this.Controls.Add(this.uiButton_GenerateAndSendData);
            this.Controls.Add(this.uiRichTextBox_SendData);
            this.Controls.Add(this.uiListBox_SendDataMessage);
            this.Controls.Add(this.uiRichTextBox_RecvDataMessage);
            this.Controls.Add(this.uiButton_openServer);
            this.Controls.Add(this.uiTextBox_Port);
            this.Controls.Add(this.uiButton_GetLocalIPAddress);
            this.Controls.Add(this.uiTextBox_LocalIPAddress);
            this.Name = "TestFormServer";
            this.Text = "TestFormServer";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox uiTextBox_LocalIPAddress;
        private Sunny.UI.UIButton uiButton_GetLocalIPAddress;
        private Sunny.UI.UITextBox uiTextBox_Port;
        private Sunny.UI.UIButton uiButton_openServer;
        private Sunny.UI.UIRichTextBox uiRichTextBox_RecvDataMessage;
        private Sunny.UI.UIListBox uiListBox_SendDataMessage;
        private Sunny.UI.UIRichTextBox uiRichTextBox_SendData;
        private Sunny.UI.UIButton uiButton_GenerateAndSendData;
        private Sunny.UI.UIButton uiButton_RecurSendData;
        private Sunny.UI.UIButton uiButton_StopGenerateAndSendData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox1;
    }
}