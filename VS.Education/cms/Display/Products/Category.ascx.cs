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
    public partial class Category : System.Web.UI.UserControl
    {
        private string cid = "-1";
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
        string price = "0";
        string produce = "0";
        string Loc = "";
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
                // CollectionPager1.FirstText = label("FirstText");
                // CollectionPager1.LastText = label("LastText");

                if (this.cid.Equals(""))
                {
                    this.ltcatename.Text = "Sản phẩm";
                }
                else
                {
                    Menu dt = db.Menus.SingleOrDefault(p => p.ID == int.Parse(More.TangNameicid(hp)));
                    if (dt != null)
                    {
                        ltcatename.Text = dt.Name.ToString();
                        ltcontent.Text = dt.Description;
                    }
                }
                LoadItems();
            }
            // LoadItems();
        }
        public void LoadItems()
        {
            #region Boloc
            String chuoi = "";
            if (produce != "0")
            {
                chuoi += " and ID_Hang in(" + produce + ")";
            }
            if (price != "0")
            {
                string Gia = " and (";
                string[] strArray = price.ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    Gia += (i == 0 ? "" : " OR ") + "(Price between (" + Commond.GiaTu(strArray[i].ToString()) + ") and (" + Commond.GiaDen(strArray[i].ToString()) + ")) ";
                }
                chuoi += Gia + ")";
            }
            #endregion

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(Commond.Setting("pagepro"));

            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.Category_Product> iitem = SProducts.CATEGORY_PHANTRANG_locTongbanghi(Commond.SubMenu(More.PR, More.TangNameicid(hp)), chuoi, language, "1");
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.Category_Product> dt = SProducts.CATEGORY_PHANTRANG_loc(Commond.SubMenu(More.PR, More.TangNameicid(hp)), chuoi, language, "1", (pages - 1), Tongsotrang);
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
            ltpage.Text = Commond.Phantrang_loc("/" + hp + ".html", Loc, Tongsobanghi, pages);
        }
        //public void LoadItems()
        //{
        //    int Tongsobanghi = 0;
        //    Int16 pages = 1;
        //    int Tongsotrang = int.Parse(Commond.Setting("pagepro"));

        //    if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
        //    {
        //        pages = Convert.ToInt16(Request.QueryString["page"].Trim());
        //    }
        //    List<Entity.Category_Product> dt = SProducts.CATEGORY_PHANTRANG(Commond.SubMenu(More.PR, More.TangNameicid(hp)), language, "1", (pages - 1), ref Tongsobanghi, Tongsotrang);
        //    if (dt.Count >= 1)
        //    {
        //        ltShow.Text = Commond.LoadProductList(dt);
        //    }
        //    else
        //    {
        //        ltShow.Text = "<div class='Checkdata'>" + this.label("I_dulieuchuadccapnhat") + "</div>";
        //    }
        //    if (Tongsobanghi % Tongsotrang > 0)
        //    {
        //        Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
        //    }
        //    else
        //    {
        //        Tongsobanghi = Tongsobanghi / Tongsotrang;
        //    }
        //    ltpage.Text = Commond.Phantrang("/" + hp + ".html", Tongsobanghi, pages);
        //}

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}