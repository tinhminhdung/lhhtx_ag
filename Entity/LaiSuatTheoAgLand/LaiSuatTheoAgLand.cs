using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELaiSuatTheoAgLand
    {
        private int _ID;
        private long _IDThanhVienMua;
        private long _IDHuongF1;
        private long _IDDonHang;
        private string _HoaHong;
        private int _PhanTramLai;
        private DateTime _NgayTao;

        public int ID { get { return _ID; } set { _ID = value; } }
        public long IDThanhVienMua { get { return _IDThanhVienMua; } set { _IDThanhVienMua = value; } }
        public long IDHuongF1 { get { return _IDHuongF1; } set { _IDHuongF1 = value; } }
        public long IDDonHang { get { return _IDDonHang; } set { _IDDonHang = value; } }
        public string HoaHong { get { return _HoaHong; } set { _HoaHong = value; } }
        public int PhanTramLai { get { return _PhanTramLai; } set { _PhanTramLai = value; } }
        public DateTime NgayTao { get { return _NgayTao; } set { _NgayTao = value; } }
    }
}
