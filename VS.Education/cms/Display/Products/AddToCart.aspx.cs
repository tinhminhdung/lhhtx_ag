using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.E_Commerce.cms.Display.Products
{
    public partial class AddToCart : System.Web.UI.Page
    {
        private string ipid = "-1";
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
            if (Request["ipid"] != null && !Request["ipid"].Equals(""))
            {
                ipid = Request["ipid"];
            }
            if (!IsPostBack)
            {
                List<Entity.Products> dt = SProducts.GetById(ipid);
                if (dt.Count > 0)
                {
                    if (MoreAll.MoreAll.GetCookies("Members") == "")
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    else
                    {
                        SessionCarts.ShoppingCart_AddProduct_Sesion(ipid.ToString(), Convert.ToInt32("1"), "0", "0", "1");
                    }

                }
            }
            base.Response.Redirect("/gio-hang.html");
        }
    }
}