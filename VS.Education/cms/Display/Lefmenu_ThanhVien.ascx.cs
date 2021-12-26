using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Services;
using Framework;
using Entity;

namespace VS.E_Commerce.cms.Display
{
    public partial class Lefmenu_ThanhVien : System.Web.UI.UserControl
    {
        #region string
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
        #endregion
        private string su = "";
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["su"] != null && !Request["su"].Equals(""))
            {
                su = Request["su"];
            }
            #region #
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            #endregion
            #region Requesthp
            if (Request["hp"] != null && !Request["hp"].Equals(""))
            {
                hp = Request["hp"].ToString();
            }
            iEmptyIndex = hp.IndexOf("?");
            if (iEmptyIndex != -1)
            {
                hp = hp.Substring(0, iEmptyIndex);
            }
            #endregion
            if (!base.IsPostBack)
            {
                ShowInfo();
            }

        }
        private void ShowInfo()
        {
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                if (table != null)
                {
                    hdid.Value = table.iuser_id.ToString();

                    ltquanlydonhang.Text = ShowProducts(table.iuser_id.ToString());
                    ltlichsumuahang.Text = ShowLichSuMuahang(table.iuser_id.ToString());
                    if (table.ThanhVienAgLang.ToString() == "1")
                    {
                       // ltagland.Text += "<span class=\"tag-item  " + returnCSS("LaiSuatagland") + "\"> <a href=\"/lai-suat-agland.html\"><i class=\"fa fa-gift\"></i> Lãi suất AG LAND</a></span>";
                    }

                    if (table.Type.ToString() == "2")
                    {
                        ltagland.Text += "<span class=\"tag-item  " + returnCSS("lichsuduyetvitamgiu") + "\"> <a href=\"/lich-su-duyet-vi-tam-giu.html\"><i class=\"fa fa-clock-o\"></i> L/S duyệt ví tạm giữ bán hàng</a></span>";
                    }
                    lttinnhan.Text = "<a href=\"/tin-nhan.html\"><i class=\"fa fa-bell\" style=\" color:#127ec2\"></i> Tin nhắn của tôi (" + ShowTongThongBao(table.iuser_id.ToString()) + ")</a>";

                    //if (table.iuser_id.ToString() == Commond.SetThanhVienChuyenGia())
                    //{
                    //    ltchuyengia.Text = " <span class=\"tag-item " + returnCSS("TinNhan") + "\"><a href=\"/chuyen-gia.html\"><i class=\"fa fa-credit-card\" style=\" color:red\"></i> Lịch sử hh chuyên gia</a></span>";
                    //}
                  
                    if (table.istatus.ToString() == "0")
                    {
                        MoreAll.MoreAll.SetCookie("Members", "", -1);
                        MoreAll.MoreAll.SetCookie("MembersID", "", -1);
                        Response.Redirect("/");
                    }

                    //lthoahongQRCode.Text += "<span class=\"tag-item  " + returnCSS("HoaHongQRcode") + "\"> <a href=\"/Hoa-hong-QRcode.html\"><i class=\"fa fa-gift\"></i>Hoa hồng QRCode</a></span>";

                    if (table.TrangThaiThamGiaQRCode.ToString() == "1")
                    {
                        lthoahongQRCode.Text += "<span class=\"tag-item  " + returnCSS("lichsuthanhtoanqrcode") + "\"> <a href=\"/lich-su-thanh-toan-qrcode.html\"><i class=\"fa fa-clock-o\"></i>Lịch sử thanh toán QRCode</a></span>";
                    }

                    ltdanhsachthanhvien.Text = "<a href=\"/Danh-sach-thanh-vien.html?ID=" + table.iuser_id.ToString() + "\"><i class=\"fa fa-gift\"></i> Danh sách thành viên</a>";
                    // hdid.Value = table.iuser_id.ToString();
                    if (table.Type.ToString() == "2") // là nhà cung cấp
                    {
                        Panel1.Visible = true;
                    }
                    else
                    {
                        Panel1.Visible = false;
                    }
                }
            }
        }

        public string SubCart(string id)
        {
            string submn = "0";
            var dt = db.S_TimSanPhamDaBan(int.Parse(id.ToString())).ToList();
            for (int i = 0; i < dt.Count; i++)
            {
                submn = submn + "," + dt[i].ID_Cart.ToString();
            }
            return submn;
        }
        protected string ShowProducts(string id)
        {
            List<CartDetail> dt = db.ExecuteQuery<CartDetail>(@"select * from CartDetail where IDNhaCungCap=" + id + " and TrangThaiNguoiMuaHang=3").ToList();
            if (dt.Count > 0)
            {
                return dt.Count.ToString();
            }
            return "0";
        }
        public string ShowLichSuMuahang(string ID)
        {
            List<CartDetail> dt = db.ExecuteQuery<CartDetail>(@"select * from CartDetail where IDThanhVien=" + ID + " and TrangThaiNguoiMuaHang=3").ToList();
            //string sql = "select * from Carts where IDThanhVien=" + ID + "";
            //sql = sql + " and  Status=0 ";
            //sql = sql + " order by Create_Date desc";
            //List<LCart> dt = db.ExecuteQuery<LCart>(@"" + sql + "").ToList();
            if (dt.Count > 0)
            {
                return dt.Count.ToString();
            }
            return "0";
        }
        protected string returnCSS(string ctrol)
        {
            if ((this.su != "") && this.su.Equals(ctrol))
            {
                return "activex";
            }
            return "";
        }
        protected string ShowTongThongBao(string id)
        {
            string Tong1 = "0";
            DatalinqDataContext db = new DatalinqDataContext();
            List<Notification> dt1 = db.Notifications.Where(s => s.IDThanhVienNhanThongBao == int.Parse(id) && s.TrangThai == 0).ToList();
            if (dt1.Count > 0)
            {
                Tong1 = dt1.Count.ToString();
            }
            return Tong1;
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}