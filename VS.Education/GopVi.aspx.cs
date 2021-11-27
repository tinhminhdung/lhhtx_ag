using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class GopVi : System.Web.UI.Page
    {
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                if (table != null)
                {
                    lten.Text = table.vfname + "/" + table.iuser_id;
                    ltvithuongmai.Text = table.TongTienCoinDuocCap;
                    ltviquanly.Text = table.VIAAFFILIATE;
                    ltvimuahang.Text = table.ViMuaHangAFF;
                    hdid.Value = table.iuser_id.ToString();
                }
            }
            catch (Exception)
            { }
        }

        protected void btgopvi_Click(object sender, EventArgs e)
        {

            Double ViThuongMai = Convert.ToDouble(ltvithuongmai.Text);
            Double ViQuanLy = Convert.ToDouble(ltviquanly.Text);
            Double ViMuaHang = Convert.ToDouble(ltvimuahang.Text);
            Double Tong = ViThuongMai + ViQuanLy + ViMuaHang;
            Susers.Name_Text("update users set TongTienCoinDuocCap=" + Tong.ToString() + ",VIAAFFILIATE=0,ViMuaHangAFF=0 where iuser_id=" + hdid.Value + "");

            Response.Write("<script type=\"text/javascript\">alert('Gôp ví thành công ');window.location.href='/GopVi.aspx'; </script>");
        }
    }
}