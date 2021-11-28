using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.control
{
    public partial class ControlThanhVien_QuanLy : System.Web.UI.UserControl
    {
        #region string
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        public string Moldul = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DatalinqDataContext db = new DatalinqDataContext();
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

            #region Request_e
            //try
            // {
            if (Request["e"] != null)
            {
                if (Request["e"].ToString() == "load")
                {
                    string request = Request["hp"] != null ? Request["hp"].ToString() : Request.Path;
                    string t = Request["hp"].ToString() + ".html";
                    if (!request.ToLower().Contains("index.aspx"))
                    {
                        Moldul = Commond.RequestMenu(Request["hp"]);
                        switch (Moldul)
                        {
                            case "99":// trang page
                                phcontrol.Controls.Add(LoadControl("~/cms/Display/Page/Detail.ascx"));
                                break;
                            //News
                            case "1":
                                phcontrol.Controls.Add(LoadControl("~/cms/Display/News/Category.ascx"));
                                break;
                            case "2":
                                phcontrol.Controls.Add(LoadControl("~/cms/Display/News/Detail.ascx"));
                                break;
                        }
                    }
                }

            }
            //}
            //catch (Exception)
            //{
            //    Response.Redirect("/page-404.html");
            //}
            #endregion
            #region Request_su
            switch (Request["su"])
            {
                case "Register":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Register.ascx"));
                    break;
                case "Dautu":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/MDauTuBatDongSan.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "ChuyenDiemViDiem":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/ChuyenDiemTrongVi.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "Chuyengia":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/List_HoaHongChuyeGia.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                      case "TinNhan":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/TinNhanCuaBan.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "Lichsutruthue":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/MLichSuChuyenDiem_TruThue.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "lichsuthanhtoanqrcode":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/MLichSuThanhToanQRCode.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "lichsuduyetvitamgiu":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/MLichSuDuyetViTamGiu.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "HoaHongQRcode":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/List_HoaHong_QRCode.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "LichSuCapDiem":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/LichSuCapDiem.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "LichSuRutTien":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/MLichSuRutTien.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "LichSuMuaDiem":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/MLichSuMuaDiem.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "LaiSuatagland":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/List_HoaHong_LaisuatAGLand.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;

                case "MuaDiem":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/MuaDiem.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "RutTien":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/RutTien.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "ViTienThanhVien":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/ViTienThanhVien.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "quanlygiadaily":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/GiaDaiLy.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "Thanhvien":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/DanhSachListViewThanhVien2.ascx"));
                        // phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/DanhSachListViewThanhVien.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "LichSuChuyenDiem":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/LichSuChuyenDiem.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "LinkGoiThieu":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/Link_GioiThieu.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;

                #region Chi tiết đơn hàng của nhà cũng cấp đã bán
                case "orderssold":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/DonBanHang_Detail.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "DonBanHang":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/DonBanHang.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                #endregion

                case "Detailorders":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/Detailorders.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "Lichsu":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/Lich_su_mua_hang.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "HoaHongGioiThieu":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/List_HoaHong_GioiThieu.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "HoaHongMua":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/List_HoaHong_Mua.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "HoaHongBan":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/List_HoaHong_Ban.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;


                case "HoaHong":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/List_HoaHong.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "ChuyenDiem":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/MChuyenDiem.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "quanlysanpham":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/ThanhVienDangBai.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "Infos":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Info.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "changepass":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        this.phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Changepassword.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    return;
                case "changinfo":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        this.phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Changinfo.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    return;
                case "resetpassword":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Resetpassword.ascx"));
                    break;
                case "Login":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Login.ascx"));
                    break;


            }
            #endregion

        }
    }
}