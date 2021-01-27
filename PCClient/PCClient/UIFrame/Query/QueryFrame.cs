using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using ColorimeterDB;
using System.IO;
using ColorimeterService.Utils;
//using ZedGraph;
namespace PCClient.UIFrame.Query
{
    public partial class QueryFrame : Form
    {
        public Form aaa;
        public string connStr = ConfigurationManager.ConnectionStrings["ColorimeterDB"].ConnectionString;
        private static ColorimeterDBDataContext DBContext = new ColorimeterDBDataContext();
        
        public QueryFrame()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            
        }


        

        private void QueryFrame_Load(object sender, EventArgs e)
        {

        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            //this.panel3.Controls.Clear();
            aaa = new Form();
            aaa.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            aaa.TopLevel = false;//指示子窗体非顶级窗体
            this.panel1.Controls.Add(aaa);//将子窗体载入panel
            aaa.Show();
            //showchart();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 删除列表的记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_delData_Click(object sender, EventArgs e)
        {

            //MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否确定删除记录", "删除记录", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)//如果点击“确定”按钮
            {
                GetDataAboutChoose("delete");
            }
        }

        /// <summary>
        /// 修改列表数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_UpdateData_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否确定修改记录", "修改记录", messButton);
            if (dr == DialogResult.OK)//如果点击“确定”按钮
            {
                GetDataAboutChoose("update");
            }
        }

        /// <summary>
        /// 导出数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ExportData_Click(object sender, EventArgs e)
        {
            string a = "D:" + "\\导出的数据.xls";
            ExportExcels(a, dataGridView_CommomShow);


        }
        /// <summary>
        /// 查询带钢生产按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_IronQuery_Click(object sender, EventArgs e)
        {
            QueryResult();
        }
        /// <summary>
        /// 导出数据的方法
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="myDGV"></param>
        private void ExportExcels(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            //写入标题
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = myDGV.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//强行销毁
            MessageBox.Show("文件： " + fileName + ".xls 保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// 获取当前列表行信息,点击明细查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CommomShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView_CommomShow.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    try
                    {
                        //string BatchGUID = this.dataGridView_CommomShow.Rows[e.RowIndex].Cells[e.ColumnIndex + 20].Value.ToString();
                        string productTime = this.dataGridView_CommomShow.Rows[e.RowIndex].Cells[2].Value.ToString();
                        string RollNumber = this.dataGridView_CommomShow.Rows[e.RowIndex].Cells[3].Value.ToString();
                        string SubRollNumber = this.dataGridView_CommomShow.Rows[e.RowIndex].Cells[4].Value.ToString();

                        QueryDetailFrame querydetailframe = new QueryDetailFrame(productTime, RollNumber,SubRollNumber);
                        querydetailframe.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }


        /// <summary>
        /// 查询带钢生产的方法
        /// </summary>
        private void QueryResult()
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {

                string strSql = @"SELECT ProductTime,RollNumber,
                                        ColorCode,DeltaL_Mean,DeltaA_Mean,DeltaB_Mean,DeltaE_Mean,DeltaL_MSE,DeltaA_MSE
                                        ,DeltaB_MSE,DeltaE_MSE,flag,GoodNumber,BadNumber,GUID from StatisticsProduction order by  ProductTime desc ";
                //strSql = string.Format(strSql,textBox2.text)
                //创建一个 适配器类。  
                using (SqlDataAdapter adapter = new SqlDataAdapter(strSql, conn))
                {
                    DataTable dt = new DataTable();
                    //把数据库中的数据填充到内存表Dt中。  
                    //填充之前不需要打开数据库连接，Adapter会自动打开连接，并执行sql。  
                    adapter.Fill(dt);

                    //dt.Rows[0][1]  //第一行第一列的值  

                    List<StatisticsProduction> List = new List<StatisticsProduction>();
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        List.Add(new StatisticsProduction()
                        {
                            ProductTime = Convert.ToDateTime(dataRow["ProductTime"].ToString()),
                            //ProductTime = Convert.ToDateTime(ProductTime).ToString("hh:mm:ss"),
                            RollNumber = dataRow["RollNumber"].ToString(),
                            ColorCode = dataRow["ColorCode"].ToString(),
                            DeltaL_Mean = (float)dataRow["DeltaL_Mean"],
                            DeltaA_Mean = (float)dataRow["DeltaA_Mean"],
                            DeltaB_Mean = (float)dataRow["DeltaB_Mean"],
                            DeltaE_Mean = (float)dataRow["DeltaE_Mean"],
                            DeltaL_MSE = (float)dataRow["DeltaL_MSE"],
                            DeltaA_MSE = (float)dataRow["DeltaA_MSE"],
                            DeltaB_MSE = (float)dataRow["DeltaB_MSE"],
                            DeltaE_MSE = (float)dataRow["DeltaE_MSE"],
                            flag = (float)dataRow["flag"],
                            GoodNumber = (int)dataRow["GoodNumber"],
                            BadNumber = (int)dataRow["BadNumber"],
                            GUID = (Guid)dataRow["GUID"],


                        });
                    }
                    int aa = List.Count;
                    int i = 0;
                    int j = 1;
                    foreach (var a in List)
                    {
                        int index = dataGridView_CommomShow.Rows.Add();
                        dataGridView_CommomShow.Rows[i].Cells[0].Value = j;
                        dataGridView_CommomShow.Rows[i].Cells[1].Value = "明细";
                        dataGridView_CommomShow.Rows[i].Cells[2].Value = a.ProductTime;
                        dataGridView_CommomShow.Rows[i].Cells[3].Value = a.RollNumber;
                        dataGridView_CommomShow.Rows[i].Cells[4].Value = a.ColorCode;
                        dataGridView_CommomShow.Rows[i].Cells[5].Value = a.DeltaL_Mean;
                        dataGridView_CommomShow.Rows[i].Cells[6].Value = a.DeltaA_Mean;
                        dataGridView_CommomShow.Rows[i].Cells[7].Value = a.DeltaB_Mean;
                        dataGridView_CommomShow.Rows[i].Cells[8].Value = a.DeltaE_Mean;
                        dataGridView_CommomShow.Rows[i].Cells[9].Value = a.DeltaL_MSE;
                        dataGridView_CommomShow.Rows[i].Cells[10].Value = a.DeltaA_MSE;
                        dataGridView_CommomShow.Rows[i].Cells[11].Value = a.DeltaB_MSE;
                        dataGridView_CommomShow.Rows[i].Cells[12].Value = a.DeltaE_MSE;
                        dataGridView_CommomShow.Rows[i].Cells[13].Value = a.flag;
                        dataGridView_CommomShow.Rows[i].Cells[14].Value = a.BadNumber;
                        dataGridView_CommomShow.Rows[i].Cells[15].Value = a.GoodNumber;
                        dataGridView_CommomShow.Rows[i].Cells[16].Value = a.GUID;
                        i++;
                        j++;


                    }
                }
            }
        }

