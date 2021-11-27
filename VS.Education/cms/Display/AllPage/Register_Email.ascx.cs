using Entity;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.AllPage
{
    public partial class Register_Email : System.Web.UI.UserControl
    {
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
        }
        protected void btRegisterbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RegularExpressions.IsValidEmail(this.txtRegisterEmail.Text))
                {
                    ltmgs.Text = "Kiểm tra dữ liệu nhập vào";
                    txtRegisterEmail.Focus();
                }
                else if (SMarketing.Name_Text("select * from Marketing where Email='" + this.txtRegisterEmail.Text.Trim().ToLower() + "'").Count > 0)
                {
                    ltmgs.Text = "Email này đã tồn tại trong hệ thống";
                    txtRegisterEmail.Focus();
                }
                else
                {
                    #region Marketing
                    Entity.Marketing ob = new Entity.Marketing();
                    ob.Name = "Kh\x00e1ch h\x00e0ng nhận th\x00f4ng b\x00e1o qua email";
                    ob.Email = txtRegisterEmail.Text;
                    ob.Phone = "";
                    ob.Address = "";
                    ob.dcreatedate = DateTime.Now;
                    ob.istatus = 0;
                    if (SMarketing.INSERT(ob) == true)
                    {
                        ltmgs.Text = "Đăng ký nhận tin qua Email thành công";
                    }
                    #endregion
                    this.txtRegisterEmail.Text = "";
                }
            }
            catch (Exception)
            { }

        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}