using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using PCClient.Helper;
using System.Net.Sockets;

using System.Threading;
using ColorimeterService.Service;
using ColorimeterService.Service.impl;
using ColorimeterDAO.WinDomain;
using System.Net;
using Sunny.UI;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using PlcComm.DataExchange;


namespace PCClient.UIFrame.Check
{

    //设置可以调用的范围
    [ComVisible(true)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    
    public partial class Checkframe : Form
    {
        // 色差仪控制模块接口
        ColorimeterControlService colorimeterService = null;
        // 生产信息模块接口
        StripProductionInformationService stripService = null;
        // 生产信息模块窗体实体类
        StripProductionInformationDomain spiDomain = StripProductionInformationDomain.Instance;
        // 颜色标准模块窗体实体类
        ColorStandardDomain csDomain = ColorStandardDomain.Instance;

        // 创建一个定时线程，定时 执行updateUIStart方法。
        System.Timers.Timer timer = new System.Timers.Timer(100);
        public WebBrowser full{get;set; }

        
        string fullchart = System.Environment.CurrentDirectory + "\\showEchart\\test.html";
        string midchart = System.Environment.CurrentDirectory + "\\showEchart\\V1midchart.html";
        string rightchart = System.Environment.CurrentDirectory + "\\showEchart\\V1rightchart.html";

        string fullchart_larger = System.Environment.CurrentDirectory + "\\showEchart\\V1fullchart_Larger.html";
        string leftchart_larger = System.Environment.CurrentDirectory + "\\showEchart\\V1leftchart_Larger.html";
        string midchart_larger = System.Environment.CurrentDirectory + "\\showEchart\\V1midchart_Larger.html";
        string rightchart_larger = System.Environment.CurrentDirectory + "\\showEchart\\V1rightchart_Larger.html";


        string str4 = AppDomain.CurrentDomain.BaseDirectory;//获取基目录，它由程序集冲突解决程序用来探测程序集。 
        string str5 = Application.StartupPath;//获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。 
        string str6 = Application.ExecutablePath;//获取启动了应用程序的可执行文件的路径，包括可执行文件的名称。 
        string str7 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//获取或设置包含该应用程序的目录的名称。
       

        Echarts_demo.Echarts_helper echarts = new Echarts_demo.Echarts_helper();

       
        
        public Checkframe()
        {
            InitializeComponent();     
            //调用浏览器兼容类
            new BrowerProblem().SetWebBrowserFeatures(10);
            //this.wb_fullchart.Url = new Uri(fullchart);
            this.btn_Linked.Enabled = true;
            this.btn_Broken.Enabled = false;
           
            InitializeWinDomain();
            
        }

        private void InitializeWinDomain()
        {
            // 带钢生产信息
            this.text_JuanPorduceTime.DataBindings.Add("Text", spiDomain, "rollStartProductTime", false);
            this.text_JuanProduceEndtime.DataBindings.Add("Text", spiDomain, "rollEndProductTime", false);
            this.text_JuanNum.DataBindings.Add("Text", spiDomain, "rollNumber", false);
            this.text_SubJuanNum.DataBindings.Add("Text", spiDomain, "subRollNumber", false);
            this.text_JuanBroadwise.DataBindings.Add("Text", spiDomain, "broadwise", false);

            this.text_JuanLength.DataBindings.Add("Text", spiDomain, "length", false);
            this.text_JuanWidth.DataBindings.Add("Text", spiDomain, "width", false);
            this.text_JuanThickness.DataBindings.Add("Text", spiDomain, "thickness", false);
            this.text_ColorCode.DataBindings.Add("Text", spiDomain, "colorCode", false);
            this.text_colorName.DataBindings.Add("Text", spiDomain, "colorName", false);

            this.text_DeviationValue_L.DataBindings.Add("Text", spiDomain, "deltaL", false);
            this.text_DeviationValue_a.DataBindings.Add("Text", spiDomain, "deltaA", false);
            this.text_DeviationValue_b.DataBindings.Add("Text", spiDomain, "deltaB", false);
            this.text_StandardDeviation.DataBindings.Add("Text", spiDomain, "deltaE", false);
            this.text_IfGood.DataBindings.Add("FillColor", spiDomain, "fillColor", false);
            this.text_IfGood.DataBindings.Add("Text", spiDomain, "fillText", false);
            this.Text_NotGoodNum.DataBindings.Add("Text", spiDomain, "notGoodCount", false);
            this.Text_CheckNum.DataBindings.Add("Text", spiDomain, "checkCount", false);

            this.text_std_L.DataBindings.Add("Text", spiDomain, "std_L", false);
            this.text_std_a.DataBindings.Add("Text", spiDomain, "std_a", false);
            this.text_std_b.DataBindings.Add("Text", spiDomain, "std_b", false);
            // 颜色标准
            this.text_LetPassDeviationValue_L.DataBindings.Add("Text", csDomain, "deltaL_std", false);
            this.text_LetPassDeviationValue_a.DataBindings.Add("Text", csDomain, "deltaA_std", false);
            this.text_LetPassDeviationValue_b.DataBindings.Add("Text", csDomain, "deltaB_std", false);
            this.text_LetPassDeviationValue_E.DataBindings.Add("Text", csDomain, "deltaE_std", false);
            this.text_LetPassValue_L.DataBindings.Add("Text", csDomain, "L_C", false);
            this.text_LetPassValue_a.DataBindings.Add("Text", csDomain, "a_C", false);
            this.text_LetPassValue_b.DataBindings.Add("Text", csDomain, "b_C", false);

            this.text_std_L2.DataBindings.Add("Text", csDomain, "std_L", false);
            this.text_std_a2.DataBindings.Add("Text", csDomain, "std_a", false);
            this.text_std_b2.DataBindings.Add("Text", csDomain, "std_b", false);

            this.text_Manualinput_ColorCode.DataBindings.Add("Text", csDomain, "Manualinput_colorCode", false);
            this.text_Manualinput_L.DataBindings.Add("Text", csDomain, "Manualinput_L_star", false);
            this.text_Manualinput_a.DataBindings.Add("Text", csDomain, "Manualinput_a_star", false);
            this.text_Manualinput_b.DataBindings.Add("Text", csDomain, "Manualinput_b_star", false);

            this.text_SelfCheck_ColorCode.DataBindings.Add("Text", csDomain, "SelfCheck_colorCode", false);
            this.text_SelfCheck_L.DataBindings.Add("Text", csDomain, "SelfCheck_l_star", false);
            this.text_SelfCheck_a.DataBindings.Add("Text", csDomain, "SelfCheck_a_star", false);
            this.text_SelfCheck_b.DataBindings.Add("Text", csDomain, "SelfCheck_b_star", false);
            this.text_SelfCheck_ResampleIndex.DataBindings.Add("Text", csDomain, "SelfCheck_sample_index", false);


            // 色差仪控制指示灯
            this.label_LongDistance.DataBindings.Add("BackColor", csDomain, "remote", false);
            this.lable_ComeZero_PC_Complete.DataBindings.Add("BackColor", csDomain, "homeReached1", false);
            this.label_ServoRunning.DataBindings.Add("BackColor", csDomain, "motRunning", false);
            this.label_StateGreen.DataBindings.Add("BackColor", csDomain, "statusGreen", false);
            this.label_SelfCheckBlue.DataBindings.Add("BackColor", csDomain, "statusBlue", false);
            this.label_StatusRed.DataBindings.Add("BackColor", csDomain, "statusRed", false);
            this.label_LocalModelYellow.DataBindings.Add("BackColor", csDomain, "statusYellow", false);
            this.label_GuoFengSuona.DataBindings.Add("BackColor", csDomain, "weldHorn1", false);
            this.label_Error.DataBindings.Add("BackColor", csDomain, "error", false);
            // 色差仪控制伺服代码
            this.text_ServoErrorCode.DataBindings.Add("Text", csDomain, "servoErrorCode", false);
            this.text_ServoWarningCode.DataBindings.Add("Text", csDomain, "servoWarningCode", false);
            // 右侧色差仪滑动杆
            this.label_OpeningPlus.DataBindings.Add("BackColor", csDomain, "pmax", false);
            this.label_OpeningSub.DataBindings.Add("BackColor", csDomain, "pmin", false);
            this.text_leftPosition.DataBindings.Add("Text", csDomain, "positionRef", false);
            this.text_RightPosition.DataBindings.Add("Text", csDomain, "position", false);
            this.text_ExportSpeed.DataBindings.Add("Text", csDomain, "exportSpeed", false);
            this.text_TechnicSpeed.DataBindings.Add("Text", csDomain, "technicSpeed", false);
            this.text_RollLength.DataBindings.Add("Text", csDomain, "rollLength", false);
            this.text_HanFeng5.DataBindings.Add("Text", csDomain, "hanFeng5", false);
            this.text_HanFeng6.DataBindings.Add("Text", csDomain, "hanFeng6", false);
        }

        private delegate void updateUIStartDelegate(object sender, System.Timers.ElapsedEventArgs e);
        private void updateUIStart(object sender, System.Timers.ElapsedEventArgs e)
        {

            if (this.text_JuanPorduceTime.InvokeRequired)
            {
                updateUIStartDelegate d = updateUIStart;
                this.text_JuanPorduceTime.Invoke(d, sender, e);
            }
            else
            {
                this.text_JuanPorduceTime.DataBindings[0].ReadValue();
                this.text_JuanProduceEndtime.DataBindings[0].ReadValue();
                this.text_JuanNum.DataBindings[0].ReadValue();
                this.text_SubJuanNum.DataBindings[0].ReadValue();
                this.text_JuanLength.DataBindings[0].ReadValue();
                this.text_JuanWidth.DataBindings[0].ReadValue();
                this.text_JuanThickness.DataBindings[0].ReadValue();
                this.text_JuanBroadwise.DataBindings[0].ReadValue();

                this.text_LetPassDeviationValue_L.DataBindings[0].ReadValue();
                this.text_LetPassDeviationValue_a.DataBindings[0].ReadValue();
                this.text_LetPassDeviationValue_b.DataBindings[0].ReadValue();
                this.text_LetPassDeviationValue_E.DataBindings[0].ReadValue();
                this.text_LetPassValue_L.DataBindings[0].ReadValue();
                this.text_LetPassValue_a.DataBindings[0].ReadValue();
                this.text_LetPassValue_b.DataBindings[0].ReadValue();
                this.text_ColorCode.DataBindings[0].ReadValue();
                this.text_colorName.DataBindings[0].ReadValue();

                this.text_DeviationValue_L.DataBindings[0].ReadValue();
                this.text_DeviationValue_a.DataBindings[0].ReadValue();
                this.text_DeviationValue_b.DataBindings[0].ReadValue();
                this.text_StandardDeviation.DataBindings[0].ReadValue();


                this.text_IfGood.DataBindings[0].ReadValue();
                this.text_IfGood.DataBindings[1].ReadValue();

                this.Text_NotGoodNum.DataBindings[0].ReadValue();
                this.Text_CheckNum.DataBindings[0].ReadValue();

                this.text_std_L.DataBindings[0].ReadValue();
                this.text_std_a.DataBindings[0].ReadValue();
                this.text_std_b.DataBindings[0].ReadValue();

                this.text_std_L2.DataBindings[0].ReadValue();
                this.text_std_a2.DataBindings[0].ReadValue();
                this.text_std_b2.DataBindings[0].ReadValue();

                this.text_Manualinput_ColorCode.DataBindings[0].ReadValue();

                this.text_SelfCheck_ColorCode.DataBindings[0].ReadValue();
                this.text_SelfCheck_L.DataBindings[0].ReadValue();
                this.text_SelfCheck_a.DataBindings[0].ReadValue();
                this.text_SelfCheck_b.DataBindings[0].ReadValue();
                this.text_SelfCheck_ResampleIndex.DataBindings[0].ReadValue();

                this.label_LongDistance.DataBindings[0].ReadValue();
                this.lable_ComeZero_PC_Complete.DataBindings[0].ReadValue();
                this.label_StateGreen.DataBindings[0].ReadValue();
                this.label_SelfCheckBlue.DataBindings[0].ReadValue();
                this.label_StatusRed.DataBindings[0].ReadValue();
                this.label_LocalModelYellow.DataBindings[0].ReadValue();
                this.label_GuoFengSuona.DataBindings[0].ReadValue();
                this.label_Error.DataBindings[0].ReadValue();

                this.text_ServoErrorCode.DataBindings[0].ReadValue();
                this.text_ServoWarningCode.DataBindings[0].ReadValue();

                this.label_OpeningPlus.DataBindings[0].ReadValue();
                this.label_OpeningSub.DataBindings[0].ReadValue();
                this.text_leftPosition.DataBindings[0].ReadValue();
                this.text_RightPosition.DataBindings[0].ReadValue();
                this.text_ExportSpeed.DataBindings[0].ReadValue();
                this.text_TechnicSpeed.DataBindings[0].ReadValue();
                this.text_RollLength.DataBindings[0].ReadValue();
                this.text_HanFeng5.DataBindings[0].ReadValue();
                this.text_HanFeng6.DataBindings[0].ReadValue();
            }
        }

        /// <summary>
        /// 监听domain,判断domain是否有值，进行持续访问
        /// </summary>
        private void monitorDomain()
        {
            Object[] objArray = new Object[5];
            objArray[0] = (Object)spiDomain.deltaL;
            objArray[1] = (Object)spiDomain.deltaA;
            objArray[2] = (Object)spiDomain.deltaB;
            objArray[3] = (Object)spiDomain.deltaE;
            objArray[4] = (Object)spiDomain.length;
        }

        
        private void CheckFrame_Load(object sender, EventArgs e)
        {
            //this.text_Manualinput_L.Text = "12";
            //this.text_Manualinput_a.Text = "0";
            //this.text_Manualinput_b.Text = "2";
            //this.text_Manualinput_ColorCode.Text = "2004509";
            csDomain.Manualinput_colorCode = "2004509";
            csDomain.Manualinput_l_star = "85";
            csDomain.Manualinput_a_star = "0";
            csDomain.Manualinput_b_star = "2";
            this.wb_fullchart.Url = new Uri(fullchart);
            wb_fullchart.ObjectForScripting = this;//具体公开的对象,这里可以公开自定义对象
        }

        
        private void btn_Linked_Click_1(object sender, EventArgs e)
        {

            this.btn_Linked.Enabled = false;
            this.btn_Broken.Enabled = true;
            // IP and EndPoint
            IPAddress ipAddress = IPAddress.Parse(this.text_IP.Text.Trim());
            IPEndPoint endPoint = new IPEndPoint(ipAddress, int.Parse(this.text_Port.Text.Trim()));

            // new service
            stripService = new StripProductionInformationServiceImpl(
                ref this.text_CurrentValue_L,
                ref this.text_CurrentValue_a,
                ref this.text_CurrentValue_b,
                ref this.text_Height,
                ref this.label_isPlcConnect,
                ref this.wb_leftchart,
                ref this.wb_fullchart
                );

            colorimeterService = new ColorimeterControlServiceImpl(
                ipAddress,
                endPoint,
                ref this.text_ReceptMessage,
                ref this.text_ReceptPackagLength,
                ref this.text_ReceptTime,
                ref this.text_SendMessage,

                ref this.label_isConnect,

                ref this.text_CurrentValue_L,
                ref this.text_CurrentValue_a,
                ref this.text_CurrentValue_b,
                ref this.text_Height,
                ref this.btn_Linked
                );

            


            timer.Elapsed += updateUIStart;
            timer.Enabled = false;
            timer.Start();

            //// 默认自对比模式
            stripService.absColorStd = new SelfCalibrationColorStandard(ref this.text_CurrentValue_L,
                    ref this.text_CurrentValue_a, ref this.text_CurrentValue_b);

            btn_LIMSS.FillColor = Color.LightGray;
            btn_rengongInput.FillColor = Color.LightGray;
            btn_selfTest.FillColor = Color.Green;

            try
            {
                // 连接服务(colorimeterService.connectServer)要在stripService获取当前LAB(stripService.getCurrentLAB)之前，
                // 否则，第一次连接无法得到LAB，
                // 因为ColorimeterControlServiceImpl.threadRecvDataFlag = false，不开启getCurrentLAB的线程

                // 色差仪连接服务器
                colorimeterService.connectServer();
                // initConnectPlc thread start here
                stripService.initConnectPlc();

                stripService.readPLC();
                // 得到当前色差仪 l a b,
                // 计算并判断是否合格
                stripService.currentJudge();

                this.button_AutoCirculation.PerformClick();
            }

            catch (Exception ex)
            {
                MessageBox.Show("无法连接服务器!" + ex.Message);
            }
        }

        /// <summary>
        /// [btn_Broken]断开连接的点击事件函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "全":
                    this.panel_Chart_Content.Controls.Clear();
                    full = new WebBrowser();
                    
                    panel_fullchart.Visible = false;
                    //panel_leftchart.Visible = false;
                    panel_midchart.Visible = false;
                    panel_rightchart.Visible = false;

                    full.Width = panel_Chart_Content.Width;
                    full.Height = panel_Chart_Content.Height;
                    this.panel_Chart_Content.Controls.Add(full);//将子窗体载入panel

                    this.full.Url = new Uri(fullchart_larger); 
                    break;
                    
                case "左":
                    this.panel_Chart_Content.Controls.Clear();
                    full = new WebBrowser();
                    
                    panel_fullchart.Visible = false;
                    //panel_leftchart.Visible = false;
                    panel_midchart.Visible = false;
                    panel_rightchart.Visible = false;

                    full.Width = panel_Chart_Content.Width;
                    full.Height = panel_Chart_Content.Height;
                    this.panel_Chart_Content.Controls.Add(full);//将子窗体载入panel
                    
                    this.full.Url = new Uri(leftchart_larger);
                    //getdata();
                    break;

                case "中":
                    this.panel_Chart_Content.Controls.Clear();
                    full = new WebBrowser();
                    
                    panel_fullchart.Visible = false;
                    //panel_leftchart.Visible = false;
                    panel_midchart.Visible = false;
                    panel_rightchart.Visible = false;

                    full.Width = panel_Chart_Content.Width;
                    full.Height = panel_Chart_Content.Height;
                    this.panel_Chart_Content.Controls.Add(full);//将子窗体载入panel

                    this.full.Url = new Uri(midchart_larger); 
                    break;
                case "右":
                    this.panel_Chart_Content.Controls.Clear();
                    full = new WebBrowser();
                    
                    panel_fullchart.Visible = false;
                    //panel_leftchart.Visible = false;
                    panel_midchart.Visible = false;
                    panel_rightchart.Visible = false;

                    full.Width = panel_Chart_Content.Width;
                    full.Height = panel_Chart_Content.Height;
                    this.panel_Chart_Content.Controls.Add(full);//将子窗体载入panel

                    this.full.Url = new Uri(rightchart_larger); 
                    break;
                case "同时":
                    this.wb_fullchart.Url = new Uri(fullchart);
                    //this.wb_leftchart.Url = new Uri(leftchart);
                    //this.wb_middlechart.Url = new Uri(midchart);
                    //this.wb_rightchart.Url = new Uri(rightchart);    
                    panel_fullchart.Visible = true;
                    //panel_leftchart.Visible = true;
                    panel_midchart.Visible = true;
                    panel_rightchart.Visible = true;
                    break;
                    //new Checkframe().ShowDialog();
                    
                    //this.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                    //this.TopLevel = false;//指示子窗体非顶级窗体
                    //break;
            }
            
        }

