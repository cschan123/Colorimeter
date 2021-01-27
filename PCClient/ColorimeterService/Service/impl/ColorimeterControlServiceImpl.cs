using ColorimeterDAO.Dao;
using ColorimeterDAO.Dao.impl;
using ColorimeterDAO.WinDomain;
using ColorimeterService.Utils;
using Sunny.UI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ColorimeterService.Service.impl
{
    public class ColorimeterControlServiceImpl : ColorimeterControlService
    {
        /// <summary>
        /// IP地址
        /// </summary>
        private IPAddress ipAddress { get; set; }
        /// <summary>
        /// IPEndPoint
        /// </summary>
        private IPEndPoint endPoint { get; set; }
        /// <summary>
        /// 客户端PC的socket
        /// </summary>
        private Socket socketClient { get; set; }
        /// <summary>
        /// 接收数据报文的线程
        /// </summary>
        private Thread threadRecvData { get; set; }
        // 关闭threadRecvData线程使用的变量 flag
        public static volatile bool threadRecvDataFlag;

        // 接收数据字节长度
        private int recvDataLength { get; set; }
        // 接收数据的时间
        private DateTime recvDataDateTime { get; set; }

        // 页面控件对象，作为引用参数传入
        public UIRichTextBox textBoxRecvData;
        public UITextBox textBoxRecvDataLength;
        public UITextBox textBoxRecvDataTime;
        public UITextBox textBoxSendData;
        public Label label_ConnectStatus;
        public UITextBox textBox_CurrentL;
        public UITextBox textBox_CurrentA;
        public UITextBox textBox_CurrentB;
        public UITextBox text_Height;

        public Button buttonConnection;

        // 接收数据报文的字节缓冲
        private byte[] readBuffer;
        private List<byte> buffer = new List<byte>(4096);

        // 解析数据的concurrentQueue的最大长度
        public static readonly int MAX_QUEUE_SIZE = 10;
        private const int COMPLETED_LEN = 68;

        // 解析得到的 颜色 l a b 高度 h的对象ParsedData的同步队列
        public static ConcurrentQueue<Transporter.ParsedData> parsedDataQueue = new ConcurrentQueue<Transporter.ParsedData>();

        // 色差仪开始命令报文, 前面还有一个1个byte = 1
        public static string START_MSG = "0081R10000220271";
        // 色差仪停止命令报文, 前面还有一个1个byte = 1
        public static string STOP_MSG = "0021R00146";


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="endPoint">IPEndPoint IP端口</param>
        /// <param name="textBoxRecvData">接收数据textBox对象</param>
        /// <param name="textBoxRecvDataLength">接收数据长度textBox对象</param>
        /// <param name="textBoxRecvDataTime">接收数据时间textBox对象</param>
        /// <param name="button">连接服务器按钮Button对象</param>
        public ColorimeterControlServiceImpl(
            IPAddress ipAddress,
            IPEndPoint endPoint,
            ref UIRichTextBox textBoxRecvData,
            ref UITextBox textBoxRecvDataLength, 
            ref UITextBox textBoxRecvDataTime, 
            ref UITextBox textBoxSendData,
            ref Label label_ConnectionStatus,
            ref UITextBox textBox_CurrentL,
            ref UITextBox textBox_CurrentA,
            ref UITextBox textBox_CurrentB,
            ref UITextBox text_Height,

            ref Button button
            )
        {
            this.ipAddress = ipAddress;
            this.endPoint = endPoint;
            this.socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.textBoxRecvData = textBoxRecvData;
            this.textBoxRecvDataLength = textBoxRecvDataLength;
            this.textBoxRecvDataTime = textBoxRecvDataTime;
            this.textBoxSendData = textBoxSendData;
            this.label_ConnectStatus = label_ConnectionStatus;
            this.textBox_CurrentA = textBox_CurrentA;
            this.textBox_CurrentB = textBox_CurrentB;
            this.textBox_CurrentL = textBox_CurrentL;
            this.text_Height = text_Height;

            this.buttonConnection = button;

        }

        /// <summary>
        /// 连接服务器，改变buttonConnection（连接按钮状态），连接成功后开启线程接收数据。
        /// </summary>
        public void connectServer()
        {
            try
            {
                socketClient.Connect(endPoint);
                DelegateFactory.changeButtonStatus(ref buttonConnection, false);

                DelegateFactory.changeDataInLabel(ref this.label_ConnectStatus, "已连接");
                DelegateFactory.changeColorInLable(ref this.label_ConnectStatus, Color.Green);

                threadRecvData = new Thread(threadRecvDataStart);
                threadRecvData.IsBackground = true;
                threadRecvDataFlag = true;
                threadRecvData.Start();
            }
            catch (SocketException)
            {
                throw new Exception("请输入正确的IP和端口号！");
            }
        }


        /// <summary>
        /// 接收数据的线程，执行的方法ThreadStart threadRecvDataStart
        /// </summary>
        private void threadRecvDataStart()
        {
            socketClient.Send(getBytes());
            while (true)
            {
                if (!threadRecvDataFlag)  // 如果线程已关闭(flag=flase)，break
                {
                    break;
                }
                readBuffer = new byte[1024];
                try
                {
                    // 确保接收数据完整性
                    int length = socketClient.Receive(readBuffer, SocketFlags.None);
                    byte[] temp = new byte[length];
                    Array.Copy(readBuffer, temp, length);
                    buffer.AddRange(temp);

                    //长度大于6，含有FF 01 + count +msgno = 2+3+1
                    while (buffer.Count >= 6)
                    {
                        string head = Convert.ToString(buffer.Get(0), 16) + "0" + Convert.ToString(buffer.Get(1), 16);
                        if ("FF01".EqualsIgnoreCase(head))
                        {

                            string asciiStr = Transporter.bytes2str(buffer.GetRange(2, 3).ToArray(), 3); // 如303341
                            string hexStr = Transporter.asciiStr2HexStr(asciiStr); // 03A
                            float num = Convert.ToInt32(hexStr, 16); // 58
                            int len = (int)num;  // 数据区长度 应该是 58, 58个字节才是完整的数据报文
                            int completedLen = len + 10; // 68

                            // 如果缓冲区长度小于 数据报文长度，break，继续接收数据
                            if (buffer.Count < completedLen)
                            {
                                break;
                            }

                            byte[] ReceiveBytes = new byte[completedLen];
                            buffer.CopyTo(0, ReceiveBytes, 0, completedLen);

                            recvDataLength = completedLen;  // 接收到的数据包的字节长度
                            recvDataDateTime = DateTime.Now; // 接收到数据包的时间

                            // 转换 byte[] 为 ASCII字符串
                            string asciiString = Transporter.bytes2str(ReceiveBytes, completedLen);

                            // 如果全长为 COMPLETED_LEN(68)，才做数据解析，显示报文
                            if (completedLen == COMPLETED_LEN)
                            {
                                // ----------------------------------------------------------------------- 
                                // 解析接收的asciiString， asciiString --> 字符串(意义为16进制）
                                string asciiString2 = asciiString.Substring(3);
                                if (!asciiString2.Contains("ff1"))
                                {
                                    string hexString = Transporter.asciiStr2HexStr(asciiString2); //03A0H0000000000170005242238E9B3D7940DD4282D45800000000000000000D32
                                    // 解析接收的16进制字符串，转换出 颜色 l a b和 高度 h，封装为对象

                                    Transporter.ParsedData parsedData = Transporter.GetParsedData(hexString);
                                    // 锁
                                    lock (StripProductionInformationDomain.Instance)
                                    {
                                        // 解析的数据parsedData对象，加入 concurrentQueue parsedDataQueue
                                        if (parsedDataQueue.Count == MAX_QUEUE_SIZE) // 如果等于queue最大长度，队首出队，再入队
                                        {
                                            Transporter.ParsedData result;
                                            if (!parsedDataQueue.TryDequeue(out result))  // 不会发生
                                            {
                                                throw new Exception("同步队列parsedDataQueue满时入队时未出队!");
                                            }
                                        }
                                        parsedDataQueue.Enqueue(parsedData);
                                    }
                                    // ----------------------------------------------------------------------- 

                                    // 取queue最后一个，显示到窗体上。
                                    Transporter.ParsedData data = parsedData; // 最后一个
                                    double l_2 = Math.Round(data.l, 2); // 保留2位小数
                                    double a_2 = Math.Round(data.a, 2);
                                    double b_2 = Math.Round(data.b, 2);
                                    double h_2 = Math.Round(data.height, 0);
                                    string l = l_2.ToString();
                                    string a = a_2.ToString();
                                    string b = b_2.ToString();
                                    string height = h_2.ToString();

                                    DelegateFactory.showDataInTextBox(ref this.textBox_CurrentL, l);
                                    DelegateFactory.showDataInTextBox(ref this.textBox_CurrentA, a);
                                    DelegateFactory.showDataInTextBox(ref this.textBox_CurrentB, b);
                                    DelegateFactory.showDataInTextBox(ref this.text_Height, height);
                                    

                                    // 委托调用其他线程创建的控件显示数据。
                                    DelegateFactory.showDataInRichTextBox(ref textBoxRecvData, insertWithSpace(head.ToUpper() + asciiString.Substring(3)));

                                    DelegateFactory.showDataInTextBox(ref textBoxRecvDataLength, recvDataLength.ToString());

                                    DelegateFactory.showDataInTextBox(ref textBoxRecvDataTime, recvDataDateTime.ToString());
                                }
                            }
                            // 清空缓冲区
                            buffer.RemoveRange(0, completedLen);
                        }
                        else
                        {
                            // 报文头不是FF01，remove头部两个字节
                            buffer.RemoveRange(0, 2);
                        }
                    }
                    
                }
                catch (SocketException)
                {
                    MessageBox.Show("连接已关闭");
                }
            }
        }


        /// <summary>
        /// 委托获取textBoxSendData的文本
        /// </summary>
        /// <returns></returns>
        private delegate byte[] getBytesDelegate();
        private byte[] getBytes()
        {
            if (this.textBoxSendData.InvokeRequired)
            {
                getBytesDelegate d = getBytes;
                return (byte[])this.textBoxSendData.Invoke(d);
            }
            else
            {
                List<Byte> bytes = new List<byte>();
                bytes.Add(1);
                bytes.AddRange(Encoding.UTF8.GetBytes(START_MSG));
                return bytes.ToArray();
            }
        }


        /// <summary>
        /// 在字符串中插入空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string insertWithSpace(string str)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            for (i = 0; i < str.Length - 2; i += 2)
            {
                sb.Append(str.Substring(i, 2) + " ");
            }
            sb.Append(str.Substring(i, 2));
            return sb.ToString();
        }
        

        /// <summary>
        /// 向 色差仪 发送数据，主要发送 命令报文
        /// </summary>
        /// <param name="bytes">发送的数据 byte[]</param>
        public void sendData(byte[] bytes)
        {
            try
            {
                socketClient.Send(bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 关闭socket连接
        /// </summary>
        public void closeConnection()
        {
            threadRecvDataFlag = false;

            DelegateFactory.changeDataInLabel(ref this.label_ConnectStatus, "已断开");
            DelegateFactory.changeColorInLable(ref this.label_ConnectStatus, Color.Red);

            this.socketClient.Close();
        }


        /// <summary>
        /// 得到本地时间
        /// </summary>
        /// <returns>DateTime.Now</returns>
        public DateTime getCurrentTime()
        {
            return DateTime.Now;
        }


        /// <summary>
        /// 得到本地IPv4地址
        /// </summary>
        /// <returns>本地IPAddress</returns>
        public IPAddress getLocalIPv4Address()
        {
            IPAddress localIPv4 = null;
            IPAddress[] Iplist = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress IP in Iplist)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIPv4 = IP;
                }
                else
                {
                    continue;
                }
            }
            return localIPv4;
        }


        /// <summary>
        /// 得到远程服务器地址IPAddress
        /// </summary>
        /// <returns>服务器IPAddresss</returns>
        public IPAddress getRemoteIPv4Address()
        {
            return this.ipAddress;
        }


        /// <summary>
        /// 得到服务器IPEndPoint
        /// </summary>
        /// <returns>服务器IPEndPoint</returns>
        public IPEndPoint getRemoteEndPoint()
        {
            return this.endPoint;
        }
    }
}
