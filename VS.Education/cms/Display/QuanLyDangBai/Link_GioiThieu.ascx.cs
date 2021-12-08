using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class Link_GioiThieu : System.Web.UI.UserControl
    {
        string ssl = "http://";
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

            ShowInfo();
        }
        private void ShowInfo()
        {
            if (Commond.Setting("SSL").Equals("1"))
            {
                ssl = "https://";
            }

            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                if (table != null)
                {
                    //if (table.DuyetTienDanap == 0)
                    //{
                    //    Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này. Yêu cầu kích hoạt tài khoản thành viên.');window.location.href='/vi-tien.html'; </script>");
                    //}

                    // hdid.Value = table.iuser_id.ToString();
                    txtlinkgioithieu.Text = ssl + Request.Url.Host + "?link=" + table.vphone.ToString() + "";
                }
            }
        }
    }
}