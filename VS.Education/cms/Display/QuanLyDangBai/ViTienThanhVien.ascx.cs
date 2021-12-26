using Entity;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWindowService;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class ViTienThanhVien : System.Web.UI.UserControl
    {
        public int i = 1;
        protected bool Dung = false;
        private string IDSanPham = "";
        private string IDGioHang = "0";
        private string URL = "";
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            URL = Request.RawUrl.ToString();
            if (!base.IsPostBack)
            {
                try
                {
                    user table = db.users.SingleOrDefault(p => p.iuser_id == Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                    if (table != null)
                    {

                        List<Entity.users> obj = Susers.Name_Text("SELECT *  FROM users WHERE iuser_id=" + table.iuser_id + " ");
                        if (obj.Count() > 0)
                        {
                            string Mtr = "|" + obj[0].MTree.ToString();
                            if (Mtr.Contains("|" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + "|"))
                            {
                            }
                            else
                            {
                                string Cay = table.MTree + table.iuser_id + "|";
                                Susers.Name_Text("UPDATE [users] SET MTree='" + Cay + "' WHERE iuser_id =" + table.iuser_id + "");
                            }
                        }

                        if (table.TrangThaiThongBao == 1)
                        {
                            ltjavascript.Text = ("<script type=\"text/javascript\" > $.toast({ heading: 'Thông báo', text: '<p><strong>Công ty cổ phần Ag Ecom trân trọng thông báo.</strong></p> <p>Tài khoản của quý khách đã hết hạn 1 năm kích hoạt làm đại lý .<br /> Quý khách vui lòng kích hoạt lại để được nhận các quyền lợi theo chính sách của công ty.<br /> &nbsp;</p> <p>&nbsp;</p>', position: 'top-center', stack: false }) </script>");
                        }
                        if (table.DuyetTienDanap == 0)
                        {
                            pnKichHoat.Visible = true;
                        }
                        string sqls = "SELECT * from DauTuBatDongSan where  IDThanhVien=" + table.iuser_id + " order by NgayTao desc";
                        List<DauTuBatDongSan> tabledt = db.ExecuteQuery<DauTuBatDongSan>(@"" + sqls + "").ToList();
                        if (tabledt.Count > 0)
                        {
                            double tien = 0.0;
                            for (int i = 0; i < tabledt.Count; i++)
                            {
                                tien += Convert.ToDouble(tabledt[i].TongTienDauTu.ToString());
                            }
                            lttongtiendautu.Text = AllQuery.MorePro.FormatMoney_VND(tien.ToString());
                        }
                        else
                        {
                            lttongtiendautu.Text = "0 đ";
                        }
                        hdCauHinhDuyetTuDong.Value = table.CauHinhDuyetDonTuDong.ToString();
                    }

                    ShowThanhVien();
                }
                catch (Exception)
                { }
            }
        }
        public void CapNhatIDNguoiGioiThieu(string GioiThieu, string iuser_id)
        {
            try
            {
                List<Entity.users> obj = Susers.Name_Text("SELECT *  FROM users WHERE ((MTree LIKE N'%|" + GioiThieu + "|%'))  and iuser_id=" + iuser_id + " ");
                if (obj.Count() <= 0)
                {
                    List<Entity.users> obk = Susers.Name_Text("SELECT *  FROM users WHERE iuser_id=" + iuser_id + " ");
                    if (obk.Count() > 0)
                    {
                        string Cay = obk[0].MTree + GioiThieu + "|";
                        Susers.Name_Text("UPDATE [users] SET MTree='" + Cay + "' WHERE iuser_id =" + iuser_id + "");
                    }
                }
            }
            catch (Exception)
            { }
        }
        private void ShowThanhVien()
        {
            user table = db.users.SingleOrDefault(p => p.iuser_id == Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
            if (table != null)
            {
                if (table.vuserpwd.ToString() == "12345" || table.vuserpwd.ToString() == "123456" || table.vuserpwd.ToString() == "1234567" || table.vuserpwd.ToString() == "12345678")
                {
                    Response.Write("<script type=\"text/javascript\">alert('Mật khẩu của bạn quá đơn giản,Để đảm bảo cho tài khoản yêu cầu bạn vui lòng vào thay đổi mật khẩu mới.');window.location.href='/thay-doi-mat-khau.html'; </script>");
                }

                CapNhatIDNguoiGioiThieu(table.GioiThieu.ToString(), table.iuser_id.ToString());

                //if (table.iuser_id.ToString() == Commond.SetThanhVienChuyenGia())//67357
                //{
                //    Panel2.Visible = true;
                //    // ShowViHoahongChuyenGiaAgland(table.iuser_id.ToString());
                //    // ShowViHoahongChuyenGiaAFF(table.iuser_id.ToString());
                //}
                //if (table.TrangThaiThamGiaQRCode.ToString() == "1")
                //{
                //    pvViQRCode.Visible = true;
                //    this.ltViQRCode.Text = table.ViQRCode;
                //}

                ltvimuahang.Text = table.ViMuaHangAFF;

                this.ltViHoaHongMuaBan.Text = table.ViHoaHongMuaBan;
                // this.ltViHoaHongAFF.Text = table.ViHoaHongAFF;

                this.lttongvicoin.Text = table.TongTienCoinDuocCap;
                /// lthoahongvimoi.Text = table.ViTienHHGioiThieu;// ví này mới làm coppy toàn bộ hh giới thiệu vào
                lthoahonggioithieu.Text = table.VIAAFFILIATE;
                /// ltviagland.Text = table.ViAgLang;

                ltvivip.Text = table.ViTangTienVip;

                //  ltsotiendangsohuucophan
                /// ltviuutien.Text = table.ViUuTien;
                //Double bb = Convert.ToDouble(table.TienDangSoHuuBatDongSan.ToString());
                //if (bb > 0)
                //{
                //    //  Double Tong = bb * 1000;
                //    Double VTong = Math.Round(bb);
                //    ltsophancophan.Text = VTong.ToString();
                //}
                //else
                //{
                //    ltsophancophan.Text = "0";
                //}
                //double VTienDangSoHuuBatDongSan = Convert.ToDouble(table.TienDangSoHuuBatDongSan);
                //if (VTienDangSoHuuBatDongSan > 0)
                //{
                //    double VTienmotcophan = Convert.ToDouble(Commond.Setting("tienmotcophan"));
                //    double VCoPhan = 0;
                //    VCoPhan = (VTienDangSoHuuBatDongSan * VTienmotcophan);
                //    ltsotiendangsohuucophan.Text = AllQuery.MorePro.Detail_CoPhan(VCoPhan.ToString());
                //}
                //else
                //{
                //    ltsotiendangsohuucophan.Text = "0";
                //}

                //if (table.ThanhVienAgLang.ToString() == "1")
                //{
                //    pnViagland.Visible = true;
                //}
                if (table.Type.ToString() == "2") // là nhà cung cấp
                {
                    Panel1.Visible = true;
                }
                else
                {
                    Panel1.Visible = false;
                }

                //if (table.Uutien.ToString() == "1")
                //{
                //    pnviuutien.Visible = true;
                //}
                //else
                //{
                //    pnviuutien.Visible = false;
                //}

                //   ShowHoahongMuaQRCode(table.iuser_id.ToString());

                //ShowHoahongBan(table.iuser_id.ToString());
                // ShowHoahongGioiThieu(table.iuser_id.ToString());
                //ShowHoahongMua(table.iuser_id.ToString());
                // ShowChuyenDiem(table.iuser_id.ToString());
                //ShowDienmDuocCap(table.iuser_id.ToString());
                // ShowProducts(table.iuser_id.ToString());
                ShowViTamGiuBan(table.iuser_id.ToString());
                ShowViTamGiuMua(table.iuser_id.ToString());

                // ShowBanhang(table.iuser_id.ToString());
                // ShowTongRut(table.iuser_id.ToString());
                LoadListVideo();
            }
        }
        
        private void ShowViHoahongChuyenGiaAFF(string IDThanhVien)
        {
            var dt = db.S_HoaHongChuyeGia_ThongKe(Convert.ToInt32(IDThanhVien), 1).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    ltchuyengiaAFF.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    ltchuyengiaAFF.Text = "0";
                }
            }
            else
            {
                ltchuyengiaAFF.Text = "0";
            }
        }
        private void ShowViHoahongChuyenGiaAgland(string IDThanhVien)
        {
            var dt = db.S_HoaHongChuyeGia_ThongKe(Convert.ToInt32(IDThanhVien), 2).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    ltchuyengiaAgland.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    ltchuyengiaAgland.Text = "0";
                }
            }
            else
            {
                ltchuyengiaAgland.Text = "0";
            }
        }
        protected void LoadListVideo()
        {
            List<Entity.Advertisings> list = SAdvertisings.VALUES(MoreAll.MoreAll.Language, "8", "1");
            if (list.Count > 0)
            {
                string kq = "";
                for (int i = 0; i < list.Count(); i++)
                {
                    var urlVideo = list[i].Youtube;
                    kq += " <div class=\"col-xs-12 col-md-6 col-sm-6\">";
                    kq += " <div class=\"artitle-item \">";
                    kq += "<div class=\"video-container\">";
                    kq += "  <iframe width=\"100%\" class=\"vdyoutube\" src=\"https://www.youtube.com/embed/" + urlVideo + "\" frameborder=\"0\" allowfullscreen=\"\"></iframe>";
                    kq += "</div>";
                    kq += "<div class=\"article-info-box\">";
                    kq += "<a title=\"" + list[i].Name + "\" class=\"title\">";
                    kq += " <h2>" + list[i].Name + "</h2>";
                    kq += " </a>";
                    kq += " <div class=\"description\">" + list[i].Contents + "</div>";

                    kq += " </div>";
                    kq += " </div>";
                    kq += " </div>";

                }
                ltrListVideo.Text = kq;
            }
        }
        void TraLaiTienChoNguoiMua(string IDThanhVien)
        {
            string str = "";
            // Nếu đơn nào mà chưa chấp nhận thì sẽ bị trả lại sau 3 ngày
            List<ViTamMuaHang> List = db.ViTamMuaHangs.Where(s => s.IDThanhVienMua == int.Parse(IDThanhVien) && s.NCCDuyet == 1).ToList();
            if (List.Count > 0)
            {
                foreach (var item in List)
                {
                    DateTime NgayDuyetHang = Convert.ToDateTime(item.NgayCapNhat.ToString());
                    DateTime NgayXuLy = NgayDuyetHang.AddDays((double)Convert.ToInt32(MoreAll.Other.Giatri("TuDongDuyetDonHang")));// ăn theo cấu hình chung tự động duyệt theo bao nhiêu ngày

                    DateTime NgayHienTai = DateTime.Now;
                    if ((NgayHienTai >= NgayXuLy))
                    {
                        List<Entity.users> iiit = Susers.GET_BY_ID(item.IDThanhVienMua.ToString());
                        if (iiit.Count > 0)
                        {

                            double ViHienTaiCoin = Convert.ToDouble(iiit[0].TongTienCoinDuocCap);
                            double VIAFF = Convert.ToDouble(iiit[0].ViMuaHangAFF);

                            double ChietKhauVip = Convert.ToDouble(item.ChietKhauVip.ToString());
                            double ViTangTienVip = Convert.ToDouble(iiit[0].ViTangTienVip);
                            double TSoTienNguoiMuaBiTru = Convert.ToDouble(item.SoTienNguoiMuaBiTru.ToString());
                            if (item.LayTienOVi == 0)// trả cho ví AFF
                            {
                                double Conglai = 0;
                                Conglai = ((VIAFF) + (TSoTienNguoiMuaBiTru));
                                Susers.Name_Text("update users set ViMuaHangAFF=" + Conglai.ToString() + "  where iuser_id=" + item.IDThanhVienMua.ToString() + "");
                            }
                            else if (item.LayTienOVi == 1)// trả cho ví Thương mại
                            {
                                double Conglai = 0;
                                Conglai = ((ViHienTaiCoin) + (TSoTienNguoiMuaBiTru));
                                Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + item.IDThanhVienMua.ToString() + "");
                            }

                            double CongVip = ((ViTangTienVip) + (ChietKhauVip));
                            // Trả lại tiền vào THƯỞNG MUA HÀNG
                            Susers.Name_Text("update users set ViTangTienVip=" + CongVip.ToString() + "  where iuser_id=" + item.IDThanhVienMua.ToString() + "");
                        }

                        // Xóa tiền ở bảng tạm
                        ViTamMuaHang delv = db.ViTamMuaHangs.Where(s => s.IDCartDetail == int.Parse(item.IDCartDetail.ToString()) && s.NCCDuyet == 1).FirstOrDefault();// xóa 1
                        if (delv != null)
                        {
                            db.ViTamMuaHangs.DeleteOnSubmit(delv);
                            db.SubmitChanges();
                        }

                        #region Gửi mail cho nhà cung cấp khi thành viên mua hủy sản phẩm.
                        List<CartDetail> ds = db.CartDetails.OrderByDescending(s => s.ID == Convert.ToInt32(item.IDCartDetail.ToString())).ToList();
                        if (ds.Count > 0)
                        {
                            SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=2,[TrangThaiNhaCungCap]=2,LyDoHuyHang=N'Hệ thống tự động hủy đơn do nhà cung cấp không phản hồi sớm.',LyDoTraHang=N'Hệ thống tự động hủy đơn do nhà cung cấp không phản hồi sớm.',TienTuViNao=0 WHERE ID =" + ds[0].ID.ToString() + "");

                            List<Entity.users> dc = Susers.GET_BY_ID(ds[0].IDThanhVien.ToString());
                            if (dc.Count > 0)
                            {
                                string Emails = dc[0].vemail.ToString();

                                //Gửi email
                                string Noidung = "";
                                Noidung += "<b>Chúng tôi rất xin lỗi! </b><br /> Đơn hàng <a href=\"http://lienhiephoptac.vn/account/orders/" + ds[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + ds[0].ID_Cart.ToString() + "</b></a> của bạn đã bị hủy do nhà cung cấp không thể phản hồi sớm cho bạn. Chúng tôi đã hoàn lại điểm cho thành viên <b>" + dc[0].vfname.ToString() + " </b>. ";
                                Noidung += "Quý khách vui lòng kiểm tra lại điểm của mình.";
                                Noidung += "<br />";
                                Noidung += "<br />";
                                Noidung += "<b>Tên sản phẩm :  </b>" + Commond.ShowPro(ds[0].ipid.ToString()) + "<br />";
                                Noidung += "<b>Số lượng sản phẩm :  </b>" + ds[0].Quantity.ToString() + "<br />";
                                Noidung += "<b>Tổng số tiền :  </b>" + ds[0].Money.ToString() + "<br />";
                                Noidung += "---------------------------------";
                                Noidung += "<br />";

                                Noidung += "<b>Người mua hàng: </b>" + dc[0].vfname.ToString() + "<br />";
                                Noidung += "<b>Địa chỉ: </b>" + dc[0].vaddress.ToString() + "<br />";
                                Noidung += "<b>Điện thoại: </b>" + dc[0].vphone.ToString() + "<br />";
                                Noidung += "<b>Email: </b>" + dc[0].vemail.ToString() + "<br />";

                                Noidung += "<br />";
                                Noidung += "<br />";
                                Noidung += Commond.Setting("txtfooterEmail");
                                Noidung += "<br />";

                                try
                                {
                                    new MailHelper().SendMail(ShowEmailNhaCungCap(ds[0].IDNhaCungCap.ToString()), "Hủy đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + ds[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                    new MailHelper().SendMail(dc[0].vemail.ToString(), "Hủy đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + ds[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                }
                                catch { }
                            }
                        }
                        #endregion
                    }
                }
            }
        }
        public string ShowEmailNhaCungCap(string id)
        {
            string code = "0";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count > 0)
            {
                code = dt[0].vemail.ToString();
            }
            return code;
        }
        private void ShowViTamGiuBan(string IDThanhVien)
        {
            var dt = db.S_ViTamMuaHang_ThongKe(Convert.ToInt32(IDThanhVien)).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    ltvitamgiu.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    ltvitamgiu.Text = "0";
                }
            }
            else
            {
                ltvitamgiu.Text = "0";
            }
        }
        private void ShowViTamGiuMua(string IDThanhVien)
        {
            var dt = db.S_ViTamMuaHang_MUA_ThongKe(Convert.ToInt32(IDThanhVien)).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    ltvitamgiumuahang.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    ltvitamgiumuahang.Text = "0";
                }
            }
            else
            {
                ltvitamgiumuahang.Text = "0";
            }
        }
        
        public string ShowTrangThai(string enable)
        {
            if (enable.Trim().Equals("0"))
            {
                return "Đơn hàng chưa duyệt";
            }
            else if (enable.Trim().Equals("1"))
            {
                return "Đơn hàng đã duyệt";
            }
            else if (enable.Trim().Equals("2"))
            {
                return "Đơn hàng đang chờ xử lý";
            }
            else if (enable.Trim().Equals("3"))
            {
                return "Đơn hàng đang vận chuyển";
            }
            return "";
        }

        #region Các công thức tính kèm

        void CongTien(string Type, string IDUserNguoiDuocHuong, string SoCoin)
        {
            #region Cộng điểm  theo hoa hồng coin vào bảng thành viên để tích điểm trong các trường hợp mua hàng cần
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + "");
            if (iitem != null)
            {
                if (Type == "30" || Type == "401")// 30 la thanh toan truc tiếp thì cho vào ví thương mại luôn. ngoài
                {
                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                    double TongTienNapVao = Convert.ToDouble(SoCoin);
                    double Conglai = 0;
                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }
                else if (Type == "300")// 300 là thành viên Free tặng điểm là về ví mua hàng
                {
                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                    double TongTienNapVao = Convert.ToDouble(SoCoin);
                    double Conglai = 0;
                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }
                else if (Type != "301")// 301 đổ về ví công ty AG, chỉ đổ về lịch sử giao dịch rồi show tổng lên thôi, chứ ko đổ về ví nào cả
                {
                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViHoaHongMuaBan);
                    double TongTienNapVao = Convert.ToDouble(SoCoin);
                    double Conglai = 0;
                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));

                    Susers.Name_Text("update users set ViHoaHongMuaBan=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }
            }
            #endregion
        }

        protected string CapoLevelID(string level)
        {
            List<Entity.Menu> cdd = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.LV + "'  and Views=" + level + " and lang='VIE' order by level,Orders asc");
            if (cdd != null)
            {
                return cdd[0].ID.ToString();// ID chính là thuộc cấp độ mấy do tiêu đề ghi
            }
            return "0";
        }
        protected string CapoLevelHoaHong(string level)
        {
            List<Entity.Menu> cdd = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.LV + "'  and Views=" + level + " and lang='VIE' order by level,Orders asc");
            if (cdd != null)
            {
                return cdd[0].Noidung1.ToString();// ID chính là thuộc cấp độ mấy do tiêu đề ghi
            }
            return "0";
        }
        #region Cách tính cho gián tiếp --> cứ - được mà ko âm thì cho lấy ra số điểm để thưởng cho b gián tiếp
        //TinhDiemthuongGiantiep("2","5") // lv2 - lv5 = 6
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
        public string SetLevel(string id)
        {
            List<Entity.Menu> dt = SMenu.Name_Text("select * from Menu where capp='" + More.LV + "' and Views=" + id + " ");
            if (dt.Count > 0)
            {
                return dt[0].Noidung1.ToString();
            }
            return "0";
        }
        #endregion

        protected string TimThanhVienGioiThieu(string id)
        {
            string str = "0";
            List<Entity.users> dt = Susers.Name_Text("select * from users where iuser_id=" + id + "");
            if (dt.Count >= 1)
            {
                str = dt[0].ThanhVienAgLang.ToString();
            }
            return str;
        }

        #region ChiNhanh
        protected string ShowIDChiNhanh(string id)
        {
            string str = "0";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count >= 1)
            {
                str = dt[0].Type.ToString();// chính là ID của thành viên nào đang quản lý (Bảng thành viên)
            }
            return str;
        }
        protected string ShowChiNhanh(string id)
        {
            string str = "0";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count >= 1)
            {
                if (dt[0].Name.ToString().Length > 0)
                {
                    str += dt[0].Name;
                }
            }
            return str;
        }
        #endregion

        #region Tìm nhà bán cung cấp hàng từ sản phẩm đang bán
        protected string ShowNhaCungCap(string id)
        {
            string str = "0";
            List<Entity.Products> dt = SProducts.Name_Text("select * from products  where ipid=" + id + "");
            if (dt.Count >= 1)
            {
                str = dt[0].IDThanhVien.ToString();
            }
            return str;
        }
        #endregion
        protected string HoaHongTheoLevel_TheoThoiDiemMuahang_News(string CapoLevelHoaHongs, string Tongd)
        {
            double TongCoin = Convert.ToDouble(Tongd);
            double HoaHongs = Convert.ToDouble("30");//CapoLevelHoaHongs
            double Tong = (TongCoin * HoaHongs) / 100;
            return Tong.ToString();
        }
        protected string HoaHongTheoLevel_TheoThoiDiemMuahang(string IDCart, string ID, string Tongd, string IDThanhVien)
        {
            #region Show % Hoa Hồng theo thời điểm mua hàng (Lịch sử)
            //Xem thành viên đang ở level nào rồi nhân với level đó
            List<Carts> table2 = SCarts.Carts_GetById(IDCart.ToString());
            if (table2.Count > 0)
            {
                // Nếu đã đặt hàng thành công xong rồi trạng thái status =1 thì sẽ phải lấy HoaHongTheoLevel trong bảng cartdetail để làm căn cứ lịch sử lúc mua tại thời điểm đó chỉ dc bằng đó %
                if (table2[0].Status == 1)
                {
                    List<CartDetail> table = db.CartDetails.Where(p => p.ID == int.Parse(ID.ToString())).ToList();
                    if (table != null)
                    {
                        string CapoLevelHoaHongs = (table[0].HoaHongTheoLevel.ToString());
                        double TongCoin = Convert.ToDouble(Tongd);

                        double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                        double Tong = (TongCoin * HoaHongs) / 100;
                        return Tong.ToString();
                    }
                }
                else
                {
                    // Nếu chưa đặt hàng thì trọc vào bảng thành viên lấy hoa hồng hiện tại thành viên đang ở level mấy  ra nhé
                    user tables = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien.ToString()));// Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                    if (tables != null)
                    {
                        string CapoLevelHoaHongs = CapoLevelHoaHong(tables.LevelThanhVien.ToString());
                        double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                        double TongCoin = Convert.ToDouble(Tongd);
                        double Tong = (TongCoin * HoaHongs) / 100;
                        return Tong.ToString();
                    }
                }
            }
            #endregion
            return "";
        }
        protected string FormatMoneyDiemMuaHang(string Money)
        {
            try
            {
                double TongCoin = Convert.ToDouble(Money.ToString());
                double Tong = (TongCoin) / 1000;
                return Tong.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string ShowThanhVien(string IDThanhVien)
        {
            if (IDThanhVien != "0")
            {
                try
                {
                    user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien));
                    if (table != null)
                    {
                        return "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + table.iuser_id.ToString() + "\"><span style='color:red'>" + table.vfname + "</span></a>";
                    }
                }
                catch (Exception)
                { }
            }
            return "Admin";
        }

        public string ShowGiacongtynhapvao(string id, string quantity)
        {
            product table = db.products.SingleOrDefault(p => p.ipid == int.Parse(id));
            if (table != null)
            {
                Double Tongtien = Convert.ToInt32(quantity) * Convert.ToDouble(table.Giacongtynhapvao.ToString());
                return Tongtien.ToString();
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
        public static string TimLeader(string id)
        {
            string str = "0";
            List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + " ");
            if (dt.Count > 0)
            {
                if (dt[0].Leader.ToString() == "1")
                {
                    return dt[0].iuser_id.ToString();
                }
                else
                {
                    str = dt[0].GioiThieu.ToString();
                    return TimLeader(str);
                }
            }
            return str;
        }
        #endregion

        #region Tìm ra người giới thiệu gần nhất để cho Level phần Ag Land
        protected string ShowF2(string IDF1, string IDF2)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " and ThanhVienAgLang=1");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  and ThanhVienAgLang=1");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF3(string IDF1, string IDF2, string IDF3)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  and ThanhVienAgLang=1");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  and ThanhVienAgLang=1");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  and ThanhVienAgLang=1");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF4(string IDF1, string IDF2, string IDF3, string IDF4)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  and ThanhVienAgLang=1");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  and ThanhVienAgLang=1");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  and ThanhVienAgLang=1");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf4 = Susers.Name_Text("select * from users  where iuser_id=" + IDF4 + "  and ThanhVienAgLang=1");
            if (dtf4.Count > 0)
            {
                return dtf4[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF5(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  and ThanhVienAgLang=1");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  and ThanhVienAgLang=1");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  and ThanhVienAgLang=1");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  and ThanhVienAgLang=1");
            if (dtf5.Count > 0)
            {
                return dtf5[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        #endregion
        protected void btchuyensangvithuongmai_Click(object sender, EventArgs e)
        {
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                user table = db.users.SingleOrDefault(p => p.iuser_id == Convert.ToInt32(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                if (table != null)
                {
                    double TongTienCoinDuocCap = Convert.ToDouble(table.TongTienCoinDuocCap);
                    double ViMuaHangAFF = Convert.ToDouble(table.ViMuaHangAFF);

                    double ViHoaHongMuaBan = Convert.ToDouble(table.ViHoaHongMuaBan);
                    double Thue = Convert.ToDouble(Commond.Setting("Thue"));

                    if (ViHoaHongMuaBan > 0)
                    {
                        double TinhThe = (ViHoaHongMuaBan * Thue) / 100;
                        double TruThue = (ViHoaHongMuaBan - TinhThe);

                        double ViTM = (TruThue * 90) / 100;// cho 90% vào ví Thương mại / theo chính sách ngày 01/01/2001
                        double VIMuaHang = (TruThue * 10) / 100;// cho 10% vào ví mua hàng / theo chính sách ngày 01/01/2001

                        double ViTMS = ((ViTM) + (TongTienCoinDuocCap));
                        double VIMuaHangS = ((VIMuaHang) + (ViMuaHangAFF));


                        // lưu lịch sử ChuyenDiemSangVi_Thue
                        ChuyenDiemSangVi_Thue obj = new ChuyenDiemSangVi_Thue();
                        obj.IDThanhVien = int.Parse(table.iuser_id.ToString());
                        obj.MTree = table.MTree.ToString();
                        obj.SoDiemViHoaHong = ViHoaHongMuaBan.ToString();// số điểm ở ví hoa hồng trước khi cộng sang ví chính và trước khi bị trừ thuế
                        obj.PhanTramThue = int.Parse(Thue.ToString());// thuế 10%
                        obj.SoDiemSauKhiTru = TruThue.ToString();// số điểm sau khi bị trừ thuế
                        obj.SoDienBiTru = TinhThe.ToString();// số điểm sẽ bị trừ
                        obj.SoDiemViChinhTruocKhiCongSang = TongTienCoinDuocCap.ToString();// Số điểm ở ví chính trước khi cộng điểm sang
                        obj.NgayGiaoDich = DateTime.Now;
                        obj.LoaiVi = 1;//0 là Ví quản lý , 1 là ví thương mại
                        obj.ViMuaHang = VIMuaHang.ToString();
                        obj.ViThuongMai = ViTM.ToString();
                        db.ChuyenDiemSangVi_Thues.InsertOnSubmit(obj);
                        db.SubmitChanges();

                        Susers.Name_Text("UPDATE [users] SET ViMuaHangAFF='" + VIMuaHangS + "',ViHoaHongMuaBan=0 WHERE iuser_id =" + table.iuser_id + "");
                        Susers.Name_Text("UPDATE [users] SET TongTienCoinDuocCap='" + ViTMS + "',ViHoaHongMuaBan=0 WHERE iuser_id =" + table.iuser_id + "");

                        ltthongbao.Text = "<script type=\"text/javascript\">alert('Chuyển điểm thành công.');window.location.href='" + URL + "'; </script>";
                    }
                    else
                    {
                        ltthongbao.Text = "<script type=\"text/javascript\">alert('Số điểm không đủ để chuyển sang ví thương mại.');window.location.href='" + URL + "'; </script>";
                    }
                }
            }
        }
    }
}