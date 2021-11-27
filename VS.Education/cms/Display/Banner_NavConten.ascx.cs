using MoreAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display
{
    public partial class Banner_NavConten : System.Web.UI.UserControl
    {
        #region string
        private string language = Captionlanguage.Language;
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        public string Case = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
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

            if (Request["e"] != null)
            {
                if (Request["e"].ToString() == "load")
                {
                    Case = Commond.RequestMenu(Request["hp"]);
                }
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}