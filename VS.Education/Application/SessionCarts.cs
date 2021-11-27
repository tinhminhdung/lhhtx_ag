using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framwork;
using Entity;
using System.Data.SqlClient;
using System.Data;

namespace Services
{
    public class SessionCarts
    {
        #region "Cart"
        public static void ShoppingCreateCart()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PID", typeof(Int32));
            //   dt.Columns.Add("IDGiaThanh", typeof(Int32));
            dt.Columns.Add("Vimg", typeof(String));
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Price", typeof(String));
            dt.Columns.Add("Quantity", typeof(Int32));
            dt.Columns.Add("Money", typeof(String));
            dt.Columns.Add("Mausac", typeof(String));
            dt.Columns.Add("Kichco", typeof(String));
            dt.Columns.Add("GiaThanhVien", typeof(String));
            dt.Columns.Add("GiaPhaiTraNCC", typeof(String));

            dt.Columns.Add("GiaThanhVienFree", typeof(String));
            dt.Columns.Add("GiaChietKhauDaiLy", typeof(String));

            dt.Columns.Add("ViVip", typeof(String));
            dt.Columns.Add("ChietKhau", typeof(String));

            dt.Columns.Add("Diemcoin", typeof(float));
            dt.Columns.Add("TongDiemcoin", typeof(float));
            dt.Columns.Add("SanPhamChienLuoc", typeof(Int32));
            System.Web.HttpContext.Current.Session["cart"] = dt;
        }

