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
    public partial class Register_Phone : System.Web.UI.UserControl
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
                if (SMarketing.Name_Text("select * from Marketing where Phone='" + this.txtRegisterPhone.Text.Trim().ToLower() + "'").Count > 0)
                {
                    ltmgs.Text = "Phone này đã tồn tại trong hệ thống";
                    txtRegisterPhone.Focus();
                }
                else
                {
                    #region Marketing
                    Entity.Marketing ob = new Entity.Marketing();
                    ob.Name = "Gọi lại cho khách hàng";
                    ob.Email = "";
                    ob.Phone = txtRegisterPhone.Text;
                    ob.Address = "";
                    ob.dcreatedate = DateTime.Now;
                    ob.istatus = 0;
                    if (SMarketing.INSERT(ob) == true)
                    {
                        ltmgs.Text = "Chúng tôi sẽ gọi lại cho quý khách sớm nhất có thể";
                    }
                    #endregion
                    this.txtRegisterPhone.Text = "";
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