using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.E_Commerce.cms.Display.Products
{
    public partial class KHoatDaiLy : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
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
            #region Requesthp
            if (Request["hp"] != null && !Request["hp"].Equals(""))
            {
                hp = Request["hp"].ToString();
            }
            iEmptyIndex = hp.IndexOf("?");
            if (iEmptyIndex != -1)
            {
                hp = hp.Substring(0, iEmptyIndex);
            }
            #endregion

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
            if (!IsPostBack)
            {
                LoadItems();
            }
        }
        public void LoadItems()
        {
            String chuoi = " and KichHoatDaiLy=1 ";
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(Commond.Setting("pagepro"));
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.Category_Product> iitem = SProducts.Product_All_locTongbanghi(chuoi, language, "1");
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.Category_Product> dt = SProducts.Product_All_loc(chuoi, language, "1", (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                ltShow.Text = Commond.LoadProductList_Home_Cate(dt);
            }
            else
            {
                ltShow.Text = "<div class='Checkdata'>" + this.label("I_dulieuchuadccapnhat") + "</div>";
            }
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.Phantrang_loc("/san-pham-dieu-kien-tro-thanh-dai-ly.html", Loc, Tongsobanghi, pages);
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}