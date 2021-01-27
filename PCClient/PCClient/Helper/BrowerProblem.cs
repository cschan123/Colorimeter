using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

using System.ComponentModel;
namespace PCClient.Helper
{
    class BrowerProblem
    {
        /// <summary>
        /// 修改浏览器兼容问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetWebBrowserFeatures(int ieVersion)
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
        public int GetBrowserVersion()
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
        public UInt32 GeoEmulationModee(int browserVersion)
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



        /// <summary>
        /// Echart 展示
        /// </summary>
        //private void getAllHtmlFile()
        //{
        //    //获取指定文件夹的所有文件  
        //    string[] paths = Directory.GetFiles(HighchartFile);
        //    foreach (var item in paths)
        //    {
        //        //获取文件后缀名  
        //        string extension = Path.GetExtension(item).ToLower();
        //        if (extension == ".html")
        //        {
        //            comboBox_Selectchart.Items.Add(Path.GetFileName(item));
        //        }
        //    }

        //    if (comboBox_Selectchart.Items.Count > 0)
        //    {
        //        comboBox_Selectchart.SelectedIndex = 0;
        //        this.wb_fullchart.Url = new Uri(str + "\\" + comboBox_Selectchart.Text.Trim());
        //    }
        //}
        //private void comboBox_Selectchart_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    string name = comboBox_Selectchart.Text.Trim();
        //    //this.wb_fullchart.Url = new Uri(str + "\\" + comboBox_Selectchart.Text.Trim());
        //    this.wb_fullchart.Url = new Uri(fullchart);

        //}


        //public class SeriesItem
        //{

        //    private char p4;
        //    private char p5;
        //    private int p6;
        //    private string[] myArray1;



        //    public SeriesItem(char p4, char p5, int p6, string[] myArray1)
        //    {
        //        // TODO: Complete member initialization
        //        this.p4 = p4;
        //        this.p5 = p5;
        //        this.p6 = p6;
        //        this.myArray1 = myArray1;
        //    }
        //    /// <summary>
        //    /// 每个系列通过 type 决定自己的图表类型
        //    /// </summary>
        //    public string type { get; set; }
        //    public string name { get; set; }
        //    public int yAxisIndex { get; set; }
        //    public List<int> data { get; set; }

        //}
        //private void loadchart()
        //{
        //    List<SeriesItem> serisConntent = new List<SeriesItem>();
        //    string[] myArray = new string[] { "1", "2", "3", "4" };
        //    SeriesItem sc1 = new SeriesItem('r', 'f', 0, myArray);

        //    serisConntent.Add(sc1);


        //    // Xdata.Add(i);

        //}
    }
}
