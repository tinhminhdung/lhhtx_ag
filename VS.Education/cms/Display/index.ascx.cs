using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;
using System.Data;
using Advertisings;
using System.Text;

namespace VS.E_Commerce.cms.Display
{
    public partial class index : System.Web.UI.UserControl
    {
        string key = "";
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
               
                if (Commond.Setting("CooKie").Equals("1"))
                {
                    ltShowDanhMuc.Text = MoreAll.MoreAll.GetCache("ShowDanhMuc", HttpContext.Current.Cache["ShowDanhMuc"] != null ? "" : Ad_vertisings.ShowDanhMuc("12"));
                    ltsildechinh.Text = MoreAll.MoreAll.GetCache("Sildechinh", HttpContext.Current.Cache["Sildechinh"] != null ? "" : Ad_vertisings.ShowA("1"));

                    //  lt2anhduoibanner.Text = MoreAll.MoreAll.GetCache("2anhduoibanner", HttpContext.Current.Cache["2anhduoibanner"] != null ? "" : Ad_vertisings.ShowA2("2"));
                    // ltanhbenphai.Text = MoreAll.MoreAll.GetCache("Anhbenphai", HttpContext.Current.Cache["Anhbenphai"] != null ? "" : Ad_vertisings.Advertisings_A_Images("3"));
                   // ltproBancothethich.Text = MoreAll.MoreAll.GetCache("LoadCoTheBanThich", HttpContext.Current.Cache["LoadCoTheBanThich"] != null ? "" : LoadCoTheBanThich());
                    ltlistpro.Text = MoreAll.MoreAll.GetCache("AGShowNhomsanpham", HttpContext.Current.Cache["AGShowNhomsanpham"] != null ? "" : ShowNhomsanpham());
                    // ltbanchay.Text = MoreAll.MoreAll.GetCache("ShowBanchay", HttpContext.Current.Cache["ShowBanchay"] != null ? "" : ShowBanchay());

                    //ltShowtab.Text = MoreAll.MoreAll.GetCache("Showtab", HttpContext.Current.Cache["Showtab"] != null ? "" : Showtab());
                    // ltShowLoadPro.Text = MoreAll.MoreAll.GetCache("ShowLoadPro", HttpContext.Current.Cache["ShowLoadPro"] != null ? "" : ShowLoadPro());
                    ltShowPhuogThucThanhToan.Text = MoreAll.MoreAll.GetCache("ShowPhuogThucThanhToan", HttpContext.Current.Cache["ShowPhuogThucThanhToan"] != null ? "" : Ad_vertisings.ShowPhuogThucThanhToan("4"));
                }
                else
                {
                    ltShowDanhMuc.Text = Ad_vertisings.ShowDanhMuc("12");
                    ltsildechinh.Text = Ad_vertisings.ShowA("1");
                    // lt2anhduoibanner.Text = Ad_vertisings.ShowA2("2");
                    // ltanhbenphai.Text = Ad_vertisings.Advertisings_A_Images("3");
                   // ltproBancothethich.Text = LoadCoTheBanThich();
                    ltlistpro.Text = ShowNhomsanpham();
                    //ltShowtab.Text =  Showtab();
                    ltShowPhuogThucThanhToan.Text = Ad_vertisings.ShowPhuogThucThanhToan("4");
                }

                ltShowLoadPro.Text = ShowLoadPro();
                ltbanchay.Text = ShowBanchay();