        private void wb_fullchart_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void wb_leftchart_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void btn_Broken_Click(object sender, EventArgs e)
        {
            this.btn_Linked.Enabled = true;
            this.btn_Broken.Enabled = false;
            this.text_IP.Enabled = true;
            this.text_Port.Enabled = true;
            // 关闭与色差仪的连接
            colorimeterService.closeConnection();
            // 关闭UI更新
            timer.Stop();
        }

        private void btn_selfTest_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否切换<自对比>模式？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                stripService.absColorStd = new SelfCalibrationColorStandard(ref this.text_CurrentValue_L,
                    ref this.text_CurrentValue_a, ref this.text_CurrentValue_b);

                btn_LIMSS.FillColor = Color.LightGray;
                btn_rengongInput.FillColor = Color.LightGray;
                btn_selfTest.FillColor = Color.Lime;

            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 自校验重采样按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton_SelfCheckResample_Click(object sender, EventArgs e)
        {
            if (stripService.absColorStd == null || !stripService.absColorStd.GetType().Name.Equals(typeof(SelfCalibrationColorStandard).Name))
            {
                MessageBox.Show("未开启自交验模式！请点击左侧按钮！");
            }
            else
            {
                SelfCalibrationColorStandard cs = (SelfCalibrationColorStandard)stripService.absColorStd;
                cs.resetStatus();
            }
        }

