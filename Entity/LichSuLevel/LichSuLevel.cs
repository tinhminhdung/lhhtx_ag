using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELichSuLevel
    {
        private int _ID;
        private int _IDThanhVien;
        private DateTime _NgayLenCap;
        private int _CapLevel;
        private string _NguoiTao;
        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public DateTime NgayLenCap { get { return _NgayLenCap; } set { _NgayLenCap = value; } }
        public int CapLevel { get { return _CapLevel; } set { _CapLevel = value; } }
        public string NguoiTao { get { return _NguoiTao; } set { _NguoiTao = value; } }
    }
}
