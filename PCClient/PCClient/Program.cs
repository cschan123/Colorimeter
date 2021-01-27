using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HomeForm());



            //新建线程开启本地服务端，发送测试数据
            //Thread testFormThread = new Thread(runServer);
            //testFormThread.SetApartmentState(System.Threading.ApartmentState.STA);
            //testFormThread.Start();
            //Application.Run(new MainFormJJJP());
            
            //Application.Run(new LoginForm());
        }

        //static void runServer()
        //{
        //    Application.Run(new TestFormServer());
        //}
    }
}