using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Services;
using Entity;

namespace VS.E_Commerce
{
    public partial class Shopcart : System.Web.UI.Page
    {
        private string language = Captionlanguage.Language;
        public string tongien = "0";
        public string sosp = "0";
        public string ipid = "0";
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
            if (Request["pid"] != null && !Request["pid"].Equals(""))
            {
                ipid = Request["pid"].ToString();
            }
            if (!base.IsPostBack)
            {
                if (ipid != "0" && ipid != "")
                {
                    ShoppingCart_AddProduct(ipid.ToString(), int.Parse("1"));
                }
                this.LoadCart();
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
       

        protected static string ShoppingCart_AddProduct(string pid, int quantity)
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
            {
                // create session cart.
                SessionCarts.ShoppingCreateCart();
                ShoppingCart_AddProduct(pid, quantity);
            }
            else
            {
                List<Products> dt = new List<Products>();
                // lay chi tiet san pham.
                dt = SProducts.GetById(pid);
                if (dt.Count > 0)
                {
                    string vimg = dt[0].Images.ToString();
                    string name = dt[0].Name.ToString();
                    string Mausac = "0";
                    string Kichco = "0";
                    try
                    {
                        if (System.Web.HttpContext.Current.Session["Session_Size"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_Size"].ToString().Equals(""))
                        {
                            Kichco = System.Web.HttpContext.Current.Session["Session_Size"].ToString();
                        }
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        if (System.Web.HttpContext.Current.Session["Session_MauSac"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_MauSac"].ToString().Equals(""))
                        {
                            Mausac = System.Web.HttpContext.Current.Session["Session_MauSac"].ToString();
                        }
                    }
                    catch (Exception)
                    { }
                    if (!dt[0].Price.ToString().Equals(""))
                    {
                        float prices = 0;
                        float pricesi = 0;
                        if (dt[0].Price.Length > 0)
                        {
                            prices = Convert.ToSingle(dt[0].Price);
                        }
                        float money = prices * quantity;
                        DataTable dtcart = new DataTable();
                        dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                        bool hasincart = false;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                            {
                                if (dtcart.Rows[i]["Price"].ToString().Length > 0)
                                {
                                    pricesi = Convert.ToSingle(dtcart.Rows[i]["Price"]);
                                }
                                hasincart = true;
                                // cap nhat thong tin cua cart.
                                quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
                                dtcart.Rows[i]["Quantity"] = quantity;
                                dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(pricesi);
                                dtcart.Rows[i]["Mausac"] = Mausac;
                                dtcart.Rows[i]["Kichco"] = Kichco;
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                                break;
                            }
                        }
                        if (hasincart == false)
                        {
                            if (dtcart != null)
                            {
                                DataRow dr = dtcart.NewRow();
                                dr["PID"] = pid;
                                dr["Vimg"] = vimg;
                                dr["Name"] = name;
                                dr["Price"] = prices;
                                dr["Quantity"] = quantity;
                                dr["Money"] = money;
                                dr["Mausac"] = Mausac;
                                dr["Kichco"] = Kichco;
                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                    else
                    {
                        float prices = 0; float pricesi = 0;
                        if (dt[0].Price.Length > 0)
                        {
                            prices = Convert.ToSingle(dt[0].Price);
                        }
                        float money = prices * quantity;
                        DataTable dtcart = new DataTable();
                        dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                        bool hasincart = false;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                            {
                                if (dtcart.Rows[i]["Price"].ToString().Length > 0)
                                {
                                    pricesi = Convert.ToSingle(dtcart.Rows[i]["Price"]);
                                }
                                hasincart = true;
                                // cap nhat thong tin cua cart.
                                quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
                                dtcart.Rows[i]["Quantity"] = quantity;
                                dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(pricesi);
                                dtcart.Rows[i]["Mausac"] = Mausac;
                                dtcart.Rows[i]["Kichco"] = Kichco;
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                                break;
                            }
                        }
                        if (hasincart == false)
                        {
                            if (dtcart != null)
                            {
                                DataRow dr = dtcart.NewRow();
                                dr["PID"] = pid;
                                dr["Vimg"] = vimg;
                                dr["Name"] = name;
                                dr["Price"] = prices;
                                dr["Quantity"] = quantity;
                                dr["Money"] = money;
                                dr["Mausac"] = Mausac;
                                dr["Kichco"] = Kichco;
                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                }
            }
            System.Web.HttpContext.Current.Session["Session_Size"] = null;
            System.Web.HttpContext.Current.Session["Session_MauSac"] = null;
            return "";
        }

        private void LoadCart()
        {
            if (Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    Repeater1.DataSource = dtcart;
                    Repeater1.DataBind();
                    string inumofproducts = "0";
                    string totalvnd = "0";
                    if (dtcart.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                            num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    if (totalvnd.ToString().Length > 0)
                    {
                        tongien = AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());
                    }
                    else
                    {
                        tongien = "0";
                    }

                    if (inumofproducts.ToString().Length > 0)
                    {
                        sosp = inumofproducts;
                    }
                    else
                    {
                        sosp = "0";
                    }
                    float total = 0;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        total += Convert.ToSingle(dtcart.Rows[i]["Money"]);
                    }
                    this.pnOrder.Visible = true;
                    this.pnmessage.Visible = false;
                }
                else
                {
                    this.pnOrder.Visible = false;
                    this.pnmessage.Visible = true;
                }
            }
            else
            {
                this.pnOrder.Visible = false;
                this.pnmessage.Visible = true;
            }
        }
        protected string Giacu(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                if (dt[0].OldPrice.ToString().Length > 0)
                {
                    str = AllQuery.MorePro.Detail_Price(dt[0].OldPrice.ToString()) + " đ";
                }
            }
            return str.ToString();
        }

    }
}