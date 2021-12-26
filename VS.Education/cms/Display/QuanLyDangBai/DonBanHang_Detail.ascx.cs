using Entity;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class DonBanHang_Detail : System.Web.UI.UserControl
    {
        public int i = 1;
        DatalinqDataContext db = new DatalinqDataContext();
        string IDCart = "";
        string URL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IDCart"] != null && !Request["IDCart"].Equals(""))
            {
                IDCart = Request["IDCart"];
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
                    hdid.Value = table.iuser_id.ToString();
                }
            }
        }
        private void ShowProducts()
        {
            string sql = "select * from Carts where ID=" + IDCart + " order by Create_Date desc";
            List<LCart> dt = db.ExecuteQuery<LCart>(@"" + sql + "").ToList();
            if (dt != null)
            {
                ltmadonhang.Text = dt[0].ID.ToString();
                ltngaydathang.Text = dt[0].Create_Date.ToString();
                lttrangthai.Text = ShowTrangThai(dt[0].Status.ToString());
                lthovaten.Text = dt[0].Name.ToString();
                ltdiachi.Text = dt[0].Address.ToString();
                ltdienthoai.Text = dt[0].Phone.ToString();
                ltnoidung.Text = dt[0].Contents;

                List<Entity.CartDetail> dtcart = SCartDetail.Detail_NhaCungCap(dt[0].ID.ToString(), hdid.Value);
                if (dtcart.Count > 0)
                {
                    this.rpcartdetail.DataSource = dtcart;
                    this.rpcartdetail.DataBind();

                    foreach (var item in dtcart)
                    {
                        if (item.NoiDungGiaoHang.Length > 5)
                        {
                            ltthongtin.Text += "<div class=\"alert alert-info\" role=\"alert\">Thông báo từ nhà cung cấp với sản phẩm : <b>" + Commond.ShowPro(item.ipid.ToString()) + " <br></b>";
                            ltthongtin.Text += "<div style='color:red'> " + item.NoiDungGiaoHang + "</div>";
                            ltthongtin.Text += "</div>";
                        }
                    }

                    string totalvnd = "";
                    if (dtcart.Count > 0)
                    {
                        double num = 0.0;
                        for (int i = 0; i < dtcart.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart[i].Money.ToString());
                        }
                        totalvnd = num.ToString();
                    }
                    lttongtien.Text = AllQuery.MorePro.Detail_Price(totalvnd.ToString());
                    lttongtienbangchu.Text = MoreAll.Hamdoisorachu.So_chu(Convert.ToDouble(totalvnd.ToString()));
                }
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
        protected string CapoLevelHoaHong(string level)
        {
            List<Entity.Menu> cdd = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.LV + "'  and Views=" + level + " and lang='VIE' order by level,Orders asc");
            if (cdd != null)
            {
                return cdd[0].Noidung1.ToString();// ID chính là thuộc cấp độ mấy do tiêu đề ghi
            }
            return "0";
        }
        protected string HoaHongTheoLevel_TheoThoiDiemMuahang_News(string CapoLevelHoaHongs, string Tongd)
        {
            double TongCoin = Convert.ToDouble(Tongd);
            double HoaHongs = Convert.ToDouble("30");//CapoLevelHoaHongs
            double Tong = (TongCoin * HoaHongs) / 100;
            return Tong.ToString();
        }
        protected string Showtrangthai(string status)
        {
            if (status.Equals("1"))
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Đã duyệt</span>";
            }
            if (status.Equals("2"))
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Hủy đơn</span>";
            }
            if (status.Equals("5"))
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Hoàn tiền</span>";
            }
            if (status.Equals("6"))
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Khiếu kiện Admin</span><div style='margin:5px;color: #a50606;' title='Đang chờ Admin xử lý'>Đang chờ Admin xử lý</div>";
            }
            else if (status.Equals("7"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;width: 142px;border-radius: 3px;text-align: center;margin: auto;' title='Admin Chấp nhận thanh toán'>Admin Chấp nhận TT</a>";
            }
            else if (status.Equals("8"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;width: 130px;border-radius: 3px;text-align: center;margin: auto;' title='Admin Hoàn tiền'>Admin Hoàn tiền</a>";
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
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin
            //7: Chấp nhận thanh toán
            // 8: Hoàn tiền
            if (Enable.ToString() == "2")
            {
                return true;
            }
            return false;
        }
        protected bool EnableLock_TraHang(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin
            if (Enable.ToString() == "2")
            {
                return true;
            }
            return false;
        }
        protected bool EnableLock_DuyetHang(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin
            if (Enable.ToString() == "3")
            {
                return true;
            }
            return false;
        }
        public static string EnableLock_DuyetHangstyle(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin
            if (Enable.ToString() == "8" || Enable.ToString() == "7" || Enable.ToString() == "1" || Enable.ToString() == "6" || Enable.ToString() == "5" || Enable.ToString() == "2" || Enable.ToString() == "4")
            {
                return "display:none";
            }
            return "display:block";
        }

        protected bool EnableLock_DuyetHang_HuyHang(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin

            if (Enable.ToString() == "3" || Enable.ToString() == "4")
            {
                return true;
            }
            return false;
        }
        protected bool EnableLock_KhieuKien(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin

            if (Enable.ToString() == "5")
            {
                return true;
            }
            return false;
        }
        protected void DuyetDon_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn duyệt sản phẩm này. Đồng nghĩa là bạn có sẽ giao hàng.?')";
        }
        protected void HoanTien_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn hoàn tiền cho người mua đối với sản phẩm này ?')";
        }
        protected void KhieuKien_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn khiếu kiện với người mua đối với sản phẩm này - Admin sẽ vào xử lý tình huống này ?')";
        }
        protected void HuyDon_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn hủy sản phẩm này?. Đồng nghĩa là bạn sẽ không thể giao hàng.')";
        }
        protected void rpcartdetail_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str = e.CommandName.Trim();
            string str2 = e.CommandArgument.ToString().Trim();
            string str4 = str;

            switch (e.CommandName)
            {
                case "HuyDon":
                    List<CartDetail> item = db.ExecuteQuery<CartDetail>(@"SELECT * FROM CartDetail where ID =" + str2 + " and TrangThaiNguoiMuaHang=3").ToList();
                    if (item.Count > 0)
                    {
                        #region Trừ vào ví của NCC
                        List<Entity.users> thanhtoan = Susers.Name_Text("select * from users where iuser_id=" + int.Parse(item[0].IDNhaCungCap.ToString()) + " ");
                        if (thanhtoan.Count > 0)
                        {
                            double Money = Convert.ToDouble(item[0].Money.ToString()) / 1000;
                            double TongTienCoinDuocCap = Convert.ToDouble(thanhtoan[0].TongTienCoinDuocCap.ToString());
                            double ConglaiCoin = ((TongTienCoinDuocCap) - (Money));
                            if (TongTienCoinDuocCap >= Money)
                            {
                                Susers.Name_Text("update users set TongTienCoinDuocCap='" + ConglaiCoin + "'  where iuser_id=" + thanhtoan[0].iuser_id.ToString() + "");
                            }
                            else
                            {
                                ltthongbao.Text = "<script type=\"text/javascript\">alert('Tài khoản nhà cung cấp không đủ để trả.');window.location.href='" + URL + "'; </script></div>";
                                return;
                                break;
                            }
                        }
                        #endregion

                        #region Thanh toán tiền cho ng mua
                        List<Entity.users> Nhanve = Susers.Name_Text("select * from users where iuser_id=" + int.Parse(item[0].IDThanhVien.ToString()) + " ");
                        if (Nhanve.Count > 0)
                        {
                            double Money = Convert.ToDouble(item[0].Money.ToString()) / 1000;
                            double TongTienCoinDuocCap = Convert.ToDouble(Nhanve[0].TongTienCoinDuocCap.ToString());
                            double ConglaiCoin = ((TongTienCoinDuocCap) + (Money));
                            Susers.Name_Text("update users set TongTienCoinDuocCap='" + ConglaiCoin + "'  where iuser_id=" + Nhanve[0].iuser_id.ToString() + "");
                        }
                        #endregion

                        SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=2 WHERE ID =" + str2 + "");
                        #region Gửi mail cho thành viên mua, NCC hoàn trả tiền
                        List<CartDetail> tablv = db.CartDetails.Where(s => s.ID == int.Parse(str2)).ToList();
                        if (tablv.Count > 0)
                        {
                            List<Entity.users> dc = Susers.GET_BY_ID(tablv[0].IDThanhVien.ToString());
                            if (dc.Count > 0)
                            {
                                string Emails = dc[0].vemail.ToString();
                                //Gửi email cho người mua
                                string Noidung = "";
                                Noidung += "Kính gửi: <b>" + dc[0].vfname.ToString() + "</b><br />";

                                Noidung += "Đơn hàng <a href=\"http://lienhiephoptac.vn/account/orders/" + tablv[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + tablv[0].ID_Cart.ToString() + "</b></a> đã được hoàn tiền . Quý khách vui lòng kiểm tra lại.";

                                Noidung += "<br />";
                                Noidung += "<br />";
                                Noidung += "<b>Tên sản phẩm  </b>: " + Commond.ShowPro(tablv[0].ipid.ToString()) + "<br />";
                                Noidung += "<b>Số lượng sản phẩm  </b>: " + tablv[0].Quantity.ToString() + "<br />";
                                Noidung += "<b>Tổng số tiền  </b>: " + tablv[0].Money.ToString() + "<br />";

                                Noidung += "---------------------------------";
                                Noidung += "<br />";

                                Noidung += "THÔNG TIN NGƯỜI MUA HÀNG:";
                                Noidung += "<br />";
                                Noidung += "<b>Người mua hàng: </b>" + dc[0].vfname.ToString() + "<br />";
                                Noidung += "<b>Địa chỉ: </b>" + dc[0].vaddress.ToString() + "<br />";
                                Noidung += "<b>Điện thoại: </b>" + dc[0].vphone.ToString() + "<br />";
                                Noidung += "<b>Email: </b>" + dc[0].vemail.ToString() + "<br />";

                                Noidung += "---------------------------------";
                                Noidung += "<br />";

                                Noidung += "THÔNG TIN NHÀ CUNG CẤP:";
                                Noidung += "<br />";
                                Noidung += ShowNameNCC(tablv[0].IDNhaCungCap.ToString()) + "<br />";
                                Noidung += "<br />";
                                Noidung += "<br />";
                                Noidung += Commond.Setting("txtfooterEmail");
                                Noidung += "<br />";
                                try
                                {
                                    new MailHelper().SendMail(ShowEmailNhaCungCap(tablv[0].IDNhaCungCap.ToString()), "Hoàn tiền từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + tablv[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                    new MailHelper().SendMail(dc[0].vemail.ToString(), "Hoàn tiền từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + tablv[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                }
                                catch
                                {
                                    ltthongbao.Text = "<script type=\"text/javascript\">alert('Hoàn tiền lại cho người mua thành công. Hãy Nêu lý do hủy đơn vào ô lý do.');window.location.href='" + URL + "'; </script></div>";
                                }
                            }
                        }
                        #endregion
                        return;
                    }

                    ltthongbao.Text = "<script type=\"text/javascript\">alert('Hủy hàng thành công. Hãy Nêu lý do hủy hàng vào ô lý do.');window.location.href='" + URL + "'; </script></div>";
                    return;


            }
        }
        public string ShowNameNCC(string id)
        {
            string Noidung = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count > 0)
            {
                Noidung += "<b>Tên nhà cung cấp: </b>" + dt[0].vfname.ToString() + "<br />";
                Noidung += "<b>Email: </b>" + dt[0].vemail.ToString() + "<br />";
                Noidung += "<b>Điện thoại: </b>" + dt[0].vphone.ToString() + "<br />";
            }
            return Noidung;
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
        protected void txtLyDoHuyHang_TextChanged(object sender, EventArgs e)
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
                        SCartDetail.Name_Text("UPDATE [CartDetail] SET [LyDoHuyHang] =N'" + LyDo.Text + "' WHERE ID =" + b.Value + "");
                    }
                    ltthongbao.Text = "<script type=\"text/javascript\">alert('Nhập lý do hủy hàng thành công.');window.location.href='" + URL + "'; </script></div>";
                }
            }
            else
            {
                ltthongbao.Text = "<script type=\"text/javascript\">alert('Vui lòng nhập lý do hủy hàng trên 10 ký tự.');window.location.href='" + URL + "'; </script></div>";
            }
        }
        protected string ShowtrangthaiNMH(string status, string LyDoHuyHang)
        {
            if (status.Equals("1"))
            {
                return "<a style='background:#0b98ea;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Người Mua đã chấp nhận đơn hàng này.'>Chấp nhận</a>";
            }
            else if (status.Equals("3"))
            {
                return "<a style='background:#ffa903;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Người Mua chưa xử lý đơn hàng này'>Chưa xử lý</a>";
            }
            else if (status.Equals("2"))
            {
                string ld = "";
                if (LyDoHuyHang.Length > 0)
                {
                    ld = "<span class='lydohuyhang'>" + LyDoHuyHang + "</span>";
                }
                return "<a style='background:#ed1c24;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'  title='Người Mua đã trả lại đơn hàng này'>Trả Hàng</a><br>" + ld + "";
            }
            return "";
        }

        protected void btduyetdon_Click(object sender, EventArgs e)
        {
            List<Entity.users> dt1 = Susers.GET_BY_ID(MoreAll.MoreAll.GetCookies("MembersID").ToString());
            if (dt1.Count >= 1)
            {
                if (dt1[0].DuyetTienDanap.ToString() == "0")
                {
                    ltthongbao.Text = ("<script type=\"text/javascript\" > $.toast({ heading: 'Thông báo', text: 'Bạn không thể duyệt đơn hàng này vì tài khoản của quý khách đã hết hạn 1 năm kích hoạt .<br /> Vui lòng kích hoạt lại 480 điểm để được hưởng các chính sách của đại lý<br />', position: 'top-center', stack: false }) </script>");
                    // ltthongbao.Text = "<script type=\"text/javascript\">alert('Bạn không thể duyệt đơn hàng này vì tài khoản của quý khách đã hết hạn 1 năm kích hoạt . Vui lòng kích hoạt lại 480 điểm để được hưởng các chính sách của đại lý.');window.location.href='" + URL + "'; </script></div>";
                    return;
                }
            }

            // duyệt đơn kiểu mới , có thêm phần nội dung thông báo ngày chuyển hàng
            try
            {
                List<CartDetail> table = db.ExecuteQuery<CartDetail>(@"SELECT * FROM CartDetail where ID =" + hdgiatri.Value + " and TrangThaiNguoiMuaHang=3").ToList();
                if (table.Count > 0)
                {
                    if (txtnoidung.Text.Length > 0)
                    {
                        SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=1,LyDoTraHang='',LyDoHuyHang='',NoiDungGiaoHang=N'" + txtnoidung.Text + "' WHERE ID =" + hdgiatri.Value + "");
                    }
                    else
                    {
                        SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=1,LyDoTraHang='',LyDoHuyHang='',NoiDungGiaoHang='' WHERE ID =" + hdgiatri.Value + "");
                    }
                    #region Gửi mail cho thành viên mua, khi NCC chấp nhận đơn hàng
                    List<Entity.users> dc = Susers.GET_BY_ID(table[0].IDThanhVien.ToString());
                    if (dc.Count > 0)
                    {
                        string Emails = dc[0].vemail.ToString();

                        //Gửi email cho người mua
                        string Noidung = "";

                        Noidung += "Kính gửi: <b>" + dc[0].vfname.ToString() + "</b><br />";
                        Noidung += "Chúc mừng bạn. Nhà cung cấp đã xác nhận đơn hàng <a href=\"http://lienhiephoptac.vn/account/orders/" + table[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + table[0].ID_Cart.ToString() + "</b></a> của bạn. Vui lòng xem lời nhắn phía dưới!<br />";
                        Noidung += "<br />";
                        Noidung += "Tên sản phẩm : " + Commond.ShowPro(table[0].ipid.ToString()) + "<br />";
                        Noidung += "Số lượng sản phẩm : " + table[0].Quantity.ToString() + "<br />";
                        Noidung += "Tổng số tiền : " + table[0].Money.ToString() + "<br />";
                        Noidung += "<b>Nội dung: </b>" + txtnoidung.Text + ".<br />";

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
                            new MailHelper().SendMail(dc[0].vemail.ToString(), "Chập nhận đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + table[0].ID_Cart.ToString() + " ", Noidung.ToString());
                        }
                        catch { ltthongbao.Text = "<script type=\"text/javascript\">alert('Duyệt đơn hàng thành công..');window.location.href='" + URL + "'; </script></div>"; }
                    }
                    #endregion

                    ltthongbao.Text = "<script type=\"text/javascript\">alert('Duyệt đơn hàng thành công..');window.location.href='" + URL + "'; </script></div>";
                }

            }
            catch (Exception)
            { }
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
            string Noidung = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count > 0)
            {
                Noidung += "<b>Tên nhà cung cấp: </b>" + dt[0].vfname.ToString() + "<br />";
                Noidung += "<b>Email: </b>" + dt[0].vemail.ToString() + "<br />";
                Noidung += "<b>Điện thoại: </b>" + dt[0].vphone.ToString() + "<br />";
            }
            return Noidung;
        }
    }
}