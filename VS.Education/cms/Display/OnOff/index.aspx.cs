using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;

namespace VS.E_Commerce.cms.Display.OnOff
{
    public partial class index : System.Web.UI.Page
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
                if (OnOffs.StatusOnOff().Equals("1"))
                {
                    this.ltcontent.Text = OnOffs.OnOff();
                }
                else if (OnOffs.StatusOnOff().Equals("0"))
                {
                    Response.Redirect("/index.aspx");
                }
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}