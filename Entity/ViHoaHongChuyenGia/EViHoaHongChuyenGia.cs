using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class EViHoaHongChuyenGia
    {
        private int _ID;
        private long _IDDonHang;
        private long _IDThanhVien;
        private long _IDThanhVienMua_KichHoat;
        private string _TongDiem;
        private string _MoTa;
        private int _LoaiHoaHong;
        private DateTime _NgayTao;
        private int _PhanTram;


        public int ID { get { return _ID; } set { _ID = value; } }
        public long IDDonHang { get { return _IDDonHang; } set { _IDDonHang = value; } }
        public long IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public long IDThanhVienMua_KichHoat { get { return _IDThanhVienMua_KichHoat; } set { _IDThanhVienMua_KichHoat = value; } }
        public string TongDiem { get { return _TongDiem; } set { _TongDiem = value; } }
        public string MoTa { get { return _MoTa; } set { _MoTa = value; } }
        public int LoaiHoaHong { get { return _LoaiHoaHong; } set { _LoaiHoaHong = value; } }
        public DateTime NgayTao { get { return _NgayTao; } set { _NgayTao = value; } }
        public int PhanTram { get { return _PhanTram; } set { _PhanTram = value; } }
        
    }
}
