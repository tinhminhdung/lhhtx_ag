using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.E_Commerce.cms.Display.News
{
    public partial class Category1 : System.Web.UI.UserControl
    {
        #region string
        string cid = "-1";
        private string language = Captionlanguage.Language;
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region #
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            #endregion
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

            if (!IsPostBack)
            {
                //CollectionPager1.FirstText = label("FirstText");
                //CollectionPager1.LastText = label("LastText");
                try
                {
                    ltcatename.Text = Commond.Name(More.TangNameicid(hp));
                    //List<Entity.Menu> item = SMenu.GETBYID();
                    //if (item.Count > 0)
                    //{
                    //    ltcatename.Text = item[0].Name.ToString();
                    //}
                }
                catch (Exception)
                { }
                LoadItems();
            }
            // LoadItems();
        }

        //#region LoadItems
        //void LoadItems()
        //{
        //    List<Entity.News> dt = new List<Entity.News>();
        //    dt = SNews.CATEGORY(More.Sub_Menu(More.NS, More.TangNameicid(hp)), language, "1");
        //    if (dt.Count > 0)
        //    {
        //        CollectionPager1.DataSource = dt;
        //        CollectionPager1.MaxPages = 10000;
        //        CollectionPager1.BindToControl = rpcates;
        //        CollectionPager1.PageSize = int.Parse(Commond.Setting("pagenews"));
        //        rpcates.DataSource = CollectionPager1.DataSourcePaged;
        //        rpcates.DataBind();
        //    }
        //    else lterr.Text = "<div class='Checkdata'>" + this.label("I_dulieuchuadccapnhat") + "</div>";
        //}
        //#endregion

        public void LoadItems()
        {
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(Commond.Setting("pagenews"));

            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.Category_News> dt = SNews.CATEGORY_PHANTRANG(More.Sub_Menu(More.NS, More.TangNameicid(hp)), language, "1", (pages - 1), ref Tongsobanghi, Tongsotrang);
            if (dt.Count >= 1)
            {
                ltShow.Text = Commond.LoadNewsList(dt, language);
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
            ltpage.Text = Commond.Phantrang("/" + hp + ".html", Tongsobanghi, pages);
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}