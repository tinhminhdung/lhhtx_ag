using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using Entity;
using MoreAll;

namespace VS.E_Commerce.cms.Display.contact
{
    public partial class contact : System.Web.UI.UserControl
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
            this.Page.Form.DefaultButton = btgui.UniqueID;
            txtfullname.Focus();
            if (!base.IsPostBack)
            {
                load();
                // this.btgui.Text = this.label("I_lienhegui");
            }
        }

        public void load()
        {
            #region Setting
            string str = "";
            string str2 = "";
            string str3 = "";
            List<Entity.Setting> Obj = SSetting.GETBYALL(language);
            if (Obj.Count >= 1)
            {
                foreach (Entity.Setting its in Obj)
                {
                    if (its.Properties == "Contactswidth")
                    {
                        str = its.Value;
                    }
                    if (its.Properties == "Contactsheight")
                    {
                        str2 = its.Value;
                    }
                    if (its.Properties == "Contactspath")
                    {
                        str3 = its.Value;
                    }
                    if (its.Properties == "LienHe")
                    {
                        this.ltcontactcontent.Text = its.Value;
                    }
                }
            }
            #endregion
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }

        protected void btgui_Click(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                #region Senmail
                if (!Commond.Setting("Emailden").Equals(""))
                        Senmail();
                #endregion
                
                #region Contacts
                Entity.Contacts obj = new Entity.Contacts();
                obj.vtitle = txttieude.Text;
                obj.vname = txtfullname.Text;
                obj.vaddress = txtaddress.Text;
                obj.vphone = txtphone.Text;
                obj.vemail = txtemail.Text;
                obj.vcontent = txtcontent.Text;
                obj.dcreatedate = DateTime.Now;
                obj.lang = language;
                obj.istatus = 0;
                if (SContacts.INSERT(obj) == true)
                {
                    this.ltmsg.Text = this.label("l_postsucess");
                }
                #endregion
                this.txtaddress.Text = "";
                this.txtcontent.Text = "";
                this.txtemail.Text = "";
                this.txtfullname.Text = "";
                this.txtphone.Text = "";
                this.txttieude.Text = "";
            }
            catch (Exception)
            {
                this.ltmsg.Text = "Hệ thống đang xẩy ra sự cố , yêu cầu bạn liên hệ với quản trị viên website";
            }
        }

        void Senmail()
        {
            try
            {
                string title = "";
                System.Text.StringBuilder strb = new System.Text.StringBuilder();
                strb.AppendLine("<div style=\"width:100%; padding:10px; line-height:22px;\"> ");
                strb.AppendLine("<div style=\"font-size:18px; font-weight:bold; text-align:center; color:#F00; text-decoration:none;text-transform:uppercase;\">Thông tin liên hệ của khách hàng</div> ");
                strb.AppendLine("<div style=\"font-weight:bold; color:#666; padding-top:10px; text-align:center;text-decoration:none;\"> Website: " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "/</div>");
                strb.AppendLine("<div style=\" color:#666; padding-top:10px\"> ");
                strb.AppendLine("<div style=\"font-size:14px;font-weight:bold; padding-bottom:5px;text-transform:uppercase; text-decoration:underline;color:#fe0505\">Thông tin khách hàng</div> ");
                strb.AppendLine(" <table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\"> ");
                strb.AppendLine(" <tr> ");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px; width:20%\">Họ và tên:</td> ");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;color:#fe0505\">" + this.txtfullname.Text.Trim() + "</td> ");
                strb.AppendLine("</tr> ");
                strb.AppendLine("<tr> ");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Địa chỉ:</td>");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">" + this.txtaddress.Text.Trim() + "</td>");
                strb.AppendLine("</tr>");
                strb.AppendLine("<tr>");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Điện thoại:</td>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">" + this.txtphone.Text.Trim() + "</td>");
                strb.AppendLine("</tr>");
                strb.AppendLine(" <tr>");
                strb.AppendLine("  <td style=\"border-bottom:dotted 1px #d6d6d6;height:22px;\">Email:</td>");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">" + this.txtemail.Text.Trim() + "</td>");
                strb.AppendLine("</tr>");
                strb.AppendLine("<tr>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Tiêu đề:</td>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\"> " + txttieude.Text + "</td>");
                strb.AppendLine("</tr>");
                strb.AppendLine("<tr>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Ngày Gửi:</td>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\"> " + DateTime.Now.AddYears(3).ToString("MM/dd/yyyy HH:mm:ss") + "</td>");
                strb.AppendLine("</tr>");
                strb.AppendLine("<tr>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Nội dung:</td>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\"> " + this.txtcontent.Text.Trim() + "</td>");
                strb.AppendLine("</tr>");
                strb.AppendLine("</table>");
                strb.AppendLine("</div>");
                strb.AppendLine("</div>");
                string email = Email.email();
                string password = Email.password();
                int port = Convert.ToInt32(Email.port());
                string host = Email.host();
                MailUtilities.SendMail("Liên hệ từ " + txtfullname.Text.Trim() + "", email, password, Commond.Setting("Emailden"), host, port, "Thông tin liên hệ của khách hàng từ Website: " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "", strb.ToString());
                //MailUtilities.SendMail("Liên hệ từ " + txtfullname.Text.Trim() + "", email, password, this.txtemail.Text.Trim(), host, port, "Thông tin liên hệ của khách hàng từ Website: " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "", strb.ToString());
            }
            catch (Exception)
            { }
        }
    }
}