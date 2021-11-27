using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELichSuQRCode
    {
        private int _ID;
        private int _IDThanhVien;
        private string _ChietKhauHH;
        private string _HHNGuoiMua;
        private string _HHHeThong;
        private string _NguoiDuyet;
        private DateTime _NgayDuyet;

        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public string ChietKhauHH { get { return _ChietKhauHH; } set { _ChietKhauHH = value; } }
        public string HHNGuoiMua { get { return _HHNGuoiMua; } set { _HHNGuoiMua = value; } }
        public string HHHeThong { get { return _HHHeThong; } set { _HHHeThong = value; } }
        public string NguoiDuyet { get { return _NguoiDuyet; } set { _NguoiDuyet = value; } }
        public DateTime NgayDuyet { get { return _NgayDuyet; } set { _NgayDuyet = value; } }

    }
}
