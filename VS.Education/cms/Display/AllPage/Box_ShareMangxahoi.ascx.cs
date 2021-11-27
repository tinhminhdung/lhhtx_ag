using MoreAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.AllPage
{
    public partial class Box_ShareMangxahoi : System.Web.UI.UserControl
    {
        public string hp = "";
        public string Modul = "";
        int iEmptyIndex = 0;
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        string ssl = "http://";
        public string ShowUrl = "";
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
                if (Commond.Setting("SSL").Equals("1"))
                {
                    ssl = "https://";
                }

                #region Request["hp"]
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
                if (Request["su"] == null && Request["e"] == null)
                {
                    ShowUrl = ssl + Request.Url.Host;
                }
                else
                {
                    ShowUrl = ssl + Request.Url.Host + "/" + hp + ".html";
                }
            }
        }
    }
}