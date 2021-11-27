using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VS.E_Commerce.VanChuyen
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public class PriceGhtkRequestModel
        {
            public string pick_province { get; set; }
            public string pick_district { get; set; }
            public string province { get; set; }
            public string district { get; set; }
            public string address { get; set; }
            public float weight { get; set; }
            public double value { get; set; }
            public string transport { get; set; }
        }
        public class PriceGhtkResultModel
        {
            public bool success { get; set; }
            public string message { get; set; }
            public GhtkFeeModel fee { get; set; }
            public OrderCart order { get; set; }
            public error error { get; set; }

        }

        public class TinhPhiVanChuyen
        {
            public bool success { get; set; }
            public string message { get; set; }
            public Order order { get; set; }

        }
        public class Order
        {
            public string label_id { get; set; }
            public string partner_id { get; set; }
            public string status { get; set; }
            public string status_text { get; set; }
            public string created { get; set; }
            public string modified { get; set; }
            public string message { get; set; }
            public string pick_date { get; set; }
            public string deliver_date { get; set; }
            public string customer_fullname { get; set; }
            public string customer_tel { get; set; }
            public string address { get; set; }
            public string storage_day { get; set; }
            public string ship_money { get; set; }
            public string insurance { get; set; }
            public string value { get; set; }
            public string weight { get; set; }
            public string pick_money { get; set; }
            public string is_freeship { get; set; }
        }

        public class OrderCart
        {
            public string partner_id { get; set; }
            public string label { get; set; }
            public string area { get; set; }
            public string fee { get; set; }
            public string insurance_fee { get; set; }
            public string estimated_pick_time { get; set; }
            public string estimated_deliver_time { get; set; }
            public string products { get; set; }
            public string status_id { get; set; }
            public string ghtk_label { get; set; }


        }

        public class error
        {
            public string code { get; set; }
            public string partner_id { get; set; }
            public string ghtk_label { get; set; }
            public string created { get; set; }
            public string status { get; set; }

        }

        public class GhtkFeeModel
        {
            public string name { get; set; }
            public decimal fee { get; set; }
            public decimal insurance_fee { get; set; }
            public string delivery_type { get; set; }
            public double a { get; set; }
            public string dt { get; set; }
            public bool delivery { get; set; }
        }

        string LinkApi = "https://dev.ghtk.vn/";
        protected void Page_Load(object sender, EventArgs e)
        {
            // code mới đang phát triển
            //  TinhPhiNews();
            DatHangNews();
            //  Kiemtratrangthaidonhang();

            // code dang cũ chạy trực tiếp
            //DatHang();
            //TinhPhi();
        }
        public void DatHang()
        {
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://dev.ghtk.vn/services/shipment/order/?ver=1.5");
            tRequest.Method = "POST";
            tRequest.UseDefaultCredentials = true;
            tRequest.PreAuthenticate = true;

            tRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
            //định dạng JSON
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add("Token", "3d6C65727cF5A5791E495236d31Be8E64cc7f4ea");
            string RegArr = string.Empty;

            string postData = "{ \"products\": [{ \"name\": \"bút\", \"weight\": 0.1, \"quantity\": 1 }, { \"name\": \"tẩy\", \"weight\": 0.2, \"quantity\": 1 }], \"order\": { \"id\": \"135111\", \"pick_name\": \"HCM-nội thành\", \"pick_address\": \"590 CMT8 P.11\", \"pick_province\": \"TP. Hồ Chí Minh\", \"pick_district\": \"Quận 3\", \"pick_ward\": \"Phường 1\", \"pick_tel\": \"0911222333\", \"tel\": \"0911222333\", \"name\": \"GHTK - HCM - Noi Thanh\", \"address\": \"123 nguyễn chí thanh\", \"province\": \"TP. Hồ Chí Minh\", \"district\": \"Quận 1\", \"ward\": \"Phường Bến Nghé\", \"hamlet\": \"Khác\", \"is_freeship\": \"1\", \"pick_date\": \"2016-09-30\", \"pick_money\": 47000, \"note\": \"Khối lượng tính cước tối đa: 1.00 kg\", \"value\": 3000000, \"transport\": \"fly\" } } ";
            //  postData = new StringContent(postData, Encoding.UTF8, \"application/json\");

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();

            //Response.Write(sResponseFromServer);

            var data = JsonConvert.DeserializeObject<PriceGhtkResultModel>(sResponseFromServer, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            if (data.success)
            {
                Response.Write("message:" + data.message + "<br>");
                Response.Write("order:" + data.order + "<br>");
                Response.Write("partner_id:" + data.order.partner_id + "<br>");
                Response.Write("status_id:" + data.order.status_id + "<br>");
                Response.Write("Mã đơn hàng: " + data.order.label + "<br>");
            }
            else
            {
                Response.Write("else message: " + data.message + "<br>");
                Response.Write("else created: " + data.error.created + "<br>");
                Response.Write("else partner_id: " + data.error.partner_id + "<br>");
            }
            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }
        public void TinhPhi()
        {
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://dev.ghtk.vn/services/shipment/fee?/services/shipment/fee?address=P.503%20t%C3%B2a%20nh%C3%A0%20Auu%20Vi%E1%BB%87t,%20s%E1%BB%91%201%20L%C3%AA%20%C4%90%E1%BB%A9c%20Th%E1%BB%8D&province=H%C3%A0%20n%E1%BB%99i&district=Qu%E1%BA%ADn%20C%E1%BA%A7u%20Gi%E1%BA%A5y&pick_province=H%C3%A0%20N%E1%BB%99i&pick_district=Qu%E1%BA%ADn%20Hai%20B%C3%A0%20Tr%C6%B0ng&weight=1000&value=3000000");
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

            var data = JsonConvert.DeserializeObject<PriceGhtkResultModel>(sResponseFromServer, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            //Response.Write(data.fee.fee);

            //Response.Write(sResponseFromServer);

            if (data.success)
            {
                Response.Write(data.message + "<br>");
                Response.Write(data.order + "<br>");
                Response.Write(data.order.partner_id + "<br>");
                Response.Write(data.order.status_id + "<br>");

            }
            else
            {
                Response.Write(data.message + "<br>");
                Response.Write(data.error.created + "<br>");
                Response.Write(data.error.partner_id + "<br>");
            }

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }


        // Áp dụng
        public void TinhPhiNews()
        {
            string value = "9000000";// tiền vnd về sản phẩm
            string weight = "90000";// trọng lượng 
            string pick_province = "Hà Nội";//Tên tỉnh/thành phố nơi lấy hàng hóa
            string pick_district = "Quận Hai Bà Trưng";
            string province = "Hà Nội";
            string district = "Quận Cầu Giấy";
            string address = "P.503 tòa nhà Auu Việt, số 1 Lê Đức Thọ";

            string tRequest = "https://dev.ghtk.vn/services/shipment/fee?/services/shipment/fee?address=" + address + "&province=" + province + "&district=" + district + "&pick_province=" + pick_province + "&pick_district=" + pick_district + "&weight=" + weight + "&value=" + value + "";
            var Url = Get(tRequest);

            //PriceGhtkResultModel chuyển các obj thành các trường để gọi ra ở dưới cho Dễ.
            // chuyển các obj trả về thành 1 dạng JsonConvert và add vào entity  PriceGhtkResultModel
            var data = JsonConvert.DeserializeObject<PriceGhtkResultModel>(Url, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            Response.Write("TinhPhiNews <br>");
            if (data.success)
            {
                Response.Write(data.fee.name + "<br>");//Tên gói cước được áp dụng, 
                Response.Write("Cước vận chuyển tính theo VNĐ: " + data.fee.fee + "<br>");//Integer - Cước vận chuyển tính theo VNĐ
                Response.Write("Hỗ trợ giao ở địa chỉ này chưa: " + data.fee.delivery + "<br>");//Hỗ trợ giao ở địa chỉ này chưa, nếu điểm giao đã được GHTK hỗ trợ giao trả về true, nếu GTHK chưa hỗ trợ giao đến khu vực này thì trả về false
                Response.Write("Giá bảo hiểm tính theo VNĐ: " + data.fee.insurance_fee + "<br>");// Integer - Giá bảo hiểm tính theo VNĐ
            }
            else
            {
                Response.Write(data.message + "<br>");
                // Response.Write(data.error.created + "<br>");
                // Response.Write(data.error.partner_id + "<br>");
            }
        }
        public void DatHangNews()
        {
            string tRequest = "https://dev.ghtk.vn/services/shipment/order/?ver=1.5";
            // string postData = "{ \"products\": [{ \"name\": \"bú11t\", \"weight\": 0.1, \"quantity\": 1 }, { \"name\": \"tẩy11\", \"weight\": 0.2, \"quantity\": 1 }], \"order\": { \"id\": \"3412\", \"pick_name\": \"HCM-nội thành\", \"pick_address\": \"590 CMT8 P.11\", \"pick_province\": \"TP. Hồ Chí Minh\", \"pick_district\": \"Quận 3\", \"pick_ward\": \"Phường 1\", \"pick_tel\": \"0911222333\", \"tel\": \"0911222333\", \"name\": \"GHTK - HCM - Noi Thanh\", \"address\": \"500 nguyễn chí thanh\", \"province\": \"TP. Hồ Chí Minh\", \"district\": \"Quận 1\", \"ward\": \"Phường Bến Nghé\", \"hamlet\": \"Khác\", \"is_freeship\": \"1\", \"pick_date\": \"2016-09-30\", \"pick_money\": 47000, \"note\": \"Khối lượng tính cước tối đa: 1.00 kg\", \"value\": 3000000, \"transport\": \"fly\" } } ";

            string postData = "";
            postData += "    {";
            postData += "\"products\": [";

            postData += "{";
            postData += "    \"name\": \"Máy tính 1\",";
            postData += "    \"weight\": 0.1,";
            postData += "    \"quantity\": 1";
            postData += "},";

            postData += "{";
            postData += "    \"name\": \"Máy tính 2\",";
            postData += "    \"weight\": 0.2,";
            postData += "    \"quantity\": 1";
            postData += "}";

            postData += "],";
            postData += "\"order\": {";

            postData += "    \"id\": \"225452\",";
            postData += "    \"pick_name\": \"HCM-nội thành\",";
            postData += "    \"pick_address\": \"590 CMT8 P.11\",";
            postData += "    \"pick_province\": \"TP. Hồ Chí Minh\",";
            postData += "    \"pick_district\": \"Quận 3\",";
            postData += "    \"pick_ward\": \"Phường 1\",";
            postData += "    \"pick_tel\": \"0976658433\",";
            postData += "    \"tel\": \"0976658433\",";
            postData += "    \"name\": \"GHTK - HCM - Noi Thanh\",";
            postData += "    \"address\": \"450 nguyễn chí thanh\",";
            postData += "    \"province\": \"TP. Hồ Chí Minh\",";
            postData += "    \"district\": \"Quận 1\",";
            postData += "    \"ward\": \"Phường Bến Nghé\",";
            postData += "    \"hamlet\": \"Khác\",";
            postData += "    \"is_freeship\": \"1\",";
            postData += "    \"pick_date\": \"2016-09-30\",";
            postData += "    \"pick_money\": 47000,";
            postData += "    \"note\": \"Khối lượng tính cước tối đa: 1.00 kg\",";
            postData += "    \"value\": 3000000,";
            postData += "    \"transport\": \"fly\"";
            postData += "}";
            postData += "}";


            Response.Write(postData + "<br><br>");

            var Url = Post(tRequest, postData);

            var data = JsonConvert.DeserializeObject<DonHang.DangDonHang>(Url, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

           // string tmp = Url;
           //// DonHang.DangDonHang data = JsonConvert.DeserializeObject<DonHang.DangDonHang>(tmp);

           // JavaScriptSerializer serializer = new JavaScriptSerializer();
           // DonHang.DangDonHang data = serializer.Deserialize<DonHang.DangDonHang>(tmp);

            //if (data.success)
            //{
            //    Response.Write(data.message + "<br>");
            //    Response.Write(data.order + "<br>");
            //    Response.Write(data.order.partner_id + "<br>");
            //    Response.Write(data.order.status_id + "<br>");

            //}
            //else
            //{
            //    Response.Write(data.message + "<br>");
            //    Response.Write(data.error.created + "<br>");
            //    Response.Write(data.error.partner_id + "<br>");
            //}


            //"{\"success\":true,\"message\":\"",\"order\":{\"partner_id\":\"55671\",\"label\":\"S69611.SGB.22C2.999995922\",\"area\":\"1\",\"fee\":\"22000\",\"status_id\":\"1\",\"insurance_fee\":\"0\",\"estimated_pick_time\":\"S\\u00e1ng 2020-09-23\",\"estimated_deliver_time\":\"Chi\\u1ec1u 2020-09-23\",\"products\":[],\"tracking_id\":999995922,\"sorting_code\":\"SGB.22C2\"}}"

            //"{\"success\":false,\"message\":\" h\\u1ec7 th\\u1ed1ng GHTK\",\"error\":{\"code\":\"ORDER_ID_EXIST\",\"partner_id\":\"34780\",\"ghtk_label\":\"S69611.SGB.22C2.999995910\",\"created\":\"2020-09-23 09:21:53\",\"status\":\"1\"}}"
            if (data.success)
            {
                Response.Write("message:" + data.message + "<br>");
                Response.Write("order:" + data.order + "<br>");
                Response.Write("partner_id:" + data.order.partner_id + "<br>");
                Response.Write("label:" + data.order.label + "<br>");
                Response.Write("area:" + data.order.area + "<br>");
                Response.Write("fee:" + data.order.fee + "<br>");
                Response.Write("insurance_fee:" + data.order.insurance_fee + "<br>");

                Response.Write("estimated_pick_time:" + data.order.estimated_pick_time + "<br>");
                Response.Write("estimated_deliver_time:" + data.order.estimated_deliver_time + "<br>");
                Response.Write("status_id:" + data.order.status_id + "<br>");

            }
            else
            {
                Response.Write("else message: " + data.message + "<br>");
                Response.Write("else created: " + data.error.created + "<br>");
                Response.Write("else partner_id: " + data.error.partner_id + "<br>");
                Response.Write("Mã đơn hàng: " + data.error.ghtk_label + "<br>");
            }
        }

        public void Kiemtratrangthaidonhang()
        {
            //Giả sử đơn hàng mã “S1.A1.17373471” (mã trên hệ thống khách hàng là “1234567”) được cập nhật “"đã giao hàng thành công”.
            string Iddonhang = "S69611.SGB.22C2.999995910";
            string tRequest = LinkApi + "services/shipment/v2/" + Iddonhang + "";
            var Url = Get(tRequest);

            //PriceGhtkResultModel chuyển các obj thành các trường để gọi ra ở dưới cho Dễ.
            // chuyển các obj trả về thành 1 dạng JsonConvert và add vào entity  PriceGhtkResultModel

            var data = JsonConvert.DeserializeObject<TinhPhiVanChuyen>(Url, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            Response.Write("TinhPhiNews <br>");
            if (data.success)
            {
                //                {
                //    "success": true,
                //    "message": "",
                //    "order": {
                //        "label_id": "S1.A1.17373471",
                //        "partner_id": "1234567",
                //        "status": "1",
                //        "status_text": "Chưa tiếp nhận",
                //        "created": "2016-10-31 22:32:08",
                //        "modified": "2016-10-31 22:32:08",
                //        "message": "Không giao hàng 1 phần",
                //        "pick_date": "2017-09-13",
                //        "deliver_date": "2017-09-14",
                //        "customer_fullname": "Vân Nguyễn",
                //        "customer_tel": "0911222333",
                //        "address": "123 nguyễn chí thanh Quận 1, TP Hồ Chí Minh",
                //        "storage_day": "3",
                //        "ship_money": "16500",
                //        "insurance": "16500",
                //        "value": "3000000",
                //        "weight": "300",
                //        "pick_money": 47000,
                //        "is_freeship": "1"
                //    }
                //}


                //"{\"success\":true,\"message\":\"\",\"order\":{\"label_id\":\"S69611.SGB.22C2.999995910\",\"partner_id\":\"34780\",\"status\":\"1\",\"value\":\"3000000\",\"insurance\":\"0\",\"ship_money\":\"22000\",\"storage_day\":\"0\",\"created\":\"2020-09-23 09:21:53\",\"pick_money\":\"47000\",\"is_freeship\":\"1\",\"modified\":\"2020-09-23 09:21:53\",\"customer_fullname\":\"GHTK - HCM - Noi Thanh\",\"customer_tel\":\"0911222333\",\"message\":\"Kh\\u1ed1i l\\u01b0\\u1ee3ng t\\u00ednh c\\u01b0\\u1edbc t\\u1ed1i \\u0111a: 1.00 kg\",\"pick_date\":\"2020-09-23\",\"deliver_date\":\"2020-09-23\",\"address\":\"123 nguy\\u1ec5n ch\\u00ed thanh Ph\\u01b0\\u1eddng B\\u1ebfn Ngh\\u00e9, Qu\\u1eadn 1, TP H\\u1ed3 Ch\\u00ed Minh\",\"weight\":\"300\",\"status_text\":\"Ch\\u01b0a ti\\u1ebfp nh\\u1eadn\"}}"


                Response.Write(data.order.label_id + "<br>");//
                Response.Write(data.order.status_text + "<br>");//
                Response.Write(data.order.message + "<br>");//
                Response.Write(data.order.customer_fullname + "<br>");//
                Response.Write(data.order.address + "<br>");//

            }
            else
            {
                Response.Write(data.message + "<br>");
                // Response.Write(data.error.created + "<br>");
                // Response.Write(data.error.partner_id + "<br>");
            }
        }


        // Hàm Dùng chung
        protected string Post(string Link, string Conten)
        {

            WebRequest request = WebRequest.Create(Link);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = Conten;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json";
            request.Headers.Add("Token", "3d6C65727cF5A5791E495236d31Be8E64cc7f4ea");

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();


            //WebRequest tRequest;
            //tRequest = WebRequest.Create(Link);
            //tRequest.Method = "POST";
            //tRequest.UseDefaultCredentials = true;
            //tRequest.PreAuthenticate = true;

            //tRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
            ////định dạng JSON
            //tRequest.ContentType = "application/json";
            //tRequest.Headers.Add("Token", "3d6C65727cF5A5791E495236d31Be8E64cc7f4ea");
            //string RegArr = string.Empty;

            //string postData = Conten;
            //Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //tRequest.ContentLength = byteArray.Length;

            //Stream dataStream = tRequest.GetRequestStream();
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //dataStream.Close();
            //WebResponse tResponse = tRequest.GetResponse();
            //dataStream = tResponse.GetResponseStream();
            //StreamReader tReader = new StreamReader(dataStream);
            //String sResponseFromServer = tReader.ReadToEnd();


            byte[] bytes = Encoding.Default.GetBytes(responseFromServer);
            responseFromServer = Encoding.UTF8.GetString(bytes);

            Response.Write("<br>*******<br>" + (responseFromServer) + "<br>*******<br>");

            //var data = JsonConvert.DeserializeObject<PriceGhtkResultModel>(sResponseFromServer, new JsonSerializerSettings
            //{
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //});

            //if (data.success)
            //{
            //    Response.Write(data.message + "<br>");
            //    Response.Write(data.order + "<br>");
            //    Response.Write(data.order.partner_id + "<br>");
            //    Response.Write(data.order.status_id + "<br>");

            //}
            //else
            //{
            //    Response.Write(data.message + "<br>");
            //    Response.Write(data.error.created + "<br>");
            //    Response.Write(data.error.partner_id + "<br>");
            //}

            //tReader.Close();
            //dataStream.Close();
            //tResponse.Close();

            return responseFromServer;
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

            //var data = JsonConvert.DeserializeObject<PriceGhtkResultModel>(sResponseFromServer, new JsonSerializerSettings
            //{
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //});
            //Response.Write(data.fee.fee);

            Response.Write("<br>*******<br>" + sResponseFromServer + "<br>*******<br>");

            //if (data.success)
            //{
            //    Response.Write(data.message + "<br>");
            //    Response.Write(data.order + "<br>");
            //    Response.Write(data.order.partner_id + "<br>");
            //    Response.Write(data.order.status_id + "<br>");

            //}
            //else
            //{
            //    Response.Write(data.message + "<br>");
            //    Response.Write(data.error.created + "<br>");
            //    Response.Write(data.error.partner_id + "<br>");
            //}

            tReader.Close();
            dataStream.Close();
            tResponse.Close();

            return sResponseFromServer;
        }

    }
}