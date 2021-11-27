using MoreAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.AllPage
{
    public partial class Box_lang : System.Web.UI.UserControl
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
        }
        protected void lnkEnglish_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["language"] = "ENG";
            base.Response.Redirect("/");
        }
        protected void lnkVIE_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["language"] = "VIE";
            base.Response.Redirect("/");
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}