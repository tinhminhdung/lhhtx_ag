using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2;


namespace VS.E_Commerce
{
    public partial class OnpaySuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region OnePay return
            string SECURE_SECRET = Commond.Setting("Hashcode");
            string hashvalidateResult = "";
            VPCRequest conn = new VPCRequest("http://onepay.vn");
            conn.SetSecureSecret(SECURE_SECRET);
            hashvalidateResult = conn.Process3PartyResponse(Page.Request.QueryString);
            String vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");
            string amount = conn.GetResultField("vpc_Amount", "Unknown");
            string localed = conn.GetResultField("vpc_Locale", "Unknown");
            string command = conn.GetResultField("vpc_Command", "Unknown");
            string version = conn.GetResultField("vpc_Version", "Unknown");
            string cardBin = conn.GetResultField("vpc_Card", "Unknown");
            string orderInfo = conn.GetResultField("vpc_OrderInfo", "Unknown");
            string merchantID = conn.GetResultField("vpc_Merchant", "Unknown");
            string authorizeID = conn.GetResultField("vpc_AuthorizeId", "Unknown");
            string merchTxnRef = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
            string transactionNo = conn.GetResultField("vpc_TransactionNo", "Unknown");
            string txnResponseCode = vpc_TxnResponseCode;
            string message = conn.GetResultField("vpc_Message", "Unknown");
            if (hashvalidateResult == "CORRECTED" && txnResponseCode.Trim() == "0")
            {
                // if (Ketqua())// nên truyền ID hoặc mã sản phẩm để so sánh rồi thay đổi trạng thái giỏ hàng khi mua thành công
                // {

                Response.Redirect("http://" + Request.Url.Authority + "/thanh-cong/" + Session["strCartCode"].ToString() + "/true.html");

                ////string strKQ = "";
                ////strKQ += "<span id='lblStatus'>Bạn đã giao dịch trả phí thành công theo mã đơn hàng <strong>" + orderInfo + "</strong> với số tiền là <strong>" + string.Format("{0:N0}", (float.Parse(amount) / 100)) + "</strong>VNĐ</span>";
                ////strKQ += "<div class='cartInfo'>Chúng tôi sẽ kiểm tra lại và giao dịch cho quý khách ngay sau khi nhận được thông tin.";
                ////strKQ += "</br>Chậm nhất trong vòng 24h, quý khách sẽ nhận được thông báo chuyển sản phẩm.";
                ////strKQ += "</br>Cảm ơn quý khách đã sử dụng dịch vụ của chúng tôi.</div>";
                ////lblStatus.Text = strKQ;
                ////Session["prcart"] = null;
                ////Session["customerInfo"] = null;
                ////Session["totalPrice"] = null;
                ////Session["strCartCode"] = null;
                //}
            }
            else if (hashvalidateResult == "INVALIDATED" && txnResponseCode.Trim() == "0")
            {
                lblStatus.Text = "Giao dịch đang chờ";
            }
            else
            {
                lblStatus.Text = "Giao dịch không thành công";
                // Response.Redirect("http://" + Request.Url.Authority + "/thanh-toan.html?id=" + Session["strCartCode"].ToString() + "&success=false");
            }
            #endregion
        }
        public bool Ketqua()
        {
            // viết hàm trả kết quả vào giỏ hàng xác nhận đã thanh toán thành công ....
            return true;
        }
    }
}