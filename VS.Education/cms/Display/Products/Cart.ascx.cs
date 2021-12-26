using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Services;
using Entity;
using Framwork;
using Framework;
using WebApplication2;
using API_NganLuong;
using System.Configuration;
using TestWindowService;

namespace VS.E_Commerce.cms.Display.Products
{
    public partial class Cart : System.Web.UI.UserControl
    {
        string LogFile = ConfigurationManager.AppSettings.Get("LogFileCart");
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected bool Dung = false;
        string Plevel = "99999999999";
        string TongLevel = "0";
        private string IDMaDonTao = "1";
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
            this.Page.Form.DefaultButton = btnSendOrder.UniqueID;
            if (!base.IsPostBack)
            {
                if (MoreAll.MoreAll.GetCookies("Members") != "")
                {
                    Fusers item = new Fusers();
                    DataTable table = item.Detailvuserun(MoreAll.MoreAll.GetCookies("Members").ToString());
                    if (table.Rows.Count > 0)
                    {
                        if (table.Rows[0]["TatChucNang"].ToString() == "1")
                        {
                            Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này.');window.location.href='/vi-tien.html'; </script>");
                        }

                        hdidthanhvien.Value = table.Rows[0]["iuser_id"].ToString();
                        hdthanhvienFree.Value = table.Rows[0]["DuyetTienDanap"].ToString();

                        if (table.Rows[0]["DuyetTienDanap"].ToString() == "1" && table.Rows[0]["CuaHang"].ToString() == "1")
                        {
                            hdcuahang.Value = "2";
                        }
                        else if (table.Rows[0]["DuyetTienDanap"].ToString() == "1" && table.Rows[0]["CuaHang"].ToString() == "0")
                        {
                            hdcuahang.Value = "1";
                        }
                        else if (table.Rows[0]["DuyetTienDanap"].ToString() == "0")
                        {
                            hdcuahang.Value = "0";
                        }

                        ShowTinhThanh();
                        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddltinhthanh, table.Rows[0]["TinhThanh"].ToString());

                        hdChiNhanh.Value = table.Rows[0]["IDChiNhanh"].ToString();
                        hdvivip.Value = table.Rows[0]["ViTangTienVip"].ToString();
                        this.txtName.Text = table.Rows[0]["vfname"].ToString();
                        this.txtAddress.Text = table.Rows[0]["vaddress"].ToString();
                        this.txtEmail.Text = table.Rows[0]["vemail"].ToString();
                        this.txtPhone.Text = table.Rows[0]["vphone"].ToString();
                       
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                }
                ltgiohang.Text = Commond.Setting("GioHang");
                // ShowTinhThanh();
                this.LoadCart();
                //this.btnSendOrder.Text = this.label("l_produc_sder");
                //this.btnorder.Text = this.label("l_produc_sder");
                //this.btnEditCart.Text = this.label("l_produc_Edit_Cart");
                //this._btctnew.Text = this.label("l_produc_Continue_Shopping");
                //this.btnext.Text = this.label("l_produc_Continue_Shopping");
                //this.btnCancelOrder.Text = this.label("l_produc_Cancel_Order");
                //this.btndelete.Text = this.label("l_produc_Cancel_Order");
            }
        }
        #region Menu
        protected void ShowTinhThanh()
        {
            int str = 0;
            var dt = db.Tinhthanhs.Where(s => s.capp == "TT" && s.Parent_ID == -1 && s.Lang == language).ToList();
            for (int i = 0; i < dt.Count; i++)
            {
                if (dt[i].Parent_ID.ToString() == "-1")
                {
                    ddltinhthanh.Items.Insert(str, new ListItem(dt[i].Name.ToString(), dt[i].ID.ToString()));
                }
            }
            this.ddltinhthanh.Items.Insert(0, new ListItem("== Chọn tỉnh thành == ", "0"));
            this.ddltinhthanh.DataBind();
        }
        #endregion

