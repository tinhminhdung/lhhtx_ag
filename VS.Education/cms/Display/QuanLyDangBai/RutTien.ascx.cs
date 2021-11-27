using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class RutTien : System.Web.UI.UserControl
    {
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Form.DefaultButton = btrutien.UniqueID;
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                ShowInfo();
            }
            else
            {
                Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
        }
        private void ShowInfo()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                if (table != null)
                {
                    if (table.DuyetTienDanap == 0)
                    {
                        Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này. Yêu cầu kích hoạt tài khoản thành viên.');window.location.href='/vi-tien.html'; </script>");
                    }
                    if (table.TatChucNang == 1)
                    {
                        Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này.');window.location.href='/vi-tien.html'; </script>");
                    }
                    hdid.Value = table.iuser_id.ToString();
                    hdmobile.Value = table.vphone.ToString();
                    hdemail.Value = table.vemail.ToString();
                    hdaddress.Value = table.vaddress.ToString();

                    if (table.Uutien.ToString() == "1")
                    {
                        btrutien.Enabled = false;
                        lthongbao.Text = "Tài khoản của bạn đang ở chế độ Ưu tiên nên bạn không thể rút được điểm . Vui lòng liên hệ với Công ty.";
                    }

                    if (table.TongTienCoinDuocCap.ToString() != "0")
                    {
                        lttongtien.Text = table.TongTienCoinDuocCap.ToString();
                    }
                    else
                    {
                        lttongtien.Text = "0 điểm";
                    }
                }
            }
        }
        protected void btrutien_Click(object sender, EventArgs e)
        {
            #region Trừ tiền trong ví
            user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
            if (iitem != null)
            {
                double ConglaiCoin = 0;
                double TongSoCoinDaCo = Convert.ToDouble(iitem.TongTienCoinDuocCap);
                double TongTienCanRutCoin = Convert.ToDouble(txtsotiencanrut.Text);

                #region Chỉ được rút tiền khi điểm lớn hơn hoặc = 200 điểm
                if (TongSoCoinDaCo <= 200)
                {
                    ltmsg.Text = "<div class=\"ruttienthongbaos\">Chỉ được rút tiền khi điểm lớn hơn hoặc bằng 200 điểm.</div>";
                    return;
                }
                #endregion
                // double QuYVNDRaCoin = (TongTienCanRutCoin) / 1000;
                if (TongSoCoinDaCo >= TongTienCanRutCoin)
                {
                    if (TongSoCoinDaCo.ToString() != "0")// Nếu trong ví có lớn hơn 0 đồng thì cộng tiếp
                    {
                        ConglaiCoin = ((TongSoCoinDaCo) - (TongTienCanRutCoin));
                        iitem.TongTienCoinDuocCap = ConglaiCoin.ToString();
                        db.SubmitChanges();
                    }
                    if (TongSoCoinDaCo.ToString() != "0")// Nếu trong ví có lớn hơn 0 đồng thì cộng tiếp
                    {
                        LichSuRutTien obj = new LichSuRutTien();
                        obj.IDThanhVien = int.Parse(hdid.Value);
                        obj.TongTienTrongVi = "";// iitem.TongTienCongLai;
                        obj.SoTienCanRut = txtsotiencanrut.Text;
                        obj.SoCoin = ConglaiCoin.ToString();
                        obj.TenNganHang = txttennganhang.Text;
                        obj.HoVaTen = txthovaten.Text;
                        obj.SoTaiKHoan = txtsotaikhoan.Text;
                        obj.ChiNhanh = txtchinhanh.Text;
                        obj.NoiDungChuyenTien = txtnoidungchuyentien.Text;
                        obj.GhiChu = txtghichu.Text;
                        obj.TrangThai = 0;
                        obj.NgayTao = DateTime.Now;
                        obj.NgayDuyet = "";
                        obj.NguoiDuyet = "";
                        db.LichSuRutTiens.InsertOnSubmit(obj);
                        db.SubmitChanges();

                        LichSuGiaoDich("19", "Rút tiền", hdid.Value, hdid.Value, "0", TongSoCoinDaCo.ToString());
                        ltmsg.Text = "";
                        ltmsg.Text += "<div class=\"thongbaos\">Bạn đã gửi yêu cầu rút điểm thành công.</div> ";
                        ltmsg.Text += "<div class=\"thongbaos\">Số điểm đã rút :" + txtsotiencanrut.Text + " điểm</div>";

                        // Sent Mail
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/cms/Display/SentMail/RutTien.html"));
                        content = content.Replace("{{CustomerName}}", txthovaten.Text);
                        content = content.Replace("{{Phone}}", hdmobile.Value);
                        content = content.Replace("{{Email}}", hdemail.Value);
                        content = content.Replace("{{Address}}", hdaddress.Value);
                        content = content.Replace("{{Total}}", txtsotiencanrut.Text);

                        content = content.Replace("{{Tennganhang}}", txttennganhang.Text);
                        content = content.Replace("{{Sotaikhoan}}", txtsotaikhoan.Text);
                        content = content.Replace("{{Chinhanh}}", txtchinhanh.Text);
                        content = content.Replace("{{Noidung}}", txtnoidungchuyentien.Text);
                        content = content.Replace("{{Ghichu}}", txtghichu.Text);

                        //emailnhanthongbaorutien
                        var EmailContTy = Commond.Setting("Emailden");
                        var emailnhanthongbaorutien = Commond.Setting("emailnhanthongbaorutien");
                        if (!EmailContTy.Equals(""))
                            new MailHelper().SendMail(EmailContTy, "Thành viên " + txthovaten.Text + " rút tiền trên hệ thống Agroupusa.com", content);
                        if (!emailnhanthongbaorutien.Equals(""))
                            new MailHelper().SendMail(emailnhanthongbaorutien, "Thành viên " + txthovaten.Text + " rút tiền trên hệ thống Agroupusa.com", content);

                        txtsotiencanrut.Text = "";
                        txttennganhang.Text = "";
                        txthovaten.Text = "";
                        txtsotaikhoan.Text = "";
                        txtchinhanh.Text = "";
                        txtnoidungchuyentien.Text = "";
                        txtghichu.Text = "";
                        ShowInfo();


                    }
                }
                else
                {
                    ltmsg.Text = "<div class=\"thongbaos\">Số điểm không đủ để thanh toán</div> ";
                    return;
                }
            }
            #endregion
        }
        void LichSuGiaoDich(string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
        {
            #region LichSuGiaoDich
            LichSuGiaoDich obl = new LichSuGiaoDich();
            obl.IDProducts = int.Parse("0");
            obl.IDType = int.Parse(IDType);
            obl.Type = Type;
            obl.IDThanhVien = int.Parse(IDThanhVien);
            obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
            obl.PhamTramHoaHong = PhamTramHoaHong;
            obl.SoCoin = SoCoin.ToString();
            obl.NgayTao = DateTime.Now;
            db.LichSuGiaoDiches.InsertOnSubmit(obl);
            db.SubmitChanges();
            #endregion
        }

        protected void bthuy_Click(object sender, EventArgs e)
        {
            txtsotiencanrut.Text = "";
            txttennganhang.Text = "";
            txthovaten.Text = "";
            txtsotaikhoan.Text = "";
            txtchinhanh.Text = "";
            txtnoidungchuyentien.Text = "";
            txtghichu.Text = "";
            ltmsg.Text = "";
        }
    }
}