using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWindowService;

namespace VS.E_Commerce
{
    public partial class ThanhVienLevel : System.Web.UI.Page
    {
        protected bool Dung = false;
        DatalinqDataContext db = new DatalinqDataContext();
        string Plevel = "99999999999";
        string TongLevel = "0";
        string Diemcoin = "480";
        string Gioithieu = "26";
        string IDMaDonTao = MoreAll.MoreAll.FormatDate_ID(DateTime.Now);
        string IDThanhVien = "87";

        protected void Page_Load(object sender, EventArgs e)
        {
            //#region Hoa hồng cấp quản lý và F1
            //#region HHF1
            //List<Entity.users> F00 = Susers.Name_Text("select * from users  where iuser_id=" + IDThanhVien + " ");
            //if (F00.Count() > 0)
            //{
            //    if (!F00[0].GioiThieu.Equals("0") && F00[0].DuyetTienDanap.ToString() != "0")
            //    {
            //        #region Hoa Hồng cho người giới thiệu trực tiếp  F1 30%
            //        double HoaHongTrucTiep = Convert.ToDouble(Commond.Setting("hoahonggttructiep"));
            //        ThemHoaHong("0", "1", "Hoa hồng quản lý 1", IDThanhVien, F00[0].GioiThieu.ToString(), HoaHongTrucTiep.ToString(), HoaHongTrucTiep.ToString(), IDMaDonTao, "");
            //        #endregion
            //    }
            //}

            //#endregion
            //user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien));
            //if (table != null)
            //{
            //    #region Hoa Hồng Gián tiếp F1
            //    if (table.GioiThieu.ToString() != "0")
            //    {
            //        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
            //        try
            //        {
            //            if (table.LevelThanhVien.ToString() == "5")
            //            {
            //                Dung = false;
            //                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //            }
            //            else
            //            {
            //                Dung = true;
            //                Plevel = Plevel + "," + table.LevelThanhVien.ToString();
            //                ThemHoaHong_ThuongLevel("0", "F1", "3", IDThanhVien, table.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(table.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                #region Dừng nếu gặp lelvel5
            //                string leveeeel = TimLevelB(table.GioiThieu.ToString());
            //                if (leveeeel == "5")
            //                {
            //                    Plevel = "45";
            //                }
            //                #endregion
            //            }
            //        }
            //        catch (Exception)
            //        { }
            //        #endregion
            //    }
            //    #region Hoa Hồng Gián tiếp F2
            //    user tableTVTF2 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(table.GioiThieu.ToString()));
            //    if (tableTVTF2 != null)
            //    {
            //        if (tableTVTF2.GioiThieu.ToString() != "0")
            //        {
            //            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
            //            // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
            //            try
            //            {
            //                if (ShowF2(tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                {
            //                    Dung = false;
            //                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                }
            //                else
            //                {
            //                    Dung = true;
            //                    Plevel = Plevel + "," + ShowF2(tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                }
            //                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                if (Plevel.ToString() == "99999999999")
            //                {

            //                }
            //                else
            //                {
            //                    TongLevel = MinAndMax(Plevel.ToString());
            //                }
            //                if (Dung == true)
            //                {
            //                    if (TongLevel != "8")// 8 là ko tìm thấy giá trị nào cao hơn 0 ở hàm MinAndMax
            //                    {
            //                        if (TongLevel != "45")// 45 là ko hưởng hoa hồng nữa
            //                        {
            //                            ThemHoaHong_ThuongLevel("0", "F2", "3", IDThanhVien, tableTVTF2.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                        }
            //                    }
            //                    else
            //                    {
            //                        ThemHoaHong_ThuongLevel("0", "F2", "3", IDThanhVien, tableTVTF2.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                    }
            //                    #region Dừng nếu gặp lelvel5
            //                    string leveeeel = TimLevelB(tableTVTF2.GioiThieu.ToString());
            //                    if (leveeeel == "5")
            //                    {
            //                        Plevel = "45";
            //                    }
            //                    #endregion

            //                }
            //            }
            //            catch (Exception)
            //            { }
            //            #endregion
            //        }

            //        #region Hoa Hồng Gián tiếp F3
            //        user tableTVTF3 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF2.GioiThieu.ToString()));
            //        if (tableTVTF3 != null)
            //        {
            //            double TongDiemF3 = 0;
            //            if (tableTVTF3.GioiThieu.ToString() != "0")
            //            {
            //                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
            //                try
            //                {
            //                    if (ShowF3(tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                    {
            //                        Dung = false;
            //                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                    }
            //                    else
            //                    {
            //                        Dung = true;
            //                        Plevel = Plevel + "," + ShowF3(tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                    }
            //                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                    if (Plevel.ToString() == "99999999999")
            //                    {

            //                    }
            //                    else
            //                    {
            //                        TongLevel = MinAndMax(Plevel.ToString());
            //                    }
            //                    if (Dung == true)
            //                    {
            //                        if (TongLevel != "8")
            //                        {
            //                            if (TongLevel != "45")
            //                            {
            //                                ThemHoaHong_ThuongLevel("0", "F3", "3", IDThanhVien, tableTVTF3.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ThemHoaHong_ThuongLevel("0", "F3", "3", IDThanhVien, tableTVTF3.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                        }
            //                        #region Dừng nếu gặp lelvel5
            //                        string leveeeel = TimLevelB(tableTVTF3.GioiThieu.ToString());
            //                        if (leveeeel == "5")
            //                        {
            //                            Plevel = "45";
            //                        }
            //                        #endregion
            //                    }
            //                }
            //                catch (Exception)
            //                { }
            //                #endregion

            //            }
            //            #region Hoa Hồng Gián tiếp F4
            //            user tableTVTF4 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF3.GioiThieu.ToString()));
            //            if (tableTVTF4 != null)
            //            {
            //                if (tableTVTF4.GioiThieu.ToString() != "0")
            //                {
            //                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
            //                    try
            //                    {
            //                        if (ShowF4(tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                        {
            //                            Dung = false;
            //                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                        }
            //                        else
            //                        {
            //                            Dung = true;
            //                            Plevel = Plevel + "," + ShowF4(tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                        }
            //                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                        if (Plevel.ToString() == "99999999999")
            //                        {

            //                        }
            //                        else
            //                        {
            //                            TongLevel = MinAndMax(Plevel.ToString());
            //                        }
            //                        if (Dung == true)
            //                        {
            //                            if (TongLevel != "8")
            //                            {
            //                                if (TongLevel != "45")
            //                                {
            //                                    ThemHoaHong_ThuongLevel("0", "F4", "3", IDThanhVien, tableTVTF4.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                }
            //                            }
            //                            else
            //                            {
            //                                ThemHoaHong_ThuongLevel("0", "F4", "3", IDThanhVien, tableTVTF4.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                            }
            //                            #region Dừng nếu gặp lelvel5
            //                            string leveeeel = TimLevelB(tableTVTF4.GioiThieu.ToString());
            //                            if (leveeeel == "5")
            //                            {
            //                                Plevel = "45";
            //                            }
            //                            #endregion
            //                        }
            //                    }
            //                    catch (Exception)
            //                    { }
            //                    #endregion
            //                }

            //                #region Hoa Hồng Gián tiếp F5
            //                user tableTVTF5 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF4.GioiThieu.ToString()));
            //                if (tableTVTF5 != null)
            //                {
            //                    if (tableTVTF5.GioiThieu.ToString() != "0")
            //                    {
            //                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
            //                        // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
            //                        try
            //                        {
            //                            if (ShowF5(tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                            {
            //                                Dung = false;
            //                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                            }
            //                            else
            //                            {
            //                                Dung = true;
            //                                Plevel = Plevel + "," + ShowF5(tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                            }
            //                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                            if (Plevel.ToString() == "99999999999")
            //                            {

            //                            }
            //                            else
            //                            {
            //                                TongLevel = MinAndMax(Plevel.ToString());
            //                            }
            //                            if (Dung == true)
            //                            {
            //                                if (TongLevel != "8")
            //                                {
            //                                    if (TongLevel != "45")
            //                                    {
            //                                        ThemHoaHong_ThuongLevel("0", "F5", "3", IDThanhVien, tableTVTF5.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    ThemHoaHong_ThuongLevel("0", "F5", "3", IDThanhVien, tableTVTF5.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                }
            //                                #region Dừng nếu gặp lelvel5
            //                                string leveeeel = TimLevelB(tableTVTF5.GioiThieu.ToString());
            //                                if (leveeeel == "5")
            //                                {
            //                                    Plevel = "45";
            //                                }
            //                                #endregion
            //                            }
            //                        }
            //                        catch (Exception)
            //                        { }
            //                        #endregion
            //                    }

            //                    #region Hoa Hồng Gián tiếp F6
            //                    user tableTVTF6 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF5.GioiThieu.ToString()));
            //                    if (tableTVTF6 != null)
            //                    {
            //                        if (tableTVTF6.GioiThieu.ToString() != "0")
            //                        {
            //                            try
            //                            {
            //                                if (ShowFQRcode.ShowF6(tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                {
            //                                    Dung = false;
            //                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                }
            //                                else
            //                                {
            //                                    Dung = true;
            //                                    Plevel = Plevel + "," + ShowFQRcode.ShowF6(tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                }
            //                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                if (Plevel.ToString() == "99999999999")
            //                                {

            //                                }
            //                                else
            //                                {
            //                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                }
            //                                if (Dung == true)
            //                                {
            //                                    if (TongLevel != "8")
            //                                    {
            //                                        if (TongLevel != "45")
            //                                        {
            //                                            ThemHoaHong_ThuongLevel("0", "F6", "3", IDThanhVien, tableTVTF6.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        ThemHoaHong_ThuongLevel("0", "F6", "3", IDThanhVien, tableTVTF6.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                    }
            //                                    #region Dừng nếu gặp lelvel5
            //                                    string leveeeel = TimLevelB(tableTVTF6.GioiThieu.ToString());
            //                                    if (leveeeel == "5")
            //                                    {
            //                                        Plevel = "45";
            //                                    }
            //                                    #endregion
            //                                }
            //                            }
            //                            catch (Exception)
            //                            { }
            //                        }
            //                        #region Hoa Hồng Gián tiếp F7
            //                        user tableTVTF7 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF6.GioiThieu.ToString()));
            //                        if (tableTVTF7 != null)
            //                        {
            //                            if (tableTVTF7.GioiThieu.ToString() != "0")
            //                            {
            //                                try
            //                                {
            //                                    if (ShowFQRcode.ShowF7(tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                    {
            //                                        Dung = false;
            //                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                    }
            //                                    else
            //                                    {
            //                                        Dung = true;
            //                                        Plevel = Plevel + "," + ShowFQRcode.ShowF7(tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                    }
            //                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                    if (Plevel.ToString() == "99999999999")
            //                                    {

            //                                    }
            //                                    else
            //                                    {
            //                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                    }
            //                                    if (Dung == true)
            //                                    {
            //                                        if (TongLevel != "8")
            //                                        {
            //                                            if (TongLevel != "45")
            //                                            {
            //                                                ThemHoaHong_ThuongLevel("0", "F7", "3", IDThanhVien, tableTVTF7.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                            }
            //                                        }
            //                                        else
            //                                        {
            //                                            ThemHoaHong_ThuongLevel("0", "F7", "3", IDThanhVien, tableTVTF7.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                        }
            //                                        #region Dừng nếu gặp lelvel5
            //                                        string leveeeel = TimLevelB(tableTVTF7.GioiThieu.ToString());
            //                                        if (leveeeel == "5")
            //                                        {
            //                                            Plevel = "45";
            //                                        }
            //                                        #endregion
            //                                    }
            //                                }
            //                                catch (Exception)
            //                                { }
            //                            }
            //                            #region Hoa Hồng Gián tiếp F8
            //                            user tableTVTF8 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF7.GioiThieu.ToString()));
            //                            if (tableTVTF8 != null)
            //                            {
            //                                if (tableTVTF8.GioiThieu.ToString() != "0")
            //                                {
            //                                    try
            //                                    {
            //                                        if (ShowFQRcode.ShowF8(tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                        {
            //                                            Dung = false;
            //                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                        }
            //                                        else
            //                                        {
            //                                            Dung = true;
            //                                            Plevel = Plevel + "," + ShowFQRcode.ShowF8(tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                        }
            //                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                        if (Plevel.ToString() == "99999999999")
            //                                        {

            //                                        }
            //                                        else
            //                                        {
            //                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                        }
            //                                        if (Dung == true)
            //                                        {
            //                                            if (TongLevel != "8")
            //                                            {
            //                                                if (TongLevel != "45")
            //                                                {

            //                                                    ThemHoaHong_ThuongLevel("0", "F8", "3", IDThanhVien, tableTVTF8.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                }
            //                                            }
            //                                            else
            //                                            {
            //                                                ThemHoaHong_ThuongLevel("0", "F8", "3", IDThanhVien, tableTVTF8.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                            }

            //                                            #region Dừng nếu gặp lelvel5
            //                                            string leveeeel = TimLevelB(tableTVTF8.GioiThieu.ToString());
            //                                            if (leveeeel == "5")
            //                                            {
            //                                                Plevel = "45";
            //                                            }
            //                                            #endregion

            //                                        }
            //                                    }
            //                                    catch (Exception)
            //                                    { }
            //                                }

            //                                #region Hoa Hồng Gián tiếp F9
            //                                user tableTVTF9 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF8.GioiThieu.ToString()));
            //                                if (tableTVTF9 != null)
            //                                {
            //                                    if (tableTVTF9.GioiThieu.ToString() != "0")
            //                                    {
            //                                        try
            //                                        {
            //                                            if (ShowFQRcode.ShowF9(tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                            {
            //                                                Dung = false;
            //                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                            }
            //                                            else
            //                                            {
            //                                                Dung = true;
            //                                                Plevel = Plevel + "," + ShowFQRcode.ShowF9(tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                            }
            //                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                            if (Plevel.ToString() == "99999999999")
            //                                            {

            //                                            }
            //                                            else
            //                                            {
            //                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                            }
            //                                            if (Dung == true)
            //                                            {
            //                                                if (TongLevel != "8")
            //                                                {
            //                                                    if (TongLevel != "45")
            //                                                    {

            //                                                        ThemHoaHong_ThuongLevel("0", "F9", "3", IDThanhVien, tableTVTF9.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                    }
            //                                                }
            //                                                else
            //                                                {
            //                                                    ThemHoaHong_ThuongLevel("0", "F9", "3", IDThanhVien, tableTVTF9.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                }
            //                                                #region Dừng nếu gặp lelvel5
            //                                                string leveeeel = TimLevelB(tableTVTF9.GioiThieu.ToString());
            //                                                if (leveeeel == "5")
            //                                                {
            //                                                    Plevel = "45";
            //                                                }
            //                                                #endregion
            //                                            }
            //                                        }
            //                                        catch (Exception)
            //                                        { }
            //                                    }
            //                                    #region Hoa Hồng Gián tiếp F10
            //                                    user tableTVTF10 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF9.GioiThieu.ToString()));
            //                                    if (tableTVTF10 != null)
            //                                    {
            //                                        if (tableTVTF10.GioiThieu.ToString() != "0")
            //                                        {
            //                                            try
            //                                            {
            //                                                if (ShowFQRcode.ShowF10(tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                {
            //                                                    Dung = false;
            //                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                }
            //                                                else
            //                                                {
            //                                                    Dung = true;
            //                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF10(tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                }
            //                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                if (Plevel.ToString() == "99999999999")
            //                                                {

            //                                                }
            //                                                else
            //                                                {
            //                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                }
            //                                                if (Dung == true)
            //                                                {
            //                                                    if (TongLevel != "8")
            //                                                    {
            //                                                        if (TongLevel != "45")
            //                                                        {
            //                                                            ThemHoaHong_ThuongLevel("0", "F10", "3", IDThanhVien, tableTVTF10.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                        }
            //                                                    }
            //                                                    else
            //                                                    {
            //                                                        ThemHoaHong_ThuongLevel("0", "F10", "3", IDThanhVien, tableTVTF10.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                    }
            //                                                    #region Dừng nếu gặp lelvel5
            //                                                    string leveeeel = TimLevelB(tableTVTF10.GioiThieu.ToString());
            //                                                    if (leveeeel == "5")
            //                                                    {
            //                                                        Plevel = "45";
            //                                                    }
            //                                                    #endregion
            //                                                }
            //                                            }
            //                                            catch (Exception)
            //                                            { }
            //                                        }
            //                                        #region Hoa Hồng Gián tiếp F11
            //                                        user tableTVTF11 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF10.GioiThieu.ToString()));
            //                                        if (tableTVTF11 != null)
            //                                        {
            //                                            if (tableTVTF11.GioiThieu.ToString() != "0")
            //                                            {
            //                                                try
            //                                                {
            //                                                    if (ShowFQRcode.ShowF11(tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                    {
            //                                                        Dung = false;
            //                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                    }
            //                                                    else
            //                                                    {
            //                                                        Dung = true;
            //                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF11(tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                    }
            //                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                    if (Plevel.ToString() == "99999999999")
            //                                                    {

            //                                                    }
            //                                                    else
            //                                                    {
            //                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                    }
            //                                                    if (Dung == true)
            //                                                    {
            //                                                        if (TongLevel != "8")
            //                                                        {
            //                                                            if (TongLevel != "45")
            //                                                            {
            //                                                                ThemHoaHong_ThuongLevel("0", "F11", "3", IDThanhVien, tableTVTF11.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                            }
            //                                                        }
            //                                                        else
            //                                                        {
            //                                                            ThemHoaHong_ThuongLevel("0", "F11", "3", IDThanhVien, tableTVTF11.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                        }
            //                                                        #region Dừng nếu gặp lelvel5
            //                                                        string leveeeel = TimLevelB(tableTVTF11.GioiThieu.ToString());
            //                                                        if (leveeeel == "5")
            //                                                        {
            //                                                            Plevel = "45";
            //                                                        }
            //                                                        #endregion
            //                                                    }
            //                                                }
            //                                                catch (Exception)
            //                                                { }
            //                                            }
            //                                            #region Hoa Hồng Gián tiếp F12
            //                                            user tableTVTF12 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF11.GioiThieu.ToString()));
            //                                            if (tableTVTF12 != null)
            //                                            {
            //                                                if (tableTVTF12.GioiThieu.ToString() != "0")
            //                                                {
            //                                                    try
            //                                                    {
            //                                                        if (ShowFQRcode.ShowF12(tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                        {
            //                                                            Dung = false;
            //                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                        }
            //                                                        else
            //                                                        {
            //                                                            Dung = true;
            //                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF12(tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                        }
            //                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                        if (Plevel.ToString() == "99999999999")
            //                                                        {

            //                                                        }
            //                                                        else
            //                                                        {
            //                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                        }
            //                                                        if (Dung == true)
            //                                                        {
            //                                                            if (TongLevel != "8")
            //                                                            {
            //                                                                if (TongLevel != "45")
            //                                                                {
            //                                                                    ThemHoaHong_ThuongLevel("0", "F12", "3", IDThanhVien, tableTVTF12.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                }
            //                                                            }
            //                                                            else
            //                                                            {
            //                                                                ThemHoaHong_ThuongLevel("0", "F12", "3", IDThanhVien, tableTVTF12.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                            }
            //                                                            #region Dừng nếu gặp lelvel5
            //                                                            string leveeeel = TimLevelB(tableTVTF12.GioiThieu.ToString());
            //                                                            if (leveeeel == "5")
            //                                                            {
            //                                                                Plevel = "45";
            //                                                            }
            //                                                            #endregion
            //                                                        }
            //                                                    }
            //                                                    catch (Exception)
            //                                                    { }
            //                                                }
            //                                                #region Hoa Hồng Gián tiếp tableTVTF13
            //                                                user tableTVTF13 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF12.GioiThieu.ToString()));
            //                                                if (tableTVTF13 != null)
            //                                                {
            //                                                    if (tableTVTF13.GioiThieu.ToString() != "0")
            //                                                    {
            //                                                        try
            //                                                        {
            //                                                            if (ShowFQRcode.ShowF13(tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                            {
            //                                                                Dung = false;
            //                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                            }
            //                                                            else
            //                                                            {
            //                                                                Dung = true;
            //                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF13(tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                            }
            //                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                            if (Plevel.ToString() == "99999999999")
            //                                                            {

            //                                                            }
            //                                                            else
            //                                                            {
            //                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                            }
            //                                                            if (Dung == true)
            //                                                            {
            //                                                                if (TongLevel != "8")
            //                                                                {
            //                                                                    if (TongLevel != "45")
            //                                                                    {
            //                                                                        ThemHoaHong_ThuongLevel("0", "F13", "3", IDThanhVien, tableTVTF13.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                    }
            //                                                                }
            //                                                                else
            //                                                                {
            //                                                                    ThemHoaHong_ThuongLevel("0", "F13", "3", IDThanhVien, tableTVTF13.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                }
            //                                                                #region Dừng nếu gặp lelvel5
            //                                                                string leveeeel = TimLevelB(tableTVTF13.GioiThieu.ToString());
            //                                                                if (leveeeel == "5")
            //                                                                {
            //                                                                    Plevel = "45";
            //                                                                }
            //                                                                #endregion
            //                                                            }
            //                                                        }
            //                                                        catch (Exception)
            //                                                        { }
            //                                                    }
            //                                                    #region Hoa Hồng Gián tiếp tableTVTF14
            //                                                    user tableTVTF14 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF13.GioiThieu.ToString()));
            //                                                    if (tableTVTF14 != null)
            //                                                    {
            //                                                        if (tableTVTF14.GioiThieu.ToString() != "0")
            //                                                        {
            //                                                            try
            //                                                            {
            //                                                                if (ShowFQRcode.ShowF14(tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                {
            //                                                                    Dung = false;
            //                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                }
            //                                                                else
            //                                                                {
            //                                                                    Dung = true;
            //                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF14(tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                }
            //                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                if (Plevel.ToString() == "99999999999")
            //                                                                {

            //                                                                }
            //                                                                else
            //                                                                {
            //                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                }
            //                                                                if (Dung == true)
            //                                                                {
            //                                                                    if (TongLevel != "8")
            //                                                                    {
            //                                                                        if (TongLevel != "45")
            //                                                                        {
            //                                                                            ThemHoaHong_ThuongLevel("0", "F14", "3", IDThanhVien, tableTVTF14.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                        }
            //                                                                    }
            //                                                                    else
            //                                                                    {
            //                                                                        ThemHoaHong_ThuongLevel("0", "F14", "3", IDThanhVien, tableTVTF14.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                    }
            //                                                                    #region Dừng nếu gặp lelvel5
            //                                                                    string leveeeel = TimLevelB(tableTVTF14.GioiThieu.ToString());
            //                                                                    if (leveeeel == "5")
            //                                                                    {
            //                                                                        Plevel = "45";
            //                                                                    }
            //                                                                    #endregion
            //                                                                }
            //                                                            }
            //                                                            catch (Exception)
            //                                                            { }
            //                                                        }
            //                                                        #region Hoa Hồng Gián tiếp tableTVTF15
            //                                                        user tableTVTF15 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF14.GioiThieu.ToString()));
            //                                                        if (tableTVTF15 != null)
            //                                                        {
            //                                                            if (tableTVTF15.GioiThieu.ToString() != "0")
            //                                                            {
            //                                                                try
            //                                                                {
            //                                                                    if (ShowFQRcode.ShowF15(tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                    {
            //                                                                        Dung = false;
            //                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                    }
            //                                                                    else
            //                                                                    {
            //                                                                        Dung = true;
            //                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF15(tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                    }
            //                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                    if (Plevel.ToString() == "99999999999")
            //                                                                    {

            //                                                                    }
            //                                                                    else
            //                                                                    {
            //                                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                                    }
            //                                                                    if (Dung == true)
            //                                                                    {
            //                                                                        if (TongLevel != "8")
            //                                                                        {
            //                                                                            if (TongLevel != "45")
            //                                                                            {
            //                                                                                ThemHoaHong_ThuongLevel("0", "F15", "3", IDThanhVien, tableTVTF15.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                            }
            //                                                                        }
            //                                                                        else
            //                                                                        {
            //                                                                            ThemHoaHong_ThuongLevel("0", "F15", "3", IDThanhVien, tableTVTF15.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                        }
            //                                                                        #region Dừng nếu gặp lelvel5
            //                                                                        string leveeeel = TimLevelB(tableTVTF15.GioiThieu.ToString());
            //                                                                        if (leveeeel == "5")
            //                                                                        {
            //                                                                            Plevel = "45";
            //                                                                        }
            //                                                                        #endregion
            //                                                                    }
            //                                                                }
            //                                                                catch (Exception)
            //                                                                { }
            //                                                            }

            //                                                            #region Hoa Hồng Gián tiếp tableTVTF16
            //                                                            user tableTVTF16 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF15.GioiThieu.ToString()));
            //                                                            if (tableTVTF16 != null)
            //                                                            {
            //                                                                if (tableTVTF16.GioiThieu.ToString() != "0")
            //                                                                {
            //                                                                    try
            //                                                                    {
            //                                                                        if (ShowFQRcode.ShowF16(tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                        {
            //                                                                            Dung = false;
            //                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                        }
            //                                                                        else
            //                                                                        {
            //                                                                            Dung = true;
            //                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF16(tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                        }
            //                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                        if (Plevel.ToString() == "99999999999")
            //                                                                        {

            //                                                                        }
            //                                                                        else
            //                                                                        {
            //                                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                                        }
            //                                                                        if (Dung == true)
            //                                                                        {
            //                                                                            if (TongLevel != "8")
            //                                                                            {
            //                                                                                if (TongLevel != "45")
            //                                                                                {
            //                                                                                    ThemHoaHong_ThuongLevel("0", "F16", "3", IDThanhVien, tableTVTF16.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                }
            //                                                                            }
            //                                                                            else
            //                                                                            {
            //                                                                                ThemHoaHong_ThuongLevel("0", "F16", "3", IDThanhVien, tableTVTF16.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                            }
            //                                                                            #region Dừng nếu gặp lelvel5
            //                                                                            string leveeeel = TimLevelB(tableTVTF16.GioiThieu.ToString());
            //                                                                            if (leveeeel == "5")
            //                                                                            {
            //                                                                                Plevel = "45";
            //                                                                            }
            //                                                                            #endregion
            //                                                                        }
            //                                                                    }
            //                                                                    catch (Exception)
            //                                                                    { }
            //                                                                }
            //                                                                #region Hoa Hồng Gián tiếp tableTVTF17
            //                                                                user tableTVTF17 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF16.GioiThieu.ToString()));
            //                                                                if (tableTVTF17 != null)
            //                                                                {
            //                                                                    if (tableTVTF17.GioiThieu.ToString() != "0")
            //                                                                    {
            //                                                                        try
            //                                                                        {
            //                                                                            if (ShowFQRcode.ShowF17(tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                            {
            //                                                                                Dung = false;
            //                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                            }
            //                                                                            else
            //                                                                            {
            //                                                                                Dung = true;
            //                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF17(tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                            }
            //                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                            if (Plevel.ToString() == "99999999999")
            //                                                                            {

            //                                                                            }
            //                                                                            else
            //                                                                            {
            //                                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                                            }
            //                                                                            if (Dung == true)
            //                                                                            {
            //                                                                                if (TongLevel != "8")
            //                                                                                {
            //                                                                                    if (TongLevel != "45")
            //                                                                                    {
            //                                                                                        ThemHoaHong_ThuongLevel("0", "F17", "3", IDThanhVien, tableTVTF17.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                    }
            //                                                                                }
            //                                                                                else
            //                                                                                {
            //                                                                                    ThemHoaHong_ThuongLevel("0", "F17", "3", IDThanhVien, tableTVTF17.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                }
            //                                                                                #region Dừng nếu gặp lelvel5
            //                                                                                string leveeeel = TimLevelB(tableTVTF17.GioiThieu.ToString());
            //                                                                                if (leveeeel == "5")
            //                                                                                {
            //                                                                                    Plevel = "45";
            //                                                                                }
            //                                                                                #endregion
            //                                                                            }
            //                                                                        }
            //                                                                        catch (Exception)
            //                                                                        { }
            //                                                                    }

            //                                                                    #region Hoa Hồng Gián tiếp tableTVTF18
            //                                                                    user tableTVTF18 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF17.GioiThieu.ToString()));
            //                                                                    if (tableTVTF18 != null)
            //                                                                    {
            //                                                                        if (tableTVTF18.GioiThieu.ToString() != "0")
            //                                                                        {
            //                                                                            try
            //                                                                            {
            //                                                                                if (ShowFQRcode.ShowF18(tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                {
            //                                                                                    Dung = false;
            //                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                }
            //                                                                                else
            //                                                                                {
            //                                                                                    Dung = true;
            //                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF18(tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                }
            //                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                if (Plevel.ToString() == "99999999999")
            //                                                                                {

            //                                                                                }
            //                                                                                else
            //                                                                                {
            //                                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                }
            //                                                                                if (Dung == true)
            //                                                                                {
            //                                                                                    if (TongLevel != "8")
            //                                                                                    {
            //                                                                                        if (TongLevel != "45")
            //                                                                                        {
            //                                                                                            ThemHoaHong_ThuongLevel("0", "F18", "3", IDThanhVien, tableTVTF18.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                        }
            //                                                                                    }
            //                                                                                    else
            //                                                                                    {
            //                                                                                        ThemHoaHong_ThuongLevel("0", "F18", "3", IDThanhVien, tableTVTF18.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                    }
            //                                                                                    #region Dừng nếu gặp lelvel5
            //                                                                                    string leveeeel = TimLevelB(tableTVTF18.GioiThieu.ToString());
            //                                                                                    if (leveeeel == "5")
            //                                                                                    {
            //                                                                                        Plevel = "45";
            //                                                                                    }
            //                                                                                    #endregion
            //                                                                                }
            //                                                                            }
            //                                                                            catch (Exception)
            //                                                                            { }
            //                                                                        }

            //                                                                        #region Hoa Hồng Gián tiếp tableTVTF19
            //                                                                        user tableTVTF19 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF18.GioiThieu.ToString()));
            //                                                                        if (tableTVTF19 != null)
            //                                                                        {
            //                                                                            if (tableTVTF19.GioiThieu.ToString() != "0")
            //                                                                            {
            //                                                                                try
            //                                                                                {
            //                                                                                    if (ShowFQRcode.ShowF19(tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                    {
            //                                                                                        Dung = false;
            //                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                    }
            //                                                                                    else
            //                                                                                    {
            //                                                                                        Dung = true;
            //                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF19(tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                    }
            //                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                    if (Plevel.ToString() == "99999999999")
            //                                                                                    {

            //                                                                                    }
            //                                                                                    else
            //                                                                                    {
            //                                                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                    }
            //                                                                                    if (Dung == true)
            //                                                                                    {
            //                                                                                        if (TongLevel != "8")
            //                                                                                        {
            //                                                                                            if (TongLevel != "45")
            //                                                                                            {
            //                                                                                                ThemHoaHong_ThuongLevel("0", "F19", "3", IDThanhVien, tableTVTF19.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                            }
            //                                                                                        }
            //                                                                                        else
            //                                                                                        {
            //                                                                                            ThemHoaHong_ThuongLevel("0", "F19", "3", IDThanhVien, tableTVTF19.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                        }
            //                                                                                        #region Dừng nếu gặp lelvel5
            //                                                                                        string leveeeel = TimLevelB(tableTVTF19.GioiThieu.ToString());
            //                                                                                        if (leveeeel == "5")
            //                                                                                        {
            //                                                                                            Plevel = "45";
            //                                                                                        }
            //                                                                                        #endregion
            //                                                                                    }
            //                                                                                }
            //                                                                                catch (Exception)
            //                                                                                { }
            //                                                                            }
            //                                                                            #region Hoa Hồng Gián tiếp tableTVTF20
            //                                                                            user tableTVTF20 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF19.GioiThieu.ToString()));
            //                                                                            if (tableTVTF20 != null)
            //                                                                            {
            //                                                                                if (tableTVTF20.GioiThieu.ToString() != "0")
            //                                                                                {
            //                                                                                    try
            //                                                                                    {
            //                                                                                        if (ShowFQRcode.ShowF20(tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                        {
            //                                                                                            Dung = false;
            //                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                        }
            //                                                                                        else
            //                                                                                        {
            //                                                                                            Dung = true;
            //                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF20(tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                        }
            //                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                        if (Plevel.ToString() == "99999999999")
            //                                                                                        {

            //                                                                                        }
            //                                                                                        else
            //                                                                                        {
            //                                                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                        }
            //                                                                                        if (Dung == true)
            //                                                                                        {
            //                                                                                            if (TongLevel != "8")
            //                                                                                            {
            //                                                                                                if (TongLevel != "45")
            //                                                                                                {
            //                                                                                                    ThemHoaHong_ThuongLevel("0", "F20", "3", IDThanhVien, tableTVTF20.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                }
            //                                                                                            }
            //                                                                                            else
            //                                                                                            {
            //                                                                                                ThemHoaHong_ThuongLevel("0", "F20", "3", IDThanhVien, tableTVTF20.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                            }
            //                                                                                            #region Dừng nếu gặp lelvel5
            //                                                                                            string leveeeel = TimLevelB(tableTVTF20.GioiThieu.ToString());
            //                                                                                            if (leveeeel == "5")
            //                                                                                            {
            //                                                                                                Plevel = "45";
            //                                                                                            }
            //                                                                                            #endregion
            //                                                                                        }
            //                                                                                    }
            //                                                                                    catch (Exception)
            //                                                                                    { }
            //                                                                                }
            //                                                                                #region Hoa Hồng Gián tiếp tableTVTF21
            //                                                                                user tableTVTF21 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF20.GioiThieu.ToString()));
            //                                                                                if (tableTVTF21 != null)
            //                                                                                {
            //                                                                                    if (tableTVTF21.GioiThieu.ToString() != "0")
            //                                                                                    {
            //                                                                                        try
            //                                                                                        {
            //                                                                                            if (ShowFQRcode.ShowF21(tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                            {
            //                                                                                                Dung = false;
            //                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                            }
            //                                                                                            else
            //                                                                                            {
            //                                                                                                Dung = true;
            //                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF21(tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                            }
            //                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                            if (Plevel.ToString() == "99999999999")
            //                                                                                            {

            //                                                                                            }
            //                                                                                            else
            //                                                                                            {
            //                                                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                            }
            //                                                                                            if (Dung == true)
            //                                                                                            {
            //                                                                                                if (TongLevel != "8")
            //                                                                                                {
            //                                                                                                    if (TongLevel != "45")
            //                                                                                                    {
            //                                                                                                        ThemHoaHong_ThuongLevel("0", "F21", "3", IDThanhVien, tableTVTF21.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                    }
            //                                                                                                }
            //                                                                                                else
            //                                                                                                {
            //                                                                                                    ThemHoaHong_ThuongLevel("0", "F21", "3", IDThanhVien, tableTVTF21.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                }
            //                                                                                                #region Dừng nếu gặp lelvel5
            //                                                                                                string leveeeel = TimLevelB(tableTVTF21.GioiThieu.ToString());
            //                                                                                                if (leveeeel == "5")
            //                                                                                                {
            //                                                                                                    Plevel = "45";
            //                                                                                                }
            //                                                                                                #endregion
            //                                                                                            }
            //                                                                                        }
            //                                                                                        catch (Exception)
            //                                                                                        { }
            //                                                                                    }
            //                                                                                    #region Hoa Hồng Gián tiếp tableTVTF22
            //                                                                                    user tableTVTF22 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF21.GioiThieu.ToString()));
            //                                                                                    if (tableTVTF22 != null)
            //                                                                                    {
            //                                                                                        if (tableTVTF22.GioiThieu.ToString() != "0")
            //                                                                                        {
            //                                                                                            try
            //                                                                                            {
            //                                                                                                if (ShowFQRcode.ShowF22(tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                {
            //                                                                                                    Dung = false;
            //                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                }
            //                                                                                                else
            //                                                                                                {
            //                                                                                                    Dung = true;
            //                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF22(tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                }
            //                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                if (Plevel.ToString() == "99999999999")
            //                                                                                                {

            //                                                                                                }
            //                                                                                                else
            //                                                                                                {
            //                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                }
            //                                                                                                if (Dung == true)
            //                                                                                                {
            //                                                                                                    if (TongLevel != "8")
            //                                                                                                    {
            //                                                                                                        if (TongLevel != "45")
            //                                                                                                        {
            //                                                                                                            ThemHoaHong_ThuongLevel("0", "F22", "3", IDThanhVien, tableTVTF22.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                        }
            //                                                                                                    }
            //                                                                                                    else
            //                                                                                                    {
            //                                                                                                        ThemHoaHong_ThuongLevel("0", "F22", "3", IDThanhVien, tableTVTF22.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                    }
            //                                                                                                    #region Dừng nếu gặp lelvel5
            //                                                                                                    string leveeeel = TimLevelB(tableTVTF22.GioiThieu.ToString());
            //                                                                                                    if (leveeeel == "5")
            //                                                                                                    {
            //                                                                                                        Plevel = "45";
            //                                                                                                    }
            //                                                                                                    #endregion
            //                                                                                                }
            //                                                                                            }
            //                                                                                            catch (Exception)
            //                                                                                            { }
            //                                                                                        }
            //                                                                                        #region Hoa Hồng Gián tiếp tableTVTF23
            //                                                                                        user tableTVTF23 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF22.GioiThieu.ToString()));
            //                                                                                        if (tableTVTF23 != null)
            //                                                                                        {
            //                                                                                            if (tableTVTF23.GioiThieu.ToString() != "0")
            //                                                                                            {
            //                                                                                                try
            //                                                                                                {
            //                                                                                                    if (ShowFQRcode.ShowF23(tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                    {
            //                                                                                                        Dung = false;
            //                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                    }
            //                                                                                                    else
            //                                                                                                    {
            //                                                                                                        Dung = true;
            //                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF23(tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                    }
            //                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                    if (Plevel.ToString() == "99999999999")
            //                                                                                                    {

            //                                                                                                    }
            //                                                                                                    else
            //                                                                                                    {
            //                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                    }
            //                                                                                                    if (Dung == true)
            //                                                                                                    {
            //                                                                                                        if (TongLevel != "8")
            //                                                                                                        {
            //                                                                                                            if (TongLevel != "45")
            //                                                                                                            {
            //                                                                                                                ThemHoaHong_ThuongLevel("0", "F23", "3", IDThanhVien, tableTVTF23.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                            }
            //                                                                                                        }
            //                                                                                                        else
            //                                                                                                        {
            //                                                                                                            ThemHoaHong_ThuongLevel("0", "F23", "3", IDThanhVien, tableTVTF23.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                        }

            //                                                                                                        #region Dừng nếu gặp lelvel5
            //                                                                                                        string leveeeel = TimLevelB(tableTVTF23.GioiThieu.ToString());
            //                                                                                                        if (leveeeel == "5")
            //                                                                                                        {
            //                                                                                                            Plevel = "45";
            //                                                                                                        }
            //                                                                                                        #endregion
            //                                                                                                    }
            //                                                                                                }
            //                                                                                                catch (Exception)
            //                                                                                                { }
            //                                                                                            }
            //                                                                                            #region Hoa Hồng Gián tiếp tableTVTF24
            //                                                                                            user tableTVTF24 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF23.GioiThieu.ToString()));
            //                                                                                            if (tableTVTF24 != null)
            //                                                                                            {
            //                                                                                                if (tableTVTF24.GioiThieu.ToString() != "0")
            //                                                                                                {
            //                                                                                                    try
            //                                                                                                    {
            //                                                                                                        if (ShowFQRcode.ShowF24(tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                        {
            //                                                                                                            Dung = false;
            //                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                        }
            //                                                                                                        else
            //                                                                                                        {
            //                                                                                                            Dung = true;
            //                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF24(tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                        }
            //                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                        if (Plevel.ToString() == "99999999999")
            //                                                                                                        {

            //                                                                                                        }
            //                                                                                                        else
            //                                                                                                        {
            //                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                        }
            //                                                                                                        if (Dung == true)
            //                                                                                                        {
            //                                                                                                            if (TongLevel != "8")
            //                                                                                                            {
            //                                                                                                                if (TongLevel != "45")
            //                                                                                                                {
            //                                                                                                                    ThemHoaHong_ThuongLevel("0", "F24", "3", IDThanhVien, tableTVTF24.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                }
            //                                                                                                            }
            //                                                                                                            else
            //                                                                                                            {
            //                                                                                                                ThemHoaHong_ThuongLevel("0", "F24", "3", IDThanhVien, tableTVTF24.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                            }
            //                                                                                                            #region Dừng nếu gặp lelvel5
            //                                                                                                            string leveeeel = TimLevelB(tableTVTF24.GioiThieu.ToString());
            //                                                                                                            if (leveeeel == "5")
            //                                                                                                            {
            //                                                                                                                Plevel = "45";
            //                                                                                                            }
            //                                                                                                            #endregion
            //                                                                                                        }
            //                                                                                                    }
            //                                                                                                    catch (Exception)
            //                                                                                                    { }
            //                                                                                                }
            //                                                                                                #region Hoa Hồng Gián tiếp tableTVTF25
            //                                                                                                user tableTVTF25 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF24.GioiThieu.ToString()));
            //                                                                                                if (tableTVTF25 != null)
            //                                                                                                {
            //                                                                                                    if (tableTVTF25.GioiThieu.ToString() != "0")
            //                                                                                                    {
            //                                                                                                        try
            //                                                                                                        {
            //                                                                                                            if (ShowFQRcode.ShowF25(tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                            {
            //                                                                                                                Dung = false;
            //                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                            }
            //                                                                                                            else
            //                                                                                                            {
            //                                                                                                                Dung = true;
            //                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF25(tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                            }
            //                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                            if (Plevel.ToString() == "99999999999")
            //                                                                                                            {

            //                                                                                                            }
            //                                                                                                            else
            //                                                                                                            {
            //                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                            }
            //                                                                                                            if (Dung == true)
            //                                                                                                            {
            //                                                                                                                if (TongLevel != "8")
            //                                                                                                                {
            //                                                                                                                    if (TongLevel != "45")
            //                                                                                                                    {
            //                                                                                                                        ThemHoaHong_ThuongLevel("0", "F25", "3", IDThanhVien, tableTVTF25.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                    }
            //                                                                                                                }
            //                                                                                                                else
            //                                                                                                                {
            //                                                                                                                    ThemHoaHong_ThuongLevel("0", "F25", "3", IDThanhVien, tableTVTF25.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                }
            //                                                                                                                #region Dừng nếu gặp lelvel5
            //                                                                                                                string leveeeel = TimLevelB(tableTVTF25.GioiThieu.ToString());
            //                                                                                                                if (leveeeel == "5")
            //                                                                                                                {
            //                                                                                                                    Plevel = "45";
            //                                                                                                                }
            //                                                                                                                #endregion
            //                                                                                                            }
            //                                                                                                        }
            //                                                                                                        catch (Exception)
            //                                                                                                        { }
            //                                                                                                    }
            //                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF26
            //                                                                                                    user tableTVTF26 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF25.GioiThieu.ToString()));
            //                                                                                                    if (tableTVTF26 != null)
            //                                                                                                    {
            //                                                                                                        if (tableTVTF26.GioiThieu.ToString() != "0")
            //                                                                                                        {
            //                                                                                                            try
            //                                                                                                            {
            //                                                                                                                if (ShowFQRcode.ShowF26(tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                {
            //                                                                                                                    Dung = false;
            //                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                }
            //                                                                                                                else
            //                                                                                                                {
            //                                                                                                                    Dung = true;
            //                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF26(tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                }
            //                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                if (Plevel.ToString() == "99999999999")
            //                                                                                                                {

            //                                                                                                                }
            //                                                                                                                else
            //                                                                                                                {
            //                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                }
            //                                                                                                                if (Dung == true)
            //                                                                                                                {
            //                                                                                                                    if (TongLevel != "8")
            //                                                                                                                    {
            //                                                                                                                        if (TongLevel != "45")
            //                                                                                                                        {
            //                                                                                                                            ThemHoaHong_ThuongLevel("0", "F26", "3", IDThanhVien, tableTVTF26.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                        }
            //                                                                                                                    }
            //                                                                                                                    else
            //                                                                                                                    {
            //                                                                                                                        ThemHoaHong_ThuongLevel("0", "F26", "3", IDThanhVien, tableTVTF26.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                    }
            //                                                                                                                    #region Dừng nếu gặp lelvel5
            //                                                                                                                    string leveeeel = TimLevelB(tableTVTF26.GioiThieu.ToString());
            //                                                                                                                    if (leveeeel == "5")
            //                                                                                                                    {
            //                                                                                                                        Plevel = "45";
            //                                                                                                                    }
            //                                                                                                                    #endregion
            //                                                                                                                }
            //                                                                                                            }
            //                                                                                                            catch (Exception)
            //                                                                                                            { }
            //                                                                                                        }

            //                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF27
            //                                                                                                        user tableTVTF27 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF26.GioiThieu.ToString()));
            //                                                                                                        if (tableTVTF27 != null)
            //                                                                                                        {
            //                                                                                                            if (tableTVTF27.GioiThieu.ToString() != "0")
            //                                                                                                            {
            //                                                                                                                try
            //                                                                                                                {
            //                                                                                                                    if (ShowFQRcode.ShowF27(tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                    {
            //                                                                                                                        Dung = false;
            //                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                    }
            //                                                                                                                    else
            //                                                                                                                    {
            //                                                                                                                        Dung = true;
            //                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF27(tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                    }
            //                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                    if (Plevel.ToString() == "99999999999")
            //                                                                                                                    {

            //                                                                                                                    }
            //                                                                                                                    else
            //                                                                                                                    {
            //                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                    }
            //                                                                                                                    if (Dung == true)
            //                                                                                                                    {
            //                                                                                                                        if (TongLevel != "8")
            //                                                                                                                        {
            //                                                                                                                            if (TongLevel != "45")
            //                                                                                                                            {
            //                                                                                                                                ThemHoaHong_ThuongLevel("0", "F27", "3", IDThanhVien, tableTVTF27.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                            }
            //                                                                                                                        }
            //                                                                                                                        else
            //                                                                                                                        {
            //                                                                                                                            ThemHoaHong_ThuongLevel("0", "F27", "3", IDThanhVien, tableTVTF27.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                        }
            //                                                                                                                        #region Dừng nếu gặp lelvel5
            //                                                                                                                        string leveeeel = TimLevelB(tableTVTF27.GioiThieu.ToString());
            //                                                                                                                        if (leveeeel == "5")
            //                                                                                                                        {
            //                                                                                                                            Plevel = "45";
            //                                                                                                                        }
            //                                                                                                                        #endregion
            //                                                                                                                    }
            //                                                                                                                }
            //                                                                                                                catch (Exception)
            //                                                                                                                { }
            //                                                                                                            }
            //                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF28
            //                                                                                                            user tableTVTF28 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF27.GioiThieu.ToString()));
            //                                                                                                            if (tableTVTF28 != null)
            //                                                                                                            {
            //                                                                                                                if (tableTVTF28.GioiThieu.ToString() != "0")
            //                                                                                                                {
            //                                                                                                                    try
            //                                                                                                                    {
            //                                                                                                                        if (ShowFQRcode.ShowF28(tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                        {
            //                                                                                                                            Dung = false;
            //                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                        }
            //                                                                                                                        else
            //                                                                                                                        {
            //                                                                                                                            Dung = true;
            //                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF28(tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                        }
            //                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                        if (Plevel.ToString() == "99999999999")
            //                                                                                                                        {

            //                                                                                                                        }
            //                                                                                                                        else
            //                                                                                                                        {
            //                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                        }
            //                                                                                                                        if (Dung == true)
            //                                                                                                                        {
            //                                                                                                                            if (TongLevel != "8")
            //                                                                                                                            {
            //                                                                                                                                if (TongLevel != "45")
            //                                                                                                                                {
            //                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F28", "3", IDThanhVien, tableTVTF28.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                }
            //                                                                                                                            }
            //                                                                                                                            else
            //                                                                                                                            {
            //                                                                                                                                ThemHoaHong_ThuongLevel("0", "F28", "3", IDThanhVien, tableTVTF28.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                            }
            //                                                                                                                            #region Dừng nếu gặp lelvel5
            //                                                                                                                            string leveeeel = TimLevelB(tableTVTF28.GioiThieu.ToString());
            //                                                                                                                            if (leveeeel == "5")
            //                                                                                                                            {
            //                                                                                                                                Plevel = "45";
            //                                                                                                                            }
            //                                                                                                                            #endregion
            //                                                                                                                        }
            //                                                                                                                    }
            //                                                                                                                    catch (Exception)
            //                                                                                                                    { }
            //                                                                                                                }

            //                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF29
            //                                                                                                                user tableTVTF29 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF28.GioiThieu.ToString()));
            //                                                                                                                if (tableTVTF29 != null)
            //                                                                                                                {
            //                                                                                                                    if (tableTVTF29.GioiThieu.ToString() != "0")
            //                                                                                                                    {
            //                                                                                                                        try
            //                                                                                                                        {
            //                                                                                                                            if (ShowFQRcode.ShowF29(tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                            {
            //                                                                                                                                Dung = false;
            //                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                            }
            //                                                                                                                            else
            //                                                                                                                            {
            //                                                                                                                                Dung = true;
            //                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF29(tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                            }
            //                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                            if (Plevel.ToString() == "99999999999")
            //                                                                                                                            {

            //                                                                                                                            }
            //                                                                                                                            else
            //                                                                                                                            {
            //                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                            }
            //                                                                                                                            if (Dung == true)
            //                                                                                                                            {
            //                                                                                                                                if (TongLevel != "8")
            //                                                                                                                                {
            //                                                                                                                                    if (TongLevel != "45")
            //                                                                                                                                    {
            //                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F29", "3", IDThanhVien, tableTVTF29.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                    }
            //                                                                                                                                }
            //                                                                                                                                else
            //                                                                                                                                {
            //                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F29", "3", IDThanhVien, tableTVTF29.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                }
            //                                                                                                                                #region Dừng nếu gặp lelvel5
            //                                                                                                                                string leveeeel = TimLevelB(tableTVTF29.GioiThieu.ToString());
            //                                                                                                                                if (leveeeel == "5")
            //                                                                                                                                {
            //                                                                                                                                    Plevel = "45";
            //                                                                                                                                }
            //                                                                                                                                #endregion
            //                                                                                                                            }
            //                                                                                                                        }
            //                                                                                                                        catch (Exception)
            //                                                                                                                        { }
            //                                                                                                                    }
            //                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF30
            //                                                                                                                    user tableTVTF30 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF29.GioiThieu.ToString()));
            //                                                                                                                    if (tableTVTF30 != null)
            //                                                                                                                    {
            //                                                                                                                        if (tableTVTF30.GioiThieu.ToString() != "0")
            //                                                                                                                        {
            //                                                                                                                            try
            //                                                                                                                            {
            //                                                                                                                                if (ShowFQRcode.ShowF30(tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                {
            //                                                                                                                                    Dung = false;
            //                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                }
            //                                                                                                                                else
            //                                                                                                                                {
            //                                                                                                                                    Dung = true;
            //                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF30(tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                }
            //                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                if (Plevel.ToString() == "99999999999")
            //                                                                                                                                {

            //                                                                                                                                }
            //                                                                                                                                else
            //                                                                                                                                {
            //                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                }
            //                                                                                                                                if (Dung == true)
            //                                                                                                                                {
            //                                                                                                                                    if (TongLevel != "8")
            //                                                                                                                                    {
            //                                                                                                                                        if (TongLevel != "45")
            //                                                                                                                                        {
            //                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F30", "3", IDThanhVien, tableTVTF30.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                        }
            //                                                                                                                                    }
            //                                                                                                                                    else
            //                                                                                                                                    {
            //                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F30", "3", IDThanhVien, tableTVTF30.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                    }
            //                                                                                                                                    #region Dừng nếu gặp lelvel5
            //                                                                                                                                    string leveeeel = TimLevelB(tableTVTF30.GioiThieu.ToString());
            //                                                                                                                                    if (leveeeel == "5")
            //                                                                                                                                    {
            //                                                                                                                                        Plevel = "45";
            //                                                                                                                                    }
            //                                                                                                                                    #endregion
            //                                                                                                                                }
            //                                                                                                                            }
            //                                                                                                                            catch (Exception)
            //                                                                                                                            { }
            //                                                                                                                        }

            //                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF31
            //                                                                                                                        user tableTVTF31 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF30.GioiThieu.ToString()));
            //                                                                                                                        if (tableTVTF31 != null)
            //                                                                                                                        {
            //                                                                                                                            if (tableTVTF31.GioiThieu.ToString() != "0")
            //                                                                                                                            {
            //                                                                                                                                try
            //                                                                                                                                {
            //                                                                                                                                    if (ShowFQRcode.ShowF31(tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                    {
            //                                                                                                                                        Dung = false;
            //                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                    }
            //                                                                                                                                    else
            //                                                                                                                                    {
            //                                                                                                                                        Dung = true;
            //                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF31(tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                    }
            //                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                    if (Plevel.ToString() == "99999999999")
            //                                                                                                                                    {

            //                                                                                                                                    }
            //                                                                                                                                    else
            //                                                                                                                                    {
            //                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                    }
            //                                                                                                                                    if (Dung == true)
            //                                                                                                                                    {
            //                                                                                                                                        if (TongLevel != "8")
            //                                                                                                                                        {
            //                                                                                                                                            if (TongLevel != "45")
            //                                                                                                                                            {
            //                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F31", "3", IDThanhVien, tableTVTF31.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                            }
            //                                                                                                                                        }
            //                                                                                                                                        else
            //                                                                                                                                        {
            //                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F31", "3", IDThanhVien, tableTVTF31.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                        }
            //                                                                                                                                        #region Dừng nếu gặp lelvel5
            //                                                                                                                                        string leveeeel = TimLevelB(tableTVTF31.GioiThieu.ToString());
            //                                                                                                                                        if (leveeeel == "5")
            //                                                                                                                                        {
            //                                                                                                                                            Plevel = "45";
            //                                                                                                                                        }
            //                                                                                                                                        #endregion
            //                                                                                                                                    }
            //                                                                                                                                }
            //                                                                                                                                catch (Exception)
            //                                                                                                                                { }
            //                                                                                                                            }

            //                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF32
            //                                                                                                                            user tableTVTF32 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF31.GioiThieu.ToString()));
            //                                                                                                                            if (tableTVTF32 != null)
            //                                                                                                                            {
            //                                                                                                                                if (tableTVTF32.GioiThieu.ToString() != "0")
            //                                                                                                                                {
            //                                                                                                                                    try
            //                                                                                                                                    {
            //                                                                                                                                        if (ShowFQRcode.ShowF32(tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                        {
            //                                                                                                                                            Dung = false;
            //                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                        }
            //                                                                                                                                        else
            //                                                                                                                                        {
            //                                                                                                                                            Dung = true;
            //                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF32(tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                        }
            //                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                        if (Plevel.ToString() == "99999999999")
            //                                                                                                                                        {

            //                                                                                                                                        }
            //                                                                                                                                        else
            //                                                                                                                                        {
            //                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                        }
            //                                                                                                                                        if (Dung == true)
            //                                                                                                                                        {
            //                                                                                                                                            if (TongLevel != "8")
            //                                                                                                                                            {
            //                                                                                                                                                if (TongLevel != "45")
            //                                                                                                                                                {
            //                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F32", "3", IDThanhVien, tableTVTF32.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                }
            //                                                                                                                                            }
            //                                                                                                                                            else
            //                                                                                                                                            {
            //                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F32", "3", IDThanhVien, tableTVTF32.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                            }
            //                                                                                                                                            #region Dừng nếu gặp lelvel5
            //                                                                                                                                            string leveeeel = TimLevelB(tableTVTF32.GioiThieu.ToString());
            //                                                                                                                                            if (leveeeel == "5")
            //                                                                                                                                            {
            //                                                                                                                                                Plevel = "45";
            //                                                                                                                                            }
            //                                                                                                                                            #endregion
            //                                                                                                                                        }
            //                                                                                                                                    }
            //                                                                                                                                    catch (Exception)
            //                                                                                                                                    { }
            //                                                                                                                                }
            //                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF33
            //                                                                                                                                user tableTVTF33 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF32.GioiThieu.ToString()));
            //                                                                                                                                if (tableTVTF33 != null)
            //                                                                                                                                {
            //                                                                                                                                    if (tableTVTF33.GioiThieu.ToString() != "0")
            //                                                                                                                                    {
            //                                                                                                                                        try
            //                                                                                                                                        {
            //                                                                                                                                            if (ShowFQRcode.ShowF33(tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                            {
            //                                                                                                                                                Dung = false;
            //                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                            }
            //                                                                                                                                            else
            //                                                                                                                                            {
            //                                                                                                                                                Dung = true;
            //                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF33(tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                            }
            //                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                            if (Plevel.ToString() == "99999999999")
            //                                                                                                                                            {

            //                                                                                                                                            }
            //                                                                                                                                            else
            //                                                                                                                                            {
            //                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                            }
            //                                                                                                                                            if (Dung == true)
            //                                                                                                                                            {
            //                                                                                                                                                if (TongLevel != "8")
            //                                                                                                                                                {
            //                                                                                                                                                    if (TongLevel != "45")
            //                                                                                                                                                    {
            //                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F33", "3", IDThanhVien, tableTVTF33.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                    }
            //                                                                                                                                                }
            //                                                                                                                                                else
            //                                                                                                                                                {
            //                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F33", "3", IDThanhVien, tableTVTF33.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                }
            //                                                                                                                                                #region Dừng nếu gặp lelvel5
            //                                                                                                                                                string leveeeel = TimLevelB(tableTVTF33.GioiThieu.ToString());
            //                                                                                                                                                if (leveeeel == "5")
            //                                                                                                                                                {
            //                                                                                                                                                    Plevel = "45";
            //                                                                                                                                                }
            //                                                                                                                                                #endregion
            //                                                                                                                                            }
            //                                                                                                                                        }
            //                                                                                                                                        catch (Exception)
            //                                                                                                                                        { }
            //                                                                                                                                    }
            //                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF34
            //                                                                                                                                    user tableTVTF34 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF33.GioiThieu.ToString()));
            //                                                                                                                                    if (tableTVTF34 != null)
            //                                                                                                                                    {
            //                                                                                                                                        if (tableTVTF34.GioiThieu.ToString() != "0")
            //                                                                                                                                        {
            //                                                                                                                                            try
            //                                                                                                                                            {
            //                                                                                                                                                if (ShowFQRcode.ShowF34(tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                {
            //                                                                                                                                                    Dung = false;
            //                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                }
            //                                                                                                                                                else
            //                                                                                                                                                {
            //                                                                                                                                                    Dung = true;
            //                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF34(tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                }
            //                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                {

            //                                                                                                                                                }
            //                                                                                                                                                else
            //                                                                                                                                                {
            //                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                }
            //                                                                                                                                                if (Dung == true)
            //                                                                                                                                                {
            //                                                                                                                                                    if (TongLevel != "8")
            //                                                                                                                                                    {
            //                                                                                                                                                        if (TongLevel != "45")
            //                                                                                                                                                        {
            //                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F34", "3", IDThanhVien, tableTVTF34.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                        }
            //                                                                                                                                                    }
            //                                                                                                                                                    else
            //                                                                                                                                                    {
            //                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F34", "3", IDThanhVien, tableTVTF34.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                    }
            //                                                                                                                                                    #region Dừng nếu gặp lelvel5
            //                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF34.GioiThieu.ToString());
            //                                                                                                                                                    if (leveeeel == "5")
            //                                                                                                                                                    {
            //                                                                                                                                                        Plevel = "45";
            //                                                                                                                                                    }
            //                                                                                                                                                    #endregion
            //                                                                                                                                                }
            //                                                                                                                                            }
            //                                                                                                                                            catch (Exception)
            //                                                                                                                                            { }
            //                                                                                                                                        }
            //                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF35
            //                                                                                                                                        user tableTVTF35 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF34.GioiThieu.ToString()));
            //                                                                                                                                        if (tableTVTF35 != null)
            //                                                                                                                                        {
            //                                                                                                                                            if (tableTVTF35.GioiThieu.ToString() != "0")
            //                                                                                                                                            {
            //                                                                                                                                                try
            //                                                                                                                                                {
            //                                                                                                                                                    if (ShowFQRcode.ShowF35(tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                    {
            //                                                                                                                                                        Dung = false;
            //                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                    }
            //                                                                                                                                                    else
            //                                                                                                                                                    {
            //                                                                                                                                                        Dung = true;
            //                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF35(tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                    }
            //                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                    if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                    {

            //                                                                                                                                                    }
            //                                                                                                                                                    else
            //                                                                                                                                                    {
            //                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                    }
            //                                                                                                                                                    if (Dung == true)
            //                                                                                                                                                    {
            //                                                                                                                                                        if (TongLevel != "8")
            //                                                                                                                                                        {
            //                                                                                                                                                            if (TongLevel != "45")
            //                                                                                                                                                            {
            //                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F35", "3", IDThanhVien, tableTVTF35.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                            }
            //                                                                                                                                                        }
            //                                                                                                                                                        else
            //                                                                                                                                                        {
            //                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F35", "3", IDThanhVien, tableTVTF35.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                        }
            //                                                                                                                                                        #region Dừng nếu gặp lelvel5
            //                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF35.GioiThieu.ToString());
            //                                                                                                                                                        if (leveeeel == "5")
            //                                                                                                                                                        {
            //                                                                                                                                                            Plevel = "45";
            //                                                                                                                                                        }
            //                                                                                                                                                        #endregion
            //                                                                                                                                                    }
            //                                                                                                                                                }
            //                                                                                                                                                catch (Exception)
            //                                                                                                                                                { }
            //                                                                                                                                            }
            //                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF36
            //                                                                                                                                            user tableTVTF36 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF35.GioiThieu.ToString()));
            //                                                                                                                                            if (tableTVTF36 != null)
            //                                                                                                                                            {
            //                                                                                                                                                if (tableTVTF36.GioiThieu.ToString() != "0")
            //                                                                                                                                                {
            //                                                                                                                                                    try
            //                                                                                                                                                    {
            //                                                                                                                                                        if (ShowFQRcode.ShowF36(tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                        {
            //                                                                                                                                                            Dung = false;
            //                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                        }
            //                                                                                                                                                        else
            //                                                                                                                                                        {
            //                                                                                                                                                            Dung = true;
            //                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF36(tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                        }
            //                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                        if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                        {

            //                                                                                                                                                        }
            //                                                                                                                                                        else
            //                                                                                                                                                        {
            //                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                        }
            //                                                                                                                                                        if (Dung == true)
            //                                                                                                                                                        {
            //                                                                                                                                                            if (TongLevel != "8")
            //                                                                                                                                                            {
            //                                                                                                                                                                if (TongLevel != "45")
            //                                                                                                                                                                {
            //                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F36", "3", IDThanhVien, tableTVTF36.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                }
            //                                                                                                                                                            }
            //                                                                                                                                                            else
            //                                                                                                                                                            {
            //                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F36", "3", IDThanhVien, tableTVTF36.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                            }
            //                                                                                                                                                            #region Dừng nếu gặp lelvel5
            //                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF36.GioiThieu.ToString());
            //                                                                                                                                                            if (leveeeel == "5")
            //                                                                                                                                                            {
            //                                                                                                                                                                Plevel = "45";
            //                                                                                                                                                            }
            //                                                                                                                                                            #endregion
            //                                                                                                                                                        }
            //                                                                                                                                                    }
            //                                                                                                                                                    catch (Exception)
            //                                                                                                                                                    { }
            //                                                                                                                                                }
            //                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF37
            //                                                                                                                                                user tableTVTF37 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF36.GioiThieu.ToString()));
            //                                                                                                                                                if (tableTVTF37 != null)
            //                                                                                                                                                {
            //                                                                                                                                                    if (tableTVTF37.GioiThieu.ToString() != "0")
            //                                                                                                                                                    {
            //                                                                                                                                                        try
            //                                                                                                                                                        {
            //                                                                                                                                                            if (ShowFQRcode.ShowF37(tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                            {
            //                                                                                                                                                                Dung = false;
            //                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                            }
            //                                                                                                                                                            else
            //                                                                                                                                                            {
            //                                                                                                                                                                Dung = true;
            //                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF37(tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                            }
            //                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                            if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                            {

            //                                                                                                                                                            }
            //                                                                                                                                                            else
            //                                                                                                                                                            {
            //                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                            }
            //                                                                                                                                                            if (Dung == true)
            //                                                                                                                                                            {
            //                                                                                                                                                                if (TongLevel != "8")
            //                                                                                                                                                                {
            //                                                                                                                                                                    if (TongLevel != "45")
            //                                                                                                                                                                    {
            //                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F37", "3", IDThanhVien, tableTVTF37.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                    }
            //                                                                                                                                                                }
            //                                                                                                                                                                else
            //                                                                                                                                                                {
            //                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F37", "3", IDThanhVien, tableTVTF37.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                }
            //                                                                                                                                                                #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF37.GioiThieu.ToString());
            //                                                                                                                                                                if (leveeeel == "5")
            //                                                                                                                                                                {
            //                                                                                                                                                                    Plevel = "45";
            //                                                                                                                                                                }
            //                                                                                                                                                                #endregion
            //                                                                                                                                                            }
            //                                                                                                                                                        }
            //                                                                                                                                                        catch (Exception)
            //                                                                                                                                                        { }
            //                                                                                                                                                    }
            //                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF38
            //                                                                                                                                                    user tableTVTF38 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF37.GioiThieu.ToString()));
            //                                                                                                                                                    if (tableTVTF38 != null)
            //                                                                                                                                                    {
            //                                                                                                                                                        if (tableTVTF38.GioiThieu.ToString() != "0")
            //                                                                                                                                                        {
            //                                                                                                                                                            try
            //                                                                                                                                                            {
            //                                                                                                                                                                if (ShowFQRcode.ShowF38(tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                {
            //                                                                                                                                                                    Dung = false;
            //                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                }
            //                                                                                                                                                                else
            //                                                                                                                                                                {
            //                                                                                                                                                                    Dung = true;
            //                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF38(tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                }
            //                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                {

            //                                                                                                                                                                }
            //                                                                                                                                                                else
            //                                                                                                                                                                {
            //                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                }
            //                                                                                                                                                                if (Dung == true)
            //                                                                                                                                                                {
            //                                                                                                                                                                    if (TongLevel != "8")
            //                                                                                                                                                                    {
            //                                                                                                                                                                        if (TongLevel != "45")
            //                                                                                                                                                                        {
            //                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F38", "3", IDThanhVien, tableTVTF38.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                        }
            //                                                                                                                                                                    }
            //                                                                                                                                                                    else
            //                                                                                                                                                                    {
            //                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F38", "3", IDThanhVien, tableTVTF38.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                    }
            //                                                                                                                                                                    #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF38.GioiThieu.ToString());
            //                                                                                                                                                                    if (leveeeel == "5")
            //                                                                                                                                                                    {
            //                                                                                                                                                                        Plevel = "45";
            //                                                                                                                                                                    }
            //                                                                                                                                                                    #endregion
            //                                                                                                                                                                }
            //                                                                                                                                                            }
            //                                                                                                                                                            catch (Exception)
            //                                                                                                                                                            { }
            //                                                                                                                                                        }
            //                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF39
            //                                                                                                                                                        user tableTVTF39 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF38.GioiThieu.ToString()));
            //                                                                                                                                                        if (tableTVTF39 != null)
            //                                                                                                                                                        {
            //                                                                                                                                                            if (tableTVTF39.GioiThieu.ToString() != "0")
            //                                                                                                                                                            {
            //                                                                                                                                                                try
            //                                                                                                                                                                {
            //                                                                                                                                                                    if (ShowFQRcode.ShowF39(tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                    {
            //                                                                                                                                                                        Dung = false;
            //                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                    }
            //                                                                                                                                                                    else
            //                                                                                                                                                                    {
            //                                                                                                                                                                        Dung = true;
            //                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF39(tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                    }
            //                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                    {

            //                                                                                                                                                                    }
            //                                                                                                                                                                    else
            //                                                                                                                                                                    {
            //                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                    }
            //                                                                                                                                                                    if (Dung == true)
            //                                                                                                                                                                    {
            //                                                                                                                                                                        if (TongLevel != "8")
            //                                                                                                                                                                        {
            //                                                                                                                                                                            if (TongLevel != "45")
            //                                                                                                                                                                            {
            //                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F39", "3", IDThanhVien, tableTVTF39.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                            }
            //                                                                                                                                                                        }
            //                                                                                                                                                                        else
            //                                                                                                                                                                        {
            //                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F39", "3", IDThanhVien, tableTVTF39.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                        }

            //                                                                                                                                                                        #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF39.GioiThieu.ToString());
            //                                                                                                                                                                        if (leveeeel == "5")
            //                                                                                                                                                                        {
            //                                                                                                                                                                            Plevel = "45";
            //                                                                                                                                                                        }
            //                                                                                                                                                                        #endregion
            //                                                                                                                                                                    }
            //                                                                                                                                                                }
            //                                                                                                                                                                catch (Exception)
            //                                                                                                                                                                { }
            //                                                                                                                                                            }

            //                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF40
            //                                                                                                                                                            user tableTVTF40 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF39.GioiThieu.ToString()));
            //                                                                                                                                                            if (tableTVTF40 != null)
            //                                                                                                                                                            {
            //                                                                                                                                                                if (tableTVTF40.GioiThieu.ToString() != "0")
            //                                                                                                                                                                {
            //                                                                                                                                                                    try
            //                                                                                                                                                                    {
            //                                                                                                                                                                        if (ShowFQRcode.ShowF40(tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                        {
            //                                                                                                                                                                            Dung = false;
            //                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                        }
            //                                                                                                                                                                        else
            //                                                                                                                                                                        {
            //                                                                                                                                                                            Dung = true;
            //                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF40(tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                        }
            //                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                        {

            //                                                                                                                                                                        }
            //                                                                                                                                                                        else
            //                                                                                                                                                                        {
            //                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                        }
            //                                                                                                                                                                        if (Dung == true)
            //                                                                                                                                                                        {
            //                                                                                                                                                                            if (TongLevel != "8")
            //                                                                                                                                                                            {
            //                                                                                                                                                                                if (TongLevel != "45")
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F40", "3", IDThanhVien, tableTVTF40.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                }
            //                                                                                                                                                                            }
            //                                                                                                                                                                            else
            //                                                                                                                                                                            {
            //                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F40", "3", IDThanhVien, tableTVTF40.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                            }
            //                                                                                                                                                                            #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF40.GioiThieu.ToString());
            //                                                                                                                                                                            if (leveeeel == "5")
            //                                                                                                                                                                            {
            //                                                                                                                                                                                Plevel = "45";
            //                                                                                                                                                                            }
            //                                                                                                                                                                            #endregion
            //                                                                                                                                                                        }
            //                                                                                                                                                                    }
            //                                                                                                                                                                    catch (Exception)
            //                                                                                                                                                                    { }
            //                                                                                                                                                                }
            //                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF41
            //                                                                                                                                                                user tableTVTF41 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF40.GioiThieu.ToString()));
            //                                                                                                                                                                if (tableTVTF41 != null)
            //                                                                                                                                                                {
            //                                                                                                                                                                    if (tableTVTF41.GioiThieu.ToString() != "0")
            //                                                                                                                                                                    {
            //                                                                                                                                                                        try
            //                                                                                                                                                                        {
            //                                                                                                                                                                            if (ShowFQRcode.ShowF41(tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                            {
            //                                                                                                                                                                                Dung = false;
            //                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                            }
            //                                                                                                                                                                            else
            //                                                                                                                                                                            {
            //                                                                                                                                                                                Dung = true;
            //                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF41(tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                            }
            //                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                            {

            //                                                                                                                                                                            }
            //                                                                                                                                                                            else
            //                                                                                                                                                                            {
            //                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                            }
            //                                                                                                                                                                            if (Dung == true)
            //                                                                                                                                                                            {
            //                                                                                                                                                                                if (TongLevel != "8")
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    if (TongLevel != "45")
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F41", "3", IDThanhVien, tableTVTF41.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                }
            //                                                                                                                                                                                else
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F41", "3", IDThanhVien, tableTVTF41.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                }
            //                                                                                                                                                                                #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF41.GioiThieu.ToString());
            //                                                                                                                                                                                if (leveeeel == "5")
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    Plevel = "45";
            //                                                                                                                                                                                }
            //                                                                                                                                                                                #endregion
            //                                                                                                                                                                            }
            //                                                                                                                                                                        }
            //                                                                                                                                                                        catch (Exception)
            //                                                                                                                                                                        { }
            //                                                                                                                                                                    }

            //                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF42
            //                                                                                                                                                                    user tableTVTF42 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF41.GioiThieu.ToString()));
            //                                                                                                                                                                    if (tableTVTF42 != null)
            //                                                                                                                                                                    {
            //                                                                                                                                                                        if (tableTVTF42.GioiThieu.ToString() != "0")
            //                                                                                                                                                                        {
            //                                                                                                                                                                            try
            //                                                                                                                                                                            {
            //                                                                                                                                                                                if (ShowFQRcode.ShowF42(tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    Dung = false;
            //                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                                }
            //                                                                                                                                                                                else
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    Dung = true;
            //                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF42(tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                                }
            //                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                                {

            //                                                                                                                                                                                }
            //                                                                                                                                                                                else
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                                }
            //                                                                                                                                                                                if (Dung == true)
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    if (TongLevel != "8")
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        if (TongLevel != "45")
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F42", "3", IDThanhVien, tableTVTF42.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                    else
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F42", "3", IDThanhVien, tableTVTF42.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF42.GioiThieu.ToString());
            //                                                                                                                                                                                    if (leveeeel == "5")
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        Plevel = "45";
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                    #endregion
            //                                                                                                                                                                                }
            //                                                                                                                                                                            }
            //                                                                                                                                                                            catch (Exception)
            //                                                                                                                                                                            { }
            //                                                                                                                                                                        }

