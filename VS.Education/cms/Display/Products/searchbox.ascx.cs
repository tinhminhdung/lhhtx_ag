using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;

namespace VS.E_Commerce.cms.Display.Products
{
    public partial class searchbox : System.Web.UI.UserControl
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
            this.Page.Form.DefaultButton = lnksearch.UniqueID;
            if (!base.IsPostBack)
            {
                this.txtkeyword.Text = this.label("l_search") + "...";
            }
        }
        protected void Text_Load(object sender, EventArgs e)
        {
            string str = this.label("l_search") + "...";
            ((TextBox)sender).Attributes["onfocus"] = "if (this.value=='" + str + "') this.value='';";
            ((TextBox)sender).Attributes["onblur"] = "if (this.value=='') this.value='" + str + "';";
        }
        protected void lnksearch_Click(object sender, EventArgs e)
        {
            if ((this.txtkeyword.Text.Trim().Length <= 0) || this.txtkeyword.Text.Equals(this.label("l_search") + "..."))
            {
                Response.Redirect("/Search/0/Search.aspx");
            }
            Response.Redirect("/Search/" + txtkeyword.Text.Replace("&nbsp;", "") + "/Search.aspx");
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