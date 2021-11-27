using Framework;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class Autologon : System.Web.UI.Page
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string U1 = "";
        string U2 = "";
        string ID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["U1"] != null && !Request["U1"].Equals(""))
            {
                U1 = Request["U1"];
            }
            if (Request["U2"] != null && !Request["U2"].Equals(""))
            {
                U2 = Request["U2"];
            }
            if (Request["ID"] != null && !Request["ID"].Equals(""))
            {
                ID = Request["ID"];
            }
            if (MoreAll.MoreAll.GetCookies("UName") != "")
            {
                Fusers item = new Fusers();
                List<Entity.users> table = Susers.Name_Text("select * from users where vuserun='" + U1.Trim().ToLower() + "' and iuser_id=" + (ID.Trim().ToLower()) + " ");// and DuyetTienDanap=1 phải nạp tiền xong mới cho đăng nhập
                if (table.Count > 0)
                {
                    #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
                    Commond.CheckNgayHetHan(table[0].iuser_id.ToString());
                    #endregion

                    MoreAll.MoreAll.SetCookie("Members", U1, 5000);
                    MoreAll.MoreAll.SetCookie("MembersID", table[0].iuser_id.ToString(), 5000);
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
                        obj.ViTangTienVip = table[0].ViTangTienVip.ToString();
                        db.HistoryLogins.InsertOnSubmit(obj);
                        db.SubmitChanges();
                    }
                    catch (Exception)
                    { }
                    Response.Redirect("/vi-tien.html");
                }
            }
            else
            {
                Response.Redirect("/");
            }
        }
        protected string CheckBrowserCaps()
        {
            System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
            if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
            {
                return "Mobile : " + myBrowserCaps.Browser + " <br /><span style=\"color:red; font-weight:bold\"> Admin:  " + MoreAll.MoreAll.GetCookies("UName").ToString() + "</span>";
            }
            return "Destop: " + myBrowserCaps.Browser + "  <br /><span style=\"color:red; font-weight:bold\"> Admin:  " + MoreAll.MoreAll.GetCookies("UName").ToString() + "</span>";
        }
    }
}