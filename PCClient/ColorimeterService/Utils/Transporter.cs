using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorimeterService.Utils
{
    public static class Transporter
    {
        /// <summary>
        /// 将16进制字符串转换为10进制单精度浮点数
        /// </summary>
        /// <param name="s">待32位16进制转换字符串</param>
        /// <returns>10进制浮点数</returns>
        public static float Hex2Float(string s)
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
        public static string Float2Hex(float f)
        {
            var bytes = BitConverter.GetBytes(f);
            var i = BitConverter.ToInt32(bytes, 0);
            return /*"0x" + */  i.ToString("X8");
        }


        /// <summary>
        /// 16进制数字符串转ASCII码字符串
        /// </summary>
        /// <param name="hexNumString"></param>
        /// <returns>16进制字符串</returns>
        public static string hexStr2AsciiStr(string hexNumString)
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
        public static string asciiStr2HexStr(string asciiString)
        {
            byte[] buff = new byte[asciiString.Length / 2];
            int index = 0;
            for (int i = 0; i < asciiString.Length; i += 2)
            {
                buff[index] = Convert.ToByte(asciiString.Substring(i, 2), 16);
                ++index;
            }
            return Encoding.ASCII.GetString(buff);
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 将byte[] 转换为 ASCII码 字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string bytes2str(byte[] data, int len)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                byte b = data[i];
                string c = Convert.ToString(b, 16);
                str.Append(c);
            }
            return str.ToString();
        }

        /// <summary>
        /// 解析报文字符串，获得 颜色值l,a,b和高度h，封装为对象 ParsedData
        /// </summary>
        /// <param name="ori">16进制ASCII字符串数据报文
        /// 03A 0 H 0000 0000 0017 0005 2 42238E9B 3D7940DD  4282D458 0000 0000 0000 0000 0D32</param>
        /// <returns></returns>
        public static ParsedData GetParsedData(String ori)
        {
            String color = ori.Substring(22, 24);
            String _height = ori.Substring(22 + 24 + 8, 8);
            String _l = color.Substring(0, 8);
            String _a = color.Substring(8, 8);
            String _b = color.Substring(16, 8);

            float _l_f = Hex2Float(_l);
            float _a_f = Hex2Float(_a);
            float _b_f = Hex2Float(_b);
            float _h_f = Hex2Float(_height);

            ParsedData ret = new ParsedData() { l = _l_f, a = _a_f, b = _b_f, height = _h_f };

            return ret;
        }

        /// <summary>
        /// 解析封装对象
        /// 颜色 l a b
        /// 高度 height
        /// </summary>
        public class ParsedData
        {
            public float l { get; set; }
            public float a { get; set; }
            public float b { get; set; }
            public float height { get; set; }
        }
    }
}
