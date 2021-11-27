using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWindowService;

namespace VS.E_Commerce
{
    public partial class ThanhVienTest : System.Web.UI.Page
    {
        DatalinqDataContext db = new DatalinqDataContext();

        string TM = "0";
        string AFF = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //Double TongCong = 0;
                //Double TongCong1 = 0;
                //Double TongCong2 = 0;

                //Double TongTienDonHang = Convert.ToDouble(158);

                //Double TongTienTM = Convert.ToDouble(110);
                //Double ViMuaHangAFF = Convert.ToDouble(58);
                //if (ViMuaHangAFF >= TongTienDonHang)
                //{
                //    TongCong = (ViMuaHangAFF - TongTienDonHang);
                //    Response.Write("1, ViMuaHangAFF bị trừ: " + TongCong.ToString() + "<br>");
                //}
                //else if (TongTienTM >= TongTienDonHang)
                //{
                //    TongCong = (TongTienTM - TongTienDonHang);
                //    Response.Write("2, TongTienTM bị trừ: " + TongCong.ToString() + "<br>");
                //}

                //else if (ViMuaHangAFF < TongTienDonHang)
                //{
                //    TongCong1 = (ViMuaHangAFF);
                //    TongCong2 = (TongTienDonHang - TongCong1);
                //    if (TongTienTM >= TongCong2)
                //    {
                //        Response.Write("3, ViMuaHangAFF bị trừ: " + TongCong1.ToString() + "<br>");
                //        Response.Write("3, TongTienTM bị trừ: " + TongCong2.ToString() + "<br>");
                //        TongCong = (TongTienTM - TongCong2);
                //    }
                //}
                //Response.Write(TongCong);

                ////double a = Convert.ToDouble(85.92);
                ////double B = Convert.ToDouble(158);
                ////double TTTCONGLAI = a - B;

                ////Response.Write(TTTCONGLAI.ToString() + "<br>");


                ////double a1 = Convert.ToDouble(566.08);
                ////double B1 = Convert.ToDouble(638);
                ////double TTTCONGLAI2 = a1 - B1;

                ////Response.Write(TTTCONGLAI2.ToString() + "<br>");

                //double a = Convert.ToDouble(-71.92);
                //double B = Convert.ToDouble(158);
                //double TTTCONGLAI = a + B;

                //Response.Write(TTTCONGLAI.ToString()+"<br>");




                //decimal a = (decimal)85.92;
                //decimal B = (decimal)158;
                //decimal TTTCONGLAI = a - B;

                //Response.Write(TTTCONGLAI.ToString() + "<br>");

                //decimal a1 = (decimal)566.08;
                //decimal B1 = (decimal)638;
                //decimal TTTCONGLAI2 = (a1 - B1);

                //Response.Write(TTTCONGLAI2.ToString() + "<br>");



