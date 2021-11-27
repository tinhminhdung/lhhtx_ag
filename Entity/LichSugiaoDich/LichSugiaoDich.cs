using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class ELichSuGiaoDich
    {
        private int _ID;
        private int _IDProducts;
        private int _IDType;
        private string _Type;
        private int _IDThanhVien;
        private int _IDUserNguoiDuocHuong;
        private string _PhamTramHoaHong;
        private string _SoCoin;
        private DateTime _NgayTao;
        private string _NoiDung;
        private long _IDCart;

        public int ID { get { return _ID; } set { _ID = value; } }
        public int IDProducts { get { return _IDProducts; } set { _IDProducts = value; } }
        public int IDType { get { return _IDType; } set { _IDType = value; } }
        public string Type { get { return _Type; } set { _Type = value; } }
        public int IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public int IDUserNguoiDuocHuong { get { return _IDUserNguoiDuocHuong; } set { _IDUserNguoiDuocHuong = value; } }
        public string PhamTramHoaHong { get { return _PhamTramHoaHong; } set { _PhamTramHoaHong = value; } }
        public string SoCoin { get { return _SoCoin; } set { _SoCoin = value; } }
        public DateTime NgayTao { get { return _NgayTao; } set { _NgayTao = value; } }
        public string NoiDung { get { return _NoiDung; } set { _NoiDung = value; } }
        public long IDCart { get { return _IDCart; } set { _IDCart = value; } }

    }
}
