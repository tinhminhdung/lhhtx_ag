using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.VanChuyen
{
    public partial class TinhPhi : System.Web.UI.Page
    {
        public class DonHang
        {
            public string partner_id { get; set; }//String - Mã đơn hàng thuộc hệ thống của đối tác
            public string label { get; set; }
            public string area { get; set; }
            public string fee { get; set; }
            public string insurance_fee { get; set; }
            public string estimated_pick_time { get; set; }
            public string estimated_deliver_time { get; set; }
            public string province { get; set; }
        }
        public class Ketqua
        {
            public string Success { get; set; }
            public string message { get; set; }
            public DonHang Order { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string Get(string Link)
        {
            WebRequest tRequest;
            tRequest = WebRequest.Create(Link);
            tRequest.Method = "GET";
            tRequest.UseDefaultCredentials = true;
            tRequest.PreAuthenticate = true;

            tRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
            //định dạng JSON
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add("Token", "3d6C65727cF5A5791E495236d31Be8E64cc7f4ea");
            string RegArr = string.Empty;
            WebResponse tResponse = tRequest.GetResponse();
            Stream dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();
           // Response.Write("<br>*******<br>" + sResponseFromServer + "<br>*******<br>");
            tReader.Close();
            dataStream.Close();
            tResponse.Close();

            return sResponseFromServer;
        }
        protected void Tinhphivanchuyen_Click(object sender, EventArgs e)
        {

            var url = "https://services.giaohangtietkiem.vn/services/shipment/fee?address=P.503%20t%C3%B2a%20nh%C3%A0%20Auu%20Vi%E1%BB%87t,%20s%E1%BB%91%201%20L%C3%AA%20%C4%90%E1%BB%A9c%20Th%E1%BB%8D&province=H%C3%A0%20n%E1%BB%99i&district=Qu%E1%BA%ADn%20C%E1%BA%A7u%20Gi%E1%BA%A5y&pick_province=H%C3%A0%20N%E1%BB%99i&pick_district=Qu%E1%BA%ADn%20Hai%20B%C3%A0%20Tr%C6%B0ng&weight=1000&value=3000000";
            var responseData = Get(url);
            Ketqua m = JsonConvert.DeserializeObject<Ketqua>(responseData);
            string Success = m.Success;
            string message = m.message;
            string str = "";
            if (Success == "True")
            {
                str += "Request thành công, không có lỗi xảy ra :";
                str += m.message + " <br />";
                str += "partner_id:" + m.Order.partner_id + " <br />";
                str += "label:" + m.Order.label + " <br />";
                str += "area:" + m.Order.area + " <br />";
                str += "fee:" + m.Order.fee + " <br />";
                str += "insurance_fee:" + m.Order.insurance_fee + " <br />";
                str += "estimated_pick_time: " + m.Order.estimated_pick_time + " <br />";
                str += "estimated_deliver_time: " + m.Order.estimated_deliver_time + " <br />";
            }
            else
            {
                str += " Có lỗi xảy ra: " + message + "";
            }
            ltmessage.Text = str;
            txtKetQua.Text = "Success: " + Success + " <br> Message: " + message + "";
            ltketqua.Text = "Success: " + Success + " <br> Message: " + message + "";

        }
    }
}