using ColorimeterDAO.Dao;
using ColorimeterDAO.Dao.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorimeterService.Service.impl
{
    /// <summary>
    /// 2 标样板校验模式
    /// </summary>
    public class StandardTemplateColorStandard : AbstractColorStandard
    {
        private string NEED_INDICES_CD = "2004804";

        public StandardTemplateColorStandard()
        {
            standardSrcMode = 2;// 标准值来源设为2
        }
        /// <summary>
        /// 标样板校验模式处理逻辑
        /// </summary>
        public override bool process(string colorCode, out float l_star, out float a_star, out float b_star)
        {
            if (!verify())
            {
                MessageBox.Show("填写不完整!");
                l_star = a_star = b_star = float.NaN;
                return false;
            }
            //if (!match(colorCode, csDomain.GDB1,out l_star, out b_star, out a_star))
            //{
            //    if (!match(colorCode, csDomain.GDB2, out l_star, out b_star, out a_star))
            //    {
            //        if (!match(colorCode, csDomain.GDB3, out l_star, out b_star, out a_star))
            //        {
            //            return false;
            //        }
            //    }
            //}

            throw new NotImplementedException();
        }


        public override bool verify()
        {
            return this.verify(csDomain);
        }

        //private bool match(string currColorCode, ColorimeterDAO.WinDomain.ColorStandardDomain.ColorStandardClass GDB,
        //    out float l_star, out float a_star, out float b_star)
        //{
        //    throw new NotImplementedException();
        //    if (GDB.colorCode.Equals(currColorCode))
        //    {
                
        //        l_star = float.Parse(GDB.l_star);
        //        b_star = float.Parse(GDB.b_star);
        //        a_star = float.Parse(GDB.a_star);
        //        if (csDomain.ifLabDataBaseCorrect & NEED_INDICES_CD.Equals(GDB.colorCode))
        //        {
        //            labDataBaseCorrect(out l_star,out a_star, out b_star);
        //        }
        //        l_star = b_star = a_star = float.NaN;
        //        return true;
        //    }
        //    l_star = b_star = a_star = float.NaN;
        //    return false;
        //}

        private void labDataBaseCorrect(out float l_star, out float a_star, out float b_star)
        {
            // TODO 在放行标准表中寻找对应的偏移值，对原始值进行偏移。
            PassStdDao psDao = new PassStdDaoImpl();
            throw new NotImplementedException();
        }

        public override bool verify(ColorimeterDAO.WinDomain.ColorStandardDomain csDomain)
        {
            throw new NotImplementedException();
        }
    }
}
