using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELaiSuatAGLANG
    {
        private int _ID;
        private int _IDSanPham;
        private int _IDThanhVienBan;
        private int _IDThanhVienHuongHH;
        private string _LaiSuat;
        private DateTime _NgayNhan;
        private string _SoTienDauTu;
        private string _NguoiDuyet;
        private int _TrangThai;
        private int _KieuPhatSinhGiaoDich;
        private int _KieuLaiSuat;

        private DateTime _NgayThamGia;
        private int _IDCart;
        private string _NoiDung;
        private long _IDGioiThieuTrucTiep;
        private string _MTreeHuong;


        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDSanPham { get { return _IDSanPham; } set { _IDSanPham = value; } }
        public int IDThanhVienBan { get { return _IDThanhVienBan; } set { _IDThanhVienBan = value; } }
        public int IDThanhVienHuongHH { get { return _IDThanhVienHuongHH; } set { _IDThanhVienHuongHH = value; } }
        public string LaiSuat { get { return _LaiSuat; } set { _LaiSuat = value; } }
        public DateTime NgayNhan { get { return _NgayNhan; } set { _NgayNhan = value; } }
        public string SoTienDauTu { get { return _SoTienDauTu; } set { _SoTienDauTu = value; } }
        public string NguoiDuyet { get { return _NguoiDuyet; } set { _NguoiDuyet = value; } }
        public int TrangThai { get { return _TrangThai; } set { _TrangThai = value; } }
        public int KieuPhatSinhGiaoDich { get { return _KieuPhatSinhGiaoDich; } set { _KieuPhatSinhGiaoDich = value; } }
        public int KieuLaiSuat { get { return _KieuLaiSuat; } set { _KieuLaiSuat = value; } }
        public int IDCart { get { return _IDCart; } set { _IDCart = value; } }
        public DateTime NgayThamGia { get { return _NgayThamGia; } set { _NgayThamGia = value; } }
        public string NoiDung { get { return _NoiDung; } set { _NoiDung = value; } }
        public long IDGioiThieuTrucTiep { get { return _IDGioiThieuTrucTiep; } set { _IDGioiThieuTrucTiep = value; } }
        public string MTreeHuong { get { return _MTreeHuong; } set { _MTreeHuong = value; } }

    }
}