                // chuyển điểm hoa hồng aff sang ví mua hàng
                ////List<Entity.users> dt = Susers.Name_Text("select * from users where iuser_id in (23,64646,65642,65788,67116,63773,92,64028,8868,65450,63172,3527,65828,66953,66870,64105,62433,64205,67208,66936,63521,63000,66793,63120,15,67248,67242,64735,65270,64629,64474,66893,63696,63541,64723,62067,63790,62133,66873,66827,66432,64451,67119,64311,61901,66867,26,67125,66392,66982,65087,86,61984,65253,63089,61958,62425,67096,67025,63836,62568,63538,61964,62854,66933,66939,37235,64875,61821,66890,63644,1280,61921,63794,66782,62752,37777,66874,65571,61687,62105,62228,65780,113,64807,66822,67166,66218,62234,67206,64057,61750,38092,67306,107,65205,66865,81,65468,61859,63310,65105,101,64678,66848,63682,61830,62912,62712,65268,66934,66891,66250,61773,24,61375,66831,35742,67094,65428,3018,65674,62640,4847,62285,66862,66370,176,61747,63419,124,67257,62076,66819,3450,63685,37313,67123,67200,67100,67037,64867,67143,65520,61913,64667,1286,147,61802,65715,65383,96,66757,66880,2967,65523,328,61988,63076,25,63047,228,65274,51357,85,66677,66385,65223,67132,65317,63600,63537,64619,62220,61965,66940,67181,67232,1281,64198,65858,62987,65171,63010,67092,64175,62037,67069,66923,66900,67023,64785,63860,66328,66963,66720,61845,64493,66866,67198,67149,64350,61882,10444,65469,63903,67172,67098,62080,65400,65506,62558,62807,62409,63442,67229,63348,66004,64232,64871,66878,67187,66755,65427,186,66778,61952,65627,472,66809,61800,64207,67256,83,63784,63449,63406,1385,632,65421,67124,64688,63114,63835,67273,66987,67230,65464,65458,62610,61920,64482,143,65407,65384,120,65501,63440,62444,66758,65570,65670,63457,3701,66480,35769,67061,63171,65530,63363,63672,64645,28,67107,63125,66841,64116,63758,66414,66649,64591,61860,64614,37,62899,64073,63054,60835,64236,66400,63589,66792,13917,67133)");
                ////// List<Entity.users> dt = Susers.Name_Text("select * from users where iuser_id in (9432,15,23,24,25,26,9436,9437,87)");//9432,15,23,24,25,26,9436,9437
                ////if (dt.Count > 0)
                ////{
                ////    foreach (var item in dt)
                ////    {
                ////        // thống kê tổng số điểm dc hưởng hoa hồng
                ////        //List<Entity.EHoaHongThanhVien> dt2 = SHoaHongThanhVien.Name_Text("SELECT  sum(convert(float,(Socoin))) as Socoin  FROM HoaHongThanhVien  WHERE  IDType in (1,2,3,4,5) and (NgayTao>='07/01/2020' and  NgayTao<='07/27/2020') and IDUserNguoiDuocHuong =" + item.iuser_id.ToString() + "");
                ////        var dt2 = db.S_HoaHongSSSSSSSSSSSSSSSSSSS(int.Parse(item.iuser_id.ToString())).ToList();
                ////        if (dt2.Count > 0)
                ////        {
                ////            if (dt2[0].Socoin.HasValue)
                ////            {
                ////                // Response.Write(item.iuser_id.ToString() + " - " + dt2[0].Socoin.ToString()+"<br>");
                ////                // so sánh ví xem có đủ ko nếu đủ thì cho sang ví mua hàng, ko đủ thì thôi
                ////                Double ViAF = Convert.ToDouble(item.VIAAFFILIATE.ToString());
                ////                Double Hoahong = Convert.ToDouble(dt2[0].Socoin.ToString());
                ////                Double ViMuaHang = Convert.ToDouble(item.ViMuaHangAFF.ToString());
                ////                if (ViAF >= Hoahong)
                ////                {
                ////                    // Cộng điểm tương ứng với hoa hồng vào ví mua hàng
                ////                    double Conglais = 0;
                ////                    Conglais = ((ViMuaHang) + (Hoahong));
                ////                    Susers.Name_Text("update users set ViMuaHangAFF=" + Conglais.ToString() + " where iuser_id=" + item.iuser_id.ToString() + "");

                ////                    // Trừ hoa hồng trong ví VIAAFFILIATE đi
                ////                    double Trudi = 0;
                ////                    Trudi = ((ViAF) - (Hoahong));
                ////                    Susers.Name_Text("update users set VIAAFFILIATE=" + Trudi.ToString() + " where iuser_id=" + item.iuser_id.ToString() + "");

                ////                    Library.WriteErrorLog("TH: if (ViAF >= Hoahong) --> iuser_id:" + item.iuser_id.ToString());
                ////                    Library.WriteErrorLog("Hoahong:" + Hoahong);

                ////                    Library.WriteErrorLog("Trước khi cộng: ViMuaHangAFF:" + ViMuaHang);
                ////                    Library.WriteErrorLog("Sau khi cộng lại: ViMuaHangAFF:" + Conglais);
                ////                    Library.WriteErrorLog("Trước khi trừ: VIAAFFILIATE:" + ViAF);
                ////                    Library.WriteErrorLog("Sau khi trừ lại: VIAAFFILIATE:" + Trudi);
                ////                    Library.WriteErrorLog("********");
                ////                }
                ////                else if (ViAF < Hoahong)
                ////                {
                ////                    // Cộng điểm tương ứng với hoa hồng vào ví mua hàng
                ////                    double Conglais = 0;
                ////                    Conglais = ((ViMuaHang) + (ViAF));
                ////                    Susers.Name_Text("update users set ViMuaHangAFF=" + Conglais.ToString() + " where iuser_id=" + item.iuser_id.ToString() + "");

