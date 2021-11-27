using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class EViTamMuaHang
    {
        private int _ID;
        private int _IDCarts;
        private int _IDCartDetail;
        private int _IDSanPham;
        private int _IDThanhVienMua;
        private int _IDNhaCungCap;
        private string _SoTienNhaCCSeNhan;
        private string _SoTienNguoiMuaBiTru;
        private string _SoDiemThuong;
        private DateTime _NgayCapNhat;
        private int _LayTienOVi;

        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDCarts { get { return _IDCarts; } set { _IDCarts = value; } }
        public int IDCartDetail { get { return _IDCartDetail; } set { _IDCartDetail = value; } }
        public int IDSanPham { get { return _IDSanPham; } set { _IDSanPham = value; } }
        public int IDThanhVienMua { get { return _IDThanhVienMua; } set { _IDThanhVienMua = value; } }
        public int IDNhaCungCap { get { return _IDNhaCungCap; } set { _IDNhaCungCap = value; } }
        public string SoTienNhaCCSeNhan { get { return _SoTienNhaCCSeNhan; } set { _SoTienNhaCCSeNhan = value; } }
        public string SoTienNguoiMuaBiTru { get { return _SoTienNguoiMuaBiTru; } set { _SoTienNguoiMuaBiTru = value; } }
        public string SoDiemThuong { get { return _SoDiemThuong; } set { _SoDiemThuong = value; } }
        public DateTime NgayCapNhat { get { return _NgayCapNhat; } set { _NgayCapNhat = value; } }
        public int LayTienOVi { get { return _LayTienOVi; } set { _LayTienOVi = value; } }
    }
}
