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
    public partial class ThanhToanQRCode : System.Web.UI.UserControl
    {
        string key = "";
        DatalinqDataContext db = new DatalinqDataContext();
        protected bool Dung = false;
        protected double PhanTramChietKhau = 0;
        protected double PhanTramHHChoNguoiMua = 0;
        protected double PhanTramHHHeThong = 0;

        private string IDSanPham = "QRCode";
        private string IDGioHang = "888";
        private string IDMaDonTao = "0";
        protected void Page_Load(object sender, EventArgs e)
        {

            Showload();
            if (Request["key"] != null && !Request["key"].Equals(""))
            {
                key = Request["key"].ToString();
            }

            List<Entity.users> dt = Susers.Name_Text("select * from users where (vuserun ='" + key + "')");
            if (dt.Count >= 1)
            {
                if (MoreAll.MoreAll.GetCookies("Members").ToString() == key)
                {
                    lthovaten.Text = "Vui lòng kiểm tra lại. Ko thể chuyển cho chính mình được.";
                    ltemail.Text = "Vui lòng kiểm tra lại. Ko thể chuyển cho chính mình được.";
                    ltsodienthoai.Text = "Vui lòng kiểm tra lại. Ko thể chuyển cho chính mình được.";
                    this.ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\"><img src='/Resources/images/iconthongbao.jpg' /> Không thể chuyển cho chính mình được. vui lòng kiểm tra lại.</div>"; return;
                }
                else
                {
                    hdIDThanhVien.Value = dt[0].iuser_id.ToString();
                    lthovaten.Text = dt[0].vfname.ToString();
                    ltemail.Text = dt[0].vemail.ToString();
                    ltsodienthoai.Text = dt[0].vphone.ToString();

                    PhanTramChietKhau = Convert.ToDouble(dt[0].QRCodeChietKhauHH);
                    PhanTramHHChoNguoiMua = Convert.ToDouble(dt[0].QRCodeHHNguoiMua);
                    PhanTramHHHeThong = Convert.ToDouble(dt[0].QRCodeHHHeThong);
                }
            }
            else
            {
                hdIDThanhVien.Value = "0";
                lthovaten.Text = "Không tìm thấy";
                ltemail.Text = "Không tìm thấy";
                ltsodienthoai.Text = "Không tìm thấy";
            }
        }

        void Showload()
        {
            try
            {
                if (MoreAll.MoreAll.GetCookies("Members") != "")
                {
                    user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                    if (table != null)
                    {
                        Panel1.Visible = true;
                        this.lttenthanhvien.Text = table.vfname.ToString();
                        this.sodiemconlai.Text = table.TongTienCoinDuocCap.ToString();
                    }
                }
            }
            catch (Exception)
            { }
        }
        protected void btthanhtoan_Click(object sender, EventArgs e)
        {
            if (txttendangnhap.Text.Trim() == key)
            {
                this.ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\"><img src='/Resources/images/iconthongbao.jpg' /> Không thể chuyển cho chính mình được. vui lòng kiểm tra lại.</div>"; return;
            }
            else if (hdIDThanhVien.Value != "0")
            {
                if (this.txttendangnhap.Text.Trim().Length < 1)
                {
                    this.ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\"><img src='/Resources/images/iconthongbao.jpg' /> Vui lòng điền tên đăng nhập thanh toán</div>"; return;
                }
                else if (this.txtmatkhau.Text.Trim().Length < 1)
                {
                    this.ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\"><img src='/Resources/images/iconthongbao.jpg' /> Vui lòng điền mật khẩu thanh toán</div>"; return;
                }
                else if (this.txtsotiencanthanhtoan.Text.Trim().Length < 1)
                {
                    this.ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\"><img src='/Resources/images/iconthongbao.jpg' /> Vui lòng điền số tiền cần thanh toán</div>"; return;
                }
                else
                {
                    var item = db.S_Member_GetPAss(txttendangnhap.Text.Trim(), txtmatkhau.Text).ToList();
                    if (item[0].Tong.ToString() == "0")
                    {
                        this.ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\"><img src='/Resources/images/iconthongbao.jpg' /> Mật khẩu hiện tại không đúng</div>"; return;
                    }
                    else
                    {
                        if (PhanTramChietKhau.ToString() == "0")
                        {
                            ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\">Không thể thanh toán, vì người nhận chưa nhập % chiết khấu</div> "; return;
                        }

                        try
                        {
                            SinhHoaHong(txttendangnhap.Text.Trim(), txtmatkhau.Text.Trim());

                            string IDThanhVienNhans = "0";
                            user items = db.users.SingleOrDefault(p => p.vuserun == txttendangnhap.Text.Trim());
                            if (items != null)
                            {
                                IDThanhVienNhans = items.iuser_id.ToString();
                            }
                            #region LichSuQRCode
                            LichSuThanhToanQRCode obj = new LichSuThanhToanQRCode();
                            obj.IDThanhVien = int.Parse(IDThanhVienNhans);
                            obj.IDThanhVienNhan = int.Parse(hdIDThanhVien.Value);
                            obj.SoDiemThanhToan = txtsotiencanthanhtoan.Text;
                            obj.NgayDuyet = DateTime.Now;
                            obj.NoiDung = txtnoidungthanhtoan.Text;
                            db.LichSuThanhToanQRCodes.InsertOnSubmit(obj);
                            db.SubmitChanges();
                            #endregion

                            txttendangnhap.Text = "";
                            txtmatkhau.Text = "";
                            txtsotiencanthanhtoan.Text = "";
                            ltmess.Text = "";
                            Showload();
                        }
                        catch (Exception)
                        {
                            txttendangnhap.Text = "";
                            txtmatkhau.Text = "";
                            txtsotiencanthanhtoan.Text = "";
                            this.ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\"><img src='/Resources/images/iconthongbao.jpg' /> Vui lòng kiểm tra lại toàn bộ đầu vào.</div>"; return;
                        }

                    }
                }
            }
            else
            {
                ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\">Không tìm thấy người nhận điểm.</div> "; return;
            }
        }

        void SinhHoaHong(string IDThanhVienThanhToan, string matkhau)
        {
            IDMaDonTao = MoreAll.MoreAll.FormatDate_IDQR(DateTime.Now);
            string IDThanhVien = "0";
            #region Tính tiền trừ vào bảng User TongTienCoinDuocCap
            string str = "";
            double SoTienPhaiThanhToanCoin = Convert.ToDouble(txtsotiencanthanhtoan.Text);
            if (SoTienPhaiThanhToanCoin < 0)
            {
                ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\">Số tiền quá bé để thanh toán.</div> "; return;
            }

            user iiit = db.users.SingleOrDefault(p => p.vuserun == IDThanhVienThanhToan.ToString() && p.vuserpwd == matkhau);
            if (iiit != null)
            {


                double ViHienTaiCoin = Convert.ToDouble(iiit.TongTienCoinDuocCap);
                if (ViHienTaiCoin >= SoTienPhaiThanhToanCoin)
                {
                    double ConglaiCoin = ((ViHienTaiCoin) - (SoTienPhaiThanhToanCoin));
                    iiit.TongTienCoinDuocCap = ConglaiCoin.ToString();
                    db.SubmitChanges();

                    double DauVao = Convert.ToDouble(SoTienPhaiThanhToanCoin);
                    double ChieuKhau = Convert.ToDouble(PhanTramChietKhau);
                    double TongCong = (DauVao * ChieuKhau) / 100;
                    double TongTienNguoiNhan = (DauVao - TongCong);
                    // Response.Write(Tong2.ToString() + "<br>");

                    #region TH1:Thanh toán cho người mua
                    ThemHoaHong("0", "214", "Thanh toán cho người mua (QRCode)", iiit.iuser_id.ToString(), hdIDThanhVien.Value, "0", TongTienNguoiNhan.ToString(), IDMaDonTao);
                    #endregion

                    #region Tính Hoa hồng QRCode

                    // string IDNhaCungCapBanHang = dtcart[0].IDNhaCungCap.ToString();

                    string Plevel = "99999999999";
                    string TongLevel = "0";

                    //TH1 : Nếu thành viên và leader mua hàng mua hàng
                    user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdIDThanhVien.Value));// ID thành viên nhận điểm
                    if (table != null)
                    {
                        //Tổng tiền sau khi chiết khấu
                        double SoTienChietKhau = (SoTienPhaiThanhToanCoin * PhanTramChietKhau) / 100;
                        //  Response.Write(SoTienChietKhau + "<br>");

                        // % hh chia cho người mua
                        double HoaHongChonNguoiMua = (SoTienChietKhau * PhanTramHHChoNguoiMua) / 100;
                        // Response.Write(HoaHongChonNguoiMua + "<br>");

                        // % hh chia cho hệ thống
                        double HoaHongChonNHHHeThong = (SoTienChietKhau * PhanTramHHHeThong) / 100;
                        // Response.Write(HoaHongChonNHHHeThong + "<br>");

                        if (SoTienChietKhau > 0)// Kiểm tra nếu điểm thưởng mà nhỏ hơn 0 thì sẽ ko phát sinh Hoa hồng QRCode nào nhé.
                        {
                            #region Phát sinh Hoa hồng QRCode giới thiệu Và thưởng thêm theo level
                            if (table != null)
                            {
                                IDThanhVien = iiit.iuser_id.ToString();// ID thành viên thanh toán (Người mua)
                                #region TH1: Nếu thành viên mua hàng mua hàng 30% tùy theo level
                                if (PhanTramHHChoNguoiMua != 0)
                                {
                                    ThemHoaHong("0", "213", "Hoa hồng QRCode cho người mua QRCode " + PhanTramHHChoNguoiMua + "%", iiit.iuser_id.ToString(), iiit.iuser_id.ToString(), PhanTramHHChoNguoiMua.ToString(), HoaHongChonNguoiMua.ToString(), IDMaDonTao);
                                }
                                #endregion

                                if (HoaHongChonNHHHeThong != 0)
                                {

                                    #region Hoa Hồng Thưởng Quản Lý
                                    try
                                    {
                                        double txtthuongquanly = Convert.ToDouble(MoreAll.Other.Giatri("txtthuongquanly"));
                                        if (!MoreAll.Other.Giatri("txtthuongquanly").Equals("0"))
                                        {
                                            double TongLeader = (HoaHongChonNHHHeThong * txtthuongquanly) / 100;
                                            ThemHoaHong("0", "405", "Hoa Hồng QRCode (Thưởng Quản Lý)", table.iuser_id.ToString(), SetVi.SetViThuongQuanLy(), txtthuongquanly.ToString(), TongLeader.ToString(), IDMaDonTao);
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                    #endregion


                                    //  string CapoLevelHoaHongs = SetLevel(table.LevelThanhVien.ToString());
                                    string CapoLevelHoaHongs = MoreAll.Other.Giatri("txtHoaHongGioiThieuTrucTiepmuahangVaF1");
                                    if (table.GioiThieu.ToString() != "0")
                                    {
                                        //double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs.ToString());
                                        double HoaHongs = Convert.ToDouble(MoreAll.Other.Giatri("txtHoaHongGioiThieuTrucTiepmuahangVaF1"));
                                        double Tong = (HoaHongChonNHHHeThong * HoaHongs) / 100;
                                        #region Hoa hồng QRCode Gián tiếp F1
                                        double HoaHongF1 = Tong;
                                        if (table.GioiThieu.ToString() != "0")
                                        {
                                            ThemHoaHong("0", "200", "Hoa hồng QRCode giới thiệu F1 ", IDThanhVien.Trim().ToLower(), table.GioiThieu.ToString(), CapoLevelHoaHongs.ToString(), HoaHongF1.ToString(), IDMaDonTao);
                                            #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa
                                            // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa
                                            // Nếu người mua hàng có level = 4 tức =45% thì dừng ko cho F1 được hưởng nữa
                                            try
                                            {
                                                if (table.LevelThanhVien.ToString() == "4")
                                                {
                                                    Dung = false;
                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                }
                                                else
                                                {
                                                    Dung = true;
                                                    Plevel = Plevel + "," + table.LevelThanhVien.ToString();
                                                    ThemHoaHong_ThuongLevel("0", "F1", "205", IDThanhVien, table.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(table.GioiThieu.ToString()), TimLevelB(IDThanhVien));
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                            #endregion
                                        }
                                        #region Hoa hồng QRCode Gián tiếp F2
                                        user tableTVTF2 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(table.GioiThieu.ToString()));
                                        if (tableTVTF2 != null)
                                        {
                                            double HoaHongGioiThieuF2 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF2"));
                                            double HoaHongF2 = (HoaHongF1 * HoaHongGioiThieuF2) / 100;
                                            if (tableTVTF2.GioiThieu.ToString() != "0")
                                            {
                                                ThemHoaHong("0", "201", "Hoa hồng QRCode giới thiệu F2 ", IDThanhVien.Trim().ToLower(), tableTVTF2.GioiThieu.ToString(), HoaHongGioiThieuF2.ToString(), HoaHongF2.ToString(), IDMaDonTao);
                                                #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa
                                                // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa
                                                try
                                                {
                                                    if (ShowFQRcode.ShowF2(tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                    {
                                                        Dung = false;
                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                    }
                                                    else
                                                    {
                                                        Dung = true;
                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF2(tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                    }
                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                    if (Plevel.ToString() == "99999999999")
                                                    {

                                                    }
                                                    else
                                                    {
                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                    }
                                                    if (Dung == true)
                                                    {
                                                        if (TongLevel != "8")// 8 là ko tìm thấy giá trị nào cao hơn 0 ở hàm MinAndMax
                                                        {
                                                            if (TongLevel != "45")// 45 là ko hưởng Hoa hồng QRCode nữa
                                                            {
                                                                ThemHoaHong_ThuongLevel("0", "F2", "206", IDThanhVien, tableTVTF2.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), TongLevel);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ThemHoaHong_ThuongLevel("0", "F2", "206", IDThanhVien, tableTVTF2.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), "0");
                                                        }
                                                    }
                                                }
                                                catch (Exception)
                                                { }
                                                #endregion
                                            }

                                            #region Hoa hồng QRCode Gián tiếp F3
                                            user tableTVTF3 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF2.GioiThieu.ToString()));
                                            if (tableTVTF3 != null)
                                            {
                                                double HoaHongGioiThieuF3 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF3"));
                                                double HoaHongF3 = (HoaHongF2 * HoaHongGioiThieuF3) / 100;
                                                if (tableTVTF3.GioiThieu.ToString() != "0")
                                                {
                                                    ThemHoaHong("0", "202", "Hoa hồng QRCode giới thiệu F3 ", IDThanhVien.Trim().ToLower(), tableTVTF3.GioiThieu.ToString(), HoaHongGioiThieuF3.ToString(), HoaHongF3.ToString(), IDMaDonTao);
                                                    #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa
                                                    // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa
                                                    try
                                                    {
                                                        if (ShowFQRcode.ShowF3(tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                        {
                                                            Dung = false;
                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                        }
                                                        else
                                                        {
                                                            Dung = true;
                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF3(tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                        }



                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                        if (Plevel.ToString() == "99999999999")
                                                        {

                                                        }
                                                        else
                                                        {
                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                        }
                                                        if (Dung == true)
                                                        {
                                                            if (TongLevel != "8")
                                                            {
                                                                if (TongLevel != "45")
                                                                {
                                                                    ThemHoaHong_ThuongLevel("0", "F3", "207", IDThanhVien, tableTVTF3.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), TongLevel);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ThemHoaHong_ThuongLevel("0", "F3", "207", IDThanhVien, tableTVTF3.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), "0");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    { }
                                                    #endregion

                                                }
                                                #region Hoa hồng QRCode Gián tiếp F4
                                                user tableTVTF4 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF3.GioiThieu.ToString()));
                                                if (tableTVTF4 != null)
                                                {
                                                    double HoaHongGioiThieuF4 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF4"));
                                                    double HoaHongF4 = (HoaHongF3 * HoaHongGioiThieuF4) / 100;
                                                    if (tableTVTF4.GioiThieu.ToString() != "0")
                                                    {
                                                        ThemHoaHong("0", "203", "Hoa hồng QRCode giới thiệu F4 ", IDThanhVien.Trim().ToLower(), tableTVTF4.GioiThieu.ToString(), HoaHongGioiThieuF4.ToString(), HoaHongF4.ToString(), IDMaDonTao);

                                                        #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa
                                                        // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa

                                                        try
                                                        {
                                                            if (ShowFQRcode.ShowF4(tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                            {
                                                                Dung = false;
                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                            }
                                                            else
                                                            {
                                                                Dung = true;
                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF4(tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                            }
                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                            if (Plevel.ToString() == "99999999999")
                                                            {

                                                            }
                                                            else
                                                            {
                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                            }
                                                            if (Dung == true)
                                                            {
                                                                if (TongLevel != "8")
                                                                {
                                                                    if (TongLevel != "45")
                                                                    {
                                                                        ThemHoaHong_ThuongLevel("0", "F4", "208", IDThanhVien, tableTVTF4.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), TongLevel);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ThemHoaHong_ThuongLevel("0", "F4", "208", IDThanhVien, tableTVTF4.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), "0");
                                                                }
                                                            }
                                                        }
                                                        catch (Exception)
                                                        { }
                                                        #endregion
                                                    }

                                                    #region Hoa hồng QRCode Gián tiếp F5
                                                    user tableTVTF5 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF4.GioiThieu.ToString()));
                                                    if (tableTVTF5 != null)
                                                    {
                                                        double HoaHongGioiThieuF5 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF5"));
                                                        double HoaHongF5 = (HoaHongF4 * HoaHongGioiThieuF5) / 100;
                                                        if (tableTVTF5.GioiThieu.ToString() != "0")
                                                        {
                                                            ThemHoaHong("0", "204", "Hoa hồng QRCode giới thiệu F5", IDThanhVien.Trim().ToLower(), tableTVTF5.GioiThieu.ToString(), HoaHongGioiThieuF5.ToString(), HoaHongF5.ToString(), IDMaDonTao);

                                                            #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa
                                                            // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % Hoa hồng QRCode thêm theo level nữa
                                                            try
                                                            {
                                                                if (ShowFQRcode.ShowF5(tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                {
                                                                    Dung = false;
                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                }
                                                                else
                                                                {
                                                                    Dung = true;
                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF5(tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                }

                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                if (Plevel.ToString() == "99999999999")
                                                                {

                                                                }
                                                                else
                                                                {
                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                }
                                                                if (Dung == true)
                                                                {
                                                                    if (TongLevel != "8")
                                                                    {
                                                                        if (TongLevel != "45")
                                                                        {
                                                                            ThemHoaHong_ThuongLevel("0", "F5", "209", IDThanhVien, tableTVTF5.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), TongLevel);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ThemHoaHong_ThuongLevel("0", "F5", "209", IDThanhVien, tableTVTF5.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), "0");
                                                                    }
                                                                }
                                                            }
                                                            catch (Exception)
                                                            { }
                                                            #endregion

                                                            #region Hoa Hồng Gián tiếp F6
                                                            user tableTVTF6 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF5.GioiThieu.ToString()));
                                                            if (tableTVTF6 != null)
                                                            {
                                                                if (tableTVTF6.GioiThieu.ToString() != "0")
                                                                {
                                                                    try
                                                                    {
                                                                        if (ShowFQRcode.ShowF6(tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                        {
                                                                            Dung = false;
                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                        }
                                                                        else
                                                                        {
                                                                            Dung = true;
                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF6(tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                        }
                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                        if (Plevel.ToString() == "99999999999")
                                                                        {

                                                                        }
                                                                        else
                                                                        {
                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                        }
                                                                        if (Dung == true)
                                                                        {
                                                                            if (TongLevel != "8")
                                                                            {
                                                                                if (TongLevel != "45")
                                                                                {
                                                                                    ThemHoaHong_ThuongLevel("0", "F6", "55", IDThanhVien, tableTVTF6.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), TongLevel);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                ThemHoaHong_ThuongLevel("0", "F6", "55", IDThanhVien, tableTVTF6.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), "0");
                                                                            }
                                                                            #region Dừng nếu gặp lelvel5
                                                                            string leveeeel = TimLevelB(tableTVTF6.GioiThieu.ToString());
                                                                            if (leveeeel == "5")
                                                                            {
                                                                                Plevel = "45";
                                                                            }
                                                                            #endregion
                                                                        }
                                                                    }
                                                                    catch (Exception)
                                                                    { }
                                                                }
                                                                #region Hoa Hồng Gián tiếp F7
                                                                user tableTVTF7 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF6.GioiThieu.ToString()));
                                                                if (tableTVTF7 != null)
                                                                {
                                                                    if (tableTVTF7.GioiThieu.ToString() != "0")
                                                                    {
                                                                        try
                                                                        {
                                                                            if (ShowFQRcode.ShowF7(tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                            {
                                                                                Dung = false;
                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                            }
                                                                            else
                                                                            {
                                                                                Dung = true;
                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF7(tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                            }
                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                            if (Plevel.ToString() == "99999999999")
                                                                            {

                                                                            }
                                                                            else
                                                                            {
                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                            }
                                                                            if (Dung == true)
                                                                            {
                                                                                if (TongLevel != "8")
                                                                                {
                                                                                    if (TongLevel != "45")
                                                                                    {
                                                                                        ThemHoaHong_ThuongLevel("0", "F7", "55", IDThanhVien, tableTVTF7.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), TongLevel);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    ThemHoaHong_ThuongLevel("0", "F7", "55", IDThanhVien, tableTVTF7.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), "0");
                                                                                }
                                                                                #region Dừng nếu gặp lelvel5
                                                                                string leveeeel = TimLevelB(tableTVTF7.GioiThieu.ToString());
                                                                                if (leveeeel == "5")
                                                                                {
                                                                                    Plevel = "45";
                                                                                }
                                                                                #endregion
                                                                            }
                                                                        }
                                                                        catch (Exception)
                                                                        { }
                                                                    }
                                                                    #region Hoa Hồng Gián tiếp F8
                                                                    user tableTVTF8 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF7.GioiThieu.ToString()));
                                                                    if (tableTVTF8 != null)
                                                                    {
                                                                        if (tableTVTF8.GioiThieu.ToString() != "0")
                                                                        {

                                                                            if (ShowFQRcode.ShowF8(tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                            {
                                                                                Dung = false;
                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                            }
                                                                            else
                                                                            {
                                                                                Dung = true;
                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF8(tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                            }
                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                            if (Plevel.ToString() == "99999999999")
                                                                            {

                                                                            }
                                                                            else
                                                                            {
                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                            }
                                                                            if (Dung == true)
                                                                            {
                                                                                if (TongLevel != "8")
                                                                                {
                                                                                    if (TongLevel != "45")
                                                                                    {
                                                                                        ThemHoaHong_ThuongLevel("0", "F8", "55", IDThanhVien, tableTVTF8.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), TongLevel);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    ThemHoaHong_ThuongLevel("0", "F8", "55", IDThanhVien, tableTVTF8.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), "0");
                                                                                }

                                                                                #region Dừng nếu gặp lelvel5
                                                                                string leveeeel = TimLevelB(tableTVTF8.GioiThieu.ToString());
                                                                                if (leveeeel == "5")
                                                                                {
                                                                                    Plevel = "45";
                                                                                }
                                                                                #endregion

                                                                            }

                                                                        }

                                                                        #region Hoa Hồng Gián tiếp F9
                                                                        user tableTVTF9 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF8.GioiThieu.ToString()));
                                                                        if (tableTVTF9 != null)
                                                                        {
                                                                            if (tableTVTF9.GioiThieu.ToString() != "0")
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (ShowFQRcode.ShowF9(tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                    {
                                                                                        Dung = false;
                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Dung = true;
                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF9(tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                    }
                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                    {

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                    }
                                                                                    if (Dung == true)
                                                                                    {
                                                                                        if (TongLevel != "8")
                                                                                        {
                                                                                            if (TongLevel != "45")
                                                                                            {

                                                                                                ThemHoaHong_ThuongLevel("0", "F9", "55", IDThanhVien, tableTVTF9.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), TongLevel);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            ThemHoaHong_ThuongLevel("0", "F9", "55", IDThanhVien, tableTVTF9.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), "0");
                                                                                        }
                                                                                        #region Dừng nếu gặp lelvel5
                                                                                        string leveeeel = TimLevelB(tableTVTF9.GioiThieu.ToString());
                                                                                        if (leveeeel == "5")
                                                                                        {
                                                                                            Plevel = "45";
                                                                                        }
                                                                                        #endregion
                                                                                    }
                                                                                }
                                                                                catch (Exception)
                                                                                { }
                                                                            }
                                                                            #region Hoa Hồng Gián tiếp F10
                                                                            user tableTVTF10 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF9.GioiThieu.ToString()));
                                                                            if (tableTVTF10 != null)
                                                                            {
                                                                                if (tableTVTF10.GioiThieu.ToString() != "0")
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        if (ShowFQRcode.ShowF10(tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                        {
                                                                                            Dung = false;
                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Dung = true;
                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF10(tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                        }
                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                        {

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                        }
                                                                                        if (Dung == true)
                                                                                        {
                                                                                            if (TongLevel != "8")
                                                                                            {
                                                                                                if (TongLevel != "45")
                                                                                                {
                                                                                                    ThemHoaHong_ThuongLevel("0", "F10", "55", IDThanhVien, tableTVTF10.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), TongLevel);
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                ThemHoaHong_ThuongLevel("0", "F10", "55", IDThanhVien, tableTVTF10.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), "0");
                                                                                            }
                                                                                            #region Dừng nếu gặp lelvel5
                                                                                            string leveeeel = TimLevelB(tableTVTF10.GioiThieu.ToString());
                                                                                            if (leveeeel == "5")
                                                                                            {
                                                                                                Plevel = "45";
                                                                                            }
                                                                                            #endregion
                                                                                        }
                                                                                    }
                                                                                    catch (Exception)
                                                                                    { }
                                                                                }
                                                                                #region Hoa Hồng Gián tiếp F11
                                                                                user tableTVTF11 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF10.GioiThieu.ToString()));
                                                                                if (tableTVTF11 != null)
                                                                                {
                                                                                    if (tableTVTF11.GioiThieu.ToString() != "0")
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            if (ShowFQRcode.ShowF11(tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                            {
                                                                                                Dung = false;
                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Dung = true;
                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF11(tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                            }
                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                            {

                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                            }
                                                                                            if (Dung == true)
                                                                                            {
                                                                                                if (TongLevel != "8")
                                                                                                {
                                                                                                    if (TongLevel != "45")
                                                                                                    {
                                                                                                        ThemHoaHong_ThuongLevel("0", "F11", "55", IDThanhVien, tableTVTF11.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), TongLevel);
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    ThemHoaHong_ThuongLevel("0", "F11", "55", IDThanhVien, tableTVTF11.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), "0");
                                                                                                }
                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                string leveeeel = TimLevelB(tableTVTF11.GioiThieu.ToString());
                                                                                                if (leveeeel == "5")
                                                                                                {
                                                                                                    Plevel = "45";
                                                                                                }
                                                                                                #endregion
                                                                                            }
                                                                                        }
                                                                                        catch (Exception)
                                                                                        { }
                                                                                    }
                                                                                    #region Hoa Hồng Gián tiếp F12
                                                                                    user tableTVTF12 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF11.GioiThieu.ToString()));
                                                                                    if (tableTVTF12 != null)
                                                                                    {
                                                                                        if (tableTVTF12.GioiThieu.ToString() != "0")
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                if (ShowFQRcode.ShowF12(tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                {
                                                                                                    Dung = false;
                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Dung = true;
                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF12(tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                }
                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                {

                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                }
                                                                                                if (Dung == true)
                                                                                                {
                                                                                                    if (TongLevel != "8")
                                                                                                    {
                                                                                                        if (TongLevel != "45")
                                                                                                        {
                                                                                                            ThemHoaHong_ThuongLevel("0", "F12", "55", IDThanhVien, tableTVTF12.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), TongLevel);
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        ThemHoaHong_ThuongLevel("0", "F12", "55", IDThanhVien, tableTVTF12.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), "0");
                                                                                                    }
                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                    string leveeeel = TimLevelB(tableTVTF12.GioiThieu.ToString());
                                                                                                    if (leveeeel == "5")
                                                                                                    {
                                                                                                        Plevel = "45";
                                                                                                    }
                                                                                                    #endregion
                                                                                                }
                                                                                            }
                                                                                            catch (Exception)
                                                                                            { }
                                                                                        }
                                                                                        #region Hoa Hồng Gián tiếp tableTVTF13
                                                                                        user tableTVTF13 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF12.GioiThieu.ToString()));
                                                                                        if (tableTVTF13 != null)
                                                                                        {
                                                                                            if (tableTVTF13.GioiThieu.ToString() != "0")
                                                                                            {
                                                                                                try
                                                                                                {
                                                                                                    if (ShowFQRcode.ShowF13(tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                    {
                                                                                                        Dung = false;
                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Dung = true;
                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF13(tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                    }
                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                    {

                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                    }
                                                                                                    if (Dung == true)
                                                                                                    {
                                                                                                        if (TongLevel != "8")
                                                                                                        {
                                                                                                            if (TongLevel != "45")
                                                                                                            {
                                                                                                                ThemHoaHong_ThuongLevel("0", "F13", "55", IDThanhVien, tableTVTF13.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), TongLevel);
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            ThemHoaHong_ThuongLevel("0", "F13", "55", IDThanhVien, tableTVTF13.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), "0");
                                                                                                        }
                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                        string leveeeel = TimLevelB(tableTVTF13.GioiThieu.ToString());
                                                                                                        if (leveeeel == "5")
                                                                                                        {
                                                                                                            Plevel = "45";
                                                                                                        }
                                                                                                        #endregion
                                                                                                    }
                                                                                                }
                                                                                                catch (Exception)
                                                                                                { }
                                                                                            }
                                                                                            #region Hoa Hồng Gián tiếp tableTVTF14
                                                                                            user tableTVTF14 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF13.GioiThieu.ToString()));
                                                                                            if (tableTVTF14 != null)
                                                                                            {
                                                                                                if (tableTVTF14.GioiThieu.ToString() != "0")
                                                                                                {
                                                                                                    try
                                                                                                    {
                                                                                                        if (ShowFQRcode.ShowF14(tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                        {
                                                                                                            Dung = false;
                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            Dung = true;
                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF14(tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                        }
                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                        {

                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                        }
                                                                                                        if (Dung == true)
                                                                                                        {
                                                                                                            if (TongLevel != "8")
                                                                                                            {
                                                                                                                if (TongLevel != "45")
                                                                                                                {
                                                                                                                    ThemHoaHong_ThuongLevel("0", "F14", "55", IDThanhVien, tableTVTF14.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), TongLevel);
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                ThemHoaHong_ThuongLevel("0", "F14", "55", IDThanhVien, tableTVTF14.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), "0");
                                                                                                            }
                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                            string leveeeel = TimLevelB(tableTVTF14.GioiThieu.ToString());
                                                                                                            if (leveeeel == "5")
                                                                                                            {
                                                                                                                Plevel = "45";
                                                                                                            }
                                                                                                            #endregion
                                                                                                        }
                                                                                                    }
                                                                                                    catch (Exception)
                                                                                                    { }
                                                                                                }
                                                                                                #region Hoa Hồng Gián tiếp tableTVTF15
                                                                                                user tableTVTF15 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF14.GioiThieu.ToString()));
                                                                                                if (tableTVTF15 != null)
                                                                                                {
                                                                                                    if (tableTVTF15.GioiThieu.ToString() != "0")
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (ShowFQRcode.ShowF15(tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                            {
                                                                                                                Dung = false;
                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                Dung = true;
                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF15(tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                            }
                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                            {

                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                            }
                                                                                                            if (Dung == true)
                                                                                                            {
                                                                                                                if (TongLevel != "8")
                                                                                                                {
                                                                                                                    if (TongLevel != "45")
                                                                                                                    {
                                                                                                                        ThemHoaHong_ThuongLevel("0", "F15", "55", IDThanhVien, tableTVTF15.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), TongLevel);
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    ThemHoaHong_ThuongLevel("0", "F15", "55", IDThanhVien, tableTVTF15.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), "0");
                                                                                                                }
                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                string leveeeel = TimLevelB(tableTVTF15.GioiThieu.ToString());
                                                                                                                if (leveeeel == "5")
                                                                                                                {
                                                                                                                    Plevel = "45";
                                                                                                                }
                                                                                                                #endregion
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception)
                                                                                                        { }
                                                                                                    }

                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF16
                                                                                                    user tableTVTF16 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF15.GioiThieu.ToString()));
                                                                                                    if (tableTVTF16 != null)
                                                                                                    {
                                                                                                        if (tableTVTF16.GioiThieu.ToString() != "0")
                                                                                                        {
                                                                                                            try
                                                                                                            {
                                                                                                                if (ShowFQRcode.ShowF16(tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                {
                                                                                                                    Dung = false;
                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    Dung = true;
                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF16(tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                }
                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                {

                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                }
                                                                                                                if (Dung == true)
                                                                                                                {
                                                                                                                    if (TongLevel != "8")
                                                                                                                    {
                                                                                                                        if (TongLevel != "45")
                                                                                                                        {
                                                                                                                            ThemHoaHong_ThuongLevel("0", "F16", "55", IDThanhVien, tableTVTF16.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), TongLevel);
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        ThemHoaHong_ThuongLevel("0", "F16", "55", IDThanhVien, tableTVTF16.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), "0");
                                                                                                                    }
                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                    string leveeeel = TimLevelB(tableTVTF16.GioiThieu.ToString());
                                                                                                                    if (leveeeel == "5")
                                                                                                                    {
                                                                                                                        Plevel = "45";
                                                                                                                    }
                                                                                                                    #endregion
                                                                                                                }
                                                                                                            }
                                                                                                            catch (Exception)
                                                                                                            { }
                                                                                                        }
                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF17
                                                                                                        user tableTVTF17 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF16.GioiThieu.ToString()));
                                                                                                        if (tableTVTF17 != null)
                                                                                                        {
                                                                                                            if (tableTVTF17.GioiThieu.ToString() != "0")
                                                                                                            {
                                                                                                                try
                                                                                                                {
                                                                                                                    if (ShowFQRcode.ShowF17(tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                    {
                                                                                                                        Dung = false;
                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        Dung = true;
                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF17(tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                    }
                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                    {

                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                    }
                                                                                                                    if (Dung == true)
                                                                                                                    {
                                                                                                                        if (TongLevel != "8")
                                                                                                                        {
                                                                                                                            if (TongLevel != "45")
                                                                                                                            {
                                                                                                                                ThemHoaHong_ThuongLevel("0", "F17", "55", IDThanhVien, tableTVTF17.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), TongLevel);
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            ThemHoaHong_ThuongLevel("0", "F17", "55", IDThanhVien, tableTVTF17.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), "0");
                                                                                                                        }
                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                        string leveeeel = TimLevelB(tableTVTF17.GioiThieu.ToString());
                                                                                                                        if (leveeeel == "5")
                                                                                                                        {
                                                                                                                            Plevel = "45";
                                                                                                                        }
                                                                                                                        #endregion
                                                                                                                    }
                                                                                                                }
                                                                                                                catch (Exception)
                                                                                                                { }
                                                                                                            }

                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF18
                                                                                                            user tableTVTF18 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF17.GioiThieu.ToString()));
                                                                                                            if (tableTVTF18 != null)
                                                                                                            {
                                                                                                                if (tableTVTF18.GioiThieu.ToString() != "0")
                                                                                                                {
                                                                                                                    try
                                                                                                                    {
                                                                                                                        if (ShowFQRcode.ShowF18(tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                        {
                                                                                                                            Dung = false;
                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            Dung = true;
                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF18(tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                        }
                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                        {

                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                        }
                                                                                                                        if (Dung == true)
                                                                                                                        {
                                                                                                                            if (TongLevel != "8")
                                                                                                                            {
                                                                                                                                if (TongLevel != "45")
                                                                                                                                {
                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F18", "55", IDThanhVien, tableTVTF18.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), TongLevel);
                                                                                                                                }
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                ThemHoaHong_ThuongLevel("0", "F18", "55", IDThanhVien, tableTVTF18.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), "0");
                                                                                                                            }
                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                            string leveeeel = TimLevelB(tableTVTF18.GioiThieu.ToString());
                                                                                                                            if (leveeeel == "5")
                                                                                                                            {
                                                                                                                                Plevel = "45";
                                                                                                                            }
                                                                                                                            #endregion
                                                                                                                        }
                                                                                                                    }
                                                                                                                    catch (Exception)
                                                                                                                    { }
                                                                                                                }

                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF19
                                                                                                                user tableTVTF19 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF18.GioiThieu.ToString()));
                                                                                                                if (tableTVTF19 != null)
                                                                                                                {
                                                                                                                    if (tableTVTF19.GioiThieu.ToString() != "0")
                                                                                                                    {
                                                                                                                        try
                                                                                                                        {
                                                                                                                            if (ShowFQRcode.ShowF19(tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                            {
                                                                                                                                Dung = false;
                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                Dung = true;
                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF19(tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                            }
                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                            {

                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                            }
                                                                                                                            if (Dung == true)
                                                                                                                            {
                                                                                                                                if (TongLevel != "8")
                                                                                                                                {
                                                                                                                                    if (TongLevel != "45")
                                                                                                                                    {
                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F19", "55", IDThanhVien, tableTVTF19.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), TongLevel);
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F19", "55", IDThanhVien, tableTVTF19.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), "0");
                                                                                                                                }
                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                string leveeeel = TimLevelB(tableTVTF19.GioiThieu.ToString());
                                                                                                                                if (leveeeel == "5")
                                                                                                                                {
                                                                                                                                    Plevel = "45";
                                                                                                                                }
                                                                                                                                #endregion
                                                                                                                            }
                                                                                                                        }
                                                                                                                        catch (Exception)
                                                                                                                        { }
                                                                                                                    }
                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF20
                                                                                                                    user tableTVTF20 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF19.GioiThieu.ToString()));
                                                                                                                    if (tableTVTF20 != null)
                                                                                                                    {
                                                                                                                        if (tableTVTF20.GioiThieu.ToString() != "0")
                                                                                                                        {
                                                                                                                            try
                                                                                                                            {
                                                                                                                                if (ShowFQRcode.ShowF20(tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                {
                                                                                                                                    Dung = false;
                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    Dung = true;
                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF20(tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                }
                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                {

                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                }
                                                                                                                                if (Dung == true)
                                                                                                                                {
                                                                                                                                    if (TongLevel != "8")
                                                                                                                                    {
                                                                                                                                        if (TongLevel != "45")
                                                                                                                                        {
                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F20", "55", IDThanhVien, tableTVTF20.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), TongLevel);
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F20", "55", IDThanhVien, tableTVTF20.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), "0");
                                                                                                                                    }
                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                    string leveeeel = TimLevelB(tableTVTF20.GioiThieu.ToString());
                                                                                                                                    if (leveeeel == "5")
                                                                                                                                    {
                                                                                                                                        Plevel = "45";
                                                                                                                                    }
                                                                                                                                    #endregion
                                                                                                                                }
                                                                                                                            }
                                                                                                                            catch (Exception)
                                                                                                                            { }
                                                                                                                        }
                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF21
                                                                                                                        user tableTVTF21 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF20.GioiThieu.ToString()));
                                                                                                                        if (tableTVTF21 != null)
                                                                                                                        {
                                                                                                                            if (tableTVTF21.GioiThieu.ToString() != "0")
                                                                                                                            {
                                                                                                                                try
                                                                                                                                {
                                                                                                                                    if (ShowFQRcode.ShowF21(tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                    {
                                                                                                                                        Dung = false;
                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        Dung = true;
                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF21(tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                    }
                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                    {

                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                    }
                                                                                                                                    if (Dung == true)
                                                                                                                                    {
                                                                                                                                        if (TongLevel != "8")
                                                                                                                                        {
                                                                                                                                            if (TongLevel != "45")
                                                                                                                                            {
                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F21", "55", IDThanhVien, tableTVTF21.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), TongLevel);
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F21", "55", IDThanhVien, tableTVTF21.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), "0");
                                                                                                                                        }
                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                        string leveeeel = TimLevelB(tableTVTF21.GioiThieu.ToString());
                                                                                                                                        if (leveeeel == "5")
                                                                                                                                        {
                                                                                                                                            Plevel = "45";
                                                                                                                                        }
                                                                                                                                        #endregion
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                catch (Exception)
                                                                                                                                { }
                                                                                                                            }
                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF22
                                                                                                                            user tableTVTF22 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF21.GioiThieu.ToString()));
                                                                                                                            if (tableTVTF22 != null)
                                                                                                                            {
                                                                                                                                if (tableTVTF22.GioiThieu.ToString() != "0")
                                                                                                                                {
                                                                                                                                    try
                                                                                                                                    {
                                                                                                                                        if (ShowFQRcode.ShowF22(tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                        {
                                                                                                                                            Dung = false;
                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            Dung = true;
                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF22(tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                        }
                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                        {

                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                        }
                                                                                                                                        if (Dung == true)
                                                                                                                                        {
                                                                                                                                            if (TongLevel != "8")
                                                                                                                                            {
                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                {
                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F22", "55", IDThanhVien, tableTVTF22.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), TongLevel);
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F22", "55", IDThanhVien, tableTVTF22.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), "0");
                                                                                                                                            }
                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                            string leveeeel = TimLevelB(tableTVTF22.GioiThieu.ToString());
                                                                                                                                            if (leveeeel == "5")
                                                                                                                                            {
                                                                                                                                                Plevel = "45";
                                                                                                                                            }
                                                                                                                                            #endregion
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    catch (Exception)
                                                                                                                                    { }
                                                                                                                                }
                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF23
                                                                                                                                user tableTVTF23 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF22.GioiThieu.ToString()));
                                                                                                                                if (tableTVTF23 != null)
                                                                                                                                {
                                                                                                                                    if (tableTVTF23.GioiThieu.ToString() != "0")
                                                                                                                                    {
                                                                                                                                        try
                                                                                                                                        {
                                                                                                                                            if (ShowFQRcode.ShowF23(tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                            {
                                                                                                                                                Dung = false;
                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                Dung = true;
                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF23(tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                            }
                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                            {

                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                            }
                                                                                                                                            if (Dung == true)
                                                                                                                                            {
                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                {
                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                    {
                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F23", "55", IDThanhVien, tableTVTF23.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), TongLevel);
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F23", "55", IDThanhVien, tableTVTF23.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), "0");
                                                                                                                                                }

                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                string leveeeel = TimLevelB(tableTVTF23.GioiThieu.ToString());
                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                {
                                                                                                                                                    Plevel = "45";
                                                                                                                                                }
                                                                                                                                                #endregion
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        catch (Exception)
                                                                                                                                        { }
                                                                                                                                    }
                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF24
                                                                                                                                    user tableTVTF24 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF23.GioiThieu.ToString()));
                                                                                                                                    if (tableTVTF24 != null)
                                                                                                                                    {
                                                                                                                                        if (tableTVTF24.GioiThieu.ToString() != "0")
                                                                                                                                        {
                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                if (ShowFQRcode.ShowF24(tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                {
                                                                                                                                                    Dung = false;
                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    Dung = true;
                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF24(tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                }
                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                {

                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                }
                                                                                                                                                if (Dung == true)
                                                                                                                                                {
                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                    {
                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                        {
                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F24", "55", IDThanhVien, tableTVTF24.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), TongLevel);
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F24", "55", IDThanhVien, tableTVTF24.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), "0");
                                                                                                                                                    }
                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF24.GioiThieu.ToString());
                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                    {
                                                                                                                                                        Plevel = "45";
                                                                                                                                                    }
                                                                                                                                                    #endregion
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            catch (Exception)
                                                                                                                                            { }
                                                                                                                                        }
                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF25
                                                                                                                                        user tableTVTF25 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF24.GioiThieu.ToString()));
                                                                                                                                        if (tableTVTF25 != null)
                                                                                                                                        {
                                                                                                                                            if (tableTVTF25.GioiThieu.ToString() != "0")
                                                                                                                                            {
                                                                                                                                                try
                                                                                                                                                {
                                                                                                                                                    if (ShowFQRcode.ShowF25(tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                    {
                                                                                                                                                        Dung = false;
                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        Dung = true;
                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF25(tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                    }
                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                    {

                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                    }
                                                                                                                                                    if (Dung == true)
                                                                                                                                                    {
                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                        {
                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                            {
                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F25", "55", IDThanhVien, tableTVTF25.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), TongLevel);
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F25", "55", IDThanhVien, tableTVTF25.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), "0");
                                                                                                                                                        }
                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF25.GioiThieu.ToString());
                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                        {
                                                                                                                                                            Plevel = "45";
                                                                                                                                                        }
                                                                                                                                                        #endregion
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                catch (Exception)
                                                                                                                                                { }
                                                                                                                                            }
                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF26
                                                                                                                                            user tableTVTF26 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF25.GioiThieu.ToString()));
                                                                                                                                            if (tableTVTF26 != null)
                                                                                                                                            {
                                                                                                                                                if (tableTVTF26.GioiThieu.ToString() != "0")
                                                                                                                                                {
                                                                                                                                                    try
                                                                                                                                                    {
                                                                                                                                                        if (ShowFQRcode.ShowF26(tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                        {
                                                                                                                                                            Dung = false;
                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            Dung = true;
                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF26(tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                        }
                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                        {

                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                        }
                                                                                                                                                        if (Dung == true)
                                                                                                                                                        {
                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                            {
                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                {
                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F26", "55", IDThanhVien, tableTVTF26.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F26", "55", IDThanhVien, tableTVTF26.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), "0");
                                                                                                                                                            }
                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF26.GioiThieu.ToString());
                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                            {
                                                                                                                                                                Plevel = "45";
                                                                                                                                                            }
                                                                                                                                                            #endregion
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    catch (Exception)
                                                                                                                                                    { }
                                                                                                                                                }

                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF27
                                                                                                                                                user tableTVTF27 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF26.GioiThieu.ToString()));
                                                                                                                                                if (tableTVTF27 != null)
                                                                                                                                                {
                                                                                                                                                    if (tableTVTF27.GioiThieu.ToString() != "0")
                                                                                                                                                    {
                                                                                                                                                        try
                                                                                                                                                        {
                                                                                                                                                            if (ShowFQRcode.ShowF27(tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                            {
                                                                                                                                                                Dung = false;
                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                Dung = true;
                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF27(tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                            }
                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                            {

                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                            }
                                                                                                                                                            if (Dung == true)
                                                                                                                                                            {
                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                {
                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                    {
                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F27", "55", IDThanhVien, tableTVTF27.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F27", "55", IDThanhVien, tableTVTF27.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), "0");
                                                                                                                                                                }
                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF27.GioiThieu.ToString());
                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                {
                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                }
                                                                                                                                                                #endregion
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        catch (Exception)
                                                                                                                                                        { }
                                                                                                                                                    }
                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF28
                                                                                                                                                    user tableTVTF28 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF27.GioiThieu.ToString()));
                                                                                                                                                    if (tableTVTF28 != null)
                                                                                                                                                    {
                                                                                                                                                        if (tableTVTF28.GioiThieu.ToString() != "0")
                                                                                                                                                        {
                                                                                                                                                            try
                                                                                                                                                            {
                                                                                                                                                                if (ShowFQRcode.ShowF28(tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                {
                                                                                                                                                                    Dung = false;
                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    Dung = true;
                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF28(tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                }
                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                {

                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                }
                                                                                                                                                                if (Dung == true)
                                                                                                                                                                {
                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                    {
                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                        {
                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F28", "55", IDThanhVien, tableTVTF28.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F28", "55", IDThanhVien, tableTVTF28.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), "0");
                                                                                                                                                                    }
                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF28.GioiThieu.ToString());
                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                    {
                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                    }
                                                                                                                                                                    #endregion
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            catch (Exception)
                                                                                                                                                            { }
                                                                                                                                                        }

                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF29
                                                                                                                                                        user tableTVTF29 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF28.GioiThieu.ToString()));
                                                                                                                                                        if (tableTVTF29 != null)
                                                                                                                                                        {
                                                                                                                                                            if (tableTVTF29.GioiThieu.ToString() != "0")
                                                                                                                                                            {
                                                                                                                                                                try
                                                                                                                                                                {
                                                                                                                                                                    if (ShowFQRcode.ShowF29(tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                    {
                                                                                                                                                                        Dung = false;
                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        Dung = true;
                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF29(tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                    }
                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                    {

                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                    }
                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                    {
                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                        {
                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                            {
                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F29", "55", IDThanhVien, tableTVTF29.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F29", "55", IDThanhVien, tableTVTF29.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), "0");
                                                                                                                                                                        }
                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF29.GioiThieu.ToString());
                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                        {
                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                        }
                                                                                                                                                                        #endregion
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                catch (Exception)
                                                                                                                                                                { }
                                                                                                                                                            }
                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF30
                                                                                                                                                            user tableTVTF30 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF29.GioiThieu.ToString()));
                                                                                                                                                            if (tableTVTF30 != null)
                                                                                                                                                            {
                                                                                                                                                                if (tableTVTF30.GioiThieu.ToString() != "0")
                                                                                                                                                                {
                                                                                                                                                                    try
                                                                                                                                                                    {
                                                                                                                                                                        if (ShowFQRcode.ShowF30(tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                        {
                                                                                                                                                                            Dung = false;
                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            Dung = true;
                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF30(tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                        }
                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                        {

                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                        }
                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                        {
                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                            {
                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                {
                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F30", "55", IDThanhVien, tableTVTF30.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F30", "55", IDThanhVien, tableTVTF30.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), "0");
                                                                                                                                                                            }
                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF30.GioiThieu.ToString());
                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                            {
                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                            }
                                                                                                                                                                            #endregion
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    catch (Exception)
                                                                                                                                                                    { }
                                                                                                                                                                }

                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF31
                                                                                                                                                                user tableTVTF31 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF30.GioiThieu.ToString()));
                                                                                                                                                                if (tableTVTF31 != null)
                                                                                                                                                                {
                                                                                                                                                                    if (tableTVTF31.GioiThieu.ToString() != "0")
                                                                                                                                                                    {
                                                                                                                                                                        try
                                                                                                                                                                        {
                                                                                                                                                                            if (ShowFQRcode.ShowF31(tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                            {
                                                                                                                                                                                Dung = false;
                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                Dung = true;
                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF31(tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                            }
                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                            {

                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                            }
                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                            {
                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                {
                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                    {
                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F31", "55", IDThanhVien, tableTVTF31.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F31", "55", IDThanhVien, tableTVTF31.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), "0");
                                                                                                                                                                                }
                                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF31.GioiThieu.ToString());
                                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                                {
                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                }
                                                                                                                                                                                #endregion
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        catch (Exception)
                                                                                                                                                                        { }
                                                                                                                                                                    }

                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF32
                                                                                                                                                                    user tableTVTF32 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF31.GioiThieu.ToString()));
                                                                                                                                                                    if (tableTVTF32 != null)
                                                                                                                                                                    {
                                                                                                                                                                        if (tableTVTF32.GioiThieu.ToString() != "0")
                                                                                                                                                                        {
                                                                                                                                                                            try
                                                                                                                                                                            {
                                                                                                                                                                                if (ShowFQRcode.ShowF32(tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                {
                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF32(tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                }
                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                {

                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                }
                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                {
                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                    {
                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                        {
                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F32", "55", IDThanhVien, tableTVTF32.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F32", "55", IDThanhVien, tableTVTF32.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), "0");
                                                                                                                                                                                    }
                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF32.GioiThieu.ToString());
                                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                                    {
                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                    }
                                                                                                                                                                                    #endregion
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            catch (Exception)
                                                                                                                                                                            { }
                                                                                                                                                                        }
                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF33
                                                                                                                                                                        user tableTVTF33 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF32.GioiThieu.ToString()));
                                                                                                                                                                        if (tableTVTF33 != null)
                                                                                                                                                                        {
                                                                                                                                                                            if (tableTVTF33.GioiThieu.ToString() != "0")
                                                                                                                                                                            {
                                                                                                                                                                                try
                                                                                                                                                                                {
                                                                                                                                                                                    if (ShowFQRcode.ShowF33(tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                    {
                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF33(tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                    }
                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                    {

                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                    }
                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                    {
                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                        {
                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                            {
                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F33", "55", IDThanhVien, tableTVTF33.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F33", "55", IDThanhVien, tableTVTF33.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), "0");
                                                                                                                                                                                        }
                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF33.GioiThieu.ToString());
                                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                                        {
                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                        }
                                                                                                                                                                                        #endregion
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                { }
                                                                                                                                                                            }
                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF34
                                                                                                                                                                            user tableTVTF34 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF33.GioiThieu.ToString()));
                                                                                                                                                                            if (tableTVTF34 != null)
                                                                                                                                                                            {
                                                                                                                                                                                if (tableTVTF34.GioiThieu.ToString() != "0")
                                                                                                                                                                                {
                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        if (ShowFQRcode.ShowF34(tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                        {
                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF34(tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                        }
                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                        {

                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                        }
                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                        {
                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                            {
                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                {
                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F34", "55", IDThanhVien, tableTVTF34.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F34", "55", IDThanhVien, tableTVTF34.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), "0");
                                                                                                                                                                                            }
                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF34.GioiThieu.ToString());
                                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                                            {
                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                            }
                                                                                                                                                                                            #endregion
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                    { }
                                                                                                                                                                                }
                                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF35
                                                                                                                                                                                user tableTVTF35 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF34.GioiThieu.ToString()));
                                                                                                                                                                                if (tableTVTF35 != null)
                                                                                                                                                                                {
                                                                                                                                                                                    if (tableTVTF35.GioiThieu.ToString() != "0")
                                                                                                                                                                                    {
                                                                                                                                                                                        try
                                                                                                                                                                                        {
                                                                                                                                                                                            if (ShowFQRcode.ShowF35(tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                            {
                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF35(tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                            }
                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                            {

                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                            }
                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                            {
                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F35", "55", IDThanhVien, tableTVTF35.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F35", "55", IDThanhVien, tableTVTF35.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), "0");
                                                                                                                                                                                                }
                                                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF35.GioiThieu.ToString());
                                                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                                                {
                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                }
                                                                                                                                                                                                #endregion
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                        { }
                                                                                                                                                                                    }
                                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF36
                                                                                                                                                                                    user tableTVTF36 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF35.GioiThieu.ToString()));
                                                                                                                                                                                    if (tableTVTF36 != null)
                                                                                                                                                                                    {
                                                                                                                                                                                        if (tableTVTF36.GioiThieu.ToString() != "0")
                                                                                                                                                                                        {
                                                                                                                                                                                            try
                                                                                                                                                                                            {
                                                                                                                                                                                                if (ShowFQRcode.ShowF36(tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                {
                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF36(tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                }
                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                {

                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                }
                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F36", "55", IDThanhVien, tableTVTF36.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F36", "55", IDThanhVien, tableTVTF36.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), "0");
                                                                                                                                                                                                    }
                                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF36.GioiThieu.ToString());
                                                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                    }
                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                            { }
                                                                                                                                                                                        }
                                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF37
                                                                                                                                                                                        user tableTVTF37 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF36.GioiThieu.ToString()));
                                                                                                                                                                                        if (tableTVTF37 != null)
                                                                                                                                                                                        {
                                                                                                                                                                                            if (tableTVTF37.GioiThieu.ToString() != "0")
                                                                                                                                                                                            {
                                                                                                                                                                                                try
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (ShowFQRcode.ShowF37(tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF37(tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                    }
                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                    {

                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                    }
                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F37", "55", IDThanhVien, tableTVTF37.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F37", "55", IDThanhVien, tableTVTF37.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), "0");
                                                                                                                                                                                                        }
                                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF37.GioiThieu.ToString());
                                                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                        }
                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                { }
                                                                                                                                                                                            }
                                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF38
                                                                                                                                                                                            user tableTVTF38 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF37.GioiThieu.ToString()));
                                                                                                                                                                                            if (tableTVTF38 != null)
                                                                                                                                                                                            {
                                                                                                                                                                                                if (tableTVTF38.GioiThieu.ToString() != "0")
                                                                                                                                                                                                {
                                                                                                                                                                                                    try
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (ShowFQRcode.ShowF38(tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF38(tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                        }
                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                        {

                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                        }
                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F38", "55", IDThanhVien, tableTVTF38.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F38", "55", IDThanhVien, tableTVTF38.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), "0");
                                                                                                                                                                                                            }
                                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF38.GioiThieu.ToString());
                                                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                            }
                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                    { }
                                                                                                                                                                                                }
                                                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF39
                                                                                                                                                                                                user tableTVTF39 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF38.GioiThieu.ToString()));
                                                                                                                                                                                                if (tableTVTF39 != null)
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (tableTVTF39.GioiThieu.ToString() != "0")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        try
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (ShowFQRcode.ShowF39(tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF39(tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                            }
                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                            {

                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                            }
                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F39", "55", IDThanhVien, tableTVTF39.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F39", "55", IDThanhVien, tableTVTF39.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                }

                                                                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF39.GioiThieu.ToString());
                                                                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                }
                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                        { }
                                                                                                                                                                                                    }

                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF40
                                                                                                                                                                                                    user tableTVTF40 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF39.GioiThieu.ToString()));
                                                                                                                                                                                                    if (tableTVTF40 != null)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (tableTVTF40.GioiThieu.ToString() != "0")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            try
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (ShowFQRcode.ShowF40(tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF40(tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                }
                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                {

                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                }
                                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F40", "55", IDThanhVien, tableTVTF40.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F40", "55", IDThanhVien, tableTVTF40.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF40.GioiThieu.ToString());
                                                                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                            { }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF41
                                                                                                                                                                                                        user tableTVTF41 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF40.GioiThieu.ToString()));
                                                                                                                                                                                                        if (tableTVTF41 != null)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (tableTVTF41.GioiThieu.ToString() != "0")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                try
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (ShowFQRcode.ShowF41(tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF41(tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                    {

                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F41", "55", IDThanhVien, tableTVTF41.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F41", "55", IDThanhVien, tableTVTF41.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF41.GioiThieu.ToString());
                                                                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                { }
                                                                                                                                                                                                            }

                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF42
                                                                                                                                                                                                            user tableTVTF42 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF41.GioiThieu.ToString()));
                                                                                                                                                                                                            if (tableTVTF42 != null)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (tableTVTF42.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    try
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (ShowFQRcode.ShowF42(tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF42(tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                        {

                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F42", "55", IDThanhVien, tableTVTF42.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F42", "55", IDThanhVien, tableTVTF42.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF42.GioiThieu.ToString());
                                                                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                    { }
                                                                                                                                                                                                                }

                                                                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF43
                                                                                                                                                                                                                user tableTVTF43 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF42.GioiThieu.ToString()));
                                                                                                                                                                                                                if (tableTVTF43 != null)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (tableTVTF43.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        try
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (ShowFQRcode.ShowF43(tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF43(tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                            {

                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F43", "55", IDThanhVien, tableTVTF43.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F43", "55", IDThanhVien, tableTVTF43.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF43.GioiThieu.ToString());
                                                                                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                                        { }
                                                                                                                                                                                                                    }

                                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF44
                                                                                                                                                                                                                    user tableTVTF44 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF43.GioiThieu.ToString()));
                                                                                                                                                                                                                    if (tableTVTF44 != null)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (tableTVTF44.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            try
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (ShowFQRcode.ShowF44(tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF44(tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                {

                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F44", "55", IDThanhVien, tableTVTF44.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F44", "55", IDThanhVien, tableTVTF44.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF44.GioiThieu.ToString());
                                                                                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                                            { }
                                                                                                                                                                                                                        }

                                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF45
                                                                                                                                                                                                                        user tableTVTF45 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF44.GioiThieu.ToString()));
                                                                                                                                                                                                                        if (tableTVTF45 != null)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (tableTVTF45.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                try
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (ShowFQRcode.ShowF45(tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF45(tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                    {

                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F45", "55", IDThanhVien, tableTVTF45.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F45", "55", IDThanhVien, tableTVTF45.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF45.GioiThieu.ToString());
                                                                                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                                { }
                                                                                                                                                                                                                            }

                                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF46
                                                                                                                                                                                                                            user tableTVTF46 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF45.GioiThieu.ToString()));
                                                                                                                                                                                                                            if (tableTVTF46 != null)
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (tableTVTF46.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    try
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (ShowFQRcode.ShowF46(tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF46(tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                        {

                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F46", "55", IDThanhVien, tableTVTF46.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F46", "55", IDThanhVien, tableTVTF46.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF46.GioiThieu.ToString());
                                                                                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                                    { }
                                                                                                                                                                                                                                }

                                                                                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF47
                                                                                                                                                                                                                                user tableTVTF47 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF46.GioiThieu.ToString()));
                                                                                                                                                                                                                                if (tableTVTF47 != null)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (tableTVTF47.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        try
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (ShowFQRcode.ShowF47(tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF47(tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                            {

                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F47", "55", IDThanhVien, tableTVTF47.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F47", "55", IDThanhVien, tableTVTF47.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF47.GioiThieu.ToString());
                                                                                                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                                                        { }
                                                                                                                                                                                                                                    }

                                                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF48
                                                                                                                                                                                                                                    user tableTVTF48 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF47.GioiThieu.ToString()));
                                                                                                                                                                                                                                    if (tableTVTF48 != null)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (tableTVTF48.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            try
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (ShowFQRcode.ShowF48(tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF48(tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                                {

                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F48", "55", IDThanhVien, tableTVTF48.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F48", "55", IDThanhVien, tableTVTF48.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF48.GioiThieu.ToString());
                                                                                                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                                                            { }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF49
                                                                                                                                                                                                                                        user tableTVTF49 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF48.GioiThieu.ToString()));
                                                                                                                                                                                                                                        if (tableTVTF49 != null)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (tableTVTF49.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                try
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (ShowFQRcode.ShowF49(tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF49(tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                                    {

                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F49", "55", IDThanhVien, tableTVTF49.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F49", "55", IDThanhVien, tableTVTF49.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF49.GioiThieu.ToString());
                                                                                                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                                                { }
                                                                                                                                                                                                                                            }

                                                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF50
                                                                                                                                                                                                                                            user tableTVTF50 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF49.GioiThieu.ToString()));
                                                                                                                                                                                                                                            if (tableTVTF50 != null)
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (tableTVTF50.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    try
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (ShowFQRcode.ShowF50(tableTVTF50.iuser_id.ToString(), tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF50(tableTVTF50.iuser_id.ToString(), tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                                        {

                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F50", "55", IDThanhVien, tableTVTF50.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F50", "55", IDThanhVien, tableTVTF50.GioiThieu.ToString(), HoaHongChonNHHHeThong.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF50.GioiThieu.ToString());
                                                                                                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                                                    { }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                }
                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                            }
                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                        }
                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                    }
                                                                                                                                                                                                    #endregion

                                                                                                                                                                                                }
                                                                                                                                                                                                #endregion
                                                                                                                                                                                            }
                                                                                                                                                                                            #endregion
                                                                                                                                                                                        }
                                                                                                                                                                                        #endregion
                                                                                                                                                                                    }
                                                                                                                                                                                    #endregion
                                                                                                                                                                                }
                                                                                                                                                                                #endregion
                                                                                                                                                                            }
                                                                                                                                                                            #endregion
                                                                                                                                                                        }
                                                                                                                                                                        #endregion
                                                                                                                                                                    }
                                                                                                                                                                    #endregion

                                                                                                                                                                }
                                                                                                                                                                #endregion
                                                                                                                                                            }
                                                                                                                                                            #endregion
                                                                                                                                                        }
                                                                                                                                                        #endregion
                                                                                                                                                    }
                                                                                                                                                    #endregion
                                                                                                                                                }
                                                                                                                                                #endregion


                                                                                                                                            }
                                                                                                                                            #endregion
                                                                                                                                        }
                                                                                                                                        #endregion
                                                                                                                                    }
                                                                                                                                    #endregion
                                                                                                                                }
                                                                                                                                #endregion
                                                                                                                            }
                                                                                                                            #endregion
                                                                                                                        }
                                                                                                                        #endregion
                                                                                                                    }
                                                                                                                    #endregion


                                                                                                                }
                                                                                                                #endregion
                                                                                                            }
                                                                                                            #endregion
                                                                                                        }
                                                                                                        #endregion
                                                                                                    }
                                                                                                    #endregion
                                                                                                }
                                                                                                #endregion
                                                                                            }
                                                                                            #endregion
                                                                                        }
                                                                                        #endregion
                                                                                    }
                                                                                    #endregion
                                                                                }
                                                                                #endregion
                                                                            }
                                                                            #endregion
                                                                        }
                                                                        #endregion

                                                                    }
                                                                    #endregion
                                                                }
                                                                #endregion
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                            }
                                            #endregion
                                        }
                                        #endregion
                                        #endregion
                                    }
                                }
                            }
                            #endregion
                            if (HoaHongChonNHHHeThong != 0)
                            {
                                #region Tính hoa hồng Nhà Cung Cấp Và Chi Nhánh cho kiểu sản phẩm (TrangThaiAgLang=1)
                                #region Mua hàng
                                #region Chi nhánh được hưởng 10 khi có giao dịch mua%
                                // Đối với chi nhánh hưởng 10 mua hàng%
                                if (!table.IDChiNhanh.Equals("0"))
                                {
                                    try
                                    {
                                        if (ShowIDChiNhanh(table.IDChiNhanh.ToString()) != "0")
                                        {
                                            double HoaHongChiNhanhMuaHang = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongChiNhanhMuaHang"));
                                            if (!MoreAll.Other.Giatri("HoaHongChiNhanhMuaHang").Equals("0"))
                                            {
                                                double TongCoinChiNhanh = (HoaHongChonNHHHeThong * HoaHongChiNhanhMuaHang) / 100;
                                                ThemHoaHong("0", "211", "Hoa Hồng QRCode (Chi Nhánh Mua Hàng) " + HoaHongChiNhanhMuaHang + "%", table.iuser_id.ToString(), ShowIDChiNhanh(table.IDChiNhanh.ToString()), HoaHongChiNhanhMuaHang.ToString(), TongCoinChiNhanh.ToString(), IDMaDonTao);
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                }
                                #endregion

                             

                                #region Hoa Hồng Leader cho người giới thiệu mua hàng
                                if (!TimLeader(table.GioiThieu).Equals("0"))
                                {
                                    try
                                    {
                                        double HoaHongLeaderMuaHang = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongLeaderMuaHang"));
                                        if (!MoreAll.Other.Giatri("HoaHongLeaderMuaHang").Equals("0"))
                                        {
                                            double TongLeader = (HoaHongChonNHHHeThong * HoaHongLeaderMuaHang) / 100;
                                            ThemHoaHong("0", "212", "Hoa Hồng QRCode (Leader - Mua Hàng)", table.iuser_id.ToString(), TimLeader(table.GioiThieu), HoaHongLeaderMuaHang.ToString(), TongLeader.ToString(), IDMaDonTao);
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                }
                                #endregion
                                #endregion

                                // không có hooa hồng cho nhánh bán hàng hay nhà cung cấp vì mình ko biết họ bán sản phảm j
                                #endregion
                            }

                            #region Vi Loi Nhuan sau khi da chia HH
                            try
                            {
                                var tongdiemdachia = db.S_TongDiemDaChia_QRCode(int.Parse(IDThanhVien.ToString()), int.Parse(IDMaDonTao)).ToList();
                                if (tongdiemdachia[0].sodiem >= 0)
                                {
                                    Double TongDaChia = Convert.ToDouble(tongdiemdachia[0].sodiem.ToString());
                                    Double TongCongs = SoTienChietKhau - TongDaChia;

                                    //string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
                                    //if (LogFile == "true")
                                    //{
                                    //    Library.WriteErrorLogTongThanhToan(" Diemcoin  " + Tiencoin + "TongDaChia  " + TongDaChia + " TongCongs  " + TongCongs);
                                    //}

                                    // ID đơn hàng lấy thành mã thành viên đăng ký
                                    LoiNhuanMuaBanQRCode abln = new LoiNhuanMuaBanQRCode();
                                    abln.IDThanhVienMua = int.Parse(IDThanhVien.ToString());
                                    abln.IDThanhVienBan = int.Parse(hdIDThanhVien.Value);
                                    abln.MoTa = txtnoidungthanhtoan.Text;
                                    abln.NgayTao = DateTime.Now;
                                    abln.SoDiemChuyen = SoTienChietKhau.ToString();
                                    abln.SoDiemConLai = TongCongs.ToString();
                                    abln.SoDiemDaChia = TongDaChia.ToString();
                                    abln.MTreeIDThanhVienMua = Commond.ShowMTree(IDThanhVien.ToString());
                                    abln.MTReIDThanhVienBan = Commond.ShowMTree(hdIDThanhVien.ToString());
                                    abln.IDMaDonTao = Convert.ToInt32(IDMaDonTao);
                                    db.LoiNhuanMuaBanQRCodes.InsertOnSubmit(abln);
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    Double TongCongs = SoTienChietKhau;
                                    LoiNhuanMuaBanQRCode abln = new LoiNhuanMuaBanQRCode();
                                    abln.IDThanhVienMua = int.Parse(IDThanhVien.ToString());
                                    abln.IDThanhVienBan = int.Parse(hdIDThanhVien.Value);
                                    abln.MoTa = txtnoidungthanhtoan.Text;
                                    abln.NgayTao = DateTime.Now;
                                    abln.SoDiemChuyen = SoTienChietKhau.ToString();
                                    abln.SoDiemConLai = TongCongs.ToString();
                                    abln.SoDiemDaChia = "0";
                                    abln.MTreeIDThanhVienMua = Commond.ShowMTree(IDThanhVien.ToString());
                                    abln.MTReIDThanhVienBan = Commond.ShowMTree(hdIDThanhVien.ToString());
                                    abln.IDMaDonTao = Convert.ToInt32(IDMaDonTao);
                                    db.LoiNhuanMuaBanQRCodes.InsertOnSubmit(abln);
                                    db.SubmitChanges();
                                }
                            }
                            catch (Exception)
                            { }
                            #endregion

                        }
                    }
                    #endregion


                    Response.Write("<script type=\"text/javascript\">alert('Bạn đã thanh toán điểm thành công.');</script>");
                    // ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\">Bạn đã thanh toán thành công đơn hàng.</div> ";
                }
                else
                {
                    ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\">Số tiền trong ví không đủ để thanh toán.</div> ";
                    return;
                }
            }
            else
            {
                ltmess.Text = "<div class=\"alert alert-warning\" role=\"alert\">Số tiền trong ví không đủ để thanh toán</div> ";
                return;
            }
            #endregion
        }

        void ThemHoaHong(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDMaDonTao)
        {
            #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
            Commond.CheckNgayHetHan(IDUserNguoiDuocHuong.ToString());
            #endregion

            List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and DuyetTienDanap=1 ");
            if (F1.Count() > 0 || (IDUserNguoiDuocHuong == "0") || IDThanhVien == IDUserNguoiDuocHuong)
            {
                #region HoaHongThanhVien
                HoaHongThanhVien obj = new HoaHongThanhVien();
                obj.IDProducts = int.Parse(IDProducts);
                obj.IDType = int.Parse(IDType);
                obj.Type = Type;
                obj.IDThanhVien = int.Parse(IDThanhVien);
                obj.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
                obj.PhamTramHoaHong = PhamTramHoaHong;
                obj.SoCoin = SoCoin.ToString();
                obj.NgayTao = DateTime.Now;
                obj.TrangThai = 1;
                obj.NoiDung = txtnoidungthanhtoan.Text;
                obj.IDCart = Convert.ToInt64(IDMaDonTao);
                db.HoaHongThanhViens.InsertOnSubmit(obj);
                db.SubmitChanges();
                #endregion

                #region LichSuGiaoDich
                LichSuGiaoDich obl = new LichSuGiaoDich();
                obl.IDProducts = int.Parse(IDProducts);
                obl.IDType = int.Parse(IDType);
                obl.Type = Type;
                obl.IDThanhVien = int.Parse(IDThanhVien);
                obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
                obl.PhamTramHoaHong = PhamTramHoaHong;
                obl.SoCoin = SoCoin.ToString();
                obl.NgayTao = DateTime.Now;
                obl.NoiDung = txtnoidungthanhtoan.Text;
                obl.IDCart = Convert.ToInt64(IDMaDonTao);
                db.LichSuGiaoDiches.InsertOnSubmit(obl);
                db.SubmitChanges();
                #endregion

                CongTien(IDType, IDUserNguoiDuocHuong, SoCoin);
            }
        }
        void CongTien(string Type, string IDUserNguoiDuocHuong, string SoCoin)
        {
            #region Cộng điểm  theo hoa hồng coin vào bảng thành viên để tích điểm trong các trường hợp mua hàng cần
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + "");
            if (iitem != null)
            {
                if (Type == "214")// 214 la thanh toan truc tiếp thì cho vào ví thương mại luôn. ngoài
                {
                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                    double TongTienNapVao = Convert.ToDouble(SoCoin);
                    double Conglai = 0;
                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));

                    //  double ViQRCodes = Convert.ToDouble(iitem[0].ViQRCode);
                    //double ConglaiQRCode = 0;
                    //ConglaiQRCode = ((ViQRCodes) + (TongTienNapVao));
                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "   where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }
                else
                {
                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViHoaHongMuaBan);
                    double TongTienNapVao = Convert.ToDouble(SoCoin);
                    double Conglai = 0;
                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));

                    //double ViQRCodes = Convert.ToDouble(iitem[0].ViQRCode);
                    //double ConglaiQRCode = 0;
                    //ConglaiQRCode = ((ViQRCodes) + (TongTienNapVao));

                    Susers.Name_Text("update users set ViHoaHongMuaBan=" + Conglai.ToString() + "   where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }
            }
            #endregion
        }
        void ThemHoaHong_ThuongLevel(string IDProducts, string ThuTu, string IDType, string IDThanhVien, string IDUserNguoiDuocHuong, string SoCoin, string LevelThanhVienA, string LevelThanhVienB)
        {
            //string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
            //if (LogFile == "true")
            //{
            //    Library.WriteErrorLog("Người Duyệt đơn hàng : " + MoreAll.MoreAll.GetCookies("Members").ToString() + " -  Duyet thường thành viên - Sản phẩm: " + IDProducts + " - " + ThuTu + " - IDThanhVien: " + IDThanhVien + " - IDUserNguoiDuocHuong: " + IDUserNguoiDuocHuong + " - LevelThanhVienA: " + LevelThanhVienA + " - LevelThanhVienB: " + LevelThanhVienB);
            //}

            if (TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()) != "0")
            {
                #region Hoa hồng QRCode gián tiếp khi giới thiệu được hưởng sau khi lấy level A - level B
                double SoPhanTram = Convert.ToDouble(TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()));
                double TongTien = Convert.ToDouble(SoCoin);
                double ThuongLevel = (TongTien * SoPhanTram) / 100;
                ThemHoaHong(IDProducts, IDType, "Hoa hồng QRCode Level " + ThuTu, IDThanhVien, IDUserNguoiDuocHuong, SoPhanTram.ToString(), ThuongLevel.ToString(), IDMaDonTao);
                #endregion
            }
        }
        public string TinhDiemthuongGiantiep(string LevelA, string LevelB)
        {
            if (LevelA.Length > 0 && LevelB.Length > 0)
            {
                if (Convert.ToDouble(LevelA.ToString()) > Convert.ToDouble(LevelB.ToString()))
                {
                    double TLevelA = Convert.ToDouble(SetLevel(LevelA.ToString()));
                    double TLevelB = Convert.ToDouble(SetLevel(LevelB.ToString()));
                    double Tong = (TLevelA - TLevelB);
                    if (Tong != 0)
                    {
                        return Tong.ToString();
                    }
                }
            }
            return "0";
        }
        public string SetLevel(string id)
        {
            List<Entity.Menu> dt = SMenu.Name_Text("select * from Menu where capp='" + More.LV + "' and Views=" + id + " ");
            if (dt.Count > 0)
            {
                return dt[0].Noidung1.ToString();
            }
            return "0";
        }
        protected string TimLevelB(string ID)
        {
            user iitems = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(ID.ToString()));
            if (iitems != null)
            {
                return iitems.LevelThanhVien.ToString();
            }
            return "0";
        }

        #region Tìm giá trị lớn nhất trong level để thưởng cho các đời F1 đến F5
        public string MinAndMax(string c)
        {
            String intString = c.Replace("99999999999,", ""); ;//.Replace(",0", "").Replace("0,", "");
            int[] strArray = stringArrayToIntArray(intString);
            int max = strArray[0];
            if (strArray.Length > 0)
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i] > max)
                    {
                        max = strArray[i];
                    }
                }
            }
            if (max.ToString() == "0")
            {
                return "8";
            }
            else
            {
                return max.ToString();
            }
            return "8";// Nếu trong toàn bộ đều có level =0 thì gán cho nó là 8
        }
        public static int[] stringArrayToIntArray(String intString)
        {
            String[] intStringSplit = intString.Trim().Split(new char[] { ',' });
            int[] result = new int[intStringSplit.Length]; //Used to store our ints

            for (int i = 0; i < intStringSplit.Length; i++)
            {
                result[i] = int.Parse(intStringSplit[i]);
            }
            return result;
        }
        #endregion;
        public static string TimLeader(string id)
        {
            string str = "0";
            List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + " ");
            if (dt.Count > 0)
            {
                if (dt[0].Leader.ToString() == "1")
                {
                    return dt[0].iuser_id.ToString();
                }
                else
                {
                    str = dt[0].GioiThieu.ToString();
                    return TimLeader(str);
                }
            }
            return str;
        }
        protected string ShowIDChiNhanh(string id)
        {
            string str = "0";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count >= 1)
            {
                str = dt[0].Type.ToString();// chính là ID của thành viên nào đang quản lý (Bảng thành viên)
            }
            return str;
        }

    }
}