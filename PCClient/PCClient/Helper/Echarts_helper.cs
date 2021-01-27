using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCClient.UIFrame.Check;
using Newtonsoft.Json;

using System.IO;

namespace Echarts_demo
{
     [System.Runtime.InteropServices.ComVisible(true)]
    class Echarts_helper
    {



        /// <summary>  
        /// 修改注册表信息来兼容当前程序  
        ///   
        /// </summary>  
        static void SetWebBrowserFeatures(int ieVersion)
        {
            // don't change the registry if running in-proc inside Visual Studio  
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
                return;
            //获取程序及名称  
            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            //得到浏览器的模式的值  
            UInt32 ieMode = GeoEmulationModee(ieVersion);
            var featureControlRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\";
            //设置浏览器对应用程序（appName）以什么模式（ieMode）运行  
            Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION",
                appName, ieMode, RegistryValueKind.DWord);
            // enable the features which are "On" for the full Internet Explorer browser  
            //不晓得设置有什么用  
            Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION",
                appName, 1, RegistryValueKind.DWord);

          
        }
        /// <summary>  
        /// 获取浏览器的版本  
        /// </summary>  
        /// <returns></returns>  
        static int GetBrowserVersion()
        {
            int browserVersion = 0;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }
            //如果小于7  
            if (browserVersion < 10)
            {
                throw new ApplicationException("no support ie");
            }
            return browserVersion;
        }
        /// <summary>  
        /// 通过版本得到浏览器模式的值  
        /// </summary>  
        /// <param name="browserVersion"></param>  
        /// <returns></returns>  
        static UInt32 GeoEmulationModee(int browserVersion)
        {
            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode.   
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.   
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.   
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                      
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10.  
                    break;
                case 11:
                    mode = 11000; // Internet Explorer 11  
                    break;
            }
            return mode;
        }
         [System.Runtime.InteropServices.ComVisible(true)]
        public string echarts_init(WebBrowser wb, Checkframe eh)
        {
            //首先判断系统浏览器支不支持相关操作
            try
            {
                GetBrowserVersion();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "index4.html";
            SetWebBrowserFeatures(10);
            wb.Url = new Uri(path);
            wb.DocumentCompleted += webBrowser1_DocumentCompleted;
            //防止 WebBrowser 控件打开拖放到其上的文件。
            wb.AllowWebBrowserDrop = false;

            //防止 WebBrowser 控件在用户右击它时显示其快捷菜单.
            wb.IsWebBrowserContextMenuEnabled = false;

            //以防止 WebBrowser 控件响应快捷键。
            wb.WebBrowserShortcutsEnabled = false;

            //以防止 WebBrowser 控件显示脚本代码问题的错误信息。    
            wb.ScriptErrorsSuppressed = false;

            //（这个属性比较重要，可以通过这个属性，把WINFROM中的变量，传递到JS中，供内嵌的网页使用；但设置到的类型必须是COM可见的，所以要设置     [System.Runtime.InteropServices.ComVisibleAttribute(true)]，因为我的值设置为this,所以这个特性要加载窗体类上）
            wb.ObjectForScripting = eh;
            wb.SizeChanged += webBrowser1_SizeChanged;

            return "successed";
        }


        /// <summary>
        /// 改变大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void webBrowser1_SizeChanged(object sender, EventArgs e)
         {
             WebBrowser webBrowser1 = (WebBrowser)sender;
             string size_str = "[" + webBrowser1.Height.ToString() + "," + webBrowser1.Width.ToString() + "]"; //个列已[]分开
             HtmlDocument htmlDocument = webBrowser1.Document;
             object[] objArray = new object[] { size_str };
             htmlDocument.InvokeScript("setPosition", objArray);
         }

        //网页加载完成
         private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
         {

             ////加载完成更新一下显示区域
             WebBrowser wb = (WebBrowser)sender;
             HtmlDocument hd = wb.Document;
             int height = Convert.ToInt32(hd.Body.GetAttribute("scrollHeight")); //;
             int width = Convert.ToInt32(hd.Body.GetAttribute("scrollWidth")); // ;
             wb.Height = height;
             wb.Width = width;
         }




        /// <summary>
        /// 获取网页图片
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
         private Bitmap SnapWeb(WebBrowser wb)
         {
             HtmlDocument hd = wb.Document;
             int height = Convert.ToInt32(hd.Body.GetAttribute("scrollHeight")) +10; //;
             int width = Convert.ToInt32(hd.Body.GetAttribute("scrollWidth")); //;
             wb.Height = height;
             wb.Width = width;
             
             Bitmap bmp = new Bitmap(width, height);
             Rectangle rec = new Rectangle();
             rec.Width = width;
             rec.Height = height;
             wb.DrawToBitmap(bmp, rec);
             return bmp;
         }

         [DllImportAttribute("gdi32.dll")]



         private static extern bool BitBlt(

             IntPtr hdcDest, // 目标 DC的句柄

             int nXDest,

             int nYDest,

             int nWidth,

             int nHeight,

             IntPtr hdcSrc,  // 源DC的句柄

             int nXSrc,

             int nYSrc,

             System.Int32 dwRop  // 光栅的处理数值

             );
        /// <summary>
        ///保存网页图片到指定路径
        /// </summary>
        /// <param name="save_path">路径</param>
         public void save_wb_image(WebBrowser wb, string save_path)
         {
             //Bitmap bit = SnapWeb(wb);
             string time_str = DateTime.Now.ToString("yyyyMMddHHmmss");
             //bit.Save(save_path + time_str + ".png");
             //bit.Dispose();
            // int height = wb.Height; //;
            // int width = wb.Width; //;
            // Bitmap bit = new Bitmap(width, height);//实例化一个和窗体一样大的bitmap
            // Graphics g = Graphics.FromImage(bit);
            //// g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
            // var screenPoint = wb.PointToScreen(wb.Location);
            // g.CopyFromScreen(wb.PointToScreen(wb.Location), Point.Empty,  wb.Size);//保存整个窗体为图片
            // //g.CopyFromScreen(panel游戏区 .PointToScreen(Point.Empty), Point.Empty, panel游戏区.Size);//只保存某个控件（这里是panel游戏区）
            // bit.Save(save_path + time_str + ".png");//默认保存格式为PNG，保存成jpg格式质量不是很好
            // bit.Dispose();

             //获得当前屏幕的大小

             Rectangle rect = new Rectangle();

             rect = Screen.GetWorkingArea(wb);

             //创建一个以当前屏幕为模板的图象

             Graphics g1 = wb.CreateGraphics();

             //创建以屏幕大小为标准的位图 

             Image myImage = new Bitmap(wb.Width, wb.Height, g1);

             Graphics g2 = Graphics.FromImage(myImage);

             //得到屏幕的DC

             IntPtr dc1 = g1.GetHdc();

             //得到Bitmap的DC 

             IntPtr dc2 = g2.GetHdc();

             //调用此API函数，实现屏幕捕获

             BitBlt(dc2, 0, 0, rect.Width, rect.Height, dc1, wb.Location.X, wb.Location.Y, 13369376);

             //释放掉屏幕的DC

             g1.ReleaseHdc(dc1);

             //释放掉Bitmap的DC 

             g2.ReleaseHdc(dc2);

             myImage.Save(save_path + time_str + ".png", ImageFormat.Png);
         }


        /// <summary>
        /// 设置起始通道 
        /// </summary>
        /// <param name="start_pos">通道号，注意从1开始， 依次8通道显示</param>
         public void set_channel_start(WebBrowser wb,int start_pos)
         {
             HtmlDocument htmlDocument = wb.Document;
             object[] objArray_start_channel = new object[] { start_pos };
             htmlDocument.InvokeScript("set_start_channel", objArray_start_channel);
         }


        /// <summary>
        /// 设置的y轴的字符,异步执行
        /// </summary>
        /// <param name="wb">控件</param>
         /// <param name="y_data">y轴数据</param>
         public static void set_y_data(WebBrowser wb, List<string>[] y_data, int start_pos)
         {
          ////   await Task.Run(() =>
           //  {
                 HtmlDocument htmlDocument = wb.Document;
                 string option = "[";
                 for (int i = start_pos; i < start_pos+8; i++)
                 {
                     if (i < start_pos + 7)
                         option += "[" + String.Join(",", y_data[i].ToArray()) + "],";
                     else if (i == start_pos + 7)
                         option += "[" + String.Join(",", y_data[i].ToArray()) + "]";

                 }
                 option += "]";
                 object[] objArray = new object[] { option };
                 htmlDocument.InvokeScript("showChart", objArray);
            // });
            
         }

         /// <summary>
         /// 设置的x轴的字符
         /// </summary>
         /// <param name="wb">控件</param>
         /// <param name="y_data">x轴数据</param>
         public void set_x_data(WebBrowser wb, string[] x_data)
         {
             try {
                 HtmlDocument htmlDocument = wb.Document;
                 object[] objArray_x = new object[] { String.Join(",", x_data.ToArray()) };
                 htmlDocument.InvokeScript("set_x", objArray_x);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("---------------------" + ex.Message + "------------------------------");
             }
         
         
         }

         /// <summary>
         /// 设置的x轴的标题
         /// </summary>
         /// <param name="wb">控件</param>
         /// <param name="y_data">x轴数据</param>
         public void set_x_title(WebBrowser wb, string x_data)
         {
             HtmlDocument htmlDocument = wb.Document;
             object[] objArray_x = new object[] { x_data };
             htmlDocument.InvokeScript("set_x_title", objArray_x);


         }

         public void test(WebBrowser wb, string[] data)
         {
             HtmlDocument htmlDocument = wb.Document;
             htmlDocument.InvokeScript("showChart", new object[] {});

         }

         /// <summary>
         /// 设置的y轴的标题
         /// </summary>
         /// <param name="wb">控件</param>
         /// <param name="y_data">x轴数据</param>
         public void set_y_title(WebBrowser wb, string y_data)
         {
             HtmlDocument htmlDocument = wb.Document;
             object[] objArray_x = new object[] { y_data };
             htmlDocument.InvokeScript("set_y_title", objArray_x);


         }
    }
}
