//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Configuration;
//using System.Data.SqlClient;
//using ColorimeterDB;
using ColorimeterDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCClient.UIFrame.Query
{
    
    public partial class QueryDetailFrame : Form
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ColorimeterDB"].ConnectionString;
         public QueryDetailFrame(String Productime,String RollNumber,String SubRollNumber)
        {
            InitializeComponent();
            
           //{0} and SubRollNumber = @SubRollNumber  and ProductTime =@Productime";
            string strSql = @"SELECT * FROM RealTimeProduction WHERE RollNumber = @RollNumber and SubRollNumber = @SubRollNumber  order by ProductTime desc";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(strSql, conn);
                command.Parameters.AddWithValue("@RollNumber", RollNumber);
                command.Parameters.AddWithValue("@SubRollNumber", SubRollNumber);
                //SqlDataReader data = command.ExecuteReader();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    //把数据库中的数据填充到内存表Dt中。  
                    //填充之前不需要打开数据库连接，Adapter会自动打开连接，并执行sql。  
                    adapter.Fill(dt);

                    //dt.Rows[0][1]  //第一行第一列的值  

                    List<RealTimeProduction> List = new List<RealTimeProduction>();
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        List.Add(new RealTimeProduction()
                        {
                            // ProductTime = dataRow["ProductTime"].ToString(),

                            ProductTime = Convert.ToDateTime(dataRow["ProductTime"].ToString()),
                            //ProductTime = Convert.ToDateTime(ProductTime).ToString("hh:mm:ss"),
                            RollNumber = dataRow["RollNumber"].ToString(),
                            SubRollNumber = dataRow["SubRollNumber"].ToString(),
                            ColorCode = dataRow["ColorCode"].ToString(),
                            ORDWTH = (float)dataRow["ORDWTH"],
                            LengthLocation = (float)dataRow["LengthLocation"],
                            WidthLocation = (float)dataRow["WidthLocation"],
                            RealTimeL = (float)dataRow["RealTimeL"],
                            RealTimeA = (float)dataRow["RealTimeA"],
                            RealTimeB = (float)dataRow["RealTimeB"],
                            RealTimeHeight = (float)dataRow["RealTimeHeight"],
                            StandardL = (float)dataRow["StandardL"],
                            StandardA = (float)dataRow["StandardA"],
                            StandardB = (float)dataRow["StandardB"],
                            DeltaL = (float)dataRow["DeltaL"],
                            DeltaA = (float)dataRow["DeltaA"],
                            DeltaB = (float)dataRow["DeltaB"],
                            DeltaE = (float)dataRow["DeltaE"],
                            Flag = dataRow["flag"].ToString(),
                            DeltaL_Std = (float)dataRow["DeltaL_Std"],
                            DeltaA_Std = (float)dataRow["DeltaA_Std"],
                            DeltaB_Std = (float)dataRow["DeltaB_Std"],
                            DeltaE_Std = (float)dataRow["DeltaE_Std"]

                        });
                    }
                    this.dataGridView_DetailShow.DataSource = List;


                    //把dt的数据转换成List<UserInfo>   
                    //this.dgvUserInfo.DataSource = userList;  //DataGridView控件  
                }

                

            }
            
            
            //command.Parameters.AddWithValue("@SubRollNumber", SubRollNumber);
            //command.Parameters.AddWithValue("@ProductTime", Productime);
                //strSql = string.Format(RollNumber,)
                //创建一个 适配器类。  
           
            
        }

        private void QueryDetailFrame_Load(object sender, EventArgs e)
        {
            //dataGridView_DetailShow.DataSource = QueryData();
        }

        private object QueryData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT * FROM RealTimeProduction WHERE RollNumber = '1' and SubRollNumber = '分卷号1'  and ProductTime < '2020-12-30' ";

                //创建一个 适配器类。  
                using (SqlDataAdapter adapter = new SqlDataAdapter(strSql, conn))
                {
                    DataTable dt = new DataTable();
                    //把数据库中的数据填充到内存表Dt中。  
                    //填充之前不需要打开数据库连接，Adapter会自动打开连接，并执行sql。  
                    adapter.Fill(dt);

                    //dt.Rows[0][1]  //第一行第一列的值  

                    List<RealTimeProduction> List = new List<RealTimeProduction>();
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        List.Add(new RealTimeProduction()
                        {
                            // ProductTime = dataRow["ProductTime"].ToString(),

                            ProductTime = Convert.ToDateTime(dataRow["ProductTime"].ToString()),
                            //ProductTime = Convert.ToDateTime(ProductTime).ToString("hh:mm:ss"),
                            RollNumber = dataRow["RollNumber"].ToString(),
                            SubRollNumber = dataRow["SubRollNumber"].ToString(),
                            ColorCode = dataRow["ColorCode"].ToString(),
                            ORDWTH = (float)dataRow["ORDWTH"],
                            LengthLocation = (float)dataRow["LengthLocation"],
                            WidthLocation = (float)dataRow["WidthLocation"],
                            RealTimeL = (float)dataRow["RealTimeL"],
                            RealTimeA = (float)dataRow["RealTimeA"],
                            RealTimeB = (float)dataRow["RealTimeB"],
                            RealTimeHeight = (float)dataRow["RealTimeHeight"],
                            StandardL = (float)dataRow["StandardL"],
                            StandardA = (float)dataRow["StandardA"],
                            StandardB = (float)dataRow["StandardB"],
                            DeltaL = (float)dataRow["DeltaL"],
                            DeltaA = (float)dataRow["DeltaA"],
                            DeltaB = (float)dataRow["DeltaB"],
                            DeltaE = (float)dataRow["DeltaE"],
                            Flag = dataRow["flag"].ToString(),
                            DeltaL_Std = (float)dataRow["DeltaL_Std"],
                            DeltaA_Std = (float)dataRow["DeltaA_Std"],
                            DeltaB_Std = (float)dataRow["DeltaB_Std"],
                            DeltaE_Std = (float)dataRow["DeltaE_Std"]

                        });
                    }
                    this.dataGridView_DetailShow.DataSource = List;
                    return dataGridView_DetailShow.DataSource;

                    //把dt的数据转换成List<UserInfo>   
                    //this.dgvUserInfo.DataSource = userList;  //DataGridView控件  
                }
            }
        }

        private void dataGridView_DetailShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
