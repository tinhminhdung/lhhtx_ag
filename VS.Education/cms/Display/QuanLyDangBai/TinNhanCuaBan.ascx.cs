using Entity;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWindowService;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class TinNhanCuaBan : System.Web.UI.UserControl
    {

        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            if (!base.IsPostBack)
            {
                ltthongbaonhaCC.Text += ThongBaoTungNhaCC();
            }
        }
        protected string ThongBaoTungNhaCC()
        {
            string str = "";
            // Tìm ví xem có giao dịch nào đủ điều kiện ko 
            List<Notification> List = db.Notifications.Where(s => s.IDThanhVienNhanThongBao == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()) && s.TrangThai == 0).ToList();
            if (List.Count > 0)
            {
                foreach (var item in List)
                {
                    str += item.NoiDung;
                    str += "<div style=\"text-align: center;\"> <a class='btn btn-hai btn-primary'  onclick=\"XacNhanNhaCC('" + item.ID.ToString() + "')\"  target=\"_blank\">Xác nhận thông tin này</a></div>";
                    str += "<div style=\"clear:both; height:10px;\"></div>";
                }
            }
            return str;
        }
    }
}