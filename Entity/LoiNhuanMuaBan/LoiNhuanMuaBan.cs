using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELoiNhuanMuaBan
    {
        private int _ID;
        private int _IDThanhVienMua;
        private int _IDThanhVienBan;
        private int _IDDonHang;
        private int _IDSanPham;
        private string _MoTa;
        private DateTime _NgayTao;
        private string _SoDiemGoc;
        private string _SoDiemConLai;
        private string _SoDiemDaChia;

        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVienMua { get { return _IDThanhVienMua; } set { _IDThanhVienMua = value; } }
        public int IDThanhVienBan { get { return _IDThanhVienBan; } set { _IDThanhVienBan = value; } }
        public int IDDonHang { get { return _IDDonHang; } set { _IDDonHang = value; } }
        public int IDSanPham { get { return _IDSanPham; } set { _IDSanPham = value; } }
        public string MoTa { get { return _MoTa; } set { _MoTa = value; } }
        public DateTime NgayTao { get { return _NgayTao; } set { _NgayTao = value; } }
        public string SoDiemGoc { get { return _SoDiemGoc; } set { _SoDiemGoc = value; } }
        public string SoDiemConLai { get { return _SoDiemConLai; } set { _SoDiemConLai = value; } }
        public string SoDiemDaChia { get { return _SoDiemDaChia; } set { _SoDiemDaChia = value; } }
    }
}