        static void ShoppingCart_CreateCart()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PID", typeof(Int32));
            dt.Columns.Add("Vimg", typeof(String));
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Price", typeof(String));
            dt.Columns.Add("Quantity", typeof(Int32));
            dt.Columns.Add("Money", typeof(String));
            dt.Columns.Add("Mausac", typeof(String));
            dt.Columns.Add("Kichco", typeof(String));
            dt.Columns.Add("GiaThanhVien", typeof(String));
            dt.Columns.Add("GiaPhaiTraNCC", typeof(String));
            dt.Columns.Add("GiaThanhVienFree", typeof(String));
            dt.Columns.Add("GiaChietKhauDaiLy", typeof(String));
            dt.Columns.Add("ViVip", typeof(String));
            dt.Columns.Add("ChietKhau", typeof(String));
            dt.Columns.Add("Diemcoin", typeof(float));
            dt.Columns.Add("TongDiemcoin", typeof(float));
            dt.Columns.Add("SanPhamChienLuoc", typeof(Int32));
            System.Web.HttpContext.Current.Session["cart"] = dt;
        }

        public static void ShoppingCart_RemoveProduct(string pid)
        {
            if (System.Web.HttpContext.Current.Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                        {
                            dtcart.Rows.RemoveAt(i);
                            break;
                        }
                    }
                }
                System.Web.HttpContext.Current.Session["cart"] = dtcart;
            }
        }

        public static void Cart_UpdateNumber(ref DataTable dtcart, string pid, string quantity)
        {
            if (dtcart.Rows.Count > 0)
            {
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                    {
                        dtcart.Rows[i]["Quantity"] = quantity;
                        dtcart.Rows[i]["Money"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["Price"].ToString());
                        dtcart.Rows[i]["TongDiemcoin"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["Diemcoin"].ToString());
                        return;
                    }
                }
            }
        }

        public static void Cart_Updatequantity(ref DataTable dtcart, string pid, string quantity)
        {
            if (dtcart.Rows.Count > 0)
            {
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                    {
                        dtcart.Rows[i]["quantity"] = quantity;
                        dtcart.Rows[i]["Money"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["Price"].ToString());
                        dtcart.Rows[i]["GiaThanhVienFree"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["GiaThanhVienFree"].ToString());
                        dtcart.Rows[i]["GiaChietKhauDaiLy"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["GiaChietKhauDaiLy"].ToString());

                        dtcart.Rows[i]["ChietKhau"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["ChietKhau"].ToString());

                        dtcart.Rows[i]["TongDiemcoin"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["Diemcoin"].ToString());
                        return;
                    }
                }
            }
        }

        public static void ShoppingCart_AddProduct(string pid, int quantity)
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
            {
                // create session cart.
                ShoppingCart_CreateCart();
                ShoppingCart_AddProduct(pid, quantity);
            }
            else
            {
                List<Products> dt = new List<Products>();
                // lay chi tiet san pham.
                dt = SProducts.GetById(pid);
                if (dt.Count > 0)
                {
                    string vimg = dt[0].Images.ToString();
                    string name = dt[0].Name.ToString();
                    if (!dt[0].Price.ToString().Equals(""))
                    {
                        float price = Convert.ToSingle(dt[0].Price);
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

                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                    else
                    {
                        float price = Convert.ToSingle(0);
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
                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                }
            }

        }
        public static string DiemTichLuyAdd(string GiaDaiLy, string GiaCongTy)
        {
            //Giá đại lý - giá công ty=16
            // 158-142=16
            if (GiaDaiLy.Length > 0 && GiaCongTy.Length > 0)
            {
                if (Convert.ToDouble(GiaDaiLy.ToString()) > Convert.ToDouble(GiaCongTy.ToString()))
                {
                    double GiaCongTys = Convert.ToDouble(GiaCongTy.ToString());
                    double GiaDaiLys = Convert.ToDouble(GiaDaiLy.ToString());
                    double Tong = (GiaDaiLys - GiaCongTys) / 1000;
                    if (Tong != 0)
                    {
                        return Tong.ToString();
                    }
                }
            }
            return "0";
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


        //ThanhVien Nếu đăng nhập sẽ mua giá của thành viên
        //Chú ý: Khi đăng sản phẩm chiến lược để trừ vào (THƯỞNG MUA HÀNG)
        //- 10% (THƯỞNG MUA HÀNG) trừ cho thành viên mua sản phẩm chiến lược
        //- 30% trừ vào (THƯỞNG MUA HÀNG) cho thành viên mua sản phẩm thông thường ko phải sản phẩm chiến lược
        public static void ShoppingCart_AddProduct_Sesion(string pid, int quantity, string Mausac, string Kichco, string ThanhVien)
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
            {
                // create session cart.
                ShoppingCart_CreateCart();
                ShoppingCart_AddProduct_Sesion(pid, quantity, Mausac, Kichco, ThanhVien);
            }
            else
            {
                string KieuThanhVien = "0";
                string CuaHangTV = "0";

                //  double ViTangTienVip = 0;
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
                        //if (System.Web.HttpContext.Current.Session["ViTangTienVip"] == null)
                        //{
                        //    ViTangTienVip = Convert.ToDouble(table[0].ViTangTienVip);
                        //}
                        //else
                        //{
                        //    ViTangTienVip = Convert.ToDouble(System.Web.HttpContext.Current.Session["ViTangTienVip"].ToString());
                        //}
                    }
                }
                List<Products> dt = new List<Products>();
                // lay chi tiet san pham.
                dt = SProducts.Name_Text("SELECT * FROM [products] WHERE ipid = " + pid + " and Status=1 ");
                if (dt.Count > 0)
                {
                    Double DiemTichLuy = Convert.ToDouble(DiemTichLuyAdd(dt[0].GiaThanhVien.ToString(), dt[0].Giacongtynhapvao.ToString()));
                    Double PhanTramGiaChietKhau = 0;
                    if (KieuThanhVien.ToString() == "1")
                    {
                        PhanTramGiaChietKhau = Convert.ToDouble(dt[0].PhanTramChietKhauDaiLy);
                    }
                    else
                    {
                        PhanTramGiaChietKhau = Convert.ToDouble(dt[0].PhanTramChietKhauThanhVien);
                    }
                    string SanPhamChienLuoc = dt[0].KichHoatDaiLy.ToString();
                    string vimg = dt[0].Images.ToString();
                    string name = dt[0].Name.ToString();
                    //  string ViVip = dt[0].Noidung3.ToString();// số tiền sẽ trừ vào THƯỞNG MUA HÀNG
                    string ViVip = dt[0].News.ToString();// số tiền sẽ trừ vào THƯỞNG MUA HÀNG
                    if (!dt[0].Price.ToString().Equals(""))
                    {
                        Double price = Convert.ToDouble(dt[0].Price);

                        // (((price * PhanTramGiaChietKhau) / 100) / 1000) * quantity;

                        Double GiaThanhVien = Convert.ToDouble(dt[0].GiaThanhVien);
                        Double GiaCuaHang = Convert.ToDouble(dt[0].GiaCuaHang);
                        Double GiaThanhVienFree = 0;
                        if (dt[0].TrangThaiAgLang.ToString() != "2")
                        {
                            if (dt[0].GiaThanhVienFree.ToString() != "0")
                            {
                                GiaThanhVienFree = Convert.ToDouble(dt[0].GiaThanhVienFree) * quantity;// / 1000;
                            }
                            else
                            {
                                GiaThanhVienFree = Convert.ToDouble(ThanhVienFree(price.ToString(), GiaThanhVien.ToString())) * quantity;
                            }
                        }

                        Double GiaChietKhauDaiLy = 0;
                        if (dt[0].TrangThaiAgLang.ToString() != "2")
                        {
                            if (CuaHangTV.ToString() == "1")// nếu là cửa hàng
                            {
                                if (dt[0].GiaCuaHang.ToString() != "0")// chiết khấu cửa hàng theo từng sản phẩm
                                {
                                    GiaChietKhauDaiLy = Convert.ToDouble(dt[0].GiaCuaHang) * quantity;// / 1000;
                                }
                                else
                                {
                                    GiaChietKhauDaiLy = Convert.ToDouble(ThanhVienCuaHang(price.ToString(), GiaThanhVien.ToString())) * quantity;// chiết khấu cửa hàng theo cấu hình 
                                }
                            }
                            else// Đại lý
                            {
                                if (dt[0].GiaChietKhauDaiLy.ToString() != "0")
                                {
                                    GiaChietKhauDaiLy = Convert.ToDouble(dt[0].GiaChietKhauDaiLy) * quantity;// / 1000;
                                }
                                else
                                {
                                    GiaChietKhauDaiLy = Convert.ToDouble(ThanhVienDaiLy(price.ToString(), GiaThanhVien.ToString())) * quantity;
                                }
                            }
                        }

                        string GiaChietKhau = "0";
                        double GiaChietKhaudb = 0;

                        if (KieuThanhVien.ToString() == "1")// thành viên đại lý đã kích hoạt 480 K và cửa hàng
                        {
                            Double Giatien = Convert.ToDouble(dt[0].Price.ToString()) * Convert.ToDouble(quantity);
                            Double GiaThanhViens = GiaThanhVien * quantity;
                            GiaChietKhau = Commond.ThanhVienTongTien(Giatien.ToString(), GiaThanhViens.ToString(), GiaChietKhauDaiLy.ToString());
                            //if (ViTangTienVip >= Convert.ToDouble(GiaChietKhau))
                            //{
                            // Nếu khi đăng sản phẩm mà điền trừ vào ví víp thì cho trừ còn ko thì cho trừ tự động chia 30% 
                            // Xếp Vân Ngày 11/01/2021 trao đổi lại và bắt đưa vào văn bản.
                            if (ViVip == "1")// 10% THƯỞNG MUA HÀNG trừ cho thành viên mua sản phẩm chiến lược
                            {
                                Double TongDiem = Convert.ToDouble(GiaChietKhau);
                                Double Tong = (TongDiem * 10) / 100;
                                //Response.Write(Tong.ToString() + "<br />");
                                //Double Tong2 = (TongDiem - Tong);
                                //Response.Write(Tong2.ToString() + "<br />");
                                GiaChietKhaudb = Convert.ToDouble(Tong);
                            }
                            else // 30% trừ vào THƯỞNG MUA HÀNG cho thành viên mua sản phẩm thông thường ko phải sản phẩm chiến lược
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
                        else// thành viên Free
                        {
                            Double Giatien = Convert.ToDouble(dt[0].Price.ToString()) * Convert.ToDouble(quantity);
                            Double GiaThanhViens = GiaThanhVien * quantity;
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

                        //GiaThanhVienFree = GiaThanhVienFree * quantity;
                        //  GiaChietKhauDaiLy = GiaChietKhauDaiLy * quantity;

                        Double GiaPhaiTraNCC = Convert.ToSingle(dt[0].Giacongtynhapvao);
                        Double money = price * quantity;
                        Double DiemTichLuy2 = DiemTichLuy * quantity;
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
                                dtcart.Rows[i]["Money"] = quantity * Convert.ToDouble(dtcart.Rows[i]["Price"]);
                                dtcart.Rows[i]["Mausac"] = Mausac;
                                dtcart.Rows[i]["Kichco"] = Kichco;
                                dtcart.Rows[i]["GiaThanhVien"] = GiaThanhVien;
                                dtcart.Rows[i]["GiaThanhVienFree"] = Convert.ToInt32(quantity) * GiaThanhVienFree;
                                dtcart.Rows[i]["GiaChietKhauDaiLy"] = Convert.ToInt32(quantity) * GiaChietKhauDaiLy;

                                dtcart.Rows[i]["ChietKhau"] = Convert.ToDouble(GiaChietKhaudb) * Convert.ToInt32(quantity);

                                dtcart.Rows[i]["GiaPhaiTraNCC"] = GiaPhaiTraNCC;
                                dtcart.Rows[i]["TongDiemcoin"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["Diemcoin"].ToString());

                                dtcart.Rows[i]["SanPhamChienLuoc"] = SanPhamChienLuoc;

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
                                dr["GiaThanhVien"] = GiaThanhVien;
                                dr["GiaPhaiTraNCC"] = GiaPhaiTraNCC;
                                dr["ChietKhau"] = GiaChietKhaudb;

                                dr["GiaThanhVienFree"] = GiaThanhVienFree;

                                dr["GiaChietKhauDaiLy"] = GiaChietKhauDaiLy;

                                dr["Diemcoin"] = DiemTichLuy;
                                dr["TongDiemcoin"] = DiemTichLuy2;
                                dr["SanPhamChienLuoc"] = SanPhamChienLuoc;

                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                    else
                    {
                        Double VDiemTichLuy = Convert.ToDouble(0);
                        Double price = Convert.ToDouble(0);
                        Double GiaThanhVien = Convert.ToDouble(0);
                        Double money = price * quantity;

                        Double DiemTichLuy2 = VDiemTichLuy * quantity;
                        Double GiaPhaiTraNCC = Convert.ToDouble(0);
                        Double GiaThanhVienFree = Convert.ToDouble(0);
                        Double GiaChietKhauDaiLy = Convert.ToDouble(0);
                        Double GiaChietKhau = Convert.ToDouble(0);
                        Double GiaChietKhaudb = Convert.ToDouble(0);
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
                                dtcart.Rows[i]["Money"] = quantity * Convert.ToDouble(dtcart.Rows[i]["Price"]);
                                dtcart.Rows[i]["Mausac"] = Mausac;
                                dtcart.Rows[i]["Kichco"] = Kichco;
                                dtcart.Rows[i]["GiaThanhVien"] = GiaThanhVien;
                                dtcart.Rows[i]["GiaPhaiTraNCC"] = GiaPhaiTraNCC;

                                dtcart.Rows[i]["GiaThanhVienFree"] = Convert.ToInt32(quantity) * GiaThanhVienFree;
                                dtcart.Rows[i]["GiaChietKhauDaiLy"] = Convert.ToInt32(quantity) * GiaChietKhauDaiLy;

                                dtcart.Rows[i]["ChietKhau"] = Convert.ToDouble(GiaChietKhaudb) * Convert.ToInt32(quantity);

                                dtcart.Rows[i]["Diemcoin"] = quantity * Convert.ToDouble(DiemTichLuy);
                                dtcart.Rows[i]["TongDiemcoin"] = quantity * Convert.ToDouble(dtcart.Rows[i]["Diemcoin"]);


                                dtcart.Rows[i]["SanPhamChienLuoc"] = SanPhamChienLuoc;
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
                                dr["GiaThanhVien"] = GiaThanhVien;
                                dr["GiaPhaiTraNCC"] = GiaPhaiTraNCC;
                                dr["ChietKhau"] = GiaChietKhau;
                                dr["GiaThanhVienFree"] = GiaThanhVienFree;
                                dr["GiaChietKhauDaiLy"] = GiaChietKhauDaiLy;
                                dr["Diemcoin"] = DiemTichLuy;
                                dr["TongDiemcoin"] = DiemTichLuy2;
                                dr["SanPhamChienLuoc"] = SanPhamChienLuoc;
                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                }
            }
        }


        //public static void ShoppingCart_AddProduct_Sesion_MuaTheoGiaThanhVienDong(string IDGiaThanh, string GiaThanhVien, string pid, int quantity, string Mausac, string Kichco, string ThanhVien)
        //{
        //    string trangthaigiathanhvien = "0";
        //    if (System.Web.HttpContext.Current.Session["cart"] == null)
        //    {
        //        // create session cart.
        //        ShoppingCreateCart();
        //        ShoppingCart_AddProduct_Sesion_MuaTheoGiaThanhVienDong(IDGiaThanh, GiaThanhVien, pid, quantity, Mausac, Kichco, ThanhVien);
        //    }
        //    else
        //    {
        //        List<Products> dt = new List<Products>();
        //        // lay chi tiet san pham.
        //        dt = SProducts.GetById(pid);
        //        if (dt.Count > 0)
        //        {
        //            string DiemTichLuy = DiemTichLuyAdd(GiaThanhVien.ToString(), dt[0].Giacongtynhapvao.ToString());
        //            string vimg = dt[0].Images.ToString();
        //            string name = dt[0].Name.ToString();
        //            if (!GiaThanhVien.ToString().Equals(""))
        //            {
        //                float price = 0;
        //                if (ThanhVien == "1")
        //                {
        //                    if (GiaThanhVien.Length > 0)// nếu ko có giá thành viên thì lấy giá gốc nhé
        //                    {
        //                        price = Convert.ToSingle(GiaThanhVien);
        //                        trangthaigiathanhvien = "1";
        //                    }
        //                    else
        //                    {
        //                        price = Convert.ToSingle(dt[0].Price);
        //                    }
        //                }
        //                else
        //                {
        //                    price = Convert.ToSingle(dt[0].Price);
        //                }
        //                float money = price * quantity;
        //                DataTable dtcart = new DataTable();
        //                dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
        //                bool hasincart = false;
        //                for (int i = 0; i < dtcart.Rows.Count; i++)
        //                {
        //                    if (dtcart.Rows[i]["IDGiaThanh"].ToString().Equals(IDGiaThanh))
        //                        {
        //                            hasincart = true;
        //                            // cap nhat thong tin cua cart.
        //                            quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
        //                            dtcart.Rows[i]["Quantity"] = quantity;
        //                            dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(dtcart.Rows[i]["Price"]);
        //                            dtcart.Rows[i]["Mausac"] = Mausac;
        //                            dtcart.Rows[i]["Kichco"] = Kichco;
        //                            dtcart.Rows[i]["GiaThanhVien"] = trangthaigiathanhvien;
        //                            dtcart.Rows[i]["Diemcoin"] = DiemTichLuy;
        //                            System.Web.HttpContext.Current.Session["cart"] = dtcart;
        //                            break;
        //                        }
        //                }
        //                if (hasincart == false)
        //                {
        //                    if (dtcart != null)
        //                    {
        //                        DataRow dr = dtcart.NewRow();
        //                        dr["PID"] = pid;
        //                        dr["IDGiaThanh"] = IDGiaThanh;
        //                        dr["Vimg"] = vimg;
        //                        dr["Name"] = name;
        //                        dr["Price"] = price;
        //                        dr["Quantity"] = quantity;
        //                        dr["Money"] = money;
        //                        dr["Mausac"] = Mausac;
        //                        dr["Kichco"] = Kichco;
        //                        dr["GiaThanhVien"] = trangthaigiathanhvien;
        //                        dr["Diemcoin"] = DiemTichLuy;
        //                        dtcart.Rows.Add(dr);
        //                        System.Web.HttpContext.Current.Session["cart"] = dtcart;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                float price = Convert.ToSingle(0);
        //                float money = price * quantity;
        //                DataTable dtcart = new DataTable();
        //                dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
        //                bool hasincart = false;
        //                for (int i = 0; i < dtcart.Rows.Count; i++)
        //                {
        //                    if (dtcart.Rows[i]["IDGiaThanh"].ToString().Equals(IDGiaThanh))
        //                    {
        //                        hasincart = true;
        //                        // cap nhat thong tin cua cart.
        //                        quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
        //                        dtcart.Rows[i]["Quantity"] = quantity;
        //                        dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(dtcart.Rows[i]["Price"]);
        //                        dtcart.Rows[i]["Mausac"] = Mausac;
        //                        dtcart.Rows[i]["Kichco"] = Kichco;
        //                        dtcart.Rows[i]["GiaThanhVien"] = trangthaigiathanhvien;
        //                        dtcart.Rows[i]["Diemcoin"] = DiemTichLuy;
        //                        System.Web.HttpContext.Current.Session["cart"] = dtcart;
        //                        break;
        //                    }
        //                }
        //                if (hasincart == false)
        //                {
        //                    if (dtcart != null)
        //                    {
        //                        DataRow dr = dtcart.NewRow();
        //                        dr["PID"] = pid;
        //                        dr["IDGiaThanh"] = IDGiaThanh;
        //                        dr["Vimg"] = vimg;
        //                        dr["Name"] = name;
        //                        dr["Price"] = price;
        //                        dr["Quantity"] = quantity;
        //                        dr["Money"] = money;
        //                        dr["Mausac"] = Mausac;
        //                        dr["Kichco"] = Kichco;
        //                        dr["GiaThanhVien"] = trangthaigiathanhvien;
        //                        dr["Diemcoin"] = DiemTichLuy;
        //                        dtcart.Rows.Add(dr);
        //                        System.Web.HttpContext.Current.Session["cart"] = dtcart;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //}
        public static void AddProduct(string pid, int quantity)
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
            {
                // create session cart.
                ShoppingCart_CreateCart();
                AddProduct(pid, quantity);
            }
            else
            {
                List<Products> dt = new List<Products>();
                // lay chi tiet san pham.
                dt = SProducts.GetById(pid);
                if (dt.Count > 0)
                {
                    string name = dt[0].Name.ToString();
                    string vimg = dt[0].Images.ToString();
                    if (!dt[0].Price.ToString().Equals(""))
                    {
                        float price = Convert.ToSingle(dt[0].Price);
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

                                //
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
                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                    else
                    {
                        float price = Convert.ToSingle(0);
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

                                //
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
                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Cart_Calculate_Cart
        public static void Cart_Calculate_Cart(List<Entity.CartDetail> dtcart, ref string inumofproducts, ref string totalvnd)
        {
            try
            {
                if (dtcart.Count > 0)
                {
                    double num = 0.0;
                    int num2 = 0;
                    for (int i = 0; i < dtcart.Count; i++)
                    {
                        num += Convert.ToDouble(dtcart[i].Money.ToString());
                        num2 += Convert.ToInt32(dtcart[i].Quantity.ToString());
                    }
                    totalvnd = num.ToString();
                    inumofproducts = num2.ToString();
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        public static string LoadCart()
        {
            if (System.Web.HttpContext.Current.Session["cart"] != null)
            {
                DataTable cartdetail = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (cartdetail.Rows.Count > 0)
                {
                    string inumofproducts = "";
                    string totalvnd = "";
                    // S_Product_Carts.Cart_Calculate_Cart(ref cartdetail, ref inumofproducts, ref totalvnd);
                    if (cartdetail.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < cartdetail.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(cartdetail.Rows[i]["Money"].ToString());
                            num2 += Convert.ToInt32(cartdetail.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    return inumofproducts;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
    }
}



