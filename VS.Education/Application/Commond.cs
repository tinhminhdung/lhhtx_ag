using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.E_Commerce;

public class Commond
{
    public static string ShowThanhVien(string id)
    {
        string str = "";
        List<Entity.users> dt = Susers.GET_BY_ID(id);
        if (dt.Count >= 1)
        {
            str += "<span id=" + dt[0].iuser_id.ToString() + " style=\" color:red\">";
            if (dt[0].vfname.ToString().Length > 0)
            {
                str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + " [Level " + dt[0].LevelThanhVien + "]" + "</span></a>";
            }
            str += "</span><br>";
            if (dt[0].vphone.ToString().Length > 0)
            {
                str += dt[0].vphone;
            }
        }
        else
        {
            str = "Không tìm thấy thành viên";
        }
        return str;
    }
    public static string SearchThanhVien(string keyword)
    {
        string str = "0";
        List<Entity.users> dt = Susers.Name_Text("select * from users where (vuserun like N'%" + keyword + "%')");
        if (dt.Count >= 1)
        {
            for (int i = 0; i < dt.Count; i++)
            {
                str = str + "," + dt[i].iuser_id.ToString();
            }
        }
        return str.Replace("0,", "");
    }
   
    public static bool Check(object String)
    {
        return ((String != null) && (String.ToString().Trim().Length > 0));
    }
    public static DateTime ConvertStringToDate(string Date, string FromFormat)
    {
        return DateTime.ParseExact(Date, FromFormat, null);
    }
    public static string FormatDate(object date)
    {
        if (date != null)
        {
            if (date.ToString().Trim().Length > 0 && date != null)
            {
                if (DateTime.Parse(date.ToString()).Year != 1900)
                {
                    DateTime dNgay = Convert.ToDateTime(date.ToString());
                    return ((DateTime)dNgay).ToString("yyyy-MM-dd");
                }
            }
        }
        return "";
    }

    
    public static void CheckNgayHetHan(string IDThanhVien)
    {
        //DatalinqDataContext db = new DatalinqDataContext();
        //#region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K -- Phải khác với 3 người này: chuyên gia, đồng hưởng, quản lý, và aggroupusa,AI LIFE
        //if (IDThanhVien.ToString() != "67357" && IDThanhVien.ToString() != "69501" && IDThanhVien.ToString() != "69460" && IDThanhVien.ToString() != "15" && IDThanhVien.ToString() != "68314")
        //{
        //    List<LichSuKichHoat> lichs = db.ExecuteQuery<LichSuKichHoat>(@"SELECT * FROM LichSuKichHoat where IDThanhVien=" + IDThanhVien.ToString() + " ").ToList();
        //    if (lichs.Count() > 0)
        //    {
        //        DateTime oldDate = Convert.ToDateTime(lichs[0].NgayKichHoat);
        //        DateTime newDate = DateTime.Now;
        //        TimeSpan ts = newDate - oldDate;
        //        if (ts.Days >= 365)
        //        {
        //            Susers.Name_Text("update users set DuyetTienDanap=0,TrangThaiThongBao=1 where iuser_id=" + IDThanhVien.ToString() + " ");

        //            #region Thông báo
        //            string chuoi = "";
        //            chuoi += "<p><strong>Công ty cổ phần AgEcom trân trọng thông báo.</strong></p>";
        //            chuoi += "<p>Tài khoản của quý khách đã hết hạn <span style=\"color:#FF0000;\"><strong>1 năm kích hoạt làm đại lý</strong></span>.<br />";
        //            chuoi += "Quý khách vui lòng kích hoạt lại để được nhận các quyền lợi theo chính sách của công ty.<br />";
        //            chuoi += "&nbsp;</p>";
        //            chuoi += "<p>&nbsp;</p>";
        //            Notification obj = new Notification();
        //            obj.IDThanhVienNhanThongBao = int.Parse(IDThanhVien);
        //            obj.NgayTao = DateTime.Now;
        //            obj.NoiDung = chuoi;
        //            obj.NguoiTao = MoreAll.MoreAll.GetCookies("UName").ToString();
        //            obj.TrangThai = 0;
        //            db.Notifications.InsertOnSubmit(obj);
        //            db.SubmitChanges();
        //            #endregion
        //        }
        //    }
        //}
        //#endregion
    
    }
    public static void SetLichSuKichHoat(string IDThanhViens, string chuoi)
    {
        Susers.Name_Text("update users set DuyetTienDanap=1,TrangThaiThongBao=0 where iuser_id=" + IDThanhViens.ToString() + "");

        DatalinqDataContext db = new DatalinqDataContext();
        #region Cập Nhật bảng Lịch sử Ngày kích hoạt tham gia thành viên 480K
        List<LichSuKichHoat> lichs = db.ExecuteQuery<LichSuKichHoat>(@"SELECT * FROM LichSuKichHoat where IDThanhVien=" + IDThanhViens + " ").ToList();
        if (lichs.Count() > 0)
        {
            // Nếu thành viên đã có trong bảng LichSuKichHoat nghĩa là đã kích hoạt trước đây bay giờ chỉ cần cập nhật lại thôi
            LichSuKichHoat abc = db.LichSuKichHoats.SingleOrDefault(p => p.IDThanhVien == Convert.ToInt32(lichs[0].IDThanhVien.ToString()));
            abc.IDThanhVien = Convert.ToInt32(lichs[0].IDThanhVien.ToString());
            abc.NgayKichHoat = DateTime.Now;
            abc.KieuKichHoat = "3";// Kích hoạt mới năm 2021
            abc.NoiDung = chuoi;
            db.SubmitChanges();
        }
        else
        {
            // Nếu chưa kích hoạt bao giờ mà là thành viên mới tham gia lần đầu thì thêm mới vào bảng LichSuKichHoat
            #region HetHanKichHoat
            LichSuKichHoat obj = new LichSuKichHoat();
            obj.IDThanhVien = Convert.ToInt32(IDThanhViens);
            obj.NgayKichHoat = DateTime.Now;
            obj.KieuKichHoat = "3";// Kích hoạt mới năm 2021
            obj.NoiDung = chuoi;
            db.LichSuKichHoats.InsertOnSubmit(obj);
            db.SubmitChanges();
            #endregion
        }
        #endregion
    }

