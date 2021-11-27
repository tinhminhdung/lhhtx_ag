using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Entity
{
    public class CartDetail
    {
        #region[Entity Private]
        private int _ID;
        private int _ID_Cart;
        private int _ipid;
        private Double _Price;
        private int _Quantity;
        private Double _Money;
        private int _Mausac;
        private int _Kichco;
        private string _Name;
        private int _GiaThanhVien;
        private Double _Diemcoin;
        private int _HoaHongTheoLevel;
        private int _IDThanhVien;
        private int _IDNhaCungCap;
        private int _TrangThaiAgLang;
        private int _TrangThaiNhaCungCap;
        private string _LyDoHuyHang;
        private int _TrangThaiNguoiMuaHang;
        private string _LyDoTraHang;
        private int _TrangThaiKhieuKien;
        private int _SentMail;
        private string _NoiDungGiaoHang;
        private int _TienTuViNao;

        private string _TongTienThanhToan;
        private string _TangThanhVienFree;
        private string _ChietKhauChoDaiLy;
        private string _TongDiemDemDiChia;
        private int _ThanhVienFree_DaiLy;// kiểu thành viên Free=1, đại lý =2
        private string _CongDiemVechoAg;
        private string _ThanhToanNCC;
        private string _ChietKhauVip;
        private int _SanPhamChienLuoc;
      
        #endregion

        #region[Properties]
        public int ID { get { return _ID; } set { _ID = value; } }
        public int ID_Cart { get { return _ID_Cart; } set { _ID_Cart = value; } }
        public int ipid { get { return _ipid; } set { _ipid = value; } }
        public Double Price { get { return _Price; } set { _Price = value; } }
        public int Quantity { get { return _Quantity; } set { _Quantity = value; } }
        public Double Money { get { return _Money; } set { _Money = value; } }
        public int Mausac { get { return _Mausac; } set { _Mausac = value; } }
        public int Kichco { get { return _Kichco; } set { _Kichco = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public int GiaThanhVien { get { return _GiaThanhVien; } set { _GiaThanhVien = value; } }
        public Double Diemcoin { get { return _Diemcoin; } set { _Diemcoin = value; } }
        public int HoaHongTheoLevel { get { return _HoaHongTheoLevel; } set { _HoaHongTheoLevel = value; } }
        public int IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public int IDNhaCungCap { get { return _IDNhaCungCap; } set { _IDNhaCungCap = value; } }
        public int TrangThaiAgLang { get { return _TrangThaiAgLang; } set { _TrangThaiAgLang = value; } }
        public int TrangThaiNhaCungCap { get { return _TrangThaiNhaCungCap; } set { _TrangThaiNhaCungCap = value; } }
        public string LyDoHuyHang { get { return _LyDoHuyHang; } set { _LyDoHuyHang = value; } }
        public int TrangThaiNguoiMuaHang { get { return _TrangThaiNguoiMuaHang; } set { _TrangThaiNguoiMuaHang = value; } }
        public string LyDoTraHang { get { return _LyDoTraHang; } set { _LyDoTraHang = value; } }
        public int TrangThaiKhieuKien { get { return _TrangThaiKhieuKien; } set { _TrangThaiKhieuKien = value; } }
        public int SentMail { get { return _SentMail; } set { _SentMail = value; } }
        public string NoiDungGiaoHang { get { return _NoiDungGiaoHang; } set { _NoiDungGiaoHang = value; } }
        public int TienTuViNao { get { return _TienTuViNao; } set { _TienTuViNao = value; } }

        public string TongTienThanhToan { get { return _TongTienThanhToan; } set { _TongTienThanhToan = value; } }
        public string TangThanhVienFree { get { return _TangThanhVienFree; } set { _TangThanhVienFree = value; } }
        public string ChietKhauChoDaiLy { get { return _ChietKhauChoDaiLy; } set { _ChietKhauChoDaiLy = value; } }
        public string TongDiemDemDiChia { get { return _TongDiemDemDiChia; } set { _TongDiemDemDiChia = value; } }
        public int ThanhVienFree_DaiLy { get { return _ThanhVienFree_DaiLy; } set { _ThanhVienFree_DaiLy = value; } }
        public string CongDiemVechoAg { get { return _CongDiemVechoAg; } set { _CongDiemVechoAg = value; } }
        public string ThanhToanNCC { get { return _ThanhToanNCC; } set { _ThanhToanNCC = value; } }
        public string ChietKhauVip { get { return _ChietKhauVip; } set { _ChietKhauVip = value; } }
        public int SanPhamChienLuoc { get { return _SanPhamChienLuoc; } set { _SanPhamChienLuoc = value; } }
       
        
        #endregion
    }
}