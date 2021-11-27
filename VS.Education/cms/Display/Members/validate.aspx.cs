using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Framework;

namespace VS.E_Commerce.cms.Display.Members
{
    public partial class validate : System.Web.UI.Page
    {
        string key = "";
        string str = "";
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
            try
            {
                Fusers item = new Fusers();
                if (Request["key"] != null && !Request["key"].Equals(""))
                {
                    key = Request["key"];
                }
                if (item.vvalidatekey(key.ToLower()).Rows.Count > 0)
                {
                    DataTable table = item.vvalidatekey(key);
                    if (table.Rows.Count > 0)
                    {
                        string newpassword = DateTime.Now.Ticks.ToString();
                        newpassword = newpassword.Substring(newpassword.Length - 8, 7);
                        item.users_update_updatepassword(table.Rows[0]["vuserun"].ToString(), newpassword);
                        string info = "Thông tin tài khoản từ web : http://" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "/" + "<br>Thông tin tài khoản của bạn!<br>Tên đăng nhập: <b>" + table.Rows[0]["vuserun"].ToString() + " </b><br>Mật khẩu mới:  <b>" + newpassword + " </b>";
                        string title = "Cập nhật lại mật khẩu Mới!";

                        string email = Email.email();
                        string password = Email.password();
                        int str6 = Convert.ToInt32(Email.port());
                        string host = Email.host();

                        MailUtilities.SendMail("Cập nhật lại mật khẩu Mới!", email, password, table.Rows[0]["vemail"].ToString(), host, Convert.ToInt32(str6), title, info);
                        item.Detailemail(table.Rows[0]["vemail"].ToString());
                        base.Response.Redirect("/xac-nhan-email/" + key);
                    }
                }
                base.Response.Redirect("/xac-nhan-email/" + key );
            }
            catch (Exception) { }
        }
    }
}