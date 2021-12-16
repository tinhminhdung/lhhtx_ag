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

public class SinhHHBatDongSan
{
    public static string VanPhongChiNhanh()
    {
        string bc = System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.RawUrl.ToString();
        if (!bc.Contains("localhost"))
        {
            return "3";
        }
        return "39";//????????????
    }
    public static string DongHuong()
    {
        // 9443 là tạm thời để test còn ID chính là : 67357
        string bc = System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.RawUrl.ToString();
        if (!bc.Contains("localhost"))
        {
            return "4";
        }
        return "40";
    }
    //public static string BanDieuHanh()
    //{
    //    // 9443 là tạm thời để test còn ID chính là : 67357
    //    string bc = System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.RawUrl.ToString();
    //    if (!bc.Contains("localhost"))
    //    {
    //        return "29";
    //    }
    //    return "4";
    //}

    public static string HoaHongF(string IDThanhVien, string IDNguoiBan, string IDDonHang, string Tien)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        Double TongTienDaChia = 0;
        double TienDauVao = Convert.ToDouble(Tien);

        #region F1 - Trực tiếp - Người bán
        List<Entity.users> F1 = Susers.GET_BY_ID(IDNguoiBan);
        if (F1.Count > 0)
        {
            ChiaHoaHongBatDongSan.CongTienTongTienDaMua(IDThanhVien, TienDauVao.ToString());
            ChiaHoaHongBatDongSan.CapNhatTrangThai(IDThanhVien);

            #region Số lần bán được sản phẩm
            Susers.Name_Text("update users set DaBanDuocSanPham=DaBanDuocSanPham+1 where iuser_id=" + IDNguoiBan.ToString() + "");
            #endregion

            #region F1
            List<Entity.users> F2 = Susers.GET_BY_ID(IDThanhVien.ToString());
            if (F2.Count > 0)
            {
                if (F2[0].iuser_id.ToString() != "0")
                {
                    double HoaHongF1 = Convert.ToDouble(Commond.Setting("HHTrucTiep"));
                    double TienHoaHongF2 = (TienDauVao * HoaHongF1) / 100;
                    if (HoaHongF1 != 0)
                    {
                        if (TrangThaiThuPhi(F2[0].iuser_id.ToString()) == true)
                        {
                            TongTienDaChia += TienHoaHongF2;
                            ChiaHoaHongBatDongSan.ThemHoaHong("0", "500", "Hoa hồng trực tiếp", IDThanhVien.Trim(), F2[0].GioiThieu.ToString(), HoaHongF1.ToString(), TienHoaHongF2.ToString(), IDDonHang.ToString(), "");
                        }
                    }
                    //
                    if (F2[0].GioiThieu.ToString() != "0")
                    {
                        List<Entity.users> FGianTiep = Susers.GET_BY_ID(F2[0].GioiThieu.ToString());
                        if (FGianTiep.Count > 0)
                        {
                            if (FGianTiep[0].iuser_id.ToString() != "0")
                            {
                                double HoaHongGT = Convert.ToDouble(Commond.Setting("GianTiepBDS"));
                                if (HoaHongGT != 0)
                                {
                                    double TienHoaHongGT = (TienHoaHongF2 * HoaHongGT) / 100;
                                    if (TrangThaiThuPhi(F2[0].iuser_id.ToString()) == true)
                                    {
                                        TongTienDaChia += TienHoaHongGT;
                                        ChiaHoaHongBatDongSan.ThemHoaHong("0", "504", "Hoa hồng gián tiếp", IDThanhVien.Trim(), FGianTiep[0].GioiThieu.ToString(), HoaHongGT.ToString(), TienHoaHongGT.ToString(), IDDonHang.ToString(), "");
                                    }
                                }
                                //GianTiepBDS
                            }
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region VanPhongChiNhanh
        List<Entity.users> vp = Susers.GET_BY_ID(VanPhongChiNhanh());
        if (vp.Count > 0)
        {
            double HoaHongVP = Convert.ToDouble(Commond.Setting("VanPhongChiNhanh"));
            double TienHoaHongVP = (TienDauVao * HoaHongVP) / 100;
            if (TrangThaiThuPhi(VanPhongChiNhanh()) == true)
            {
                if (HoaHongVP != 0)
                {
                    TongTienDaChia += TienHoaHongVP;
                    ChiaHoaHongBatDongSan.ThemHoaHong("0", "501", "Văn Phòng", IDThanhVien.Trim(), VanPhongChiNhanh(), HoaHongVP.ToString(), TienHoaHongVP.ToString(), IDDonHang.ToString(), "");
                }
            }
        }
        #endregion

        #region BanDaoTao
        List<Entity.users> DT = Susers.GET_BY_ID(DongHuong());
        if (DT.Count > 0)
        {
            double HoaHongDT = Convert.ToDouble(Commond.Setting("DongHuong"));
            double TienHoaHongDT = (TienDauVao * HoaHongDT) / 100;
            if (TrangThaiThuPhi(DongHuong()) == true)
            {
                if (HoaHongDT != 0)
                {
                    TongTienDaChia += TienHoaHongDT;
                    ChiaHoaHongBatDongSan.ThemHoaHong("0", "502", "Đồng hưởng", IDThanhVien.Trim(), DongHuong(), HoaHongDT.ToString(), TienHoaHongDT.ToString(), IDDonHang.ToString(), "");
                }
            }
        }
        #endregion

        // Hoa Hồng Cấp Quản Lý
        // Tìm trong dây của mình xem có ai được trưởng nhóm kinh doanh không thì cho hh
        // Có 5 F1 đã đóng phí là được và đã bán dc 1 sản phẩm DaBanDuocSanPham
        // Tìm ông gần nhất để cho 

        ChiaHoaHongBatDongSan.ChiaHoaHongCapBacs(IDThanhVien.ToString(), TienDauVao.ToString(), IDDonHang.ToString());

        #region Vi Loi Nhuan sau khi da chia HH
        try
        {
            Double TongTienHoaHongDaChiaCapBac = Convert.ToDouble(ChiaHoaHongBatDongSan.KiemTraTongTienHoaHongDaChiaCapBac(IDDonHang, IDThanhVien));

            Double TongTiens = Convert.ToDouble(TongTienDaChia.ToString());
            Double TongTienss = Convert.ToDouble(TienDauVao.ToString());
            Double TongCong = (TongTienss - TongTiens - TongTienHoaHongDaChiaCapBac);
            Double TongTienDaChias = Convert.ToDouble(TongTienDaChia.ToString()) + TongTienHoaHongDaChiaCapBac;

            LoiNhuanMuaBan_BatDongSan abln = new LoiNhuanMuaBan_BatDongSan();
            abln.IDThanhVienMua = int.Parse(IDThanhVien.ToString());
            abln.IDDonHang = IDDonHang.ToString();
            abln.MoTa = "";
            abln.NgayTao = DateTime.Now;
            abln.TongTien = TienDauVao.ToString();
            abln.TongTienCon = TongCong.ToString();
            abln.TongTienDaChia = TongTienDaChias.ToString();
            abln.MTreeIDThanhVienMua = Commond.ShowMTree(IDThanhVien.ToString());
            abln.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
            db.LoiNhuanMuaBan_BatDongSans.InsertOnSubmit(abln);
            db.SubmitChanges();

        }
        catch (Exception)
        { }

        #endregion
        return "";
    }
    public static string TimTruongNhomGanNhat(string id)
    {
        string str = "0";
        List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + " ");
        if (dt.Count > 0)
        {
            if (dt[0].LevelThanhVien.ToString() == "1")
            {
                return dt[0].iuser_id.ToString();
            }
            else
            {
                str = dt[0].GioiThieu.ToString();
                return TimTruongNhomGanNhat(str);
            }
        }
        return str;
    }
    public static string TimTruongPhongGanNhat(string id)
    {
        string str = "0";
        List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + " ");
        if (dt.Count > 0)
        {
            if (dt[0].LevelThanhVien.ToString() == "2")
            {
                return dt[0].iuser_id.ToString();
            }
            else
            {
                str = dt[0].GioiThieu.ToString();
                return TimTruongPhongGanNhat(str);
            }
        }
        return str;
    }

    public static string TimGiamDocGanNhat(string id)
    {
        string str = "0";
        List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + " ");
        if (dt.Count > 0)
        {
            if (dt[0].LevelThanhVien.ToString() == "3")
            {
                return dt[0].iuser_id.ToString();
            }
            else
            {
                str = dt[0].GioiThieu.ToString();
                return TimGiamDocGanNhat(str);
            }
        }
        return str;
    }

    public static bool TrangThaiThuPhi(string IDThanhVienHuong)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + IDThanhVienHuong.ToString() + "");//DaKichHoat
        if (F1.Count() > 0)
        {
            return true;
        }
        return false;
    }



}
