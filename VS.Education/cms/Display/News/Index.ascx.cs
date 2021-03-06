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
    public partial class Index : System.Web.UI.UserControl
    {
        #region string
        private string language = Captionlanguage.Language;
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
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
                LoadItems();
            }
            //LoadItems();
        }

        //#region LoadItems
        //void LoadItems()
        //{
        //    List<Entity.News> dt = SNews.INDEX(language, "1");
        //    if (dt.Count > 0)
        //    {
        //        CollectionPager1.DataSource = dt;
        //        CollectionPager1.MaxPages = 10000;
        //        CollectionPager1.BindToControl = rpcates;
        //        CollectionPager1.PageSize = int.Parse(Commond.Setting("pagenews"));
        //        rpcates.DataSource = CollectionPager1.DataSourcePaged;
        //        rpcates.DataBind();
        //    }
        //    else lterr.Text = "<div style='color:Red; font-weight:bold; text-align:center; margin-bottom:10px; padding-top:10px'>" + this.label("I_dulieuchuadccapnhat") + "</div>";
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
            List<Entity.Category_News> dt = SNews.News_All(language, "1", (pages - 1), ref Tongsobanghi, Tongsotrang);
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
            ltpage.Text = Commond.Phantrang("/tin-tuc-new.html", Tongsobanghi, pages);
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }

        // load menu dạng đa cấp như trang thuongdinhyen.vn
        ///<%#LoadUrl(Eval("icid").ToString()) %>
        protected string LoadUrl(string ID)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            string nav = "";
            try
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(ID));
                if (item != null)
                {
                    nav += item.TangName.ToString();
                    if (item.Parent_ID != -1)
                    {
                        var item2 = db.Menus.FirstOrDefault(s => s.ID == int.Parse(item.Parent_ID.ToString()));
                        if (item2 != null)
                        {
                            nav += "/" + item2.TangName.ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return nav;
        }
    }
}