        private void btn_rengongInput_Click(object sender, EventArgs e)
        {
            
            DialogResult dr = MessageBox.Show("是否切换<人工输入>模式？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                stripService.absColorStd = new ArtificialElectronicsColorStandard();

               // csDomain.Manualinput_colorCode = this.text_Manualinput_ColorCode.Text;
                //csDomain.Manualinput_l_star = this.text_Manualinput_L.Text;
                //csDomain.Manualinput_a_star = this.text_Manualinput_a.Text;
                //csDomain.Manualinput_b_star = this.text_Manualinput_b.Text;
                

                btn_LIMSS.FillColor = Color.LightGray;
                btn_selfTest.FillColor = Color.LightGray;
                btn_rengongInput.FillColor = Color.Lime;
            }
            else
            {
                return;
            }
            
        }
        
        private void btn_LIMSS_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否切换<LIMS>模式？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                btn_selfTest.FillColor = Color.LightGray;
                btn_rengongInput.FillColor = Color.LightGray;
                btn_LIMSS.FillColor = Color.Lime;
            }
            else
            {
                return;
            }
        }

        

        private void btn_LinkedSechayi_Click(object sender, EventArgs e)
        {
            List<Byte> bytes = new List<byte>();
            bytes.Add(1);
            bytes.AddRange(Encoding.UTF8.GetBytes(ColorimeterControlServiceImpl.START_MSG));
            this.text_SendMessage.Text = Encoding.UTF8.GetString(bytes.ToArray());
            colorimeterService.sendData(Encoding.UTF8.GetBytes(this.text_SendMessage.Text));
        }

        private void btn_StopSechayi_Click(object sender, EventArgs e)
        {
            List<Byte> bytes = new List<byte>();
            bytes.Add(1);
            bytes.AddRange(Encoding.UTF8.GetBytes(ColorimeterControlServiceImpl.STOP_MSG));
            this.text_SendMessage.Text = Encoding.UTF8.GetString(bytes.ToArray());
            colorimeterService.sendData(Encoding.UTF8.GetBytes(this.text_SendMessage.Text));
        }

        private void writePlc1500ThreadMethod()
        {
            DataExchange.WritePlc1500();
        }

        /// <summary>
        /// 回零PC按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_BackZeroPC_Click(object sender, EventArgs e)
        {
            // 如果Control，则已经按下，现在释放
            if (button_BackZeroPC.BackColor == SystemColors.Control)
            {
                button_BackZeroPC.BackColor = SystemColors.ControlDark; // 变为ControlDark
                DataExchange.PCBackToHome = false;
            }
            else // 已经释放，现在按下
            {
                button_BackZeroPC.BackColor = SystemColors.Control;
                DataExchange.PCBackToHome = true;
            }
            Thread t = new Thread(writePlc1500ThreadMethod);
            t.IsBackground = true;
            t.Start();
        }


        /// <summary>
        /// 回零准备点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_BackZeroCompleted_Click(object sender, EventArgs e)
        {
            this.button_BackZeroCompleted.BackColor = SystemColors.ControlDark;
            this.button_ManualMode.BackColor = SystemColors.Control;
            this.button_AutoCirculation.BackColor = SystemColors.Control;
            this.button_AutoFixationMode.BackColor = SystemColors.Control;

            DataExchange.DB11_DBD8 = 1;
            Thread t = new Thread(writePlc1500ThreadMethod);
            t.IsBackground = true;
            t.Start();

        }

        /// <summary>
        /// 手动模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ManualMode_Click(object sender, EventArgs e)
        {
            this.button_BackZeroCompleted.BackColor = SystemColors.Control;
            this.button_ManualMode.BackColor = SystemColors.ControlDark;
            this.button_AutoCirculation.BackColor = SystemColors.Control;
            this.button_AutoFixationMode.BackColor = SystemColors.Control;

            DataExchange.DB11_DBD8 = 2;
            Thread t = new Thread(writePlc1500ThreadMethod);
            t.IsBackground = true;
            t.Start();

        }

        /// <summary>
        /// 自动循环模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_AutoCirculation_Click(object sender, EventArgs e)
        {
            this.button_BackZeroCompleted.BackColor = SystemColors.Control;
            this.button_ManualMode.BackColor = SystemColors.Control;
            this.button_AutoCirculation.BackColor = SystemColors.ControlDark;
            this.button_AutoFixationMode.BackColor = SystemColors.Control;

            DataExchange.DB11_DBD8 = 3;
            Thread t = new Thread(writePlc1500ThreadMethod);
            t.IsBackground = true;
            t.Start();

        }

        /// <summary>
        /// 自动固定模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_AutoFixationMode_Click(object sender, EventArgs e)
        {
            this.button_BackZeroCompleted.BackColor = SystemColors.Control;
            this.button_ManualMode.BackColor = SystemColors.Control;
            this.button_AutoCirculation.BackColor = SystemColors.Control;
            this.button_AutoFixationMode.BackColor = SystemColors.ControlDark;

            DataExchange.DB11_DBD8 = 4;
            Thread t = new Thread(writePlc1500ThreadMethod);
            t.IsBackground = true;
            t.Start();

        }

        /// <summary>
        /// 输入固定位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_PositionLocation_Click(object sender, EventArgs e)
        {
            float positionLocation;
            bool ret = float.TryParse(this.text_PositionLocation.Text, out positionLocation);
            if (ret == true) 
            {
                DataExchange.DB11_DBD12 = positionLocation;
                Thread t = new Thread(writePlc1500ThreadMethod);
                t.IsBackground = true;
                t.Start();
                MessageBox.Show("已输入固定位置:" + positionLocation);
                
            }
            else
            {
                MessageBox.Show("输入固定位置内容不是合法数字！");
            }
        }

        /// <summary>
        /// 复位按钮Reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Reset_Click(object sender, EventArgs e)
        {
            // DataExchange.Reset = true;
        }

        private void uiLabel13_Click(object sender, EventArgs e)
        {

        }

    }
}
