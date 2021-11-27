using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Entity
{
    public class Products
    {
        #region[Entity Private]
        private int _ipid;
        private int _icid;
        private int _ID_Hang;
        private string _Code;
        private string _Name;
        private string _Brief;
        private string _Contents;
        private string _search;
        private string _Images;
        private string _ImagesSmall;
        private int _Equals;
        private int _Quantity;
        private string _Price;
        private string _OldPrice;
        private int _Chekdata;
        private DateTime _Create_Date;
        private DateTime _Modified_Date;
        private int _Views;
        private string _lang;
        private int _News;
        private int _Home;
        private int _Check_01;
        private int _Check_02;
        private int _Check_03;
        private int _Check_04;
        private int _Check_05;
        private int _Status;
        private string _Titleseo;
        private string _Meta;
        private string _Keyword;
        private string _Anh;
        private int _sanxuat;
        private string _TangName;
        private string _Noidung1;
        private string _Noidung2;
        private string _Noidung3;
        private string _Noidung4;
        private string _Noidung5;
        private int _RateCount;
        private int _RateSum;
        private string _Alt;
        private int _IDThanhVien;
        private int _DiemMuaHang;
        private string _GiaThanhVien;
        private string _Giacongtynhapvao;
        private int _TrangThaiAgLang;
        private int _Phaply;
        private int _SanPhamAg;
        private string _TrongLuong;
        // private string _GiaThanhVienFree;
        private string _GiaThanhVienFree;
        private string _GiaChietKhauDaiLy;
        private string _ChietKhau;
        private int _PhanTramChietKhauDaiLy;
        private int _PhanTramChietKhauThanhVien;
        private int _KichHoatDaiLy;
        private string _GiaCuaHang;
        #endregion

        #region[Properties]
        public int ipid { get { return _ipid; } set { _ipid = value; } }
        public int icid { get { return _icid; } set { _icid = value; } }
        public int ID_Hang { get { return _ID_Hang; } set { _ID_Hang = value; } }
        public string Code { get { return _Code; } set { _Code = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Brief { get { return _Brief; } set { _Brief = value; } }
        public string Contents { get { return _Contents; } set { _Contents = value; } }
        public string search { get { return _search; } set { _search = value; } }
        public string Images { get { return _Images; } set { _Images = value; } }
        public string ImagesSmall { get { return _ImagesSmall; } set { _ImagesSmall = value; } }
        public int Equals { get { return _Equals; } set { _Equals = value; } }
        public int Quantity { get { return _Quantity; } set { _Quantity = value; } }
        public string Price { get { return _Price; } set { _Price = value; } }
        public string OldPrice { get { return _OldPrice; } set { _OldPrice = value; } }
        public int Chekdata { get { return _Chekdata; } set { _Chekdata = value; } }
        public DateTime Create_Date { get { return _Create_Date; } set { _Create_Date = value; } }
        public DateTime Modified_Date { get { return _Modified_Date; } set { _Modified_Date = value; } }
        public int Views { get { return _Views; } set { _Views = value; } }
        public string lang { get { return _lang; } set { _lang = value; } }
        public int News { get { return _News; } set { _News = value; } }
        public int Home { get { return _Home; } set { _Home = value; } }
        public int Check_01 { get { return _Check_01; } set { _Check_01 = value; } }
        public int Check_02 { get { return _Check_02; } set { _Check_02 = value; } }
        public int Check_03 { get { return _Check_03; } set { _Check_03 = value; } }
        public int Check_04 { get { return _Check_04; } set { _Check_04 = value; } }
        public int Check_05 { get { return _Check_05; } set { _Check_05 = value; } }
        public int Status { get { return _Status; } set { _Status = value; } }
        public string Titleseo { get { return _Titleseo; } set { _Titleseo = value; } }
        public string Meta { get { return _Meta; } set { _Meta = value; } }
        public string Keyword { get { return _Keyword; } set { _Keyword = value; } }
        public string Anh { get { return _Anh; } set { _Anh = value; } }
        public int sanxuat { get { return _sanxuat; } set { _sanxuat = value; } }
        public string TangName { get { return _TangName; } set { _TangName = value; } }
        public string Noidung1 { get { return _Noidung1; } set { _Noidung1 = value; } }
        public string Noidung2 { get { return _Noidung2; } set { _Noidung2 = value; } }
        public string Noidung3 { get { return _Noidung3; } set { _Noidung3 = value; } }
        public string Noidung4 { get { return _Noidung4; } set { _Noidung4 = value; } }
        public string Noidung5 { get { return _Noidung5; } set { _Noidung5 = value; } }
        public int RateCount { get { return _RateCount; } set { _RateCount = value; } }
        public int RateSum { get { return _RateSum; } set { _RateSum = value; } }
        public string Alt { get { return _Alt; } set { _Alt = value; } }
        public int IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public int DiemMuaHang { get { return _DiemMuaHang; } set { _DiemMuaHang = value; } }
        public string GiaThanhVien { get { return _GiaThanhVien; } set { _GiaThanhVien = value; } }
        public string Giacongtynhapvao { get { return _Giacongtynhapvao; } set { _Giacongtynhapvao = value; } }
        public int TrangThaiAgLang { get { return _TrangThaiAgLang; } set { _TrangThaiAgLang = value; } }
        public int Phaply { get { return _Phaply; } set { _Phaply = value; } }
        public int SanPhamAg { get { return _SanPhamAg; } set { _SanPhamAg = value; } }
        public string TrongLuong { get { return _TrongLuong; } set { _TrongLuong = value; } }
        public string GiaThanhVienFree { get { return _GiaThanhVienFree; } set { _GiaThanhVienFree = value; } }
        public string GiaChietKhauDaiLy { get { return _GiaChietKhauDaiLy; } set { _GiaChietKhauDaiLy = value; } }
        public string ChietKhau { get { return _ChietKhau; } set { _ChietKhau = value; } }
        public int PhanTramChietKhauDaiLy { get { return _PhanTramChietKhauDaiLy; } set { _PhanTramChietKhauDaiLy = value; } }
        public int PhanTramChietKhauThanhVien { get { return _PhanTramChietKhauThanhVien; } set { _PhanTramChietKhauThanhVien = value; } }
        public int KichHoatDaiLy { get { return _KichHoatDaiLy; } set { _KichHoatDaiLy = value; } }
        public string GiaCuaHang { get { return _GiaCuaHang; } set { _GiaCuaHang = value; } }

        #endregion

    }
    public class Product_Count
    {
        public int ipid { get; set; }
    }
    public class Category_Product
    {
        public int icid { get; set; }
        public string Brief { get; set; }
        public string Code { get; set; }
        public DateTime Create_Date { get; set; }
        public int ID_Hang { get; set; }
        public string Images { get; set; }
        public string ImagesSmall { get; set; }
        public int ipid { get; set; }
        public string Name { get; set; }
        public string OldPrice { get; set; }
        public string Price { get; set; }
        public int sanxuat { get; set; }
        public string TangName { get; set; }
        public string Alt { get; set; }
        public int IDThanhVien { get; set; }
        public int DiemMuaHang { get; set; }
        public string GiaThanhVien { get; set; }
        public string Giacongtynhapvao { get; set; }
        //public string GiaThanhVienFree { get; set; }
        public string GiaThanhVienFree { get; set; }
        public string GiaChietKhauDaiLy { get; set; }
        public string ChietKhau { get; set; }
        public int PhanTramChietKhauDaiLy { get; set; }
        public int PhanTramChietKhauThanhVien { get; set; }
        public int KichHoatDaiLy { get; set; }
        public string GiaCuaHang { get; set; }
        
    }

    public class ProductDISTINCT
    {
        public int IDThanhVien { get; set; }
    }

    //public class Category_Product
    //{
    //    #region[Entity Private]
    //    private int _ipid;
    //    private int _icid;
    //    private int _ID_Hang;
    //    private string _Code;
    //    private string _Name;
    //    private string _Brief;
    //    private string _Images;
    //    private string _ImagesSmall;
    //    private DateTime _Create_Date;
    //    private string _Price;
    //    private string _OldPrice;
    //    private int _sanxuat;
    //    private string _TangName;
    //    #endregion

    //    #region[Properties]
    //    public int ipid { get { return _ipid; } set { _ipid = value; } }
    //    public int icid { get { return _icid; } set { _icid = value; } }
    //    public int ID_Hang { get { return _ID_Hang; } set { _ID_Hang = value; } }
    //    public string Code { get { return _Code; } set { _Code = value; } }
    //    public string Name { get { return _Name; } set { _Name = value; } }
    //    public string Brief { get { return _Brief; } set { _Brief = value; } }
    //    public DateTime Create_Date { get { return _Create_Date; } set { _Create_Date = value; } }
    //    public string Images { get { return _Images; } set { _Images = value; } }
    //    public string ImagesSmall { get { return _ImagesSmall; } set { _ImagesSmall = value; } }
    //    public string Price { get { return _Price; } set { _Price = value; } }
    //    public string OldPrice { get { return _OldPrice; } set { _OldPrice = value; } }
    //    public string TangName { get { return _TangName; } set { _TangName = value; } }
    //    public int sanxuat { get { return _sanxuat; } set { _sanxuat = value; } }

    //    #endregion

    //}
}