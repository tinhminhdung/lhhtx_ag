using Entity;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWindowService;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class ChuyenDiemTrongVi : System.Web.UI.UserControl
    {
        public int i = 1;
        protected bool Dung = false;
        private string IDSanPham = "";
        private string IDGioHang = "0";
        private string URL = "";
        private string language = Captionlanguage.Language;
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
            URL = Request.RawUrl.ToString();
            if (!base.IsPostBack)
            {
                ShowThanhVien();
            }
        }
        private void ShowThanhVien()
        {
            user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
            if (table != null)
            {
                hdid.Value = table.ViMuaHangAFF;
                ltvimuahang.Text = table.ViMuaHangAFF;
                ltvithuongmai.Text = table.TongTienCoinDuocCap;
            }
        }

        protected void btchuyensangvithuongmai_Click(object sender, EventArgs e)
        {
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                if (table != null)
                {
                    ltmsg.Text = "";
                    try
                    {
                        double ViMuaHangAFF = Convert.ToDouble(table.ViMuaHangAFF);
                        double ViThuongMai = Convert.ToDouble(table.TongTienCoinDuocCap);
                        double TongTienChuyen = Convert.ToDouble(txtsodiem.Text.Trim().Replace(",", "."));
                        if (ViThuongMai >= TongTienChuyen)
                        {
                            #region Lưu số tiền đã bị trừ ở ví thương mại
                            double Conglai = 0;
                            Conglai = ((ViThuongMai) - (TongTienChuyen));
                            Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + " where iuser_id=" + table.iuser_id.ToString() + "");
                            #endregion


                            #region Cộng số tiền ở ví mua hàng
                            double TongCong = 0;
                            TongCong = ((ViMuaHangAFF) + (TongTienChuyen));
                            Susers.Name_Text("update users set ViMuaHangAFF=" + TongCong.ToString() + " where iuser_id=" + table.iuser_id.ToString() + "");
                            #endregion


                            LichSuGiaoDich("14", "Chuyển điểm từ ví thương mại sang ví mua hàng của chính mình.", table.iuser_id.ToString(), table.iuser_id.ToString(), "0", TongTienChuyen.ToString());

                            #region Thêm vào Bảng ChuyenDiemThanhVien
                            ChuyenDiemThanhVien obks = new ChuyenDiemThanhVien();
                            obks.IDNguoiCap = int.Parse(table.iuser_id.ToString());
                            obks.IDNguoiNhan = int.Parse(table.iuser_id.ToString());
                            obks.SoCoin = TongTienChuyen.ToString();
                            obks.NgayCap = DateTime.Now;
                            obks.MoTa = "Chuyển điểm từ ví thương mại sang ví mua hàng của chính mình";
                            obks.ViChuyen = int.Parse("1");
                            obks.ViNhan = int.Parse("3");
                            obks.TrangThai = int.Parse("2");// trang thái 1 là chuyển trong hệ thống, 2 chuyển cho chính mình
                            db.ChuyenDiemThanhViens.InsertOnSubmit(obks);
                            db.SubmitChanges();
                            #endregion

                            #region Thêm vào Bảng CapDiemThanhVien
                            CapDiemThanhVien obkp = new CapDiemThanhVien();
                            obkp.IDNguoiCap = int.Parse(table.iuser_id.ToString());
                            obkp.IDNguoiNhanDiemCoin = int.Parse(table.iuser_id.ToString());
                            obkp.SoDiemCoin = TongTienChuyen.ToString();
                            obkp.NgayCap = DateTime.Now;
                            obkp.MoTa = "Chuyển điểm từ ví thương mại sang ví mua hàng của chính mình";
                            obkp.NguoiTao = MoreAll.MoreAll.GetCookies("Members");
                            obkp.TrangThai = 1;
                            obkp.KieuVi = int.Parse("3");
                            db.CapDiemThanhViens.InsertOnSubmit(obkp);
                            db.SubmitChanges();
                            #endregion
                            ltthongbao.Text = "<script type=\"text/javascript\">alert('Chuyển điểm từ ví thương mại sang ví mua hàng thành công.');window.location.href='" + URL + "'; </script>";
                        }
                        else
                        {
                            ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\">Số điểm không đủ để chuyển sang ví thương mại.</div>";
                        }
                    }
                    catch (Exception)
                    {
                        ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\">Vui lòng kiểm tra lại số điểm cần chuyển.</div>";
                    }
                }
            }
        }

        void LichSuGiaoDich(string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
        {
            #region LichSuGiaoDich
            LichSuGiaoDich obl = new LichSuGiaoDich();
            obl.IDProducts = int.Parse("0");
            obl.IDType = int.Parse(IDType);
            obl.Type = Type;
            obl.IDThanhVien = int.Parse(IDThanhVien);
            obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
            obl.PhamTramHoaHong = PhamTramHoaHong;
            obl.SoCoin = SoCoin.ToString();
            obl.NgayTao = DateTime.Now;
            db.LichSuGiaoDiches.InsertOnSubmit(obl);
            db.SubmitChanges();
            #endregion
        }
    }
}