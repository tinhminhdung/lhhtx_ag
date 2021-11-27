using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class paypal_process : System.Web.UI.Page
    {
        protected string BusinessValue { get; set; }
        protected string ItemNameValue { get; set; }
        protected string ItemNumberValue { get; set; }
        protected string AmountValue { get; set; }
        protected string NoShippingValue { get; set; }
        protected string QuantityValue { get; set; }
        protected string OS2Value { get; set; }
        protected string UrlReturn { get; set; }
        protected string CancelUrlReturn { get; set; }
        Paypal.PaypalEntity pe = new Paypal.PaypalEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                pe = (Paypal.PaypalEntity)Session["OrderPaypal"];

                this.BusinessValue = pe.Business;
                this.ItemNameValue = pe.ItemName;
                this.ItemNumberValue = pe.ItemNumber;
                this.AmountValue = pe.Amount;
                this.NoShippingValue = pe.NoShipping;
                this.QuantityValue = pe.Quantity;
                this.OS2Value = pe.OS2;
                // this.UrlReturn = "http://" + Request.Url.Authority; //+ "/checkout-success.aspx?id=" + pe.ItemNumber + "&success=true";
                // this.CancelUrlReturn = "http://" + Request.Url.Authority; //+ "/checkout-success.aspx?id=" + pe.ItemNumber + "&success=false";
                this.UrlReturn = "http://" + Request.Url.Authority + "/thanh-cong/" + pe.ItemNumber + "/true.html";
                this.CancelUrlReturn = "http://" + Request.Url.Authority + "/thanh-cong/" + pe.ItemNumber + "/false.html";
            }
        }
    }
}