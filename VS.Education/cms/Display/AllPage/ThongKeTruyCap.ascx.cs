using MoreAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.AllPage
{
    public partial class ThongKeTruyCap : System.Web.UI.UserControl
    {

        public string hp = "";
        public string Modul = "";
        int iEmptyIndex = 0;
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        public string ssl = "http://";
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
            { }
            if (Commond.Setting("SSL").Equals("1"))
            {
                ssl = "https://";
            }

            lthistats.Text = " <script type='text/javascript'>var _Hasync = _Hasync || []; _Hasync.push(['Histats.start', '" + Commond.Setting("txtHistats") + "']); _Hasync.push(['Histats.fasi', '1']); _Hasync.push(['Histats.track_hits', '']); (function () { var hs = document.createElement('script'); hs.type = 'text/javascript'; hs.async = true; hs.src = ('" + ssl + "s10.histats.com/js15_as.js'); (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(hs); })();</script>";
        }
    }
}