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
    public partial class Detailorders : System.Web.UI.UserControl
    {
        public int i = 1;
        DatalinqDataContext db = new DatalinqDataContext();
        string IDMuahang = "";
        string URL = "";
        protected bool Dung = false;

        private string IDSanPham = "";
        private string IDGioHang = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IDMuahang"] != null && !Request["IDMuahang"].Equals(""))
            {
                IDMuahang = Request["IDMuahang"];
            }
            URL = Request.RawUrl.ToString();
            if (!base.IsPostBack)
            {
                ShowInfo();
                if (MoreAll.MoreAll.GetCookies("Members") != "")
                {
                    ShowProducts();
                }
                else
                {
                    Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                }

            }
        }
        private void ShowInfo()
        {
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                if (table != null)
                {
                    //if (table.DuyetTienDanap == 0)
                    //{
                    //    Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này. Yêu cầu kích hoạt tài khoản thành viên.');window.location.href='/vi-tien.html'; </script>");
                    //}

                    hdid.Value = table.iuser_id.ToString();
                }
            }
        }
        private void ShowProducts()
        {
            string sql = "select * from Carts where IDThanhVien=" + hdid.Value + " and ID=" + IDMuahang + "";
            List<LCart> dt = db.ExecuteQuery<LCart>(@"" + sql + "").ToList();
            if (dt != null)
            {
                try
                {
                    ltmadonhang.Text = dt[0].ID.ToString();
                    ltngaydathang.Text = dt[0].Create_Date.ToString();
                    lttrangthai.Text = ShowTrangThai(dt[0].Status.ToString());
                    lthovaten.Text = dt[0].Name.ToString();
                    ltdiachi.Text = dt[0].Address.ToString();
                    ltdienthoai.Text = dt[0].Phone.ToString();
                    ltnoidung.Text = dt[0].Contents;
                    lttongtien.Text = AllQuery.MorePro.Detail_Price(dt[0].Money.ToString());
                    lttongtienbangchu.Text = MoreAll.Hamdoisorachu.So_chu(Convert.ToDouble(dt[0].Money.ToString()));

                    List<Entity.CartDetail> table = SCartDetail.Detail_ID_Cart(dt[0].ID.ToString());
                    if (table.Count > 0)
                    {
                        this.rpcartdetail.DataSource = table;
                        this.rpcartdetail.DataBind();

                        foreach (var item in table)
                        {
                            if (item.NoiDungGiaoHang.Length > 5)
                            {
                                ltthongtin.Text += "<div class=\"alert alert-info\" role=\"alert\">Thông báo từ nhà cung cấp với sản phẩm : <b>" + Commond.ShowPro(item.ipid.ToString()) + " <br></b>";
                                ltthongtin.Text += "<div style='color:red'> " + item.NoiDungGiaoHang + "</div>";
                                ltthongtin.Text += "</div>";
                            }
                        }
                        //ltthongtin
                    }

                }
                catch (Exception)
                { }
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
        protected string Quantity(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Quantity.ToString();
            }
            return str.ToString();
        }
        protected string LayTuViNao(string TienTuViNao)
        {
            if (TienTuViNao == "1")
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Ví Mua Hàng</span>";
            }
            else if (TienTuViNao == "2")
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Ví Thương mại</span>";
            }
            else
            {
                return "";
            }
        }

        protected string Code(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Code.ToString();
            }
            return str.ToString();
        }
        protected string Name(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Name.ToString();
            }
            return str.ToString();
        }

        // chi tiết giỏ hàng

        protected string Anh(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Images.ToString();
            }
            return str.ToString();
        }
        protected string Codes(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Code.ToString();
            }
            return str.ToString();
        }
        protected string Kho(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str = dt[0].Quantity.ToString();
            }
            return str.ToString();
        }

        protected string Showtrangthai(string status)
        {
            if (status.Equals("1"))
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Chấp Nhận</span>";
            }
            if (status.Equals("2"))
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Trả hàng</span>";
            }
            return "";
        }
        protected string ShowtrangthaiNCC(string status, string LyDoHuyHang)
        {
            if (status.Equals("1"))
            {
                return "<a style='background:#0b98ea;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Nhà cung cấp đã duyệt đơn hàng này.'>Đã duyệt</a>";
            }
            else if (status.Equals("3"))
            {
                return "<a style='background:#ffa903;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Nhà cung cấp chưa xử lý đơn hàng này'>Chưa xử lý</a>";
            }
            else if (status.Equals("2"))
            {
                string ld = "";
                if (LyDoHuyHang.Length > 0)
                {
                    ld = "<span class='lydohuyhang'>" + LyDoHuyHang + "</span>";
                }
                return "<a style='background:#ed1c24;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'  title='Nhà cung cấp chưa đã hủy đơn hàng này'>Hủy đơn</a><br>" + ld + "";
            }
            else if (status.Equals("4"))
            {
                return "<a style='background:#669d0b;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Nhà cung cấp đang xử lý cho người mua'>Đang chờ xử lý</a>";
            }
            else if (status.Equals("5"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Nhà cung cấp đã hoàn lại tiền cho hàng đơn hàng này của bạn'>Hoàn tiền</a>";
            }
            else if (status.Equals("6"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Khiếu kiện lên Admin'>Khiếu kiện</a><a style='margin:5px;color: #a50606;float: left;;' title='Đang chờ Admin xử lý'>Đang chờ Admin xử lý</a>";
            }
            else if (status.Equals("7"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Admin Chấp nhận thanh toán'>Admin Chấp nhận TT</a>";
            }
            else if (status.Equals("8"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Admin Hoàn tiền'>Admin Hoàn tiền</a>";
            }
            return "";
        }
        protected bool EnableLock(string id)
        {
            List<Carts> table2 = SCarts.Carts_GetById(id.ToString());
            if (table2.Count > 0)
            {
                if (table2[0].Status.ToString() == "1")
                {
                    return false;
                }
            }
            return true;
        }
        protected bool EnableLock_HuyDonHang(string Enable)
        {
            // 1 Đã duyệt
            // 2: Hủy đơn hàng
            //3:  Chờ xử lý
            if (Enable.ToString() == "2")
            {
                return true;
            }
            return false;
        }
        protected bool EnableLock_DuyetHang(string Enable)
        {
            // 1 Đã duyệt
            // 2: Hủy đơn hàng
            //3:  Chờ xử lý
            if (Enable.ToString() == "1" || Enable.ToString() == "2")
            {
                return false;
            }
            return true;
        }
        protected bool EnableLock_XoaDon(string TrangThaiKhieuKien, string TrangThaiNhaCungCap, string TrangThaiNguoiMuaHang)
        {
            if (TrangThaiKhieuKien.ToString() == "0" && TrangThaiNhaCungCap.ToString() == "3" && TrangThaiNguoiMuaHang.ToString() == "3")
            {
                return true;
            }
            return false;
        }

        protected string NhaCungCapDaDuyet(string Enable)
        {
            // 1 Đã duyệt
            // 2: Hủy đơn hàng
            //3:  Chờ xử lý
            if (Enable.ToString() == "1")
            {
                return "display:block";
            }
            return "display:none";
        }
        protected void ChapNhan_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn chấp nhận sản phẩm này. Đồng nghĩa là sẽ không có phát sinh tranh chấp nào. và bạn đã nhận được hàng.?')";
        }
        protected void TraHang_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn Trả sản phẩm này?. Đồng nghĩa là bạn sẽ không nhận được hàng.')";
        }
        protected void XoaDon_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn hủy sản phẩm này khỏi đơn hàng?.')";
        }

        protected void rpcartdetail_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string strv = e.CommandName.Trim();
            string str2 = e.CommandArgument.ToString().Trim();
            string str4 = strv;

            switch (e.CommandName)
            {
                case "ChapNhan":
                    #region ChapNhan

                    SinhHoaHong(str2);
                    List<CartDetail> ds = db.CartDetails.OrderByDescending(s => s.TrangThaiKhieuKien == 0 && s.TrangThaiNhaCungCap == 3 && s.TienTuViNao != 0 && s.ID == Convert.ToInt32(str2)).ToList();
                    if (ds.Count > 0)
                    {
                        #region Gửi mail cho nhà cung cấp khi thành viên mua hủy sản phẩm.
                        List<Entity.users> dc = Susers.GET_BY_ID(ds[0].IDThanhVien.ToString());
                        if (dc.Count > 0)
                        {
                            string Emails = dc[0].vemail.ToString();
                            string Noidung = "";
                            Noidung += "Kính gửi nhà cung cấp: <b>" + ShowNameNhaCungCap(ds[0].IDNhaCungCap.ToString()) + "</b><br />";
                            // 
                            Noidung += "<b> Chúc mừng đơn hàng của bạn đã được người mua chấp nhận thành công, và người mua đã nhận được hàng. ! </b><br /> Đơn hàng <a href=\"http://aggroup365.com/account/orders/" + ds[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + ds[0].ID_Cart.ToString() + "</b></a> . Vui lòng xem nội dung ở phía dưới.<br />";
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
                                new MailHelper().SendMail(ShowEmailNhaCungCap(ds[0].IDNhaCungCap.ToString()), "Chấp nhận đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + ds[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                new MailHelper().SendMail(dc[0].vemail.ToString(), "Chấp nhận đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + ds[0].ID_Cart.ToString() + " ", Noidung.ToString());
                            }
                            catch { }
                        }
                        #endregion
                    }
                    SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=1,LyDoHuyHang='',LyDoTraHang='' WHERE ID =" + str2 + "");
                    ltthongbao.Text = "<script type=\"text/javascript\">alert('Duyệt đơn hàng thành công..');window.location.href='" + URL + "'; </script>";

                    #endregion
                    return;
                case "TraHang":
                    #region TraHang
                    //// Cập nhậ trạng thái trả đơn hàng
                    //SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=2 WHERE ID =" + str2 + "");
                    ////Cập nhật trạng thái nhà cung cấp chưa xử lý đơn hàng, vì có trường hợp bên nhà cung cấp và ng mua đã trao đổi xong và họ muốn đặt lại đơn hàng đó
                    //SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNhaCungCap]=4 WHERE ID =" + str2 + "");
                    // List<CartDetail> item = db.CartDetails.OrderByDescending(s => s.TrangThaiKhieuKien == 0 && s.TrangThaiNhaCungCap == 3 && s.TienTuViNao != 0 && s.ID == Convert.ToInt32(str2)).ToList();
                    // if (item.Count > 0)
                    // {
                    //     #region Gửi mail cho nhà cung cấp khi thành viên mua hủy sản phẩm.
                    //     List<Entity.users> dc = Susers.GET_BY_ID(item[0].IDThanhVien.ToString());
                    //     if (dc.Count > 0)
                    //     {
                    //         string Emails = dc[0].vemail.ToString();
                    //         string Noidung = "";
                    //         Noidung += "Kính gửi nhà cung cấp: <b>" + ShowNameNhaCungCap(item[0].IDNhaCungCap.ToString()) + "</b><br />";
                    //         Noidung += "<b>Chúng tôi rất xin lỗi! </b><br /> Đơn hàng <a href=\"http://aggroup365.com/account/orders/" + item[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + item[0].ID_Cart.ToString() + "</b></a> của bạn đã trả lại. Vui lòng xem nội dung ở phía dưới.<br />";
                    //         Noidung += "<br />";

                    //         Noidung += "<b>Tên sản phẩm :  </b>" + Commond.ShowPro(item[0].ipid.ToString()) + "<br />";
                    //         Noidung += "<b>Số lượng sản phẩm :  </b>" + item[0].Quantity.ToString() + "<br />";
                    //         Noidung += "<b>Tổng số tiền :  </b>" + item[0].Money.ToString() + "<br />";
                    //         Noidung += "---------------------------------";
                    //         Noidung += "<br />";

                    //         Noidung += "<b>Người mua hàng: </b>" + dc[0].vfname.ToString() + "<br />";
                    //         Noidung += "<b>Địa chỉ: </b>" + dc[0].vaddress.ToString() + "<br />";
                    //         Noidung += "<b>Điện thoại: </b>" + dc[0].vphone.ToString() + "<br />";
                    //         Noidung += "<b>Email: </b>" + dc[0].vemail.ToString() + "<br />";

                    //         Noidung += "<br />";
                    //         Noidung += "<br />";
                    //         Noidung += Commond.Setting("txtfooterEmail");
                    //         Noidung += "<br />";

                    //         try
                    //         {
                    //             new MailHelper().SendMail(ShowEmailNhaCungCap(item[0].IDNhaCungCap.ToString()), "Trả lại đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + item[0].ID_Cart.ToString() + " ", Noidung.ToString());
                    //             new MailHelper().SendMail(dc[0].vemail.ToString(), "Trả lại đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + item[0].ID_Cart.ToString() + " ", Noidung.ToString());
                    //         }
                    //         catch { }
                    //     }
                    //     #endregion
                    // }
                    //ltthongbao.Text = "<script type=\"text/javascript\">alert('Trả hàng thành công. Hãy Nêu lý do Trả hàng vào ô lý do.');window.location.href='" + URL + "'; </script></div>";
                    #endregion
                    return;
                case "XoaDon":
                    #region XoaDon
                    List<CartDetail> dt = db.CartDetails.OrderByDescending(s => s.TrangThaiKhieuKien == 0 && s.TrangThaiNhaCungCap == 3 && s.TienTuViNao != 0 && s.ID == Convert.ToInt32(str2)).ToList();
                    if (dt.Count > 0)
                    {
                        SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=2,[TrangThaiNhaCungCap]=2,LyDoHuyHang=N'Người mua đã hủy sản phẩm này',LyDoTraHang=N'Tôi không mua sản phẩm này nữa.',TienTuViNao=0 WHERE ID =" + str2 + "");
                        //SCartDetail.Name_Text("delete from CartDetail WHERE ID =" + str2 + "");

                        /// Trả lại tiền
                        // Trả điểm cho người mua và bán lấy ở bảng tạm
                        ViTamMuaHang Listv = db.ViTamMuaHangs.Where(s => s.IDCartDetail == int.Parse(str2) && s.NCCDuyet == 1).FirstOrDefault();
                        if (Listv != null)
                        {
                            //user iiit = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(Listv.IDThanhVienMua.ToString()));
                            //if (iiit != null)
                            //{
                            //    double ViTangTienVip = Convert.ToDouble(iiit.ViTangTienVip);
                            //    double ViHienTaiCoin = Convert.ToDouble(iiit.TongTienCoinDuocCap);
                            //    double ChietKhauVip = Convert.ToDouble(Listv.ChietKhauVip.ToString());

                            //    double TSoTienNguoiMuaBiTru = Convert.ToDouble(Listv.SoTienNguoiMuaBiTru.ToString());

                            //    double Conglai = 0;
                            //    Conglai = ((ViHienTaiCoin) + (TSoTienNguoiMuaBiTru));
                            //    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + Listv.IDThanhVienMua.ToString() + "");

                            //    double CongVip = ((ViTangTienVip) + (ChietKhauVip));
                            //    // Trả lại tiền vào THƯỞNG MUA HÀNG
                            //    Susers.Name_Text("update users set ViTangTienVip=" + CongVip.ToString() + "  where iuser_id=" + Listv.IDThanhVienMua.ToString() + "");
                            //}
                            user iiit = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(Listv.IDThanhVienMua.ToString()));
                            if (iiit != null)
                            {
                                double ViTangTienVip = Convert.ToDouble(iiit.ViTangTienVip);
                                double ViHienTaiCoin = Convert.ToDouble(iiit.TongTienCoinDuocCap);
                                double VIAFF = Convert.ToDouble(iiit.ViMuaHangAFF);
                                double ChietKhauVip = Convert.ToDouble(Listv.ChietKhauVip.ToString());

                                double TSoTienNguoiMuaBiTru = Convert.ToDouble(Listv.SoTienNguoiMuaBiTru.ToString());
                                if (Listv.LayTienOVi == 0)// trả cho ví AFF
                                {
                                    double Conglai = 0;
                                    Conglai = ((VIAFF) + (TSoTienNguoiMuaBiTru));
                                    Susers.Name_Text("update users set ViMuaHangAFF=" + Conglai.ToString() + "  where iuser_id=" + Listv.IDThanhVienMua.ToString() + "");
                                }
                                else if (Listv.LayTienOVi == 1)// trả cho ví Thương mại
                                {
                                    double Conglai = 0;
                                    Conglai = ((ViHienTaiCoin) + (TSoTienNguoiMuaBiTru));
                                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + Listv.IDThanhVienMua.ToString() + "");
                                }

                                double CongVip = ((ViTangTienVip) + (ChietKhauVip));
                                // Trả lại tiền vào THƯỞNG MUA HÀNG
                                Susers.Name_Text("update users set ViTangTienVip=" + CongVip.ToString() + "  where iuser_id=" + Listv.IDThanhVienMua.ToString() + "");
                            }

                            ViTamMuaHang delv = db.ViTamMuaHangs.Where(s => s.IDCartDetail == int.Parse(str2) && s.NCCDuyet == 1).FirstOrDefault();// xóa 1
                            if (delv != null)
                            {
                                db.ViTamMuaHangs.DeleteOnSubmit(delv);
                                db.SubmitChanges();
                            }

                            #region Gửi mail cho nhà cung cấp khi thành viên mua hủy sản phẩm.
                            List<Entity.users> dc = Susers.GET_BY_ID(dt[0].IDThanhVien.ToString());
                            if (dc.Count > 0)
                            {
                                string Emails = dc[0].vemail.ToString();

                                //Gửi email
                                string Noidung = "";
                                //Noidung += " <b>Website</b>: <span style=\"color:red\">" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "</span><br /><br />";

                                Noidung += "Kính gửi nhà cung cấp: <b>" + ShowNameNhaCungCap(dt[0].IDNhaCungCap.ToString()) + "</b><br />";
                                Noidung += "<b>Chúng tôi rất xin lỗi! </b><br /> Đơn hàng <a href=\"http://aggroup365.com/account/orders/" + dt[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + dt[0].ID_Cart.ToString() + "</b></a> của bạn đã bị hủy do người mua đã hủy mua sản phẩm này.<br />";
                                Noidung += "<br />";
                                Noidung += "<b>Tên sản phẩm :  </b>" + Commond.ShowPro(dt[0].ipid.ToString()) + "<br />";
                                Noidung += "<b>Số lượng sản phẩm :  </b>" + dt[0].Quantity.ToString() + "<br />";
                                Noidung += "<b>Tổng số tiền :  </b>" + dt[0].Money.ToString() + "<br />";
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
                                    new MailHelper().SendMail(ShowEmailNhaCungCap(dt[0].IDNhaCungCap.ToString()), "Hủy đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + dt[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                    // new MailHelper().SendMail(dc[0].vemail.ToString(), "Hủy đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + dt[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                }
                                catch { }
                            }
                            #endregion
                            ltthongbao.Text = "<script type=\"text/javascript\">alert('Hủy đơn hàng thành công. Điểm tạm giữ đã bị trả lại. Quý khách vui lòng kiểm tra lại.');window.location.href='" + URL + "'; </script></div>";
                        }
                        else
                        {
                            ltthongbao.Text = "<script type=\"text/javascript\">alert('Hủy đơn hàng thành công.');window.location.href='" + URL + "'; </script></div>";
                        }
                    }
                    else
                    {
                        ltthongbao.Text = "<script type=\"text/javascript\">alert('Bạn không thể xóa sản phẩm này vì nhà cung cấp đã duyệt sản phẩm này rồi.');window.location.href='" + URL + "'; </script></div>";
                    }
                    #endregion
                    return;
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
        public string ShowNameNhaCungCap(string id)
        {
            string code = "0";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count > 0)
            {
                code = dt[0].vfname.ToString();
            }
            return code;
        }

        protected string GiaNhap(string id, string quantity)
        {
            string str = "0";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                if (dt[0].Giacongtynhapvao.Length > 0)
                {
                    Double Tongtien = Convert.ToInt32(quantity) * Convert.ToDouble(dt[0].Giacongtynhapvao.ToString());
                    return Tongtien.ToString();
                }
            }
            return str.ToString();
        }
        protected void txtLyDoTraHang_TextChanged(object sender, EventArgs e)
        {
            TextBox LyDo = (TextBox)sender;
            var b = (HiddenField)LyDo.FindControl("hdids");
            if (LyDo.Text.Length > 10)
            {
                List<Entity.CartDetail> item = SCartDetail.GetDetail(b.Value);
                if (item.Count > 0)
                {
                    List<Entity.CartDetail> list = SCartDetail.Name_Text("select * from CartDetail where id=" + b.Value + "  ");
                    if (list.Count > 0)
                    {
                        SCartDetail.Name_Text("UPDATE [CartDetail] SET [LyDoTraHang] =N'" + LyDo.Text + "' WHERE ID =" + b.Value + "");
                    }
                    ltthongbao.Text = "<script type=\"text/javascript\">alert('Nhập lý do trả hàng thành công.');window.location.href='" + URL + "'; </script></div>";
                }
            }
            else
            {
                ltthongbao.Text = "<script type=\"text/javascript\">alert('Vui lòng nhập lý do trả hàng trên 10 ký tự.');window.location.href='" + URL + "'; </script></div>";
            }
        }

        #region Các công thức tính kèm

        void SinhHoaHong(string str2)
        {
            string SoTienNhaCCBan = "";
            string SoTienDaiLyMua = "";
            string TienLayOViNao = "";
            string TimF1Agland = "0";
            List<CartDetail> dtcart = db.CartDetails.Where(s => s.ID == int.Parse(str2)).ToList();
            if (dtcart.Count > 0)
            {
                IDSanPham = dtcart[0].ipid.ToString();
                IDGioHang = dtcart[0].ID_Cart.ToString();

                #region Lấy điểm ở bảng tạm và cộng cho nhà cung cấp
                string str = "";
                // Lấy điểm ở bảng tạm và cộng cho nhà cung cấp 
                ViTamMuaHang List = db.ViTamMuaHangs.Where(s => s.IDCartDetail == int.Parse(str2)).FirstOrDefault();
                if (List != null)
                {
                    user iiit = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(List.IDNhaCungCap.ToString()));
                    if (iiit != null)
                    {

                        SoTienNhaCCBan = List.SoTienNhaCCSeNhan;
                        SoTienDaiLyMua = List.SoTienNguoiMuaBiTru;
                        TienLayOViNao = List.LayTienOVi.ToString();

                        double ViHienTaiCoin = Convert.ToDouble(iiit.TongTienCoinDuocCap);
                        double TSoTienNhaCCSeNhan = Convert.ToDouble(List.SoTienNhaCCSeNhan.ToString());

                        double Conglai = 0;
                        Conglai = ((ViHienTaiCoin) + (TSoTienNhaCCSeNhan));

                        ThemHoaHongTamGiuMuaHang(dtcart[0].ipid.ToString(), "30", "Thanh toán tiền đơn hàng cho nhà Cung cấp", dtcart[0].IDThanhVien.ToString(), List.IDNhaCungCap.ToString(), "0", TSoTienNhaCCSeNhan.ToString());
                        Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + List.IDNhaCungCap.ToString() + "");

                        if (dtcart[0].ThanhVienFree_DaiLy.ToString() == "0")// 0 là free/ 1 là đại lý
                        {
                            ThemHoaHong(dtcart[0].ipid.ToString(), "300", "Tặng cho thành viên Free", dtcart[0].IDThanhVien.ToString(), dtcart[0].IDThanhVien.ToString(), "0", dtcart[0].TangThanhVienFree.ToString());
                        }
                        if (dtcart[0].CongDiemVechoAg.ToString() != "0")
                        {
                            ThemHoaHong(dtcart[0].ipid.ToString(), "301", "Tiền đổ về công ty", dtcart[0].IDThanhVien.ToString(), "0", "0", dtcart[0].CongDiemVechoAg.ToString());
                        }
                        try
                        {
                            Commond.CongSanPhamDaBan(List.IDNhaCungCap.ToString(), dtcart[0].Quantity.ToString());
                        }
                        catch (Exception)
                        { }

                    }
                }
                #endregion

                #region Tính Hoa Hồng

                ////////////////////#region Công tiền khi người mua đã thanh toán bằng ví khi mua sản phẩm --> cho nhà cung cấp sản phẩm đăng bán
                string IDNhaCungCapBanHang = dtcart[0].IDNhaCungCap.ToString();


                //1: thưởng cho người mua hàng trực tiếp dc hưởng 30% theo level nhé
                //2: thưởng cho người giới thiệu
                //3: Gián tiếp : thưởng cho người giới thiệu là 10%
                //4: Nâng level cho thành viên khi đủ điểm
                string Plevel = "99999999999";
                string TongLevel = "0";

                //TH1 : Nếu thành viên và leader mua hàng mua hàng
                user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(dtcart[0].IDThanhVien.ToString()));// Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                if (table != null)
                {
                    string TrangThaiAgLang = dtcart[0].TrangThaiAgLang.ToString();// 1 = sản phẩm , 2= bất động sản
                    // Kiểm tra nếu điểm thưởng mà nhỏ hơn 0 thì sẽ ko phát sinh hoa hồng nào nhé.
                    //Tiền Coin
                    //string CapoLevelHoaHongs = CapoLevelHoaHong(table.LevelThanhVien.ToString());
                    //double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);

                    //string CapoLevelHoaHongs = dtcart[0].HoaHongTheoLevel.ToString();
                    //double HoaHongs = Convert.ToDouble(dtcart[0].HoaHongTheoLevel.ToString());

                    string CapoLevelHoaHongs = MoreAll.Other.Giatri("txtHoaHongGioiThieuTrucTiepmuahangVaF1");//dtcart[i].HoaHongTheoLevel.ToString();
                    double HoaHongs = Convert.ToDouble(MoreAll.Other.Giatri("txtHoaHongGioiThieuTrucTiepmuahangVaF1"));// Convert.ToDouble(dtcart[i].HoaHongTheoLevel.ToString());

                    double Diemcoin = Convert.ToDouble(dtcart[0].TongDiemDemDiChia.ToString());
                    double TongCoin = (Diemcoin);/// 100;
                    double Tong = (TongCoin * HoaHongs) / 100;
                    if (TongCoin > 0)// Kiểm tra nếu điểm thưởng mà nhỏ hơn 0 thì sẽ ko phát sinh hoa hồng nào nhé.
                    {
                        #region Đối với mua hàng trực tiếp & hoa hồng & Chi Nhánh
                        //if (TrangThaiAgLang == "1")// Hoa hòng mua trực tiếp cho người mua hàng
                        //{
                        //    #region TH1: Nếu thành viên mua hàng mua hàng 30% tùy theo level
                        //    ThemHoaHong(dtcart[0].ipid.ToString(), "6", "Hoa hồng Thành Viên (Mua Hàng Trực Tiếp)  " + CapoLevelHoaHongs + "%", table.iuser_id.ToString(), table.iuser_id.ToString(), CapoLevelHoaHongs, Tong.ToString());
                        //    #endregion
                        //}

                        #region Phát sinh hoa hồng giới thiệu Và thưởng thêm theo level
                        // Ngày 7/01/2020 phát sinh yêu cầu về AGlang
                        // Khi có đơn hàng nào thuộc Thành viên (TrangThaiAgLang) AgLang giới thiệu thì mới được hoa hồng , nếu ai ko thuộc thành viên aglang thì ko được ăn hoa hồng 
                        // Về phía sản phẩm thì vẫn phát sinh hoa hồng bình thường.
                        if (table != null)
                        {
                            string IDThanhVien = table.iuser_id.ToString();// ID thành viên mua

                            #region Tính hoa hồng cho kiểu sản phẩm (TrangThaiAgLang=1)
                            if (TrangThaiAgLang == "1")
                            {

                                #region Ví chuyên gia mua bán
                                try
                                {
                                    user ChuyenGiga = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(SetVi.SetThanhVienChuyenGia()));
                                    if (ChuyenGiga != null)
                                    {
                                        string IDBanChuyenGia = SetVi.SetThanhVienChuyenGia();
                                        double HHChuyengia = Convert.ToDouble(Commond.Setting("txtbanchuyengia"));
                                        double TienHH = Convert.ToDouble(Diemcoin * HHChuyengia) / 100;
                                        ThemHoaHong(dtcart[0].ipid.ToString(), "401", "Hoa hồng Mua Bán - Ban Đào tạo - Chuyên gia ", IDThanhVien.Trim().ToLower(), IDBanChuyenGia.ToString(), HHChuyengia.ToString(), TienHH.ToString());
                                    }
                                }
                                catch (Exception)
                                { }
                                #endregion

                                #region Ví doanh số đồng hưởng
                                try
                                {
                                    string IDDongHuong = SetVi.SetViDongHuongDoanhSo();
                                    double HHDH = Convert.ToDouble(Commond.Setting("txtdoanhsodonghuongmuaban"));
                                    double TienHH = Convert.ToDouble(Diemcoin * HHDH) / 100;
                                    ThemHoaHong(dtcart[0].ipid.ToString(), "403", "Hoa hồng  - Danh số đồng hưởng", IDThanhVien.Trim().ToLower(), IDDongHuong.ToString(), HHDH.ToString(), TienHH.ToString());
                                    MDoanhSoDongHuong.DoanhSoDongHuongMuaBan(TienHH.ToString(), dtcart[0].ipid.ToString(), IDThanhVien.ToString(), IDGioHang.ToString(), "");
                                }
                                catch (Exception)
                                { }
                                #endregion
                                #region Hoa Hồng Thưởng quản lý
                                try
                                {
                                    double txtthuongquanly = Convert.ToDouble(MoreAll.Other.Giatri("txtthuongquanly"));
                                    if (!MoreAll.Other.Giatri("txtthuongquanly").Equals("0"))
                                    {
                                        double TongLeader = (TongCoin * txtthuongquanly) / 100;
                                        ThemHoaHong(dtcart[0].ipid.ToString(), "405", "Hoa Hồng (Thưởng Quản Lý)", table.iuser_id.ToString(), SetVi.SetViThuongQuanLy(), txtthuongquanly.ToString(), TongLeader.ToString());
                                    }
                                }
                                catch (Exception)
                                { }
                                #endregion

                                if (table.GioiThieu.ToString() != "0")
                                {
                                    #region Hoa Hồng Gián tiếp F1
                                    double HoaHongGioiThieuF1 = Convert.ToDouble(MoreAll.Other.Giatri("txtHoaHongGioiThieuTrucTiepmuahangVaF1"));

                                    //  double HoaHongF1 = (Tong * HoaHongGioiThieuF1) / 100;
                                    double HoaHongF1 = Tong;

                                    if (table.GioiThieu.ToString() != "0")
                                    {
                                        ThemHoaHong(dtcart[0].ipid.ToString(), "7", "Hoa hồng giới thiệu F1 ", IDThanhVien.Trim().ToLower(), table.GioiThieu.ToString(), CapoLevelHoaHongs.ToString(), HoaHongF1.ToString());
                                        #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                        // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                        // Nếu người mua hàng có level = 4 tức =45% thì dừng ko cho F1 được hưởng nữa
                                        try
                                        {
                                            if (table.LevelThanhVien.ToString() == "5")
                                            {
                                                Dung = false;
                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                            }
                                            else
                                            {
                                                Dung = true;
                                                Plevel = Plevel + "," + table.LevelThanhVien.ToString();
                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F1", "75", IDThanhVien, table.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(table.GioiThieu.ToString()), TimLevelB(IDThanhVien));
                                            }
                                        }
                                        catch (Exception)
                                        { }
                                        #endregion
                                    }
                                    #region Hoa Hồng Gián tiếp F2
                                    user tableTVTF2 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(table.GioiThieu.ToString()));
                                    if (tableTVTF2 != null)
                                    {
                                        double HoaHongGioiThieuF2 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF2"));
                                        double HoaHongF2 = (Diemcoin * HoaHongGioiThieuF2) / 100;
                                        if (tableTVTF2.GioiThieu.ToString() != "0")
                                        {
                                            ThemHoaHong(dtcart[0].ipid.ToString(), "71", "Hoa hồng giới thiệu F2 ", IDThanhVien.Trim().ToLower(), tableTVTF2.GioiThieu.ToString(), HoaHongGioiThieuF2.ToString(), HoaHongF2.ToString());
                                            #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                            // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                            try
                                            {
                                                if (tableTVTF2.LevelThanhVien.ToString() == "5")
                                                {
                                                    Dung = false;
                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                }
                                                else
                                                {
                                                    Dung = true;
                                                    Plevel = Plevel + "," + tableTVTF2.LevelThanhVien.ToString();
                                                }
                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                if (Plevel.ToString() == "99999999999")
                                                {

                                                }
                                                else
                                                {
                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                }
                                                if (Dung == true)
                                                {
                                                    if (TongLevel != "8")// 8 là ko tìm thấy giá trị nào cao hơn 0 ở hàm MinAndMax
                                                    {
                                                        if (TongLevel != "45")// 45 là ko hưởng hoa hồng nữa
                                                        {
                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F2", "76", IDThanhVien, tableTVTF2.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), TongLevel);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F2", "76", IDThanhVien, tableTVTF2.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), "0");
                                                    }
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                            #endregion
                                        }

                                        #region Hoa Hồng Gián tiếp F3
                                        user tableTVTF3 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF2.GioiThieu.ToString()));
                                        if (tableTVTF3 != null)
                                        {
                                            double HoaHongGioiThieuF3 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF3"));
                                            double HoaHongF3 = (Diemcoin * HoaHongGioiThieuF3) / 100;
                                            if (tableTVTF3.GioiThieu.ToString() != "0")
                                            {
                                                ThemHoaHong(dtcart[0].ipid.ToString(), "72", "Hoa hồng giới thiệu F3 ", IDThanhVien.Trim().ToLower(), tableTVTF3.GioiThieu.ToString(), HoaHongGioiThieuF3.ToString(), HoaHongF3.ToString());
                                                #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                try
                                                {
                                                    if (tableTVTF3.LevelThanhVien.ToString() == "5")
                                                    {
                                                        Dung = false;
                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                    }
                                                    else
                                                    {
                                                        Dung = true;
                                                        Plevel = Plevel + "," + tableTVTF3.LevelThanhVien.ToString();
                                                    }


                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                    if (Plevel.ToString() == "99999999999")
                                                    {

                                                    }
                                                    else
                                                    {
                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                    }
                                                    if (Dung == true)
                                                    {
                                                        if (TongLevel != "8")
                                                        {
                                                            if (TongLevel != "45")
                                                            {
                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F3", "77", IDThanhVien, tableTVTF3.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), TongLevel);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F3", "77", IDThanhVien, tableTVTF3.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), "0");
                                                        }
                                                    }
                                                }
                                                catch (Exception)
                                                { }
                                                #endregion

                                            }
                                            #region Hoa Hồng Gián tiếp F4
                                            user tableTVTF4 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF3.GioiThieu.ToString()));
                                            if (tableTVTF4 != null)
                                            {
                                                double HoaHongGioiThieuF4 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF4"));
                                                double HoaHongF4 = (Diemcoin * HoaHongGioiThieuF4) / 100;
                                                if (tableTVTF4.GioiThieu.ToString() != "0")
                                                {
                                                    ThemHoaHong(dtcart[0].ipid.ToString(), "73", "Hoa hồng giới thiệu F4 ", IDThanhVien.Trim().ToLower(), tableTVTF4.GioiThieu.ToString(), HoaHongGioiThieuF4.ToString(), HoaHongF4.ToString());

                                                    #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                    // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa

                                                    try
                                                    {
                                                        if (tableTVTF4.LevelThanhVien.ToString() == "5")
                                                        {
                                                            Dung = false;
                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                        }
                                                        else
                                                        {
                                                            Dung = true;
                                                            Plevel = Plevel + "," + tableTVTF4.LevelThanhVien.ToString();
                                                        }
                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                        if (Plevel.ToString() == "99999999999")
                                                        {

                                                        }
                                                        else
                                                        {
                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                        }
                                                        if (Dung == true)
                                                        {
                                                            if (TongLevel != "8")
                                                            {
                                                                if (TongLevel != "45")
                                                                {
                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F4", "78", IDThanhVien, tableTVTF4.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), TongLevel);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F4", "78", IDThanhVien, tableTVTF4.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), "0");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    { }
                                                    #endregion
                                                }

                                                #region Hoa Hồng Gián tiếp F5
                                                user tableTVTF5 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF4.GioiThieu.ToString()));
                                                if (tableTVTF5 != null)
                                                {
                                                    double HoaHongGioiThieuF5 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF5"));
                                                    double HoaHongF5 = (Diemcoin * HoaHongGioiThieuF5) / 100;
                                                    if (tableTVTF5.GioiThieu.ToString() != "0")
                                                    {
                                                        ThemHoaHong(dtcart[0].ipid.ToString(), "74", "Hoa hồng giới thiệu F5", IDThanhVien.Trim().ToLower(), tableTVTF5.GioiThieu.ToString(), HoaHongGioiThieuF5.ToString(), HoaHongF5.ToString());

                                                        #region Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                        // Nếu level =45% (tưng ứng 4) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                        try
                                                        {
                                                            if (tableTVTF5.LevelThanhVien.ToString() == "5")
                                                            {
                                                                Dung = false;
                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                            }
                                                            else
                                                            {
                                                                Dung = true;
                                                                Plevel = Plevel + "," + tableTVTF5.LevelThanhVien.ToString();
                                                            }

                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                            if (Plevel.ToString() == "99999999999")
                                                            {

                                                            }
                                                            else
                                                            {
                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                            }
                                                            if (Dung == true)
                                                            {
                                                                if (TongLevel != "8")
                                                                {
                                                                    if (TongLevel != "45")
                                                                    {
                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F5", "79", IDThanhVien, tableTVTF5.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), TongLevel);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F5", "79", IDThanhVien, tableTVTF5.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), "0");
                                                                }
                                                            }
                                                        }
                                                        catch (Exception)
                                                        { }
                                                        #endregion
                                                    }
                                                    ////////////555555555555555
                                                    #region Hoa Hồng Gián tiếp F6
                                                    user tableTVTF6 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF5.GioiThieu.ToString()));
                                                    if (tableTVTF6 != null)
                                                    {
                                                        if (tableTVTF6.GioiThieu.ToString() != "0")
                                                        {
                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                            // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                            try
                                                            {
                                                                if (tableTVTF6.LevelThanhVien.ToString() == "5")
                                                                {
                                                                    Dung = false;
                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                }
                                                                else
                                                                {
                                                                    Dung = true;
                                                                    Plevel = Plevel + "," + tableTVTF6.LevelThanhVien.ToString();
                                                                }
                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                if (Plevel.ToString() == "99999999999")
                                                                {

                                                                }
                                                                else
                                                                {
                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                }
                                                                if (Dung == true)
                                                                {
                                                                    if (TongLevel != "8")
                                                                    {
                                                                        if (TongLevel != "45")
                                                                        {
                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F6", "55", IDThanhVien, tableTVTF6.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), TongLevel);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F6", "55", IDThanhVien, tableTVTF6.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), "0");
                                                                    }
                                                                }
                                                            }
                                                            catch (Exception)
                                                            { }
                                                            #endregion
                                                        }

                                                        #region Hoa Hồng Gián tiếp F7
                                                        user tableTVTF7 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF6.GioiThieu.ToString()));
                                                        if (tableTVTF7 != null)
                                                        {
                                                            if (tableTVTF7.GioiThieu.ToString() != "0")
                                                            {
                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                try
                                                                {
                                                                    if (tableTVTF7.LevelThanhVien.ToString() == "5")
                                                                    {
                                                                        Dung = false;
                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                    }
                                                                    else
                                                                    {
                                                                        Dung = true;
                                                                        Plevel = Plevel + "," + tableTVTF7.LevelThanhVien.ToString();
                                                                    }
                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                    if (Plevel.ToString() == "99999999999")
                                                                    {

                                                                    }
                                                                    else
                                                                    {
                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                    }
                                                                    if (Dung == true)
                                                                    {
                                                                        if (TongLevel != "8")
                                                                        {
                                                                            if (TongLevel != "45")
                                                                            {
                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F7", "55", IDThanhVien, tableTVTF7.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), TongLevel);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F7", "55", IDThanhVien, tableTVTF7.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), "0");
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception)
                                                                { }
                                                                #endregion
                                                            }

                                                            #region Hoa Hồng Gián tiếp F8
                                                            user tableTVTF8 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF7.GioiThieu.ToString()));
                                                            if (tableTVTF8 != null)
                                                            {
                                                                if (tableTVTF8.GioiThieu.ToString() != "0")
                                                                {
                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                    try
                                                                    {
                                                                        if (tableTVTF8.LevelThanhVien.ToString() == "5")
                                                                        {
                                                                            Dung = false;
                                                                            Plevel = "45";
                                                                        }
                                                                        else
                                                                        {
                                                                            Dung = true;
                                                                            Plevel = Plevel + "," + tableTVTF8.LevelThanhVien.ToString();
                                                                        }
                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                        if (Plevel.ToString() == "99999999999")
                                                                        {

                                                                        }
                                                                        else
                                                                        {
                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                        }
                                                                        if (Dung == true)
                                                                        {
                                                                            if (TongLevel != "8")
                                                                            {
                                                                                if (TongLevel != "45")
                                                                                {
                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F8", "55", IDThanhVien, tableTVTF8.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), TongLevel);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F8", "55", IDThanhVien, tableTVTF8.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), "0");
                                                                            }
                                                                        }
                                                                    }
                                                                    catch (Exception)
                                                                    { }
                                                                    #endregion
                                                                }

                                                                #region Hoa Hồng Gián tiếp F9
                                                                user tableTVTF9 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF8.GioiThieu.ToString()));
                                                                if (tableTVTF9 != null)
                                                                {
                                                                    if (tableTVTF9.GioiThieu.ToString() != "0")
                                                                    {
                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                        try
                                                                        {
                                                                            if (tableTVTF9.LevelThanhVien.ToString() == "5")
                                                                            {
                                                                                Dung = false;
                                                                                Plevel = "45";
                                                                            }
                                                                            else
                                                                            {
                                                                                Dung = true;
                                                                                Plevel = Plevel + "," + tableTVTF9.LevelThanhVien.ToString();
                                                                            }
                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                            if (Plevel.ToString() == "99999999999")
                                                                            {

                                                                            }
                                                                            else
                                                                            {
                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                            }
                                                                            if (Dung == true)
                                                                            {
                                                                                if (TongLevel != "8")
                                                                                {
                                                                                    if (TongLevel != "45")
                                                                                    {
                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F9", "55", IDThanhVien, tableTVTF9.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), TongLevel);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F9", "55", IDThanhVien, tableTVTF9.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), "0");
                                                                                }
                                                                            }
                                                                        }
                                                                        catch (Exception)
                                                                        { }
                                                                        #endregion
                                                                    }

                                                                    #region Hoa Hồng Gián tiếp F10
                                                                    user tableTVTF10 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF9.GioiThieu.ToString()));
                                                                    if (tableTVTF10 != null)
                                                                    {
                                                                        if (tableTVTF10.GioiThieu.ToString() != "0")
                                                                        {
                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                            try
                                                                            {
                                                                                if (tableTVTF10.LevelThanhVien.ToString() == "5")
                                                                                {
                                                                                    Dung = false;
                                                                                    Plevel = "45";
                                                                                }
                                                                                else
                                                                                {
                                                                                    Dung = true;
                                                                                    Plevel = Plevel + "," + tableTVTF10.LevelThanhVien.ToString();
                                                                                }
                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                if (Plevel.ToString() == "99999999999")
                                                                                {

                                                                                }
                                                                                else
                                                                                {
                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                }
                                                                                if (Dung == true)
                                                                                {
                                                                                    if (TongLevel != "8")
                                                                                    {
                                                                                        if (TongLevel != "45")
                                                                                        {
                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F10", "55", IDThanhVien, tableTVTF10.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), TongLevel);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F10", "55", IDThanhVien, tableTVTF10.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), "0");
                                                                                    }
                                                                                }
                                                                            }
                                                                            catch (Exception)
                                                                            { }
                                                                            #endregion
                                                                        }

                                                                        #region Hoa Hồng Gián tiếp F11
                                                                        user tableTVTF11 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF10.GioiThieu.ToString()));
                                                                        if (tableTVTF11 != null)
                                                                        {
                                                                            if (tableTVTF11.GioiThieu.ToString() != "0")
                                                                            {
                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                try
                                                                                {
                                                                                    if (tableTVTF11.LevelThanhVien.ToString() == "5")
                                                                                    {
                                                                                        Dung = false;
                                                                                        Plevel = "45";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Dung = true;
                                                                                        Plevel = Plevel + "," + tableTVTF11.LevelThanhVien.ToString();
                                                                                    }
                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                    {

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                    }
                                                                                    if (Dung == true)
                                                                                    {
                                                                                        if (TongLevel != "8")
                                                                                        {
                                                                                            if (TongLevel != "45")
                                                                                            {
                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F11", "55", IDThanhVien, tableTVTF11.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), TongLevel);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F11", "55", IDThanhVien, tableTVTF11.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), "0");
                                                                                        }
                                                                                    }
                                                                                }
                                                                                catch (Exception)
                                                                                { }
                                                                                #endregion
                                                                            }
                                                                            #region Hoa Hồng Gián tiếp F12
                                                                            user tableTVTF12 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF11.GioiThieu.ToString()));
                                                                            if (tableTVTF12 != null)
                                                                            {
                                                                                if (tableTVTF12.GioiThieu.ToString() != "0")
                                                                                {
                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                    try
                                                                                    {
                                                                                        if (tableTVTF12.LevelThanhVien.ToString() == "5")
                                                                                        {
                                                                                            Dung = false;
                                                                                            Plevel = "45";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Dung = true;
                                                                                            Plevel = Plevel + "," + tableTVTF12.LevelThanhVien.ToString();
                                                                                        }
                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                        {

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                        }
                                                                                        if (Dung == true)
                                                                                        {
                                                                                            if (TongLevel != "8")
                                                                                            {
                                                                                                if (TongLevel != "45")
                                                                                                {
                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F12", "55", IDThanhVien, tableTVTF12.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), TongLevel);
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F12", "55", IDThanhVien, tableTVTF12.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), "0");
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    catch (Exception)
                                                                                    { }
                                                                                    #endregion
                                                                                }

                                                                                #region Hoa Hồng Gián tiếp F13
                                                                                user tableTVTF13 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF12.GioiThieu.ToString()));
                                                                                if (tableTVTF13 != null)
                                                                                {
                                                                                    if (tableTVTF13.GioiThieu.ToString() != "0")
                                                                                    {
                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                        try
                                                                                        {
                                                                                            if (tableTVTF13.LevelThanhVien.ToString() == "5")
                                                                                            {
                                                                                                Dung = false;
                                                                                                Plevel = "45";
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Dung = true;
                                                                                                Plevel = Plevel + "," + tableTVTF13.LevelThanhVien.ToString();
                                                                                            }
                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                            {

                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                            }
                                                                                            if (Dung == true)
                                                                                            {
                                                                                                if (TongLevel != "8")
                                                                                                {
                                                                                                    if (TongLevel != "45")
                                                                                                    {
                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F13", "55", IDThanhVien, tableTVTF13.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), TongLevel);
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F13", "55", IDThanhVien, tableTVTF13.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), "0");
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        catch (Exception)
                                                                                        { }
                                                                                        #endregion
                                                                                    }

                                                                                    #region Hoa Hồng Gián tiếp F14
                                                                                    user tableTVTF14 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF13.GioiThieu.ToString()));
                                                                                    if (tableTVTF14 != null)
                                                                                    {
                                                                                        if (tableTVTF14.GioiThieu.ToString() != "0")
                                                                                        {
                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                            try
                                                                                            {
                                                                                                if (tableTVTF14.LevelThanhVien.ToString() == "5")
                                                                                                {
                                                                                                    Dung = false;
                                                                                                    Plevel = "45";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Dung = true;
                                                                                                    Plevel = Plevel + "," + tableTVTF14.LevelThanhVien.ToString();
                                                                                                }
                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                {

                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                }
                                                                                                if (Dung == true)
                                                                                                {
                                                                                                    if (TongLevel != "8")
                                                                                                    {
                                                                                                        if (TongLevel != "45")
                                                                                                        {
                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F14", "55", IDThanhVien, tableTVTF14.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), TongLevel);
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F14", "55", IDThanhVien, tableTVTF14.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), "0");
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            catch (Exception)
                                                                                            { }
                                                                                            #endregion
                                                                                        }

                                                                                        #region Hoa Hồng Gián tiếp F15
                                                                                        user tableTVTF15 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF14.GioiThieu.ToString()));
                                                                                        if (tableTVTF15 != null)
                                                                                        {
                                                                                            if (tableTVTF15.GioiThieu.ToString() != "0")
                                                                                            {
                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                try
                                                                                                {
                                                                                                    if (tableTVTF15.LevelThanhVien.ToString() == "5")
                                                                                                    {
                                                                                                        Dung = false;
                                                                                                        Plevel = "45";
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Dung = true;
                                                                                                        Plevel = Plevel + "," + tableTVTF15.LevelThanhVien.ToString();
                                                                                                    }
                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                    {

                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                    }
                                                                                                    if (Dung == true)
                                                                                                    {
                                                                                                        if (TongLevel != "8")
                                                                                                        {
                                                                                                            if (TongLevel != "45")
                                                                                                            {
                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F15", "55", IDThanhVien, tableTVTF15.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), TongLevel);
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F15", "55", IDThanhVien, tableTVTF15.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), "0");
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                                catch (Exception)
                                                                                                { }
                                                                                                #endregion
                                                                                            }

                                                                                            #region Hoa Hồng Gián tiếp F16
                                                                                            user tableTVTF16 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF15.GioiThieu.ToString()));
                                                                                            if (tableTVTF16 != null)
                                                                                            {
                                                                                                if (tableTVTF16.GioiThieu.ToString() != "0")
                                                                                                {
                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                    try
                                                                                                    {
                                                                                                        if (tableTVTF16.LevelThanhVien.ToString() == "5")
                                                                                                        {
                                                                                                            Dung = false;
                                                                                                            Plevel = "45";
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            Dung = true;
                                                                                                            Plevel = Plevel + "," + tableTVTF16.LevelThanhVien.ToString();
                                                                                                        }
                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                        {

                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                        }
                                                                                                        if (Dung == true)
                                                                                                        {
                                                                                                            if (TongLevel != "8")
                                                                                                            {
                                                                                                                if (TongLevel != "45")
                                                                                                                {
                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F16", "55", IDThanhVien, tableTVTF16.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), TongLevel);
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F16", "55", IDThanhVien, tableTVTF16.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), "0");
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                    catch (Exception)
                                                                                                    { }
                                                                                                    #endregion
                                                                                                }

                                                                                                #region Hoa Hồng Gián tiếp F17
                                                                                                user tableTVTF17 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF16.GioiThieu.ToString()));
                                                                                                if (tableTVTF17 != null)
                                                                                                {
                                                                                                    if (tableTVTF17.GioiThieu.ToString() != "0")
                                                                                                    {
                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                        try
                                                                                                        {
                                                                                                            if (tableTVTF17.LevelThanhVien.ToString() == "5")
                                                                                                            {
                                                                                                                Dung = false;
                                                                                                                Plevel = "45";
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                Dung = true;
                                                                                                                Plevel = Plevel + "," + tableTVTF17.LevelThanhVien.ToString();
                                                                                                            }
                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                            {

                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                            }
                                                                                                            if (Dung == true)
                                                                                                            {
                                                                                                                if (TongLevel != "8")
                                                                                                                {
                                                                                                                    if (TongLevel != "45")
                                                                                                                    {
                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F17", "55", IDThanhVien, tableTVTF17.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), TongLevel);
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F17", "55", IDThanhVien, tableTVTF17.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), "0");
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception)
                                                                                                        { }
                                                                                                        #endregion
                                                                                                    }

                                                                                                    #region Hoa Hồng Gián tiếp F18
                                                                                                    user tableTVTF18 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF17.GioiThieu.ToString()));
                                                                                                    if (tableTVTF18 != null)
                                                                                                    {
                                                                                                        if (tableTVTF18.GioiThieu.ToString() != "0")
                                                                                                        {
                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                            try
                                                                                                            {
                                                                                                                if (tableTVTF18.LevelThanhVien.ToString() == "5")
                                                                                                                {
                                                                                                                    Dung = false;
                                                                                                                    Plevel = "45";
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    Dung = true;
                                                                                                                    Plevel = Plevel + "," + tableTVTF18.LevelThanhVien.ToString();
                                                                                                                }
                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                {

                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                }
                                                                                                                if (Dung == true)
                                                                                                                {
                                                                                                                    if (TongLevel != "8")
                                                                                                                    {
                                                                                                                        if (TongLevel != "45")
                                                                                                                        {
                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F18", "55", IDThanhVien, tableTVTF18.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), TongLevel);
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F18", "55", IDThanhVien, tableTVTF18.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), "0");
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                            catch (Exception)
                                                                                                            { }
                                                                                                            #endregion
                                                                                                        }

                                                                                                        #region Hoa Hồng Gián tiếp F19
                                                                                                        user tableTVTF19 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF18.GioiThieu.ToString()));
                                                                                                        if (tableTVTF19 != null)
                                                                                                        {
                                                                                                            if (tableTVTF19.GioiThieu.ToString() != "0")
                                                                                                            {
                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                try
                                                                                                                {
                                                                                                                    if (tableTVTF19.LevelThanhVien.ToString() == "5")
                                                                                                                    {
                                                                                                                        Dung = false;
                                                                                                                        Plevel = "45";
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        Dung = true;
                                                                                                                        Plevel = Plevel + "," + tableTVTF19.LevelThanhVien.ToString();
                                                                                                                    }
                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                    {

                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                    }
                                                                                                                    if (Dung == true)
                                                                                                                    {
                                                                                                                        if (TongLevel != "8")
                                                                                                                        {
                                                                                                                            if (TongLevel != "45")
                                                                                                                            {
                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F19", "55", IDThanhVien, tableTVTF19.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), TongLevel);
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F19", "55", IDThanhVien, tableTVTF19.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), "0");
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                                catch (Exception)
                                                                                                                { }
                                                                                                                #endregion
                                                                                                            }
                                                                                                            ///22222222222222
                                                                                                            #region Hoa Hồng Gián tiếp F20
                                                                                                            user tableTVTF20 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF19.GioiThieu.ToString()));
                                                                                                            if (tableTVTF20 != null)
                                                                                                            {
                                                                                                                if (tableTVTF20.GioiThieu.ToString() != "0")
                                                                                                                {
                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                    try
                                                                                                                    {
                                                                                                                        if (tableTVTF20.LevelThanhVien.ToString() == "5")
                                                                                                                        {
                                                                                                                            Dung = false;
                                                                                                                            Plevel = "45";
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            Dung = true;
                                                                                                                            Plevel = Plevel + "," + tableTVTF20.LevelThanhVien.ToString();
                                                                                                                        }
                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                        {

                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                        }
                                                                                                                        if (Dung == true)
                                                                                                                        {
                                                                                                                            if (TongLevel != "8")
                                                                                                                            {
                                                                                                                                if (TongLevel != "45")
                                                                                                                                {
                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F20", "55", IDThanhVien, tableTVTF20.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), TongLevel);
                                                                                                                                }
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F20", "55", IDThanhVien, tableTVTF20.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), "0");
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                    catch (Exception)
                                                                                                                    { }
                                                                                                                    #endregion
                                                                                                                }
                                                                                                                #region Hoa Hồng Gián tiếp F21
                                                                                                                user tableTVTF21 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF20.GioiThieu.ToString()));
                                                                                                                if (tableTVTF21 != null)
                                                                                                                {
                                                                                                                    if (tableTVTF21.GioiThieu.ToString() != "0")
                                                                                                                    {
                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                        try
                                                                                                                        {
                                                                                                                            if (tableTVTF21.LevelThanhVien.ToString() == "5")
                                                                                                                            {
                                                                                                                                Dung = false;
                                                                                                                                Plevel = "45";
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                Dung = true;
                                                                                                                                Plevel = Plevel + "," + tableTVTF21.LevelThanhVien.ToString();
                                                                                                                            }
                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                            {

                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                            }
                                                                                                                            if (Dung == true)
                                                                                                                            {
                                                                                                                                if (TongLevel != "8")
                                                                                                                                {
                                                                                                                                    if (TongLevel != "45")
                                                                                                                                    {
                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F21", "55", IDThanhVien, tableTVTF21.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), TongLevel);
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F21", "55", IDThanhVien, tableTVTF21.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), "0");
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                        catch (Exception)
                                                                                                                        { }
                                                                                                                        #endregion
                                                                                                                    }

                                                                                                                    #region Hoa Hồng Gián tiếp F22
                                                                                                                    user tableTVTF22 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF21.GioiThieu.ToString()));
                                                                                                                    if (tableTVTF22 != null)
                                                                                                                    {
                                                                                                                        if (tableTVTF22.GioiThieu.ToString() != "0")
                                                                                                                        {
                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                            try
                                                                                                                            {
                                                                                                                                if (tableTVTF22.LevelThanhVien.ToString() == "5")
                                                                                                                                {
                                                                                                                                    Dung = false;
                                                                                                                                    Plevel = "45";
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    Dung = true;
                                                                                                                                    Plevel = Plevel + "," + tableTVTF22.LevelThanhVien.ToString();
                                                                                                                                }
                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                {

                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                }
                                                                                                                                if (Dung == true)
                                                                                                                                {
                                                                                                                                    if (TongLevel != "8")
                                                                                                                                    {
                                                                                                                                        if (TongLevel != "45")
                                                                                                                                        {
                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F22", "55", IDThanhVien, tableTVTF22.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), TongLevel);
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F22", "55", IDThanhVien, tableTVTF22.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), "0");
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                            catch (Exception)
                                                                                                                            { }
                                                                                                                            #endregion
                                                                                                                        }

                                                                                                                        #region Hoa Hồng Gián tiếp F23
                                                                                                                        user tableTVTF23 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF22.GioiThieu.ToString()));
                                                                                                                        if (tableTVTF23 != null)
                                                                                                                        {
                                                                                                                            if (tableTVTF23.GioiThieu.ToString() != "0")
                                                                                                                            {
                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                try
                                                                                                                                {
                                                                                                                                    if (tableTVTF23.LevelThanhVien.ToString() == "5")
                                                                                                                                    {
                                                                                                                                        Dung = false;
                                                                                                                                        Plevel = "45";
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        Dung = true;
                                                                                                                                        Plevel = Plevel + "," + tableTVTF23.LevelThanhVien.ToString();
                                                                                                                                    }
                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                    {

                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                    }
                                                                                                                                    if (Dung == true)
                                                                                                                                    {
                                                                                                                                        if (TongLevel != "8")
                                                                                                                                        {
                                                                                                                                            if (TongLevel != "45")
                                                                                                                                            {
                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F23", "55", IDThanhVien, tableTVTF23.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), TongLevel);
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F23", "55", IDThanhVien, tableTVTF23.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), "0");
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                catch (Exception)
                                                                                                                                { }
                                                                                                                                #endregion
                                                                                                                            }

                                                                                                                            #region Hoa Hồng Gián tiếp F24
                                                                                                                            user tableTVTF24 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF23.GioiThieu.ToString()));
                                                                                                                            if (tableTVTF24 != null)
                                                                                                                            {
                                                                                                                                if (tableTVTF24.GioiThieu.ToString() != "0")
                                                                                                                                {
                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                    try
                                                                                                                                    {
                                                                                                                                        if (tableTVTF24.LevelThanhVien.ToString() == "5")
                                                                                                                                        {
                                                                                                                                            Dung = false;
                                                                                                                                            Plevel = "45";
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            Dung = true;
                                                                                                                                            Plevel = Plevel + "," + tableTVTF24.LevelThanhVien.ToString();
                                                                                                                                        }
                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                        {

                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                        }
                                                                                                                                        if (Dung == true)
                                                                                                                                        {
                                                                                                                                            if (TongLevel != "8")
                                                                                                                                            {
                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                {
                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F24", "55", IDThanhVien, tableTVTF24.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), TongLevel);
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F24", "55", IDThanhVien, tableTVTF24.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), "0");
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    catch (Exception)
                                                                                                                                    { }
                                                                                                                                    #endregion
                                                                                                                                }

                                                                                                                                #region Hoa Hồng Gián tiếp F25
                                                                                                                                user tableTVTF25 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF24.GioiThieu.ToString()));
                                                                                                                                if (tableTVTF25 != null)
                                                                                                                                {
                                                                                                                                    if (tableTVTF25.GioiThieu.ToString() != "0")
                                                                                                                                    {
                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                        try
                                                                                                                                        {
                                                                                                                                            if (tableTVTF25.LevelThanhVien.ToString() == "5")
                                                                                                                                            {
                                                                                                                                                Dung = false;
                                                                                                                                                Plevel = "45";
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                Dung = true;
                                                                                                                                                Plevel = Plevel + "," + tableTVTF25.LevelThanhVien.ToString();
                                                                                                                                            }
                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                            {

                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                            }
                                                                                                                                            if (Dung == true)
                                                                                                                                            {
                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                {
                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                    {
                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F25", "55", IDThanhVien, tableTVTF25.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), TongLevel);
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F25", "55", IDThanhVien, tableTVTF25.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), "0");
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        catch (Exception)
                                                                                                                                        { }
                                                                                                                                        #endregion
                                                                                                                                    }

                                                                                                                                    #region Hoa Hồng Gián tiếp F26
                                                                                                                                    user tableTVTF26 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF25.GioiThieu.ToString()));
                                                                                                                                    if (tableTVTF26 != null)
                                                                                                                                    {
                                                                                                                                        if (tableTVTF26.GioiThieu.ToString() != "0")
                                                                                                                                        {
                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                if (tableTVTF26.LevelThanhVien.ToString() == "5")
                                                                                                                                                {
                                                                                                                                                    Dung = false;
                                                                                                                                                    Plevel = "45";
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    Dung = true;
                                                                                                                                                    Plevel = Plevel + "," + tableTVTF26.LevelThanhVien.ToString();
                                                                                                                                                }
                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                {

                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                }
                                                                                                                                                if (Dung == true)
                                                                                                                                                {
                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                    {
                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                        {
                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F26", "55", IDThanhVien, tableTVTF26.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), TongLevel);
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F26", "55", IDThanhVien, tableTVTF26.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), "0");
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            catch (Exception)
                                                                                                                                            { }
                                                                                                                                            #endregion
                                                                                                                                        }

                                                                                                                                        #region Hoa Hồng Gián tiếp F27
                                                                                                                                        user tableTVTF27 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF26.GioiThieu.ToString()));
                                                                                                                                        if (tableTVTF27 != null)
                                                                                                                                        {
                                                                                                                                            if (tableTVTF27.GioiThieu.ToString() != "0")
                                                                                                                                            {
                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                try
                                                                                                                                                {
                                                                                                                                                    if (tableTVTF27.LevelThanhVien.ToString() == "5")
                                                                                                                                                    {
                                                                                                                                                        Dung = false;
                                                                                                                                                        Plevel = "45";
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        Dung = true;
                                                                                                                                                        Plevel = Plevel + "," + tableTVTF27.LevelThanhVien.ToString();
                                                                                                                                                    }
                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                    {

                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                    }
                                                                                                                                                    if (Dung == true)
                                                                                                                                                    {
                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                        {
                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                            {
                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F27", "55", IDThanhVien, tableTVTF27.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), TongLevel);
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F27", "55", IDThanhVien, tableTVTF27.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), "0");
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                catch (Exception)
                                                                                                                                                { }
                                                                                                                                                #endregion
                                                                                                                                            }

                                                                                                                                            #region Hoa Hồng Gián tiếp F28
                                                                                                                                            user tableTVTF28 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF27.GioiThieu.ToString()));
                                                                                                                                            if (tableTVTF28 != null)
                                                                                                                                            {
                                                                                                                                                if (tableTVTF28.GioiThieu.ToString() != "0")
                                                                                                                                                {
                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                    try
                                                                                                                                                    {
                                                                                                                                                        if (tableTVTF28.LevelThanhVien.ToString() == "5")
                                                                                                                                                        {
                                                                                                                                                            Dung = false;
                                                                                                                                                            Plevel = "45";
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            Dung = true;
                                                                                                                                                            Plevel = Plevel + "," + tableTVTF28.LevelThanhVien.ToString();
                                                                                                                                                        }
                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                        {

                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                        }
                                                                                                                                                        if (Dung == true)
                                                                                                                                                        {
                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                            {
                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                {
                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F28", "55", IDThanhVien, tableTVTF28.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F28", "55", IDThanhVien, tableTVTF28.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), "0");
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    catch (Exception)
                                                                                                                                                    { }
                                                                                                                                                    #endregion
                                                                                                                                                }
                                                                                                                                                ///88888888888888
                                                                                                                                                #region Hoa Hồng Gián tiếp F29
                                                                                                                                                user tableTVTF29 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF28.GioiThieu.ToString()));
                                                                                                                                                if (tableTVTF29 != null)
                                                                                                                                                {
                                                                                                                                                    if (tableTVTF29.GioiThieu.ToString() != "0")
                                                                                                                                                    {
                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                        try
                                                                                                                                                        {
                                                                                                                                                            if (tableTVTF29.LevelThanhVien.ToString() == "5")
                                                                                                                                                            {
                                                                                                                                                                Dung = false;
                                                                                                                                                                Plevel = "45";
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                Dung = true;
                                                                                                                                                                Plevel = Plevel + "," + tableTVTF29.LevelThanhVien.ToString();
                                                                                                                                                            }
                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                            {

                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                            }
                                                                                                                                                            if (Dung == true)
                                                                                                                                                            {
                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                {
                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                    {
                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F29", "55", IDThanhVien, tableTVTF29.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F29", "55", IDThanhVien, tableTVTF29.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), "0");
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        catch (Exception)
                                                                                                                                                        { }
                                                                                                                                                        #endregion
                                                                                                                                                    }

                                                                                                                                                    #region Hoa Hồng Gián tiếp F30
                                                                                                                                                    user tableTVTF30 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF29.GioiThieu.ToString()));
                                                                                                                                                    if (tableTVTF30 != null)
                                                                                                                                                    {
                                                                                                                                                        if (tableTVTF30.GioiThieu.ToString() != "0")
                                                                                                                                                        {
                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                            try
                                                                                                                                                            {
                                                                                                                                                                if (tableTVTF30.LevelThanhVien.ToString() == "5")
                                                                                                                                                                {
                                                                                                                                                                    Dung = false;
                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    Dung = true;
                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF30.LevelThanhVien.ToString();
                                                                                                                                                                }
                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                {

                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                }
                                                                                                                                                                if (Dung == true)
                                                                                                                                                                {
                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                    {
                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                        {
                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F30", "55", IDThanhVien, tableTVTF30.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F30", "55", IDThanhVien, tableTVTF30.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), "0");
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            catch (Exception)
                                                                                                                                                            { }
                                                                                                                                                            #endregion
                                                                                                                                                        }

                                                                                                                                                        #region Hoa Hồng Gián tiếp F31
                                                                                                                                                        user tableTVTF31 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF30.GioiThieu.ToString()));
                                                                                                                                                        if (tableTVTF31 != null)
                                                                                                                                                        {
                                                                                                                                                            if (tableTVTF31.GioiThieu.ToString() != "0")
                                                                                                                                                            {
                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                try
                                                                                                                                                                {
                                                                                                                                                                    if (tableTVTF31.LevelThanhVien.ToString() == "5")
                                                                                                                                                                    {
                                                                                                                                                                        Dung = false;
                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        Dung = true;
                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF31.LevelThanhVien.ToString();
                                                                                                                                                                    }
                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                    {

                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                    }
                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                    {
                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                        {
                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                            {
                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F31", "55", IDThanhVien, tableTVTF31.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F31", "55", IDThanhVien, tableTVTF31.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), "0");
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                catch (Exception)
                                                                                                                                                                { }
                                                                                                                                                                #endregion
                                                                                                                                                            }

                                                                                                                                                            #region Hoa Hồng Gián tiếp F32
                                                                                                                                                            user tableTVTF32 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF31.GioiThieu.ToString()));
                                                                                                                                                            if (tableTVTF32 != null)
                                                                                                                                                            {
                                                                                                                                                                if (tableTVTF32.GioiThieu.ToString() != "0")
                                                                                                                                                                {
                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                    try
                                                                                                                                                                    {
                                                                                                                                                                        if (tableTVTF32.LevelThanhVien.ToString() == "5")
                                                                                                                                                                        {
                                                                                                                                                                            Dung = false;
                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            Dung = true;
                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF32.LevelThanhVien.ToString();
                                                                                                                                                                        }
                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                        {

                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                        }
                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                        {
                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                            {
                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                {
                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F32", "55", IDThanhVien, tableTVTF32.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F32", "55", IDThanhVien, tableTVTF32.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), "0");
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    catch (Exception)
                                                                                                                                                                    { }
                                                                                                                                                                    #endregion
                                                                                                                                                                }

                                                                                                                                                                #region Hoa Hồng Gián tiếp F33
                                                                                                                                                                user tableTVTF33 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF32.GioiThieu.ToString()));
                                                                                                                                                                if (tableTVTF33 != null)
                                                                                                                                                                {
                                                                                                                                                                    if (tableTVTF33.GioiThieu.ToString() != "0")
                                                                                                                                                                    {
                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                        try
                                                                                                                                                                        {
                                                                                                                                                                            if (tableTVTF33.LevelThanhVien.ToString() == "5")
                                                                                                                                                                            {
                                                                                                                                                                                Dung = false;
                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                Dung = true;
                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF33.LevelThanhVien.ToString();
                                                                                                                                                                            }
                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                            {

                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                            }
                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                            {
                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                {
                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                    {
                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F33", "55", IDThanhVien, tableTVTF33.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F33", "55", IDThanhVien, tableTVTF33.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), "0");
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        catch (Exception)
                                                                                                                                                                        { }
                                                                                                                                                                        #endregion
                                                                                                                                                                    }
                                                                                                                                                                    //000000000000000000000
                                                                                                                                                                    #region Hoa Hồng Gián tiếp F34
                                                                                                                                                                    user tableTVTF34 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF33.GioiThieu.ToString()));
                                                                                                                                                                    if (tableTVTF34 != null)
                                                                                                                                                                    {
                                                                                                                                                                        if (tableTVTF34.GioiThieu.ToString() != "0")
                                                                                                                                                                        {
                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                            try
                                                                                                                                                                            {
                                                                                                                                                                                if (tableTVTF34.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                {
                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF34.LevelThanhVien.ToString();
                                                                                                                                                                                }
                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                {

                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                }
                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                {
                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                    {
                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                        {
                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F34", "55", IDThanhVien, tableTVTF34.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F34", "55", IDThanhVien, tableTVTF34.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), "0");
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            catch (Exception)
                                                                                                                                                                            { }
                                                                                                                                                                            #endregion
                                                                                                                                                                        }

                                                                                                                                                                        #region Hoa Hồng Gián tiếp F35
                                                                                                                                                                        user tableTVTF35 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF34.GioiThieu.ToString()));
                                                                                                                                                                        if (tableTVTF35 != null)
                                                                                                                                                                        {
                                                                                                                                                                            if (tableTVTF35.GioiThieu.ToString() != "0")
                                                                                                                                                                            {
                                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                try
                                                                                                                                                                                {
                                                                                                                                                                                    if (tableTVTF35.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                    {
                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF35.LevelThanhVien.ToString();
                                                                                                                                                                                    }
                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                    {

                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                    }
                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                    {
                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                        {
                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                            {
                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F35", "55", IDThanhVien, tableTVTF35.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F35", "55", IDThanhVien, tableTVTF35.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), "0");
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                { }
                                                                                                                                                                                #endregion
                                                                                                                                                                            }
                                                                                                                                                                            #region Hoa Hồng Gián tiếp F36
                                                                                                                                                                            user tableTVTF36 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF35.GioiThieu.ToString()));
                                                                                                                                                                            if (tableTVTF36 != null)
                                                                                                                                                                            {
                                                                                                                                                                                if (tableTVTF36.GioiThieu.ToString() != "0")
                                                                                                                                                                                {
                                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        if (tableTVTF36.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                        {
                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF36.LevelThanhVien.ToString();
                                                                                                                                                                                        }
                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                        {

                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                        }
                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                        {
                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                            {
                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                {
                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F36", "55", IDThanhVien, tableTVTF36.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F36", "55", IDThanhVien, tableTVTF36.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), "0");
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                    { }
                                                                                                                                                                                    #endregion
                                                                                                                                                                                }
                                                                                                                                                                                #region Hoa Hồng Gián tiếp F37
                                                                                                                                                                                user tableTVTF37 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF36.GioiThieu.ToString()));
                                                                                                                                                                                if (tableTVTF37 != null)
                                                                                                                                                                                {
                                                                                                                                                                                    if (tableTVTF37.GioiThieu.ToString() != "0")
                                                                                                                                                                                    {
                                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                        try
                                                                                                                                                                                        {
                                                                                                                                                                                            if (tableTVTF37.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                            {
                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF37.LevelThanhVien.ToString();
                                                                                                                                                                                            }
                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                            {

                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                            }
                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                            {
                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F37", "55", IDThanhVien, tableTVTF37.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F37", "55", IDThanhVien, tableTVTF37.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), "0");
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                        { }
                                                                                                                                                                                        #endregion
                                                                                                                                                                                    }

                                                                                                                                                                                    #region Hoa Hồng Gián tiếp F38
                                                                                                                                                                                    user tableTVTF38 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF37.GioiThieu.ToString()));
                                                                                                                                                                                    if (tableTVTF38 != null)
                                                                                                                                                                                    {
                                                                                                                                                                                        if (tableTVTF38.GioiThieu.ToString() != "0")
                                                                                                                                                                                        {
                                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                            try
                                                                                                                                                                                            {
                                                                                                                                                                                                if (tableTVTF38.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                {
                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF38.LevelThanhVien.ToString();
                                                                                                                                                                                                }
                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                {

                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                }
                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F38", "55", IDThanhVien, tableTVTF38.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F38", "55", IDThanhVien, tableTVTF38.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), "0");
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                            { }
                                                                                                                                                                                            #endregion
                                                                                                                                                                                        }
                                                                                                                                                                                        #region Hoa Hồng Gián tiếp F39
                                                                                                                                                                                        user tableTVTF39 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF38.GioiThieu.ToString()));
                                                                                                                                                                                        if (tableTVTF39 != null)
                                                                                                                                                                                        {
                                                                                                                                                                                            if (tableTVTF39.GioiThieu.ToString() != "0")
                                                                                                                                                                                            {
                                                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                try
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (tableTVTF39.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF39.LevelThanhVien.ToString();
                                                                                                                                                                                                    }
                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                    {

                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                    }
                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F39", "55", IDThanhVien, tableTVTF39.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F39", "55", IDThanhVien, tableTVTF39.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), "0");
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                { }
                                                                                                                                                                                                #endregion
                                                                                                                                                                                            }
                                                                                                                                                                                            #region Hoa Hồng Gián tiếp F40
                                                                                                                                                                                            user tableTVTF40 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF39.GioiThieu.ToString()));
                                                                                                                                                                                            if (tableTVTF40 != null)
                                                                                                                                                                                            {
                                                                                                                                                                                                if (tableTVTF40.GioiThieu.ToString() != "0")
                                                                                                                                                                                                {
                                                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                    try
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (tableTVTF40.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF40.LevelThanhVien.ToString();
                                                                                                                                                                                                        }
                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                        {

                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                        }
                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F40", "55", IDThanhVien, tableTVTF40.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F40", "55", IDThanhVien, tableTVTF40.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), "0");
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                    { }
                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                }
                                                                                                                                                                                                #region Hoa Hồng Gián tiếp F41
                                                                                                                                                                                                user tableTVTF41 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF40.GioiThieu.ToString()));
                                                                                                                                                                                                if (tableTVTF41 != null)
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (tableTVTF41.GioiThieu.ToString() != "0")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                        try
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (tableTVTF41.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF41.LevelThanhVien.ToString();
                                                                                                                                                                                                            }
                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                            {

                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                            }
                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F41", "55", IDThanhVien, tableTVTF41.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F41", "55", IDThanhVien, tableTVTF41.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                        { }
                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                    }
                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp F42
                                                                                                                                                                                                    user tableTVTF42 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF41.GioiThieu.ToString()));
                                                                                                                                                                                                    if (tableTVTF42 != null)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (tableTVTF42.GioiThieu.ToString() != "0")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                            try
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (tableTVTF42.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF42.LevelThanhVien.ToString();
                                                                                                                                                                                                                }
                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                {

                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                }
                                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F42", "55", IDThanhVien, tableTVTF42.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F42", "55", IDThanhVien, tableTVTF42.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                            { }
                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                        }

                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp F43
                                                                                                                                                                                                        user tableTVTF43 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF42.GioiThieu.ToString()));
                                                                                                                                                                                                        if (tableTVTF43 != null)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (tableTVTF43.GioiThieu.ToString() != "0")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                try
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (tableTVTF43.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF43.LevelThanhVien.ToString();
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                    {

                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F43", "55", IDThanhVien, tableTVTF43.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F43", "55", IDThanhVien, tableTVTF43.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                { }
                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                            }
                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp F44
                                                                                                                                                                                                            user tableTVTF44 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF43.GioiThieu.ToString()));
                                                                                                                                                                                                            if (tableTVTF44 != null)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (tableTVTF44.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                    try
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (tableTVTF44.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF44.LevelThanhVien.ToString();
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                        {

                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F44", "55", IDThanhVien, tableTVTF44.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F44", "55", IDThanhVien, tableTVTF44.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                    { }
                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                }

                                                                                                                                                                                                                #region Hoa Hồng Gián tiếp F45
                                                                                                                                                                                                                user tableTVTF45 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF44.GioiThieu.ToString()));
                                                                                                                                                                                                                if (tableTVTF45 != null)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (tableTVTF45.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                        try
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (tableTVTF45.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF45.LevelThanhVien.ToString();
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                            {

                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F45", "55", IDThanhVien, tableTVTF45.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F45", "55", IDThanhVien, tableTVTF45.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                                        { }
                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp F46
                                                                                                                                                                                                                    user tableTVTF46 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF45.GioiThieu.ToString()));
                                                                                                                                                                                                                    if (tableTVTF46 != null)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (tableTVTF46.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                            try
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (tableTVTF46.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF46.LevelThanhVien.ToString();
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                {

                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F46", "55", IDThanhVien, tableTVTF46.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F46", "55", IDThanhVien, tableTVTF46.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                                            { }
                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp F47
                                                                                                                                                                                                                        user tableTVTF47 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF46.GioiThieu.ToString()));
                                                                                                                                                                                                                        if (tableTVTF47 != null)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (tableTVTF47.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                try
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (tableTVTF47.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF47.LevelThanhVien.ToString();
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                    {

                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F47", "55", IDThanhVien, tableTVTF47.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F47", "55", IDThanhVien, tableTVTF47.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                                { }
                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp F48
                                                                                                                                                                                                                            user tableTVTF48 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF47.GioiThieu.ToString()));
                                                                                                                                                                                                                            if (tableTVTF48 != null)
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (tableTVTF48.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                    try
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (tableTVTF48.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF48.LevelThanhVien.ToString();
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                        {

                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F48", "55", IDThanhVien, tableTVTF48.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F48", "55", IDThanhVien, tableTVTF48.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                                    { }
                                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                                }

                                                                                                                                                                                                                                #region Hoa Hồng Gián tiếp F49
                                                                                                                                                                                                                                user tableTVTF49 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF48.GioiThieu.ToString()));
                                                                                                                                                                                                                                if (tableTVTF49 != null)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (tableTVTF49.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                        try
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (tableTVTF49.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF49.LevelThanhVien.ToString();
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                            {

                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F49", "55", IDThanhVien, tableTVTF49.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F49", "55", IDThanhVien, tableTVTF49.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                                                        { }
                                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                                    }

                                                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp F50
                                                                                                                                                                                                                                    user tableTVTF50 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF49.GioiThieu.ToString()));
                                                                                                                                                                                                                                    if (tableTVTF50 != null)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (tableTVTF50.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                            try
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (tableTVTF50.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF50.LevelThanhVien.ToString();
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                                {

                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F50", "55", IDThanhVien, tableTVTF50.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), TongLevel);
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[0].ipid.ToString(), "F50", "55", IDThanhVien, tableTVTF50.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), "0");
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                                                            { }
                                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                }
                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                            }
                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                        }
                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                    }
                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                }
                                                                                                                                                                                                #endregion
                                                                                                                                                                                            }
                                                                                                                                                                                            #endregion
                                                                                                                                                                                        }
                                                                                                                                                                                        #endregion
                                                                                                                                                                                    }
                                                                                                                                                                                    #endregion
                                                                                                                                                                                }
                                                                                                                                                                                #endregion

                                                                                                                                                                            }
                                                                                                                                                                            #endregion
                                                                                                                                                                        }
                                                                                                                                                                        #endregion

                                                                                                                                                                    }
                                                                                                                                                                    #endregion
                                                                                                                                                                }
                                                                                                                                                                #endregion
                                                                                                                                                            }
                                                                                                                                                            #endregion
                                                                                                                                                        }
                                                                                                                                                        #endregion
                                                                                                                                                    }
                                                                                                                                                    #endregion
                                                                                                                                                }
                                                                                                                                                #endregion
                                                                                                                                            }
                                                                                                                                            #endregion
                                                                                                                                        }
                                                                                                                                        #endregion
                                                                                                                                    }
                                                                                                                                    #endregion
                                                                                                                                }
                                                                                                                                #endregion
                                                                                                                            }
                                                                                                                            #endregion
                                                                                                                        }
                                                                                                                        #endregion
                                                                                                                    }
                                                                                                                    #endregion
                                                                                                                }
                                                                                                                #endregion
                                                                                                            }
                                                                                                            #endregion
                                                                                                        }
                                                                                                        #endregion
                                                                                                    }
                                                                                                    #endregion
                                                                                                }
                                                                                                #endregion
                                                                                            }
                                                                                            #endregion
                                                                                        }
                                                                                        #endregion
                                                                                    }
                                                                                    #endregion
                                                                                }
                                                                                #endregion
                                                                            }
                                                                            #endregion
                                                                        }
                                                                        #endregion
                                                                    }
                                                                    #endregion
                                                                }
                                                                #endregion
                                                            }
                                                            #endregion
                                                        }
                                                        #endregion


                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #endregion
                                }
                            }
                            #endregion

                            
                        }
                        #endregion

                        #region Tính hoa hồng Nhà Cung Cấp Và Chi Nhánh cho kiểu sản phẩm (TrangThaiAgLang=1)
                        if (TrangThaiAgLang == "1")
                        {
                            #region Mua hàng
                            #region Chi nhánh được hưởng 10 khi có giao dịch mua%
                            // Đối với chi nhánh hưởng 10 mua hàng%
                            if (!table.IDChiNhanh.Equals("0"))
                            {
                                try
                                {
                                    if (ShowIDChiNhanh(table.IDChiNhanh.ToString()) != "0")
                                    {
                                        double HoaHongChiNhanhMuaHang = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongChiNhanhMuaHang"));

                                        double TongCoinChiNhanh = (TongCoin * HoaHongChiNhanhMuaHang) / 100;
                                        //Ngày 10/02/2020 anh Đào sang trao đổi rào lại va chỉ cho hoa hồng rơi =0
                                        //ThemHoaHong(dtcart[0].ipid.ToString(), "9", "Hoa Hồng (Chi Nhánh Mua Hàng) 10%", table.iuser_id.ToString(), ShowIDChiNhanh(table.IDChiNhanh.ToString()),HoaHongChiNhanhMuaHang.ToString(), TongCoinChiNhanh.ToString());
                                        ThemHoaHong(dtcart[0].ipid.ToString(), "9", "Hoa Hồng (Chi Nhánh Mua Hàng) " + HoaHongChiNhanhMuaHang + "%", table.iuser_id.ToString(), ShowIDChiNhanh(table.IDChiNhanh.ToString()), HoaHongChiNhanhMuaHang.ToString(), TongCoinChiNhanh.ToString());
                                    }
                                }
                                catch (Exception)
                                { }
                            }
                            #endregion


                        


                            #region Hoa Hồng Leader cho người giới thiệu mua hàng
                            if (!TimLeader(table.GioiThieu).Equals("0"))
                            {
                                try
                                {
                                    double HoaHongLeaderMuaHang = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongLeaderMuaHang"));
                                    if (!MoreAll.Other.Giatri("HoaHongLeaderMuaHang").Equals("0"))
                                    {
                                        double TongLeader = (TongCoin * HoaHongLeaderMuaHang) / 100;
                                        ThemHoaHong(dtcart[0].ipid.ToString(), "13", "Hoa Hồng (Leader - Mua Hàng)", table.iuser_id.ToString(), TimLeader(table.GioiThieu), HoaHongLeaderMuaHang.ToString(), TongLeader.ToString());
                                    }
                                }
                                catch (Exception)
                                { }
                            }
                            #endregion
                            #endregion

                            #region TH: Đối với người cấp dưới bán hàng
                            //TH: Đối với người cấp dưới bán hàng
                            // Đi tìm sản phẩm đăng lên bán này là ai giới thiệu và cho ng giới thiệu %
                            // Đi tìm nhà cung A đã giới thiệu cho B
                            string IDNhaCungCap = ShowNhaCungCap(dtcart[0].ipid.ToString());
                            if (IDNhaCungCap != "0")
                            {
                                //Thành viên giới thiệu A bán hàng dc hưởng 1%
                                #region Thành viên và leader
                                user Cungcap = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDNhaCungCap));//&& p.Type == 2//&& p.ChiNhanh == 0// Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                                if (Cungcap != null)
                                {
                                    #region ******* đang để là trực tiếp là 10%
                                    //B là nhà cung cấp được A thành viên giới thiệu
                                    //kiểm tra b (Kiểm tra sản phẩm này có đúng là B đăng lên bán ko ?) xem có phải là nhà cung cấp ko ,nếu đúng lúc bán hàng thì cho họ %
                                    // Thưởng cho A 1%
                                    try
                                    {
                                        double HoaHongGioiThieuTrucTiepNhaCungCap = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuTrucTiepNhaCungCap"));
                                        double TongCoinCC = (TongCoin) * (HoaHongGioiThieuTrucTiepNhaCungCap) / 100;
                                        if (Cungcap.GioiThieu.ToString() != "0")
                                        {
                                            ThemHoaHong(dtcart[0].ipid.ToString(), "10", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) Trực Tiếp", table.iuser_id.ToString(), Cungcap.GioiThieu.ToString(), HoaHongGioiThieuTrucTiepNhaCungCap.ToString(), TongCoinCC.ToString());
                                        }
                                        //#region Chia Thu Nhập 5F cho Nhà cung cấp
                                        //user NCCFF1 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(Cungcap.GioiThieu.ToString()));
                                        //if (NCCFF1 != null)
                                        //{
                                        //    double HHNCCFF1 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                        //    double HoaHongNCCF1 = (TongCoinCC * HHNCCFF1) / 100;
                                        //    if (NCCFF1.GioiThieu.ToString() != "0")
                                        //    {
                                        //        ThemHoaHong(dtcart[0].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F1", table.iuser_id.ToString(), NCCFF1.GioiThieu.ToString(), HHNCCFF1.ToString(), HoaHongNCCF1.ToString());
                                        //    }
                                        //    user NCCFF2 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(NCCFF1.GioiThieu.ToString()));
                                        //    if (NCCFF2 != null)
                                        //    {
                                        //        double HHNCCFF2 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                        //        double HoaHongNCCF2 = (HoaHongNCCF1 * HHNCCFF2) / 100;
                                        //        if (NCCFF1.GioiThieu.ToString() != "0")
                                        //        {
                                        //            ThemHoaHong(dtcart[0].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F2", table.iuser_id.ToString(), NCCFF2.GioiThieu.ToString(), HHNCCFF2.ToString(), HoaHongNCCF2.ToString());
                                        //        }
                                        //        user NCCFF3 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(NCCFF2.GioiThieu.ToString()));
                                        //        if (NCCFF3 != null)
                                        //        {
                                        //            double HHNCCFF3 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                        //            double HoaHongNCCF3 = (HoaHongNCCF2 * HHNCCFF3) / 100;
                                        //            if (NCCFF3.GioiThieu.ToString() != "0")
                                        //            {
                                        //                ThemHoaHong(dtcart[0].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F3", table.iuser_id.ToString(), NCCFF3.GioiThieu.ToString(), HHNCCFF3.ToString(), HoaHongNCCF3.ToString());
                                        //            }
                                        //            user NCCFF4 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(NCCFF3.GioiThieu.ToString()));
                                        //            if (NCCFF4 != null)
                                        //            {
                                        //                double HHNCCFF4 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                        //                double HoaHongNCCF4 = (HoaHongNCCF3 * HHNCCFF4) / 100;
                                        //                if (NCCFF4.GioiThieu.ToString() != "0")
                                        //                {
                                        //                    ThemHoaHong(dtcart[0].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F4", table.iuser_id.ToString(), NCCFF4.GioiThieu.ToString(), HHNCCFF4.ToString(), HoaHongNCCF4.ToString());
                                        //                }
                                        //                user NCCFF5 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(NCCFF4.GioiThieu.ToString()));
                                        //                if (NCCFF5 != null)
                                        //                {
                                        //                    double HHNCCFF5 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                        //                    double HoaHongNCCF5 = (HoaHongNCCF4 * HHNCCFF5) / 100;
                                        //                    if (NCCFF5.GioiThieu.ToString() != "0")
                                        //                    {
                                        //                        ThemHoaHong(dtcart[0].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F5", table.iuser_id.ToString(), NCCFF5.GioiThieu.ToString(), HHNCCFF5.ToString(), HoaHongNCCF5.ToString());
                                        //                    }
                                        //                }
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                        //#endregion
                                    }
                                    catch (Exception)
                                    { }
                                    #endregion



                                }
                                #endregion


                                #region Đối với chi nhánh
                                // ?? có cần DK Tìm nhà cung cấp hay ko (Type == 2) ? hay chỉ cần người giới thiệu là có %
                                // Tìm nhà chi nhánh
                                user Cungcapchinhanh = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDNhaCungCap));//// && p.ChiNhanh == 1  --- Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                                if (Cungcapchinhanh != null)
                                {
                                    try
                                    {
                                        if (ShowIDChiNhanh(Cungcapchinhanh.IDChiNhanh.ToString()) != "0")
                                        {
                                            double HoaHongChiNhanhBanHang = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongChiNhanhBanHang"));
                                            if (HoaHongChiNhanhBanHang > 0)
                                            {
                                                double TongCoinChinhanh = (TongCoin * HoaHongChiNhanhBanHang) / 100;
                                                ThemHoaHong(dtcart[0].ipid.ToString(), "12", "Hoa Hồng (Chi Nhánh Bán Hàng)", table.iuser_id.ToString(), ShowIDChiNhanh(Cungcapchinhanh.IDChiNhanh.ToString()), HoaHongChiNhanhBanHang.ToString(), TongCoinChinhanh.ToString());
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion

                        
                        #endregion

                        if (TrangThaiAgLang == "1")
                        {
                            #region Vi Loi Nhuan sau khi da chia HH
                            try
                            {
                                var tongdiemdachia = db.S_TongDiemDaChia(int.Parse(IDGioHang.ToString()), int.Parse(dtcart[0].ipid.ToString())).ToList();
                                if (tongdiemdachia[0].sodiem >= 0)
                                {
                                    Double TongDaChia = Convert.ToDouble(tongdiemdachia[0].sodiem.ToString());
                                    Double TongCongs = Diemcoin - TongDaChia;
                                    //string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
                                    //if (LogFile == "true")
                                    //{
                                    //    Library.WriteErrorLogTongThanhToan("ipid:" + dtcart[0].ipid.ToString());
                                    //    Library.WriteErrorLogTongThanhToan(" Diemcoin  " + Diemcoin + "TongDaChia  " + TongDaChia + " TongCongs  " + TongCongs);
                                    //}
                                    LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                                    abln.IDThanhVienMua = int.Parse(dtcart[0].IDThanhVien.ToString());
                                    abln.IDThanhVienBan = int.Parse(dtcart[0].IDNhaCungCap.ToString());
                                    abln.IDDonHang = int.Parse(IDGioHang.ToString());
                                    abln.IDSanPham = int.Parse(dtcart[0].ipid.ToString());
                                    abln.MoTa = Commond.ShowPro(dtcart[0].ipid.ToString());
                                    abln.NgayTao = DateTime.Now;
                                    abln.SoDiemGoc = Diemcoin.ToString();
                                    abln.SoDiemConLai = TongCongs.ToString();
                                    abln.SoDiemDaChia = TongDaChia.ToString();

                                    abln.MTreeIDThanhVienMua = Commond.ShowMTree(dtcart[0].IDThanhVien.ToString());
                                    abln.MTReIDThanhVienBan = Commond.ShowMTree(dtcart[0].IDNhaCungCap.ToString());

                                    abln.SoTienNhaCCBan = SoTienNhaCCBan;
                                    abln.SoTienDaiLyMua = SoTienDaiLyMua;
                                    abln.TienLayOViNao = Convert.ToInt32(TienLayOViNao);
                                    abln.SoLuong = int.Parse(dtcart[0].Quantity.ToString());

                                    db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    Double TongCongs = Diemcoin;
                                    //string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
                                    //if (LogFile == "true")
                                    //{
                                    //    Library.WriteErrorLogTongThanhToan("ipid:" + dtcart[0].ipid.ToString());
                                    //    Library.WriteErrorLogTongThanhToan(" Diemcoin  " + Diemcoin + "TongDaChia  " + TongDaChia + " TongCongs  " + TongCongs);
                                    //}
                                    LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                                    abln.IDThanhVienMua = int.Parse(dtcart[0].IDThanhVien.ToString());
                                    abln.IDThanhVienBan = int.Parse(dtcart[0].IDNhaCungCap.ToString());
                                    abln.IDDonHang = int.Parse(IDGioHang.ToString());
                                    abln.IDSanPham = int.Parse(dtcart[0].ipid.ToString());
                                    abln.MoTa = Commond.ShowPro(dtcart[0].ipid.ToString());
                                    abln.NgayTao = DateTime.Now;
                                    abln.SoDiemGoc = Diemcoin.ToString();
                                    abln.SoDiemConLai = TongCongs.ToString();
                                    abln.SoDiemDaChia = "0";

                                    abln.MTreeIDThanhVienMua = Commond.ShowMTree(dtcart[0].IDThanhVien.ToString());
                                    abln.MTReIDThanhVienBan = Commond.ShowMTree(dtcart[0].IDNhaCungCap.ToString());

                                    abln.SoTienNhaCCBan = SoTienNhaCCBan;
                                    abln.SoTienDaiLyMua = SoTienDaiLyMua;
                                    abln.TienLayOViNao = Convert.ToInt32(TienLayOViNao);
                                    abln.SoLuong = int.Parse(dtcart[0].Quantity.ToString());

                                    db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                                    db.SubmitChanges();
                                }
                            }
                            catch (Exception)
                            { }
                            #endregion
                        }

                        if (TrangThaiAgLang == "2") // Đối với bất động sản thì phải chia thêm 2% hoa hồng đầu tư để cho chuyên gia.
                        {
                            //// AGland
                            //double DauVao = Convert.ToDouble("12000000");
                            //double PhanTram = Convert.ToDouble("2");
                            //double ThanhTien = (DauVao * PhanTram) / 100;
                            //Response.Write(ThanhTien);

                            //#region ViHoaHongChuyenGia
                            //double TongDiemCanChia = Convert.ToDouble(dtcart[0].Money.ToString());
                            //double PhanTram = Convert.ToDouble(Commond.Setting("txtAGLandChuyengia"));
                            //double ThanhTien = (TongDiemCanChia * PhanTram) / 100; // VD: 2400*0,02=24
                            //double TongThanhTien = (ThanhTien) / 1000;
                            //// ví chuyên gia AFF
                            //ViHoaHongChuyenGia obp = new ViHoaHongChuyenGia();
                            //obp.IDDonHang = Convert.ToInt64(IDGioHang.ToString());
                            //obp.IDThanhVien = Convert.ToInt64(Commond.SetThanhVienChuyenGia());
                            //obp.IDThanhVienMua_KichHoat = Convert.ToInt64(dtcart[0].IDThanhVien.ToString());//Thành viên mua hàng
                            //obp.TongDiem = TongThanhTien.ToString();
                            //obp.LoaiHoaHong = 2; //1: Hoa hồng AGland
                            //obp.NgayTao = DateTime.Now;
                            //obp.PhanTram = int.Parse(Commond.Setting("txtAGLandChuyengia").ToString());
                            //db.ViHoaHongChuyenGias.InsertOnSubmit(obp);
                            //db.SubmitChanges();
                            //#endregion

                            #region Vi Loi Nhuan sau khi da chia HH
                            try
                            {
                                var tongdiemdachia = db.S_TongDiemDaChia(int.Parse(IDGioHang.ToString()), int.Parse(dtcart[0].ipid.ToString())).ToList();
                                if (tongdiemdachia[0].sodiem >= 0)
                                {
                                    Double TongDaChia = Convert.ToDouble(tongdiemdachia[0].sodiem.ToString());
                                    Double TongDaChiaConlai = TongDaChia;// +TongThanhTien;
                                    Double TongCongs = Diemcoin - TongDaChiaConlai;


                                    Double MoneyMua = Convert.ToDouble(dtcart[0].Money.ToString());
                                    Double TongSoTienDaiLyMua = (MoneyMua / 1000);

                                    Double TongSoMua = Convert.ToDouble(GiaNhap(dtcart[0].ipid.ToString(), dtcart[0].Quantity.ToString()));
                                    Double TongSoTienNhaCCBan = (TongSoMua / 1000);

                                    LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                                    abln.IDThanhVienMua = int.Parse(dtcart[0].IDThanhVien.ToString());
                                    abln.IDThanhVienBan = int.Parse(dtcart[0].IDNhaCungCap.ToString());
                                    abln.IDDonHang = int.Parse(IDGioHang.ToString());
                                    abln.IDSanPham = int.Parse(dtcart[0].ipid.ToString());
                                    abln.MoTa = Commond.ShowPro(dtcart[0].ipid.ToString());
                                    abln.NgayTao = DateTime.Now;
                                    abln.SoDiemGoc = Diemcoin.ToString();
                                    abln.SoDiemConLai = TongCongs.ToString();
                                    abln.SoDiemDaChia = TongDaChiaConlai.ToString();
                                    abln.MTreeIDThanhVienMua = Commond.ShowMTree(dtcart[0].IDThanhVien.ToString());
                                    abln.MTReIDThanhVienBan = Commond.ShowMTree(dtcart[0].IDNhaCungCap.ToString());

                                    abln.SoTienNhaCCBan = SoTienNhaCCBan;
                                    abln.SoTienDaiLyMua = SoTienDaiLyMua;
                                    abln.TienLayOViNao = Convert.ToInt32(TienLayOViNao);
                                    abln.SoLuong = int.Parse(dtcart[0].Quantity.ToString());
                                    db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                                    db.SubmitChanges();
                                }
                                //else
                                //{
                                //    Double TongDaChiaConlai =  TongThanhTien;
                                //    Double TongCongs = Diemcoin - TongDaChiaConlai;


                                //    Double MoneyMua = Convert.ToDouble(dtcart[0].Money.ToString());
                                //    Double TongSoTienDaiLyMua = (MoneyMua / 1000);

                                //    Double TongSoMua = Convert.ToDouble(GiaNhap(dtcart[0].ipid.ToString(), dtcart[0].Quantity.ToString()));
                                //    Double TongSoTienNhaCCBan = (TongSoMua / 1000);

                                //    LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                                //    abln.IDThanhVienMua = int.Parse(dtcart[0].IDThanhVien.ToString());
                                //    abln.IDThanhVienBan = int.Parse(dtcart[0].IDNhaCungCap.ToString());
                                //    abln.IDDonHang = int.Parse(IDGioHang.ToString());
                                //    abln.IDSanPham = int.Parse(dtcart[0].ipid.ToString());
                                //    abln.MoTa = Commond.ShowPro(dtcart[0].ipid.ToString());
                                //    abln.NgayTao = DateTime.Now;
                                //    abln.SoDiemGoc = Diemcoin.ToString();
                                //    abln.SoDiemConLai = TongCongs.ToString();
                                //    abln.SoDiemDaChia = TongDaChiaConlai.ToString();
                                //    abln.MTreeIDThanhVienMua = Commond.ShowMTree(dtcart[0].IDThanhVien.ToString());
                                //    abln.MTReIDThanhVienBan = Commond.ShowMTree(dtcart[0].IDNhaCungCap.ToString());

                                //    abln.SoTienNhaCCBan = SoTienNhaCCBan;
                                //    abln.SoTienDaiLyMua = SoTienDaiLyMua;
                                //    abln.TienLayOViNao = Convert.ToInt32(TienLayOViNao);
                                //    abln.SoLuong = int.Parse(dtcart[0].Quantity.ToString());
                                //    db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                                //    db.SubmitChanges();
                                //}
                            }
                            catch (Exception)
                            { }

                            #endregion

                        }
                    }
                    #region Hoa hồng Lãi suất của AGLANG
                    //Lưu vào bảng Lãi suất AGLANG
                    //Lưu vào bảng Service AGLANG theo từng sản phẩm --> sau đó chạy service chạy theo từng ngày để phát sinh ra số tiền lãi và trả theo ngày
                    //Công tiền lãi suất vào ví thương mại
                    if (TrangThaiAgLang == "2")
                    {
                        //Nếu gói có số tiền <=200 triệu thì sẽ có lãi suất là 28%/ Năm
                        //KieuPhatSinhGiaoDich: =1 là đặt hàng , =2 là chạy Service_LaiSuatAGLANG
                        double GoiDauTu = Convert.ToDouble(dtcart[0].Money.ToString());
                        double SoTienDauTu = Convert.ToDouble("200000000");
                        if (GoiDauTu <= SoTienDauTu)
                        {
                            double HaiTamPhanTram = Convert.ToDouble(MoreAll.Other.Giatri("HaiTamPhanTram"));
                            double ChiaHH = (HaiTamPhanTram) / 100;

                            double DauVao1 = Convert.ToDouble(ChiaHH);// VD: 0.28
                            double DauVao2 = Convert.ToDouble("365");
                            // double DauVao = Convert.ToDouble("200000000");

                            Double HoaHong2 = (DauVao1 / DauVao2);
                            //Response.Write(HoaHong2.ToString() + "<br>");
                            Double HoaHong1 = (HoaHong2) * GoiDauTu;
                            // Response.Write(HoaHong1.ToString() + "<br>");
                            Double HoaHong3 = (HoaHong1) / 1000;

                            #region Thêm vào Bảng ChuyenDiemThanhVien
                            LaiSuatAGLANG bds = new LaiSuatAGLANG();
                            bds.IDSanPham = int.Parse(dtcart[0].ipid.ToString());
                            bds.IDThanhVienBan = int.Parse(IDNhaCungCapBanHang);
                            bds.IDThanhVienHuongHH = int.Parse(dtcart[0].IDThanhVien.ToString());// Người được hưởng chính là nhà đầu tư
                            //bds.IDThanhVienHuongHH = int.Parse(IDNhaCungCapBanHang);
                            bds.LaiSuat = HoaHong3.ToString();
                            bds.NgayNhan = DateTime.Now;
                            bds.SoTienDauTu = dtcart[0].Money.ToString();
                            bds.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                            bds.TrangThai = 1;
                            bds.KieuPhatSinhGiaoDich = 1;// 1, đặt hàng, 2= chạy Service
                            bds.KieuLaiSuat = 1;// kiểu lãi suất chính là 28% theo tháng hay 32%  theo quý
                            bds.NgayThamGia = DateTime.Now;
                            bds.IDCart = int.Parse(IDGioHang.ToString());
                            bds.NoiDung = Commond.ShowPro(dtcart[0].ipid.ToString());
                            bds.IDGioiThieuTrucTiep = Convert.ToInt64(TimF1Agland);
                            bds.MTreeHuong = Commond.ShowMTrees(dtcart[0].IDThanhVien.ToString());
                            db.LaiSuatAGLANGs.InsertOnSubmit(bds);
                            db.SubmitChanges();
                            #endregion

                            #region Lưu vào bảng Service_LaiSuatAGLANG
                            LaiSuatAGLANG tbn = db.LaiSuatAGLANGs.Where(s => s.TrangThai == 1).OrderByDescending(s => s.ID).FirstOrDefault();
                            string LaiSuatID = tbn.ID.ToString();

                            Service_LaiSuatAGLANG svbds = new Service_LaiSuatAGLANG();
                            svbds.IDLaiSuatAGLANG = int.Parse(LaiSuatID);
                            svbds.IDSanPham = int.Parse(dtcart[0].ipid.ToString());
                            svbds.IDThanhVienBan = int.Parse(IDNhaCungCapBanHang);
                            svbds.IDThanhVienHuongHH = int.Parse(dtcart[0].IDThanhVien.ToString());// Người được hưởng chính là nhà đầu tư
                            //svbds.IDThanhVienHuongHH = int.Parse(IDNhaCungCapBanHang);
                            svbds.LaiSuat = HoaHong3.ToString();
                            svbds.NgayNhan = DateTime.Now;
                            svbds.SoTienDauTu = dtcart[0].Money.ToString();
                            svbds.KieuLaiSuat = 1;// kiểu lãi suất chính là 28% theo tháng hay 32%  theo quý
                            svbds.SoNgayDaChay = 1;//Tổng sẽ chạy = 365 ngày, // Ngày đầu tiên khi duyệt đơn hàng sẽ được tính là 1 ngày
                            svbds.NgayThamGia = DateTime.Now;
                            svbds.IDCart = int.Parse(IDGioHang.ToString());
                            svbds.NoiDung = Commond.ShowPro(dtcart[0].ipid.ToString());
                            svbds.IDGioiThieuTrucTiep = Convert.ToInt64(TimF1Agland);
                            svbds.MTreeHuong = Commond.ShowMTrees(dtcart[0].IDThanhVien.ToString());
                            db.Service_LaiSuatAGLANGs.InsertOnSubmit(svbds);
                            db.SubmitChanges();

                            // CongDiemAgLand(dtcart[0].IDThanhVien.ToString(), HoaHong3.ToString());

                            CongDiemAgLand(dtcart[0].ipid.ToString(), "80", "Lãi suất AG LAND - Đặt Hàng", IDNhaCungCapBanHang, dtcart[0].IDThanhVien.ToString(), HoaHong3.ToString(), GoiDauTu.ToString(), TimF1Agland, IDGioHang.ToString());

                            //Lưu vào ví AGLAng ở bảng thành viên
                            //Cộng điểm vào Ví thương mại

                            // Lưu ý : khi viết Service_LaiSuatAGLANG phải Loại bỏ ngày đặt hàng này ra vì hôm đặt hàng là đã rơi hoa hồng ngày hôm đó rồi ,thì được tính là 1 ngày ở SoNgayDaChay...
                            #endregion
                        }
                        else if (GoiDauTu >= SoTienDauTu)
                        {
                            double BaHaiPhanTram = Convert.ToDouble(MoreAll.Other.Giatri("BaHaiPhanTram"));
                            double ChiaHH = (BaHaiPhanTram) / 100;

                            double DauVao1 = Convert.ToDouble(ChiaHH);// VD: "0.32"
                            double DauVao2 = Convert.ToDouble("365");
                            // double DauVao = Convert.ToDouble("200000000");

                            Double HoaHong2 = (DauVao1 / DauVao2);
                            //Response.Write(HoaHong2.ToString() + "<br>");
                            Double HoaHong1 = (HoaHong2) * GoiDauTu;
                            // Response.Write(HoaHong1.ToString() + "<br>");
                            Double HoaHong3 = (HoaHong1) / 1000;

                            #region Thêm vào Bảng ChuyenDiemThanhVien
                            LaiSuatAGLANG bds = new LaiSuatAGLANG();
                            bds.IDSanPham = int.Parse(dtcart[0].ipid.ToString());
                            bds.IDThanhVienBan = int.Parse(IDNhaCungCapBanHang);
                            bds.IDThanhVienHuongHH = int.Parse(dtcart[0].IDThanhVien.ToString());// Người được hưởng chính là nhà đầu tư
                            //bds.IDThanhVienHuongHH = int.Parse(IDNhaCungCapBanHang);
                            bds.LaiSuat = HoaHong3.ToString();
                            bds.NgayNhan = DateTime.Now;
                            bds.SoTienDauTu = dtcart[0].Money.ToString();
                            bds.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                            bds.TrangThai = 1;
                            bds.KieuPhatSinhGiaoDich = 1;// 1, đặt hàng, 2= chạy Service
                            bds.KieuLaiSuat = 2;// kiểu lãi suất chính là 28% theo tháng hay 32%  theo quý
                            bds.NgayThamGia = DateTime.Now;
                            bds.IDCart = int.Parse(IDGioHang.ToString());
                            bds.NoiDung = Commond.ShowPro(dtcart[0].ipid.ToString());
                            bds.IDGioiThieuTrucTiep = Convert.ToInt64(TimF1Agland);
                            bds.MTreeHuong = Commond.ShowMTrees(dtcart[0].IDThanhVien.ToString());
                            db.LaiSuatAGLANGs.InsertOnSubmit(bds);
                            db.SubmitChanges();
                            #endregion

                            #region Lưu vào bảng Service_LaiSuatAGLANG
                            LaiSuatAGLANG tbn = db.LaiSuatAGLANGs.Where(s => s.TrangThai == 1).OrderByDescending(s => s.ID).FirstOrDefault();
                            string LaiSuatID = tbn.ID.ToString();

                            Service_LaiSuatAGLANG svbds = new Service_LaiSuatAGLANG();
                            svbds.IDLaiSuatAGLANG = int.Parse(LaiSuatID);
                            svbds.IDSanPham = int.Parse(dtcart[0].ipid.ToString());
                            svbds.IDThanhVienBan = int.Parse(IDNhaCungCapBanHang);
                            svbds.IDThanhVienHuongHH = int.Parse(dtcart[0].IDThanhVien.ToString());// Người được hưởng chính là nhà đầu tư
                            //svbds.IDThanhVienHuongHH = int.Parse(IDNhaCungCapBanHang);
                            svbds.LaiSuat = HoaHong3.ToString();
                            svbds.NgayNhan = DateTime.Now;
                            svbds.SoTienDauTu = dtcart[0].Money.ToString();
                            svbds.KieuLaiSuat = 2;// kiểu lãi suất chính là 28% theo tháng hay 32%  theo quý
                            svbds.SoNgayDaChay = 1;//Tổng sẽ chạy = 365 ngày, // Ngày đầu tiên khi duyệt đơn hàng sẽ được tính là 1 ngày
                            svbds.NgayThamGia = DateTime.Now;
                            svbds.IDCart = int.Parse(IDGioHang.ToString());
                            svbds.NoiDung = Commond.ShowPro(dtcart[0].ipid.ToString());
                            svbds.IDGioiThieuTrucTiep = Convert.ToInt64(TimF1Agland);
                            svbds.MTreeHuong = Commond.ShowMTrees(dtcart[0].IDThanhVien.ToString());
                            db.Service_LaiSuatAGLANGs.InsertOnSubmit(svbds);
                            db.SubmitChanges();

                            // CongDiemAgLand(dtcart[0].IDThanhVien.ToString(), HoaHong3.ToString());
                            CongDiemAgLand(dtcart[0].ipid.ToString(), "80", "Lãi suất AG LAND - Đặt Hàng", IDNhaCungCapBanHang, dtcart[0].IDThanhVien.ToString(), HoaHong3.ToString(), GoiDauTu.ToString(), TimF1Agland, IDGioHang.ToString());
                            #endregion
                        }
                        // Cập nhật thành viên là ThanhVienAgLang=1 khi đặt sản phẩm là Ag LanD Nhé
                        Susers.Name_Text("UPDATE [users] SET ThanhVienAgLang=1 WHERE iuser_id=" + dtcart[0].IDThanhVien.ToString() + "");
                    }
                    #endregion
                    //#region Cập nhật lại tại thời điểm thanh toán level đang là bao nhiêu và phần trăm hoa hồng là bao nhiêu vào bảng CartDetail. để còn hiển thị ở lịch sử .... đơn hàng đã xử lý
                    //string VCapdoLevelHoaHong = CapoLevelHoaHong(table.LevelThanhVien.ToString());
                    //double VHoaHongs = Convert.ToDouble(VCapdoLevelHoaHong);
                    //SCartDetail.Name_Text("UPDATE [CartDetail] SET HoaHongTheoLevel=" + VHoaHongs + " WHERE id=" + dtcart[0].ID.ToString() + "");
                    //#endregion
                }
                #endregion

                // Xóa tiền ở bảng tạm
                ViTamMuaHang del = db.ViTamMuaHangs.Where(s => s.IDCartDetail == int.Parse(str2)).FirstOrDefault();
                if (del != null)
                {
                    db.ViTamMuaHangs.DeleteOnSubmit(del);
                    db.SubmitChanges();
                }
            }
        }
        void CongDiemAgLand(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string TongTien, string GoiDauTu, string TimF1Agland, string IDDonHang)
        {
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong + "");
            if (iitem != null)
            {

                double VGoiDauTu = Convert.ToDouble(GoiDauTu);
                double VTongTienCoinDuocCap = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                double VVViUuTien = Convert.ToDouble(iitem[0].ViUuTien);

                double Tienmotcophan = Convert.ToDouble(Commond.Setting("tienmotcophan"));
                double VTienDangSoHuuBatDongSan = Convert.ToDouble(iitem[0].TienDangSoHuuBatDongSan);

                double CoPhan = 0;
                CoPhan = ((VGoiDauTu) / (Tienmotcophan));

                double TongCoPhan = 0;
                TongCoPhan = ((CoPhan) + (VTienDangSoHuuBatDongSan));


                double TongSo = Convert.ToDouble(iitem[0].ViAgLang);
                double TongTienNapVao = Convert.ToDouble(TongTien);
                double Conglai = 0;
                Conglai = ((TongSo) + (TongTienNapVao));

                double TConglai = 0;
                TConglai = ((VTongTienCoinDuocCap) + (TongTienNapVao));

                double VVConglai = 0;
                VVConglai = ((VVViUuTien) + (TongTienNapVao));

                //Susers.Name_Text("update users set ViAgLang=" + Conglai.ToString() + ",TongTienCoinDuocCap=" + TConglai + ",TienDangSoHuuBatDongSan=" + CoPhan + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");

                //Uutien=1 sẽ roi Toan bộ hh vào ví uwutien ko roi vào ví Thương mai
                if (iitem[0].Uutien.ToString() == "1")
                {
                    Susers.Name_Text("update users set ViAgLang=" + Conglai.ToString() + ",ViUuTien=" + VVConglai + ",TienDangSoHuuBatDongSan=" + TongCoPhan + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }
                else
                {
                    Susers.Name_Text("update users set ViAgLang=" + Conglai.ToString() + ",TienDangSoHuuBatDongSan=" + TongCoPhan + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }
                //F1 sẽ hưởng lãi 10% của lãi Agland
                if (TimF1Agland != "0")
                {
                    List<Entity.users> igt = Susers.Name_Text("select * from users where iuser_id=" + TimF1Agland + "");
                    if (igt != null)
                    {
                        double TongDiemATL = Convert.ToDouble(igt[0].ViFMotAnTheoAgland.ToString());
                        double HHF1AnTheoAgland = Convert.ToDouble(Commond.Setting("txtF1AnTheoAgland"));
                        double TongTienNapVaos = Convert.ToDouble(TongTien);
                        double ChiaHH = (TongTienNapVaos * HHF1AnTheoAgland) / 100;
                        double CVConglai = 0;
                        CVConglai = ((TongDiemATL) + (ChiaHH));
                        Susers.Name_Text("update users set ViFMotAnTheoAgland=" + CVConglai.ToString() + "  where iuser_id=" + TimF1Agland.ToString() + "");

                        LaiSuatTheoAgLand obj = new LaiSuatTheoAgLand();
                        obj.IDThanhVienMua = Convert.ToInt64(IDUserNguoiDuocHuong);
                        obj.IDHuongF1 = Convert.ToInt64(TimF1Agland);
                        obj.HoaHong = ChiaHH.ToString();
                        obj.PhanTramLai = int.Parse(HHF1AnTheoAgland.ToString());
                        obj.NgayTao = DateTime.Now;
                        obj.IDDonHang = Convert.ToInt64(IDDonHang);
                        db.LaiSuatTheoAgLands.InsertOnSubmit(obj);
                        db.SubmitChanges();
                    }
                }
            }

            #region LichSuGiaoDich
            LichSuGiaoDich obl = new LichSuGiaoDich();
            obl.IDProducts = int.Parse(IDProducts);
            obl.IDType = int.Parse(IDType);
            obl.Type = Type;
            obl.IDThanhVien = int.Parse(IDThanhVien);
            obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
            obl.PhamTramHoaHong = "0";
            obl.SoCoin = TongTien.ToString();
            obl.NgayTao = DateTime.Now;
            obl.NoiDung = Commond.ShowPro(IDSanPham.ToString());
            obl.IDCart = int.Parse(IDGioHang);
            db.LichSuGiaoDiches.InsertOnSubmit(obl);
            db.SubmitChanges();
            #endregion
        }
        protected string TimLevelB(string ID)
        {
            user iitems = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(ID.ToString()));
            if (iitems != null)
            {
                return iitems.LevelThanhVien.ToString();
            }
            return "0";
        }
        void ThemHoaHong_ThuongLevel(string IDProducts, string ThuTu, string IDType, string IDThanhVien, string IDUserNguoiDuocHuong, string SoCoin, string LevelThanhVienA, string LevelThanhVienB)
        {
            //string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
            //if (LogFile == "true")
            //{
            //    Library.WriteErrorLog("Người Duyệt đơn hàng : " + MoreAll.MoreAll.GetCookies("Members").ToString() + " - Mã Đơn hàng: " + hdIDGiohang.Value + " Duyet thường thành viên - Sản phẩm: " + IDProducts + " - " + ThuTu + " - IDThanhVien: " + IDThanhVien + " - IDUserNguoiDuocHuong: " + IDUserNguoiDuocHuong + " - LevelThanhVienA: " + LevelThanhVienA + " - LevelThanhVienB: " + LevelThanhVienB);
            //}

            if (TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()) != "0")
            {
                #region Hoa hồng gián tiếp khi giới thiệu được hưởng sau khi lấy level A - level B
                double SoPhanTram = Convert.ToDouble(TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()));
                double TongTien = Convert.ToDouble(SoCoin);
                double ThuongLevel = (TongTien * SoPhanTram) / 100;
                ThemHoaHong(IDProducts, IDType, "Hoa hồng (Cấp) quản lý " + ThuTu, IDThanhVien, IDUserNguoiDuocHuong, SoPhanTram.ToString(), ThuongLevel.ToString());
                #endregion
            }
        }
        void ThemHoaHongTamGiuMuaHang(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
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

            obj.NoiDung = Commond.ShowPro(IDSanPham.ToString());
            obj.IDCart = Convert.ToInt64(IDGioHang);

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
            obl.NoiDung = Commond.ShowPro(IDSanPham.ToString());
            obl.IDCart = Convert.ToInt64(IDGioHang);
            db.LichSuGiaoDiches.InsertOnSubmit(obl);
            db.SubmitChanges();
            #endregion
        }
        void ThemHoaHong(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
        {
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
                obj.NoiDung = Commond.ShowPro(IDSanPham.ToString());
                obj.IDCart = Convert.ToInt64(IDGioHang);

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
                obl.NoiDung = Commond.ShowPro(IDSanPham.ToString());
                obl.IDCart = Convert.ToInt64(IDGioHang);
                db.LichSuGiaoDiches.InsertOnSubmit(obl);
                db.SubmitChanges();
                #endregion

                CongTien(IDType, IDUserNguoiDuocHuong, SoCoin);
                //CongTien_ViTienHHGioiThieu(IDProducts, IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, SoCoin);
            }
        }
        void CongTien_ViTienHHGioiThieu(string IDProducts, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
        {
            #region Cộng điểm  theo hoa hồng coin vào bảng thành viên để tích điểm trong các trường hợp mua hàng cần
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + "");
            if (iitem.Count() > 0)
            {
                double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                double TongTienNapVao = Convert.ToDouble(SoCoin);
                double Conglai = 0;
                Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");

                // sẽ xóa đoạn code này khi ViTienHHGioiThieu=0 đồng nhé
                #region Làm thêm phần lấy điểm từ ví ViTienHHGioiThieu trừ đi và cộng thêm vào TongTienCoinDuocCap
                //06/01/2020
                //Làm thêm phần lấy điểm từ ví ViTienHHGioiThieu trừ đi và cộng thêm vào TongTienCoinDuocCap
                List<Entity.users> truvi = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + "  and HoTro=1");
                if (truvi.Count() > 0)
                {
                    double ViTienHHGioiThieu = Convert.ToDouble(truvi[0].ViTienHHGioiThieu);
                    if (ViTienHHGioiThieu >= TongTienNapVao)
                    {
                        double ConglaiBiTru = ((ViTienHHGioiThieu) - (TongTienNapVao));

                        double TongSo = Convert.ToDouble(truvi[0].TongTienCoinDuocCap);
                        double VConglai = ((TongSo) + (TongTienNapVao));

                        Susers.Name_Text("update users set ViTienHHGioiThieu=" + ConglaiBiTru.ToString() + "  where iuser_id=" + truvi[0].iuser_id.ToString() + "");
                        Susers.Name_Text("update users set TongTienCoinDuocCap=" + VConglai.ToString() + "  where iuser_id=" + truvi[0].iuser_id.ToString() + "");

                        // Hoa hồng này là lấy từ ví ViTienHHGioiThieu cộng sang ví TongTienCoinDuocCap khi có phát sinh tiền hoa hồng
                        // lưu ý: IDThanhVien ở đây chỉ mang tính chất minh họa khi có phát sinh, chứ ko phải được hoa hồng từ người này nhé.
                        ThemHoaHongThem_ViTienHHGioiThieu(IDProducts, "31", "Hoa Hồng (Hỗ Trợ)", IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, TongTienNapVao.ToString());
                        //Mục 32 này làm để lưu lịch sử để sau này nhỡ có lỗi còn lục lại được là đã bị trừ ntn
                        ThemHoaHongThem_ViTienHHGioiThieu(IDProducts, "32", "Hoa Hồng hỗ trợ (Bị trừ từ ví hoa hồng Hỗ Trợ)", IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, TongTienNapVao.ToString());
                    }
                }
                #endregion
            }
            #endregion
        }
        // Sẽ xóa đoạn code này khi ViTienHHGioiThieu=0 đồng nhé ThemHoaHongThem_ViTienHHGioiThieu
        void ThemHoaHongThem_ViTienHHGioiThieu(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
        {
            #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
            Commond.CheckNgayHetHan(IDUserNguoiDuocHuong.ToString());
            #endregion

            List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and DuyetTienDanap=1 ");
            if (F1.Count() > 0)
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
                obj.NoiDung = Commond.ShowPro(IDSanPham.ToString());
                obj.IDCart = Convert.ToInt64(IDGioHang);

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
                obl.NoiDung = Commond.ShowPro(IDSanPham.ToString());
                obl.IDCart = Convert.ToInt64(IDGioHang);

                db.LichSuGiaoDiches.InsertOnSubmit(obl);
                db.SubmitChanges();
                #endregion
            }
        }

        // sau khi xóa code ViTienHHGioiThieu thì dùng lại code này nhé
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

        protected string ShowLyDoTraHang(string info)
        {
            if (!string.IsNullOrWhiteSpace(info))
            {
                return "<span style='background: #d9857c; padding: 4px; margin: 5px; color: #fff; border-radius: 3px; width: 100%; float: left;'>" + info + "</span>";
            }
            return "";
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
        public static string EnableLock_TraHangstyle(string Enable)
        {
            if (Enable.ToString() == "1" || Enable.ToString() == "2")
            {
                return "display:none";
            }
            return "display:block ";
        }
        protected void btTrahang_Click(object sender, EventArgs e)
        {

            List<CartDetail> item = db.CartDetails.OrderByDescending(s => s.TrangThaiNguoiMuaHang == 3 && s.TrangThaiNhaCungCap == 1 && s.ID == Convert.ToInt32(hdgiatri.Value)).ToList();
            if (item.Count > 0)
            {
                // Cập nhậ trạng thái trả đơn hàng
                SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=2,[TrangThaiNhaCungCap]=4,[LyDoTraHang] =N'" + txtnoidung.Text + "' WHERE ID =" + item[0].ID.ToString() + "");
                //Cập nhật trạng thái nhà cung cấp chưa xử lý đơn hàng, vì có trường hợp bên nhà cung cấp và ng mua đã trao đổi xong và họ muốn đặt lại đơn hàng đó

                #region Gửi mail cho nhà cung cấp khi thành viên mua hủy sản phẩm.
                List<Entity.users> dc = Susers.GET_BY_ID(item[0].IDThanhVien.ToString());
                if (dc.Count > 0)
                {
                    string Emails = dc[0].vemail.ToString();
                    string Noidung = "";
                    Noidung += "Kính gửi nhà cung cấp: <b>" + ShowNameNhaCungCap(item[0].IDNhaCungCap.ToString()) + "</b><br />";
                    Noidung += "<b>Chúng tôi rất xin lỗi! </b><br /> Đơn hàng <a href=\"http://aggroup365.com/account/orders/" + item[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + item[0].ID_Cart.ToString() + "</b></a> của bạn đã trả lại. Vui lòng xem nội dung ở phía dưới.<br />";
                    Noidung += "<br />";

                    Noidung += "<b>Tên sản phẩm :  </b>" + Commond.ShowPro(item[0].ipid.ToString()) + "<br />";
                    Noidung += "<b>Số lượng sản phẩm :  </b>" + item[0].Quantity.ToString() + "<br />";
                    Noidung += "<b>Tổng số tiền :  </b>" + item[0].Money.ToString() + "<br />";
                    Noidung += "<b>Nội dung  :  </b>" + txtnoidung.Text + "<br />";

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
                        new MailHelper().SendMail(ShowEmailNhaCungCap(item[0].IDNhaCungCap.ToString()), "Trả lại đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + item[0].ID_Cart.ToString() + " ", Noidung.ToString());
                        new MailHelper().SendMail(dc[0].vemail.ToString(), "Trả lại đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + item[0].ID_Cart.ToString() + " ", Noidung.ToString());
                    }
                    catch { }
                }
                #endregion

                ltthongbao.Text = "<script type=\"text/javascript\">alert('Trả hàng thành công. Quý khách vui lòng đợi phản hồi lại từ nhà cung cấp.');window.location.href='" + URL + "'; </script></div>";

            }

        }
    }
}