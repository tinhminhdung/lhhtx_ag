using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.control
{
    public partial class ControlProCart : System.Web.UI.UserControl
    {
        #region string
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        public string Moldul = "";
        string pid = "-1";
        string cid = "-1";
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
            //if (Request["e"] != null)
            //{
            //    if (Request["e"].ToString() == "load")
            //    {
            //        string request = Request["hp"] != null ? Request["hp"].ToString() : Request.Path;
            //        string t = Request["hp"].ToString() + ".html";
            //        if (!request.ToLower().Contains("index.aspx"))
            //        {
            //            Moldul = Commond.RequestMenu(Request["hp"]);
            //            switch (Moldul)
            //            {
            //                case "21":
            //                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Detail.ascx"));
            //                    break;
            //            }
            //        }
            //    }
            //}
            #region Request_su
            switch (Request["su"])
            {
                case "viewcart":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Cart.ascx"));
                    break;
                case "msg":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Message.ascx"));
                    break;

            }
            #endregion

        }
    }
}