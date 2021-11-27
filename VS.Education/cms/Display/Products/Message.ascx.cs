using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;

namespace VS.E_Commerce.cms.Display.Products
{
    public partial class Message : System.Web.UI.UserControl
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
            string str;
            if (((HttpContext.Current.Request.QueryString["msg"] != null) && !HttpContext.Current.Request.QueryString["msg"].ToString().Equals("")) && ((str = HttpContext.Current.Request.QueryString["msg"].ToString().Trim()) != null))
            {
                if (!(str == "otc"))
                {
                    if (!(str == "removecart"))
                    {
                        if (!(str == "notexistorder"))
                        {
                            if (str == "ordersucess")
                            {
                                this.ltmessage.Text = this.label("l_produc_our_place");
                            }
                            return;
                        }
                        this.ltmessage.Text = this.label("l_produc_Orders_exist");
                        return;
                    }
                }
                else
                {
                    this.ltmessage.Text = this.label("l_produc_Order_successful");
                    return;
                }
                this.ltmessage.Text = this.label("l_produc_Canceled_order");
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}