        /// <summary>
        /// 获取列表单行信息，进行修改或者删除的方法
        /// </summary>
        /// <param name="command"></param>
        private void GetDataAboutChoose(String command)
        {
            try
            {
                //当选中某一项时，自动让该行也被选中
                dataGridView_CommomShow.Rows[dataGridView_CommomShow.CurrentCellAddress.Y].Selected = true;
                StatisticsProduction stap = new StatisticsProduction();
                //去遍历datagridview每一行
                foreach (DataGridViewRow r in dataGridView_CommomShow.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        //获取界面上选中行隐藏的batchGuid
                        //batchGuid = r.Cells[30].Value.ToString();
                        //获取界面上选中行的所有数据
                        //stap.GUID =(Guid)r.Cells[1].Value;
                        stap.ProductTime = (DateTime)r.Cells[2].Value;//生产时间
                        stap.RollNumber = r.Cells[3].Value.ToString();
                        stap.ColorCode = r.Cells[4].Value.ToString();
                        stap.DeltaL_Mean = (float)r.Cells[5].Value;
                        stap.DeltaA_Mean = (float)r.Cells[6].Value;
                        stap.DeltaB_Mean = (float)r.Cells[7].Value;
                        stap.DeltaE_Mean = (float)r.Cells[8].Value;
                        stap.DeltaL_MSE = (float)r.Cells[9].Value;
                        stap.DeltaA_MSE = (float)r.Cells[10].Value;
                        stap.DeltaB_MSE = (float)r.Cells[11].Value;
                        stap.DeltaE_MSE = (float)r.Cells[12].Value;
                        stap.flag = (float)r.Cells[13].Value;
                        stap.GoodNumber = (int)r.Cells[14].Value;
                        stap.BadNumber = (int)r.Cells[15].Value;
                        stap.GUID = (Guid)r.Cells[16].Value;

                        if (command == "delete")
                        {
                            //SqlCommand cmd = new SqlCommand("select * from tb_users where GUID = @guid ", constr);
                            //SqlDataReader data = cmd.ExecuteReader();
                            var result = from u in DBContext.StatisticsProduction where u.GUID == stap.GUID select u;
                            DBContext.StatisticsProduction.DeleteAllOnSubmit(result);
                            DBContext.SubmitChanges();


                            //重新刷新页面

                            QueryResult();
                        }
                        //else if (command == "update")
                        //{
                        //    //弹出修改记录的窗口
                        //    var result = from u in DBContext.RealTimeProduction where u.GUID == stap.GUID select u;
                        //    DBContext.RealTimeProduction.DeleteAllOnSubmit(result);
                        //    DBContext.SubmitChanges();
                        //}
                    }
                }
            }
            catch { }
        }

        
        

        

        

        

        
    }
}
