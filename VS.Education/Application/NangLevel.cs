using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TestWindowService;
using VS.E_Commerce;

public class NangLevel
{
    public static void UpDate_NangLevel(string IDThanhVien)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        #region Nâng Level cho thành viên và level ko có chi nhánh trong đây (Chỉ nâng cấp level cho thành viên và leader)
        //:TH1: khi thành viên tích lũy đủ 24 triệu vnd thì dc lên level1
        // Th2: khi cấp dưới có 5 nhân sự lên level 1 thì mới được lên level2
        user iitems = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien.ToString()));//&& p.ChiNhanh == 0
        if (iitems != null)
        {
            if (iitems.LevelThanhVien == 0)
            {
                var Tong = db.S_Member_GetID_TongSoDiemHoaHong(int.Parse(IDThanhVien.ToString())).ToList();
                if (Tong.Count() >= 0)
                {
                    if (Tong[0].sodiem.ToString() != "")
                    {
                        double TongTienLevel = Convert.ToDouble(Tong[0].sodiem.ToString());
                        double SotienCanVuotQua = Convert.ToDouble(Commond.Setting("txttienlenlevel"));
                        //Convert.ToDouble("24000");
                        if (TongTienLevel >= SotienCanVuotQua)
                        {
                            //:TH1: khi thành viên tích lũy đủ 24 triệu vnd thì dc lên level1
                            Susers.Name_Text("update users set LevelThanhVien=1  where iuser_id=" + iitems.iuser_id.ToString() + "");
                            Lichsucapdo(iitems.iuser_id.ToString(), "1", "Auto");
                        }
                    }
                }
            }
            else if (iitems.LevelThanhVien == 1)
            {
                if (Convert.ToDouble(LayRa5Level(iitems.iuser_id.ToString(), "1")) >= 3)// 5 là sẽ phải có 5 thành viên thì mới cho tăng level2 : con số (2 là đang ở Level2 )
                {
                    Susers.Name_Text("update users set LevelThanhVien=2  where iuser_id=" + iitems.iuser_id.ToString() + "");
                    Lichsucapdo(iitems.iuser_id.ToString(), "2", "Auto");
                }
            }
            else if (iitems.LevelThanhVien == 2)
            {
                if (Convert.ToDouble(LayRa5Level(iitems.iuser_id.ToString(), "2")) >= 3)// 5 là sẽ phải có 5 thành viên thì mới cho tăng level2 : con số (2 là đang ở Level2 )
                {
                    Susers.Name_Text("update users set LevelThanhVien=3  where iuser_id=" + iitems.iuser_id.ToString() + "");
                    Lichsucapdo(iitems.iuser_id.ToString(), "3", "Auto");
                }
            }
            else if (iitems.LevelThanhVien == 3)
            {
                if (Convert.ToDouble(LayRa5Level(iitems.iuser_id.ToString(), "3")) >= 3)// 5 là sẽ phải có 5 thành viên thì mới cho tăng level2
                {
                    Susers.Name_Text("update users set LevelThanhVien=4 where iuser_id=" + iitems.iuser_id.ToString() + "");
                    Lichsucapdo(iitems.iuser_id.ToString(), "4", "Auto");
                }
            }
            else if (iitems.LevelThanhVien == 4)
            {
                if (Convert.ToDouble(LayRa5Level(iitems.iuser_id.ToString(), "4")) >= 3)// 5 là sẽ phải có 5 thành viên thì mới cho tăng level2
                {
                    Susers.Name_Text("update users set LevelThanhVien=5  where iuser_id=" + iitems.iuser_id.ToString() + "");
                    Lichsucapdo(iitems.iuser_id.ToString(), "5", "Auto");
                }
            }
            else if (iitems.LevelThanhVien == 5)
            {
                if (Convert.ToDouble(LayRa5Level(iitems.iuser_id.ToString(), "5")) >= 3)// 5 là sẽ phải có 5 thành viên thì mới cho tăng level2
                {
                    Susers.Name_Text("update users set LevelThanhVien=6  where iuser_id=" + iitems.iuser_id.ToString() + "");
                    Lichsucapdo(iitems.iuser_id.ToString(), "6", "Auto");
                }
            }
        }
        #endregion
    }

    public static string Lichsucapdo(string IDThanhViens, string LevelThanhViens, string Nguoitao)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        LichSuLevel obj = new LichSuLevel();
        obj.IDThanhVien = int.Parse(IDThanhViens);
        obj.NgayLenCap = DateTime.Now;
        obj.CapLevel = int.Parse(LevelThanhViens);
        obj.NguoiTao = Nguoitao;
        db.LichSuLevels.InsertOnSubmit(obj);
        db.SubmitChanges();

        return "";
    }
    #region Lấy ra 5 level gần cấp với thành viên nhất
    //public static string LayRa5Level_OLD(string id, string LevelThanhVien)
    //{
    //    DatalinqDataContext db = new DatalinqDataContext();
    //    try
    //    {
    //        // Theo chỉ đạo từ anh đào ngày 09/06 sau khi trao đổi với Anh thủy 
    //        // Sẽ đựa vào 1 cấp F1
    //        // Trong các F1 phải ít nhất có 1 ng được sao (LevelThanhVien) thì mưới được là đủ điều kiện

    //        //var dt = db.S_Members_LayRa5Level_Tree(int.Parse(id), int.Parse(LevelThanhVien)).ToList();
    //        //if (dt.Count > 0)
    //        //{
    //        //    str = dt[0].Tong.ToString();
    //        //}

    //        int F1 = 0;// Tìm ra 5 người F1
    //        int FN = 0;// Trong 1 dây phải có 1 người có sao thì mới dc tính 
    //        var dt = db.S_Members_LayRa5Level_Tree_5CapF1(int.Parse(id), int.Parse(LevelThanhVien)).ToList();
    //        if (dt.Count > 0)
    //        {
    //            foreach (var item in dt)
    //            {
    //                F1 += 1;
    //                var dt2 = db.S_Members_LayRa5Level_TreeFN(int.Parse(item.iuser_id.ToString()), int.Parse(LevelThanhVien)).ToList();
    //                if (dt2 != null)
    //                {
    //                    if (dt2.Count > 0)
    //                    {
    //                        FN += 1;
    //                    }
    //                }
    //            }
    //        }
    //        Double TongDiem = F1 + FN;
    //        //10 là tính 5 thành viên F1 và con của 5 thành viên F1 nữa là 10
    //        if (TongDiem >= 10)
    //        {
    //            return "5";
    //        }
    //    }
    //    catch (Exception)
    //    { }
    //    return "0";
    //}
    public static string LayRa5Level(string id, string LevelThanhVien)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        // Theo chỉ đạo từ anh đào ngày 15/06 sau khi trao đổi với Anh thủy 
        // Sẽ đựa vào 1 cấp F1
        // Trong Tìm trong 5 nhánh mỗi 1 nhánh chỉ cần có 1 người đạt sao là được tính


        // Xếp vân ngày : 07/09 cùng với E giang có yêu cầu , số tiền lên level1 : 5 triệu vnd và chỉ cần 3 người trong chi nhánh đạt thì lên sao.

        int FN = 0;// Trong 1 dây phải có 1 người có sao thì mới dc tính 
        var dt = db.S_Members_LayRa5Level_Tree_5CapF1(int.Parse(id)).ToList();
        if (dt.Count > 0)
        {
            //string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
            //Library.WriteErrorLog("id:" + id);

            foreach (var item in dt)
            {
                if (item.LevelThanhVien == int.Parse(LevelThanhVien)) // tìm F1 xem có trùng level ko thì tính điểm
                {
                    //Library.WriteErrorLog("item.LevelThanhVien == int.Parse(LevelThanhVien):" + item.iuser_id.ToString() + " - " + LevelThanhVien);
                    FN += 1;
                }
                else // nếu F1 ko có thì tìm F2... Fn
                {
                    var dt2 = db.S_Members_LayRa5Level_TreeFN(int.Parse(item.iuser_id.ToString()), int.Parse(LevelThanhVien)).ToList();
                    if (dt2 != null)
                    {
                        if (dt2.Count > 0)
                        {
                            // Library.WriteErrorLog("nếu F1 ko có thì tìm F2... Fn" + item.iuser_id.ToString());
                            FN += 1;
                        }
                    }
                }
            }
        }
        // return FN.ToString();
        Double TongDiem = FN;
        ////10 là tính 5 thành viên F1 và con của 5 thành viên F1 nữa là 10
        // code cũ đang là 5
        //if (TongDiem >= 5)
        //{
        //    return "5";
        //}
        //else
        //{
        //    return "0";
        //}

        // code mới là 3 nhé. theo yêu cầu xếp vân (ngày : 07/09)
        if (TongDiem >= 3)
        {
            return "3";
        }
        else
        {
            return "0";
        }

    }
    #endregion
}
