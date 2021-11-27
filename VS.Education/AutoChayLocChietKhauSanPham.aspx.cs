using Entity;
using Framwork;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class AutoChayLocChietKhauSanPham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //List<Entity.Products> iitem = SProducts.GetByAll("VIE");
            //if (iitem.Count() > 0)
            //{
            //    foreach (var item in iitem)
            //    {
            //        SProducts.Name_Text("update products set search=N'" + MoreAll.RewriteURLNew.NameSearch(item.Name) + "'  where ipid=" + item.ipid + "");
            //        // SProducts.Name_Text("update products set PhanTramChietKhauThanhVien=" + Commond.AddEdit_Giamgia(item.Price, item.Giacongtynhapvao) + "  where ipid=" + item.ipid + "");
            //    }
            //}
        }
    }
}