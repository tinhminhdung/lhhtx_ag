using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TestWindowService;
using VS.E_Commerce;

public class MDoanhSoDongHuong
{
    public static void DoanhSoDongHuongKichHoatThanhVien(string TongDiem, string IDThanhVien, string IDCart, string NoiDung)
    {
        double HH = Convert.ToDouble(Commond.Setting("txtdoanhsodonghuongDangKy"));
        double TongDiems = Convert.ToDouble(TongDiem);
        double TienHH = Convert.ToDouble(TongDiems * HH) / 100;

        DatalinqDataContext db = new DatalinqDataContext();
        DoanhSoDongHuong obj = new DoanhSoDongHuong();
        obj.IDProducts = Convert.ToInt32("0");
        obj.IDType = Convert.ToInt32("1");// Kiểu 1 kích hoạt 480// kiểu 2 là mua bán
        obj.Type = "Kích Hoạt Thành Viên";
        obj.IDThanhVien = Convert.ToInt32(IDThanhVien);
        obj.PhamTramHoaHong = Convert.ToInt32(HH);
        obj.TongDiem = TienHH.ToString();
        obj.NgayTao = DateTime.Now;
        obj.TrangThai = 1;
        obj.NoiDung = NoiDung;
        obj.IDCart = Convert.ToInt64(IDCart);
        db.DoanhSoDongHuongs.InsertOnSubmit(obj);
        db.SubmitChanges();
    }

    public static void DoanhSoDongHuongMuaBan(string TongDiem, string IDProducts, string IDThanhVien, string IDCart, string NoiDung)
    {
        double HH = Convert.ToDouble(Commond.Setting("txtdoanhsodonghuongmuaban"));
       // double TongDiems = Convert.ToDouble(TongDiem);
       // double TienHH = Convert.ToDouble(TongDiems * HH) / 100;

        DatalinqDataContext db = new DatalinqDataContext();
        DoanhSoDongHuong obj = new DoanhSoDongHuong();
        obj.IDProducts = Convert.ToInt32(IDProducts);
        obj.IDType = Convert.ToInt32("2");// Kiểu 1 kích hoạt 480// kiểu 2 là mua bán
        obj.Type = "Mua Bán";
        obj.IDThanhVien = Convert.ToInt32(IDThanhVien);
        obj.PhamTramHoaHong = Convert.ToInt32(HH);
        obj.TongDiem = TongDiem.ToString();
        obj.NgayTao = DateTime.Now;
        obj.TrangThai = 1;
        obj.NoiDung = NoiDung;
        obj.IDCart = Convert.ToInt64(IDCart);
        db.DoanhSoDongHuongs.InsertOnSubmit(obj);
        db.SubmitChanges();
    }
}
