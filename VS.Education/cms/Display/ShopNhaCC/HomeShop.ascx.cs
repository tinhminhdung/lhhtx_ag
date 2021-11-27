using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.ShopNhaCC
{
    public partial class HomeShop : System.Web.UI.UserControl
    {
        string user = "";
        DatalinqDataContext db = new DatalinqDataContext();
        private string language = Captionlanguage.Language;
        private string Thanhvien = "";
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
            if (Request["user"] != null && !Request["user"].Equals(""))
            {
                user = Request["user"].ToString();
                Thanhvien = Request["user"].ToString();
            }
            if (!base.IsPostBack)
            {
                user table = db.users.SingleOrDefault(p => p.vuserun == user.ToString());
                if (table != null)
                {
                    List<Entity.CartDetail> dtcart = SCartDetail.Name_Text("select * from CartDetail where TrangThaiNhaCungCap=1 and TrangThaiNguoiMuaHang=1 and TrangThaiKhieuKien=0 and IDNhaCungCap=" + table.iuser_id.ToString() + "");
                    if (dtcart.Count > 0)
                    {
                        double num = 0.0;
                        for (int i = 0; i < dtcart.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart[i].Quantity.ToString());
                        }
                        ltspdaban.Text = num.ToString();
                    }
                    else
                    {
                        ltspdaban.Text = "0";
                    }
                    lttenshop1.Text = lttenshop.Text = table.TenShop;
                    ltdiachikhohang.Text = table.DiaChiKhoHang;
                    ltngaythamgia.Text = table.dcreatedate.ToString();
                    LoadItems(table.iuser_id.ToString());
                }
            }
        }

        public void LoadItems(string IDThanhVien)
        {
            String chuoi = "";
            chuoi += " and IDThanhVien = " + IDThanhVien + " ";

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse("20");
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.Category_Product> iitem = SProducts.Product_All_locTongbanghi(chuoi, language, "1");
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
                lttongsanpham.Text = iitem.Count().ToString();
            }
            List<Entity.Category_Product> dt = SProducts.Product_All_loc(chuoi, language, "1", (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                ltShow.Text = Commond.LoadProductList_Home_Cate(dt);
            }
            else
            {
                ltShow.Text = "<div class='Checkdata'>Chưa có dữ liệu</div>";
            }
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.Phantrang("/shop/" + Thanhvien, Tongsobanghi, pages);
        }

    }
}