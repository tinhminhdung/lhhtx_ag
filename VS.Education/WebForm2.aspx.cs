using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DateTime dt1 = DateTime.Parse("10/10/2020");
            //DateTime dt2 = DateTime.Now;
            //if (dt1.Date > dt2.Date)
            //{
            //    Response.Write("dt1.Date > dt2.Date");
            //    //Đó là một ngày sau đó
            //}
            //else
            //{
            //    Response.Write(" else dt1.Date > dt2.Date");
            //    //Đó là một ngày sớm hơn hoặc bằng
            //}

            //DateTime NgayVao = Convert.ToDateTime("02/03/2020");
            //DateTime NgayHienTai = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
            //Response.Write("NgayVao: " + NgayVao + "<br>");
            //Response.Write("NgayHienTai:" + NgayHienTai + "<br>");

            //if (NgayVao.Date <= NgayHienTai.Date)
            //{
            //    Response.Write("Hết hạn");
            //}
            //else
            //{
            //    Response.Write(" Còn hạn");
            //}

            ////TimeSpan Time = NgayHienTai - NgayVao;
            ////int TongSoNgay = Time.Days;
            //Response.Write("<br>");
            //Response.Write(DateGreaterOrEqual(NgayVao, NgayHienTai));
            //Response.Write("<br>");
            //Response.Write(DateLessOrEqual(NgayVao, NgayHienTai));
            //Response.Write("----<br>");
            //Response.Write(DateGreaterOrEqual(NgayHienTai, NgayVao));
            //Response.Write("<br>");
            //Response.Write(DateLessOrEqual(NgayHienTai, NgayVao));
        }

        private bool DateGreaterOrEqual(DateTime dt1, DateTime dt2)
        {
            return DateTime.Compare(dt1.Date, dt2.Date) >= 0;
        }

        private bool DateLessOrEqual(DateTime dt1, DateTime dt2)
        {
            return DateTime.Compare(dt1.Date, dt2.Date) <= 0;
        }
        public static string Date_Time(string DateKetthuc)
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = Convert.ToDateTime(DateKetthuc.Trim()).Date;
            if (dt1 >= dt2)
            {
                return ("Ngày hợp lệ");
            }
            else
            {
                return ("Ngày không hợp lệ ... Vui lòng cho biết ngày chính xác ....");
            }
        }
        public static string VipDateTime(string DateKetthuc)
        {
            DateTime oldDate = Convert.ToDateTime(DateKetthuc);
            DateTime newDate = DateTime.Now;
            if (DateTime.Compare(oldDate.Date, newDate.Date) >= 0)
            {
                return ("Hết hạn");
            }
            else
            {
                return ("Còn hạn");
            }
        }
        public static string DateTimess(string DateKetthuc)
        {
            DateTime oldDate = Convert.ToDateTime(DateKetthuc);
            DateTime newDate = DateTime.Now;
            TimeSpan ts = newDate - oldDate;
            if (ts.Days > 365)
            {
                return ("Hết hạn");
            }
            else
            {
                return ("Còn hạn");
            }
           // return ts.Days.ToString();
        }


    }
}