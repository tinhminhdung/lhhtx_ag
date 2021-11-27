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
    public partial class Resetpassword : System.Web.UI.UserControl
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
            this.Page.Form.DefaultButton = btnresets.UniqueID;
            if (!IsPostBack)
            {
                RequiredFieldValidator8.Text = label("thanhvien4");
                RequiredFieldValidator4.Text = label("thanhvien3");
                // btnresets.Text = label("thanhvien5");
                #region UpdatePanel
                this.Page.Form.Enctype = "multipart/form-data";
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnresets);
                #endregion
            }
        }
        protected void btnregisters_Click(object sender, EventArgs e)
        {
            Fusers item = new Fusers();
            System.Threading.Thread.Sleep(1000);
            if (item.Detailemail(this.txtemails.Text.Trim().ToLower()).Rows.Count < 1)
            {
                this.ltmsg.Text = "Email của bạn không tồn tại trong hệ thống";
            }
            else
            {

                // cách này là đổi mật khẩu mới
                //try
                //{
                //    DataTable table = item.Detailemail(txtemails.Text.Trim().ToLower());
                //    if (table.Rows.Count > 0)
                //    {


                //        string hash = DateTime.Now.Ticks.ToString();
                //        item.Update_validatekey_byemail(this.txtemails.Text.Trim().ToLower(), hash);
                //        string title = "";
                //        string body = "";
                //        title = "Cập nhật lại mật khẩu!";
                //        string str4 = "http://" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "/xac-nhan/" + hash;

                //        body += "Thông tin tài khoản từ web : http://" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "/" + "</p>";
                //        body += "<p>C\x00e1m ơn bạn đ\x00e3 tham gia đăng k\x00fd tại website của chúng tôi! </p>";
                //        body += "<p>Vui l\x00f2ng nhấn v\x00e0o li\x00ean kết sau để thực hiện việc cập nhật mật khẩu. </p>";
                //        body += "<p>Vui lòng : <a style='color:red; font-weight:bold; font-size:14px' href='" + str4 + "'  target='_blank'>Click vào đây</a></p>";
                //        body += "<p><span style='color:red; font-size:12px'> Lưu ý: Link này chỉ được click vào 1 lần, không thể sử dụng click lại cho lần sau.</span></p>";

                //        string email = Email.email();
                //        string password = Email.password();
                //        int str6 = Convert.ToInt32(Email.port());
                //        string host = Email.host();

                //        MailUtilities.SendMail("Cập nhật lại mật khẩu Mới!", email, password, table.Rows[0]["vemail"].ToString(), host, Convert.ToInt32(str6), title, body);
                //        item.Detailemail(table.Rows[0]["vemail"].ToString());
                //    }
                //    this.ltmsg.Text = "Email x\x00e1c nhận đ\x00e3 được gởi đến t\x00e0i khoản Email của bạn. <br>Vui l\x00f2ng check Email để x\x00e1c nhận";
                //}
                //catch (Exception)
                //{
                //    this.ltmsg.Text = "Có lỗi xảy ra khi gửi mail";
                //}

                // cách này là lấy lại mật khẩu cũ
                try
                {
                    DataTable table = item.Detailemail(txtemails.Text.Trim().ToLower());
                    if (table.Rows.Count > 0)
                    {
                        try
                        {
                            item.users_update_SolanLaymatkhau(table.Rows[0]["vuserun"].ToString());
                        }
                        catch (Exception)
                        { }

                        string info = "Thông tin tài khoản từ web : http://" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "/" + "<br>Thông tin tài khoản của bạn!<br>Tên đăng nhập: <b>" + table.Rows[0]["vuserun"].ToString() + " </b><br>Mật khẩu:  <b>" + table.Rows[0]["vuserpwd"].ToString() + " </b>";
                        string title = "Cung cấp lại mật khẩu!";

                        string email = Email.email();
                        string password = Email.password();
                        int str6 = Convert.ToInt32(Email.port());
                        string host = Email.host();

                        MailUtilities.SendMail("Cung cấp lại mật khẩu!", email, password, table.Rows[0]["vemail"].ToString(), host, Convert.ToInt32(str6), title, info);
                        item.Detailemail(table.Rows[0]["vemail"].ToString());
                    }
                    this.ltmsg.Text = "Email x\x00e1c nhận đ\x00e3 được gởi đến t\x00e0i khoản Email của bạn. <br>Vui l\x00f2ng check Email để x\x00e1c nhận";
                }
                catch (Exception)
                {
                    this.ltmsg.Text = "Có lỗi xảy ra khi gửi mail";
                }
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}