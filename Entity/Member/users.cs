using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class users
    {
        #region[Entity Private]
        private int _iuser_id;
        private string _vuserun;
        private string _vuserpwd;
        private string _vfname;
        private string _vlname;
        private int _igender;
        private DateTime _dbirthday;
        private string _vidcard;
        private string _vaddress;
        private string _vphone;
        private string _vemail;
        private int _iregionid;
        private string _vavatar;
        private string _vavatartitle;
        private DateTime _dcreatedate;
        private DateTime _dlastvisited;
        private string _vvalidatekey;
        private int _istatus;
        private string _lang;
        private int _Type;
        private int _IDChiNhanh;
        private int _ChiNhanh;
        private string _GioiThieu;
        private int _DuyetTienDanap;
        private string _TongTienDanapVND;
        private string _TongTienDanapCoin;
        private int _LevelThanhVien;
        private int _Leader;
        private string _TongTienCoinDuocCap;
        private string _MTree;
        private string _ViTienHHGioiThieu;
        private int _HoTro;
        private string _VIAAFFILIATE;
        private string _ViAgLang;
        private int _ThanhVienAgLang;
        private string _TienDangSoHuuBatDongSan;
        private int _Uutien;
        private string _ViUuTien;

        private string _ViQRCode;
        private int _TrangThaiThamGiaQRCode;
        private string _AnhQRCode;
        private string _QRCodeChietKhauHH;
        private string _QRCodeHHNguoiMua;
        private string _QRCodeHHHeThong;

        private string _SoChungMinhThu;
        private string _NoiCapChungMinhThu;
        private string _NgayCapChungMinhThu;
        private int _LoaiChungMinh;
        private string _GiayPhepKinhDoanh;
        private int _TrangThaiNhaCungCap;
        private int _TongSoSanPham;
        private int _ToiDongYCamKet;
        private string _TenShop;
        private string _DiaChiKhoHang;
        private string _AnhChungMinhThuTruoc;
        private string _AnhChungMinhThuSau;
        public string _MaSoDoanhNghiep;
        public string _ViHoaHongMuaBan;
        public string _ViHoaHongAFF;
        public int _CauHinhDuyetDonTuDong;
        public long _TongSoSanPhamDaBan;
        public string _ViMuaHangAFF;
        public string _ViFMotAnTheoAgland;
        public string _ViTangTienVip;
        public int _TinhThanh;
        public int _CuaHang;
        public int _TatChucNang;
        public int _TrangThaiThongBao;

        private int _DaBanDuocSanPham;
        private string _IDLuuTam;
        private string _TongTienDaMua;
        #endregion

        #region[Properties]
        public int iuser_id { get { return _iuser_id; } set { _iuser_id = value; } }
        public string vuserun { get { return _vuserun; } set { _vuserun = value; } }
        public string vuserpwd { get { return _vuserpwd; } set { _vuserpwd = value; } }
        public string vfname { get { return _vfname; } set { _vfname = value; } }
        public string vlname { get { return _vlname; } set { _vlname = value; } }
        public int igender { get { return _igender; } set { _igender = value; } }
        public DateTime dbirthday { get { return _dbirthday; } set { _dbirthday = value; } }
        public string vidcard { get { return _vidcard; } set { _vidcard = value; } }
        public string vaddress { get { return _vaddress; } set { _vaddress = value; } }
        public string vphone { get { return _vphone; } set { _vphone = value; } }
        public string vemail { get { return _vemail; } set { _vemail = value; } }
        public int iregionid { get { return _iregionid; } set { _iregionid = value; } }
        public string vavatar { get { return _vavatar; } set { _vavatar = value; } }
        public string vavatartitle { get { return _vavatartitle; } set { _vavatartitle = value; } }
        public DateTime dcreatedate { get { return _dcreatedate; } set { _dcreatedate = value; } }
        public DateTime dlastvisited { get { return _dlastvisited; } set { _dlastvisited = value; } }
        public string vvalidatekey { get { return _vvalidatekey; } set { _vvalidatekey = value; } }
        public int istatus { get { return _istatus; } set { _istatus = value; } }
        public string lang { get { return _lang; } set { _lang = value; } }
        public int Type { get { return _Type; } set { _Type = value; } }
        public int IDChiNhanh { get { return _IDChiNhanh; } set { _IDChiNhanh = value; } }
        public int ChiNhanh { get { return _ChiNhanh; } set { _ChiNhanh = value; } }
        public string GioiThieu { get { return _GioiThieu; } set { _GioiThieu = value; } }
        public int DuyetTienDanap { get { return _DuyetTienDanap; } set { _DuyetTienDanap = value; } }
        public string TongTienDanapVND { get { return _TongTienDanapVND; } set { _TongTienDanapVND = value; } }
        public string TongTienDanapCoin { get { return _TongTienDanapCoin; } set { _TongTienDanapCoin = value; } }
        public int LevelThanhVien { get { return _LevelThanhVien; } set { _LevelThanhVien = value; } }
        public int Leader { get { return _Leader; } set { _Leader = value; } }
        public string TongTienCoinDuocCap { get { return _TongTienCoinDuocCap; } set { _TongTienCoinDuocCap = value; } }
        public string MTree { get { return _MTree; } set { _MTree = value; } }
        public string ViTienHHGioiThieu { get { return _ViTienHHGioiThieu; } set { _ViTienHHGioiThieu = value; } }
        public int HoTro { get { return _HoTro; } set { _HoTro = value; } }
        public string VIAAFFILIATE { get { return _VIAAFFILIATE; } set { _VIAAFFILIATE = value; } }
        public string ViAgLang { get { return _ViAgLang; } set { _ViAgLang = value; } }
        public int ThanhVienAgLang { get { return _ThanhVienAgLang; } set { _ThanhVienAgLang = value; } }
        public string TienDangSoHuuBatDongSan { get { return _TienDangSoHuuBatDongSan; } set { _TienDangSoHuuBatDongSan = value; } }
        public int Uutien { get { return _Uutien; } set { _Uutien = value; } }
        public string ViUuTien { get { return _ViUuTien; } set { _ViUuTien = value; } }
        public string ViQRCode { get { return _ViQRCode; } set { _ViQRCode = value; } }
        public int TrangThaiThamGiaQRCode { get { return _TrangThaiThamGiaQRCode; } set { _TrangThaiThamGiaQRCode = value; } }

        public string AnhQRCode { get { return _AnhQRCode; } set { _AnhQRCode = value; } }
        public string QRCodeChietKhauHH { get { return _QRCodeChietKhauHH; } set { _QRCodeChietKhauHH = value; } }
        public string QRCodeHHNguoiMua { get { return _QRCodeHHNguoiMua; } set { _QRCodeHHNguoiMua = value; } }
        public string QRCodeHHHeThong { get { return _QRCodeHHHeThong; } set { _QRCodeHHHeThong = value; } }

        public string SoChungMinhThu { get { return _SoChungMinhThu; } set { _SoChungMinhThu = value; } }
        public string NoiCapChungMinhThu { get { return _NoiCapChungMinhThu; } set { _NoiCapChungMinhThu = value; } }
        public string NgayCapChungMinhThu { get { return _NgayCapChungMinhThu; } set { _NgayCapChungMinhThu = value; } }
        public int LoaiChungMinh { get { return _LoaiChungMinh; } set { _LoaiChungMinh = value; } }
        public string GiayPhepKinhDoanh { get { return _GiayPhepKinhDoanh; } set { _GiayPhepKinhDoanh = value; } }
        public int TrangThaiNhaCungCap { get { return _TrangThaiNhaCungCap; } set { _TrangThaiNhaCungCap = value; } }
        public int TongSoSanPham { get { return _TongSoSanPham; } set { _TongSoSanPham = value; } }
        public int ToiDongYCamKet { get { return _ToiDongYCamKet; } set { _ToiDongYCamKet = value; } }

        public string TenShop { get { return _TenShop; } set { _TenShop = value; } }
        public string DiaChiKhoHang { get { return _DiaChiKhoHang; } set { _DiaChiKhoHang = value; } }
        public string AnhChungMinhThuTruoc { get { return _AnhChungMinhThuTruoc; } set { _AnhChungMinhThuTruoc = value; } }
        public string AnhChungMinhThuSau { get { return _AnhChungMinhThuSau; } set { _AnhChungMinhThuSau = value; } }
        public string MaSoDoanhNghiep { get { return _MaSoDoanhNghiep; } set { _MaSoDoanhNghiep = value; } }
        public string ViHoaHongMuaBan { get { return _ViHoaHongMuaBan; } set { _ViHoaHongMuaBan = value; } }
        public string ViHoaHongAFF { get { return _ViHoaHongAFF; } set { _ViHoaHongAFF = value; } }
        public int CauHinhDuyetDonTuDong { get { return _CauHinhDuyetDonTuDong; } set { _CauHinhDuyetDonTuDong = value; } }
        public long TongSoSanPhamDaBan { get { return _TongSoSanPhamDaBan; } set { _TongSoSanPhamDaBan = value; } }
        public string ViMuaHangAFF { get { return _ViMuaHangAFF; } set { _ViMuaHangAFF = value; } }
        public string ViFMotAnTheoAgland { get { return _ViFMotAnTheoAgland; } set { _ViFMotAnTheoAgland = value; } }
        public string ViTangTienVip { get { return _ViTangTienVip; } set { _ViTangTienVip = value; } }
        public int TinhThanh { get { return _TinhThanh; } set { _TinhThanh = value; } }
        public int CuaHang { get { return _CuaHang; } set { _CuaHang = value; } }
        public int TatChucNang { get { return _TatChucNang; } set { _TatChucNang = value; } }
        public int TrangThaiThongBao { get { return _TrangThaiThongBao; } set { _TrangThaiThongBao = value; } }
        public int DaBanDuocSanPham { get { return _DaBanDuocSanPham; } set { _DaBanDuocSanPham = value; } }
        public string IDLuuTam { get { return _IDLuuTam; } set { _IDLuuTam = value; } }
        public string TongTienDaMua { get { return _TongTienDaMua; } set { _TongTienDaMua = value; } }
        #endregion
    }

    public class TongSo
    {
        private int _Tong;
        public int Tong { get { return _Tong; } set { _Tong = value; } }
    }
}
