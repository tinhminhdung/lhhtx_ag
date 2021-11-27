using Entity;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VS.E_Commerce;

public class ChiaHoaHongBatDongSan
{
    public static string ChiaHoaHongCapBacs(string IDThanhVien, string Tongtien, string IDDonHang)
    {
        string Plevel = "0";
        string chuoi1 = "0";
        string TongLevel = "0";
        System.Web.HttpContext.Current.Session["TongPT"] = "";
        #region Cộng điểm theo hoa hồng

        List<Entity.users> item = Susers.Name_Text("select * from users where iuser_id=" + IDThanhVien.ToString() + "");
        if (item.Count > 0)
        {
            int i = 0;
            string LevelThanhVien = item[0].LevelThanhVien.ToString();
            Susers.Name_Text("update users set IDLuuTam='0'  where iuser_id=" + IDThanhVien.ToString() + "");
            string chuyendoi = item[0].MTree.ToString();
            chuyendoi = chuyendoi.Replace("|", ",") + "0";
            chuyendoi = "0" + chuyendoi;
            chuoi1 = chuyendoi.ToString();
            List<Entity.users> iitems = Susers.Name_Text("select * from users where iuser_id in (" + chuyendoi.ToString() + ") and LevelThanhVien!=0   order by iuser_id desc ");
            if (iitems.Count > 0)
            {
                foreach (var obj in iitems)
                {
                    Plevel = Plevel + "," + LoaID(IDThanhVien);
                    TongLevel = MinAndMax(Plevel.ToString());
                    Double Stop = Convert.ToDouble(ChiaHH(TongLevel, obj.LevelThanhVien.ToString(), IDThanhVien.ToString(), obj.iuser_id.ToString(), Tongtien, IDDonHang));
                    if (Stop == 1)
                    {
                        break;
                    }

                    // kiểm tra trong tổng hh được chia chỉ được = giám đốc kinh doanh cấp cao
                    #region DungChay
                    Double TongHoaHong = Convert.ToDouble(KiemTraTongHoaHongDaChia(IDDonHang));
                    Double DungChay = 0;
                    DungChay = Convert.ToDouble(Commond.Setting("GiamDocKinhDoanh"));
                    if (TongHoaHong >= DungChay)
                    {
                        break;
                    }
                    #endregion
                }
            }
        }
        #endregion
        return "";
    }
    public static string KiemTraTongHoaHongDaChia(string IDDonHang)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        double num = 0.0;
        List<HoaHongThanhVien> items = db.HoaHongThanhViens.Where(s => (s.IDCart) == int.Parse(IDDonHang) && s.IDType == 503).ToList();
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                num += Convert.ToDouble(items[i].PhamTramHoaHong.ToString());
            }
        }
        return num.ToString();
    }
    public static string LoaID(string IDThanhVien)
    {
        List<Entity.users> items = Susers.Name_Text("select * from users where iuser_id=" + IDThanhVien.ToString() + "");
        if (items.Count > 0)
        {
            return items[0].IDLuuTam.ToString();
        }
        return "0";
    }
    public static string UpdateLoaID(string LevelThanhVien, string IDThanhVien)
    {
        Susers.Name_Text("update users set IDLuuTam='" + LevelThanhVien.ToString() + "'  where iuser_id=" + IDThanhVien.ToString() + "");
        return "";
    }
    public static string ChiaHH(string TongLevel, string TLevelThanhVien, string IDThanhVienMuaHang, string IDThanhViens, string Money, string IDCart)
    {
        Double Cap1 = 0;
        Double Cap2 = 0;
        Double Cap3 = 0;
        Double Cap4 = 0;
        Double Cap5 = 0;

        Cap1 = Convert.ToDouble(Commond.Setting("Nhanvien"));
        Cap2 = Convert.ToDouble(Commond.Setting("TruongNhomKinhDoanh"));
        Cap3 = Convert.ToDouble(Commond.Setting("TruongPhongKinhDoanh"));
        Cap4 = Convert.ToDouble(Commond.Setting("PhoGiamDoc"));
        Cap5 = Convert.ToDouble(Commond.Setting("GiamDocKinhDoanh"));

        double TienDauVao = Convert.ToDouble(Money);
        Double TongTien = 0;
        string LevelThanhVien = "";
        #region hoa hồng
        List<Entity.users> items = Susers.Name_Text("select * from users where iuser_id=" + IDThanhViens.ToString() + "");
        if (items.Count > 0)
        {
            double Tongphantram = Convert.ToDouble(items[0].LevelThanhVien.ToString());
            //Convert.ToDouble(PhanTramLevel(TongLevel, items[0].LevelThanhVien.ToString()));
            if (Tongphantram > 0)
            {
                Double Tru = 0;
                string PhanTram = "0" + System.Web.HttpContext.Current.Session["TongPT"].ToString();
                if (PhanTram != "")
                {
                    string[] strArray = PhanTram.ToString().Split(new char[] { ',' });
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        Tru = Convert.ToDouble(strArray[i].ToString());
                        Tongphantram = (Tongphantram - Tru);
                    }
                }
            }
            double PTHH = 0;
            if (Tongphantram > 0)
            {
                System.Web.HttpContext.Current.Session["TongPT"] += "," + Tongphantram;
                #region Tongphantram Mang đi chia
                // số 1,2,3,4 này chính là cấp bậc 1,2,3,4 tương ứng với drop trong quản lý thành viên nhé
                if (Tongphantram == 1)
                {
                    PTHH = Cap1;
                }
                if (Tongphantram == 2)
                {
                    PTHH = Cap2;
                }
                if (Tongphantram == 3)
                {
                    PTHH = Cap3;
                }
                if (Tongphantram == 4)
                {
                    PTHH = Cap4;
                }
                if (Tongphantram == 5)
                {
                    PTHH = Cap5;
                }
                #endregion
                #region TLevelThanhVien
                if (TLevelThanhVien == "1")
                {
                    LevelThanhVien = "Nhân viên KD";
                }
                if (TLevelThanhVien == "2")
                {
                    LevelThanhVien = "Trưởng nhóm kinh doanh";
                }
                if (TLevelThanhVien == "3")
                {
                    LevelThanhVien = "Trưởng phòng kinh doanh";
                }
                if (TLevelThanhVien == "4")
                {
                    LevelThanhVien = "Phó giám đốc";
                }
                if (TLevelThanhVien == "5")
                {
                    LevelThanhVien = "Giám đốc kinh doanh";
                }
                #endregion
                if (PTHH > 0)
                {
                    double TongHHTT = (TienDauVao * PTHH) / 100;
                    ThemHoaHong("0", "503", LevelThanhVien, IDThanhVienMuaHang.Trim(), items[0].iuser_id.ToString(), PTHH.ToString(), TongHHTT.ToString(), IDCart.ToString(), "");
                    TongTien += TongHHTT;
                    UpdateLoaID(Tongphantram.ToString(), IDThanhVienMuaHang);
                }
            }
            if (Tongphantram >= 5)
            {
                return "1";
            }
            //if (Tongphantram < 0)
            //{
            //    return "1";
            //}
        }
        #endregion
        return "0";
    }
    public static string PhanTramLevel(string ThanhVienA, string ThanhVienB)
    {
        // Lấy trên trừ dưới level A - level B
        Double CBThanhVienA = Convert.ToDouble(ThanhVienA);
        Double CBThanhVienB = Convert.ToDouble(ThanhVienB);
        double TongCong = Convert.ToDouble(CBThanhVienB - CBThanhVienA);
        return TongCong.ToString();
    }

    #region Tìm giá trị lớn nhất trong level để thưởng cho các đời F1 đến F5
    public static string MinAndMax(string c)
    {
        String intString = c.Replace("99999999999,", "");
        int[] strArray = stringArrayToIntArray(intString);
        int max = strArray[0];
        if (strArray.Length > 0)
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] > max)
                {
                    max = strArray[i];
                }
            }
        }
        if (max.ToString() == "0")
        {
            return "0";
        }
        else
        {
            return max.ToString();
        }
        return "0";
    }
    public static int[] stringArrayToIntArray(String intString)
    {
        String[] intStringSplit = intString.Trim().Split(new char[] { ',' });
        int[] result = new int[intStringSplit.Length]; //Used to store our ints

        for (int i = 0; i < intStringSplit.Length; i++)
        {
            result[i] = int.Parse(intStringSplit[i]);
        }
        return result;
    }
    #endregion;

    public static string KiemTraTongTienHoaHongDaChiaCapBac(string IDDonHang, string IDThanhVien)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        Double num = 0;
        List<HoaHongThanhVien> items = db.HoaHongThanhViens.Where(s => s.IDCart == int.Parse(IDDonHang) && s.IDType == 503 && s.IDThanhVien == int.Parse(IDThanhVien)).ToList();// xóa nhiều
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                num += Convert.ToDouble(items[i].SoCoin.ToString());
            }
        }
        return num.ToString();
    }

    public static string ThanhVienGioiTHieu(string IDThanhVien)
    {
        List<Entity.users> data = Susers.Name_Text("select * from users where GioiThieu=" + IDThanhVien.ToString() + "  ");
        if (data.Count() > 0)
        {
            return data.Count().ToString();
        }
        return "0";
    }
    public static string ThanhVienGioiTHieuChuaKich(string IDThanhVien)
    {
        List<Entity.users> data = Susers.Name_Text("select * from users where GioiThieu=" + IDThanhVien.ToString() + " ");
        if (data.Count() > 0)
        {
            return data.Count().ToString();
        }
        return "0";
    }


    // ThemHoaHong("10", LevelThanhVien, TienDauVao.ToString(), TongHHTT.ToString(), PTHH.ToString(), IDThanhVienMuaHang.ToString(), items[0].ID.ToString(), IDCart);

    public static void ThemHoaHong(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDCart, string NoiDung)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
        Commond.CheckNgayHetHan(IDUserNguoiDuocHuong.ToString());
        #endregion

        List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and DuyetTienDanap=1 ");
        if (F1.Count() > 0 || (IDUserNguoiDuocHuong == "0") || IDThanhVien == IDUserNguoiDuocHuong)
        {
            #region HoaHongThanhVien
            HoaHongThanhVien obj = new HoaHongThanhVien();
            obj.IDProducts = int.Parse(IDProducts);
            obj.IDType = int.Parse(IDType);
            obj.Type = Type;
            obj.IDThanhVien = int.Parse(IDThanhVien);
            obj.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
            obj.PhamTramHoaHong = PhamTramHoaHong;
            obj.SoCoin = SoCoin.ToString();
            obj.NgayTao = DateTime.Now;
            obj.TrangThai = 1;
            obj.NoiDung = Commond.ShowPro(NoiDung);
            obj.IDCart = Convert.ToInt64(IDCart);
            db.HoaHongThanhViens.InsertOnSubmit(obj);
            db.SubmitChanges();
            #endregion


            #region LichSuGiaoDich
            LichSuGiaoDich obl = new LichSuGiaoDich();
            obl.IDProducts = int.Parse(IDProducts);
            obl.IDType = int.Parse(IDType);
            obl.Type = Type;
            obl.IDThanhVien = int.Parse(IDThanhVien);
            obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
            obl.PhamTramHoaHong = PhamTramHoaHong;
            obl.SoCoin = SoCoin.ToString();
            obl.NgayTao = DateTime.Now;
            obl.NoiDung = Commond.ShowPro(NoiDung);
            obl.IDCart = Convert.ToInt64(IDCart);
            db.LichSuGiaoDiches.InsertOnSubmit(obl);
            db.SubmitChanges();
            #endregion
            try
            {
                CapNhatTrangThai(IDUserNguoiDuocHuong);
            }
            catch (Exception)
            { }
            CongTien(IDType, IDUserNguoiDuocHuong, SoCoin);

        }
    }
    // sau khi xóa code ViTienHHGioiThieu thì dùng lại code này nhé
    public static void CongTien(string Type, string IDUserNguoiDuocHuong, string SoCoin)
    {
        #region Cộng điểm  theo hoa hồng coin vào bảng thành viên để tích điểm trong các trường hợp mua hàng cần
        List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + "");
        if (iitem != null)
        {
            double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViHoaHongMuaBan);
            double TongTienNapVao = Convert.ToDouble(SoCoin);
            double Conglai = 0;
            Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
            Susers.Name_Text("update users set ViHoaHongMuaBan=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
        }
        #endregion
    }
    public static void CongTienTongTienDaMua(string IDThanhVien, string SoTien)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        #region Cộng điểm theo hoa hồng
        List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDThanhVien.ToString() + "");
        if (iitem != null)
        {
            double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienDaMua);
            double TongTienNapVao = Convert.ToDouble(SoTien);
            double Conglai = 0;
            Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
            Susers.Name_Text("update users set TongTienDaMua='" + Conglai.ToString() + "'  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
        }
        #endregion
    }


    public static void CapNhatTrangThai(string IDThanhVien)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + IDThanhVien.ToString() + "   ");//DaKichHoat
        if (F1.Count() > 0)
        {

            #region  Doanh số từ mình trở xuống đạt 10 triệu lên nhân viên KD
            if (Other.Giatri("NhanVienTongTien") != "" && Other.Giatri("NhanVienTongTien") != "0")
            {
                Double TongTienC1 = 0;
                List<Entity.users> Cap1 = Susers.Name_Text("SELECT * FROM users  WHERE  ((MTree LIKE N'%|" + IDThanhVien + "|%')) and LevelThanhVien!=1  ");
                if (Cap1.Count > 0)
                {
                    foreach (var item in Cap1)
                    {
                        TongTienC1 += Convert.ToDouble(item.TongTienDaMua);
                    }
                }
                if (TongTienC1 >= Convert.ToDouble(Other.Giatri("NhanVienTongTien")))
                {
                    if (Other.Giatri("NhanVienTimRa1F1") != "" && Other.Giatri("NhanVienTimRa1F1") != "0")// đếm F1
                    {
                        List<Entity.users> Cap22 = Susers.Name_Text("SELECT top " + Other.Giatri("NhanVienTimRa1F1") + " *  FROM users  WHERE GioiThieu=" + IDThanhVien + " ");
                        if (Cap22.Count > 0)
                        {
                            if (Convert.ToDouble(Cap22.Count) >= Convert.ToDouble(Other.Giatri("NhanVienTimRa1F1")))
                                Susers.Name_Text("update users set LevelThanhVien=1 where iuser_id=" + IDThanhVien.ToString() + "");
                        }
                    }
                }
            }
            #endregion

            #region Ds đạt 1 tỷ + tuyển 2 nhân viên kd thì lên trưởng nhóm
            if (Other.Giatri("TruongNhomKDTongTien") != "" && Other.Giatri("TruongNhomKDTongTien") != "0")
            {
                Double TongTienC2 = 0;
                List<Entity.users> Cap1 = Susers.Name_Text("SELECT * FROM users  WHERE  ((MTRee LIKE N'%|" + IDThanhVien + "|%')) and LevelThanhVien!=2   ");
                if (Cap1.Count > 0)
                {
                    foreach (var item in Cap1)
                    {
                        TongTienC2 += Convert.ToDouble(item.TongTienDaMua);
                    }
                    if (TongTienC2 >= Convert.ToDouble(Other.Giatri("TruongNhomKDTongTien")))
                    {
                        if (Other.Giatri("TruongNhomKDF1") != "" && Other.Giatri("TruongNhomKDF1") != "0")// đếm F1
                        {
                            List<Entity.users> Cap22 = Susers.Name_Text("SELECT top " + Other.Giatri("TruongNhomKDF1") + " *  FROM users  WHERE  GioiThieu=" + IDThanhVien + "  ");
                            if (Cap22.Count > 0)
                            {
                                if (Convert.ToDouble(Cap22.Count) >= Convert.ToDouble(Other.Giatri("TruongNhomKDF1")))
                                    Susers.Name_Text("update users set LevelThanhVien=2 where iuser_id=" + IDThanhVien.ToString() + "");
                            }

                        }
                    }
                }
            }
            #endregion

            #region Ds đạt 5 tỷ + 2 trưởng nhóm dưới thì lên trưởng phòng kd
            if (Other.Giatri("TruongNhomKDTongTien") != "" && Other.Giatri("TruongPhongKinhDoanhTongTien") != "0")
            {
                Double TongTienC3 = 0;
                List<Entity.users> Cap1 = Susers.Name_Text("SELECT * FROM users  WHERE  ((MTree LIKE N'%|" + IDThanhVien + "|%')) and LevelThanhVien!=3  ");
                if (Cap1.Count > 0)
                {
                    foreach (var item in Cap1)
                    {
                        TongTienC3 += Convert.ToDouble(item.TongTienDaMua);
                    }
                    if (TongTienC3 >= Convert.ToDouble(Other.Giatri("TruongPhongKinhDoanhTongTien")))
                    {
                        if (Other.Giatri("TruongPhongKinhDoanhF1") != "" && Other.Giatri("TruongPhongKinhDoanhF1") != "0")// đếm F1
                        {
                            List<Entity.users> Cap22 = Susers.Name_Text("SELECT top " + Other.Giatri("TruongPhongKinhDoanhF1") + " *  FROM users  WHERE  ((MTree LIKE N'%|" + IDThanhVien + "|%')) and ID!=" + IDThanhVien + " and LevelThanhVien=2 ");
                            if (Cap22.Count > 0)
                            {
                                if (Convert.ToDouble(Cap22.Count) >= Convert.ToDouble(Other.Giatri("TruongPhongKinhDoanhF1")))
                                    Susers.Name_Text("update users set LevelThanhVien=3 where iuser_id=" + IDThanhVien.ToString() + " ");
                            }

                        }
                    }
                }
            }
            #endregion

            #region Ds đạt 20 tỷ + 2 trưởng phòng dưới thì lên phó GĐ
            if (Other.Giatri("PhoGiamDocTien") != "" && Other.Giatri("PhoGiamDocTien") != "0")
            {
                Double TongTienC4 = 0;
                List<Entity.users> Cap1 = Susers.Name_Text("SELECT * FROM users  WHERE  ((MTree LIKE N'%|" + IDThanhVien + "|%')) and LevelThanhVien!=4  ");
                if (Cap1.Count > 0)
                {
                    foreach (var item in Cap1)
                    {
                        TongTienC4 += Convert.ToDouble(item.TongTienDaMua);
                    }
                    if (TongTienC4 >= Convert.ToDouble(Other.Giatri("PhoGiamDocTien")))
                    {
                        if (Other.Giatri("PhoGiamDocF1") != "" && Other.Giatri("PhoGiamDocF1") != "0")// đếm F1
                        {
                            List<Entity.users> Cap22 = Susers.Name_Text("SELECT top " + Other.Giatri("PhoGiamDocF1") + " *  FROM users  WHERE  ((MTree LIKE N'%|" + IDThanhVien + "|%')) and ID!=" + IDThanhVien + "   and LevelThanhVien=3");
                            if (Cap22.Count > 0)
                            {
                                if (Convert.ToDouble(Cap22.Count) >= Convert.ToDouble(Other.Giatri("PhoGiamDocF1")))
                                    Susers.Name_Text("update users set LevelThanhVien=4 where iuser_id=" + IDThanhVien.ToString() + "");
                            }

                        }
                    }
                }
            }
            #endregion

            #region Ds đạt 50 tỷ + 2 phó GĐ dưới thì lên GĐ
            if (Other.Giatri("GiamDocKinhDoanhTien") != "" && Other.Giatri("GiamDocKinhDoanhTien") != "0")
            {
                Double TongTienC5 = 0;
                List<Entity.users> Cap1 = Susers.Name_Text("SELECT * FROM users  WHERE  ((MTree LIKE N'%|" + IDThanhVien + "|%')) and LevelThanhVien!=5  ");
                if (Cap1.Count > 0)
                {
                    foreach (var item in Cap1)
                    {
                        TongTienC5 += Convert.ToDouble(item.TongTienDaMua);
                    }
                    if (TongTienC5 >= Convert.ToDouble(Other.Giatri("GiamDocKinhDoanhTien")))
                    {
                        if (Other.Giatri("GiamDocKinhDoanhF1") != "" && Other.Giatri("GiamDocKinhDoanhF1") != "0")// đếm F1
                        {
                            List<Entity.users> Cap22 = Susers.Name_Text("SELECT top " + Other.Giatri("GiamDocKinhDoanhF1") + " *  FROM users  WHERE  ((MTree LIKE N'%|" + IDThanhVien + "|%')) and ID!=" + IDThanhVien + " and LevelThanhVien=4");
                            if (Cap22.Count > 0)
                            {
                                if (Convert.ToDouble(Cap22.Count) >= Convert.ToDouble(Other.Giatri("GiamDocKinhDoanhF1")))
                                    Susers.Name_Text("update users set LevelThanhVien=5 where iuser_id=" + IDThanhVien.ToString() + "");
                            }

                        }
                    }
                }
            }
            #endregion
        }
    }
}