        //
        protected void ItemDataBound_RP(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemIndex != -1) && (e.Item.ItemType != ListItemType.Separator))
            {
                Label lb_tt = (Label)e.Item.FindControl("lb_tt");
                lb_tt.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
        private void LoadCart()
        {
            if (Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
                    if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
                    {
                        PNMobile.Visible = true;
                        PNDestop.Visible = false;

                        rpmobile.DataSource = dtcart;
                        rpmobile.DataBind();
                    }
                    else
                    {
                        PNMobile.Visible = false;
                        PNDestop.Visible = true;
                        Repeater1.DataSource = dtcart;
                        Repeater1.DataBind();
                    }
                    string inumofproducts = "";
                    string totalvnd = "";
                    if (dtcart.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                            num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    hdtongtien.Value = totalvnd.ToString();

                    this.lttotalvnd.Text = AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());
                    this.ltnumberofproducts.Text = inumofproducts;
                    float total = 0;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        total += Convert.ToSingle(dtcart.Rows[i]["Money"]);
                    }

                    this.pncart.Visible = true;
                    this.pnmessage.Visible = false;
                }
                else
                {
                    this.pncart.Visible = false;
                    this.pnmessage.Visible = true;
                }
            }
            else
            {
                this.pncart.Visible = false;
                this.pnmessage.Visible = true;
            }
        }

        protected void lnkorder_Click(object sender, EventArgs e)
        {
            this.LoadCart();
        }

        protected void lnkdeletecart_Click(object sender, EventArgs e)
        {
            Session["cart"] = null;
            base.Response.Redirect("/Message.html");
        }

        protected void btnSendOrder_Click(object sender, EventArgs e)
        {

            user iiit = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdidthanhvien.Value));
            if (iiit != null)
            {
                //if (iiit.DuyetTienDanap == 0)
                //{
                //    Response.Write("<script type=\"text/javascript\">alert('Tài khoản của bạn chưa được kích hoạt.');</script>");
                //    return;
                //}
                //else
                // {
                {
                    #region Check thông báo đủ tiền hay không đủ tiền mua hàng
                    double TSoTienPhaiThanhToan = Convert.ToDouble(hdtongtien.Value);
                    double TSoTien = (TSoTienPhaiThanhToan);
                    double TongTien = 0;
                    double TongTien2 = 0;
                    double TongCong = 0;

                    double SoTien = 0;
                    double Tong = 0;
                    double TongCK = 0;

                    if (Session["cart"] != null)
                    {
                        DataTable dtcart = (DataTable)Session["cart"];
                        if (dtcart.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcart.Rows.Count; i++)
                            {
                                TongCong = Convert.ToDouble(iiit.TongTienCoinDuocCap) + Convert.ToDouble(iiit.ViMuaHangAFF);
                                double Money = Convert.ToDouble(dtcart.Rows[i]["money"].ToString()) / 1000;
                                SoTien = (Money);
                                double ViHienTai = 0;
                                double ViAFF = 0;
                                double ViVip = 0;
                                //ViHienTai
                                if (Session["ViHienTai"] == null)
                                {
                                    ViHienTai = Convert.ToDouble(iiit.TongTienCoinDuocCap);
                                }
                                else
                                {
                                    ViHienTai = Convert.ToDouble(Session["ViHienTai"].ToString());
                                }
                                //ViAFF
                                if (Session["ViAFF"] == null)
                                {
                                    ViAFF = Convert.ToDouble(iiit.ViMuaHangAFF);
                                }
                                else
                                {
                                    ViAFF = Convert.ToDouble(Session["ViAFF"].ToString());
                                }

                                ////ViVip
                                //if (Session["ViVip"] == null)
                                //{
                                //    ViVip = Convert.ToDouble(iiit.ViTangTienVip);
                                //}
                                //else
                                //{
                                //    ViVip = Convert.ToDouble(Session["ViVip"].ToString());
                                //}

                                double Tongcong = SoTien - TongCK;
                                // 0 : Vi AFF
                                // 1: Ví Thương mại
                                if (ViAFF >= Tongcong)// trừ tiền ở ví hoa hồng aff
                                {
                                    double ConglaiCoin = ((ViAFF) - (Tongcong));
                                    Session["ViAFF"] = ConglaiCoin;

                                    TongTien = ConglaiCoin;
                                }
                                else if (ViHienTai >= Tongcong)// trừ tiền ví thương mại
                                {
                                    double ConglaiCoin = ((ViHienTai) - (Tongcong));
                                    Session["ViHienTai"] = ConglaiCoin;
                                    TongTien2 = ConglaiCoin;
                                }
                                else
                                {
                                    double TViAFF = ((ViAFF) - (Tongcong));
                                    double TViHienTai = ((ViHienTai) - (Tongcong));
                                    TongTien = TViAFF;
                                    TongTien2 = TViHienTai;
                                }

                            }
                        }
                    }
                    double Tongconlai = (SoTien - TongCK);
                    System.Web.HttpContext.Current.Session["ViAFF"] = null;
                    System.Web.HttpContext.Current.Session["ViHienTai"] = null;
                    System.Web.HttpContext.Current.Session["ViVip"] = null;
                    if (TongTien < 0 || TongTien2 < 0)
                    {
                        if (TongCong > Tongconlai)
                        {
                            Response.Write("<script type=\"text/javascript\">alert('Quý khách vui lòng chuyển điểm từ (VÍ THƯƠNG MẠI) sang (VÍ MUA HÀNG) để tiếp tục thanh toán đơn hàng này.');</script>");
                            return;
                        }
                        if (TongCong == Tongconlai)
                        {
                            Response.Write("<script type=\"text/javascript\">alert('Quý khách vui lòng chuyển điểm từ (VÍ THƯƠNG MẠI) sang (VÍ MUA HÀNG) để tiếp tục thanh toán đơn hàng này.');</script>");
                            return;
                        }
                        if (TongCong < Tongconlai)
                        {
                            Response.Write("<script type=\"text/javascript\">alert('Tài khoản của bạn không đủ để mua hàng. Quý khách vui lòng nạp thêm điểm để tiếp tục thanh toán.');window.location.href='/gio-hang.html';</script>");
                            return;
                        }
                    }
                    #endregion
                }


                if (this.txtName.Text.Length < 1)
                {
                    this.lblMsg.Text = "Bạn phải nhập họ t\x00ean!!!";
                }
                else if (this.txtAddress.Text.Length < 1)
                {
                    this.lblMsg.Text = "Bạn phải nhập địa chỉ!!!";
                }
                else if (this.txtPhone.Text.Length < 1)
                {
                    this.lblMsg.Text = "Bạn phải nhập số điện thoại li\x00ean hệ!!!";
                }
                else if (this.txtEmail.Text.Length < 1)
                {
                    this.lblMsg.Text = "Bạn phải nhập địa chỉ email!!!";
                }
                else if (!ValidateUtilities.IsValidEmail(this.txtEmail.Text.Trim()))
                {
                    this.lblMsg.Text = "Địa chỉ email kh\x00f4ng hợp lệ!!!";
                }
                else
                {
                    // btnSendOrder.Enabled = false;
                    string chuoi1 = "";
                    string chuoi2 = "";
                    Session["Phuongthucvanchuyen"] = chuoi1;
                    Session["Hinhthucthanhtoan"] = chuoi2;

                    ThanhtoanGiohang();
                }
                //}
            }
        }
        protected void ThanhtoanGiohang()
        {
            double num = 0.0;
            double num2 = 0;
            double TongTienChienLuoc = 0;
            string inumofproducts = "0";
            string totalvnd = "0";
            DataTable dtcart = (DataTable)Session["cart"];
            if (Session["cart"] != null)
            {
                if (dtcart.Rows.Count > 0)
                {
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                        num2 += Convert.ToDouble(dtcart.Rows[i]["Quantity"].ToString());
                    }
                    totalvnd = num.ToString();
                    inumofproducts = num2.ToString();
                }
            }
            // 2. them chi tiet gio hang
            Carts obj = new Carts();
            #region MyRegion
            string chuoi1 = "";
            string chuoi2 = "";
            #endregion

            double Tongtien = Convert.ToDouble(TongTienChienLuoc) / 1000;

            int cartid = 0;

            #region cartid
            try
            {
                Entity.CartDetail oj = new Entity.CartDetail();
                if (Session["cart"] != null)
                {
                    cartid = FCarts.Insert(this.txtName.Text.Trim(), this.txtAddress.Text.Trim(), this.txtPhone.Text.Trim(), this.txtEmail.Text.Trim(), this.txtnoidung.Text.Trim(), hdtongtien.Value, language, "0", chuoi1, chuoi2, "0", hdidthanhvien.Value, ddltinhthanh.SelectedValue, Tongtien.ToString());
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        #region MyRegion
                        oj.ID_Cart = int.Parse(cartid.ToString());
                        oj.ipid = int.Parse(dtcart.Rows[i]["PID"].ToString());
                        oj.Price = Convert.ToSingle(dtcart.Rows[i]["Price"].ToString());
                        oj.Quantity = Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                        oj.Money = Convert.ToSingle(dtcart.Rows[i]["Money"].ToString());
                        oj.Mausac = int.Parse(dtcart.Rows[i]["Mausac"].ToString());
                        oj.Kichco = int.Parse(dtcart.Rows[i]["Kichco"].ToString());
                        oj.GiaThanhVien = 0;
                        oj.Diemcoin = 0;

                        oj.HoaHongTheoLevel = 0;

                        oj.IDThanhVien = int.Parse(hdidthanhvien.Value);
                        string IDNhaCungCap = ShowNhaCungCap(dtcart.Rows[i]["PID"].ToString());
                        if (IDNhaCungCap != "0")
                        {
                            oj.IDNhaCungCap = int.Parse(IDNhaCungCap);
                        }
                        else
                        {
                            oj.IDNhaCungCap = 0;
                        }

                        oj.TrangThaiAgLang = 0;
                        oj.TrangThaiNhaCungCap = 0;//Trạng thái nhà cung cấp,1: duyệt đơn, 2= hủy đơn, 3= chờ xử lý
                        oj.LyDoHuyHang = "";
                        oj.TrangThaiNguoiMuaHang = 3;//Trạng thái Người mua hàng,1: duyệt đơn, 2= hủy đơn, 3= chờ xử lý
                        oj.LyDoTraHang = "";
                        oj.TrangThaiKhieuKien = 0;
                        oj.SentMail = 0;
                        oj.NoiDungGiaoHang = "";
                        oj.TienTuViNao = 0;// 1 Ví AFF , 2 Ví thương mại

                        oj.TongTienThanhToan = "0";
                        oj.TangThanhVienFree = "0";
                        oj.ChietKhauChoDaiLy = "0";
                        oj.TongDiemDemDiChia = "0";
                        oj.ThanhVienFree_DaiLy = int.Parse(hdcuahang.Value);
                        oj.CongDiemVechoAg = "0";
                        oj.ThanhToanNCC = "0";
                        oj.ChietKhauVip = "0";
                        oj.SanPhamChienLuoc = 0;
                        #endregion
                        int IDCartDetail = FCartDetail.Insert(oj);
                        if (IDCartDetail != 0)
                        {
                            #region Tính tiền trừ vào bảng User TongTienCoinDuocCap
                            List<Entity.users> iiit = Susers.Name_Text("select * from users where iuser_id=" + hdidthanhvien.Value + " ");
                            if (iiit.Count > 0)
                            {
                                double ViHienTai = Convert.ToDouble(iiit[0].TongTienCoinDuocCap);
                                double ViAFF = Convert.ToDouble(iiit[0].ViMuaHangAFF);// lấy hoa hồng quản lý (AFF) đi mua hàng
                                double ViTangTienVip = Convert.ToDouble(iiit[0].ViTangTienVip);

                                double TongVi = 0;
                                int LayTienTuViNao = 0;

                                // 0 : Vi AFF
                                // 1: Ví Thương mại

                                double Money = Convert.ToDouble(dtcart.Rows[i]["money"].ToString()) / 1000;

                                double SoTienCoin = 0;
                                SoTienCoin = (Money);
                                // Quy đổi VND ra số Coin
                                // double SoTienPhaiThanhToanCoin = Convert.ToDouble(hdtongtien.Value);
                                // double SoTienCoin = (SoTienPhaiThanhToanCoin);/// 1000;

                                double Tong = 0;
                                double TongCK = 0;
                                //if (ViTangTienVip >= ChietKhauVips)
                                //{
                                //    if (hdViVips.Value == "1")
                                //    {
                                //        Tong = ((ViTangTienVip) - (ChietKhauVips));
                                //        TongCK = ChietKhauVips;// lấy ra dc chiết khấu
                                //    }
                                //}
                                double TongTiensss = SoTienCoin - TongCK;

                                if (ViAFF >= TongTiensss)
                                {
                                    TongVi = ViAFF;
                                    LayTienTuViNao = 0;
                                }
                                else if (ViHienTai >= TongTiensss)
                                {
                                    TongVi = ViHienTai;
                                    LayTienTuViNao = 1;
                                }

                                if (TongVi >= TongTiensss)
                                {
                                    //if (hdViVips.Value == "1")
                                    //{
                                    //    Susers.Name_Text("update users set ViTangTienVip='" + Tong + "'  where iuser_id=" + hdidthanhvien.Value + "");
                                    //}
                                    double ConglaiCoin = ((TongVi) - (TongTiensss));
                                    if (LayTienTuViNao == 0)// AFF
                                    {
                                        //ViMuaHangAFF
                                        Susers.Name_Text("update users set ViMuaHangAFF='" + ConglaiCoin + "'  where iuser_id=" + hdidthanhvien.Value + "");
                                        SCartDetail.Name_Text("update CartDetail set TienTuViNao=1  where ID=" + IDCartDetail + "");
                                    }
                                    else// Ví thương mại
                                    {
                                        Susers.Name_Text("update users set TongTienCoinDuocCap='" + ConglaiCoin + "'  where iuser_id=" + hdidthanhvien.Value + "");
                                        //TienTuViNao 2 là ví Thương mại
                                        SCartDetail.Name_Text("update CartDetail set TienTuViNao=2  where ID=" + IDCartDetail + "");
                                    }

                                    //double TT1 = Convert.ToDouble(GiaNhap(dtcart.Rows[i]["PID"].ToString(), dtcart.Rows[i]["Quantity"].ToString()));
                                    //double VSoTienNhaCCSeNhan = (TT1) / 1000;
                                    //double SoTienNguoiMuaBiTru = (TongTiensss);/// 1000;

                                    //ViTamMuaHang obi = new ViTamMuaHang();
                                    //obi.IDCarts = cartid;
                                    //obi.IDCartDetail = IDCartDetail;
                                    //obi.IDSanPham = Convert.ToInt32(dtcart.Rows[i]["PID"].ToString());
                                    //obi.IDThanhVienMua = Convert.ToInt32(hdidthanhvien.Value);
                                    //obi.IDNhaCungCap = Convert.ToInt32(IDNhaCungCap);
                                    //obi.SoTienNhaCCSeNhan = VSoTienNhaCCSeNhan.ToString();
                                    //obi.SoTienNguoiMuaBiTru = SoTienNguoiMuaBiTru.ToString();
                                    //obi.SoDiemThuong = dtcart.Rows[i]["TongDiemcoin"].ToString();
                                    //obi.NgayCapNhat = DateTime.Now;
                                    //obi.LayTienOVi = LayTienTuViNao;
                                    //obi.NCCDuyet = 1;// 1 là thành viên đi mua hàng, 2 là nhà cc chấp nhận . sau 3 ngày ncc ko duyệt thì quyét trạng thái 1 và trả lại tiền cho người mua.
                                    //obi.ChietKhauVip = TongCK.ToString();
                                    //db.ViTamMuaHangs.InsertOnSubmit(obi);
                                    //db.SubmitChanges();
                                }
                            }
                            #endregion
                            #region Thanh toán tiền cho ncc
                            List<Entity.users> thanhtoan = Susers.Name_Text("select * from users where iuser_id=" + int.Parse(IDNhaCungCap) + " ");
                            if (thanhtoan.Count > 0)
                            {
                                double Money = Convert.ToDouble(dtcart.Rows[i]["money"].ToString()) / 1000;
                                double TongTienCoinDuocCap = Convert.ToDouble(thanhtoan[0].TongTienCoinDuocCap.ToString());
                                double ConglaiCoin = ((TongTienCoinDuocCap) + (Money));
                                Susers.Name_Text("update users set TongTienCoinDuocCap='" + ConglaiCoin + "'  where iuser_id=" + thanhtoan[0].iuser_id.ToString() + "");
                            }
                            #endregion
                        }


                        #region Sent Mail Cho các Nhà Cung Cấp
                        var dt = db.S_CartDetail_SentMail(int.Parse(cartid.ToString()));
                        // List<Entity.CartDetail> dt = SCartDetail.Name_Text("SELECT DISTINCT IDNhaCungCap FROM CartDetail where ID_Cart=" + cartid.ToString() + " and SentMail=0");
                        if (dt != null)
                        {
                            foreach (var item in dt)
                            {
                                List<CartDetail> dt2 = db.CartDetails.Where(s => s.ID_Cart == int.Parse(cartid.ToString()) && s.SentMail == 0 && s.IDNhaCungCap == int.Parse(item.IDNhaCungCap.ToString())).ToList();
                                // List<Entity.CartDetail> dt2 = SCartDetail.Name_Text("select * from CartDetail where ID_Cart=" + cartid.ToString() + " and SentMail=0 and IDNhaCungCap=" + item.IDNhaCungCap.ToString() + "");
                                if (dt2 != null)
                                {
                                    string Chuoisp = "";
                                    foreach (var item2 in dt2)
                                    {
                                        decimal Tien = Convert.ToDecimal(item2.Price.ToString());
                                        Chuoisp += "<b>" + ShowName(item2.ipid.ToString()) + "</b><br />";
                                        Chuoisp += "Số lượng: " + item2.Quantity.ToString() + "<br />";
                                        Chuoisp += "Số tiền: " + string.Format("{0:n0}", Tien) + "<br /><br />";
                                    }

                                    if (ShowEmailNhaCungCap(dt2[0].IDNhaCungCap.ToString()) != "0")
                                    {
                                        SendMailNhaCungCap(Chuoisp, cartid.ToString(), ShowEmailNhaCungCap(dt2[0].IDNhaCungCap.ToString()));
                                    }

                                    // cập nhật là đã gửi mail
                                    SCartDetail.Name_Text("Update CartDetail set SentMail=1 where ID_Cart=" + cartid.ToString() + "  and IDNhaCungCap=" + item.IDNhaCungCap.ToString() + "");
                                }
                            }
                        }
                        #endregion

                        try
                        {
                            if (!Commond.Setting("Emailden").Equals(""))
                                Senmail(cartid.ToString());
                        }
                        catch (Exception)
                        { }
                        //else
                        //{
                        //    System.Web.HttpContext.Current.Session["cart"] = null;
                        //    base.Response.Redirect("/Message.html");
                        //}
                    }

                }
            }
            catch (Exception)
            { }
            #endregion

            System.Web.HttpContext.Current.Session["cart"] = null;
            System.Web.HttpContext.Current.Session["Session_Size"] = null;
            System.Web.HttpContext.Current.Session["Session_MauSac"] = null;
            base.Response.Redirect("/Ordersucess.html");
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
        #region Các hàm phục vụ tính hoa hồng ở giỏ hang
        protected string CapoLevelHoaHong(string level)
        {
            List<Entity.Menu> cdd = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.LV + "'  and Views=" + level + " and lang='VIE' order by level,Orders asc");
            if (cdd != null)
            {
                return cdd[0].Noidung1.ToString();// ID chính là thuộc cấp độ mấy do tiêu đề ghi
            }
            return "0";
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
        protected string ShowTrangThaiAgLang(string id)
        {
            string str = "1";
            List<Entity.Products> dt = SProducts.Name_Text("select * from products  where ipid=" + id + "");
            if (dt.Count >= 1)
            {
                str = dt[0].TrangThaiAgLang.ToString();
            }
            return str;
        }
        #endregion

        protected void Senmail(string cartid)
        {
            try
            {
                System.Text.StringBuilder strb = new System.Text.StringBuilder();
                strb.AppendLine("<br />");
                strb.AppendLine("Chúc mừng đơn hàng <a href=\"http://aggroupusa.com/account/orders/" + cartid.ToString() + "\" target=\"_blank\"><b>#" + cartid.ToString() + "</b></a> của bạn đã được đặt thành công. <br />");
                strb.AppendLine("Chúng tôi đã gửi thông báo tới nhà cung cấp sản phẩm này. Nhà cung cấp sẽ xác nhận đơn hàng trong vòng 1-3 ngày. !<br />");//Trong vòng 3 ngày mà nhà cung cấp không chấp nhận đơn hàng thì hệ thống sẽ tự động hủy đơn này và hoàn tiền cho người mua.
                strb.AppendLine("<br />");

                strb.AppendLine("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"color: rgb(0, 0, 0); font-family: &quot;Times New Roman&quot;; font-size: medium; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; border-collapse: collapse; width: 569pt;\" width=\"758\">");
                strb.AppendLine("   <tr height=\"32\" style=\"height: 24pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"32\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 12pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center; height: 24pt;\"></td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"32\" style=\"height: 24pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"32\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 12pt; font-weight: 700; font-style: normal; text-decoration: none; font-family:Helvetica Neue,Helvetica,Lucida Grande,tahoma,verdana,arial,sans-serif; vertical-align: middle; white-space: nowrap; text-align: center; height: 24pt;\">BẢNG CHI TIẾT ĐƠN HÀNG - MÃ ĐƠN HÀNG #" + cartid + " </td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"32\" style=\"height: 24pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"32\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 12pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left; height: 24pt;\"><b>Website</b>: <span style=\"color:red\">" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "</span><br /></td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Khách hàng</b>: " + this.txtName.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Địa chỉ</b>: " + this.txtAddress.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Điện thoại</b>: " + this.txtPhone.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Email</b>: " + this.txtEmail.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                //try
                //{
                //    strb.AppendLine("    <tr height=\"26\" style=\"height: 19.5pt;\">");
                //    strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Phương thức vận chuyển</b>: " + Session["Phuongthucvanchuyen"] + "</td>");
                //    strb.AppendLine("   </tr>");

                //    strb.AppendLine("    <tr height=\"26\" style=\"height: 19.5pt;\">");
                //    strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Hình thức vận chuyển</b>: " + Session["Hinhthucthanhtoan"] + "</td>");
                //    strb.AppendLine("   </tr>");
                //}
                //catch (Exception)
                //{ }
                strb.AppendLine("      <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Nội dung</b> : " + this.txtnoidung.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("      <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt; color:red; font-weight:bold; text-transform:uppercase\">Thông tin đơn hàng:</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"45\" style=\"height: 33.75pt;\">");
                strb.AppendLine("       <td height=\"45\" style=\"border-style: solid; border-color: windowtext; border-width: 1pt 0.5pt 1pt 1pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center; height: 33.75pt;\">STT</td>");
                strb.AppendLine("       <td  style=\"border-style: solid none; border-top-color: windowtext; border-right-color: inherit; border-bottom-color: windowtext; border-left-color: inherit; border-width: 1pt medium; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center;\">Mã SP</td>");
                strb.AppendLine("       <td style=\"border-style: solid none solid solid; border-top-color: windowtext; border-right-color: inherit; border-bottom-color: windowtext; border-left-color: windowtext; border-width: 1pt medium 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: normal; text-align: center; width: 210pt;\" width=\"280\">Tên sản phẩm</td>");
                strb.AppendLine("       <td style=\"border-style: solid; border-color: windowtext; border-width: 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: normal; text-align: center; width: 100px;\" width=\"53\">Số lượng</td>");
                strb.AppendLine("       <td style=\"border-style: solid; border-color: windowtext; border-width: 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: normal; text-align: center; width: 100px;\" width=\"50\">Đ.Vtính</td>");

                strb.AppendLine("       <td style=\"border-style: solid; border-color: windowtext; border-width: 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: normal; text-align: center; width: 100px;\" width=\"65\">Đơn giá</td>");
                strb.AppendLine("       <td style=\"border-style: solid; border-color: windowtext; border-width: 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: normal; text-align: center; width: 100px;\" width=\"65\">Điểm thanh toán</td>");
                strb.AppendLine("       <td style=\"border-style: solid; border-color: windowtext; border-width: 1pt 1pt 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: normal; text-align: center; width: 150pt;\">Thành tiền</td>");
                strb.AppendLine("   </tr>");
                DataTable dtcart = new DataTable();
                dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (Session["cart"] != null)
                {
                    if (dtcart.Rows.Count > 0)
                    {
                        int j = 1;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            strb.AppendLine("   <tr height=\"28\" style=\"height: 21pt;\">");
                            strb.AppendLine("       <td height=\"28\" style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center; height: 21pt;\">" + j++ + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center;\">" + Name_Code(dtcart.Rows[i]["PID"].ToString(), i) + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: left;\">" + Name_Product(dtcart.Rows[i]["PID"].ToString(), i) + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center;\">" + dtcart.Rows[i]["Quantity"].ToString() + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center;\">VNĐ</td>");
                            strb.AppendLine("       <td  style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: normal; text-align: center; width: 49pt;\" width=\"65\">" + AllQuery.MorePro.FormatMoney_NO(dtcart.Rows[i]["Price"].ToString()) + "</td>");
                            strb.AppendLine("       <td  style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: normal; text-align: center; width: 49pt;\" width=\"65\">" + FormatMoneyDiemMuaHang(dtcart.Rows[i]["Price"].ToString()) + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 11pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; vertical-align: bottom; white-space: nowrap; text-align: center;\" width=\"76\">" + AllQuery.MorePro.FormatMoney_NO(dtcart.Rows[i]["Money"].ToString()) + "</td>");
                            strb.AppendLine("   </tr>");
                        }
                    }
                }
                string Soluong = "0";
                string Tongtien = "0";
                dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    double num = 0.0;
                    int num2 = 0;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                        num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                    }
                    Tongtien = num.ToString();
                    Soluong = num2.ToString();
                }
                strb.AppendLine("   <tr height=\"33\" style=\"height: 24.75pt;\">");
                strb.AppendLine("       <td height=\"33\" style=\"border-style: solid none solid solid; border-top-color: windowtext; border-right-color: inherit; border-bottom-color: windowtext; border-left-color: windowtext; border-width: 0.5pt medium 0.5pt 1pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center; height: 24.75pt;\">&nbsp;</td>");
                strb.AppendLine("       <td  colspan=\"2\" style=\"border: 1px solid rgb(0, 0, 0); font-size: 10pt; font-weight: 700; font-style: normal; text-align: center;\">Tổng cộng</td>");

                strb.AppendLine("       <td style=\"border: 0.5pt solid windowtext; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center;\">" + Soluong + "</td>");
                strb.AppendLine("       <td colspan=\"3\" style=\"border: 0.5pt solid windowtext; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center;\">&nbsp;</td>");
                strb.AppendLine("       <td  colspan=\"1\" style=\"border-style: solid; border-color: windowtext; border-width: 0.5pt 1px 0.5pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; ; vertical-align: middle; white-space: nowrap; text-align: center; border-image: initial;\">" + AllQuery.MorePro.FormatMoney_NO(Tongtien) + " VNĐ</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"39\" style=\"height: 29.25pt;\">");
                strb.AppendLine("       <td  colspan=\"8\" style=\"font-size: 10pt; font-weight: 700; font-style: normal; border: 1px solid rgb(0, 0, 0);  text-align: center;color:red\">Tổng số tiền (viết bằng chữ): " + ConvertSoRaChu(int.Parse(Tongtien)) + ".</td>");
                strb.AppendLine("   </tr>");


                strb.AppendLine("</table>");

                strb.AppendLine("<br />");

                strb.AppendLine(Commond.Setting("txtfooterEmail"));

                string email = Email.email();
                string password = Email.password();
                int port = Convert.ToInt32(Email.port());
                string host = Email.host();


                var toEmail = Commond.Setting("Emailden");
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ " + Request.Url.Host + " - Mã đơn hàng #" + cartid + " ", strb.ToString());
                new MailHelper().SendMail(txtEmail.Text.Trim(), "Đơn hàng mới từ " + Request.Url.Host + " - Mã đơn hàng #" + cartid + " ", strb.ToString());

                //MailUtilities.SendMail("Thông tin đặt hàng trên website " + Request.Url.Host + " từ: " + txtName.Text + " - Mã đơn hàng #" + cartid + " ", email, password, Commond.Setting("Emailden"), host, port, "Thông tin đặt hàng trên website " + Request.Url.Host + " từ: " + txtName.Text, strb.ToString());
                // MailUtilities.SendMail("Thông tin đặt hàng trên website " + Request.Url.Host + " từ: " + txtName.Text + " - Mã đơn hàng #" + cartid + " ", email, password, txtEmail.Text.Trim(), host, port, "Thông tin đặt hàng trên website " + Request.Url.Host + " từ: " + txtName.Text, strb.ToString());

            }
            catch (Exception)
            { }
        }
        protected void SendMailNhaCungCap(string Chuoisp, string MaDonHang, string EmailNhaCungCap)
        {
            string Noidung = "";
            Noidung += "Kính gửi: " + txtName.Text + "<br /><br />";

            Noidung += "Bạn có một đơn hàng mới. Hãy vào xác nhận đơn hàng. Nếu như bạn không phản hồi trong 3 ngày mà nhà cung cấp không chấp nhận đơn hàng thì hệ thống sẽ tự động hủy đơn này và hoàn tiền cho người mua.!<br /><br />";

            Noidung += txtName.Text != "" ? "<b>Người mua hàng: </b>" + txtName.Text + "<br />" : "";
            Noidung += txtAddress.Text != "" ? "<b>Địa chỉ: </b>" + txtAddress.Text + "<br />" : "";
            Noidung += txtPhone.Text != "" ? "<b>Điện thoại: </b>" + txtPhone.Text + "<br />" : "";
            Noidung += txtEmail.Text != "" ? "<b>Email: </b>" + txtEmail.Text + "<br />" : "";
            Noidung += txtnoidung.Text != "" ? "<b>Nội dung: </b>" + txtnoidung.Text + "<br />" : "";

            Noidung += "<br />";
            Noidung += "<br />" + Chuoisp;
            Noidung += "<br />";
            Noidung += "<br />";
            Noidung += Commond.Setting("txtfooterEmail");
            Noidung += "<br />";
            Noidung += "<br />";
            try
            {
                new MailHelper().SendMail(EmailNhaCungCap, "Đơn hàng mới từ " + Request.Url.Host + " - Mã đơn hàng #" + MaDonHang + " ", Noidung.ToString());
            }
            catch { }
        }
        public string Name_Product(string pid, int i)
        {
            string code = "";
            DataTable dt = new DataTable();
            SProducts.Detail_ID(dt, pid);
            if (dt.Rows.Count > 0)
            {
                code = dt.Rows[0]["Name"].ToString();
            }
            return code;
        }
        public string ShowName(string pid)
        {
            string code = "";
            List<Entity.Products> dt = SProducts.GetById(pid);
            if (dt.Count > 0)
            {
                code = dt[0].Name.ToString();
            }
            return code;
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
        public string Name_Code(string pid, int i)
        {
            string code = "";
            DataTable dt = new DataTable();
            SProducts.Detail_ID(dt, pid);
            if (dt.Rows.Count > 0)
            {
                code = dt.Rows[0]["Code"].ToString();
            }
            return code;
        }
        protected void btnEditCart_Click(object sender, EventArgs e)
        {
            this.pnmessage.Visible = false;
            this.pncart.Visible = true;
        }

        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('X\x00f3a sản phẩm n\x00e0y?')";
        }

        protected void Empty_Load(object sender, EventArgs e)
        {
            ((Button)sender).Attributes["onclick"] = "return confirm('Hủy bỏ giỏ h\x00e0ng?')";
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            {
                DataTable dtcart = new DataTable();
                string str = e.CommandName.ToString();
                string pid = e.CommandArgument.ToString();
                string str3 = str;
                if (str3 != null)
                {
                    if (str3 == "delete")
                    {
                        dtcart = (DataTable)HttpContext.Current.Session["cart"];
                        SessionCarts.ShoppingCart_RemoveProduct(e.CommandArgument.ToString());
                        Response.Redirect("/gio-hang.html");
                        HttpContext.Current.Session["cart"] = dtcart;
                    }
                    if (str3 == "update")
                    {
                        if ((HttpContext.Current.Request.Form[pid] != null) && ValidateUtilities.IsValidInt(HttpContext.Current.Request.Form[pid].ToString().Trim()))
                        {
                            dtcart = (DataTable)HttpContext.Current.Session["cart"];
                            SessionCarts.Cart_Updatequantity(ref dtcart, pid, HttpContext.Current.Request.Form[pid].ToString().Trim());
                            HttpContext.Current.Session["cart"] = dtcart;
                        }
                    }
                }
                LoadCart();
            }
        }

        protected void rpmobile_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            {
                DataTable dtcart = new DataTable();
                string str = e.CommandName.ToString();
                string pid = e.CommandArgument.ToString();
                string str3 = str;
                if (str3 != null)
                {
                    if (str3 == "delete")
                    {
                        dtcart = (DataTable)HttpContext.Current.Session["cart"];
                        SessionCarts.ShoppingCart_RemoveProduct(e.CommandArgument.ToString());
                        Response.Redirect("/gio-hang.html");
                        HttpContext.Current.Session["cart"] = dtcart;
                    }
                    if (str3 == "update")
                    {
                        if ((HttpContext.Current.Request.Form[pid] != null) && ValidateUtilities.IsValidInt(HttpContext.Current.Request.Form[pid].ToString().Trim()))
                        {
                            dtcart = (DataTable)HttpContext.Current.Session["cart"];
                            {
                                SessionCarts.Cart_Updatequantity(ref dtcart, pid, HttpContext.Current.Request.Form[pid].ToString().Trim());
                            }
                            HttpContext.Current.Session["cart"] = dtcart;
                        }
                    }
                }
                LoadCart();
            }
        }

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            this.btndelete_Click(sender, e);
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["cart"] = null;
            base.Response.Redirect("/Message.html");
        }

        protected void _btctnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");
        }

        protected void btnext_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");
        }

        protected string Kichthuoc(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count > 0)
            {
                str += "<div class=\"Kichhuoc\"><a class=\"size active\"><span>" + dt[0].Name.ToString() + "</span><div class=\"pl\"><img src=\"/Resources/images/activee.png\" /></div></a></div>";
            }
            return str.ToString();
        }

        protected string Mausac(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count > 0)
            {
                str += "<div class=\"Mausac\"><a class=\"Color active\"><img src=\"" + dt[0].Images.ToString() + "\" style=\"width:28px; height:28px;border:solid 1px #d7d7d7\" /><div class=\"pl\"><img src=\"/Resources/images/activee.png\" style=' height: 16px !important;width: 18px !important;' /></div></a></div>";
                str += "";
            }
            return str.ToString();
        }

        protected void txtxQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox Quantity = (TextBox)sender;
            var b = (HiddenField)Quantity.FindControl("hiID");
            DataTable dtcart = new DataTable();
            dtcart = (DataTable)HttpContext.Current.Session["cart"];
            SessionCarts.Cart_Updatequantity(ref dtcart, b.Value, Quantity.Text);
            HttpContext.Current.Session["cart"] = dtcart;
            LoadCart();
        }

        #region Hàm đổi số ra chữ
        private static string Chu(string gNumber)
        {
            string result = "";
            switch (gNumber)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }
            return result;
        }
        private static string Donvi(string so)
        {
            string Kdonvi = "";
            if (so.Equals("1"))
                Kdonvi = "";
            if (so.Equals("2"))
                Kdonvi = "nghìn";
            if (so.Equals("3"))
                Kdonvi = "triệu";
            if (so.Equals("4"))
                Kdonvi = "tỷ";
            if (so.Equals("5"))
                Kdonvi = "nghìn tỷ";
            if (so.Equals("6"))
                Kdonvi = "triệu tỷ";
            if (so.Equals("7"))
                Kdonvi = "tỷ tỷ";

            return Kdonvi;
        }
        private static string Tach(string tach3)
        {
            string Ktach = "";
            if (tach3.Equals("000"))
                return "";
            if (tach3.Length == 3)
            {
                string tr = tach3.Trim().Substring(0, 1).ToString().Trim();
                string ch = tach3.Trim().Substring(1, 1).ToString().Trim();
                string dv = tach3.Trim().Substring(2, 1).ToString().Trim();
                if (tr.Equals("0") && ch.Equals("0"))
                    Ktach = " không trăm lẻ " + Chu(dv.ToString().Trim()) + " ";
                if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm ";
                if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm lẻ " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (tr.Equals("0") && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                    Ktach = " không trăm mười ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                    Ktach = " không trăm mười lăm ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";
            }
            return Ktach;
        }
        public static string ConvertSoRaChu(double gNum)
        {
            if (gNum == 0)
                return "Không đồng";
            string lso_chu = "";
            string tach_mod = "";
            string tach_conlai = "";
            double Num = Math.Round(gNum, 0);
            string gN = Convert.ToString(Num);
            int m = Convert.ToInt32(gN.Length / 3);
            int mod = gN.Length - m * 3;
            string dau = "[+]";
            // Dau [+ , - ]
            if (gNum < 0)
                dau = "[-]";
            dau = "";
            // Tach hang lon nhat
            if (mod.Equals(1))
                tach_mod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
            if (mod.Equals(2))
                tach_mod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
            if (mod.Equals(0))
                tach_mod = "000";
            // Tach hang con lai sau mod :
            if (Num.ToString().Length > 2)
                tach_conlai = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();
            ///don vi hang mod
            int im = m + 1;
            if (mod > 0)
                lso_chu = Tach(tach_mod).ToString().Trim() + " " + Donvi(im.ToString().Trim());
            /// Tach 3 trong tach_conlai
            int i = m;
            int _m = m;
            int j = 1;
            string tach3 = "";
            string tach3_ = "";
            while (i > 0)
            {
                tach3 = tach_conlai.Trim().Substring(0, 3).Trim();
                tach3_ = tach3;
                lso_chu = lso_chu.Trim() + " " + Tach(tach3.Trim()).Trim();
                m = _m + 1 - j;
                if (!tach3_.Equals("000"))
                    lso_chu = lso_chu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                tach_conlai = tach_conlai.Trim().Substring(3, tach_conlai.Trim().Length - 3);

                i = i - 1;
                j = j + 1;
            }
            if (lso_chu.Trim().Substring(0, 1).Equals("k"))
                lso_chu = lso_chu.Trim().Substring(10, lso_chu.Trim().Length - 10).Trim();
            if (lso_chu.Trim().Substring(0, 1).Equals("l"))
                lso_chu = lso_chu.Trim().Substring(2, lso_chu.Trim().Length - 2).Trim();
            if (lso_chu.Trim().Length > 0)
                lso_chu = dau.Trim() + " " + lso_chu.Trim().Substring(0, 1).Trim().ToUpper() + lso_chu.Trim().Substring(1, lso_chu.Trim().Length - 1).Trim() + " đồng chẵn.";
            return lso_chu.ToString().Trim();
        }
        #endregion

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
                return "0";
            }
        }


        // cũ
        protected string HoaHongHienTaiLucMuaHang()
        {
            double HoaHongs = 0;
            user tables = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdidthanhvien.Value));
            if (tables != null)
            {
                string CapoLevelHoaHongs = CapoLevelHoaHong(tables.LevelThanhVien.ToString());
                HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
            }
            return HoaHongs.ToString();
        }
        protected string FormatDiemMuaHang(string Diemcoins)
        {
            double Tong = 0;
            double TongCoin = Convert.ToDouble(Diemcoins);
            //Xem thành viên đang ở level nào rồi nhân với level đó
            user tables = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdidthanhvien.Value));
            if (tables != null)
            {
                string CapoLevelHoaHongs = CapoLevelHoaHong(tables.LevelThanhVien.ToString());
                double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                Tong = (TongCoin * HoaHongs) / 100;
            }
            return Tong.ToString();
        }
        public static string TimLeader(string id)
        {
            string str = "0";
            List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + "  ");// and DuyetTienDanap =1
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



    }
}