using Entity;
using Framework;
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

namespace VS.E_Commerce
{
    public partial class Videmo : System.Web.UI.Page
    {
        public int i = 1;
        protected bool Dung = false;
        private string IDSanPham = "";
        private string IDGioHang = "0";
        private string URL = "";
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        string U1 = "";
        string U2 = "";
        string ID = "";
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
            if (Request["U1"] != null && !Request["U1"].Equals(""))
            {
                U1 = Request["U1"];
            }
            if (Request["U2"] != null && !Request["U2"].Equals(""))
            {
                U2 = Request["U2"];
            }
            if (Request["ID"] != null && !Request["ID"].Equals(""))
            {
                ID = Request["ID"];
                Fusers item = new Fusers();
                List<Entity.users> table = Susers.Name_Text("select * from users where vuserun='" + U1.Trim().ToLower() + "' and iuser_id=" + (ID.Trim().ToLower()) + " ");// and DuyetTienDanap=1 phải nạp tiền xong mới cho đăng nhập
                if (table.Count > 0)
                {
                    #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
                    Commond.CheckNgayHetHan(table[0].iuser_id.ToString());
                    #endregion

                    MoreAll.MoreAll.SetCookie("Members", U1, 5000);
                    MoreAll.MoreAll.SetCookie("MembersID", table[0].iuser_id.ToString(), 5000);
                    Response.Redirect("/Videmo.aspx");
                }
            }

