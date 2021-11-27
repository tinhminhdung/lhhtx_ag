using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class EChuyenDiemSangVi_Thue
    {
        private int _ID;
        private int _IDThanhVien;
        private string _MTree;
        private string _SoDiemViHoaHong;
        private int _PhanTramThue;
        private string _SoDienBiTru;
        private string _SoDiemSauKhiTru;
        private string _SoDiemViChinhTruocKhiCongSang;
        private DateTime _NgayGiaoDich;
        private int _LoaiVi;
        private string _ViMuaHang;
        private string _ViThuongMai;
        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public string MTree { get { return _MTree; } set { _MTree = value; } }
        public string SoDiemViHoaHong { get { return _SoDiemViHoaHong; } set { _SoDiemViHoaHong = value; } }
        public int PhanTramThue { get { return _PhanTramThue; } set { _PhanTramThue = value; } }
        public string SoDienBiTru { get { return _SoDienBiTru; } set { _SoDienBiTru = value; } }
        public string SoDiemSauKhiTru { get { return _SoDiemSauKhiTru; } set { _SoDiemSauKhiTru = value; } }
        public string SoDiemViChinhTruocKhiCongSang { get { return _SoDiemViChinhTruocKhiCongSang; } set { _SoDiemViChinhTruocKhiCongSang = value; } }
        public DateTime NgayGiaoDich { get { return _NgayGiaoDich; } set { _NgayGiaoDich = value; } }
        public int LoaiVi { get { return _LoaiVi; } set { _LoaiVi = value; } }
        public string ViMuaHang { get { return _ViMuaHang; } set { _ViMuaHang = value; } }
        public string ViThuongMai { get { return _ViThuongMai; } set { _ViThuongMai = value; } }
    }
}
