namespace PCClient.UIFrame.Query
{
    partial class QueryFrame
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
            this.uiButton1 = new Sunny.UI.UIButton();
            this.Btn_delData = new Sunny.UI.UIButton();
            this.uiButton3 = new Sunny.UI.UIButton();
            this.Btn_ExportData = new Sunny.UI.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uiNavBar1 = new Sunny.UI.UINavBar();
            this.Btn_IronQuery = new Sunny.UI.UIButton();
            this.dataGridView_CommomShow = new System.Windows.Forms.DataGridView();
            this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiNavBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CommomShow)).BeginInit();
            this.SuspendLayout();
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.FillColor = System.Drawing.Color.Gray;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton1.Location = new System.Drawing.Point(681, 54);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(100, 35);
            this.uiButton1.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton1.TabIndex = 3;
            this.uiButton1.Text = "查询";
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // Btn_delData
            // 
            this.Btn_delData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_delData.FillColor = System.Drawing.Color.Gray;
            this.Btn_delData.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Btn_delData.Location = new System.Drawing.Point(800, 54);
            this.Btn_delData.MinimumSize = new System.Drawing.Size(1, 1);
            this.Btn_delData.Name = "Btn_delData";
            this.Btn_delData.Size = new System.Drawing.Size(100, 35);
            this.Btn_delData.Style = Sunny.UI.UIStyle.Custom;
            this.Btn_delData.TabIndex = 4;
            this.Btn_delData.Text = "删除记录";
            this.Btn_delData.Click += new System.EventHandler(this.Btn_delData_Click);
            // 
            // uiButton3
            // 
            this.uiButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton3.FillColor = System.Drawing.Color.Gray;
            this.uiButton3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton3.Location = new System.Drawing.Point(916, 54);
            this.uiButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton3.Name = "uiButton3";
            this.uiButton3.Size = new System.Drawing.Size(100, 35);
            this.uiButton3.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton3.TabIndex = 5;
            this.uiButton3.Text = "修改记录";
            // 
            // Btn_ExportData
            // 
            this.Btn_ExportData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_ExportData.FillColor = System.Drawing.Color.Gray;
            this.Btn_ExportData.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Btn_ExportData.Location = new System.Drawing.Point(1036, 54);
            this.Btn_ExportData.MinimumSize = new System.Drawing.Size(1, 1);
            this.Btn_ExportData.Name = "Btn_ExportData";
            this.Btn_ExportData.Size = new System.Drawing.Size(100, 35);
            this.Btn_ExportData.Style = Sunny.UI.UIStyle.Custom;
            this.Btn_ExportData.TabIndex = 6;
            this.Btn_ExportData.Text = "导出数据";
            this.Btn_ExportData.Click += new System.EventHandler(this.Btn_ExportData_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(623, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "数据管理";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 2;
            // 
            // uiNavBar1
            // 
            this.uiNavBar1.Controls.Add(this.panel2);
            this.uiNavBar1.Controls.Add(this.panel1);
            this.uiNavBar1.Controls.Add(this.label1);
            this.uiNavBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiNavBar1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiNavBar1.Location = new System.Drawing.Point(0, 0);
            this.uiNavBar1.Name = "uiNavBar1";
            this.uiNavBar1.Size = new System.Drawing.Size(1904, 48);
            this.uiNavBar1.TabIndex = 0;
            this.uiNavBar1.Text = "uiNavBar1";
            // 
            // Btn_IronQuery
            // 
            this.Btn_IronQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_IronQuery.FillColor = System.Drawing.Color.Gray;
            this.Btn_IronQuery.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Btn_IronQuery.Location = new System.Drawing.Point(26, 55);
            this.Btn_IronQuery.MinimumSize = new System.Drawing.Size(1, 1);
            this.Btn_IronQuery.Name = "Btn_IronQuery";
            this.Btn_IronQuery.Size = new System.Drawing.Size(115, 35);
            this.Btn_IronQuery.Style = Sunny.UI.UIStyle.Custom;
            this.Btn_IronQuery.TabIndex = 11;
            this.Btn_IronQuery.Text = "带钢色差查询";
            this.Btn_IronQuery.Click += new System.EventHandler(this.Btn_IronQuery_Click);
            // 
            // dataGridView_CommomShow
            // 
            this.dataGridView_CommomShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_CommomShow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序号,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17});
            this.dataGridView_CommomShow.Location = new System.Drawing.Point(3, 95);
            this.dataGridView_CommomShow.Name = "dataGridView_CommomShow";
            this.dataGridView_CommomShow.RowTemplate.Height = 23;
            this.dataGridView_CommomShow.Size = new System.Drawing.Size(1876, 934);
            this.dataGridView_CommomShow.TabIndex = 12;
            this.dataGridView_CommomShow.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CommomShow_CellContentClick);
            // 
            // 序号
            // 
            this.序号.HeaderText = "序号";
            this.序号.Name = "序号";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "查看详细";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "时间";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "卷号";
            this.Column3.Name = "Column3";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "颜色代码";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "ΔL均值";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Δa均值";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Δb均值";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "ΔE均值";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "ΔL均方差";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Δa均方差";
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Δb均方差";
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "ΔE均方差";
            this.Column13.Name = "Column13";
            // 
            // Column14
            // 
            this.Column14.HeaderText = "标志位";
            this.Column14.Name = "Column14";
            // 
            // Column15
            // 
            this.Column15.HeaderText = "合格数量";
            this.Column15.Name = "Column15";
            // 
            // Column16
            // 
            this.Column16.HeaderText = "不合格数量";
            this.Column16.Name = "Column16";
            // 
            // Column17
            // 
            this.Column17.HeaderText = "GUID";
            this.Column17.Name = "Column17";
            // 
            // QueryFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.dataGridView_CommomShow);
            this.Controls.Add(this.Btn_IronQuery);
            this.Controls.Add(this.Btn_ExportData);
            this.Controls.Add(this.uiButton3);
            this.Controls.Add(this.Btn_delData);
            this.Controls.Add(this.uiButton1);
            this.Controls.Add(this.uiNavBar1);
            this.Name = "QueryFrame";
            this.Text = "QueryFrame";
            this.Load += new System.EventHandler(this.QueryFrame_Load);
            this.uiNavBar1.ResumeLayout(false);
            this.uiNavBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CommomShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UIButton Btn_delData;
        private Sunny.UI.UIButton uiButton3;
        private Sunny.UI.UIButton Btn_ExportData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Sunny.UI.UINavBar uiNavBar1;
        private Sunny.UI.UIButton Btn_IronQuery;
        private System.Windows.Forms.DataGridView dataGridView_CommomShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序号;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;

    }
}