using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.E_Commerce.cms.Display.Products
{
    public partial class Search : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
        string keyword = ""; string keywordss = "";
        string Categories = "0";
        string price = "0";
        string produce = "0";
        string Loc = "";
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
            if (Request["keyword"] != null && !Request["keyword"].Equals(""))
            {
                keyword = Request["keyword"].ToString();
            }
            if (Request["price"] != null && !Request["price"].Equals(""))
            {
                price = Request["price"];
                Loc += "&price=" + price;
            }
            if (Request["produce"] != null && !Request["produce"].Equals(""))
            {
                produce = Request["produce"];
                Loc += "&produce=" + produce;
            }
            if (keyword == "")
            {
                MoreAll.MoreAll.SetCookie("Search", "", 5000);
                Response.Redirect("/");
            }
            LoadItems();
            if (!IsPostBack)
            {
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
        public void LoadItems()
        {
            string keywordvv = "";
            {
                keywordvv = Request.RawUrl.ToString().Replace("/Search/", "").Replace("-", " ").Replace(".html", "");
                int iEmpty = 0;
                iEmpty = keywordvv.IndexOf("?");
                if (iEmpty != -1)
                {
                    keywordvv = keywordvv.Substring(0, iEmpty);
                }
                if (keywordvv == "")
                {
                    MoreAll.MoreAll.SetCookie("Search", "", 5000);
                    Response.Redirect("/");
                }
            }
            #region Boloc
            String chuois = "";
            String chuoi = "";
            #region Boloc
            if (produce != "0")
            {
                chuois += " and ID_Hang in(" + produce + ")";
            }
            if (price != "0")
            {
                string Gia = " and (";
                string[] strArray = price.ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    Gia += (i == 0 ? "" : " OR ") + "(Price between (" + Commond.GiaTu(strArray[i].ToString()) + ") and (" + Commond.GiaDen(strArray[i].ToString()) + ")) ";
                }
                chuois += Gia + ")";
            }
            #endregion
            #endregion
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(Commond.Setting("pagepro"));

            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            if (Categories != "0")
            {
                chuoi = MoreAll.MoreAll.GetCookies("Categories").ToString();
            }
            List<Entity.Category_Product> iitem = SProducts.SearchPro_locTongbanghi(keywordvv, Commond.SubMenu(More.PR, chuoi), chuois, language, "1");
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.Category_Product> dt = SProducts.SearchPro_loc(keywordvv, Commond.SubMenu(More.PR, chuoi), chuois, language, "1", (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                ltShow.Text = Commond.LoadProductList_Home_Cate(dt);
            }
            else
            {
                ltShow.Text = "<div class='Checkdata'>Không tìm thấy dữ liệu</div>";
                //ltShow.Text += "<div class='ttimkiem'><p style='margin-top: 0.33em'> Không tìm thấy <span style='color:Red;'> <b>" + keywords + "</b></span> trong tài liệu nào.</p><p style='margin-top: 1em'>Ðề xuất:</p><ul style='margin: 0px 0px 2em 1.3em'> <li>Xin bạn chắc chắn rằng tất cả các từ đều đúng chính tả. </li><li>Hãy thử những từ khoá khác. </li><li>Hãy thử những từ khoá chung hơn.</li></ul></div>";
            }
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.Phantrang_loc("" + keywordvv + ".html", Loc, Tongsobanghi, pages);
        }
    }
}