                ////                    Library.WriteErrorLog(" TH: if (ViAF < Hoahong) --> iuser_id:" + item.iuser_id.ToString());
                ////                    //Library.WriteErrorLog("Hoahong:" + Hoahong); nếu if (ViAF < Hoahong) thì ko cần quan tâm hoa hồng , chỉ cần chuển luôn điêm sang là dc

                ////                    Library.WriteErrorLog("Trước khi cộng: ViMuaHangAFF:" + ViMuaHang);
                ////                    Library.WriteErrorLog("Sau khi cộng lại: ViMuaHangAFF:" + Conglais);
                ////                    Library.WriteErrorLog("Trước trừ cộng: VIAAFFILIATE:" + ViAF);

                ////                    // Trừ hoa hồng trong ví VIAAFFILIATE đi
                ////                    double Trudi = 0;
                ////                    Trudi = ((ViAF) - (ViAF));
                ////                    Susers.Name_Text("update users set VIAAFFILIATE=" + Trudi.ToString() + " where iuser_id=" + item.iuser_id.ToString() + "");

                ////                    Library.WriteErrorLog("Sau khi trừ lại: VIAAFFILIATE:" + Trudi);
                ////                    Library.WriteErrorLog("********");


                ////                }

                ////            }



                ////        }
                ////    }
                ////}

                //double Tiencoin = Convert.ToDouble(Commond.Setting("TienKichHoat"));
                //string IDMaDonTao = "0";
                //string Ngtaaa = "0";
                //List<Entity.users> dt = Susers.Name_Text("select * from users where  iuser_id in (67205,67206,67207,67208,67209,67210,67211,67212,67213,67214,67215,67216,67217,67218,67219,67220,67221,67222,67224,67227,67228,67229,67230,67232,67233,67234,67235,67236,67239,67240,67242,67243,67245,67246,67248,67249,67251,67254,67256,67257,67259,67264,67265,67266,67267,67268,67270,67271,67272,67273,67274,67275,67276,67280,67281,67282,67283,67288,67289,67291,67292,67294,67295,67296,67297,67298,67299,67300,67301,67302,67304,67305,67306,67307,67308,67309,67310,67311,67312,67313,67315,67319,67323,67324,67328,67330,67332,67333,67334,67337,67338,67339,67340,67342,67344,67345) ");
                //if (dt.Count > 0)
                //{
                //    foreach (var item in dt)
                //    {
                //        List<Entity.EHoaHongThanhVien> dtd = SHoaHongThanhVien.Name_Text("select * from HoaHongThanhVien where  IDType=1 and IDThanhVien=" + item.iuser_id.ToString() + " ");
                //        if (dtd.Count > 0)
                //        {
                //            IDMaDonTao = dtd[0].IDCart.ToString();
                //            Ngtaaa = dtd[0].NgayTao.ToString();
                //        }

                //        var tongdiemdachia = db.S_TongDiemDaChia_DangKyThanhVien(int.Parse(item.iuser_id.ToString()), Convert.ToInt64(IDMaDonTao)).ToList();
                //        if (tongdiemdachia[0].sodiem >= 0)
                //        {
                //            Double TongDaChia = Convert.ToDouble(tongdiemdachia[0].sodiem.ToString());
                //            Double TongCongs = Tiencoin - TongDaChia;

                //            List<Entity.ELoiNhuanDangKyThanhVien> ssss = SLoiNhuanDangKyThanhVien.Name_Text("select * from LoiNhuanDangKyThanhVien where IDThanhVienDangKy=" + item.iuser_id.ToString() + " ");
                //            if (ssss.Count <= 0)
                //            {
                //                LoiNhuanDangKyThanhVien abln = new LoiNhuanDangKyThanhVien();
                //                abln.IDThanhVienDangKy = int.Parse(item.iuser_id.ToString());
                //                abln.IDThanhVienGioiThieu = int.Parse(item.GioiThieu.ToString());
                //                abln.MoTa = "Lợi nhuận đăng ký thành viên";
                //                abln.NgayTao = Convert.ToDateTime(Ngtaaa);
                //                abln.SoDiemNapVao = Tiencoin.ToString();
                //                abln.SoDiemConLai = TongCongs.ToString();
                //                abln.SoDiemDaChia = TongDaChia.ToString();
                //                abln.MTreeIDThanhVienDangKy = Commond.ShowMTree(item.iuser_id.ToString());
                //                abln.MTReIDThanhVienGioiThieu = Commond.ShowMTree(item.GioiThieu.ToString());
                //                abln.IDMaDonTao = Convert.ToInt64(IDMaDonTao);
                //                abln.IDChiNhanh = Convert.ToInt32(item.IDChiNhanh.ToString());
                //                abln.IDLeader = Convert.ToInt32(TimLeader(item.iuser_id.ToString()));
                //                db.LoiNhuanDangKyThanhViens.InsertOnSubmit(abln);
                //                db.SubmitChanges();
                //            }
                //        }
                //    }
                //}

