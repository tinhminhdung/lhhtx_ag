using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELoiNhuanMuaBanQRCode
    {
        private int _ID;
        private int _IDThanhVienMua;
        private int _IDThanhVienBan;
        private string _MoTa;
        private DateTime _NgayTao;
        private string _SoDiemChuyen;
        private string _SoDiemConLai;
        private string _SoDiemDaChia;
        private string _MTreeIDThanhVienMua;
        private string _MTReIDThanhVienBan;
        private int _IDMaDonTao;

        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVienMua { get { return _IDThanhVienMua; } set { _IDThanhVienMua = value; } }
        public int IDThanhVienBan { get { return _IDThanhVienBan; } set { _IDThanhVienBan = value; } }
        public string MoTa { get { return _MoTa; } set { _MoTa = value; } }
        public DateTime NgayTao { get { return _NgayTao; } set { _NgayTao = value; } }
        public string SoDiemChuyen { get { return _SoDiemChuyen; } set { _SoDiemChuyen = value; } }
        public string SoDiemConLai { get { return _SoDiemConLai; } set { _SoDiemConLai = value; } }
        public string SoDiemDaChia { get { return _SoDiemDaChia; } set { _SoDiemDaChia = value; } }
        public string MTreeIDThanhVienMua { get { return _MTreeIDThanhVienMua; } set { _MTreeIDThanhVienMua = value; } }
        public string MTReIDThanhVienBan { get { return _MTReIDThanhVienBan; } set { _MTReIDThanhVienBan = value; } }
        public int IDMaDonTao { get { return _IDMaDonTao; } set { _IDMaDonTao = value; } }
    }
}
