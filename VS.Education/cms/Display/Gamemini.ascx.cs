using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display
{
    public partial class Gamemini : System.Web.UI.UserControl
    {
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Loadshow();
            }
        }
        public void Loadshow()
        {
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                GameTangXu abc1 = db.GameTangXus.SingleOrDefault(p => p.IDThanhVien == Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID")));
                if (abc1 != null)
                {
                    int TongNgay = Convert.ToInt32(abc1.SoNgay + 1);
                    DateTime NgayTao = Convert.ToDateTime(abc1.NgayCapNhat.ToString()).Date;
                    DateTime NgayHienTai = DateTime.Now.Date;
                    TimeSpan diff = NgayHienTai - NgayTao;
                    // Response.Write(NgayTao);
                    // sau 2 ngay thi hệ thống tự động sét thành ngày đầu tiên
                    if (diff.Days >= 2)
                    {
                        GameTangXu ccc = db.GameTangXus.SingleOrDefault(p => p.IDThanhVien == Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID")));
                        if (ccc != null)
                        {
                            abc1.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                            abc1.SoNgay = 0;
                            abc1.NgayCapNhat = DateTime.Now;
                            // abc.TongSoDiem = "0";
                            db.SubmitChanges();
                        }
                    }

                    // Hết 7 ngày  thi hệ thống tự động sét thành ngày đầu tiên
                    if (abc1.SoNgay.ToString() == "7" && diff.Days != 0)
                    {
                        //  Response.Write(diff);
                        GameTangXu ccc = db.GameTangXus.SingleOrDefault(p => p.IDThanhVien == Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID")));
                        if (ccc != null)
                        {
                            abc1.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                            abc1.SoNgay = 0;
                            abc1.NgayCapNhat = DateTime.Now;
                            // abc.TongSoDiem = abc.TongSoDiem;
                            db.SubmitChanges();
                        }
                    }
                }


                GameTangXu abc = db.GameTangXus.SingleOrDefault(p => p.IDThanhVien == Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID")));
                if (abc != null)
                {
                    int TongNgay = Convert.ToInt32(abc.SoNgay + 1);
                    DateTime NgayTao = Convert.ToDateTime(abc.NgayCapNhat.ToString()).Date;
                    DateTime NgayHienTai = DateTime.Now.Date;
                    TimeSpan diff = NgayHienTai - NgayTao;
                    // Response.Write(NgayTao);
                    // sau 2 ngay thi hệ thống tự động sét thành ngày đầu tiên

                    if (diff.Days == 0)
                    {

                        if (abc.SoNgay.ToString() == "0")
                        {
                            if (TongNgay == 0)
                            {
                                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu1") + "</b>";
                            }
                            else if (TongNgay == 1)
                            {
                                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu1") + "</b>";
                            }
                            else if (TongNgay == 2)
                            {
                                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu2") + "</b>";
                            }
                            else if (TongNgay == 3)
                            {
                                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu3") + "</b>";
                            }
                            else if (TongNgay == 4)
                            {
                                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu4") + "</b>";
                            }
                            else if (TongNgay == 5)
                            {
                                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu5") + "</b>";
                            }
                            else if (TongNgay == 6)
                            {
                                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu6") + "</b>";
                            }
                            else if (TongNgay == 7)
                            {
                                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu7") + "</b>";
                            }
                            else
                            {
                                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu1") + "</b>";
                            }
                            lnknhanxungay.Enabled = true;
                        }
                        else
                        {
                            if (TongNgay == 1)
                            {
                                ltdiem.Text = "Quay lại vào ngày mai để nhận <b>" + Commond.Setting("txtngaythu1") + "</b>";
                            }
                            else if (TongNgay == 2)
                            {
                                ltdiem.Text = "Quay lại vào ngày mai để nhận <b>" + Commond.Setting("txtngaythu2") + "</b>";
                            }
                            else if (TongNgay == 3)
                            {
                                ltdiem.Text = "Quay lại vào ngày mai để nhận <b>" + Commond.Setting("txtngaythu3") + "</b>";
                            }
                            else if (TongNgay == 4)
                            {
                                ltdiem.Text = "Quay lại vào ngày mai để nhận <b>" + Commond.Setting("txtngaythu4") + "</b>";
                            }
                            else if (TongNgay == 5)
                            {
                                ltdiem.Text = "Quay lại vào ngày mai để nhận <b>" + Commond.Setting("txtngaythu5") + "</b>";
                            }
                            else if (TongNgay == 6)
                            {
                                ltdiem.Text = "Quay lại vào ngày mai để nhận <b>" + Commond.Setting("txtngaythu6") + "</b>";
                            }
                            else if (TongNgay == 7)
                            {
                                ltdiem.Text = "Quay lại vào ngày mai để nhận <b>" + Commond.Setting("txtngaythu7") + "</b>";
                            }
                            else
                            {
                                ltdiem.Text = "Quay lại vào ngày mai để nhận <b>" + Commond.Setting("txtngaythu1") + "</b>";
                            }
                            lnknhanxungay.Enabled = false;
                        }
                    }
                    else
                    {
                        if (TongNgay == 0)
                        {
                            ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu1") + "</b>";
                        }
                        else if (TongNgay == 1)
                        {
                            ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu1") + "</b>";
                        }
                        else if (TongNgay == 2)
                        {
                            ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu2") + "</b>";
                        }
                        else if (TongNgay == 3)
                        {
                            ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu3") + "</b>";
                        }
                        else if (TongNgay == 4)
                        {
                            ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu4") + "</b>";
                        }
                        else if (TongNgay == 5)
                        {
                            ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu5") + "</b>";
                        }
                        else if (TongNgay == 6)
                        {
                            ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu6") + "</b>";
                        }
                        else
                        {
                            ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu7") + "</b>";
                        }
                        lnknhanxungay.Enabled = true;
                    }


                }
                else// nếu thành viên chưa chơi bao giờ
                {
                    lnknhanxungay.Enabled = true;
                    ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu1") + "</b>";
                }
                TongDiem();
            }
            else
            {
                lthientai.Text = "0";
                ltdiem.Text = "Nhấn Để Nhận Ngay <b>" + Commond.Setting("txtngaythu1") + "</b>";
                lnknhanxungay.Enabled = true;
            }
            ltketqua.Text = showNgay();
        }
        public string showNgay()
        {
            string str = "";
            string Ngay = "";
            string active = "";

            DateTime NgayTao = new DateTime();
            DateTime NgayHienTai = DateTime.Now.Date;
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                GameTangXu abc = db.GameTangXus.SingleOrDefault(p => p.IDThanhVien == Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID")));
                if (abc != null)
                {
                    str += "<style>.active" + abc.SoNgay.ToString() + "{ color:#ffa903 !important} </style>";
                    str += "<style>.active" + abc.SoNgay.ToString() + " span{ color:#fff !important} </style>";
                    str += "<style>.active" + abc.SoNgay.ToString() + " .vongtron{margin-left: 11px !important;background: red !important;} </style>";
                    for (int i = 0; i < abc.SoNgay; i++)
                    {
                        str += "<style>.active" + i + "{ color:#ffa903 !important} </style>";
                        str += "<style>.active" + i + " span{ color:#fff !important} </style>";
                        str += "<style>.active" + i + " .vongtron{ background: red !important;} </style>";
                    }
                    active = "active" + abc.SoNgay.ToString() + "";
                    Ngay = abc.SoNgay.ToString();
                    NgayTao = Convert.ToDateTime(abc.NgayCapNhat.ToString()).Date;
                }
            }
            TimeSpan diff = NgayHienTai - NgayTao;
            str += "<div class='Ngay active1'><div class='vongtron'><span>" + Commond.Setting("txtngaythu1") + "</span></div> Ngày 1</div>";
            str += "<div class='Ngay active2'><div class='vongtron'><span>" + Commond.Setting("txtngaythu2") + "</span></div> Ngày 2</div>";
            str += "<div class='Ngay active3'><div class='vongtron'><span>" + Commond.Setting("txtngaythu3") + "</span></div> Ngày 3</div>";
            str += "<div class='Ngay active4'><div class='vongtron'><span>" + Commond.Setting("txtngaythu4") + "</span></div> Ngày 4</div>";
            str += "<div class='Ngay active5'><div class='vongtron'><span>" + Commond.Setting("txtngaythu5") + "</span></div> Ngày 5</div>";
            str += "<div class='Ngay active6'><div class='vongtron'><span>" + Commond.Setting("txtngaythu6") + "</span></div> Ngày 6</div>";
            str += "<div class='Ngay active7'><div class='vongtron'><span>" + Commond.Setting("txtngaythu7") + "</span></div> Ngày 7</div>";
            if (diff.Days == 0)
            {
                str = str.Replace("Ngày " + Ngay + "", "Hôm nay");
            }
            return str;
        }
        void TongDiem()
        {
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                List<GameTangXu> table = db.GameTangXus.Where(s => s.IDThanhVien == int.Parse(MoreAll.MoreAll.GetCookies("MembersID"))).ToList();
                if (table.Count > 0)
                {
                    Double tongdiem = Convert.ToDouble(table[0].TongSoDiem.ToString()) * 1000;
                    lthientai.Text = tongdiem.ToString();
                }
                else
                {
                    lthientai.Text = "0";
                }
            }
        }
        protected void lnknhanxungay_Click(object sender, EventArgs e)
        {

            showNgay();
            TongDiem();
            double DiemNhan = 0;
            int TongNgay = 0;
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                //if (System.Web.HttpContext.Current.Session["Gamemini"] != "Gamemini")
                {
                    List<Entity.users> table = Susers.Name_Text("select * from users where iuser_id=" + MoreAll.MoreAll.GetCookies("MembersID") + "");//  and DuyetTienDanap=1  Xếp vân yêu cầu cả thành vien Freee ( Theo giấy đề nghị)  --> Sẽ xẩy ra trường hợp sẽ hack bằng cách tạo ra nhiều nick và chuyển điểm cho nhau để mua hàng 
                    if (table.Count > 0)
                    {
                        GameTangXu abc = db.GameTangXus.SingleOrDefault(p => p.IDThanhVien == Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID")));
                        if (abc != null)
                        {
                            try
                            {
                                List<GameTangXu> aa = db.ExecuteQuery<GameTangXu>(@"select * from GameTangXu where IDThanhVien=" + MoreAll.MoreAll.GetCookies("MembersID") + " ").ToList();
                                if (aa.Count > 0)
                                {
                                    string ngayHeThong = aa[0].NgayNhanNut.ToString();
                                    string ngayHienTai = DateTime.Now.ToString("ddMMyyyy");
                                    if ((ngayHeThong == ngayHienTai))
                                    {
                                        Response.Redirect("/");
                                        return;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }

                            TongNgay = Convert.ToInt32(abc.SoNgay + 1);
                            if (TongNgay == 1)
                            {
                                double tongdiem = Convert.ToDouble(abc.TongSoDiem.ToString()) + ((Convert.ToDouble(Commond.Setting("txtngaythu1"))) / 1000);
                                abc.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                                abc.SoNgay = TongNgay;
                                abc.NgayCapNhat = DateTime.Now;
                                abc.TongSoDiem = tongdiem.ToString();
                                abc.NgayNhanNut = DateTime.Now.ToString("ddMMyyyy");
                                db.SubmitChanges();
                                DiemNhan = Convert.ToDouble(Commond.Setting("txtngaythu1")) / 1000;
                            }
                            else if (TongNgay == 2)
                            {
                                double tongdiem = Convert.ToDouble(abc.TongSoDiem.ToString()) + ((Convert.ToDouble(Commond.Setting("txtngaythu2"))) / 1000);
                                abc.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                                abc.SoNgay = TongNgay;
                                abc.NgayCapNhat = DateTime.Now;
                                abc.TongSoDiem = tongdiem.ToString();
                                abc.NgayNhanNut = DateTime.Now.ToString("ddMMyyyy");
                                db.SubmitChanges();
                                DiemNhan = Convert.ToDouble(Commond.Setting("txtngaythu2")) / 1000;
                            }
                            else if (TongNgay == 3)
                            {
                                double tongdiem = Convert.ToDouble(abc.TongSoDiem.ToString()) + ((Convert.ToDouble(Commond.Setting("txtngaythu3"))) / 1000);
                                abc.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                                abc.SoNgay = TongNgay;
                                abc.NgayCapNhat = DateTime.Now;
                                abc.TongSoDiem = tongdiem.ToString();
                                abc.NgayNhanNut = DateTime.Now.ToString("ddMMyyyy");
                                db.SubmitChanges();
                                DiemNhan = Convert.ToDouble(Commond.Setting("txtngaythu3")) / 1000;
                            }
                            else if (TongNgay == 4)
                            {
                                double tongdiem = Convert.ToDouble(abc.TongSoDiem.ToString()) + ((Convert.ToDouble(Commond.Setting("txtngaythu4"))) / 1000);
                                abc.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                                abc.SoNgay = TongNgay;
                                abc.NgayCapNhat = DateTime.Now;
                                abc.TongSoDiem = tongdiem.ToString();
                                abc.NgayNhanNut = DateTime.Now.ToString("ddMMyyyy");
                                db.SubmitChanges();
                                DiemNhan = Convert.ToDouble(Commond.Setting("txtngaythu4")) / 1000;
                            }
                            else if (TongNgay == 5)
                            {
                                double tongdiem = Convert.ToDouble(abc.TongSoDiem.ToString()) + ((Convert.ToDouble(Commond.Setting("txtngaythu5"))) / 1000);
                                abc.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                                abc.SoNgay = TongNgay;
                                abc.NgayCapNhat = DateTime.Now;
                                abc.TongSoDiem = tongdiem.ToString();
                                abc.NgayNhanNut = DateTime.Now.ToString("ddMMyyyy");
                                db.SubmitChanges();
                                DiemNhan = Convert.ToDouble(Commond.Setting("txtngaythu5")) / 1000;
                            }
                            else if (TongNgay == 6)
                            {
                                double tongdiem = Convert.ToDouble(abc.TongSoDiem.ToString()) + ((Convert.ToDouble(Commond.Setting("txtngaythu6"))) / 1000);
                                abc.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                                abc.SoNgay = TongNgay;
                                abc.NgayCapNhat = DateTime.Now;
                                abc.TongSoDiem = tongdiem.ToString();
                                abc.NgayNhanNut = DateTime.Now.ToString("ddMMyyyy");
                                db.SubmitChanges();
                                DiemNhan = Convert.ToDouble(Commond.Setting("txtngaythu6")) / 1000;
                            }
                            else
                            {
                                double tongdiem = Convert.ToDouble(abc.TongSoDiem.ToString()) + ((Convert.ToDouble(Commond.Setting("txtngaythu7"))) / 1000);
                                abc.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                                abc.SoNgay = TongNgay;
                                abc.NgayCapNhat = DateTime.Now;
                                abc.TongSoDiem = tongdiem.ToString();
                                abc.NgayNhanNut = DateTime.Now.ToString("ddMMyyyy");
                                db.SubmitChanges();
                                DiemNhan = Convert.ToDouble(Commond.Setting("txtngaythu7")) / 1000;
                            }
                        }
                        else// trường hợp thành viên chưa kích bao giờ lên trong bảng gametangxu chưa xuất hiện côt dữ liệu
                        {

                            List<GameTangXu> aa = db.ExecuteQuery<GameTangXu>(@"select * from GameTangXu where IDThanhVien=" + MoreAll.MoreAll.GetCookies("MembersID") + " ").ToList();
                            if (aa.Count > 0)
                            {
                                string ngayHeThong = aa[0].NgayNhanNut.ToString();
                                string ngayHienTai = DateTime.Now.ToString("ddMMyyyy");
                                if ((ngayHeThong != ngayHienTai))
                                {
                                    return;
                                }
                            }

                            GameTangXu obj = new GameTangXu();
                            Double Diem = (Convert.ToDouble(Commond.Setting("txtngaythu1")) / 1000);

                            obj.IDThanhVien = Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID"));
                            obj.SoNgay = 1;
                            obj.NgayCapNhat = DateTime.Now;
                            obj.TongSoDiem = Diem.ToString();
                            obj.NgayNhanNut = DateTime.Now.ToString("ddMMyyyy");
                            db.GameTangXus.InsertOnSubmit(obj);
                            db.SubmitChanges();
                            DiemNhan = Convert.ToDouble(Commond.Setting("txtngaythu1")) / 1000;
                        }
                        ThemHoaHong("0", "302", "Điểm danh nhận thưởng", MoreAll.MoreAll.GetCookies("MembersID"), MoreAll.MoreAll.GetCookies("MembersID"), "0", DiemNhan.ToString(), "0", "Điểm danh nhận thưởng");
                        ltthongbao.Text = "<script type=\"text/javascript\">alert('Chúc mừng bạn đã nhận được " + DiemNhan + " tiền thưởng vào ví mua hàng.');window.location.href='/#Diemdanh'; </script></div>";
                    }
                    else
                    {
                        ltthongbao.Text = "<script type=\"text/javascript\">alert('Vui lòng kích hoạt trở thành đại lý để được nhận tiền thưởng vào ví mua hàng.')</script></div>";
                    }
                    //System.Web.HttpContext.Current.Session["Gamemini"] = "Gamemini";
                }
            }
            else
            {
                Response.Redirect("/dang-nhap.html?ReturnUrl=/diemdanh.html");
                //ltthongbao.Text = "<script type=\"text/javascript\">alert('Vui lòng đăng nhập trước khi điểm danh nhận thưởng.');window.location.href='/dang-nhap.html?ReturnUrl=/'; </script></div>";
            }
        }

        void ThemHoaHong(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDCart, string NoiDung)
        {

            List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + IDUserNguoiDuocHuong.ToString() + "  ");//and DuyetTienDanap=1
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
                CongTien(IDType, IDUserNguoiDuocHuong, SoCoin.ToString());
                //CongTien_ViTienHHGioiThieu(IDProducts, IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, SoCoin);
            }
        }
        void CongTien(string Type, string IDUserNguoiDuocHuong, string SoCoin)
        {
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " ");
            if (iitem != null)
            {
                double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViTangTienVip);
                double TongTienNapVao = Convert.ToDouble(SoCoin);
                double Conglai = 0;
                Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                Susers.Name_Text("update users set ViTangTienVip=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
            }
        }
    }
}