                    ltrpNews.Text = MoreAll.MoreAll.GetCache("Newsss", HttpContext.Current.Cache["Newsss"] != null ? "" : Newsss());
            }
        }
        protected string Newsss()
        {
            string str = "";
            List<Entity.News> dt = SNews.Name_Text("select  * from  News where  new=1 and lang='" + language + "' and Status=1  order by Create_Date desc");
            if (dt.Count > 0)
            {
                foreach (var item in dt)
                {
                    str += "<article class=\"blog-item text-center\">";
                    str += "<div>";
                    str += "<div class=\"blog-item-thumbnail\">";
                    str += "<a href=\"/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + MoreAll.MoreImage.Image_width_height_Title_Alt(item.ImagesSmall.ToString(), "383", "160", item.Title.ToString(), item.Title.ToString()) + "</a>";
                    str += "</div>";
                    str += "<div class=\"blog-item-info m-4\">";
                    str += " <h3 class=\"blog-item-name\">";
                    str += "<a href=\"/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + AllQuery.MoreNews.Substring_Title(item.Title.ToString()) + "</a>";
                    str += "</h3>";
                    str += " <p class=\"blog-item-summary\">" + AllQuery.MoreNews.Substring_Mota(item.Brief.ToString()) + "</p>";
                    str += " <a class=\"btn\" href=\"/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + label("l_viewdetail") + "</a>";
                    str += " </div>";
                    str += " </div>";
                    str += "  </article>";
                }
            }
            return str.ToString();
        }

        protected string Showtab()
        {
            string str = "";
            int j = 1;
            List<Entity.MenuShow> dt = SMenu.Name_Text_MenuShow("SELECT ID,Name,Images,TangName FROM [Menu]  where capp='" + More.PR + "' and Lang='" + language + "' and News=1 and Status=1 order by Orders asc");
            if (dt.Count > 0)
            {
                foreach (Entity.MenuShow item in dt)
                {
                    if (j == 1)
                    {
                        str += "<li  class=\"tab-link has-content\" data-tab=\"tab-" + j + "\"><span>" + item.Name.ToString() + "</span></li>";
                    }
                    else
                    {
                        str += "<li  class=\"tab-link\" data-tab=\"tab-" + j + "\"><span>" + item.Name.ToString() + "</span></li>";
                    }
                    j++;
                }
            }
            return str.ToString();
        }
        protected string ShowtabGiaTot()
        {
            string str = "";
            List<Entity.MenuShow> dt = SMenu.Name_Text_MenuShow("SELECT * FROM [Menu]  where capp='" + More.PR + "' and Lang='" + language + "' and News=1 and Status=1 order by Orders asc");
            if (dt.Count > 0)
            {
                str = "<span>" + dt[0].Name.ToString() + "</span>";
            }
            return str.ToString();
        }
        protected string ShowLoadPro()// sản phẩm chiến lược
        {
            StringBuilder str = new StringBuilder();
            int j = 1;
            // List<Entity.MenuShow> dt = SMenu.Name_Text_MenuShow("SELECT ID,Name,Images,TangName FROM [Menu]  where capp='" + More.PR + "' and Lang='" + language + "' and News=1   and Status=1 order by Orders asc");
            // if (dt.Count > 0)
            // {
            // foreach (Entity.MenuShow items in dt)
            // {
            str.Append("<div class=\"tab-" + j + " tab-content\">");
            str.Append("<div class=\"products products-view-grid\">");
            //  str.Append("<div class=\"products products-view-grid owl-carousel\" data-lg-items='4' data-md-items='4' data-sm-items='3' data-xs-items=\"2\" data-xss-items=\"2\" data-margin='30' data-nav=\"true\">");
            List<Entity.Category_Product> table = SProducts.Name_Text_Rg("SELECT top " + Commond.HomePage() + " " + Commond.Sql_Product() + " FROM [products] WHERE  News=1  AND lang='" + language + "'  AND Status=1  ORDER BY NEWID()");//and icid in (" + More.Sub_Menu(More.PR, items.ID.ToString()) + ")
            if (table.Count >= 1)
            {
                str.Append(Commond.LoadProductList_Home_Cate(table));
            }
            // str.Append("</div>");
            str.Append("</div>");
            str.Append("</div>");
            // j++;
            // }
            //}
            return str.ToString();
        }

        protected string ShowBanchay()
        {
            StringBuilder str = new StringBuilder();
            int j = 1;
            str.Append("<div class=\"products products-view-grid\">");
            List<Entity.Category_Product> table = SProducts.Name_Text_Rg("SELECT top " + Commond.HomePage() + " " + Commond.Sql_Product() + " FROM [products] WHERE   lang='" + language + "'  AND Status=1  ORDER BY NEWID()");//and icid in (" + More.Sub_Menu(More.PR, items.ID.ToString()) + ")
            if (table.Count >= 1)
            {
                str.Append(Commond.LoadProductList_Home_Cate(table));
            }
            str.Append("</div>");
            return str.ToString();
        }
        protected string LoadCoTheBanThich()
        {
            string str = "";
            List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select top " + Commond.HomePage() + " " + Commond.Sql_Product() + " from  products where Check_02=1 and lang='" + language + "' and Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                str += Commond.LoadProductList_Home(table);
            }
            return str;
        }
        protected string ShowNhomsanpham()
        {
            StringBuilder str = new StringBuilder();
            List<Entity.MenuShow> dt = SMenu.Pages_Home(More.PR, language, "1");
            if (dt.Count > 0)
            {
                foreach (Entity.MenuShow item in dt)
                {
                    str.Append(" <section class=\"awe-section-5\">");
                    str.Append("<div class=\"section section-collection section-collection-1\">");
                    str.Append("<div class=\"container\">");
                    str.Append("<div class=\"collection-border\">");
                    str.Append("<div class=\"collection-main\">");
                    str.Append("<div class=\"row \">");
                    str.Append("<div class=\"col-lg-12 col-sm-12\">");
                    str.Append("<div class=\"e-tabs not-dqtab \" >");
                    str.Append("<div class=\"row row-noGutter\">");
                    str.Append("<div class=\"col-sm-12\">");
                    str.Append("<div class=\"content\">");
                    str.Append("<div class=\"section-title\">");
                    str.Append("<h2><a href=\"/" + item.TangName + ".html\">" + item.Name + "</a></h2>");
                    str.Append("</div>");
                    str.Append("<div>");
                    str.Append("<ul class=\"tabs tabs-title ajax clearfix Destop\">");
                    str.Append(ShowLinktabMenu(item.ID.ToString()));
                    str.Append("</ul>");
                    //  str.Append("<div class=\"products products-view-grid owl-carousel\" data-lgg-items=\"4\" data-lg-items='4' data-md-items='4' data-sm-items='3' data-xs-items=\"2\" data-xss-items=\"2\" data-margin='14' data-nav=\"true\">");
                    str.Append("<div class=\"products products-view-grid\">");
                    List<Entity.Category_Product> table = SProducts.GetTopProductInCategory(Commond.HomePage(), item.ID.ToString(), More.Sub_Menu(More.PR, item.ID.ToString()));
                    if (table.Count >= 1)
                    {
                        str.Append(Commond.LoadProductList_Home_Cate(table));
                    }
                    str.Append("</div>");
                    str.Append("</div>");
                    str.Append("</div>");
                    str.Append("</div>");
                    str.Append("</div>");
                    str.Append("</div>");

                    str.Append(Ad_vertisings.TheoNhom(item.ID.ToString()));

                    str.Append("</div>");
                    //str.Append("<div class=\"col-lg-3 col-sm-12 hidden-sm hidden-xs\">");
                    //str.Append("<div class=\"aside-right\">");
                    //str.Append("<div class=\"aside-item aside-mini-list-product\">");
                    //str.Append("<div>");
                    //str.Append("<div class=\"aside-title\">");
                    //str.Append("<h2 class=\"title-head\" style=\"background-color:#ffb100\">");
                    //str.Append("<a href=\"/san-pham-goi-y.html\">Gợi ý cho bạn</a>");//san-pham-noi-bat.html
                    //str.Append("</h2>");
                    //str.Append("</div>");
                    //str.Append("<div class=\"aside-content related-product\">");
                    //str.Append("<div class=\"product-mini-lists\">");
                    //str.Append("<div class=\"products\">");
                    //str.Append("<div class=\"top-right-owl-nav products-view-grid owl-carousel\" data-lgg-items=\"1\" data-lg-items='1' data-md-items='2' data-sm-items='2' data-xs-items=\"1\" data-xss-items=\"1\" data-margin='30' data-nav=\"true\">");

                    //str.Append(SanPhamGoiY(item.ID.ToString()));

                    //str.Append("</div>");
                    //str.Append("</div>");

                    //str.Append("</div>");
                    //str.Append("</div>");
                    //str.Append("</div>");
                    //str.Append("</div>");
                    //str.Append("</div>");
                    //str.Append("</div>");
                    //str.Append("</div>");
                    //str.Append("</div>");
                    //str.Append("</div>");
                    //str.Append("</div>");
                    //str.Append("</div>");
                    str.Append("</section>");
                }
            }
            return str.ToString();
        }
        protected string SanPhamGoiY(string ID)
        {
            string str = "";
            string strb = "";
            string top = Commond.Setting("Homegoiychoban");
            string sql = "select top " + top + " " + Commond.Sql_Product() + "  from  products where icid in (" + More.Sub_Menu(More.PR, ID.ToString()) + ") and Check_01=1 and lang='" + language + "' and Status=1  order by Create_Date desc";

            List<Entity.Category_Product> table = SProducts.Name_Text_Rg(sql);
            if (table.Count >= 1)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    strb += "    <div class=\"product-mini-item clearfix on-sale\">";
                    strb += " <a class=\"product-img\"  href='/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + AllQuery.MorePro.Image_Title_Alts_Css("GoiYi", table[i].ImagesSmall.ToString(), table[i].Name.ToString(), table[i].Alt.ToString()) + "</a>";
                    strb += "      <div class=\"product-info\">";
                    strb += "        <h3>";
                    strb += "<a class=\"product-name namespgoiy\" href='/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + AllQuery.MorePro.Substring_Title(table[i].Name.ToString()) + "</a>";
                    strb += "        </h3>";
                    strb += "        <div class=\"price-box\">";
                    strb += "          <span class=\"price f-left\">";
                    strb += "            <span class=\"price product-price\">" + AllQuery.MorePro.FormatMoney(table[i].Price.ToString()) + "</span>";
                    strb += "          </span>";
                    strb += "          <!-- Giá Khuyến mại -->";
                    strb += "          <span class=\"old-price f-left\">";
                    strb += "            <del class=\"sale-price\">" + AllQuery.MorePro.Detail_Price(table[i].OldPrice.ToString()) + "</del>";
                    strb += "          </span>";
                    strb += "          <!-- Giá gốc -->";
                    //  strb += Commond.Giamgia(table[i].Price.ToString(), table[i].GiaThanhVien.ToString(), table[i].OldPrice.ToString());
                    strb += "        </div>";
                    strb += "      </div>";
                    strb += "    </div>";
                    if ((i + 1) % 5 == 0)
                    {
                        strb = "<div class=\"item\">" + strb + "</div>";
                        str += strb.ToString();
                        strb = "";
                    }
                    else if (i == (table.Count - 1))
                    {
                        strb = "<div class=\"item\">" + strb + "</div>";
                        str += strb.ToString();
                    }
                }
            }
            return str;
        }
        protected string ShowLinktabMenu(string ID)
        {
            StringBuilder str = new StringBuilder();
            List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where Parent_ID =" + ID + " and Check_01=1 and  capp='" + More.PR + "' and Lang='" + language + "' and Status=1 order by Orders asc");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu items in dt)
                {
                    str.Append("<li class=\"tab-link has-content\">");
                    str.Append("<a href=\"/" + items.TangName + ".html\">");
                    str.Append("<span>" + items.Name + "</span>");
                    str.Append("</a>");
                    str.Append("</li>");
                    str.Append(ShowLinktabMenu(items.ID.ToString()));
                }
            }
            return str.ToString();
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}