using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Web.Security;
using System.Data;
using Framework;
using Entity;
using Services;

namespace VS.E_Commerce.cms.Display.Members
{
    public partial class Login : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
        string url = "";
        DatalinqDataContext db = new DatalinqDataContext();
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
            url = Request.Url.ToString();
            // url = Request.RawUrl.ToString();
            this.Page.Form.DefaultButton = btnlogin.UniqueID;
            if (!base.IsPostBack)
            {

            }
        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if ((this.txt_Uname.Text.Trim().Length < 1) || (this.txt_password.Text.Trim().Length < 1))
            {
                this.ltmsg.Text = label("login1");
            }
            else
            {
                Fusers item = new Fusers();
                List<Entity.users> table = Susers.Name_Text("select * from users where vuserun='" + this.txt_Uname.Text.Trim().ToLower() + "' and vuserpwd='" + (this.txt_password.Text.Trim().ToLower()) + "' and istatus=1");// and DuyetTienDanap=1 phải nạp tiền xong mới cho đăng nhập
                if (table.Count < 1)
                {
                    this.ltmsg.Text = label("login2");
                }
                else
                {
                    #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
                    Commond.CheckNgayHetHan(table[0].iuser_id.ToString());
                    #endregion

                    //RemoveCache.Products();
                    FormsAuthentication.SetAuthCookie(txt_Uname.Text, false);
                    MoreAll.MoreAll.SetCookie("Members", txt_Uname.Text, 5000);
                    MoreAll.MoreAll.SetCookie("MembersID", table[0].iuser_id.ToString(), 5000);
                    //  Response.Redirect("/");

                    try
                    {
                        ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                        HistoryLogin obj = new HistoryLogin();
                        obj.ThanhVien = int.Parse(table[0].iuser_id.ToString());
                        obj.IP = utlitities.REMOTE_ADDR;
                        obj.ThoiGian = DateTime.Now;
                        obj.ThietBi = CheckBrowserCaps();
                        obj.ViTangTienVip = table[0].ViTangTienVip.ToString();
                        obj.ViMuaHang = table[0].ViMuaHangAFF.ToString();
                        obj.ViQuanLy = table[0].VIAAFFILIATE.ToString();
                        obj.ViThuongMai = table[0].TongTienCoinDuocCap.ToString();
                        db.HistoryLogins.InsertOnSubmit(obj);
                        db.SubmitChanges();

                    }
                    catch (Exception)
                    { }

                    System.Web.HttpContext.Current.Session["Gamemini"] = "";
                    if (HttpContext.Current.Request["ReturnUrl"] != null && !HttpContext.Current.Request["ReturnUrl"].Equals(""))
                    {
                        if (url.Contains("diemdanh.html"))
                        {
                            Response.Redirect("/#Diemdanh");
                        }
                        else
                        {
                            Response.Redirect(HttpContext.Current.Request["ReturnUrl"].ToString());
                        }
                    }
                    else
                    {
                        Response.Redirect("/vi-tien.html");
                    }
                }
            }
        }
        public static string DateTimess(string DateKetthuc)
        {
            DateTime oldDate = Convert.ToDateTime(DateKetthuc);
            DateTime newDate = DateTime.Now;
            TimeSpan ts = newDate - oldDate;
            if (ts.Days > 365)
            {
                return ("Hết hạn");
            }
            else
            {
                return ("Còn hạn");
            }
            // return ts.Days.ToString();
        }
        protected string CheckBrowserCaps()
        {
            System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
            if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
            {
                return "Mobile : " + myBrowserCaps.Browser;
            }
            return "Destop: " + myBrowserCaps.Browser;
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}