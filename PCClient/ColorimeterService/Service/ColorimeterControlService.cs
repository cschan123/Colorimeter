using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorimeterService.Service
{
    /// <summary>
    /// 色差仪控件服务接口
    /// </summary>
    public interface ColorimeterControlService
    {
        /// <summary>
        /// 获取本地IPv4地址
        /// </summary>
        /// <returns>本地IPAddress</returns>
         IPAddress getLocalIPv4Address();

        /// <summary>
        /// 获取远程服务器IPv4地址
        /// </summary>
        /// <returns>服务器IPAddress</returns>
         IPAddress getRemoteIPv4Address();

        /// <summary>
        /// 获取远程服务器端口EndPoint
        /// </summary>
        /// <returns>服务器EndPoint</returns>
         IPEndPoint getRemoteEndPoint();

        /// <summary>
        /// 连接服务器，连接成功时将连接按钮enabled=false，
        /// 同时开启线程接收数据，接收数据线程需要自己实现
        /// </summary>
         void connectServer();

        /// <summary>
        /// 关闭服务器连接
        /// </summary>
         void closeConnection();

        /// <summary>
        /// 向服务器发送数据
        /// </summary>
         void sendData(byte[] bytes);

        /// <summary>
        /// 获取本地当前时间
        /// </summary>
        /// <returns>DateTime</returns>
         DateTime getCurrentTime();
    }
}
