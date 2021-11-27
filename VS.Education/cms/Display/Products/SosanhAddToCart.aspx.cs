using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using System.Data;

namespace VS.E_Commerce.cms.Display.Products
{
    public partial class SosanhAddToCart : System.Web.UI.Page
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
                ShoppingCart_AddProduct(ipid.ToString());
            }
        }
        protected string ShoppingCart_AddProduct(string pid)
        {
            if (System.Web.HttpContext.Current.Session["Sosanh"] == null)
            {
                // create session cart.
                SosanhCarts.ShoppingCart_CreateCart();
                ShoppingCart_AddProduct(pid);
            }
            else
            {
                List<Entity.Products> dt = new List<Entity.Products>();
                // lay chi tiet san pham.
                dt = SProducts.GetById(pid);
                if (dt.Count > 0)
                {
                    DataTable dtcart = new DataTable();
                    dtcart = (DataTable)System.Web.HttpContext.Current.Session["Sosanh"];
                    bool hasincart = false;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                        {
                            hasincart = true;
                            // cap nhat thong tin cua cart.
                            System.Web.HttpContext.Current.Session["Sosanh"] = dtcart;
                            break;
                        }
                    }
                    if (hasincart == false)
                    {
                        if (dtcart != null)
                        {
                            DataRow dr = dtcart.NewRow();
                            dr["PID"] = pid;
                            dtcart.Rows.Add(dr);
                            System.Web.HttpContext.Current.Session["Sosanh"] = dtcart;
                        }
                    }
                }
            }
            return "";
        }

    }
}