                //189.369863013699
                //216000000
                //32


                //double GoiDauTu = Convert.ToDouble("216000000");
                //double BaHaiPhanTram = Convert.ToDouble("32");
                //double ChiaHH = (BaHaiPhanTram) / 100;

                //double DauVao1 = Convert.ToDouble(ChiaHH);// VD: "0.32"
                //double DauVao2 = Convert.ToDouble("365");
                //// double DauVao = Convert.ToDouble("200000000");

                //Double HoaHong2 = (DauVao1 / DauVao2);
                //Response.Write(HoaHong2.ToString() + "<br>");
                //Double HoaHong1 = (HoaHong2) * GoiDauTu;
                //Response.Write(HoaHong1.ToString() + "<br>");
                //Double HoaHong3 = (HoaHong1) / 1000;
                //Response.Write(HoaHong3.ToString() + "<br>");

                //List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users where  iuser_id in (" + Commond.DanhSachNhaCungCapSanPham() + ")  order by iuser_id asc");
                //if (dt.Count > 0)
                //{
                //    int i = 1;
                //    foreach (var item in dt)
                //    {
                //        List<Entity.Products> dt2 = SProducts.Name_Text("SELECT * FROM products where IDThanhVien=" + item.iuser_id.ToString() + " order by IDThanhVien asc");
                //        if (dt2.Count() > 0)
                //        {
                //            user abc = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(item.iuser_id.ToString()));
                //            abc.TongSoSanPham = dt2.Count();
                //            db.SubmitChanges();
                //        }
                //    }
                //}

                //List<Entity.users> dt = Susers.Name_Text("select * from users order by iuser_id asc");
                //List<Entity.users> dt1 = dt;
                //if (dt.Count > 0)
                //{
                //    foreach (var item in dt)
                //    {
                //        foreach (var item2 in dt1)
                //        {
                //            if ((item.iuser_id != item2.iuser_id) && (item2.GioiThieu != "0"))
                //            {
                //                if ((item.iuser_id == int.Parse(item2.GioiThieu)))
                //                {
                //                    user abcv = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(item2.GioiThieu.ToString()));

                //                    user abc = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(item2.iuser_id.ToString()));
                //                    string ccc = abcv.MTree + "|" + item2.GioiThieu + "|";
                //                    abc.MTree = ccc.Replace("||", "|");
                //                    db.SubmitChanges();
                //                }
                //            }

                //        }
                //    }
                //}