            ShowThanhVien();
        }
        private void ShowThanhVien()
        {
            user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
            if (table != null)
            {
                ShowViAgLand(table.iuser_id.ToString());
                double LaiAGLAND = Convert.ToDouble(ltviagland2.Text);

                Double LaiLand = 0;
                Double TonglaiCanTru = 0;
                List<Entity.EService_LaiSuatAGLANG> tabl1 = SService_LaiSuatAGLANG.Name_Text("select *  from Service_LaiSuatAGLANG where IDThanhVienHuongHH=" + table.iuser_id.ToString() + " and  NgayThamGia  <='08/01/2020'  ");
                if (tabl1.Count > 0)
                {
                    ltviaglandngay.Text = tabl1[0].LaiSuat.ToString();
                    ltsogoi.Text = tabl1.Count.ToString();

                    // phần này chỉ tính land đối với 1 gói land, còn 2 gói land thì tự tính tay.
                    DateTime NgayBatDau = Convert.ToDateTime("01/08/2020");
                    DateTime NgayHienTai = Convert.ToDateTime(DateTime.Now);
                    TimeSpan Time = NgayHienTai - NgayBatDau;
                    int TongSoNgay = Time.Days;

                    Double TongSoNgays = Convert.ToDouble(TongSoNgay + 1);
                    Double Lai1ngay = Convert.ToDouble(ltviaglandngay.Text);
                    TonglaiCanTru = Lai1ngay * TongSoNgays;
                    Response.Write("Lãi 1 ngày: " + Lai1ngay + " * " + TongSoNgays + "=" + TonglaiCanTru + "<br>");

                    if (TonglaiCanTru > 0)
                    {
                        LaiLand = LaiAGLAND - TonglaiCanTru;
                    }
                    Response.Write("Tổng số lãi: " + LaiLand + "<br>");
                    ltviagland.Text = LaiLand.ToString();
                    // hdlailand.Value = LaiLand.ToString();
                }
                else
                {
                    ltsogoi.Text = "0";
                    ltviaglandngay.Text = "0";
                    ltviagland.Text = "0";
                    //hdlailand.Value = "0";
                }

                if (table.iuser_id.ToString() == SetVi.SetThanhVienChuyenGia())//67357
                {
                    Panel2.Visible = true;
                    // ShowViHoahongChuyenGiaAgland(table.iuser_id.ToString());
                    // ShowViHoahongChuyenGiaAFF(table.iuser_id.ToString());
                }
                Literal1.Text = table.vfname.ToString() + " - " + table.vuserun.ToString() + " - " + table.iuser_id.ToString();
                if (table.TrangThaiThamGiaQRCode.ToString() == "1")
                {
                    pvViQRCode.Visible = true;
                    this.ltViQRCode.Text = table.ViQRCode;
                }
                hdCauHinhDuyetTuDong.Value = table.CauHinhDuyetDonTuDong.ToString();

                ltvimuahang.Text = table.ViMuaHangAFF;

                this.ltViHoaHongMuaBan.Text = table.ViHoaHongMuaBan;
                this.ltViHoaHongAFF.Text = table.ViHoaHongAFF;

                this.lttongvicoin.Text = table.TongTienCoinDuocCap;
                lthoahongvimoi.Text = table.ViTienHHGioiThieu;// ví này mới làm coppy toàn bộ hh giới thiệu vào
                lthoahonggioithieu.Text = table.VIAAFFILIATE;
                ltvivip.Text = table.ViTangTienVip;


                //  ltviagland.Text = table.ViAgLang;
                //  ltsotiendangsohuucophan
                ltviuutien.Text = table.ViUuTien;
                Double bb = Convert.ToDouble(table.TienDangSoHuuBatDongSan.ToString());
                if (bb > 0)
                {
                    //Double Tong = bb * 1000;
                    Double VTong = Math.Round(bb);
                    ltsophancophan.Text = VTong.ToString();
                }
                else
                {
                    ltsophancophan.Text = "0";
                }

                double VTienDangSoHuuBatDongSan = Convert.ToDouble(table.TienDangSoHuuBatDongSan);
                if (VTienDangSoHuuBatDongSan > 0)
                {
                    double VTienmotcophan = Convert.ToDouble(Commond.Setting("tienmotcophan"));
                    double VCoPhan = 0;
                    VCoPhan = (VTienDangSoHuuBatDongSan * VTienmotcophan);
                    ltsotiendangsohuucophan.Text = AllQuery.MorePro.Detail_CoPhan(VCoPhan.ToString());
                }
                else
                {
                    ltsotiendangsohuucophan.Text = "0";
                }

                if (table.ThanhVienAgLang.ToString() == "1")
                {
                    pnViagland.Visible = true;
                }
                if (table.Type.ToString() == "2") // là nhà cung cấp
                {
                    Panel1.Visible = true;
                }
                else
                {
                    Panel1.Visible = false;
                }

                if (table.Uutien.ToString() == "1")
                {
                    pnviuutien.Visible = true;
                }
                else
                {
                    pnviuutien.Visible = false;
                }

                ShowHoahongMuaQRCode(table.iuser_id.ToString());
                ShowHoahongBan(table.iuser_id.ToString());
                ShowHoahongMua(table.iuser_id.ToString());
                ShowChuyenDiem(table.iuser_id.ToString());
                ShowDienmDuocCap(table.iuser_id.ToString());
                // ShowProducts(table.iuser_id.ToString());
                ShowViTamGiuBan(table.iuser_id.ToString());
                ShowViTamGiuMua(table.iuser_id.ToString());

                ShowBanhang(table.iuser_id.ToString());
                ShowTongRut(table.iuser_id.ToString());
                ShowThue(table.iuser_id.ToString());
                ShowHoahongGioiThieu(table.iuser_id.ToString());
                ShowThueAFF(table.iuser_id.ToString());
                ShowMuaDiem(table.iuser_id.ToString());
                ShowDiemChietKhauViVipDaMua(table.iuser_id.ToString());
                ShowDiemChietKhauViVipDaMuaTamGiu(table.iuser_id.ToString());

                #region Cộng
                //+
                // Lưu ý trong ví cấp điểm đang thể hiện dc cấp cho 2 ví nhé.
                double TongCap = Convert.ToDouble(ltdiemduoccap.Text);
                double HHMB = Convert.ToDouble(lthoahongmua.Text);
                double HHNCC = Convert.ToDouble(lthoahongban.Text);
                double HHAFF = Convert.ToDouble(lthhviquanly.Text);
                // PHẢI CỘNG HOA HỒNG AFF VÀO VÌ KHI MUA HÀNG LẤY TỪ VÍ NÀY.
                // LÊN TỔNG TIỀN MUA HÀNG NÓ TO LÊN, KO PHẢI LẤY TIWF VÍ THƯƠNG MẠI. ÂM Ở CHỖ NÀY VÌ KO CỘNG HHAFF VÀO
                double VIMUAHANG = Convert.ToDouble(ltvimuahang.Text);
                double MuaDiem = Convert.ToDouble(ltmuadiem.Text);


                double HHQL = Convert.ToDouble(ltdiemduoccap.Text);
                double ThuAFF = Convert.ToDouble(ltthueAFF.Text);
                double THUETM = Convert.ToDouble(ltthue.Text);
                double TTTVMH = HHAFF - (VIMUAHANG + ThuAFF);

                double lailand = Convert.ToDouble(hdlailand.Value);

                // phải trừ tiền mua hàng từ 1/7 đi vì lấy tiền ở ví mua hàng đi mua hàng lên tổng tiền mua hàng cao hơn bình thường
                // phải vào lich sử chuển điểm lọc xem đã chuyern bao nhiêu tiền sang ví mua hàng để đi mua hàng.
                /////////////////////////double TTTCONGLAI = (TongCap + HHMB + HHNCC + TTTVMH);// + TTTVMH

                double TTTCONGLAI = (TongCap + HHMB + HHNCC + HHAFF + MuaDiem + lailand);// +  + LaiLand +
                TongVao.Text = TTTCONGLAI.ToString();


                /// double TTTCONGLAI = (TongCap + HHMB + HHNCC + LaiAGLAND + THUETM);// + TTTVMH
                //Response.Write(TTTVMH.ToString());
                #endregion

                #region TRừ
                //-
                double DACHUYEN = Convert.ToDouble(ltdiemdachuyen.Text);
                double MUAHHANG = Convert.ToDouble(lttongtienmuahang.Text);
                // ví mua hàng đang có vấn đề, vì trong ví mua hàng có từ hoa hồng quản lý,
                //sau khi có tiền thì họ đi mua, thì trong ví tổng tiền mua hàng sẽ bị cộng to lên mà ko lấy từ ví thương mại lên sẽ ko thống kê  sẽ bị âm ở chỗ này.

                double KICHHOAT = 0;
                if (table.DuyetTienDanap.ToString() == "1")
                {
                    KICHHOAT = Convert.ToDouble(hd480.Value);
                }
                double RUTDIEM = Convert.ToDouble(lttongdarut.Text);
                double TAMGIUMUAHANG = Convert.ToDouble(ltvitamgiumuahang.Text);

                double ViVipDaMuaHang = Convert.ToDouble(ltvivipmuahang.Text);
                double VipMuahangTamGiu = Convert.ToDouble(ltvipmuahangtamgiu.Text);


                double Quet = 0;
                List<TruocKhiSetViAFF> cks = db.TruocKhiSetViAFFs.Where(s => s.IDThanhVien == int.Parse(table.iuser_id.ToString())).ToList();
                if (cks.Count > 0)
                {
                    Quet = Convert.ToDouble(cks[0].ViQuanLy.ToString());
                    ltquyet.Text = cks[0].ViQuanLy.ToString();
                }
                else
                {
                    ltquyet.Text = "0";
                }

                //double TTTCONGLAI2 = (DACHUYEN + MUAHHANG + RUTDIEM + TAMGIUMUAHANG + KICHHOAT);
                double TTTCONGLAI2 = (DACHUYEN + MUAHHANG + RUTDIEM + TAMGIUMUAHANG + KICHHOAT + THUETM + ThuAFF + Quet + ViVipDaMuaHang + VipMuahangTamGiu);

                TongRa.Text = TTTCONGLAI2.ToString();


                double TTCCC = (TTTCONGLAI - TTTCONGLAI2);
                lttongcong.Text = TTCCC.ToString();
                #endregion

                #region Cộng ví thương mại và ví AFF
                // vì lúc cấp điêm có phân biệt là cấp cho ví nào đâu, lên tổng điểm đc cấp phải bao gồm 2 ví.
                // Cộng ví thương mại và ví AFF
                // thuế phải cộng vào ví thương mại để thì mưới đúng, vì bên ví HHMB và HHNCC đó là lịch sử chưa trừ thuế.

                double VIP = Convert.ToDouble(ltvivip.Text);

                double TM = Convert.ToDouble(lttongvicoin.Text);
                double QL = Convert.ToDouble(lthoahonggioithieu.Text);

                double ViHoaHongMuaBan = Convert.ToDouble(ltViHoaHongMuaBan.Text);
                double ViHoaHongAFF = Convert.ToDouble(ltViHoaHongAFF.Text);

                double TMQL = (TM + QL + VIMUAHANG);// +HHAFF;// -VIMUAHANG;
                lttmql.Text = TMQL.ToString();

                double TON = (TM + VIP + QL + VIMUAHANG + ViHoaHongMuaBan + ViHoaHongAFF);
                Tonthucte.Text = TON.ToString();

                Double Lech = (Convert.ToDouble(Math.Round(TTTCONGLAI, 0)) - Convert.ToDouble(Math.Round(TTTCONGLAI2, 0)) - Convert.ToDouble(Math.Round(TON, 0)));
                ltlech.Text = Lech.ToString();

                #endregion
            }
        }




