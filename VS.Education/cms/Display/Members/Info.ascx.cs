using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Framework;
using MoreAll;

namespace VS.E_Commerce.cms.Display.Members
{
    public partial class Info : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
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
                try
                {
                    this.loadinformation();
                }
                catch (Exception) { }
            }
            string uid = "";
            string un = "";
            if (Request["uid"] != null && !Request["uid"].Equals(""))
            {
                uid = Request["uid"];
            }
            if (Request["un"] != null && !Request["un"].Equals(""))
            {
                un = Request["un"];
            }
        }
        public string Avatar(string img)
        {
            if (img.Trim().Length > 0)
            {
                return ("<img src='/Uploads/avatar/" + img + "' class=admavatarimg>");
            }
            return "<img src='/Uploads/avatar/no_avatar.png' class=admavatarimg>";
        }
        private void loadinformation()
        {
            try
            {
                if (MoreAll.MoreAll.GetCookies("Members") != "")
                {
                    user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                    if (table != null)
                    {
                        this.ltnickname.Text = (table.vuserun.ToString());
                        this.ltlname.Text = table.vfname.ToString();
                        this.ltaddress.Text = table.vaddress.ToString();
                        this.ltemail.Text = table.vemail.ToString();
                        this.ltphone.Text = table.vphone.ToString();
                        this.ltimg.Text = Avatar(table.vavatar.ToString());
                    }
                }
            }
            catch (Exception)
            { }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}