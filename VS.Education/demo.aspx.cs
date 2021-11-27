using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal1.Text = MoreAll.MoreAll.GetCache("MenuMobile", HttpContext.Current.Cache["MenuMobile"] != null ? "" : "");
            Literal2.Text = MoreAll.MoreAll.GetCache("Menuleft", HttpContext.Current.Cache["Menuleft"] != null ? "" : "");
            Literal3.Text = MoreAll.MoreAll.GetCache("MenuTop", HttpContext.Current.Cache["MenuTop"] != null ? "" : "");
            Literal4.Text = MoreAll.MoreAll.GetCache("Sildechinh", HttpContext.Current.Cache["Sildechinh"] != null ? "" : "");
            Literal5.Text = MoreAll.MoreAll.GetCache("2anhduoibanner", HttpContext.Current.Cache["2anhduoibanner"] != null ? "" : "");
            Literal6.Text = MoreAll.MoreAll.GetCache("Anhbenphai", HttpContext.Current.Cache["Anhbenphai"] != null ? "" : "");
            Literal7.Text = MoreAll.MoreAll.GetCache("LoadCoTheBanThich", HttpContext.Current.Cache["LoadCoTheBanThich"] != null ? "" : "");
            Literal8.Text = MoreAll.MoreAll.GetCache("AGShowNhomsanpham", HttpContext.Current.Cache["AGShowNhomsanpham"] != null ? "" : "");
            Literal9.Text = MoreAll.MoreAll.GetCache("Showtab", HttpContext.Current.Cache["Showtab"] != null ? "" : "");
            Literal10.Text = MoreAll.MoreAll.GetCache("ShowLoadPro", HttpContext.Current.Cache["ShowLoadPro"] != null ? "" : "");
            Literal11.Text = MoreAll.MoreAll.GetCache("ShowPhuogThucThanhToan", HttpContext.Current.Cache["ShowPhuogThucThanhToan"] != null ? "" : "");
            Literal12.Text = MoreAll.MoreAll.GetCache("MenuDuoi", HttpContext.Current.Cache["MenuDuoi"] != null ? "" : "");
            Literal13.Text = MoreAll.MoreAll.GetCache("MenuBottom", HttpContext.Current.Cache["MenuBottom"] != null ? "" : "");
        }
    }
}