        private void ShowMuaDiem(string IDThanhVien)
        {
            string sqls = "SELECT * from MuaDiemThanhVien where   IDThanhVien=" + IDThanhVien + " and TrangThai=1  order by NgayGui desc";
            List<MuaDiemThanhVien> table = db.ExecuteQuery<MuaDiemThanhVien>(@"" + sqls + "").ToList();
            if (table.Count > 0)
            {
                double coin = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    coin += Convert.ToDouble(table[i].SoDiemCanMua.ToString());
                }
                ltmuadiem.Text = coin.ToString();
            }
            else
            {

                ltmuadiem.Text = "0";
            }
        }
        private void ShowViAgLand(string IDThanhVien)
        {
            string sql = "";
            List<Entity.ELaiSuatAGLANG> table = SLaiSuatAGLANG.Name_Text("select *  from LaiSuatAGLANG where IDThanhVienHuongHH=" + IDThanhVien + "  ");
            if (table.Count > 0)
            {
                double coin = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    coin += Convert.ToDouble(table[i].LaiSuat.ToString());
                }
                ltviagland2.Text = coin.ToString();
            }
            else
            {

                ltviagland2.Text = "0";
            }
        }
        private void ShowThue(string IDThanhVien)
        {
            string sql = "";
            List<Entity.EChuyenDiemSangVi_Thue> table = SChuyenDiemSangVi_Thue.Name_Text("select *  from ChuyenDiemSangVi_Thue where IDThanhVien=" + IDThanhVien + " and LoaiVi=1 ");
            if (table.Count > 0)
            {
                double coin = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    coin += Convert.ToDouble(table[i].SoDienBiTru.ToString());
                }
                ltthue.Text = coin.ToString();
            }
            else
            {
                ltthue.Text = "0";
            }
        }

        private void ShowThueAFF(string IDThanhVien)
        {
            string sql = "";
            List<Entity.EChuyenDiemSangVi_Thue> table = SChuyenDiemSangVi_Thue.Name_Text("select *  from ChuyenDiemSangVi_Thue where IDThanhVien=" + IDThanhVien + " and LoaiVi=0 ");
            if (table.Count > 0)
            {
                double coin = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    coin += Convert.ToDouble(table[i].SoDienBiTru.ToString());
                }
                ltthueAFF.Text = coin.ToString();
            }
            else
            {
                ltthueAFF.Text = "0";
            }
        }


        private void ShowTongRut(string IDThanhVien)
        {
            string sql = "";
            List<Entity.ELichSuRutTien> table = SLichSuRutTien.Name_Text("select *  from LichSuRutTien where IDThanhVien=" + IDThanhVien + " and TrangThai!=2");
            if (table.Count > 0)
            {
                double coin = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    coin += Convert.ToDouble(table[i].SoTienCanRut.ToString());
                }
                lttongdarut.Text = coin.ToString();
            }
            else
            {
                lttongdarut.Text = "0";
            }

        }

        private void ShowBanhang(string IDThanhVien)
        {
            string sql = "";
            List<CartDetail> table = db.CartDetails.Where(p => p.IDThanhVien == int.Parse(IDThanhVien) && p.TrangThaiKhieuKien.ToString() == "0" && p.TrangThaiNhaCungCap.ToString() == "1" && p.TrangThaiNguoiMuaHang.ToString() == "1").ToList();
            if (table.Count > 0)
            {
                Double TienCu = 0;
                Double TienMoiNhat = 0;
                for (int i = 0; i < table.Count; i++)
                {
                    if (table[i].TongTienThanhToan.Equals(""))
                    {
                        TienCu += Convert.ToDouble(table[i].Money.ToString());
                    }
                    else
                    {
                        TienMoiNhat += Convert.ToDouble(table[i].TongTienThanhToan.ToString());
                    }
                }
                Double Tongtiens = (TienCu / 1000);

                Response.Write("Tổng điểm mua hàng hàng cũ: " + Tongtiens + "<br />");
                Response.Write("Tổng điểm mua hàng hàng mới sau 02/09/2020: " + TienMoiNhat + "<br />");
                Double TongtienCL = ((TienCu / 1000) + TienMoiNhat);
                lttongtienmuahang.Text = TongtienCL.ToString();
            }
            else
            {
                lttongtienmuahang.Text = "0";
            }

        }

        private void ShowDiemChietKhauViVipDaMua(string IDThanhVien)
        {
            string sql = "";
            List<CartDetail> table = db.CartDetails.Where(p => p.IDThanhVien == int.Parse(IDThanhVien) && p.TrangThaiKhieuKien.ToString() == "0" && p.TrangThaiNhaCungCap.ToString() == "1" && p.TrangThaiNguoiMuaHang.ToString() == "1").ToList();
            if (table.Count > 0)
            {
                Double ChietKhauVip = 0;
                for (int i = 0; i < table.Count; i++)
                {
                    ChietKhauVip += Convert.ToDouble(table[i].ChietKhauVip.ToString());
                }
                ltvivipmuahang.Text = ChietKhauVip.ToString();
            }
            else
            {
                ltvivipmuahang.Text = "0";
            }

        }

        private void ShowDiemChietKhauViVipDaMuaTamGiu(string IDThanhVien)
        {
            string sql = "";
            List<ViTamMuaHang> table = db.ViTamMuaHangs.Where(p => p.IDThanhVienMua == int.Parse(IDThanhVien)).ToList();
            if (table.Count > 0)
            {
                Double ChietKhauVip = 0;
                for (int i = 0; i < table.Count; i++)
                {
                    ChietKhauVip += Convert.ToDouble(table[i].ChietKhauVip.ToString());
                }
                ltvipmuahangtamgiu.Text = ChietKhauVip.ToString();
            }
            else
            {
                ltvipmuahangtamgiu.Text = "0";
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
        private void ShowHoahongBan(string IDThanhVien)
        {
            //string sql = "";
            //sql += " and IDUserNguoiDuocHuong=" + IDThanhVien + "";
            //sql += " and IDType in (10,11,13)";
            //List<EHoaHongThanhVien> table = SHoaHongThanhVien.Name_Text("SELECT * from HoaHongThanhVien where 1=1 " + sql + "");
            //if (table.Count > 0)
            //{
            //    double coin = 0.0;
            //    for (int i = 0; i < table.Count; i++)
            //    {
            //        coin += Convert.ToDouble(table[i].SoCoin.ToString());
            //    }
            //    lthoahongban.Text = coin.ToString();
            //}
            //else
            //{
            //    lthoahongban.Text = "0";
            //}
            var dt = db.S_HoaHongThanhVien_ThongKe(Convert.ToInt32(IDThanhVien), "10,11,12").ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    lthoahongban.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    lthoahongban.Text = "0";
                }
            }
            else
            {
                lthoahongban.Text = "0";
            }
        }

        private void ShowHoahongGioiThieu(string IDThanhVien)
        {
            //string sql = "";
            //sql += " and IDUserNguoiDuocHuong=" + IDThanhVien + "";
            //sql += " and IDType in (1,2,3,5,31)";
            //List<EHoaHongThanhVien> table = SHoaHongThanhVien.Name_Text("SELECT * from HoaHongThanhVien where 1=1 " + sql + "");
            //if (table.Count > 0)
            //{
            //    double coin = 0.0;
            //    for (int i = 0; i < table.Count; i++)
            //    {
            //        coin += Convert.ToDouble(table[i].SoCoin.ToString());
            //    }
            //    lthoahonggioithieu.Text = coin.ToString();
            //}
            //else
            //{lthhviquanly
            //    lthoahonggioithieu.Text = "0";
            //}
            var dt = db.S_HoaHongThanhVien_ThongKe(Convert.ToInt32(IDThanhVien), "1,2,3,5,31,404,406,400").ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    lthhviquanly.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    lthhviquanly.Text = "0";
                }
            }
            else
            {
                lthoahonggioithieu.Text = "0";
            }
        }
        private void ShowHoahongMua(string IDThanhVien)
        {
            //404,406,400
            //401,403,405
            var dt = db.S_HoaHongThanhVien_ThongKe(Convert.ToInt32(IDThanhVien), "6,7,71,72,73,74,75,76,77,78,79,9,13,30,55,300,302,401,403,405").ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    lthoahongmua.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    lthoahongmua.Text = "0";
                }
            }
            else
            {
                lthoahongmua.Text = "0";
            }
        }
        private void ShowHoahongMuaQRCode(string IDThanhVien)
        {
            var dt = db.S_HoaHongThanhVien_ThongKe(Convert.ToInt32(IDThanhVien), "200,201,202,203,204,205,206,207,208,209,210,211,212,213,214").ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    lthoahongmuaQRCode.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    lthoahongmuaQRCode.Text = "0";
                }
            }
            else
            {
                lthoahongmuaQRCode.Text = "0";
            }
        }

        private void ShowChuyenDiem(string IDThanhVien)
        {
            //List<ChuyenDiemThanhVien> table = db.ChuyenDiemThanhViens.Where(s => s.IDNguoiCap == int.Parse(IDThanhVien)).ToList();
            //if (table.Count > 0)
            //{
            //    double coin = 0.0;
            //    for (int i = 0; i < table.Count; i++)
            //    {
            //        coin += Convert.ToDouble(table[i].SoCoin.ToString());
            //    }
            //    ltdiemdachuyen.Text = coin.ToString();
            //}
            //else
            //{
            //    ltdiemdachuyen.Text = "0";
            //}

            var dt = db.S_ChuyenDiemThanhViens_ThongKe(Convert.ToInt32(IDThanhVien)).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    ltdiemdachuyen.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    ltdiemdachuyen.Text = "0";
                }
            }
            else
            {
                ltdiemdachuyen.Text = "0";
            }
        }
        private void ShowDienmDuocCap(string IDThanhVien)
        {
            //List<CapDiemThanhVien> table = db.CapDiemThanhViens.Where(s => s.IDNguoiNhanDiemCoin == int.Parse(IDThanhVien)).ToList();
            //if (table.Count > 0)
            //{
            //    double coin = 0.0;
            //    for (int i = 0; i < table.Count; i++)
            //    {
            //        coin += Convert.ToDouble(table[i].SoDiemCoin.ToString());
            //    }
            //    ltdiemduoccap.Text = coin.ToString();
            //}
            //else
            //{
            //    ltdiemduoccap.Text = "0";
            //}
            var dt = db.S_CapDiemThanhViens_ThongKe(Convert.ToInt32(IDThanhVien)).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    ltdiemduoccap.Text = dt[0].sodiem.ToString();
                }
                else
                {
                    ltdiemduoccap.Text = "0";
                }
            }
            else
            {
                ltdiemduoccap.Text = "0";
            }
        }

        //private void ShowProducts(string IDThanhVien)
        //{
        //    string sql = "select top 10 * from Carts where IDThanhVien=" + IDThanhVien + "";
        //    sql = sql + " order by Create_Date desc";
        //    List<LCart> dt = db.ExecuteQuery<LCart>(@"" + sql + "").ToList();
        //    if (dt.Count > 0)
        //    {
        //        rp_items.DataSource = dt;
        //        rp_items.DataBind();
        //    }
        //}

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

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                hd480.Value = "480";
            }
            else
            {
                hd480.Value = "0";
            }
            ShowThanhVien();
        }

        protected void checklang_CheckedChanged(object sender, EventArgs e)
        {
            if (checklang.Checked == true)
            {
                hdlailand.Value = ltviagland.Text;
            }
            else
            {
                hdlailand.Value = "0";
            }
            ShowThanhVien();
        }


    }
}