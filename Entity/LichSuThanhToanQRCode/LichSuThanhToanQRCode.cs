using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELichSuThanhToanQRCode
    {
        private int _ID;
        private int _IDThanhVien;
        private int _IDThanhVienNhan;
        private string _SoDiemThanhToan;
        private DateTime _NgayDuyet;
        private string _NoiDung;
        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public int IDThanhVienNhan { get { return _IDThanhVienNhan; } set { _IDThanhVienNhan = value; } }
        public string SoDiemThanhToan { get { return _SoDiemThanhToan; } set { _SoDiemThanhToan = value; } }
        public DateTime NgayDuyet { get { return _NgayDuyet; } set { _NgayDuyet = value; } }
        public string NoiDung { get { return _NoiDung; } set { _NoiDung = value; } }

    }
}
