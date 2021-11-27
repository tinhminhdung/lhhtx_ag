using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.E_Commerce.cms.Admin
{
    public partial class main : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["lang"] != null)
            {
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["lang"] = this.lang;
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }

            #region Đăng nhập 1 tài khoản trên hệ thống
            if (MoreAll.MoreAll.GetCookies("UName") != "")
            {
                if ((MoreAll.MoreAll.GetCookies("UName") != "Admin."))
                {
                    try
                    {
                        // kiểm tra Session token nếu trùng với token trong db ở lúc đầu tạo ra thì ok
                        // Nếu Session token ko trùng với  token trong db thì chứng tỏ có ng đăng nhập vào và tài khoản đăng nhập lần 1 sẽ login ra để cho tài khoản login lần 2 dc vào
                        List<AdminUser> abc = db.AdminUsers.Where(p => p.Token.Contains(MoreAll.MoreAll.GetCookies("token")) && p.ID == int.Parse(MoreAll.MoreAll.GetCookies("AdminID"))).ToList();
                        if (abc.Count <= 0)
                        {
                            MoreAll.MoreAll.DelCookie("UName");
                            MoreAll.MoreAll.DelCookie("URole");
                            MoreAll.MoreAll.DelCookie("token");
                            Response.Redirect("/admin.aspx");
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            #endregion


            // try
            //{
            #region Case
            string u = Request.QueryString["u"];
            switch (u)
            {

                case "CauHinhBDS":
                    phcontrol.Controls.Add(LoadControl("Member/ChiHoaHong.ascx"));
                    break;
                case "LichSuLoiNhuanBDS":
                    phcontrol.Controls.Add(LoadControl("Member/LoiNhuanMuaBDS.ascx"));
                    break;
                case "ChiaHHBDS":
                    phcontrol.Controls.Add(LoadControl("Member/Settings.ascx"));
                    break;

                case "LichSuLevel":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuLevel.ascx"));
                    break;
                case "LSchuyendiem":
                    phcontrol.Controls.Add(LoadControl("Member/LSMChuyenDiem.ascx"));
                    break;
                case "checkIP":
                    phcontrol.Controls.Add(LoadControl("Member/CheckTimKiemLichSuDangNhap.ascx"));
                    break;
                case "DSMuaHangTheoThang":
                    phcontrol.Controls.Add(LoadControl("Member/DSMuaHangTheoThang.ascx"));
                    break;
                case "LichSuDangNhapAdmin":
                    phcontrol.Controls.Add(LoadControl("AdminUsers/MSLichSuDangNhapAdmin.ascx"));
                    break;
                case "LScapdiem":
                    phcontrol.Controls.Add(LoadControl("Member/MLSCCapDiem.ascx"));
                    break;
                case "LichSuDangNhap":
                    phcontrol.Controls.Add(LoadControl("Member/LichSuDangNhap.ascx"));
                    break;
                case "LoiNhuanChechLechGia":
                    phcontrol.Controls.Add(LoadControl("Member/LoiNhuanChechLechGia.ascx"));
                    break;
                case "lichsuserviceland":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuServiceAgLand.ascx"));
                    break;
                case "LaiSuatTheoAgLand":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuTheoLand.ascx"));
                    break;
                case "chuyengia":
                    phcontrol.Controls.Add(LoadControl("Member/MViHoaHongChuyenGia.ascx"));
                    break;

                case "LoiNhuanNCC":
                    phcontrol.Controls.Add(LoadControl("Member/MDanhSachNhaCC_LoiNhuan.ascx"));
                    break;
                case "MThue":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuChuyenDiemSangVi_Thue.ascx"));
                    break;
                case "ViTamMuaHang":
                    phcontrol.Controls.Add(LoadControl("Member/MViTamMuaHang.ascx"));
                    break;
                case "MThongKeThanhVien":
                    phcontrol.Controls.Add(LoadControl("ThongKe/MThongKeThanhVien.ascx"));
                    break;
                case "ThongKe":
                    phcontrol.Controls.Add(LoadControl("ThongKe/ThanhVienDangKy.ascx"));
                    break;
                case "TBNotification":
                    phcontrol.Controls.Add(LoadControl("Member/TBNotification.ascx"));
                    break;
                case "LoiNhuan":
                    phcontrol.Controls.Add(LoadControl("Member/LoiNhuanMuaBan.ascx"));
                    break;
                case "LoiNhuanQRCode":
                    phcontrol.Controls.Add(LoadControl("Member/LoiNhuanDangKyQRCode.ascx"));
                    break;
                case "LoiNhuanDangKy":
                    phcontrol.Controls.Add(LoadControl("Member/LoiNhuanDangKyThanhVien.ascx"));
                    break;
                case "LichSuThanhToanQRCode":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuQRCodeThanhToan.ascx"));
                    break;

                case "LichsuDuyetTamGiu":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuDuyetTamGiu.ascx"));
                    break;

                case "LichSuQRcode":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuQRCode.ascx"));
                    break;

                case "Thongke":
                    phcontrol.Controls.Add(LoadControl("Member/ThongKeFull.ascx"));
                    break;

                case "SettingHoaHong":
                    phcontrol.Controls.Add(LoadControl("Member/SettingHoaHong.ascx"));
                    break;
                case "ChitietDonHang":
                    phcontrol.Controls.Add(LoadControl("Products/DetailCart.ascx"));
                    break;
                case "DetailCartNhanh":
                    phcontrol.Controls.Add(LoadControl("Products/DetailCartNhanh.ascx"));
                    break;

                case "ServiceAGLANGD":
                    phcontrol.Controls.Add(LoadControl("Member/ServiceLaiSuatAGLAND.ascx"));
                    break;


                case "HoaHongAGLANGD":
                    phcontrol.Controls.Add(LoadControl("Member/HoaHongLaiSuatAGLAND.ascx"));
                    break;
                case "LichSuDG":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuGiaoDich.ascx"));
                    break;

                case "MLichSuRutTien":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuRutTien.ascx"));
                    break;
                case "MLichSuMuaDiem":
                    phcontrol.Controls.Add(LoadControl("Member/MLichSuMuaDiem.ascx"));
                    break;
                case "ChuyenDiem":
                    phcontrol.Controls.Add(LoadControl("Member/MChuyenDiem.ascx"));
                    break;
                case "CCapDiem":
                    phcontrol.Controls.Add(LoadControl("Member/CCapDiem.ascx"));
                    break;
                case "Level":
                    phcontrol.Controls.Add(LoadControl("Member/CLevel.ascx"));
                    break;
                case "HoaHong":
                    phcontrol.Controls.Add(LoadControl("Member/HoaHong.ascx"));
                    break;
                case "DaiLy":
                    phcontrol.Controls.Add(LoadControl("Member/CDaiLy.ascx"));
                    break;
                case "301":
                    phcontrol.Controls.Add(LoadControl("Redirect301/StatusCode.ascx"));
                    break;
                case "info":
                    phcontrol.Controls.Add(LoadControl("NewsFooter/Control.ascx"));
                    break;
                case "Dichvu":
                    phcontrol.Controls.Add(LoadControl("DDichvu/DDichvu.ascx"));
                    break;
                case "Gioithieu":
                    phcontrol.Controls.Add(LoadControl("GioiThieu/GioiThieu.ascx"));
                    break;
                case "Thanhvien":
                    phcontrol.Controls.Add(LoadControl("Member/Members.ascx"));
                    break;
                case "faq":
                    phcontrol.Controls.Add(LoadControl("Faq/Control.ascx"));
                    break;
                case "Sitemap":
                    phcontrol.Controls.Add(LoadControl("Sitemap/Control.ascx"));
                    break;
                case "Album":
                    phcontrol.Controls.Add(LoadControl("Album/Control.ascx"));
                    break;
                case "Marketing":
                    phcontrol.Controls.Add(LoadControl("Marketing/Control.ascx"));
                    break;
                case "Tienich":
                    phcontrol.Controls.Add(LoadControl("Tienich/Control.ascx"));
                    break;
                case "Video":
                    phcontrol.Controls.Add(LoadControl("Video/Control.ascx"));
                    break;
                case "Download":
                    phcontrol.Controls.Add(LoadControl("Download/Control.ascx"));
                    break;
                case "Contacts":
                    phcontrol.Controls.Add(LoadControl("Contacts/Control.ascx"));
                    break;
                case "Advertisings":
                    phcontrol.Controls.Add(LoadControl("Advertisings/Control.ascx"));
                    break;
                case "pro":
                    phcontrol.Controls.Add(LoadControl("products/Control.ascx"));
                    break;
                case "carts":
                    phcontrol.Controls.Add(LoadControl("products/Cart.ascx"));
                    break;
                case "cartsNhanh":
                    phcontrol.Controls.Add(LoadControl("products/CartNhanh.ascx"));
                    break;

                case "news":
                    phcontrol.Controls.Add(LoadControl("CNews/Control.ascx"));
                    break;
                case "set":
                    phcontrol.Controls.Add(LoadControl("settings/Control.ascx"));
                    break;
                case "WebAnalytics":
                    phcontrol.Controls.Add(LoadControl("WebAnalytics/Control.ascx"));
                    break;
                case "Page":
                    phcontrol.Controls.Add(LoadControl("Page/Menu_Page.ascx"));
                    break;
                case "AdminUser":
                    phcontrol.Controls.Add(LoadControl("AdminUsers/MAdminUser.ascx"));
                    break;
                case "":
                default:
                    phcontrol.Controls.Add(LoadControl("settings/Control.ascx"));
                    break;
            }
            #endregion

            if (!base.IsPostBack)
            {
                #region Đăng nhập 1 tài khoản trên hệ thống
                try
                {
                    if (MoreAll.MoreAll.GetCookies("UName") != "")
                    {
                        if (MoreAll.MoreAll.GetCookies("UName") != "Admin.")
                        {
                            AdminUser abc = db.AdminUsers.SingleOrDefault(p => p.VUSER_NAME == MoreAll.MoreAll.GetCookies("UName"));
                            if (abc == null)
                            {
                                MoreAll.MoreAll.SetCookie("UName", "", -1);
                                MoreAll.MoreAll.SetCookie("URole", "", -1);
                                Response.Redirect(Request.Url.ToString());
                                Response.Redirect("/admin.aspx");
                            }
                        }
                    }
                }
                catch (Exception)
                { }
                #endregion

                #region Role
                if (MoreAll.MoreAll.GetCookies("URole") != null)
                {
                    string[] strArray = MoreAll.MoreAll.GetCookies("URole").ToString().Trim().Split(new char[] { '|' });
                    Reset_Checkbox();
                    if (strArray.Length > 0)
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (strArray[i].ToString().Equals("1"))
                            {
                                set.Style.Add("display", "block");
                                Quantri.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("2"))
                            {
                                News.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("3"))
                            {
                                Products.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("4"))
                            {
                                AdminUser.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("5"))
                            {
                                Contacts.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("6"))
                            {
                                Advertisings.Style.Add("display", "block");
                                Danhmuc.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("7"))
                            {
                                lnkthanhvien.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("8"))
                            {
                                Level.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("9"))
                            {
                                SettingHoaHong.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("10"))
                            {
                                DaiLy.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("11"))
                            {
                                User.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("12"))
                            {
                                HoaHong.Style.Add("display", "block");
                                LoiNhuan.Style.Add("display", "block");
                                LoiNhuanChechLechGia.Style.Add("display", "block");


                                LoiNhuanDangKy.Style.Add("display", "block");
                                LoiNhuanQRCode.Style.Add("display", "block");
                                LoiNhuanNCC.Style.Add("display", "block");
                                LaiSuatTheoAgLand.Style.Add("display", "block");

                            }
                            if (strArray[i].ToString().Equals("13"))
                            {
                                HoaHongAGLANGD.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("14"))
                            {
                            }
                            if (strArray[i].ToString().Equals("15"))
                            {
                                ServiceAGLANGD.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("16"))
                            {
                                Page.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("17"))
                            {
                                carts.Style.Add("display", "block");
                                giohang.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("18"))
                            {
                                //Thongke.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("19"))
                            {
                                MLichSuMuaDiem.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("20"))
                            {
                                LichSuDG.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("21"))
                            {
                                MLichSuRutTien.Style.Add("display", "block");
                            }
                            if (strArray[i].ToString().Equals("22"))
                            {
                                LichSuQRcode.Style.Add("display", "block");
                                LichSuThanhToanQRCode.Style.Add("display", "block");
                                LichsuDuyetTamGiu.Style.Add("display", "block");
                            }
                        }
                    }
                }
                #endregion
            }
            // }
            //catch (Exception) { }
        }

        protected void Reset_Checkbox()
        {
            #region ResetCheckbox
            //try
            {
                LichsuDuyetTamGiu.Style.Add("display", "none");
                LichSuQRcode.Style.Add("display", "none");
                LichSuThanhToanQRCode.Style.Add("display", "none");
                LoiNhuanQRCode.Style.Add("display", "none");
                LoiNhuanNCC.Style.Add("display", "none");
                LaiSuatTheoAgLand.Style.Add("display", "none");

                lnkthanhvien.Style.Add("display", "none");
                Level.Style.Add("display", "none");
                SettingHoaHong.Style.Add("display", "none");
                DaiLy.Style.Add("display", "none");
                HoaHong.Style.Add("display", "none");
                LoiNhuan.Style.Add("display", "none");
                LoiNhuanChechLechGia.Style.Add("display", "none");
                LoiNhuanDangKy.Style.Add("display", "none");
                HoaHongAGLANGD.Style.Add("display", "none");
                MLichSuRutTien.Style.Add("display", "none");
                MLichSuMuaDiem.Style.Add("display", "none");
                LichSuDG.Style.Add("display", "none");
                ServiceAGLANGD.Style.Add("display", "none");
                // Thongke.Style.Add("display", "none");
                #region pro
                if ((Request["u"] == "pro"))
                {
                    Products.Style.Add("background", "#ff9c00");
                    Products.Style.Add("display", "none");
                }
                Products.Style.Add("display", "none");
                #endregion

                #region carts
                if ((Request["u"] == "carts"))
                {
                    carts.Style.Add("background", "#ff9c00");
                    carts.Style.Add("display", "none");
                    giohang.Style.Add("display", "none");
                }
                carts.Style.Add("display", "none");
                giohang.Style.Add("display", "none");
                #endregion

                #region Thanhvien
                if ((Request["u"] == "Thanhvien"))
                {
                    User.Style.Add("background", "#ff9c00");
                }
                User.Style.Add("display", "none");
                #endregion

                #region AdminUser
                if ((Request["su"] == "AdminUser"))
                {
                    User.Style.Add("background", "#ff9c00");
                    AdminUser.Style.Add("display", "none");
                }
                AdminUser.Style.Add("display", "none");
                #endregion

                #region News
                if ((Request["u"] == "news"))
                {
                    News.Style.Add("background", "#ff9c00");
                    News.Style.Add("display", "none");
                }
                News.Style.Add("display", "none");
                #endregion

                #region set
                if ((Request["u"] == "set"))
                {
                    Quantri.Style.Add("background", "#ff9c00");
                    set.Style.Add("display", "none");
                }
                set.Style.Add("display", "none");
                Quantri.Style.Add("display", "none");
                #endregion

                #region Advertisings
                if ((Request["u"] == "Advertisings"))
                {
                    Danhmuc.Style.Add("background", "#ff9c00");
                    Advertisings.Style.Add("display", "none");
                }
                Advertisings.Style.Add("display", "none");
                Danhmuc.Style.Add("display", "none");
                #endregion

                #region Contacts
                if ((Request["u"] == "Contacts"))
                {
                    Quantri.Style.Add("background", "#ff9c00");
                    Contacts.Style.Add("display", "none");
                }
                Contacts.Style.Add("display", "none");
                #endregion

                //#region Video
                //if ((Request["u"] == "Video"))
                //{
                //    Video.Style.Add("background", "#ff9c00");
                //    Video.Style.Add("display", "none");
                //}
                //Video.Style.Add("display", "none");
                //#endregion

                //#region Tienich
                //if ((Request["u"] == "Tienich"))
                //{
                //    Danhmuc.Style.Add("background", "#ff9c00");
                //    Tienich.Style.Add("display", "none");
                //}
                //Tienich.Style.Add("display", "none");
                //#endregion

                //#region Marketing
                //if ((Request["u"] == "Marketing"))
                //{
                //    Quantri.Style.Add("background", "#ff9c00");
                //    Marketing.Style.Add("display", "none");
                //}
                //Marketing.Style.Add("display", "none");
                //#endregion

                //#region Album
                //if ((Request["u"] == "Album"))
                //{
                //    Album.Style.Add("background", "#ff9c00");
                //    Album.Style.Add("display", "none");
                //}
                //Album.Style.Add("display", "none");
                //#endregion

                //#region faq
                //if ((Request["u"] == "faq"))
                //{
                //    // faq.Style.Add("background", "#ff9c00");
                //    faq.Style.Add("display", "none");
                //}
                //faq.Style.Add("display", "none");
                //#endregion

                #region Page
                if ((Request["u"] == "Page"))
                {
                    Danhmuc.Style.Add("background", "#ff9c00");
                    Page.Style.Add("display", "none");
                }
                Page.Style.Add("display", "none");
                #endregion

            }
            // catch (Exception) { }
            #endregion
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void lnk_exit_Click(object sender, EventArgs e)
        {
            #region Exit
            MoreAll.MoreAll.SetCookie("UName", "", -1);
            MoreAll.MoreAll.SetCookie("URole", "", -1);
            MoreAll.MoreAll.SetCookie("UName", "", -1);
            Response.Redirect(Request.Url.ToString());
            #endregion
        }

        private void Refresh()
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void lnknew_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=news");
        }

        protected void lnkpro_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=pro");
        }

        //protected void lnksettings_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("admin.aspx?u=set");
        //}

        protected void lnkAdvertisings_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Advertisings");
        }

        protected void lnklienhe_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Contacts");
        }

        protected void lnkDownloadFile_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Download");
        }

        protected void lnkVideo_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Video");
        }

        protected void lnkTienich_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Tienich");
        }

        protected void lnkMarketing_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Marketing");
        }

        protected void lnkAlbum_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Album");
        }

        protected void lnkWebAnalytics_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=WebAnalytics");
        }

        protected void lnkhompage_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx");
        }

        protected void lnkmenuchinh_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Page");
        }

        protected void lnkSitemap_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Sitemap");
        }

        private void InitializeComponent()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        protected void Lnkfaq_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=faq");
        }
        protected void lnkGioithieu_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Gioithieu");
        }
        protected void ltthanhvien_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Thanhvien");
        }
        protected void lnkDichvu_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=Dichvu");
        }
        protected void lnkthongtin_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?u=info");
        }
        protected string returnCSS(string ctrol)
        {
            string su = "";
            if (Request["su"] != null && !Request["su"].Equals(""))
            {
                su = Request["su"];
            }
            if ((su != "") && su.Equals(ctrol))
            {
                return "";
            }
            return "";
        }
        protected string TContac()
        {
            string str = "0";
            List<Entity.Contacts> tong = SContacts.Name_Text("SELECT * FROM Contacts where istatus=0 and lang='" + lang + "'");
            if (tong.Count > 0)
            {
                str = tong.Count.ToString();
            }
            return str;
        }
        protected string TCarts()
        {
            string str = "0";
            List<Entity.CartDetail> tong = SCartDetail.Name_Text("SELECT * FROM CartDetail where TrangThaiKhieuKien=1");
            if (tong.Count > 0)
            {
                str = tong.Count.ToString();
            }
            return str;
        }
    }
}