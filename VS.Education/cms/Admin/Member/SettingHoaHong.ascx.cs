using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class SettingHoaHong : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["lang"] != null)
            {
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["lang"] = this.lang;
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            this.Page.Form.DefaultButton = btnsetup.UniqueID;
            if (!base.IsPostBack)
            {
                if (MoreAll.MoreAll.GetCookie("URole") != null)
                {
                    string strArray = MoreAll.MoreAll.GetCookie("URole").ToString().Trim();
                    if (strArray.Length > 0)
                    {
                        if (strArray.Contains("|9"))
                        {
                            this.binddata();
                        }
                        else if (!strArray.Contains("|9"))
                        {
                            Response.Redirect("/admin.aspx");
                        }
                    }
                }



            }
        }

        private void binddata()
        {
            try
            {
                #region Setting
                List<Entity.Setting> str = SSetting.GETBYALL(lang);
                ltmsg.Text = string.Empty;
                if (str.Count >= 1)
                {
                    foreach (Entity.Setting its in str)
                    {
                        if (its.Properties == "hoahonggttructiep")
                        {
                            this.txthoahonggttructiep.Text = its.Value;
                        }
                        if (its.Properties == "hoahonggtgiantiep")
                        {
                            this.txthoahonggtgiantiep.Text = its.Value;
                        }
                        if (its.Properties == "hoahonggtLeader")
                        {
                            this.txthoahonggtLeader.Text = its.Value;
                        }
                        if (its.Properties == "hoahonggtchinhanh")
                        {
                            this.txthoahonggtchinhanh.Text = its.Value;
                        }
                        if (its.Properties == "TuDongDuyetDonHang")
                        {
                            this.txtduyetdontudong.Text = its.Value;
                        }

                        if (its.Properties == "txtHoaHongGioiThieuTrucTiepmuahangVaF1")
                        {
                            this.txtHoaHongGioiThieuTrucTiepmuahangVaF1.Text = its.Value;
                        }
                        if (its.Properties == "HoaHongGioiThieuF1")
                        {
                            this.txtHoaHongGioiThieuF1.Text = its.Value;
                        }
                        if (its.Properties == "HoaHongGioiThieuF2")
                        {
                            this.txtHoaHongGioiThieuF2.Text = its.Value;
                        }
                        if (its.Properties == "HoaHongGioiThieuF3")
                        {
                            this.txtHoaHongGioiThieuF3.Text = its.Value;
                        }
                        if (its.Properties == "HoaHongGioiThieuF4")
                        {
                            this.txtHoaHongGioiThieuF4.Text = its.Value;
                        }
                        if (its.Properties == "HoaHongGioiThieuF5")
                        {
                            this.txtHoaHongGioiThieuF5.Text = its.Value;
                        }

                        if (its.Properties == "HoaHongChiNhanhMuaHang")
                        {
                            this.txtHoaHongChiNhanhMuaHang.Text = its.Value;
                        }
                        if (its.Properties == "HoaHongLeaderMuaHang")
                        {
                            this.txtHoaHongLeaderMuaHang.Text = its.Value;
                        }
                        if (its.Properties == "HoaHongGioiThieuTrucTiepNhaCungCap")
                        {
                            this.txtHoaHongGioiThieuTrucTiepNhaCungCap.Text = its.Value;
                        }
                        if (its.Properties == "HoaHongGioiThieuGianTiepNhaCungCap")
                        {
                            this.txtHoaHongGioiThieuGianTiepNhaCungCap.Text = its.Value;
                        }
                        if (its.Properties == "HoaHongChiNhanhBanHang")
                        {
                            this.txtHoaHongChiNhanhBanHang.Text = its.Value;
                        }

                        if (its.Properties == "txtAFFChuyengia")
                        {
                            this.txtAFFChuyengia.Text = its.Value;
                        }
                        if (its.Properties == "txtAGLandChuyengia")
                        {
                            this.txtAGLandChuyengia.Text = its.Value;
                        }

                        if (its.Properties == "HaiTamPhanTram")
                        {
                            this.txtHaiTamPhanTram.Text = its.Value;
                        }
                        if (its.Properties == "BaHaiPhanTram")
                        {
                            this.txtBaHaiPhanTram.Text = its.Value;
                        }
                        if (its.Properties == "tienmotcophan")
                        {
                            this.txttienmotcophan.Text = its.Value;
                        }
                        if (its.Properties == "txtF1AnTheoAgland")
                        {
                            this.txtF1AnTheoAgland.Text = its.Value;
                        }


                        if (its.Properties == "txtChietKhauQRcode")
                        {
                            this.txtChietKhauQRcode.Text = its.Value;
                        }

                        if (its.Properties == "txthoahongnguoimuaQRcode")
                        {
                            this.txthoahongnguoimuaQRcode.Text = its.Value;
                        }

                        if (its.Properties == "txthoahonghethongQRcode")
                        {
                            this.txthoahonghethongQRcode.Text = its.Value;
                        }

                        if (its.Properties == "Thongbaoduyethang")
                        {
                            this.txtthongbaoduyethang.Text = its.Value;
                        }


                        if (its.Properties == "txthuydonhang")
                        {
                            this.txthuydonhang.Text = its.Value;
                        }


                        if (its.Properties == "txttangFree")
                        {
                            this.txttangFree.Text = its.Value;
                        }
                        if (its.Properties == "txttangthanhvien")
                        {
                            this.txttangthanhvien.Text = its.Value;
                        }


                        if (its.Properties == "txtngaythu7")
                        {
                            this.txtngaythu7.Text = its.Value;
                        }
                        if (its.Properties == "txtngaythu1")
                        {
                            this.txtngaythu1.Text = its.Value;
                        }
                        if (its.Properties == "txtngaythu2")
                        {
                            this.txtngaythu2.Text = its.Value;
                        }
                        if (its.Properties == "txtngaythu3")
                        {
                            this.txtngaythu3.Text = its.Value;
                        }
                        if (its.Properties == "txtngaythu4")
                        {
                            this.txtngaythu4.Text = its.Value;
                        }
                        if (its.Properties == "txtngaythu5")
                        {
                            this.txtngaythu5.Text = its.Value;
                        }
                        if (its.Properties == "txtngaythu6")
                        {
                            this.txtngaythu6.Text = its.Value;
                        }



                        if (its.Properties == "Thue")
                        {
                            this.txtthue.Text = its.Value;
                        }
                        if (its.Properties == "txttienlenlevel")
                        {
                            this.txttienlenlevel.Text = its.Value;
                        }
                        if (its.Properties == "txtTienkichhoatdaily")
                        {
                            this.txtTienkichhoatdaily.Text = its.Value;
                        }
                        if (its.Properties == "txtThanhViencuahang")
                        {
                            this.txtThanhViencuahang.Text = its.Value;
                        }
                        if (its.Properties == "txtbanchuyengia")
                        {
                            this.txtbanchuyengia.Text = its.Value;
                        }
                        if (its.Properties == "txtbanchuyengianDangKy")
                        {
                            this.txtbanchuyengianDangKy.Text = its.Value;
                        }

                        if (its.Properties == "TienKichHoat")
                        {
                            this.txttienKichHoat.Text = its.Value;
                        }
                        if (its.Properties == "txtHHGTF1")
                        {
                            this.txtHHGTF1.Text = its.Value;
                        }
                        if (its.Properties == "txtHHGTF2")
                        {
                            this.txtHHGTF2.Text = its.Value;
                        }
                        if (its.Properties == "txtHHGTF3")
                        {
                            this.txtHHGTF3.Text = its.Value;
                        }
                        if (its.Properties == "txtHHGTF4")
                        {
                            this.txtHHGTF4.Text = its.Value;
                        }
                        if (its.Properties == "txtHHGTF5")
                        {
                            this.txtHHGTF5.Text = its.Value;
                        }

                        if (its.Properties == "txtdoanhsodonghuongmuaban")
                        {
                            this.txtdoanhsodonghuongmuaban.Text = its.Value;
                        }
                        if (its.Properties == "txtdoanhsodonghuongDangKy")
                        {
                            this.txtdoanhsodonghuongDangKy.Text = its.Value;
                        }
                        //if (its.Properties == "txtThuNhapNCC")
                        //{
                        //    this.txtThuNhapNCC.Text = its.Value;
                        //}
                        if (its.Properties == "txtthuongquanly")
                        {
                            this.txtthuongquanly.Text = its.Value;
                        }
                        if (its.Properties == "txtthuongquanlyThanhVien")
                        {
                            this.txtthuongquanlyThanhVien.Text = its.Value;
                        }

                    }
                }
                #endregion
            }
            catch (Exception) { }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }
        protected void btnsetup_Click(object sender, EventArgs e)
        {
            try
            {
                #region Setting
                if (Page.IsValid)
                {
                    #region Setting
                    Entity.Setting obj = new Entity.Setting();

                    obj.Lang = lang;
                    obj.Properties = "hoahonggttructiep";
                    obj.Value = txthoahonggttructiep.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "hoahonggtgiantiep";
                    obj.Value = txthoahonggtgiantiep.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "hoahonggtLeader";
                    obj.Value = txthoahonggtLeader.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "hoahonggtchinhanh";
                    obj.Value = txthoahonggtchinhanh.Text;
                    SSetting.UPDATE(obj);


                    obj.Lang = lang;
                    obj.Properties = "txtHoaHongGioiThieuTrucTiepmuahangVaF1";
                    obj.Value = txtHoaHongGioiThieuTrucTiepmuahangVaF1.Text;
                    SSetting.UPDATE(obj);


                    obj.Lang = lang;
                    obj.Properties = "HoaHongGioiThieuF1";
                    obj.Value = txtHoaHongGioiThieuF1.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "HoaHongGioiThieuF2";
                    obj.Value = txtHoaHongGioiThieuF2.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "HoaHongGioiThieuF3";
                    obj.Value = txtHoaHongGioiThieuF3.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "HoaHongGioiThieuF4";
                    obj.Value = txtHoaHongGioiThieuF4.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "HoaHongGioiThieuF5";
                    obj.Value = txtHoaHongGioiThieuF5.Text;
                    SSetting.UPDATE(obj);


                    obj.Lang = lang;
                    obj.Properties = "HoaHongChiNhanhMuaHang";
                    obj.Value = txtHoaHongChiNhanhMuaHang.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "HoaHongLeaderMuaHang";
                    obj.Value = txtHoaHongLeaderMuaHang.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "HoaHongGioiThieuTrucTiepNhaCungCap";
                    obj.Value = txtHoaHongGioiThieuTrucTiepNhaCungCap.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "HoaHongGioiThieuGianTiepNhaCungCap";
                    obj.Value = txtHoaHongGioiThieuGianTiepNhaCungCap.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "HoaHongChiNhanhBanHang";
                    obj.Value = txtHoaHongChiNhanhBanHang.Text;
                    SSetting.UPDATE(obj);


                    obj.Lang = lang;
                    obj.Properties = "HaiTamPhanTram";
                    obj.Value = txtHaiTamPhanTram.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "BaHaiPhanTram";
                    obj.Value = txtBaHaiPhanTram.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "tienmotcophan";
                    obj.Value = txttienmotcophan.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "TuDongDuyetDonHang";
                    obj.Value = txtduyetdontudong.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "Thongbaoduyethang";
                    obj.Value = txtthongbaoduyethang.Text;
                    SSetting.UPDATE(obj);


                    obj.Lang = lang;
                    obj.Properties = "Thue";
                    obj.Value = txtthue.Text;
                    SSetting.UPDATE(obj);


                    obj.Lang = lang;
                    obj.Properties = "txtAFFChuyengia";
                    obj.Value = txtAFFChuyengia.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtAGLandChuyengia";
                    obj.Value = txtAGLandChuyengia.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtF1AnTheoAgland";
                    obj.Value = txtF1AnTheoAgland.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txthuydonhang";
                    obj.Value = txthuydonhang.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txttangthanhvien";
                    obj.Value = txttangthanhvien.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txttangFree";
                    obj.Value = txttangFree.Text;
                    SSetting.UPDATE(obj);


                    obj.Lang = lang;
                    obj.Properties = "txtngaythu7";
                    obj.Value = txtngaythu7.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtngaythu1";
                    obj.Value = txtngaythu1.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtngaythu2";
                    obj.Value = txtngaythu2.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtngaythu3";
                    obj.Value = txtngaythu3.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtngaythu4";
                    obj.Value = txtngaythu4.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtngaythu5";
                    obj.Value = txtngaythu5.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtngaythu6";
                    obj.Value = txtngaythu6.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txttienlenlevel";
                    obj.Value = txttienlenlevel.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtTienkichhoatdaily";
                    obj.Value = txtTienkichhoatdaily.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtThanhViencuahang";
                    obj.Value = txtThanhViencuahang.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtbanchuyengia";
                    obj.Value = txtbanchuyengia.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtbanchuyengianDangKy";
                    obj.Value = txtbanchuyengianDangKy.Text;
                    SSetting.UPDATE(obj);


                    obj.Lang = lang;
                    obj.Properties = "TienKichHoat";
                    obj.Value = txttienKichHoat.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtHHGTF1";
                    obj.Value = txtHHGTF1.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtHHGTF2";
                    obj.Value = txtHHGTF2.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtHHGTF3";
                    obj.Value = txtHHGTF3.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtHHGTF4";
                    obj.Value = txtHHGTF4.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtHHGTF5";
                    obj.Value = txtHHGTF5.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtdoanhsodonghuongmuaban";
                    obj.Value = txtdoanhsodonghuongmuaban.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txtdoanhsodonghuongDangKy";
                    obj.Value = txtdoanhsodonghuongDangKy.Text;
                    SSetting.UPDATE(obj);

                    //obj.Lang = lang;
                    //obj.Properties = "txtThuNhapNCC";
                    //obj.Value = txtThuNhapNCC.Text;
                    //SSetting.UPDATE(obj);


                    obj.Lang = lang;
                    obj.Properties = "txtthuongquanly";
                    obj.Value = txtthuongquanly.Text;

                    obj.Lang = lang;
                    obj.Properties = "txtthuongquanlyThanhVien";
                    obj.Value = txtthuongquanlyThanhVien.Text;

                    SSetting.UPDATE(obj);

                    #endregion
                }
                this.binddata();
                this.ltmsg.Text = "Thiết lập th\x00e0nh c\x00f4ng!";
                #endregion
            }
            catch (Exception) { }
        }

        protected void btcapnhatQRcode_Click(object sender, EventArgs e)
        {

            Double TongChietKhauNGuoiMua = Convert.ToDouble(txthoahongnguoimuaQRcode.Text.Trim());
            Double TongChietKhau = 100 - TongChietKhauNGuoiMua;


            try
            {
                #region Setting
                if (Page.IsValid)
                {
                    #region Setting
                    Entity.Setting obj = new Entity.Setting();
                    obj.Lang = lang;
                    obj.Properties = "txtChietKhauQRcode";
                    obj.Value = txtChietKhauQRcode.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txthoahongnguoimuaQRcode";
                    obj.Value = txthoahongnguoimuaQRcode.Text;
                    SSetting.UPDATE(obj);

                    obj.Lang = lang;
                    obj.Properties = "txthoahonghethongQRcode";
                    obj.Value = TongChietKhau.ToString();// txthoahonghethongQRcode.Text;
                    SSetting.UPDATE(obj);
                    obj.Lang = lang;

                    obj.Properties = "NgayCapNhapQRCode";
                    obj.Value = "Người Cập nhật : " + MoreAll.MoreAll.GetCookies("UName").ToString() + " - " + DateTime.Now.ToString();
                    SSetting.UPDATE(obj);
                    #endregion
                }


                Susers.Name_Text("update users set QRCodeChietKhauHH='" + txtChietKhauQRcode.Text + "',QRCodeHHNguoiMua='" + txthoahongnguoimuaQRcode.Text + "',QRCodeHHHeThong='" + TongChietKhau + "'");

                #region LichSuQRCode
                LichSuQRCode objk = new LichSuQRCode();
                objk.IDThanhVien = int.Parse("0");
                objk.ChietKhauHH = txtChietKhauQRcode.Text.Trim();
                objk.HHNGuoiMua = txthoahongnguoimuaQRcode.Text.Trim();
                objk.HHHeThong = TongChietKhau.ToString();// txthoahonghethongQRcode.Text.Trim();
                objk.NguoiDuyet = "Admin cấu hình tất cả thành viên : " + MoreAll.MoreAll.GetCookies("UName").ToString();
                objk.NgayDuyet = DateTime.Now;
                db.LichSuQRCodes.InsertOnSubmit(objk);
                db.SubmitChanges();
                #endregion

                // -4 ,làm them khi thay đổi giá trị hoa hồng theo ngày , ai thay đổi, admin hay khách hàng..lưu lịch sử
                this.binddata();
                this.ltmsg.Text = "Thiết lập th\x00e0nh c\x00f4ng!";
                #endregion
            }
            catch (Exception) { }
        }
        protected void ThaydoiCauHinh(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Lưu ý: khi bạn đồng ý sự kiện cập nhật hoa hồng QRCode thì tất cả thành viên sẽ bị cập nhật hoa hồng theo. Bạn có muốn thay đổi toàn bộ hoa hồng theo cấu hình??')";
        }
    }
}