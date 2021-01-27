namespace PCClient.UIFrame.Query
{
    partial class QueryDetailFrame
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
            this.dataGridView_DetailShow = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetailShow)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_DetailShow
            // 
            this.dataGridView_DetailShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_DetailShow.Location = new System.Drawing.Point(1, -1);
            this.dataGridView_DetailShow.Name = "dataGridView_DetailShow";
            this.dataGridView_DetailShow.RowTemplate.Height = 23;
            this.dataGridView_DetailShow.Size = new System.Drawing.Size(1482, 801);
            this.dataGridView_DetailShow.TabIndex = 0;
            // 
            // QueryDetailFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 800);
            this.Controls.Add(this.dataGridView_DetailShow);
            this.Name = "QueryDetailFrame";
            this.Text = "QueryDetailFrame";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetailShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_DetailShow;
    }
}