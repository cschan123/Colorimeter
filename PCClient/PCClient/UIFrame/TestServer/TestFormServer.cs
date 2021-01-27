using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCClient
{
    public partial class TestFormServer : Form
    {
        public TestFormServer()
        {
            InitializeComponent();
            ListBox listBox11 = (ListBox)this.panel1.Controls.Find("listBox1", false)[0];
            listBox11.Items.Add("hi");
        }

        private Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Socket clientSocket = null;

        private float Hex2Float(string s)
        {
            var i = Convert.ToInt32(s, 16);
            var bytes = BitConverter.GetBytes(i);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// 浮点数转换为16进制字符串
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private string Float2Hex(float f)
        {
            var bytes = BitConverter.GetBytes(f);
            var i = BitConverter.ToInt32(bytes, 0);
            return /* "0x" + */
                i.ToString("X8");
        }
        Random rand = new Random();
        private string RandomGenerateColor()
        {
            float f = (float)(rand.NextDouble() * rand.Next(100));
            return Float2Hex(f);
        }

        int i = -1;
        bool direction = true;
        float f = 0.0f;

        private string RandomGenerateHeight()
        {

            if (f >= 600) direction = false;
            if (f <= 0) direction = true;

            if (direction)
            {
                 f = (float)((++i)  * 100);
                 if (f > 600) f = 600;
            }
            else
            {
                f = (float)((--i)  * 100);
                 if (f < 0) f = 0;
            }
            return Float2Hex(f);
        }


        /// <summary>
        /// 16进制数字符串转ASCII码字符串
        /// </summary>
        /// <param name="hexNumString"></param>
        /// <returns>16进制字符串</returns>
        private string hexStr2AsciiStr(string hexNumString)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(hexNumString);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x"));
            }
            return sb.ToString();
        }
       
        /// <summary>
        /// ASCII码字符串转16进制字符串
        /// </summary>
        /// <param name="asciiString"></param>
        /// <returns>ASCII码字符串</returns>
        private string asciiStr2HexStr(string asciiString)
        {
            byte[] buff = new byte[asciiString.Length / 2];
            int index = 0;
            for (int i = 0; i < asciiString.Length; i+=2)
            {
                buff[index] = Convert.ToByte(asciiString.Substring(i, 2), 16);
                ++index;
            }
            return Encoding.ASCII.GetString(buff);
        }

        /// <summary>
        /// 随机生成报文数据方法
        /// </summary>
        /// <returns>随机报文字符串</returns>
        private String GenerateData()
        {
            StringBuilder data = new StringBuilder(64);
          //   string header = "FF01"; 
          //  string hexadecimal = "0x01"; // 没有这一个
            string count = "03A";
            string msgno = "0";
            string H = "H";
            string status = "0000";
            string messageNum = "00000017"; // 报文数
            string measurementQuantity = "0005"; // 测量数量
            string colorStatus = "2"; // 颜色状态
            string colorValue = RandomGenerateColor() + RandomGenerateColor() + RandomGenerateColor(); // 随机生成颜色值
            string index = "00000000"; // 索引
            string height = RandomGenerateHeight(); // 高度
            string end = "0D32";

            data
                //.Append(header)
                //.Append(hexadecimal)
                .Append(count)
                .Append(msgno)
                .Append(H)
                .Append(status)
                .Append(messageNum)
                .Append(measurementQuantity)
                .Append(colorStatus)
                .Append(colorValue)
                .Append(index)
                .Append(height)
                .Append(end);

            return data.ToString();
        }

        private void uiButton_GetLocalIPAddress_Click(object sender, EventArgs e)
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
            this.uiTextBox_LocalIPAddress.Text = localIPv4.ToString();
        }

        private void uiButton_openServer_Click(object sender, EventArgs e)
        {
            try
            {
                serverSocket.Bind(new IPEndPoint(IPAddress.Parse(this.uiTextBox_LocalIPAddress.Text),
                    int.Parse(this.uiTextBox_Port.Text)));
                serverSocket.Listen(10);
                Thread thread = new Thread(watchConnecting);
                thread.IsBackground = true;
                thread.Start();
                this.uiRichTextBox_RecvDataMessage.AppendText("开始接收客户端数据...\r\n");
                this.uiButton_openServer.Enabled = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private delegate void InvokeAppendTextActionDelegate(UIRichTextBox txtBox, string message);
        private void InvokeAppendTextAction(UIRichTextBox txtBox, string message)
        {
            if (txtBox.InvokeRequired)
            {
                InvokeAppendTextActionDelegate d = InvokeAppendTextAction;
                txtBox.Invoke(d, txtBox, message);
            }
            else
            {
                txtBox.AppendText(message);
            }
        }

        private delegate void InvokeClearTextBoxDelegate(UIRichTextBox box);
        private void InvokeClearTextBox(UIRichTextBox box)
        {
            if (box.InvokeRequired)
            {
                InvokeClearTextBoxDelegate d = InvokeClearTextBox;
                box.Invoke(d, box);
            }
            else
            {
                box.ClearUndo();
            }
        }

        private delegate void InvokeAppendListBoxActionDelegate(UIListBox box, string message);
        private void InvokeAppendListBoxAction(UIListBox box, string message)
        {
            if (box.InvokeRequired)
            {
                InvokeAppendListBoxActionDelegate d = InvokeAppendListBoxAction;
                box.Invoke(d, box, message);
            }
            else
            {
                box.Items.Add(message);
                box.SelectedIndex = box.Items.Count - 1;
                
            }
        }

        /// <summary>
        /// 监听客户端发来的请求
        /// </summary>
        private void watchConnecting()
        {
            while (true)
            {
                clientSocket = serverSocket.Accept();
                InvokeAppendTextAction(this.uiRichTextBox_RecvDataMessage, "客户端连接成功！\r\n");
                Thread thr = new Thread(serverRecvData);
                thr.IsBackground = true;
                thr.Start(clientSocket);
            }
        }

        private void serverRecvData(object _clientSocket)
        {
            Socket clientSocket = _clientSocket as Socket;
            while (true)
            {
                //创建一个内存缓冲区
                byte[] arrServerRecMsg = new byte[1024];
                try
                {
                    //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度
                    int length = clientSocket.Receive(arrServerRecMsg);
                    //将机器接受到的字节数组转换为人可以读懂的字符串
                    string strRecMsg = Encoding.ASCII.GetString(arrServerRecMsg, 0, length);
                    //将发送的字符串信息附加到文本框txtMsg上  
                    InvokeAppendTextAction(this.uiRichTextBox_RecvDataMessage, "Data: " + DateTime.Now + "\r\n" + strRecMsg + "\r\n");
                }
                catch (Exception ex)
                {
                    InvokeAppendTextAction(this.uiRichTextBox_RecvDataMessage, "客户端已断开连接！" + "\r\n" + ex.Message + "\r\n");
                    break;
                }
            }
        }

        System.Timers.Timer timer;
        private void uiButton_RecurSendData_Click(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer(1000);
            this.uiButton_GenerateAndSendData.Enabled = false;
            this.uiButton_RecurSendData.Enabled = false;
            this.uiButton_StopGenerateAndSendData.Enabled = true;
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = false;
            timer.Start();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            String data = "FF01" + hexStr2AsciiStr(GenerateData());
            sendData(data);
        }

        private void uiButton_GenerateAndSendData_Click(object sender, EventArgs e)
        {
            string randomStr = GenerateData();
            string asciiString = "FF01" +  hexStr2AsciiStr(randomStr) + "FF01" + hexStr2AsciiStr(GenerateData().Substring(0, 10));
            this.uiRichTextBox_SendData.Text = "\r\n第一次send：字节长度(68+10)" + asciiString;
            sendData(asciiString);

            this.uiRichTextBox_SendData.AppendText("\r\n等待2s...");

            InvokeAppendListBoxAction(this.uiListBox_SendDataMessage, "等待2s...\r\n");

            Thread.Sleep(2000);
            string randomStr2 = GenerateData().Substring(10, 56);
            this.uiRichTextBox_SendData.AppendText("\r\n第二次send：字节长度(58)" + randomStr2);
            sendData(hexStr2AsciiStr(randomStr2));
        }

        // 10进制浮点数 --> 16进制字符串 --> ASCII码字符串 --> byte[] 色差仪服务器端
        // byte[] --> ASCII字符串 --> 16进制字符串 --> 10进制浮点数  PC客户端

        /// <summary>
        /// 将ASCII码字符串转换为 byte[]，两个字符编码为一个byte
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] str2bytes(string data)
        {
            List<Byte> byteList = new List<Byte>();
            for (int i = 0; i < data.Length - 1; i += 2)
            {
                string sub = data.Substring(i, 2);
                Byte b = Convert.ToByte(sub, 16);
                byteList.Add(b);
            }
            byte[] byteArray = byteList.ToArray();
            return byteArray;
        }



        private void sendData(string data) // data 是（FF01） 30 33 41 30...这样的FF01 + ASCII码字符串
        {
            try
            {
                byte[] arrSendData = str2bytes(data);
                clientSocket.Send(arrSendData);
                InvokeAppendListBoxAction(this.uiListBox_SendDataMessage, "服务器：" + DateTime.Now + "\r\n");
                InvokeAppendListBoxAction(this.uiListBox_SendDataMessage, "发送数据报文：" + data + "\r\n" + "数据字节长度: " + arrSendData.Length);
                InvokeAppendListBoxAction(this.uiListBox_SendDataMessage, "数据字节长度: " + arrSendData.Length + "\r\n");
            }
            catch (Exception ex)
            {
                InvokeAppendListBoxAction(this.uiListBox_SendDataMessage, "客户端已断开，无法发送数据！" + ex.Message + "\r\n");
            }
        }

        private void uiButton_StopGenerateAndSendData_Click(object sender, EventArgs e)
        {
            this.uiButton_StopGenerateAndSendData.Enabled = false;
            this.uiButton_GenerateAndSendData.Enabled = true;
            this.uiButton_RecurSendData.Enabled = true;
            timer.Stop();
        }

        private void testRef()
        {
            
        }
    }
}
