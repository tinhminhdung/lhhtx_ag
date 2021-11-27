using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VS.E_Commerce;

public class GioHangs
{
    public static void ShoppingCart_CreateCart()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PID", typeof(Int32));
        dt.Columns.Add("Vimg", typeof(String));
        dt.Columns.Add("Name", typeof(String));
        dt.Columns.Add("Price", typeof(float));
        dt.Columns.Add("Quantity", typeof(Int32));
        dt.Columns.Add("Money", typeof(float));
        dt.Columns.Add("Mausac", typeof(Int32));
        dt.Columns.Add("Kichco", typeof(Int32));
        dt.Columns.Add("GiaThanhVien", typeof(Int32));
        dt.Columns.Add("Diemcoin", typeof(Int32));
        dt.Columns.Add("TongDiemcoin", typeof(Int32));
        System.Web.HttpContext.Current.Session["cart"] = dt;
    }
    public static string ThanhVienFree(string GiaNYs, string GiaGocs)
    {
        Double GiaNY = Convert.ToDouble(GiaNYs) / 1000;
        Double GiaGoc = Convert.ToDouble(GiaGocs) / 1000;
        Double HoaHongFree = Convert.ToDouble(MoreAll.Other.Giatri("txttangFree"));

        // Nếu thành viên là Free , sẽ tặng vào ví mua hàng
        Double TongTien = 0;
        TongTien = Convert.ToDouble(GiaNY) - Convert.ToDouble(GiaGoc);

        Double Free25 = (TongTien * HoaHongFree) / 100;
        // Double Tong = (Free25 * Convert.ToDouble(SoLuong));
        return Free25.ToString();
    }
    public static string ThanhVienDaiLy(string GiaNYs, string GiaGocs)
    {
        Double GiaNY = Convert.ToDouble(GiaNYs) / 1000;
        Double GiaGoc = Convert.ToDouble(GiaGocs) / 1000;
        Double HoaHongDaiLy = Convert.ToDouble(MoreAll.Other.Giatri("txttangthanhvien"));
        // Nếu thành viên là Đại lý được chiết khấu vào đơn hàng
        Double TongTien = 0;
        TongTien = Convert.ToDouble(GiaNY) - Convert.ToDouble(GiaGoc);

        Double DaiLy = (TongTien * HoaHongDaiLy) / 100;
        //Double Tong = (DaiLy * Convert.ToDouble(SoLuong));
        return DaiLy.ToString();
    }
    public static string ThanhVienCuaHang(string GiaNYs, string GiaGocs)
    {
        Double GiaNY = Convert.ToDouble(GiaNYs) / 1000;
        Double GiaGoc = Convert.ToDouble(GiaGocs) / 1000;
        Double HoaHongDaiLy = Convert.ToDouble(MoreAll.Other.Giatri("txtThanhViencuahang"));
        // Nếu thành viên là Đại lý được chiết khấu vào đơn hàng
        Double TongTien = 0;
        TongTien = Convert.ToDouble(GiaNY) - Convert.ToDouble(GiaGoc);

        Double DaiLy = (TongTien * HoaHongDaiLy) / 100;
        //Double Tong = (DaiLy * Convert.ToDouble(SoLuong));
        return DaiLy.ToString();
    }
    public static void Cart_UpdateNumber_ThanhVienMuaTheoSoLuong(ref DataTable dtcart, string pid, string quantity, string ThanhVien)
    {
        if (dtcart.Rows.Count > 0)
        {
            for (int i = 0; i < dtcart.Rows.Count; i++)
            {
                if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                {
                    dtcart.Rows[i]["Quantity"] = quantity;
                    Double GiaThanhVienFree = 0;
                    Double price = 0;
                    Double GiaChietKhauDaiLy = 0;
                    Double GiaThanhVien = 0;
                    Double ChietKhau = 0;
                    // Double GiaChietKhau = 0;
                    string KieuThanhVien = "0";
                    string CuaHangTV = "0";
                    // double ViTangTienVip = 0;

                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        List<Entity.users> table = Susers.Name_Text("select * from users where vuserun=N'" + MoreAll.MoreAll.GetCookies("Members") + "' and istatus=1 ");
                        if (table.Count > 0)
                        {
                            if (table[0].DuyetTienDanap.ToString() == "1")
                            {
                                KieuThanhVien = "1";
                            }

                            if (table[0].CuaHang.ToString() == "1")
                            {
                                CuaHangTV = "1";
                            }
                            //if (System.Web.HttpContext.Current.Session["ViTangTienVip2"] == null)
                            //{
                            //    ViTangTienVip = Convert.ToDouble(table[0].ViTangTienVip);
                            //}
                            //else
                            //{
                            //    ViTangTienVip = Convert.ToDouble(System.Web.HttpContext.Current.Session["ViTangTienVip2"].ToString()) - Convert.ToDouble(System.Web.HttpContext.Current.Session["ViTangTienVip"].ToString());
                            //}
                        }
                    }
                    int Soluong = int.Parse(quantity);
                    List<Entity.Products> iitem = SProducts.GetById(pid);
                    if (iitem.Count > 0)
                    {
                        price = Convert.ToDouble(iitem[0].Price);
                        Double PhanTramGiaChietKhau = 0;
                        string ViVip = iitem[0].News.ToString();// số tiền sẽ trừ vào THƯỞNG MUA HÀNG

                        GiaThanhVien = Convert.ToDouble(iitem[0].GiaThanhVien);
                        if (iitem[0].TrangThaiAgLang.ToString() != "2")
                        {
                            if (iitem[0].GiaThanhVienFree.Length > 2)
                            {
                                GiaThanhVienFree = Convert.ToDouble(iitem[0].GiaThanhVienFree);// / 1000;
                            }
                            else
                            {
                                GiaThanhVienFree = Convert.ToDouble(ThanhVienFree(price.ToString(), GiaThanhVien.ToString()));
                            }

                        }
                        if (iitem[0].TrangThaiAgLang.ToString() != "2")
                        {
                            //if (iitem[0].GiaChietKhauDaiLy.Length > 2)
                            //{
                            //    GiaChietKhauDaiLy = Convert.ToDouble(iitem[0].GiaChietKhauDaiLy);// / 1000;
                            //}
                            //else
                            //{
                            //    //GiaChietKhauDaiLy = Convert.ToDouble(ThanhVienDaiLy(price.ToString(), GiaThanhVien.ToString()));
                            //    if (CuaHangTV.ToString() == "1")// nếu là cửa hàng
                            //    {
                            //        GiaChietKhauDaiLy = Convert.ToDouble(ThanhVienCuaHang(price.ToString(), GiaThanhVien.ToString()));
                            //    }
                            //    else// Đại lý
                            //    {
                            //        GiaChietKhauDaiLy = Convert.ToDouble(ThanhVienDaiLy(price.ToString(), GiaThanhVien.ToString()));
                            //    }
                            //}

                            if (CuaHangTV.ToString() == "1")// nếu là cửa hàng
                            {
                                if (iitem[0].GiaCuaHang.ToString() != "0")// chiết khấu cửa hàng theo từng sản phẩm
                                {
                                    GiaChietKhauDaiLy = Convert.ToDouble(iitem[0].GiaCuaHang);// chiết khấu cửa hàng theo từng sản phẩm
                                }
                                else
                                {
                                    GiaChietKhauDaiLy = Convert.ToDouble(ThanhVienCuaHang(price.ToString(), GiaThanhVien.ToString())) ;// chiết khấu cửa hàng theo cấu hình 
                                }
                            }
                            else// Đại lý
                            {
                                if (iitem[0].GiaChietKhauDaiLy.ToString() != "0")
                                {
                                    GiaChietKhauDaiLy = Convert.ToDouble(iitem[0].GiaChietKhauDaiLy);// / 1000;
                                }
                                else
                                {
                                    GiaChietKhauDaiLy = Convert.ToDouble(ThanhVienDaiLy(price.ToString(), GiaThanhVien.ToString()));
                                }
                            }
                        }
                        //    //}
                        //}

                        Double GiaChietKhaudb = 0;
                        string GiaChietKhau = "0";
                        if (KieuThanhVien.ToString() == "1")
                        {
                            Double GiaThanhViens = GiaThanhVien;// *Convert.ToDouble(quantity);
                            Double Giatien = Convert.ToDouble(price.ToString());// *Convert.ToDouble(quantity);
                            GiaChietKhau = Commond.ThanhVienTongTien(Giatien.ToString(), GiaThanhViens.ToString(), GiaChietKhauDaiLy.ToString());

                            if (ViVip == "1")// 10%
                            {
                                Double TongDiem = Convert.ToDouble(GiaChietKhau);
                                Double Tong = (TongDiem * 10) / 100;
                                //Response.Write(Tong.ToString() + "<br />");
                                //Double Tong2 = (TongDiem - Tong);
                                //Response.Write(Tong2.ToString() + "<br />");
                                GiaChietKhaudb = Convert.ToDouble(Tong);
                            }
                            else // 30%
                            {
                                //GiaChietKhaudb = Convert.ToDouble(GiaChietKhau) / 2;
                                Double TongDiem = Convert.ToDouble(GiaChietKhau);
                                Double Tong = (TongDiem * 30) / 100;
                                //Response.Write(Tong.ToString() + "<br />");
                                //Double Tong2 = (TongDiem - Tong);
                                //Response.Write(Tong2.ToString() + "<br />");
                                GiaChietKhaudb = Convert.ToDouble(Tong);
                            }
                        }
                        else
                        {
                            Double GiaThanhViens = GiaThanhVien;//* Convert.ToDouble(quantity);
                            Double Giatien = Convert.ToDouble(price.ToString());// *Convert.ToDouble(quantity);
                            GiaChietKhau = Commond.ThanhVienTongTien(Giatien.ToString(), GiaThanhVien.ToString(), GiaThanhVienFree.ToString());

                            if (ViVip == "1")// 10%
                            {
                                Double TongDiem = Convert.ToDouble(GiaChietKhau);
                                Double Tong = (TongDiem * 10) / 100;
                                //Response.Write(Tong.ToString() + "<br />");
                                //Double Tong2 = (TongDiem - Tong);
                                //Response.Write(Tong2.ToString() + "<br />");
                                GiaChietKhaudb = Convert.ToDouble(Tong);
                            }
                            else // 30%
                            {
                                //GiaChietKhaudb = Convert.ToDouble(GiaChietKhau) / 2;
                                Double TongDiem = Convert.ToDouble(GiaChietKhau);
                                Double Tong = (TongDiem * 30) / 100;
                                //Response.Write(Tong.ToString() + "<br />");
                                //Double Tong2 = (TongDiem - Tong);
                                //Response.Write(Tong2.ToString() + "<br />");
                                GiaChietKhaudb = Convert.ToDouble(Tong);
                            }
                        }


                        dtcart.Rows[i]["GiaThanhVienFree"] = Convert.ToInt32(quantity) * Convert.ToDouble(GiaThanhVienFree);
                        dtcart.Rows[i]["GiaChietKhauDaiLy"] = Convert.ToInt32(quantity) * Convert.ToDouble(GiaChietKhauDaiLy);

                        dtcart.Rows[i]["ChietKhau"] = Convert.ToInt32(quantity) * (Convert.ToDouble(GiaChietKhaudb));

                        //  string DiemTichLuy = SessionCarts.DiemTichLuyAdd(price.ToString(), iitem[0].Giacongtynhapvao.ToString());
                        dtcart.Rows[i]["Money"] = Convert.ToInt32(quantity) * Convert.ToDouble(price);
                        dtcart.Rows[i]["price"] = Convert.ToDouble(price);
                        //dtcart.Rows[i]["Diemcoin"] = Convert.ToInt32(quantity) * Convert.ToDouble(DiemTichLuy);
                        dtcart.Rows[i]["TongDiemcoin"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["Diemcoin"].ToString());
                        return;
                    }
                }
            }
        }
    }
    // Gi?m giá theo s? lu?ng mua hàng
    public static string ShoppingCart_AddProduct(string pid, int quantity, string Mausac, string Kichco, string ThanhVien)
    {
        string trangthaigiathanhvien = "0";
        if (System.Web.HttpContext.Current.Session["cart"] == null)
        {
            // create session cart.
            ShoppingCart_CreateCart();
            ShoppingCart_AddProduct(pid, quantity, Mausac, Kichco, ThanhVien);
        }
        else
        {
            List<Entity.Products> dt = new List<Entity.Products>();
            // lay chi tiet san pham.
            dt = SProducts.GetById(pid);
            if (dt.Count > 0)
            {
                string vimg = dt[0].Images.ToString();
                string name = dt[0].Name.ToString();
                if (!dt[0].Price.ToString().Equals(""))
                {
                    float price = Convert.ToSingle(dt[0].Price);
                    if (quantity.ToString().Length > 0)
                    {
                        int Soluong = int.Parse(quantity.ToString());

                        // s? lu?ng mà nh? hon 1 thì tính bình thu?ng
                        //    if (Soluong <= 1)
                        //    {
                        //        if (ThanhVien == "1")
                        //        {
                        //            if (dt[0].GiaThanhVien.Length > 0)// n?u ko có giá thành viên thì l?y giá g?c nhé
                        //            {
                        //                price = Convert.ToSingle(dt[0].GiaThanhVien);
                        //                trangthaigiathanhvien = "1";
                        //            }
                        //            else
                        //            {
                        //                price = Convert.ToSingle(dt[0].Price);
                        //            }
                        //        }
                        //        else
                        //        {
                        //            price = Convert.ToSingle(dt[0].Price);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        ////// Tính giá theo s? lu?ng
                        //        ////if (TinhGia(Soluong.ToString(), pid.ToString()) != "0")
                        //        ////{
                        //        ////    price = Convert.ToSingle(TinhGia(Soluong.ToString(), pid.ToString()));
                        //        ////}
                        //        ////else
                        //        ////{
                        //        ////    // n?u ko có giá nào thì l?i l?y giá cu
                        //        ////    if (ThanhVien == "1")
                        //        ////    {
                        //        ////        if (dt[0].GiaThanhVien.Length > 0)// n?u ko có giá thành viên thì l?y giá g?c nhé
                        //        ////        {
                        //        ////            price = Convert.ToSingle(dt[0].GiaThanhVien);
                        //        ////        }
                        //        ////        else
                        //        ////        {
                        //        ////            price = Convert.ToSingle(dt[0].Price);
                        //        ////        }
                        //        ////    }
                        //        ////}
                        //    }
                    }

                    string DiemTichLuy = SessionCarts.DiemTichLuyAdd(price.ToString(), dt[0].Giacongtynhapvao.ToString());
                    float DiemTichLuys = Convert.ToInt32(quantity) * Convert.ToSingle(DiemTichLuy);
                    float money = price * quantity;
                    DataTable dtcart = new DataTable();
                    dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                    bool hasincart = false;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                        {
                            hasincart = true;
                            // cap nhat thong tin cua cart.
                            quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
                            dtcart.Rows[i]["Quantity"] = quantity;
                            dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(dtcart.Rows[i]["Price"]);
                            dtcart.Rows[i]["Mausac"] = Mausac;
                            dtcart.Rows[i]["Kichco"] = Kichco;
                            dtcart.Rows[i]["GiaThanhVien"] = trangthaigiathanhvien;
                            dtcart.Rows[i]["Diemcoin"] = Convert.ToInt32(quantity) * Convert.ToSingle(DiemTichLuy);
                            System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            break;
                        }
                    }
                    if (hasincart == false)
                    {
                        if (dtcart != null)
                        {
                            DataRow dr = dtcart.NewRow();
                            dr["PID"] = pid;
                            dr["Vimg"] = vimg;
                            dr["Name"] = name;
                            dr["Price"] = price;
                            dr["Quantity"] = quantity;
                            dr["Money"] = money;
                            dr["Mausac"] = Mausac;
                            dr["Kichco"] = Kichco;
                            dr["GiaThanhVien"] = trangthaigiathanhvien;
                            dr["Diemcoin"] = DiemTichLuys;

                            dtcart.Rows.Add(dr);
                            System.Web.HttpContext.Current.Session["cart"] = dtcart;
                        }
                    }
                }
                else
                {
                    float price = Convert.ToSingle(0);
                    float money = price * quantity;
                    string DiemTichLuy = SessionCarts.DiemTichLuyAdd(price.ToString(), dt[0].Giacongtynhapvao.ToString());
                    float DiemTichLuys = Convert.ToInt32(quantity) * Convert.ToSingle(DiemTichLuy);
                    DataTable dtcart = new DataTable();
                    dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                    bool hasincart = false;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                        {
                            hasincart = true;
                            // cap nhat thong tin cua cart.
                            quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
                            dtcart.Rows[i]["Quantity"] = quantity;
                            dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(dtcart.Rows[i]["Price"]);
                            dtcart.Rows[i]["Mausac"] = Mausac;
                            dtcart.Rows[i]["Kichco"] = Kichco;
                            dtcart.Rows[i]["GiaThanhVien"] = trangthaigiathanhvien;
                            dtcart.Rows[i]["Diemcoin"] = Convert.ToInt32(quantity) * Convert.ToSingle(DiemTichLuy);
                            System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            break;
                        }
                    }
                    if (hasincart == false)
                    {
                        if (dtcart != null)
                        {
                            DataRow dr = dtcart.NewRow();
                            dr["PID"] = pid;
                            dr["Vimg"] = vimg;
                            dr["Name"] = name;
                            dr["Price"] = price;
                            dr["Quantity"] = quantity;
                            dr["Money"] = money;
                            dr["Mausac"] = Mausac;
                            dr["Kichco"] = Kichco;
                            dr["GiaThanhVien"] = trangthaigiathanhvien;
                            dr["Diemcoin"] = DiemTichLuys;
                            dtcart.Rows.Add(dr);
                            System.Web.HttpContext.Current.Session["cart"] = dtcart;
                        }
                    }
                }
            }
        }
        return "";
    }

    //public static string TinhGia(string Soluong, string IDSanPham)
    //{
    //    DatalinqDataContext db = new DatalinqDataContext();
    //    double Soluongs = Convert.ToSingle(Soluong.ToString());
    //    double Soluongtu = 0;
    //    double Soluongden = 0;
    //    List<GiaThanhVien> cdd = db.GiaThanhViens.Where(s => s.IDSanPham == int.Parse(IDSanPham)).ToList();
    //    if (cdd.Count > 0)
    //    {
    //        foreach (var item in cdd)
    //        {
    //            try
    //            {
    //                Soluongtu = Convert.ToDouble(item.SoLuongTu);
    //            }
    //            catch (Exception)
    //            {
    //                Soluongtu = 0;
    //            }
    //            try
    //            {
    //                Soluongden = Convert.ToDouble(item.SoLuongDen);
    //            }
    //            catch (Exception)
    //            {
    //                Soluongden = 0;
    //            }
    //            if (Soluongs >= Soluongtu && Soluongs <= Soluongden)
    //            {
    //                return item.GiaDaiLy.ToString();
    //            }
    //        }
    //    }
    //    return "0";
    //}


    public static string TinhGia(string Soluong, string IDSanPham)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        double Soluongs = Convert.ToSingle(Soluong.ToString());
        double Soluongtu = 0;
        double Soluongden = 0;
        List<GiaThanhVien> cdd = db.GiaThanhViens.Where(s => s.IDSanPham == int.Parse(IDSanPham)).ToList();
        if (cdd.Count > 0)
        {
            foreach (var item in cdd)
            {
                try
                {
                    Soluongtu = Convert.ToDouble(item.SoLuongTu);
                }
                catch (Exception)
                {
                    Soluongtu = 0;
                }
                try
                {
                    Soluongden = Convert.ToDouble(item.SoLuongDen);
                }
                catch (Exception)
                {
                    Soluongden = 0;
                }
                if (Soluongs >= Soluongtu && Soluongs <= Soluongden)
                {
                    return item.GiaDaiLy.ToString();
                }
            }
        }
        return TimGiaCoSoLuongLonNhat(IDSanPham);
        // return "0";
    }
    public static string TimGiaCoSoLuongLonNhat(string IDSanPham)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        var cdd = db.S_GiaThanhVien_TimGiaTriLonNhat(int.Parse(IDSanPham)).ToList();
        if (cdd.Count > 0)
        {
            return cdd[0].GiaDaiLy.ToString();
        }
        return "0";
    }
}