    public static string FormatNgayHetHan(string IDThanhVien)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        List<LichSuKichHoat> lichs = db.ExecuteQuery<LichSuKichHoat>(@"SELECT * FROM LichSuKichHoat where IDThanhVien=" + IDThanhVien.ToString() + " ").ToList();
        if (lichs.Count() > 0)
        {
            DateTime oldDate = Convert.ToDateTime(lichs[0].NgayKichHoat);
            DateTime newDate = DateTime.Now;
            TimeSpan ts = newDate - oldDate;
            if (ts.Days >= 365)
            {
                return "<span class='maukichhaoathethan'>Hết Hạn</span>  <span class='maukichhaoathethan'>Ngày kích hoạt: " + lichs[0].NgayKichHoat + "</span>";
            }
            return "<span class='maukichhaoat'>Còn hạn </span><span class='maukichhaoat'> Đã kích hoạt được: " + ts.Days + " ngày</span>  <span class='maukichhaoat'>Ngày kích hoạt: " + lichs[0].NgayKichHoat + "</span>";
        }
        return "<span class='maukichhaoathethan'>Chưa kích hoạt</span>";
    }

    public static string TachMtre(string MTRee)
    {
        return MTRee.Replace("|", " |");
    }
    public static string ThanhVienTongTien(string GiaNY, string Giagoc, string TongDiemDaTra)
    {
        Double TongTiens = Convert.ToDouble(GiaNY) / 1000;
        Double Giagocs = Convert.ToDouble(Giagoc) / 1000;
        Double TongDiemDaTras = Convert.ToDouble(TongDiemDaTra);
        Double TongCong = (TongTiens - Giagocs) - TongDiemDaTras;
        return TongCong.ToString();
    }

    public static string GiaDaiLy_FormatMoney(string GiaDaiLy_Diem, string Giagoc)
    {
        Double GiaDaiLy_Diems = Convert.ToDouble(GiaDaiLy_Diem) * 1000;
        Double Giagocs = Convert.ToDouble(Giagoc);
        Double TongCong = (Giagocs) - GiaDaiLy_Diems;
        return TongCong.ToString();
    }
    public static string ThanhVienFree(string GiaNYs, string GiaGocs)
    {
        try
        {
            Double GiaNY = Convert.ToDouble(GiaNYs) / 1000;
            Double GiaGoc = Convert.ToDouble(GiaGocs) / 1000;
            Double HoaHongFree = Convert.ToDouble(Commond.Setting("txttangFree"));

            // Nếu thành viên là Free , sẽ tặng vào ví mua hàng
            Double TongTien = 0;
            TongTien = Convert.ToDouble(GiaNY) - Convert.ToDouble(GiaGoc);

            Double Free25 = ((TongTien * HoaHongFree) / 100);
            return Free25.ToString();
        }
        catch (Exception)
        {
            return "0";
        }
    }
    public static string ThanhVienFree_SoLuong(string SoLuong, string GiaNYs, string GiaGocs)
    {
        try
        {
            Double GiaNY = Convert.ToDouble(GiaNYs) / 1000;
            Double GiaGoc = Convert.ToDouble(GiaGocs) / 1000;
            Double HoaHongFree = Convert.ToDouble(Commond.Setting("txttangFree"));


            // Nếu thành viên là Free , sẽ tặng vào ví mua hàng
            Double TongTien = 0;
            TongTien = Convert.ToDouble(GiaNY) - Convert.ToDouble(GiaGoc);

            Double Free25 = (TongTien * HoaHongFree) / 100;
            Double Tong = (Free25 * Convert.ToDouble(SoLuong));
            return Tong.ToString();
        }
        catch (Exception)
        {
            return "0";
        }
    }
    public static string ThanhVienDaiLy(string GiaNYs, string GiaGocs)
    {
        try
        {
            Double GiaNY = Convert.ToDouble(GiaNYs) / 1000;
            Double GiaGoc = Convert.ToDouble(GiaGocs) / 1000;
            Double HoaHongDaiLy = Convert.ToDouble(Commond.Setting("txttangthanhvien"));
            // Nếu thành viên là Đại lý được chiết khấu vào đơn hàng
            Double TongTien = 0;
            TongTien = Convert.ToDouble(GiaNY) - Convert.ToDouble(GiaGoc);

            Double DaiLy = ((TongTien * HoaHongDaiLy) / 100);
            return DaiLy.ToString();
        }
        catch (Exception)
        {
            return "0";
        }
    }

    public static string ThanhVienCuaHang(string GiaNYs, string GiaGocs)
    {
        try
        {
            Double GiaNY = Convert.ToDouble(GiaNYs) / 1000;
            Double GiaGoc = Convert.ToDouble(GiaGocs) / 1000;
            Double HoaHongDaiLy = Convert.ToDouble(Commond.Setting("txtThanhViencuahang"));
            // Nếu thành viên là Đại lý được chiết khấu vào đơn hàng
            Double TongTien = 0;
            TongTien = Convert.ToDouble(GiaNY) - Convert.ToDouble(GiaGoc);

            Double DaiLy = ((TongTien * HoaHongDaiLy) / 100);
            return DaiLy.ToString();
        }
        catch (Exception)
        {
            return "0";
        }
    }
    public static string ThanhVienDaiLy_SoLuong(string SoLuong, string GiaNYs, string GiaGocs)
    {
        try
        {
            Double GiaNY = Convert.ToDouble(GiaNYs) / 1000;
            Double GiaGoc = Convert.ToDouble(GiaGocs) / 1000;
            Double HoaHongDaiLy = Convert.ToDouble(Commond.Setting("txttangthanhvien"));
            // Nếu thành viên là Đại lý được chiết khấu vào đơn hàng
            Double TongTien = 0;
            TongTien = Convert.ToDouble(GiaNY) - Convert.ToDouble(GiaGoc);

            Double DaiLy = (TongTien * HoaHongDaiLy) / 100;
            Double Tong = (DaiLy * Convert.ToDouble(SoLuong));
            return Tong.ToString();
        }
        catch (Exception)
        {
            return "0";
        }
    }

  
    public static string ShowGiaGocs(string id)
    {
        string str = "";
        try
        {
            if (id != "0" || id != "")
            {
                List<Entity.Products> dt = SProducts.GetById(id);
                if (dt.Count >= 1)
                {
                    str = dt[0].GiaThanhVien;// giá này chính là giá gốc (Thật) dựa vào giá này để trả cho công ty thêm điểm khi trừ đi giá nhập ag
                }
            }
        }
        catch (Exception)
        { }
        return str;
    }
    public static string ShowTinhThanh(string id)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string str = "";
        try
        {
            if (id != "0" || id != "")
            {
                var dt = db.Tinhthanhs.Where(s => s.ID == int.Parse(id)).ToList();
                if (dt.Count > 0)
                {
                    str = dt[0].Name;
                }
            }
        }
        catch (Exception)
        { }
        return str;
    }

    #region Modul Product
    public static string ThongBaoDatHang(string IDThanhVien)
    {
        double tongtien = 0.0;
        List<Entity.CartDetail> dt = SCartDetail.Name_Text("select * from CartDetail where IDThanhVien=" + IDThanhVien + " and  TrangThaiKhieuKien=0 and TrangThaiNhaCungCap=3 and TrangThaiNguoiMuaHang=3");
        if (dt.Count > 0)
        {
            for (int i = 0; i < dt.Count; i++)
            {
                tongtien += Convert.ToDouble(dt[i].Money.ToString());
            }
        }
        return tongtien.ToString();
    }
    public static string CongSanPhamDaBan(string id, string Quantity)
    {
        List<Entity.users> detail = Susers.Name_Text("select * from users where iuser_id=" + id + "");
        if (detail.Count > 0)
        {
            double TQuantity = Convert.ToDouble(Quantity.ToString());
            double TTongSoSanPhamDaBan = Convert.ToDouble(detail[0].TongSoSanPhamDaBan.ToString());
            double TTong = TTongSoSanPhamDaBan + TQuantity;
            Susers.Name_Text("Update users set TongSoSanPhamDaBan=" + TTong.ToString() + "  where iuser_id=" + id + "");
        }
        return "";
    }

    public static string ShowPro(string id)
    {
        string str = "";
        try
        {
            if (id != "0" || id != "")
            {
                List<Entity.Products> dt = SProducts.GetById(id);
                if (dt.Count >= 1)
                {
                    str = "<a class='mausp' href=\"http://aggroup365.com/" + dt[0].TangName + "_sp" + dt[0].ipid + ".html\" target=\"_blank\">" + dt[0].Name + "</a>";
                }
            }
        }
        catch (Exception)
        { }
        return str;
    }

    public static string ShowTrangThaiSanPham(int Status, string IDThanhVien)
    {
        List<Entity.Products> dt = SProducts.Name_Text("SELECT * FROM products where IDThanhVien=" + IDThanhVien + "");
        if (dt.Count > 0)
        {
            if (dt[0].Status == Status)
            {
                return dt.Count().ToString();
            }
        }
        return "0";
    }

    public static string ShowSanPhamNCC(string IDThanhVien)
    {
        string str = "";
        List<Entity.Products> dt = SProducts.Name_Text("SELECT * FROM products where IDThanhVien=" + IDThanhVien + "");
        if (dt.Count > 0)
        {
            foreach (var item in dt)
            {
                str += "<span style='color:#ffa903'>" + Commond.ShowNhomSanPham(item.icid.ToString()) + "</span> - ";
                str += "<b style='color:red'>" + item.Name.ToString() + "</b> - ";
                str += "<span style='color:#5cbed7'>";
                if (item.Phaply.ToString() == "1")
                {
                    str += "Tôi là nhà sản xuất.";
                }
                else if (item.Phaply.ToString() == "2")
                {
                    str += "Tôi nhà phân phối.";
                }
                else if (item.Phaply.ToString() == "3")
                {
                    str += "Tôi là đại lý.";
                }
                else if (item.Phaply.ToString() == "4")
                {
                    str += "Tôi là đối tượng khác. ";
                }
                str += "</span>";
                str += "<br />";
            }
        }
        return str;
    }



    public static string ShowTongSanPhamNhaCungCap(string IDThanhVien)
    {
        string str = "0";
        List<Entity.Products> dt = SProducts.Name_Text("SELECT * FROM products where IDThanhVien=" + IDThanhVien + "");
        if (dt.Count > 0)
        {
            return dt.Count().ToString();
        }
        return str;
    }

    public static string Sql_Product()
    {
        return "GiaCuaHang,KichHoatDaiLy,IDThanhVien,DiemMuaHang,GiaThanhVien,GiaThanhVienFree,GiaChietKhauDaiLy,ChietKhau,PhanTramChietKhauDaiLy,PhanTramChietKhauThanhVien,Alt,ipid,icid,TangName,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code,Giacongtynhapvao";
    }
    public static string ShowMTrees(string id)
    {
        List<Entity.users> dt = Susers.GET_BY_ID(id);
        if (dt.Count >= 1)
        {
            return dt[0].MTree;
        }
        return id;
    }

    public static string EXLShowtThanhVien(string id)
    {
        string str = "";
        List<Entity.users> dt = Susers.GET_BY_ID(id);
        if (dt.Count >= 1)
        {
            if (dt[0].vfname.ToString().Length > 0)
            {
                str += dt[0].vfname + " - (Level:" + dt[0].LevelThanhVien + ") - ";
            }
            if (dt[0].vphone.ToString().Length > 0)
            {
                str += dt[0].vphone;
            }
        }
        else
        {
            str = "Không tìm thấy thành viên";
        }
        return str;
    }

    #region Hiển thị bao nhiêu sản phẩm ở nhóm trang chủ
    public static string HomePage()
    {
        return Commond.Setting("HomePage");
    }
    #endregion

    // Điểm tích lũy tính ở phần mua giỏ hàng
    // sửa lấy giá bán cho đại lý - giá công ty nhập vào = điểm
    public static string DiemTichLuyAdd(string GiaThanhVien, string Giacongtynhapvao)
    {
        if (GiaThanhVien.Length > 0 && Giacongtynhapvao.Length > 0)
        {
            if (Convert.ToDouble(GiaThanhVien.ToString()) > Convert.ToDouble(Giacongtynhapvao.ToString()))
            {
                double Prices = Convert.ToDouble(Giacongtynhapvao.ToString());
                double GiaThanhViens = Convert.ToDouble(GiaThanhVien.ToString());
                double Tong = (GiaThanhViens - Prices) / 1000;
                if (Tong != 0)
                {
                    return Tong.ToString();
                }
            }
        }
        return "0";
    }
    // điểm tích lũy ở giao diện hiển thị sản phẩm
    // sửa lấy giá bán cho đại lý - giá công ty nhập vào = điểm
    public static string DiemTichLuy(string GiaThanhVien, string Giacongtynhapvao)
    {
        if (GiaThanhVien.Length > 0 && Giacongtynhapvao.Length > 0)
        {
            if (Convert.ToDouble(GiaThanhVien.ToString()) > Convert.ToDouble(Giacongtynhapvao.ToString()))
            {
                double Prices = Convert.ToDouble(Giacongtynhapvao.ToString());
                double GiaThanhViens = Convert.ToDouble(GiaThanhVien.ToString());
                double Tong = (GiaThanhViens - Prices) / 1000;
                if (Tong != 0)
                {
                    return "<div class=\"DiemMuaHang ViewThanhVien\">Nhận ngay " + Tong.ToString() + " điểm thưởng</div>";
                }
            }
        }
        return "";
    }
    public static string Giamgia(string Price, string GiaThanhVien)
    {
        string Width = "";
        if (GiaThanhVien.Length > 0 && Price.Length > 0)
        {
            if (Convert.ToDouble(Price.ToString()) > Convert.ToDouble(GiaThanhVien.ToString()))
            {
                double cu = Convert.ToDouble(Price.ToString());
                double hientai = Convert.ToDouble(GiaThanhVien.ToString());
                double Tong = (((cu - hientai) / cu) * 100);
                Tong = System.Math.Round(Tong, 0);
                if (Tong != 0)
                {
                    Width += "<div class=\"sale-flash new\"> - " + Tong.ToString() + "% </div>";
                }
            }
        }
        return Width.ToString();
    }
    public static string AddEdit_Giamgia(string Price, string GiaThanhVien)
    {
        string Width = "0";
        if (GiaThanhVien.Length > 0 && Price.Length > 0)
        {
            if (Convert.ToDouble(Price.ToString()) > Convert.ToDouble(GiaThanhVien.ToString()))
            {
                double cu = Convert.ToDouble(Price.ToString());
                double hientai = Convert.ToDouble(GiaThanhVien.ToString());
                double Tong = (((cu - hientai) / cu) * 100);
                Tong = System.Math.Round(Tong, 0);
                if (Tong != 0)
                {
                    Width = Tong.ToString();
                }
            }
        }
        return Width.ToString();
    }


    //public static string Giamgia(string Price, string GiaThanhVien, string OldPrice)
    //{
    //    string Width = "";
    //    if (MoreAll.MoreAll.GetCookies("Members") != "")
    //    {
    //        if (GiaThanhVien.Length > 0 && OldPrice.Length > 0)
    //        {
    //            if (Convert.ToDouble(OldPrice.ToString()) > Convert.ToDouble(GiaThanhVien.ToString()))
    //            {
    //                double cu = Convert.ToDouble(OldPrice.ToString());
    //                double hientai = Convert.ToDouble(GiaThanhVien.ToString());
    //                double Tong = (((cu - hientai) / cu) * 100);
    //                Tong = System.Math.Round(Tong, 0);
    //                if (Tong != 0)
    //                {
    //                    Width += "<div class=\"price-sale-flash f-left\"> - " + Tong.ToString() + "% </div>";
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (Price.Length > 0 && OldPrice.Length > 0)
    //        {
    //            if (Convert.ToDouble(OldPrice.ToString()) > Convert.ToDouble(Price.ToString()))
    //            {
    //                double cu = Convert.ToDouble(OldPrice.ToString());
    //                double hientai = Convert.ToDouble(Price.ToString());
    //                double Tong = (((cu - hientai) / cu) * 100);
    //                Tong = System.Math.Round(Tong, 0);
    //                if (Tong != 0)
    //                {
    //                    Width += "<div class=\"price-sale-flash f-left\"> - " + Tong.ToString() + "% </div>";
    //                }
    //            }
    //        }
    //    }
    //    return Width.ToString();
    //}
    //public static string Giamgias(string Price, string GiaThanhVien, string OldPrice)
    //{
    //    string Width = "";
    //    if (MoreAll.MoreAll.GetCookies("Members") != "")
    //    {
    //        if (GiaThanhVien.Length > 0 && OldPrice.Length > 0)
    //        {
    //            if (Convert.ToDouble(OldPrice.ToString()) > Convert.ToDouble(GiaThanhVien.ToString()))
    //            {
    //                double cu = Convert.ToDouble(OldPrice.ToString());
    //                double hientai = Convert.ToDouble(GiaThanhVien.ToString());
    //                double Tong = (((cu - hientai) / cu) * 100);
    //                Tong = System.Math.Round(Tong, 0);
    //                if (Tong != 0)
    //                {
    //                    Width += "<div class=\"pricessale\"> - " + Tong.ToString() + "% </div>";
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (Price.Length > 0 && OldPrice.Length > 0)
    //        {
    //            if (Convert.ToDouble(OldPrice.ToString()) > Convert.ToDouble(Price.ToString()))
    //            {
    //                double cu = Convert.ToDouble(OldPrice.ToString());
    //                double hientai = Convert.ToDouble(Price.ToString());
    //                double Tong = (((cu - hientai) / cu) * 100);
    //                Tong = System.Math.Round(Tong, 0);
    //                if (Tong != 0)
    //                {
    //                    Width += "<div class=\"pricessale\"> - " + Tong.ToString() + "% </div>";
    //                }
    //            }
    //        }
    //    }
    //    return Width.ToString();
    //}

    public static string LoadProductList_NoiBatMenu(IEnumerable<Entity.Category_Product> product)
    {
        StringBuilder str = new StringBuilder();
        foreach (Entity.Category_Product item in product)
        {
            str.Append("<div class=\"product-mini-item clearfix on-sale\">");
            str.Append(" <a class=\"product-img\" href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Image_Title_Alts_Css("Noibats", item.ImagesSmall.ToString(), item.Name.ToString(), item.Alt.ToString()) + "</a>");
            str.Append("<div class=\"product-info\">");
            str.Append("<h3>");
            str.Append("<a  class=\"product-name\" href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Substring_Title(item.Name.ToString()) + "</a>");
            str.Append("</h3>");
            str.Append("<div class=\"price-box\">");
            str.Append("<span class=\"price f-left\">");
            str.Append("<span class=\"price product-price\">" + AllQuery.MorePro.FormatMoney(item.Price.ToString()) + "</span>");
            str.Append("</span>");
            str.Append("<!-- Giá Khuyến mại -->");
            str.Append("<span class=\"old-price f-left\">");
            str.Append("<del class=\"sale-price\">" + AllQuery.MorePro.Detail_Price(item.OldPrice.ToString()) + "</del>");
            str.Append("</span>");
            str.Append("<!-- Giá gốc -->");
            //  str.Append(Giamgia(item.Price.ToString(), item.GiaThanhVien.ToString(), item.OldPrice.ToString());
            str.Append("</div>");
            str.Append("</div>");
            str.Append("</div>");
        }
        return str.ToString();
    }

    public static string LoadProductList_Home_Cate(IEnumerable<Entity.Category_Product> product)
    {
        StringBuilder str = new StringBuilder();
        foreach (Entity.Category_Product item in product)
        {
            str.Append("<div class=\"col-xs-6 col-sm-4 col-md-4 col-lg-3 Nhomtrangchu\">");
            str.Append("<div class=\"product-box\">");
            str.Append("<div class=\"product-thumbnail\">");
           // str.Append("<div class=\"sale-flash new\"> <img src=\"/Resources/images/kmai.gif\" /> </div>");
            //   str.Append("    <div class=\"sale-flash new\">-100</div>");
            // str.Append(Giamgia(item.Price.ToString(), item.Giacongtynhapvao.ToString()));
            str.Append(" <a  href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Image_Title_Alts(item.ImagesSmall.ToString(), item.Name.ToString(), item.Alt.ToString()) + "</a>");
            str.Append("</div>");
            str.Append("<div class=\"product-info a-left\">");
            str.Append("    <h3 class=\"product-name\">");
            str.Append("      <a  href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Substring_Title(item.Name.ToString()) + "</a>");
            str.Append("    </h3>");
            // if (MoreAll.MoreAll.GetCookies("Members") != "")
            //{
            // str.Append(DiemTichLuy(item.GiaThanhVien.ToString(), item.Giacongtynhapvao.ToString()));
            //}
            str.Append("    <div class=\"price-box clearfix\">");
            str.Append("    <div class=\"special-price f-left\">");

            str.Append("        <div class=\"price product-price giathanh \">" + AllQuery.MorePro.FormatMoney(item.Price.ToString()) + "</div>");

            if (item.GiaThanhVienFree.ToString() != "0")
            {
                str.Append("        <div class=\"price product-price giathanh giathanhvien \">Thành viên (Thưởng): " + AllQuery.MorePro.FormatMoney_TV(item.GiaThanhVienFree.ToString()) + " </div>");
                str.Append("        <div class=\"price product-price giathanh giathanhvien AnGiathanhvienfree_daily\">Giá đại lý :" + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(item.GiaChietKhauDaiLy.ToString(), item.Price.ToString())) + "</div>");
            }
            else
            {
                str.Append("        <div class=\"price product-price giathanh giathanhvien \">Thành viên (Thưởng): " + AllQuery.MorePro.FormatMoney_TV(Commond.ThanhVienFree(item.Price.ToString(), item.GiaThanhVien.ToString())) + " </div>");
                str.Append("        <div class=\"price product-price giathanh giathanhvien AnGiathanhvienfree_daily\">Giá đại lý : " + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(Commond.ThanhVienDaiLy(item.Price.ToString(), item.GiaThanhVien.ToString()), item.Price.ToString())) + "</div>");
            }

            if (item.GiaCuaHang.ToString() != "0")
            {
                str.Append("<div class=\"price product-price giathanh giathanhvien giacuahang\">Giá cửa hàng:" + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(item.GiaCuaHang.ToString(), item.Price.ToString())) + "</div>");
            }
            else
            {
                str.Append("<div class=\"price product-price giathanh giathanhvien giacuahang\">Giá cửa hàng: " + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(Commond.ThanhVienCuaHang(item.Price.ToString(), item.GiaThanhVien.ToString()), item.Price.ToString())) + "</div>");
            }

            str.Append("    </div>");
            str.Append("    </div>");
            str.Append("</div>");

            //  str.Append("<div class=\"product-action clearfix\">");
            //  str.Append("<div class=\"variants form-nut-grid\" >");
            //  str.Append("<a  class=\"muahangnhanh\" href='/cms/Display/Products/AddToCart.aspx?ipid=" + item.ipid.ToString() + "' title=\"" + item.Name + "\"> Mua ngay</a>");
            //  //str.Append("<a data-title=\"Yêu thích\" class=\"btn btn-gray iWishAdd iwishAddWrapper\" href=\"javascript:;\">");
            //  //str.Append("    <i class=\"fa fa-heart\"></i>");
            //  //str.Append("</a>");
            //  //str.Append("<a data-title=\"Bỏ yêu thích\" class=\"btn btn-gray iWishAdded iwishAddWrapper iWishHidden\" href=\"javascript:;\" >");
            //  //str.Append("    <i class=\"fa fa-heart\"></i>");
            //  //str.Append("</a>");
            //  // str.Append("<a data-title=\"Xem nhanh\" href=\"\" data-handle=\"iphone-8\" class=\"btn-gray btn_view btn  right-to quick-view\">");
            //  // str.Append("   ");
            //  // str.Append("</a>");
            ////  str.Append(" <a href=\"javascript:void(0)\" rel=\"popuprel3\" class=\"btn-gray btn_view btn  right-to quick-view popup\" onclick=\"Xemnhanh(" + item.ipid.ToString() + ",'" + item.Name + "')\"> <i class=\"fa fa-search-plus\"></i></a>");
            //  str.Append("</div>");
            //  str.Append("</div>");

            str.Append("</div>");
            str.Append("</div>");
        }
        return str.ToString();
    }
    public static string LoadProductList_Home(IEnumerable<Entity.Category_Product> product)
    {
        StringBuilder str = new StringBuilder();
        foreach (Entity.Category_Product item in product)
        {
            str.Append("<div class=\"item\">");
            str.Append("<div class=\"product-box\">");
            str.Append("<div class=\"product-thumbnail\">");
            //str.Append("<div class=\"sale-flash new\"> <img src=\"/Resources/images/kmai.gif\" /> </div>");
            //   str.Append("    <div class=\"sale-flash new\">-100</div>");
            /// str.Append(Giamgia(item.Price.ToString(), item.GiaThanhVien.ToString()));
            str.Append(" <a  href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Image_Title_Alts(item.ImagesSmall.ToString(), item.Name.ToString(), item.Alt.ToString()) + "</a>");
            str.Append("</div>");
            str.Append("<div class=\"product-info a-left\">");
            str.Append("    <h3 class=\"product-name\">");
            str.Append("      <a  href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Substring_Title(item.Name.ToString()) + "</a>");
            str.Append("    </h3>");
            // if (MoreAll.MoreAll.GetCookies("Members") != "")
            //{
            //str.Append(DiemTichLuy(item.GiaThanhVien.ToString(), item.Giacongtynhapvao.ToString()));
            //}
            str.Append("    <div class=\"price-box clearfix\">");
            str.Append("    <div class=\"special-price f-left\">");

            str.Append("        <div class=\"price product-price giathanh \">" + AllQuery.MorePro.FormatMoney(item.Price.ToString()) + "</div>");

            if (item.GiaThanhVienFree.ToString() != "0")
            {
                str.Append("        <div class=\"price product-price giathanh giathanhvien \">Thành viên (Thưởng): " + AllQuery.MorePro.FormatMoney_TV(item.GiaThanhVienFree.ToString()) + " </div>");
                str.Append("        <div class=\"price product-price giathanh giathanhvien AnGiathanhvienfree_daily\">Giá đại lý :" + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(item.GiaChietKhauDaiLy.ToString(), item.Price.ToString())) + "</div>");
            }
            else
            {
                str.Append("        <div class=\"price product-price giathanh giathanhvien \">Thành viên (Thưởng): " + AllQuery.MorePro.FormatMoney_TV(Commond.ThanhVienFree(item.Price.ToString(), item.GiaThanhVien.ToString())) + " </div>");
                str.Append("        <div class=\"price product-price giathanh giathanhvien AnGiathanhvienfree_daily\">Giá đại lý : " + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(Commond.ThanhVienDaiLy(item.Price.ToString(), item.GiaThanhVien.ToString()), item.Price.ToString())) + "</div>");
            }

            if (item.GiaCuaHang.ToString() != "0")
            {
                str.Append("<div class=\"price product-price giathanh giathanhvien giacuahang\">Giá cửa hàng:" + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(item.GiaCuaHang.ToString(), item.Price.ToString())) + "</div>");
            }
            else
            {
                str.Append("<div class=\"price product-price giathanh giathanhvien giacuahang\">Giá cửa hàng: " + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(Commond.ThanhVienCuaHang(item.Price.ToString(), item.GiaThanhVien.ToString()), item.Price.ToString())) + "</div>");
            }

            str.Append("    </div>");
            str.Append("    </div>");
            str.Append("</div>");

            // str.Append("<div class=\"product-action clearfix\">");
            // str.Append("<div class=\"variants form-nut-grid\" >");
            // str.Append("<a  class=\"muahangnhanh\" href='/cms/Display/Products/AddToCart.aspx?ipid=" + item.ipid.ToString() + "' title=\"" + item.Name + "\"> Mua ngay</a>");
            // //str.Append("<a data-title=\"Yêu thích\" class=\"btn btn-gray iWishAdd iwishAddWrapper\" href=\"javascript:;\">");
            // //str.Append("    <i class=\"fa fa-heart\"></i>");
            // //str.Append("</a>");
            // //str.Append("<a data-title=\"Bỏ yêu thích\" class=\"btn btn-gray iWishAdded iwishAddWrapper iWishHidden\" href=\"javascript:;\" >");
            // //str.Append("    <i class=\"fa fa-heart\"></i>");
            // //str.Append("</a>");
            // // str.Append("<a data-title=\"Xem nhanh\" href=\"\" data-handle=\"iphone-8\" class=\"btn-gray btn_view btn  right-to quick-view\">");
            // // str.Append("   ");
            // // str.Append("</a>");
            //// str.Append(" <a href=\"javascript:void(0)\" rel=\"popuprel3\" class=\"btn-gray btn_view btn  right-to quick-view popup\" onclick=\"Xemnhanh(" + item.ipid.ToString() + ",'" + item.Name + "')\"> <i class=\"fa fa-search-plus\"></i></a>");
            // str.Append("</div>");
            // str.Append("</div>");

            str.Append("</div>");
            str.Append("</div>");
        }
        return str.ToString();
    }
    public static string LoadProductList(IEnumerable<Entity.Category_Product> product)
    {
        StringBuilder str = new StringBuilder();
        foreach (Entity.Category_Product item in product)
        {
            str.Append("<div class=\"col-xs-6 col-sm-4 col-md-4 col-lg-3 LoadProductList\">");
            str.Append("<div class=\"product-box\">");
            str.Append("<div class=\"product-thumbnail\">");
            //str.Append("<div class=\"sale-flash new\"> <img src=\"/Resources/images/kmai.gif\" /> </div>");
            /// str.Append(Giamgia(item.Price.ToString(), item.GiaThanhVien.ToString()));
            str.Append(" <a  href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Image_Title_Alts(item.ImagesSmall.ToString(), item.Name.ToString(), item.Name.ToString()) + "</a>");
            str.Append("</div>");
            str.Append("<div class=\"product-info a-left\">");
            str.Append("    <h3 class=\"product-name\">");
            str.Append("      <a  href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Substring_Title(item.Name.ToString()) + "</a>");
            str.Append("    </h3>");
            // if (MoreAll.MoreAll.GetCookies("Members") != "")
            // {
            ///  str.Append(DiemTichLuy(item.GiaThanhVien.ToString(), item.Giacongtynhapvao.ToString()));
            //}
            str.Append("    <div class=\"price-box clearfix\">");
            str.Append("    <div class=\"special-price f-left\">");


            str.Append("        <div class=\"price product-price giathanh \">" + AllQuery.MorePro.FormatMoney(item.Price.ToString()) + "</div>");
            if (item.GiaThanhVienFree.ToString() != "0")
            {
                str.Append("        <div class=\"price product-price giathanh giathanhvien \">Thành viên (Thưởng): " + AllQuery.MorePro.FormatMoney_TV(item.GiaThanhVienFree.ToString()) + " </div>");
                str.Append("        <div class=\"price product-price giathanh giathanhvien AnGiathanhvienfree_daily\">Giá đại lý :" + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(item.GiaChietKhauDaiLy.ToString(), item.Price.ToString())) + "</div>");
            }
            else
            {
                str.Append("        <div class=\"price product-price giathanh giathanhvien \">Thành viên (Thưởng): " + AllQuery.MorePro.FormatMoney_TV(Commond.ThanhVienFree(item.Price.ToString(), item.GiaThanhVien.ToString())) + " </div>");
                str.Append("        <div class=\"price product-price giathanh giathanhvien AnGiathanhvienfree_daily\">Giá đại lý : " + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(Commond.ThanhVienDaiLy(item.Price.ToString(), item.GiaThanhVien.ToString()), item.Price.ToString())) + "</div>");
            }

            if (item.GiaCuaHang.ToString() != "0")
            {
                str.Append("<div class=\"price product-price giathanh giathanhvien giacuahang\">Giá cửa hàng:" + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(item.GiaCuaHang.ToString(), item.Price.ToString())) + "</div>");
            }
            else
            {
                str.Append("<div class=\"price product-price giathanh giathanhvien giacuahang\">Giá cửa hàng: " + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(Commond.ThanhVienCuaHang(item.Price.ToString(), item.GiaThanhVien.ToString()), item.Price.ToString())) + "</div>");
            }
            // str.Append("        <span class=\"price product-price-old giacu\">" + AllQuery.MorePro.Detail_Price(item.OldPrice.ToString()) + "</span>");
            //if (item.GiaThanhVien.Length > 0)
            //{
            //    str.Append("<span class=\"price product-price giathanh giathanhvien\">" + AllQuery.MorePro.FormatMoney_ThanhVien(item.GiaThanhVien.ToString()) + "</span>");
            //}
            str.Append("    </div>");
            str.Append("    </div>");
            str.Append("</div>");

            //str.Append("<div class=\"product-action clearfix\">");
            //str.Append("<div class=\"variants form-nut-grid\" >");
            //str.Append("<a  class=\"muahangnhanh\" href='/cms/Display/Products/AddToCart.aspx?ipid=" + item.ipid.ToString() + "' title=\"" + item.Name + "\"> Mua ngay</a>");
            ////str.Append("<a data-title=\"Yêu thích\" class=\"btn btn-gray iWishAdd iwishAddWrapper\" href=\"javascript:;\">");
            ////str.Append("    <i class=\"fa fa-heart\"></i>");
            ////str.Append("</a>");
            ////str.Append("<a data-title=\"Bỏ yêu thích\" class=\"btn btn-gray iWishAdded iwishAddWrapper iWishHidden\" href=\"javascript:;\" >");
            ////str.Append("    <i class=\"fa fa-heart\"></i>");
            ////str.Append("</a>");
            //// str.Append("<a data-title=\"Xem nhanh\" href=\"\" data-handle=\"iphone-8\" class=\"btn-gray btn_view btn  right-to quick-view\">");
            //// str.Append("   ");
            //// str.Append("</a>");
            ////str.Append(" <a href=\"javascript:void(0)\" rel=\"popuprel3\" class=\"btn-gray btn_view btn  right-to quick-view popup\" onclick=\"Xemnhanh(" + item.ipid.ToString() + ",'" + item.Name + "')\"> <i class=\"fa fa-search-plus\"></i></a>");
            //str.Append("</div>");
            //str.Append("</div>");


            str.Append("</div>");
            str.Append("</div>");
        }
        return str.ToString();
    }
    public static string LoadProductList_Other(IEnumerable<Entity.Category_Product> product)
    {
        StringBuilder str = new StringBuilder();
        foreach (Entity.Category_Product item in product)
        {
            str.Append("<div class=\"col-xs-6 col-sm-4 col-md-4 col-lg-3 Otherrr\">");
            str.Append("<div class=\"product-box\">");
            str.Append("<div class=\"product-thumbnail\">");
            //str.Append("<div class=\"sale-flash new\"> <img src=\"/Resources/images/kmai.gif\" /> </div>");
            /// str.Append(Giamgia(item.Price.ToString(), item.GiaThanhVien.ToString()));
            str.Append(" <a  href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Image_Title_Alts(item.ImagesSmall.ToString(), item.Name.ToString(), item.Alt.ToString()) + "</a>");
            str.Append("</div>");
            str.Append("<div class=\"product-info a-left\">");
            str.Append("    <h3 class=\"product-name\">");
            str.Append("      <a  href='/" + item.TangName + "_sp" + item.ipid + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Substring_Title(item.Name.ToString()) + "</a>");
            str.Append("    </h3>");
            // if (MoreAll.MoreAll.GetCookies("Members") != "")
            //{
            //  str.Append(DiemTichLuy(item.GiaThanhVien.ToString(), item.Giacongtynhapvao.ToString()));
            // }
            str.Append("    <div class=\"price-box clearfix\">");
            str.Append("    <div class=\"special-price f-left\">");


            str.Append("        <div class=\"price product-price giathanh \">" + AllQuery.MorePro.FormatMoney(item.Price.ToString()) + "</div>");

            if (item.GiaThanhVienFree.ToString() != "0")
            {
                str.Append("        <div class=\"price product-price giathanh giathanhvien \">Thành viên (Thưởng): " + AllQuery.MorePro.FormatMoney_TV(item.GiaThanhVienFree.ToString()) + " </div>");
                str.Append("        <div class=\"price product-price giathanh giathanhvien AnGiathanhvienfree_daily\">Giá đại lý :" + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(item.GiaChietKhauDaiLy.ToString(), item.Price.ToString())) + "</div>");
            }
            else
            {
                str.Append("        <div class=\"price product-price giathanh giathanhvien \">Thành viên (Thưởng): " + AllQuery.MorePro.FormatMoney_TV(Commond.ThanhVienFree(item.Price.ToString(), item.GiaThanhVien.ToString())) + " </div>");
                str.Append("        <div class=\"price product-price giathanh giathanhvien AnGiathanhvienfree_daily\">Giá đại lý : " + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(Commond.ThanhVienDaiLy(item.Price.ToString(), item.GiaThanhVien.ToString()), item.Price.ToString())) + "</div>");
            }

            if (item.GiaCuaHang.ToString() != "0")
            {
                str.Append("<div class=\"price product-price giathanh giathanhvien giacuahang\">Giá cửa hàng:" + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(item.GiaCuaHang.ToString(), item.Price.ToString())) + "</div>");
            }
            else
            {
                str.Append("<div class=\"price product-price giathanh giathanhvien giacuahang\">Giá cửa hàng: " + AllQuery.MorePro.FormatMoney_VND(GiaDaiLy_FormatMoney(Commond.ThanhVienCuaHang(item.Price.ToString(), item.GiaThanhVien.ToString()), item.Price.ToString())) + "</div>");
            }




            str.Append("    </div>");
            str.Append("    </div>");
            str.Append("</div>");

            // str.Append("<div class=\"product-action clearfix\">");
            // str.Append("<div class=\"variants form-nut-grid\" >");
            // str.Append("<a  class=\"muahangnhanh\" href='/cms/Display/Products/AddToCart.aspx?ipid=" + item.ipid.ToString() + "' title=\"" + item.Name + "\"> Mua ngay</a>");
            // //str.Append("<a data-title=\"Yêu thích\" class=\"btn btn-gray iWishAdd iwishAddWrapper\" href=\"javascript:;\">");
            // //str.Append("    <i class=\"fa fa-heart\"></i>");
            // //str.Append("</a>");
            // //str.Append("<a data-title=\"Bỏ yêu thích\" class=\"btn btn-gray iWishAdded iwishAddWrapper iWishHidden\" href=\"javascript:;\" >");
            // //str.Append("    <i class=\"fa fa-heart\"></i>");
            // //str.Append("</a>");
            // // str.Append("<a data-title=\"Xem nhanh\" href=\"\" data-handle=\"iphone-8\" class=\"btn-gray btn_view btn  right-to quick-view\">");
            // // str.Append("   ");
            // // str.Append("</a>");
            //// str.Append(" <a href=\"javascript:void(0)\" rel=\"popuprel3\" class=\"btn-gray btn_view btn  right-to quick-view popup\" onclick=\"Xemnhanh(" + item.ipid.ToString() + ",'" + item.Name + "')\"> <i class=\"fa fa-search-plus\"></i></a>");
            // str.Append("</div>");
            // str.Append("</div>");

            str.Append("</div>");
            str.Append("</div>");

        }
        return str.ToString();
    }

    #endregion

    #region Modul News

    public static string LoadNewsListXemNhieu(IEnumerable<Entity.Category_News> news, string language)
    {
        StringBuilder str = new StringBuilder();
        foreach (Entity.Category_News item in news)
        {
            str.Append("<div class=\"loop-blog\">");
            str.Append("<div class=\"thumb-left\">");
            str.Append("<a href=\"/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + AllQuery.MoreNews.Image_Title_Alt_Css_NWH("img-responsive", item.ImagesSmall.ToString(), item.Title.ToString(), item.Alt.ToString()) + "</a>");
            str.Append("</div>");
            str.Append("<div class=\"name-right\">");
            str.Append("<h3>");
            str.Append("<a href=\"/" + item.TangName + ".html\"  title=\"" + item.Title + "\">" + AllQuery.MoreNews.Substring_Title(item.Title.ToString()) + "</a>");
            str.Append("</h3>");
            str.Append("<div class=\"post-time\">");
            str.Append("<i class=\"fa fa-clock-o\"></i> " + MoreAll.FormatDateTime.FormatDate_Brithday(item.Create_Date.ToString()) + "");
            str.Append("</div>");
            str.Append("</div>");
            str.Append("</div>");
        }
        return str.ToString();
    }

    public static string LoadNewsListHome(IEnumerable<Entity.Category_News> news, string language)
    {
        StringBuilder str = new StringBuilder();
        foreach (Entity.Category_News item in news)
        {
            str.Append("<article class=\"blog-item text-center\">");
            str.Append("<div>");
            str.Append("<div class=\"blog-item-thumbnail\">");
            str.Append("<a href=\"/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + AllQuery.MoreNews.Image_Title_Alt(item.ImagesSmall.ToString(), item.Title.ToString(), item.Alt.ToString()) + "</a>");
            str.Append("</div>");
            str.Append("<div class=\"blog-item-info\">");
            str.Append("<h3 class=\"blog-item-name\">");
            str.Append("<a href=\"/" + item.TangName + ".html\"  title=\"" + item.Title + "\">" + AllQuery.MoreNews.Substring_Title(item.Title.ToString()) + "</a>");
            str.Append("</h3>");
            str.Append("<p class=\"blog-item-summary\">" + AllQuery.MoreNews.Substring_Mota(item.Brief.ToString()) + "</p>");
            str.Append("</div>");
            str.Append("</div>");
            str.Append("</article>");
        }
        return str.ToString();
    }
    public static string LoadNewsList(IEnumerable<Entity.Category_News> news, string language)
    {
        StringBuilder str = new StringBuilder();
        foreach (Entity.Category_News item in news)
        {
            str.Append("<div class=\"col-sm-12\">");
            str.Append("<article class=\"blog-item\">");
            str.Append("<div class=\"row\">");
            str.Append("<div class=\"col-sm-4\">");
            str.Append("<div class=\"blog-item-thumbnail\">");
            str.Append(" <a href=\"/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + AllQuery.MoreNews.Image_Title_Alt(item.ImagesSmall.ToString(), item.Title.ToString(), item.Alt.ToString()) + "</a>");
            str.Append("</div>");
            str.Append("</div>");
            str.Append("<div class=\"col-sm-8\">");
            str.Append("<div class=\"blog-item-info\">");
            str.Append("<h3 class=\"blog-item-name\">");
            str.Append("<a href=\"/" + item.TangName + ".html\"  title=\"" + item.Title + "\">" + AllQuery.MoreNews.Substring_Title(item.Title.ToString()) + "</a>");
            str.Append("</h3>");
            str.Append("<div class=\"post-time\">");
            str.Append("<i class=\"fa fa-clock-o\"></i> " + MoreAll.FormatDateTime.FormatDate_Brithday(item.Create_Date.ToString()) + "");
            str.Append("</div>");
            str.Append("<p class=\"blog-item-summary\">" + AllQuery.MoreNews.Substring_Mota(item.Brief.ToString()) + "</p>");
            str.Append("</div>");
            str.Append("</div>");
            str.Append("</div>");
            str.Append("</article>");
            str.Append("</div>");
        }
        return str.ToString();
    }
    #endregion

    #region ALbum
    public static string LoadALbum_Home(IEnumerable<Entity.Album_RutGon> product)
    {
        string str = "";
        foreach (Entity.Album_RutGon item in product)
        {
            str += "        <li class=\" abcolmd\">";
            str += "            <div class=\"abitem\">";
            str += "                <div class=\"img\">";
            str += "                    <a title=\"" + item.Title + "\" href='/" + item.TangName + ".html'>" + MoreAll.MoreImage.Image_width_height_Title_Alt(item.ImagesSmall.ToString(), "195", "146", item.Title.ToString(), item.Alt.ToString()) + " <div class=\"imghover\"></div>";
            str += "                    </a>";
            str += "                </div>";
            str += "                <div class=\"tiemtitle\">";
            str += "                    <h2><a title=\"" + item.Title + "\" href='/item.TangName.html'>" + item.Title + "</a></h2>";
            str += "                </div>";
            str += "            </div>";
            str += "        </li>";
        }
        return str;
    }

    #endregion

    #region Video
    public static string LoadVideo_Home(IEnumerable<Entity.VideoClip_RutGon> product)
    {
        string str = "";
        foreach (Entity.VideoClip_RutGon item in product)
        {
            str += "<div class=\"vd-item\">";
            str += "<div class=\"img\">";
            str += "    <a title=\"" + item.Title + "\" href=\"/" + item.TangName + ".html\">" + MoreAll.MoreImage.Image_width_height_Title_Alt(item.ImagesSmall.ToString(), "195", "146", item.Title.ToString(), item.Alt.ToString()) + "</a>";
            str += "    <div class=\"pl\"><a title=\"" + item.Title + "\" href=\"/" + item.TangName + ".html\">";
            str += "        <img src=\"/Resources/images/play.png\" /></a></div>";
            str += "</div>";
            str += "<span><a title=\"" + item.Title + "\" href=\"/" + item.TangName + ".html\">" + item.Title + "</a></span>";
            str += "</div>";
        }
        return str;
    }
    #endregion

    #region All More
    public static string Name(string ID) //// Tăng và giảm thứ tự trong ô txtOrders
    {
        DatalinqDataContext db = new DatalinqDataContext();
        Menu table = db.Menus.SingleOrDefault(p => p.ID == int.Parse(ID));
        if (table != null)
        {
            return table.Name.ToString();
        }
        return "";
    }

    public static string RequestMenu(string Request)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string Modul = "";
        ModulControl str = db.ModulControls.SingleOrDefault(p => p.TangName == Request);
        if (str != null)
        {
            Modul = str.Module.ToString();
        }
        return Modul;
    }



    public static string Setting(string giatri)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string item = "";
        Setting str = db.Settings.SingleOrDefault(p => p.Properties == giatri && p.Lang == MoreAll.MoreAll.Language);
        if (str != null)
        {
            item = str.Value;
        }
        return item.ToString();
    }
    public static string SubMenu(string Capp, string cid)
    {
        string submn = cid;
        List<Entity.MenuID> dt = SMenu.Menu_ID(cid, Capp);
        for (int i = 0; i < dt.Count; i++)
        {
            submn = submn + "," + SubMenu(Capp, dt[i].ID.ToString());
        }
        return submn;
    }

    #endregion

    #region Phân trang
    //Old
    //public static string Phantrang(string Url, int Tongsobanghi, Int16 pages)
    //{
    //    string str = "<div class='Phantrang'>";
    //    if (Tongsobanghi > 1)
    //    {
    //        str += "<a href='" + Url + "?page=1'><< Trang đầu</a>";
    //        for (int i = 1; i <= Tongsobanghi; i++)
    //        {
    //            if (i == pages)
    //            {
    //                str += "<a class='pageactive' href=\"" + Url + "?page=" + i + "\">" + i + "</a>";
    //            }
    //            else
    //            {
    //                str += "<a class='page' href=\"" + Url + "?page=" + i + "\">" + i + "</a>";
    //            }
    //        }
    //        str += "<a href='" + Url + "?page=" + Tongsobanghi + "'>Cuối cùng >></a>";
    //    }
    //    str += "</div>";
    //    return str;
    //}
    //News
    public static string PhantrangAdmin(string Url, int Tongsobanghi, int pages)
    {
        string str = "<div class='PhantrangAD'>";
        if (Tongsobanghi > 1)
        {
            str += "<a href='" + Url + "&page=1'><< Trang đầu</a>";
            int startPage;
            if (Tongsobanghi <= 7)
                startPage = 1;
            else if (pages <= 4)
                startPage = 1;
            else if ((Tongsobanghi - pages) >= 3)
                startPage = pages - 3;
            else startPage = Tongsobanghi - 6;
            if (startPage > 1)
                str += string.Format("<a class=\"aso\">...</a>");
            for (int i = startPage; i <= (Tongsobanghi <= 7 ? Tongsobanghi : startPage + 6); i++)
            {
                if (i == pages)
                {
                    str += "<a class='pageactive' href=\"" + Url + "&page=" + i + "\">" + i + "</a>";
                }
                else
                {
                    str += "<a class='page' href=\"" + Url + "&page=" + i + "\">" + i + "</a>";
                }
            }
            if ((Tongsobanghi - pages > 3) && (Tongsobanghi > 7))
                str += string.Format("<a class=\"aso\">...</a>");
            str += "<a href='" + Url + "&page=" + Tongsobanghi + "'>Cuối cùng >></a>";
        }
        str += "</div>";
        return str;
    }

    public static string Phantrang(string Url, int Tongsobanghi, int pages)
    {
        string str = "<div class='Phantrang'>";
        if (Tongsobanghi > 1)
        {
            //if (pages != 1)
            // {
            str += "<a href='" + Url + "?page=1'><< Trang đầu</a>";
            //}
            int startPage;
            if (Tongsobanghi <= 7)
                startPage = 1;
            else if (pages <= 4)
                startPage = 1;
            else if ((Tongsobanghi - pages) >= 3)
                startPage = pages - 3;
            else startPage = Tongsobanghi - 6;
            if (startPage > 1)
                str += string.Format("<a class=\"aso\">...</a>");
            for (int i = startPage; i <= (Tongsobanghi <= 7 ? Tongsobanghi : startPage + 6); i++)
            {
                if (i == pages)
                {
                    str += "<a class='pageactive' href=\"" + Url + "?page=" + i + "\">" + i + "</a>";
                }
                else
                {
                    str += "<a class='page' href=\"" + Url + "?page=" + i + "\">" + i + "</a>";
                }
            }
            if ((Tongsobanghi - pages > 3) && (Tongsobanghi > 7))
                str += string.Format("<a class=\"aso\">...</a>");
            //if (pages < Tongsobanghi)
            // if (Tongsobanghi != pages)
            // {
            str += "<a href='" + Url + "?page=" + Tongsobanghi + "'>Cuối cùng >></a>";
            //}
        }
        str += "</div>";
        return str;
    }
    public static string Phantrang_Sanpham(string Url, string loc, int Tongsobanghi, int pages)
    {
        string str = "<div class='Phantrang'>";
        if (Tongsobanghi > 1)
        {
            //if (pages != 1)
            // {
            str += "<a href='" + Url + "?page=1" + loc + "'><< Trang đầu</a>";
            //}
            int startPage;
            if (Tongsobanghi <= 7)
                startPage = 1;
            else if (pages <= 4)
                startPage = 1;
            else if ((Tongsobanghi - pages) >= 3)
                startPage = pages - 3;
            else startPage = Tongsobanghi - 6;
            if (startPage > 1)
                str += string.Format("<a class=\"aso\">...</a>");
            for (int i = startPage; i <= (Tongsobanghi <= 7 ? Tongsobanghi : startPage + 6); i++)
            {
                if (i == pages)
                {
                    str += "<a class='pageactive' href=\"" + Url + "?page=" + i + "" + loc + "\">" + i + "</a>";
                }
                else
                {
                    str += "<a class='page' href=\"" + Url + "?page=" + i + "" + loc + "\">" + i + "</a>";
                }
            }
            if ((Tongsobanghi - pages > 3) && (Tongsobanghi > 7))
                str += string.Format("<a class=\"aso\">...</a>");
            //if (pages < Tongsobanghi)
            // if (Tongsobanghi != pages)
            // {
            str += "<a href='" + Url + "?page=" + Tongsobanghi + "" + loc + "'>Cuối cùng >></a>";
            //}
        }
        str += "</div>";
        return str;
    }

    public static string Phantrang_loc(string Url, string UrlLoc, int Tongsobanghi, Int16 pages)
    {
        string str = "<div class='Phantrang'>";
        if (Tongsobanghi > 1)
        {
            //if (pages != 1)
            // {
            str += "<a href='" + Url + "?page=1" + UrlLoc + "'><< Trang đầu</a>";
            //}
            int startPage;
            if (Tongsobanghi <= 7)
                startPage = 1;
            else if (pages <= 4)
                startPage = 1;
            else if ((Tongsobanghi - pages) >= 3)
                startPage = pages - 3;
            else startPage = Tongsobanghi - 6;
            if (startPage > 1)
                str += string.Format("<a class=\"aso\">...</a>");
            for (int i = startPage; i <= (Tongsobanghi <= 7 ? Tongsobanghi : startPage + 6); i++)
            {
                if (i == pages)
                {
                    str += "<a class='pageactive' href=\"" + Url + "?page=" + i + "" + UrlLoc + "\">" + i + "</a>";
                }
                else
                {
                    str += "<a class='page' href=\"" + Url + "?page=" + i + "" + UrlLoc + "\">" + i + "</a>";
                }
            }
            if ((Tongsobanghi - pages > 3) && (Tongsobanghi > 7))
                str += string.Format("<a class=\"aso\">...</a>");
            //if (pages < Tongsobanghi)
            // if (Tongsobanghi != pages)
            // {
            str += "<a href='" + Url + "?page=" + Tongsobanghi + "" + UrlLoc + "'>Cuối cùng >></a>";
            //}
        }
        str += "</div>";
        return str;
    }

    #endregion

    #region Truy Vấn Linq
    public static IEnumerable<Menu> Name_Text_Menu(string sql)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        return db.ExecuteQuery<Menu>(@"" + sql + "");
    }
    public static IEnumerable<product> Name_Text_Pro(string sql)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        return db.ExecuteQuery<product>(@"" + sql + "");
    }
    public static IEnumerable<New> Name_Text_News(string sql)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        return db.ExecuteQuery<New>(@"" + sql + "");
    }

    #endregion

    #region Lọc
    public static string GiaTu(string id)
    {
        string str = "";
        DatalinqDataContext db = new DatalinqDataContext();
        Menu table = db.Menus.SingleOrDefault(p => p.ID == int.Parse(id));
        if (table != null)
        {
            str += table.Link.ToString();
        }
        return str.ToString();
    }
    public static string GiaDen(string id)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string str = "";
        Menu table = db.Menus.SingleOrDefault(p => p.ID == int.Parse(id));
        if (table != null)
        {
            str += table.Styleshow.ToString();
        }
        return str.ToString();
    }
    #endregion
    public static string ShowMTree(string id)
    {
        string str = "";
        try
        {
            if (id != "0" || id != "")
            {
                List<Entity.users> dt = Susers.GET_BY_ID(id);
                if (dt.Count >= 1)
                {
                    str = dt[0].MTree;
                }
            }
        }
        catch (Exception)
        { }
        return str;
    }

    public static string DanhSachNhaCungCapSanPham()
    {
        string chuoi = "0";
        List<Entity.ProductDISTINCT> dt = SProducts.Name_TextDISTINCT("SELECT DISTINCT IDThanhVien FROM products order by IDThanhVien asc");
        for (int i = 0; i < dt.Count; i++)
        {
            chuoi = chuoi + "," + dt[i].IDThanhVien.ToString();
        }
        return chuoi;
    }

    public static string ShowsThanhVien(string id)
    {
        string str = "";
        List<Entity.users> dt = Susers.GET_BY_ID(id);
        if (dt.Count >= 1)
        {
            // str += "<div>ID Thành viên: " + dt[0].iuser_id.ToString() + "</div>";
            str += "<div>ID Thành viên: <b>" + dt[0].vuserun.ToString() + " </b></div>";
            str += "<div>Họ và tên:  <b>" + dt[0].vfname.ToString() + " </b></div>";
            str += "<div>Điện thoại:  <b>" + dt[0].vphone.ToString() + " </b></div>";
            str += "<div>Email:  <b>" + dt[0].vemail.ToString() + " </b></div>";
            str += "<div>Địa chỉ:  <b>" + dt[0].vaddress.ToString() + " </b></div>";
        }
        else
        {
            str = "Không tìm thấy thành viên";
        }
        return str;
    }
    public static string ShowNhomSanPham(string id)
    {
        string str = "";
        List<Entity.Menu> dt = SMenu.GETBYID(id);
        if (dt.Count >= 1)
        {
            str = dt[0].Name.ToString();
        }
        return str;
    }

    public static string CapBacChietKhauDaiLy(string Chietkhau)
    {
        string KQ = "0";
        Double Soluong = Convert.ToDouble(Chietkhau);
        if (Soluong.ToString().Length > 0)
        {
            if (Soluong >= 50)
            {
                KQ = "10";// Thanhvieen 30
            }
            if (Soluong >= 40 && Soluong <= 49)
            {
                KQ = "8";// 20
            }
            else if (Soluong >= 30 && Soluong <= 39)
            {
                KQ = "5";// 10
            }
            else if (Soluong >= 20 && Soluong <= 29)
            {
                KQ = "2";// 5
            }
            else if (Soluong >= 10 && Soluong < 19)
            {
                KQ = "2";// 2
            }
            else if (Soluong < 10)
            {
                KQ = "1";// 1
            }
        }
        return KQ;
    }

    public static string CapBacChietKhauThanhVien(string Chietkhau)
    {
        string KQ = "0";
        Double Soluong = Convert.ToDouble(Chietkhau);
        if (Soluong.ToString().Length > 0)
        {
            if (Soluong >= 50)
            {
                KQ = "30";// Thanhvieen 30
            }
            if (Soluong >= 40 && Soluong <= 49)
            {
                KQ = "20";// 20
            }
            else if (Soluong >= 30 && Soluong <= 39)
            {
                KQ = "10";// 10
            }
            else if (Soluong >= 20 && Soluong <= 29)
            {
                KQ = "5";// 5
            }
            else if (Soluong >= 10 && Soluong < 19)
            {
                KQ = "2";// 2
            }
            else if (Soluong < 10)
            {
                KQ = "1";// 1
            }
        }
        return KQ;
    }

    public static string ThanhVienExel(string id)
    {
        string str = "";
        List<Entity.users> dt = Susers.GET_BY_ID(id);
        if (dt.Count >= 1)
        {
            if (dt[0].vfname.ToString().Length > 0)
            {
                str += dt[0].vfname + " (" + dt[0].vuserun + ") " + " (" + dt[0].vphone + ")";
            }
        }
        return str;
    }
}