            //                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF43
            //                                                                                                                                                                        user tableTVTF43 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF42.GioiThieu.ToString()));
            //                                                                                                                                                                        if (tableTVTF43 != null)
            //                                                                                                                                                                        {
            //                                                                                                                                                                            if (tableTVTF43.GioiThieu.ToString() != "0")
            //                                                                                                                                                                            {
            //                                                                                                                                                                                try
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    if (ShowFQRcode.ShowF43(tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        Dung = false;
            //                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                    else
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        Dung = true;
            //                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF43(tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                                    {

            //                                                                                                                                                                                    }
            //                                                                                                                                                                                    else
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                    if (Dung == true)
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        if (TongLevel != "8")
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            if (TongLevel != "45")
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F43", "3", IDThanhVien, tableTVTF43.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                        else
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F43", "3", IDThanhVien, tableTVTF43.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF43.GioiThieu.ToString());
            //                                                                                                                                                                                        if (leveeeel == "5")
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            Plevel = "45";
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                        #endregion
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                }
            //                                                                                                                                                                                catch (Exception)
            //                                                                                                                                                                                { }
            //                                                                                                                                                                            }

            //                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF44
            //                                                                                                                                                                            user tableTVTF44 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF43.GioiThieu.ToString()));
            //                                                                                                                                                                            if (tableTVTF44 != null)
            //                                                                                                                                                                            {
            //                                                                                                                                                                                if (tableTVTF44.GioiThieu.ToString() != "0")
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    try
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        if (ShowFQRcode.ShowF44(tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            Dung = false;
            //                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                        else
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            Dung = true;
            //                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF44(tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                                        {

            //                                                                                                                                                                                        }
            //                                                                                                                                                                                        else
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                        if (Dung == true)
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            if (TongLevel != "8")
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                if (TongLevel != "45")
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F44", "3", IDThanhVien, tableTVTF44.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                            else
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F44", "3", IDThanhVien, tableTVTF44.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF44.GioiThieu.ToString());
            //                                                                                                                                                                                            if (leveeeel == "5")
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                Plevel = "45";
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                            #endregion
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                    catch (Exception)
            //                                                                                                                                                                                    { }
            //                                                                                                                                                                                }

            //                                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF45
            //                                                                                                                                                                                user tableTVTF45 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF44.GioiThieu.ToString()));
            //                                                                                                                                                                                if (tableTVTF45 != null)
            //                                                                                                                                                                                {
            //                                                                                                                                                                                    if (tableTVTF45.GioiThieu.ToString() != "0")
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        try
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            if (ShowFQRcode.ShowF45(tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                Dung = false;
            //                                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                            else
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                Dung = true;
            //                                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF45(tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                                            {

            //                                                                                                                                                                                            }
            //                                                                                                                                                                                            else
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                            if (Dung == true)
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                if (TongLevel != "8")
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    if (TongLevel != "45")
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F45", "3", IDThanhVien, tableTVTF45.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                else
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F45", "3", IDThanhVien, tableTVTF45.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF45.GioiThieu.ToString());
            //                                                                                                                                                                                                if (leveeeel == "5")
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    Plevel = "45";
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                #endregion
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                        catch (Exception)
            //                                                                                                                                                                                        { }
            //                                                                                                                                                                                    }

            //                                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF46
            //                                                                                                                                                                                    user tableTVTF46 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF45.GioiThieu.ToString()));
            //                                                                                                                                                                                    if (tableTVTF46 != null)
            //                                                                                                                                                                                    {
            //                                                                                                                                                                                        if (tableTVTF46.GioiThieu.ToString() != "0")
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            try
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                if (ShowFQRcode.ShowF46(tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    Dung = false;
            //                                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                else
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    Dung = true;
            //                                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF46(tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                                                {

            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                else
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                if (Dung == true)
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    if (TongLevel != "8")
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        if (TongLevel != "45")
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F46", "3", IDThanhVien, tableTVTF46.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                    else
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F46", "3", IDThanhVien, tableTVTF46.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF46.GioiThieu.ToString());
            //                                                                                                                                                                                                    if (leveeeel == "5")
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        Plevel = "45";
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                    #endregion
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                            catch (Exception)
            //                                                                                                                                                                                            { }
            //                                                                                                                                                                                        }

            //                                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF47
            //                                                                                                                                                                                        user tableTVTF47 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF46.GioiThieu.ToString()));
            //                                                                                                                                                                                        if (tableTVTF47 != null)
            //                                                                                                                                                                                        {
            //                                                                                                                                                                                            if (tableTVTF47.GioiThieu.ToString() != "0")
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                try
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    if (ShowFQRcode.ShowF47(tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        Dung = false;
            //                                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                    else
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        Dung = true;
            //                                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF47(tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                                                    {

            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                    else
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                    if (Dung == true)
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        if (TongLevel != "8")
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            if (TongLevel != "45")
            //                                                                                                                                                                                                            {
            //                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F47", "3", IDThanhVien, tableTVTF47.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                        else
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F47", "3", IDThanhVien, tableTVTF47.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF47.GioiThieu.ToString());
            //                                                                                                                                                                                                        if (leveeeel == "5")
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            Plevel = "45";
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                        #endregion
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                catch (Exception)
            //                                                                                                                                                                                                { }
            //                                                                                                                                                                                            }

            //                                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF48
            //                                                                                                                                                                                            user tableTVTF48 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF47.GioiThieu.ToString()));
            //                                                                                                                                                                                            if (tableTVTF48 != null)
            //                                                                                                                                                                                            {
            //                                                                                                                                                                                                if (tableTVTF48.GioiThieu.ToString() != "0")
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    try
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        if (ShowFQRcode.ShowF48(tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            Dung = false;
            //                                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                        else
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            Dung = true;
            //                                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF48(tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                                                        {

            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                        else
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                        if (Dung == true)
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            if (TongLevel != "8")
            //                                                                                                                                                                                                            {
            //                                                                                                                                                                                                                if (TongLevel != "45")
            //                                                                                                                                                                                                                {
            //                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F48", "3", IDThanhVien, tableTVTF48.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                                                }
            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                            else
            //                                                                                                                                                                                                            {
            //                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F48", "3", IDThanhVien, tableTVTF48.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF48.GioiThieu.ToString());
            //                                                                                                                                                                                                            if (leveeeel == "5")
            //                                                                                                                                                                                                            {
            //                                                                                                                                                                                                                Plevel = "45";
            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                            #endregion
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                    catch (Exception)
            //                                                                                                                                                                                                    { }
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF49
            //                                                                                                                                                                                                user tableTVTF49 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF48.GioiThieu.ToString()));
            //                                                                                                                                                                                                if (tableTVTF49 != null)
            //                                                                                                                                                                                                {
            //                                                                                                                                                                                                    if (tableTVTF49.GioiThieu.ToString() != "0")
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        try
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            if (ShowFQRcode.ShowF49(tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                                                            {
            //                                                                                                                                                                                                                Dung = false;
            //                                                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                            else
            //                                                                                                                                                                                                            {
            //                                                                                                                                                                                                                Dung = true;
            //                                                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF49(tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                                                            {

            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                            else
            //                                                                                                                                                                                                            {
            //                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                            if (Dung == true)
            //                                                                                                                                                                                                            {
            //                                                                                                                                                                                                                if (TongLevel != "8")
            //                                                                                                                                                                                                                {
            //                                                                                                                                                                                                                    if (TongLevel != "45")
            //                                                                                                                                                                                                                    {
            //                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F49", "3", IDThanhVien, tableTVTF49.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                                                    }
            //                                                                                                                                                                                                                }
            //                                                                                                                                                                                                                else
            //                                                                                                                                                                                                                {
            //                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F49", "3", IDThanhVien, tableTVTF49.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                                                }
            //                                                                                                                                                                                                                #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF49.GioiThieu.ToString());
            //                                                                                                                                                                                                                if (leveeeel == "5")
            //                                                                                                                                                                                                                {
            //                                                                                                                                                                                                                    Plevel = "45";
            //                                                                                                                                                                                                                }
            //                                                                                                                                                                                                                #endregion
            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                        catch (Exception)
            //                                                                                                                                                                                                        { }
            //                                                                                                                                                                                                    }

            //                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF50
            //                                                                                                                                                                                                    user tableTVTF50 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF49.GioiThieu.ToString()));
            //                                                                                                                                                                                                    if (tableTVTF50 != null)
            //                                                                                                                                                                                                    {
            //                                                                                                                                                                                                        if (tableTVTF50.GioiThieu.ToString() != "0")
            //                                                                                                                                                                                                        {
            //                                                                                                                                                                                                            try
            //                                                                                                                                                                                                            {
            //                                                                                                                                                                                                                if (ShowFQRcode.ShowF50(tableTVTF50.iuser_id.ToString(), tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
            //                                                                                                                                                                                                                {
            //                                                                                                                                                                                                                    Dung = false;
            //                                                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
            //                                                                                                                                                                                                                }
            //                                                                                                                                                                                                                else
            //                                                                                                                                                                                                                {
            //                                                                                                                                                                                                                    Dung = true;
            //                                                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF50(tableTVTF50.iuser_id.ToString(), tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
            //                                                                                                                                                                                                                }
            //                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
            //                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
            //                                                                                                                                                                                                                {

            //                                                                                                                                                                                                                }
            //                                                                                                                                                                                                                else
            //                                                                                                                                                                                                                {
            //                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
            //                                                                                                                                                                                                                }
            //                                                                                                                                                                                                                if (Dung == true)
            //                                                                                                                                                                                                                {
            //                                                                                                                                                                                                                    if (TongLevel != "8")
            //                                                                                                                                                                                                                    {
            //                                                                                                                                                                                                                        if (TongLevel != "45")
            //                                                                                                                                                                                                                        {
            //                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F50", "3", IDThanhVien, tableTVTF50.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
            //                                                                                                                                                                                                                        }
            //                                                                                                                                                                                                                    }
            //                                                                                                                                                                                                                    else
            //                                                                                                                                                                                                                    {
            //                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F50", "3", IDThanhVien, tableTVTF50.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), "0", IDMaDonTao, "0");
            //                                                                                                                                                                                                                    }
            //                                                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
            //                                                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF50.GioiThieu.ToString());
            //                                                                                                                                                                                                                    if (leveeeel == "5")
            //                                                                                                                                                                                                                    {
            //                                                                                                                                                                                                                        Plevel = "45";
            //                                                                                                                                                                                                                    }
            //                                                                                                                                                                                                                    #endregion
            //                                                                                                                                                                                                                }
            //                                                                                                                                                                                                            }
            //                                                                                                                                                                                                            catch (Exception)
            //                                                                                                                                                                                                            { }
            //                                                                                                                                                                                                        }
            //                                                                                                                                                                                                    }
            //                                                                                                                                                                                                    #endregion
            //                                                                                                                                                                                                }
            //                                                                                                                                                                                                #endregion
            //                                                                                                                                                                                            }
            //                                                                                                                                                                                            #endregion
            //                                                                                                                                                                                        }
            //                                                                                                                                                                                        #endregion
            //                                                                                                                                                                                    }
            //                                                                                                                                                                                    #endregion
            //                                                                                                                                                                                }
            //                                                                                                                                                                                #endregion
            //                                                                                                                                                                            }
            //                                                                                                                                                                            #endregion
            //                                                                                                                                                                        }
            //                                                                                                                                                                        #endregion
            //                                                                                                                                                                    }
            //                                                                                                                                                                    #endregion
            //                                                                                                                                                                }
            //                                                                                                                                                                #endregion
            //                                                                                                                                                            }
            //                                                                                                                                                            #endregion

            //                                                                                                                                                        }
            //                                                                                                                                                        #endregion
            //                                                                                                                                                    }
            //                                                                                                                                                    #endregion
            //                                                                                                                                                }
            //                                                                                                                                                #endregion
            //                                                                                                                                            }
            //                                                                                                                                            #endregion
            //                                                                                                                                        }
            //                                                                                                                                        #endregion
            //                                                                                                                                    }
            //                                                                                                                                    #endregion
            //                                                                                                                                }
            //                                                                                                                                #endregion
            //                                                                                                                            }
            //                                                                                                                            #endregion

            //                                                                                                                        }
            //                                                                                                                        #endregion
            //                                                                                                                    }
            //                                                                                                                    #endregion
            //                                                                                                                }
            //                                                                                                                #endregion
            //                                                                                                            }
            //                                                                                                            #endregion
            //                                                                                                        }
            //                                                                                                        #endregion


            //                                                                                                    }
            //                                                                                                    #endregion
            //                                                                                                }
            //                                                                                                #endregion
            //                                                                                            }
            //                                                                                            #endregion
            //                                                                                        }
            //                                                                                        #endregion
            //                                                                                    }
            //                                                                                    #endregion
            //                                                                                }
            //                                                                                #endregion
            //                                                                            }
            //                                                                            #endregion


            //                                                                        }
            //                                                                        #endregion
            //                                                                    }
            //                                                                    #endregion
            //                                                                }
            //                                                                #endregion
            //                                                            }
            //                                                            #endregion
            //                                                        }
            //                                                        #endregion
            //                                                    }
            //                                                    #endregion
            //                                                }
            //                                                #endregion
            //                                            }
            //                                            #endregion
            //                                        }
            //                                        #endregion
            //                                    }
            //                                    #endregion
            //                                }
            //                                #endregion

            //                            }
            //                            #endregion
            //                        }
            //                        #endregion
            //                    }
            //                    #endregion


            //                }
            //                #endregion
            //            }
            //            #endregion
            //        }
            //        #endregion
            //    }
            //    #endregion
            //    #endregion
            //}
            //#endregion
        }

        #region Kèm theo Hoa Hong
        void ThemHoaHong_ThuongLevel(string IDProducts, string ThuTu, string IDType, string IDThanhVien, string IDUserNguoiDuocHuong, string SoCoin, string LevelThanhVienA, string LevelThanhVienB, string IDCart, string Noidung)
        {
            //Library.WriteErrorLog("  LevelThanhVienA: " + LevelThanhVienA + " - LevelThanhVienB: " + LevelThanhVienB);
            if (TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()) != "0")
            {
                #region Hoa hồng gián tiếp khi giới thiệu được hưởng sau khi lấy level A - level B
                double SoPhanTram = Convert.ToDouble(TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()));
                double TongTien = Convert.ToDouble(SoCoin);
                double ThuongLevel = (TongTien * SoPhanTram) / 100;
                //Library.WriteErrorLog("  SoPhanTram: " + SoPhanTram + "  IDThanhVien: " + IDThanhVien + " IDUserNguoiDuocHuong: " + IDUserNguoiDuocHuong + " ThuongLevel: " + ThuongLevel);
                ThemHoaHong(IDProducts, IDType, "Hoa hồng quản lý Level " + ThuTu, IDThanhVien, IDUserNguoiDuocHuong, SoPhanTram.ToString(), ThuongLevel.ToString(), IDCart, Noidung);
                #endregion
            }
        }
        void ThemHoaHong(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDCart, string NoiDung)
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

            CongTien(IDType, IDUserNguoiDuocHuong, SoCoin);
            //CongTien_ViTienHHGioiThieu(IDProducts, IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, SoCoin);

        }
        void CongTien(string Type, string IDUserNguoiDuocHuong, string SoCoin)
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
        public string TinhDiemthuongGiantiep(string LevelA, string LevelB)
        {
            if (LevelA.Length > 0 && LevelB.Length > 0)
            {
                if (Convert.ToDouble(LevelA.ToString()) > Convert.ToDouble(LevelB.ToString()))
                {
                    double TLevelA = Convert.ToDouble(SetLevel(LevelA.ToString()));
                    double TLevelB = Convert.ToDouble(SetLevel(LevelB.ToString()));
                    double Tong = (TLevelA - TLevelB);
                    if (Tong != 0)
                    {
                        return Tong.ToString();
                    }
                }
            }
            return "0";
        }

        public string SetLevel(string Level)
        {
            Double DauVao = Convert.ToDouble(Level);
            if (DauVao == 0)
            {
                return "0";
            }
            else if (DauVao == 1)
            {
                return "2";
            }
            else if (DauVao == 2)
            {
                return "4";
            }
            else if (DauVao == 3)
            {
                return "6";
            }
            else if (DauVao == 4)
            {
                return "8";
            }
            else if (DauVao == 5)
            {
                return "10";
            }
            return "0";
        }

        protected string TimLevelB(string ID)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            user iitems = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(ID.ToString()));
            if (iitems != null)
            {
                return iitems.LevelThanhVien.ToString();
            }
            return "0";
        }
        #region Tìm giá trị lớn nhất trong level để thưởng cho các đời F1 đến F5
        public string MinAndMax(string c)
        {
            String intString = c.Replace("99999999999,", ""); ;//.Replace(",0", "").Replace("0,", "");
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
                return "8";
            }
            else
            {
                return max.ToString();
            }
            return "8";// Nếu trong toàn bộ đều có level =0 thì gán cho nó là 8
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

        #region Tìm ra người giới thiệu gần nhất để cho Level
        protected string ShowF2(string IDF1, string IDF2)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " ");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF3(string IDF1, string IDF2, string IDF3)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " ");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + " ");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + " ");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF4(string IDF1, string IDF2, string IDF3, string IDF4)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " ");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + " ");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + " ");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf4 = Susers.Name_Text("select * from users  where iuser_id=" + IDF4 + " ");
            if (dtf4.Count > 0)
            {
                return dtf4[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF5(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " ");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + " ");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + " ");
            if (dtf5.Count > 0)
            {
                return dtf5[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        #endregion

        #endregion
    }
}