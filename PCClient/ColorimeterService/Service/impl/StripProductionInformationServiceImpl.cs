using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorimeterService.Utils;
using Sunny.UI;
using System.Threading;
using PlcComm.DataExchange;
using ColorimeterDAO.WinDomain;
using ColorimeterDAO.Dao.impl;
using ColorimeterDAO;
using ColorimeterDB;
using ColorimeterService.Dao.impl;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace ColorimeterService.Service.impl
{
    public class StripProductionInformationServiceImpl : StripProductionInformationService
    {
        // 服务调用的UI控件的引用
        public UITextBox textBox_CurrentL;
        public UITextBox textBox_CurrentA;
        public UITextBox textBox_CurrentB;
        public UITextBox text_Height;
        public Label label_ConnectStatus;
        public WebBrowser wb_leftchart;
        public WebBrowser wb_fullchart;

        // 计算并且判断timer
        private System.Timers.Timer currentJudgeTimer;
        // 计算并且判断线程间隔时间ms
        private const int JUDGE_ELAPSE = 1000;
        // 读PLC线程间隔时间ms
        private const int READPLC_ELAPSE = 20;
        private const int BIND_ELAPSE = 200;

        // 是否成功在本地放行标准表中查询到colorcode
        public volatile bool getDeltaAndCFlag;
        // 检测不合格计数
        private int notGoodCount;
        // 检测计数
        private int checkCount;

        // 窗体实体单例，与前端控件数据绑定
        StripProductionInformationDomain spiDomain = StripProductionInformationDomain.Instance;
        ColorStandardDomain csDomain = ColorStandardDomain.Instance;

        // 封装的ConcurrentQueue，内部保存解析好的l,a,b,h数据
        ConcurrentQueue<Transporter.ParsedData> queue = ColorimeterControlServiceImpl.parsedDataQueue;

        // 计算得到的Δlab的queue
        Queue<DeltaData> deltaDataQueue = new Queue<DeltaData>();

        RealTimeProductionDaoImpl dao = new RealTimeProductionDaoImpl();

        // 放行标准数据ΔL* Δa* Δb* L_C a_C b_C
        public struct deltaAndC
        {
            public string deltaA_std;
            public string deltaB_std;
            public string deltaL_std;
            public string deltaE_std;
            public string L_C;
            public string a_C;
            public string b_C;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="textBox_CurrentL">控件的引用 当前值L</param>
        /// <param name="textBox_CurrentA">当前值A</param>
        /// <param name="textBox_CurrentB">当前值B</param>
        public StripProductionInformationServiceImpl(
            ref UITextBox textBox_CurrentL,
            ref UITextBox textBox_CurrentA,
            ref UITextBox textBox_CurrentB,
            ref UITextBox text_Height,
            ref Label label_ConnectStatus,
            ref WebBrowser wb_leftchart,
            ref WebBrowser wb_fullchart
             )
        {
            this.textBox_CurrentA = textBox_CurrentA;
            this.textBox_CurrentB = textBox_CurrentB;
            this.textBox_CurrentL = textBox_CurrentL;
            this.text_Height = text_Height;
            this.label_ConnectStatus = label_ConnectStatus;
            this.wb_leftchart = wb_leftchart;
            this.wb_fullchart = wb_fullchart;
        }

        /// <summary>
        /// 接口方法
        /// 
        /// 计算并判断是否合格
        /// △L=L实时(色差仪检测)-L标（标准值）
        /// △a=a实时(色差仪检测)-a标（标准值）
        /// △b=b实时(色差仪检测)-b标（标准值）
        /// ΔE = sqrt(ΔL**2 + Δa**2 + Δb**2)
        /// 判断当前检测数据是否合格，将检测结果插入实时检测数据库(每一个检测值)。
        /// </summary>
        public void currentJudge()
        {
            Thread th = new Thread(m);
            th.Start();
        }
        private void m()
        {
            for (; ; )
            {
                if (!ColorimeterControlServiceImpl.threadRecvDataFlag)  // 如果色差仪线程已关闭(flag=flase)，break
                {
                    return;
                }

                currentJudgeThreadStart();
                Thread.CurrentThread.Join(JUDGE_ELAPSE);
            }
        }

        public AbstractColorStandard absColorStd { get; set; }
        float l_star;
        float a_star;
        float b_star;

        public delegate void getStandardColorDelegate();
        /// <summary>
        /// TODO：得到标准颜色值
        /// </summary>
        public void getStandardColor()
        {
            /// TODO：获取标准色 l* a* b*, 应该是个异步方法
            if (absColorStd == null)
            {
                return;
            }
            // 如果flag == false，执行检测 
            else if (absColorStd.flag == false)
            {
                if (spiDomain.colorCode == null || "正在查询colorcode".Equals(spiDomain.colorCode))
                {
                    return;
                }
                else if (!absColorStd.process(spiDomain.colorCode, out l_star, out a_star, out b_star))
                {
                    return;
                }
            }
        }
        

        /// <summary>
        /// currentJudge()内部开启的线程ThreadStart
        /// </summary>
        private void currentJudgeThreadStart()
        {

            if (!ColorimeterControlServiceImpl.threadRecvDataFlag)  // 如果线程已关闭(flag=flase)，break
            {
                return;
            }

            if (!queue.IsEmpty)
            {
                // 队列中的lab
                Transporter.ParsedData currentParsedData = queue.ElementAt(queue.Count - 1);
                // 获取标准颜色值 std l a b
                getStandardColor();

                if (absColorStd == null)
                {
                    return;
                }
                csDomain.std_L = l_star.ToString();
                csDomain.std_a = a_star.ToString();
                csDomain.std_b = b_star.ToString();
                spiDomain.std_L = l_star.ToString();
                spiDomain.std_a = a_star.ToString();
                spiDomain.std_b = b_star.ToString();

                /// ---------------计算deltaL deltaA deltaB deltaE------------------------///
                float deltaL_f = currentParsedData.l - l_star;
                float deltaA_f = currentParsedData.a - a_star;
                float deltaB_f = currentParsedData.b - b_star;
                // 绑定到domain
                spiDomain.deltaL = Math.Round(deltaL_f, 2).ToString();
                spiDomain.deltaA = Math.Round(deltaA_f, 2).ToString();
                spiDomain.deltaB = Math.Round(deltaB_f, 2).ToString();
                spiDomain.deltaE = Math.Round(Math.Sqrt(
                    Math.Pow(deltaL_f, 2) +
                    Math.Pow(deltaA_f, 2) +
                    Math.Pow(deltaB_f, 2)), 2).ToString();

                // 加入delta数据队列
                deltaDataQueue.Enqueue(new DeltaData()
                {
                    deltaL = double.Parse(spiDomain.deltaL),
                    deltaA = double.Parse(spiDomain.deltaA),
                    deltaB = double.Parse(spiDomain.deltaB),
                    deltaE = double.Parse(spiDomain.deltaE)
                });
                // 如果ERP查询到colorcode并且在本地放行标准表中有colorcode对应数据delta l a b e
                if (spiDomain.colorCode != "正在查询colorcode" && !string.IsNullOrEmpty(spiDomain.colorCode) && getDeltaAndCFlag)
                {

                    InvkeMethod(spiDomain.deltaL, spiDomain.deltaA, spiDomain.deltaB, spiDomain.deltaE, spiDomain.length);
                    insertToDB(); // 存入实时数据表,TODO 数据库汇中卷取长度间隔相邻小于1米的值只保留1个。
                    // 将检测和相关有效值写入PLC
                  //  writeEffectiveValueInPlc();
                }
            }
        }



        //ccs 2020-1-8 添加, 柱状体显示
        private delegate void InvkeMethodDelegate(String data_L, String data_a, String data_b, String data_E, String changdu);
        private void InvkeMethod(String data_L, String data_a, String data_b, String data_E, String changdu)
        {
            if (wb_fullchart.InvokeRequired)
            {
                InvkeMethodDelegate d = InvkeMethod;
                wb_fullchart.Invoke(d, data_L, data_a, data_b, data_E, changdu);
            }
            else
            {

                if (wb_fullchart.Document != null)
                {
                    Object[] objArray = new Object[5];
                    objArray[0] = (Object)data_L;
                    objArray[1] = (Object)data_a;
                    objArray[2] = (Object)data_b;
                    objArray[3] = (Object)data_E;
                    objArray[4] = (Object)changdu;

                    wb_fullchart.Document.InvokeScript("accpetdata", objArray);
                }
            }
        }


        /// <summary>
        /// 将实时检测数据 插入数据库
        /// </summary>
        /// <param name="task"></param>
        private void insertToDB()
        {
            dao.insert(generateRealTimeProductionMessage());
            
        }

        private void writeEffectiveValueInPlc()
        {
            DataExchange.CoilNumber_15 = spiDomain.rollNumber;
            DataExchange.CoilSplitNumber_15 = spiDomain.subRollNumber;
            DataExchange.ColorCode_15 = spiDomain.colorCode;
            DataExchange.Width_15 = float.Parse(spiDomain.width);
            DataExchange.Thickness_15 = float.Parse(spiDomain.thickness);

            DataExchange.L_15 = float.Parse(this.textBox_CurrentL.Text);
            DataExchange.a_15 = float.Parse(this.textBox_CurrentA.Text);
            DataExchange.b_15 = float.Parse(this.textBox_CurrentB.Text);
            DataExchange.Altitude_15 = float.Parse(spiDomain.broadwise);
            DataExchange.L_STD_15 = float.Parse(spiDomain.std_L);
            DataExchange.a_STD_15 = float.Parse(spiDomain.std_a);
            DataExchange.b_STD_15 = float.Parse(spiDomain.std_b);

            DataExchange.StandardSrcMode_15 = absColorStd.standardSrcMode;// 当前选择的模式 1，2，3，4
            DataExchange.DeltaL_15 = float.Parse(spiDomain.deltaL);
            DataExchange.DeltaA_15 = float.Parse(spiDomain.deltaA);
            DataExchange.DeltaB_15 = float.Parse(spiDomain.deltaB);
            DataExchange.DeltaE_15 = float.Parse(spiDomain.deltaE);
            //     DataExchange.DeltaEMax_15 = ; // ΔE漏涂阈值，该值在参数页面设置
            DataExchange.DeltaLStd_15 = float.Parse(csDomain.deltaL_std);
            DataExchange.DeltaAStd_15 = float.Parse(csDomain.deltaA_std);
            DataExchange.DeltaBStd_15 = float.Parse(csDomain.deltaB_std);
            DataExchange.DeltaEStd_15 = float.Parse(csDomain.deltaE_std);
            //   DataExchange.InspectionCtrl_15 = ; // 该值发生变化时，给色差仪发送不同的命令
            //   DataExchange.SetPosition_15 = ; // 选择0，1，2，3对应人工位，进行模式选择后，写一次
            DataExchange.WritePlc1500();

        }


        /// <summary>
        /// 接口方法
        /// 开启线程初始化连接PLC，并且循环读PLC属性，放入实体类中(实体类与控件textBox数据绑定了)
        /// </summary>
        public void initConnectPlc()
        {
            initPlcConnectionDelegate d = new initPlcConnectionDelegate(initPlcConnection);
            IAsyncResult result = d.BeginInvoke(null, null);
            while (result.IsCompleted == false)
            {
                DelegateFactory.changeDataInLabel(ref this.label_ConnectStatus, "正在连接");
                DelegateFactory.changeColorInLable(ref this.label_ConnectStatus, Color.Yellow);
            }
            d.EndInvoke(result);
            DelegateFactory.changeDataInLabel(ref this.label_ConnectStatus, "已连接");
            DelegateFactory.changeColorInLable(ref this.label_ConnectStatus, Color.Green);
        }


        private delegate void initPlcConnectionDelegate();
        // 初始化PLC连接
        private void initPlcConnection()
        {
            try
            {
                DataExchange.Init();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 读取plc数据
        public void readPLC()
        {
            if (!ColorimeterControlServiceImpl.threadRecvDataFlag)  // 如果线程已关闭(flag=flase)，break
            {
                DelegateFactory.changeDataInLabel(ref this.label_ConnectStatus, "已断开");
                DelegateFactory.changeColorInLable(ref this.label_ConnectStatus, Color.Red);
                return;
            }
            if (DataExchange.Ready == 1)
            {
                transferStart();
                // 将实时从400PLC读取的值写入1500PLC
             //   writeRealTimeValueInPlc();
                
            }
            
            // 线程阻塞1s，等待transferStart()中执行读写操作
            Thread.CurrentThread.Join(1000);
            

            // 绑定数据到变量，并且查询数据库中的color code和放行标准
            Thread th = new Thread(new ThreadStart(bind));
            th.Start();
        }
        private void bind()
        {
            for (; ; )
            {
                bindData();
                Thread.CurrentThread.Join(BIND_ELAPSE);
            }
        }

        // ================================= 2021-01-23  修改 ============================================================
        // 读写PLC，开启5个读线程和一个写线程。
        private volatile int run;
        private void transferStart()
        {
            if (DataExchange.initReady > 0)
            {
                run = 1;
                var t1 = new Thread(readPlc_2_3);
                t1.IsBackground = true;
                t1.Start();

                var t2 = new Thread(readPlc_2_4);
                t2.IsBackground = true;
                t2.Start();

                var t3 = new Thread(readPlc_3_3);
                t3.IsBackground = true;
                t3.Start();

                var t4 = new Thread(readPlc_152);
                t4.IsBackground = true;
                t4.Start();

                var t5 = new Thread(readPlc_key);
                t5.IsBackground = true;
                t5.Start();

                var t6 = new Thread(writePlc_1500);
                t6.IsBackground = true;
                t6.Start();
            }
        }

        private void writePlc_1500()
        {
            for (; ; )
            {
                if (run == 0) { return; }
                DataExchange.RecoilLength_15 = DataExchange.RecoilLength;
                DataExchange.No6Length_15 = DataExchange.No6Length;
                DataExchange.No5Length_15 = DataExchange.No5Length;
                DataExchange.ExitSectionSpeed_15 = DataExchange.ExitSectionSpeed;
                DataExchange.ProcessSectionSpeed_15 = DataExchange.ProcessSectionSpeed;

                DataExchange.CoilNumber_15 = DataExchange.CoilNumber;
                DataExchange.CoilSplitNumber_15 = DataExchange.CoilSplitNumber;

                DataExchange.WritePlc1500();
                Thread.CurrentThread.Join(READPLC_ELAPSE); // 暂停20毫秒
            }
        }

        private void readPlc_key()
        {
            for (; ; )
            {
                if (run == 0) { return; }
                DataExchange.ReadPlcKey();
                Thread.CurrentThread.Join(READPLC_ELAPSE); // 暂停20毫秒
            }
        }

        private void readPlc_152()
        {
            for (; ; )
            {
                if (run == 0) { return; }
                DataExchange.ReadPlc152();
                Thread.CurrentThread.Join(READPLC_ELAPSE); // 暂停20毫秒
            }
        }

        private void readPlc_3_3()
        {
            for (; ; )
            {
                if (run == 0) { return; }
                DataExchange.ReadPlc3_3();
                Thread.CurrentThread.Join(READPLC_ELAPSE); // 暂停20毫秒
            }
        }

        private void readPlc_2_4()
        {
            for (; ; )
            {
                if (run == 0) { return; }
                DataExchange.ReadPlc2_4();
                Thread.CurrentThread.Join(READPLC_ELAPSE); // 暂停20毫秒
            }
        }

        private void readPlc_2_3()
        {
            for (; ; )
            {
                if (run == 0) { return; }
                DataExchange.ReadPlc2_3();
                Thread.CurrentThread.Join(READPLC_ELAPSE); // 暂停20毫秒
            }
        }
        // ======================================================================================


        /// <summary>
        /// 将plc的值写入实时数据库
        /// </summary>
        private void writeRealTimeValueInPlc()
        {
            DataExchange.RecoilLength_15 = DataExchange.RecoilLength;
            DataExchange.No6Length_15 = DataExchange.No6Length;
            DataExchange.No5Length_15 = DataExchange.No5Length;
            DataExchange.ExitSectionSpeed_15 = DataExchange.ExitSectionSpeed;
            DataExchange.ProcessSectionSpeed_15 = DataExchange.ProcessSectionSpeed;
            DataExchange.TransTo1500();
        }

        /// <summary>
        /// 插入分卷统计数据表
        /// </summary>
        private void insertToStatisDB()
        {
            StatisticsProductionDaoImpl dao = new StatisticsProductionDaoImpl();
            dao.insert(generateStatisticsProductionMessage());
        }

        /// <summary>
        /// 绑定数据方法，如果 卷号 rollNumber 改变了，则异步查询数据库，得到 颜色代码 colorCode
        /// </summary>
        private void bindData()
        {
            spiDomain.rollEndProductTime = DateTime.Now.ToString();
            spiDomain.length = Math.Round(DataExchange.RecoilLength / 1000, 0).ToString();

            spiDomain.subRollNumber = DataExchange.CoilSplitNumber;
            spiDomain.broadwise =Math.Round(DataExchange.Position, 0).ToString();

            // 伺服故障代码
            csDomain.servoErrorCode = DataExchange.MotAlarmWord.ToString();
            csDomain.servoWarningCode = DataExchange.MotErrorWord.ToString();
            // 右侧色差仪滑动杆处数据
            csDomain.position = DataExchange.Position.ToString();
            csDomain.positionRef = DataExchange.PositionRef.ToString();
            csDomain.technicSpeed = DataExchange.ProcessSectionSpeed.ToString();
            csDomain.exportSpeed = DataExchange.ExitSectionSpeed.ToString();
            csDomain.rollLength = spiDomain.length;
            csDomain.hanFeng5 = Math.Round(DataExchange.No5Length / 1000, 0).ToString();
            csDomain.hanFeng6 = Math.Round(DataExchange.No6Length / 1000, 0).ToString();

 
            // 解析卷号
            StringBuilder sb = new StringBuilder();
            string coilNumber = DataExchange.CoilNumber;
            string num = "0123456789";
            bool flag = false;
            for (int i = 0; i < coilNumber.Length; i++)
            {
                if (num.Contains(coilNumber[i]))
                {
                    flag = true;
                    sb.Append(coilNumber[i]);
                }
                else if (flag == true)
                {
                    break;
                }
            }

            string rollNumber = sb.ToString();
            //如果 卷号 rollNumber 改变
            if (!string.IsNullOrEmpty(rollNumber) && spiDomain.ifRollNumberChanged(rollNumber))
            {
                spiDomain.rollStartProductTime = DateTime.Now.ToString();

                if (!spiDomain.notfirstChangedRollNumber)
                {
                    spiDomain.notfirstChangedRollNumber = true;
                }
                else
                {
                    // 卷号改变了，将改卷信息保存入数据库。
                    // 换卷，进行重新检测标准值
                    if (absColorStd != null && absColorStd.standardSrcMode == 3 && absColorStd.flag == true)
                    {
                        absColorStd.flag = false;
                    }

                    insertToStatisDB(); // 存入分卷统计数据表

                    // 清空队列数据
                    deltaDataQueue.Clear();
                }

                //  绑定实体类属性 为 DataExchange中的属性
                spiDomain.rollNumber = rollNumber;

                string colorCode = string.Empty;
                string colorName = string.Empty;
                // 连接远程ERP数据库，查询colorCode，Thick厚度和Width宽度
                getColorCodeAndThickAndWeightDelegate d = new getColorCodeAndThickAndWeightDelegate(getColorCodeAndThickAndWeight);
                IAsyncResult result = d.BeginInvoke(out colorCode, rollNumber, null, null);
                while (result.IsCompleted == false)
                {
                    spiDomain.colorCode = "正在查询colorcode"; //TODO，异步干啥呢？
                }
                d.EndInvoke(out colorCode, result);
                spiDomain.colorCode = colorCode;
                
                // 查询本地放行标准表 得到标准值l a b e
                deltaAndC deltaAndCStruct = getDeltaAndC(colorCode, out colorName);
                if (deltaAndCStruct.deltaL_std == null)
                {
                    MessageBox.Show("放行标准表中无对应颜色代码！");
                    getDeltaAndCFlag = false;
                }
                else
                {
                    getDeltaAndCFlag = true;
                }
                spiDomain.colorName = colorName.Trim();

                csDomain.deltaL_std = deltaAndCStruct.deltaL_std;
                csDomain.deltaA_std = deltaAndCStruct.deltaA_std;
                csDomain.deltaB_std = deltaAndCStruct.deltaB_std;
                csDomain.deltaE_std = deltaAndCStruct.deltaE_std;

                csDomain.L_C = deltaAndCStruct.L_C;
                csDomain.a_C = deltaAndCStruct.a_C;
                csDomain.b_C = deltaAndCStruct.b_C;
            }
        }
        class ERP
        {
            public string COLDCOILNO { get; set; }//卷号
            public string COILSEQ { get; set; } //分卷号
            public string ORDTHK { get; set; }//厚度
            public string ORDWTH { get; set; }//宽度
            public string COLOR_UP_CD { get; set; }//颜色代码

        }

        private delegate void getColorCodeAndThickAndWeightDelegate(out string colorCode, string coldCoilNo);
        /// <summary>
        /// 异步委托方法，根据coldCoilNo卷号 查询数据库 得到colorCode颜色代码和Thick厚度和Width宽度
        /// </summary>
        /// <param name="colorCode">out 颜色代码</param>
        /// <param name="coldCoilNo">卷号</param>
        public void getColorCodeAndThickAndWeight(out string colorCode, string coldCoilNo)
        {
            string strCon = "server=10.0.1.12" + ";Database=LEVEL3" + ";Uid=sa" + ";Pwd=bule71" + ";Connection Timeout = 5;";
            SqlConnection sqlcon = new SqlConnection(strCon);
            sqlcon.Open();
            SqlDataAdapter da = new SqlDataAdapter("select COLOR_UP_CD,COLDCOILNO,COILSEQ,ORDWTH,ORDTHK from PDI where COLDCOILNO=" + "'" + coldCoilNo + "'", sqlcon);
            DataTable dt = new DataTable();//填充DataTable
            da.Fill(dt);
            //da.Fill(new DataSet(),"s");
            var row = dt.Rows[0];
            ERP obj = new ERP()
            {
                COLOR_UP_CD = row["COLOR_UP_CD"].ToString().Trim(),
                COILSEQ = row["COILSEQ"].ToString().Trim(),
                ORDTHK = row["ORDTHK"].ToString().Trim(),
                ORDWTH = row["ORDWTH"].ToString().Trim(),
            };
            colorCode = obj.COLOR_UP_CD;
            spiDomain.colorCode = obj.COLOR_UP_CD; 
            spiDomain.subRollNumber = obj.COILSEQ;
            spiDomain.width = obj.ORDWTH;
            spiDomain.thickness = obj.ORDTHK;
        }


        /// <summary>
        /// 根据颜色代码，查询数据库，得到Delta_L delta_A delta_B 和L_C a_C b_C，封装为结构体deltaAndC
        /// </summary>
        /// <param name="colorCode"></param>
        /// <returns></returns>
        public deltaAndC getDeltaAndC(string colorCode, out string colorName)
        {
            PassStdDaoImpl dao = new PassStdDaoImpl();
            PassStd ps = dao.findFirstPassStd(colorCode);
            if (ps == null)
            {
                colorName = string.Empty;
                return new deltaAndC();
            }
            colorName = ps.color_name;
            return new deltaAndC
            {
                deltaA_std = ps.delt_a_std.ToString(),
                deltaB_std = ps.delt_b_std.ToString(),
                deltaE_std = ps.delt_E_std.ToString(),
                deltaL_std = ps.delt_L_std.ToString(),
                L_C = ps.L_C.ToString(),
                a_C = ps.a_C.ToString(),
                b_C = ps.b_C.ToString()
            };
        }





        /// <summary>
        /// 判断是否合格，不合格计数+1
        /// 判断检测数据是否 合格，两个检测标准，同时满足。
        /// 1.根据色差放行标准值判定。
        /// 2.根据上下位置颜色检测偏差值，判定。
        /// </summary>
        /// <returns>是否合格</returns>
        private void judgeIfGood(
            out bool firstJudgeL,
            out bool firstJudgeA,
            out bool firstJudgeB,
            out bool firstJudgeE
            //out bool secondJudgeL,
            //out bool secondJudgeA,
            //out bool secondJudgeB,
            //out bool secondJudgeE
            )
        {
            // 第二种检测，根据上下位置颜色检测
            //   secondJudgeIfWarning(out secondJudgeL, out secondJudgeA, out secondJudgeB, out secondJudgeE);
            // 第一种检测，根据色差放行标准值判断
            firstJudgeL = Math.Abs(spiDomain.deltaL.ToDecimal()) - Math.Abs(csDomain.deltaL_std.ToDecimal()) <= 0 ? true : false;
            firstJudgeA = Math.Abs(spiDomain.deltaA.ToDecimal()) - Math.Abs(csDomain.deltaA_std.ToDecimal()) <= 0 ? true : false;
            firstJudgeB = Math.Abs(spiDomain.deltaB.ToDecimal()) - Math.Abs(csDomain.deltaB_std.ToDecimal()) <= 0 ? true : false;
            firstJudgeE = Math.Abs(spiDomain.deltaE.ToDecimal()) - Math.Abs(csDomain.deltaE_std.ToDecimal()) <= 0 ? true : false;
            if (false == (firstJudgeL && firstJudgeA && firstJudgeB && firstJudgeE
                // && secondJudgeL && secondJudgeA && secondJudgeB && secondJudgeE
                        ))
            {
                spiDomain.ifGood = false;
                spiDomain.fillColor = Color.Red;
                spiDomain.fillText = "不合格";
                spiDomain.notGoodCount = (notGoodCount++).ToString();

            }
            else
            {
                spiDomain.ifGood = true;
                spiDomain.fillText = "合格";
                spiDomain.fillColor = Color.Blue;
            }
            spiDomain.checkCount = (checkCount++).ToString();

        }

        // 位置3和4处的l，a，b，e
        float index3_l = float.NaN;
        float index3_a = float.NaN;
        float index3_b = float.NaN;
        float index3_e = float.NaN;

        float index4_l = float.NaN;
        float index4_a = float.NaN;
        float index4_b = float.NaN;
        float index4_e = float.NaN;


        /// <summary>
        /// 判断检测数据是否 合格，两个检测标准，同时满足。
        /// 2.根据上下位置颜色检测偏差值，判定。
        /// </summary>
        /// <returns>Δ(l,a,b,e)是否报警</returns>
        private void secondJudgeIfWarning(
            out bool secondJudgeL,
            out bool secondJudgeA,
            out bool secondJudgeB,
            out bool secondJudgeE)
        {
            // 如果当前没有位置4的值，说明第一次处于1，2，3位置，直接返回，没有第二种检测。
            if (float.IsNaN(index3_l))
            {
                secondJudgeL = secondJudgeA = secondJudgeB = secondJudgeE = true;
                return;
            }

            Transporter.ParsedData data = queue.ElementAt(queue.Count - 1);
            float height = data.height;
            float current_l = data.l;
            float current_a = data.a;
            float current_b = data.b;
            float current_e = (float)Math.Sqrt(Math.Pow((double)current_a, 2) +
                    Math.Pow((double)current_b, 2) + Math.Pow((double)current_l, 2));

            secondJudgeL = secondJudgeA = secondJudgeB = secondJudgeE = true; // 只是为了编译通过，后面一定会更改值

            if (height.Equals(300f))
            {
                index3_a = current_a;
                index3_b = current_b;
                index3_l = current_l;
                index3_e = (float)Math.Sqrt(Math.Pow((double)index3_a, 2) +
                    Math.Pow((double)index3_b, 2) + Math.Pow((double)index3_l, 2));
            }
            else if (height.Equals(400f))
            {
                index4_a = current_a;
                index4_b = current_b;
                index4_l = current_l;
                index4_e = (float)Math.Sqrt(Math.Pow((double)index4_a, 2) +
                    Math.Pow((double)index4_b, 2) + Math.Pow((double)index4_l, 2));
            }
            if (height.Equals(300f) || height.Equals(200f) || height.Equals(100f))
            {
                secondJudgeL = Math.Abs((decimal)(current_l - index3_l)) - Math.Abs(csDomain.deltaL_std.ToDecimal()) >= 0 ? true : false;
                secondJudgeA = Math.Abs((decimal)(current_a - index3_a)) - Math.Abs(csDomain.deltaA_std.ToDecimal()) >= 0 ? true : false;
                secondJudgeB = Math.Abs((decimal)(current_b - index3_b)) - Math.Abs(csDomain.deltaB_std.ToDecimal()) >= 0 ? true : false;
                secondJudgeE = Math.Abs((decimal)(current_e - index3_e)) - Math.Abs(csDomain.deltaE_std.ToDecimal()) >= 0 ? true : false;
            }
            else if (height.Equals(600f) || height.Equals(500f) || height.Equals(400f))
            {
                secondJudgeL = Math.Abs((decimal)(current_l - index4_l)) - Math.Abs(csDomain.deltaL_std.ToDecimal()) >= 0 ? true : false;
                secondJudgeA = Math.Abs((decimal)(current_a - index4_a)) - Math.Abs(csDomain.deltaA_std.ToDecimal()) >= 0 ? true : false;
                secondJudgeB = Math.Abs((decimal)(current_b - index4_b)) - Math.Abs(csDomain.deltaB_std.ToDecimal()) >= 0 ? true : false;
                secondJudgeE = Math.Abs((decimal)(current_e - index4_e)) - Math.Abs(csDomain.deltaE_std.ToDecimal()) >= 0 ? true : false;
            }

        }


        // 创建 实时生产数据对象
        public RealTimeProduction generateRealTimeProductionMessage()
        {
            bool firstJudgeL, firstJudgeA, firstJudgeB, firstJudgeE;
            bool secondJudgeL, secondJudgeA, secondJudgeB, secondJudgeE;
            judgeIfGood(out firstJudgeL, out firstJudgeA, out  firstJudgeB, out firstJudgeE
                // out secondJudgeL, out secondJudgeA, out secondJudgeB, out secondJudgeE
                ); // 检测是否合格

            var ProductTime = spiDomain.rollStartProductTime.ToDateTime();
            return new RealTimeProduction()
            {
                GUID = Guid.NewGuid(),
                ProductTime = DateTime.Now,
                RollNumber = spiDomain.rollNumber,
                SubRollNumber = spiDomain.subRollNumber,
                ORDWTH = spiDomain.width.ToFloat(),
                ColorCode = spiDomain.colorCode,

                Flag = "00",

                LengthLocation = spiDomain.length.ToFloat(),
                WidthLocation = spiDomain.width.ToFloat(),

                RealTimeL = this.textBox_CurrentL.Text.ToFloat(),
                RealTimeA = this.textBox_CurrentA.Text.ToFloat(),
                RealTimeB = this.textBox_CurrentB.Text.ToFloat(),
                RealTimeHeight = this.text_Height.Text.ToFloat(),

                StandardL = 2,
                StandardA = 2,
                StandardB = 2,

                DeltaL = spiDomain.deltaL.ToFloat(),
                DeltaA = spiDomain.deltaA.ToFloat(),
                DeltaB = spiDomain.deltaB.ToFloat(),
                DeltaE = spiDomain.deltaE.ToFloat(),

                DeltaL_Std = csDomain.deltaL_std.ToFloat(),
                DeltaA_Std = csDomain.deltaA_std.ToFloat(),
                DeltaB_Std = csDomain.deltaB_std.ToFloat(),
                DeltaE_Std = csDomain.deltaE_std.ToFloat(),

                DeltaL_Warning = firstJudgeL, //&& secondJudgeL,
                DeltaA_Warning = firstJudgeA, //&& secondJudgeA,
                DeltaB_Warning = firstJudgeB, //&& secondJudgeB,
                DeltaE_Warning = firstJudgeE, //&& secondJudgeE
            };
        }

        public StatisticsProduction generateStatisticsProductionMessage()
        {
            calculateDeltaMean();
            calculateMSE();
            return new StatisticsProduction()
            {
                GUID = Guid.NewGuid(),
                ProductTime = spiDomain.rollStartProductTime.ToDateTime(),
                RollNumber = spiDomain.rollNumber,
                ColorCode = spiDomain.colorCode,

                DeltaL_Mean = (float)deltaL_mean,
                DeltaA_Mean = (float)deltaA_mean,
                DeltaB_Mean = (float)deltaB_mean,
                DeltaE_Mean = (float)deltaE_mean,

                DeltaL_MSE = (float)deltaL_mse,
                DeltaA_MSE = (float)deltaA_mse,
                DeltaB_MSE = (float)deltaB_mse,
                DeltaE_MSE = (float)deltaE_mse,

                BadNumber = notGoodCount,
                GoodNumber = deltaDataQueue.Count - notGoodCount
            };
        }

        private void calculateMSE()
        {
            double errorL_sum = 0;
            double errorA_sum = 0;
            double errorB_sum = 0;
            double errorE_sum = 0;
            int count = deltaDataQueue.Count;

            foreach (var data in deltaDataQueue)
            {
                errorL_sum += Math.Pow(data.deltaL - deltaL_mean, 2);
                errorA_sum += Math.Pow(data.deltaA - deltaA_mean, 2);
                errorB_sum += Math.Pow(data.deltaB - deltaB_mean, 2);
                errorE_sum += Math.Pow(data.deltaE - deltaE_mean, 2);
            }
            deltaL_mse = Math.Sqrt(errorL_sum / count);
            deltaA_mse = Math.Sqrt(errorA_sum / count);
            deltaB_mse = Math.Sqrt(errorB_sum / count);
            deltaE_mse = Math.Sqrt(errorE_sum / count);
        }


        private class DeltaData
        {
            public double deltaL { get; set; }
            public double deltaA { get; set; }
            public double deltaB { get; set; }
            public double deltaE { get; set; }
        }

        double deltaL_mean;
        double deltaA_mean;
        double deltaB_mean;
        double deltaE_mean;
        double deltaL_mse;
        double deltaA_mse;
        double deltaB_mse;
        double deltaE_mse;

        private void calculateDeltaMean()
        {
            double deltaL_sum = 0;
            double deltaA_sum = 0;
            double deltaB_sum = 0;
            double deltaE_sum = 0;
            int count = deltaDataQueue.Count;

            foreach (DeltaData data in deltaDataQueue)
            {
                deltaL_sum += data.deltaL;
                deltaA_sum += data.deltaA;
                deltaB_sum += data.deltaB;
                deltaE_sum += data.deltaE;
            };
            deltaL_mean = deltaL_sum / count;
            deltaA_mean = deltaA_sum / count;
            deltaB_mean = deltaB_sum / count;
            deltaE_mean = deltaE_sum / count;
        }

        #region StripProductionInformationService 成员


        public void changeColorStandard(StripProductionInformationDomain spiDomain, int status)
        {
            switch (status)
            {
                case 0:

                default:
                    return;
            }
        }

        #endregion

    }
}