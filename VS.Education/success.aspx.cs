using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using API_NganLuong;
using System.Text;

namespace VS.E_Commerce
{
    public partial class success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String Token = Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = MoreAll.Ngan_Luong.MerchantSiteCode();
            info.Merchant_password = MoreAll.Ngan_Luong.PasswordNL();
            info.Token = Token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            if (result.errorCode == "00")
            {
                Response.Redirect("http://" + Request.Url.Authority + "/thanh-cong/" + Session["strCartCode"].ToString() + "/true.html");
            }
            else
            {
                result_NL.Text = result.errorCode + result.payerName;
            }
            Session["cart"] = null;
            Session["Session_Size"] = null;
            Session["Session_MauSac"] = null;
            Session["customerInfo"] = null;
            Session["totalPrice"] = null;
            Session["strCartCode"] = null;
        }

        protected string convert_utf8(string str)
        {
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(str);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            string msg = utf8.GetString(isoBytes);
            return msg;
        }
    }
}