                //  LoadItems();
            }
        }

        //public static string TimLeader(string id)
        //{
        //    string str = "0";
        //    List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + " ");
        //    if (dt.Count > 0)
        //    {
        //        if (dt[0].Leader.ToString() == "1")
        //        {
        //            return dt[0].iuser_id.ToString();
        //        }
        //        else
        //        {
        //            str = dt[0].GioiThieu.ToString();
        //            return TimLeader(str);
        //        }
        //    }
        //    return str;
        //}

        //public void LoadItems()
        //{
        //    string str = "";
        //    int Tongsobanghi = 0;
        //    Int16 pages = 1;
        //    int Tongsotrang = int.Parse("1000");
        //    if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
        //    {
        //        pages = Convert.ToInt16(Request.QueryString["page"].Trim());
        //    }
        //    string sql1 = "";

        //    List<Entity.TongSo> iitem = Susers.CATEGORY_PHANTRANG1("0", "", "0", "-1", "-1", "-1", sql1);
        //    if (iitem.Count() > 0)
        //    {
        //        Tongsobanghi = iitem[0].Tong;
        //    }
        //    List<Entity.users> dt = Susers.CATEGORY_PHANTRANG2("0", "", "0", "-1", "-1", "-1", sql1, (pages - 1), Tongsotrang);
        //    if (dt.Count >= 1)
        //    {
        //        // Repeater1.DataSource = dt;
        //        //Repeater1.DataBind();

        //        foreach (var item in dt)
        //        {
        //            // List<Entity.users> dtv = Susers.Name_Text("select * from users where ((MTree LIKE N'%|" + item.iuser_id + "|%')) order by iuser_id asc");
        //            user abcc = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(item.iuser_id.ToString()) && p.MTree.Contains("|" + item.iuser_id.ToString() + "|"));
        //            if (abcc == null)
        //            {
        //                //  user abc = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(item.iuser_id.ToString()));
        //                // string ccc = item.MTree + item.iuser_id + "|";
        //                // abc.MTree = ccc.Replace("||", "|");
        //                //  db.SubmitChanges();
        //                str += "(" + item.iuser_id + ")" + item.MTree + "<br>";
        //            }
        //        }
        //    }
        //    Literal1.Text = str;
        //    if (Tongsobanghi % Tongsotrang > 0)
        //    {
        //        Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
        //    }
        //    else
        //    {
        //        Tongsobanghi = Tongsobanghi / Tongsotrang;
        //    }
        //    ltpage.Text = Commond.Phantrang("/ThanhVienTest.aspx", Tongsobanghi, pages);
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users where  iuser_id=87 order by iuser_id asc");
            if (dt.Count > 0)
            {

                Double TongCong = 0;
                Double TongCong1 = 0;
                Double TongCong2 = 0;

                Double TongTienDonHang = Convert.ToDouble(TextBox1.Text);

                Double TongTienTM = Convert.ToDouble(dt[0].TongTienCoinDuocCap.ToString());
                Double ViMuaHangAFF = Convert.ToDouble(dt[0].ViMuaHangAFF.ToString());
                if (ViMuaHangAFF >= TongTienDonHang)
                {
                    Session["AFF"] = TongCong.ToString();
                    TongCong = (ViMuaHangAFF - TongTienDonHang);
                    Response.Write("1, ViMuaHangAFF còn lại: " + TongCong.ToString() + "<br>");

                    Susers.Name_Text("update users set ViMuaHangAFF=" + TongCong.ToString() + " where iuser_id=" + dt[0].iuser_id.ToString() + "");
                }
                else if (TongTienTM >= TongTienDonHang)
                {
                    Session["TM"] = TongCong.ToString();
                    TongCong = (TongTienTM - TongTienDonHang);
                    Response.Write("2, TongTienTM còn lại: " + TongCong.ToString() + "<br>");
                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + TongCong.ToString() + " where iuser_id=" + dt[0].iuser_id.ToString() + "");
                }

                else if (ViMuaHangAFF < TongTienDonHang)
                {
                    TongCong1 = (ViMuaHangAFF);
                    TongCong2 = (TongTienDonHang - ViMuaHangAFF);
                    if (TongTienTM >= TongCong2)
                    {
                        Double TienTMSauKhiBiTru = (TongTienTM - TongCong2);

                        Susers.Name_Text("update users set ViMuaHangAFF=0 where iuser_id=" + dt[0].iuser_id.ToString() + "");
                        Susers.Name_Text("update users set TongTienCoinDuocCap=" + TienTMSauKhiBiTru.ToString() + " where iuser_id=" + dt[0].iuser_id.ToString() + "");

                        Session["AFF"] = TongCong1.ToString();
                        Session["TM"] = TongCong2.ToString();
                        Response.Write("3, ViMuaHangAFF bị trừ: " + TongCong1.ToString() + "<br>");
                        Response.Write("4, TongTienTM bị trừ: " + TongCong2.ToString() + "<br>");

                    }
                    else
                    {
                        Response.Write("5, Không thể thanh toán <br>");
                    }
                }
                // Response.Write(TongCong);
            }
        }

        protected void bttrahag_Click(object sender, EventArgs e)
        {

            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users where  iuser_id=87 order by iuser_id asc");
            if (dt.Count > 0)
            {
                Double VTongTienTM = 0;
                Double VTongTienAFF = 0;

                Double TongTienTM = Convert.ToDouble(dt[0].TongTienCoinDuocCap.ToString());
                Double ViMuaHangAFF = Convert.ToDouble(dt[0].ViMuaHangAFF.ToString());

                VTongTienTM = TongTienTM + Convert.ToDouble(Session["AFF"]);
                VTongTienAFF = ViMuaHangAFF + Convert.ToDouble(Session["TM"]);

                Susers.Name_Text("update users set ViMuaHangAFF=" + VTongTienAFF.ToString() + ",TongTienCoinDuocCap=" + VTongTienTM.ToString() + " where iuser_id=87 ");
            }
        }

    }
}