using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class EMuaDiemThanhVien
    {
        private int _ID;
        private int _IDThanhVien;
        private int _SoDiemCanMua;
        private DateTime _NgayGui;
        private string _NgayDuyet;
        private string _NguoiDuyet;
        private string _GhiChu;
        private int _TrangThai;

        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public int SoDiemCanMua { get { return _SoDiemCanMua; } set { _SoDiemCanMua = value; } }
        public string GhiChu { get { return _GhiChu; } set { _GhiChu = value; } }
        public int TrangThai { get { return _TrangThai; } set { _TrangThai = value; } }
        public DateTime NgayGui { get { return _NgayGui; } set { _NgayGui = value; } }
        public string NgayDuyet { get { return _NgayDuyet; } set { _NgayDuyet = value; } }
        public string NguoiDuyet { get { return _NguoiDuyet; } set { _NguoiDuyet = value; } }
    }
}
