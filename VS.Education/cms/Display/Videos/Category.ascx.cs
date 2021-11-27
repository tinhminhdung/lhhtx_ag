using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.E_Commerce.cms.Display.Videos
{
    public partial class Category : System.Web.UI.UserControl
    {

        #region string
        string cid = "";
        private string language = Captionlanguage.Language;
        string hp = "-1";
        int iEmptyIndex = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Session
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
                ltcatename.Text = Commond.Name(More.TangNameicid(hp));
                LoadItems();
            }
           // LoadItems();
        }

        //#region LoadItems
        //void LoadItems()
        //{
        //    List<Entity.VideoClip> dt = SVideoClip.CATEGORY(More.Sub_Menu(More.VD, cid), language, "1");
        //    if (dt.Count > 0)
        //    {
        //        CollectionPager1.DataSource = dt;
        //        CollectionPager1.MaxPages = 10000;
        //        CollectionPager1.BindToControl = rpcates;
        //        CollectionPager1.PageSize = int.Parse(AllQuery.MoreVideoClip.Pagevideo());
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
            int Tongsotrang = int.Parse(AllQuery.MoreVideoClip.Pagevideo());

            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.VideoClip_RutGon> dt = SVideoClip.CATEGORY_PHANTRANG(More.Sub_Menu(More.VD, More.TangNameicid(hp)), language, "1", (pages - 1), ref Tongsobanghi, Tongsotrang);
            if (dt.Count >= 1)
            {
                ltShow.Text = Commond.LoadVideo_Home(dt);
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