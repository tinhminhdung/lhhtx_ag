using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.AllPage
{
    public partial class Box_search_header : System.Web.UI.UserControl
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
            if (!base.IsPostBack)
            {
                this.txtkeyword.Text = this.label("l_search") + "...";
                if (Request["su"] != "Search")
                {
                    MoreAll.MoreAll.SetCookie("Search", "", 5000);
                }
                else
                {
                    if (MoreAll.MoreAll.GetCookies("Search").ToString() != null)
                    {
                        txtkeyword.Text = MoreAll.MoreAll.GetCookies("Search").ToString();
                    }

                }
            }
        }

        protected void lnksearchheader_Click(object sender, EventArgs e)
        {
            if ((this.txtkeyword.Text.Trim().Length <= 0) || this.txtkeyword.Text.Equals(this.label("l_search") + "..."))
            {
                Response.Redirect("/Search/All.html");
            }
            MoreAll.MoreAll.SetCookie("Search", txtkeyword.Text, 5000);
            Response.Redirect("/Search/" + txtkeyword.Text.Trim().Replace(" ", "-").Replace(";", "-").Replace("+", "-").Replace(",", "-").Replace(":", "-").Replace("%20", " ").Trim() + ".html");
        }
        protected void Text_Load(object sender, EventArgs e)
        {
            string str = this.label("l_search") + "...";
            ((TextBox)sender).Attributes["onfocus"] = "if (this.value=='" + str + "') this.value='';";
            ((TextBox)sender).Attributes["onblur"] = "if (this.value=='') this.value='" + str + "';";
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}