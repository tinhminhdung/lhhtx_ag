using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELichSuDuyetViTamGiu
    {
        private int _ID;
        private int _IDThanhVienMua;
        private int _IDThanhVienNhaCC;
        private string _SoDiemThanhToan;
        private int _IDCarts;
        private int _IDCartDetail;
        private int _IDSanPham;
        private DateTime _NgayDuyet;
        private string _NoiDung;
        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVienMua { get { return _IDThanhVienMua; } set { _IDThanhVienMua = value; } }
        public int IDThanhVienNhaCC { get { return _IDThanhVienNhaCC; } set { _IDThanhVienNhaCC = value; } }
        public string SoDiemThanhToan { get { return _SoDiemThanhToan; } set { _SoDiemThanhToan = value; } }
        public int IDCarts { get { return _IDCarts; } set { _IDCarts = value; } }
        public int IDCartDetail { get { return _IDCartDetail; } set { _IDCartDetail = value; } }
        public int IDSanPham { get { return _IDSanPham; } set { _IDSanPham = value; } }
        public DateTime NgayDuyet { get { return _NgayDuyet; } set { _NgayDuyet = value; } }
        public string NoiDung { get { return _NoiDung; } set { _NoiDung = value; } }
    }
}
