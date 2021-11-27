using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELoiNhuanDangKyThanhVien
    {
        private int _ID;
        private int _IDThanhVienDangKy;
        private int _IDThanhVienGioiThieu;
        private string _MoTa;
        private DateTime _NgayTao;
        private string _SoDiemNapVao;
        private string _SoDiemConLai;
        private string _SoDiemDaChia;
        private string _MTreeIDThanhVienDangKy;
        private string _MTReIDThanhVienGioiThieu;
        private long _IDMaDonTao;
        private int _IDChiNhanh;
        private int _IDLeader;

        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVienDangKy { get { return _IDThanhVienDangKy; } set { _IDThanhVienDangKy = value; } }
        public int IDThanhVienGioiThieu { get { return _IDThanhVienGioiThieu; } set { _IDThanhVienGioiThieu = value; } }
        public string MoTa { get { return _MoTa; } set { _MoTa = value; } }
        public DateTime NgayTao { get { return _NgayTao; } set { _NgayTao = value; } }
        public string SoDiemNapVao { get { return _SoDiemNapVao; } set { _SoDiemNapVao = value; } }
        public string SoDiemConLai { get { return _SoDiemConLai; } set { _SoDiemConLai = value; } }
        public string SoDiemDaChia { get { return _SoDiemDaChia; } set { _SoDiemDaChia = value; } }
        public string MTreeIDThanhVienDangKy { get { return _MTreeIDThanhVienDangKy; } set { _MTreeIDThanhVienDangKy = value; } }
        public string MTReIDThanhVienGioiThieu { get { return _MTReIDThanhVienGioiThieu; } set { _MTReIDThanhVienGioiThieu = value; } }
        public long IDMaDonTao { get { return _IDMaDonTao; } set { _IDMaDonTao = value; } }
        public int IDChiNhanh { get { return _IDChiNhanh; } set { _IDChiNhanh = value; } }
        public int IDLeader { get { return _IDLeader; } set { _IDLeader = value; } }
    }
}
