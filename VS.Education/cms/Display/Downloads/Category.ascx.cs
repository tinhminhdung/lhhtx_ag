using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.E_Commerce.cms.Display.Downloads
{
    public partial class Category : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
       

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
            if (!IsPostBack)
            {
            }
            LoadItems();
        }

        void LoadItems()
        {
            List<Entity.Download> dt = new List<Entity.Download>();
            dt = SDownload.CATEGORY(language);
            if (dt.Count > 0)
            {
                CollectionPager1.DataSource = dt;
                CollectionPager1.MaxPages = 10000;
                CollectionPager1.BindToControl = rpcates;
                CollectionPager1.PageSize = int.Parse(AllQuery.MoreDownload.Pagedownload());
                rpcates.DataSource = CollectionPager1.DataSourcePaged;
                rpcates.DataBind();
            }
            else lterr.Text = "<div class='Checkdata'>" + this.label("I_dulieuchuadccapnhat") + "</div>";
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}