using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VS.E_Commerce.VanChuyen
{
    public class DonHang
    {
        public class DangDonHang
        {
            public bool success { get; set; }
            public string message { get; set; }
            public Order order { get; set; }
            public error error { get; set; }
        }

        public class Order
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
        }
        public class error
        {
            public string code { get; set; }
            public string partner_id { get; set; }
            public string ghtk_label { get; set; }
            public string created { get; set; }
            public string status { get; set; }

        }
    }
}