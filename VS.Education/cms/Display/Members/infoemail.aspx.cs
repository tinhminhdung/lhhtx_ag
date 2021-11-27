using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Framework;
using System.Data;

namespace VS.E_Commerce.cms.Display.Members
{
    public partial class infoemail : System.Web.UI.Page
    {
        string key = "";
        private string language = Captionlanguage.Language;
        protected void Page_Load(object sender, EventArgs e)
        {
            Fusers item = new Fusers();
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
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
                    item.Active_vvalidatekey(table.Rows[0]["vuserun"].ToString(), newpassword);
                    ltthongbao.Text = "Thông tin cập nhật lại mật khẩu mới đã được chúng tôi gủi vào email của quý khách .<br />Yêu cầu quý khách check tra lại email để lấy mật khẩu.<br />Chân thành cảm ơn.";
                }
            }
            else
            {
                ltthongbao.Text = "Thông tin mật khẩu đã được chúng tôi gủi vào email của quý vị .<br />Yêu cầu quý khách kiểm tra lại email.<br />Chân thành cảm ơn.";
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}