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


                        if (table.Rows[0]["DuyetTienDanap"].ToString() == "0")
                        {
                            pnKichHoat.Visible = true;
                        }
                        else
                        {
                            pndakichhoat.Visible = true;
                        }
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
                    string coin = "";
                    string totalvnd = "";
                    string Price = "";
                    string GiaThanhVien = "";
                    string DemdichiaHH = "0";

                    string Free = "";
                    string DaiLy = "";
                    string VipChietKhau = "";
                    Double VipChietKhaus = 0;
                    double Thanhtoansaukhichietkhauchodaily = 0.0;
                    if (dtcart.Rows.Count > 0)
                    {
                        Double Frees = 0;
                        Double DaiLys = 0;

                        Double num = 0;
                        Double num3 = 0;
                        int num2 = 0;
                        Double Prices = 0;
                        Double GiaThanhViens = 0;

                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            Prices += Convert.ToDouble(dtcart.Rows[i]["Price"].ToString());
                            GiaThanhViens += Convert.ToDouble(dtcart.Rows[i]["GiaThanhVien"].ToString()) * Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                            num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                            num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                            num3 += Convert.ToDouble(dtcart.Rows[i]["TongDiemcoin"].ToString());

                            Frees += Convert.ToDouble(dtcart.Rows[i]["GiaThanhVienFree"].ToString());
                            DaiLys += Convert.ToDouble(dtcart.Rows[i]["GiaChietKhauDaiLy"].ToString());
                            if (hdthanhvienFree.Value == "0")
                            {
                                DemdichiaHH = Commond.ThanhVienTongTien(dtcart.Rows[i]["Money"].ToString(), GiaThanhViens.ToString(), Frees.ToString());
                            }
                            else
                            {
                                DemdichiaHH = Commond.ThanhVienTongTien(dtcart.Rows[i]["Money"].ToString(), GiaThanhViens.ToString(), DaiLys.ToString());
                            }
                            VipChietKhaus += Convert.ToDouble(dtcart.Rows[i]["ChietKhau"].ToString());
                            // Frees += Convert.ToDouble(Commond.ThanhVienFree_SoLuong(dtcart.Rows[i]["Quantity"].ToString(), dtcart.Rows[i]["Price"].ToString(), dtcart.Rows[i]["GiaThanhVien"].ToString()));
                            // DaiLys += Convert.ToDouble(Commond.ThanhVienDaiLy_SoLuong(dtcart.Rows[i]["Quantity"].ToString(), dtcart.Rows[i]["Price"].ToString(), dtcart.Rows[i]["GiaThanhVien"].ToString()));
                        }
                        Price = Prices.ToString();
                        GiaThanhVien = GiaThanhViens.ToString();
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                        coin = num3.ToString();
                        Free = Frees.ToString();
                        DaiLy = DaiLys.ToString();
                        VipChietKhau = VipChietKhaus.ToString();
                        // TongTien = GiaNY - GiaGoc;
                        //Response.Write("Chia cho thành viên Freee Hoa Hồng " + Commond.ThanhVienTongTien(num.ToString(), GiaThanhViens.ToString(), Frees.ToString()) + " mang đi chia hoa hồng.<br>");
                        //Response.Write("Chia cho thành viên Đại lý Hoa Hồng " + Commond.ThanhVienTongTien(num.ToString(), GiaThanhViens.ToString(), DaiLys.ToString()) + " mang đi chia hoa hồng.<br>");
                        // Response.Write("Chia cho thành viên Freee Hoa Hồng " + Free.ToString() + " mang đi chia hoa hồng.<br>");
                        // Response.Write("Chia cho thành viên Đại lý Hoa Hồng " + DemdichiaHH.ToString() + " mang đi chia hoa hồng.<br>");
                        Thanhtoansaukhichietkhauchodaily = Convert.ToDouble(FormatMoneyDiemMuaHang(num.ToString())) - DaiLys;
                    }
                    ltsotienchietkhau.Text = DaiLy.ToString();
                    this.lttotalvnd.Text = FormatMoneyDiemMuaHang(totalvnd.ToString()); //AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());
                    this.ltnumberofproducts.Text = inumofproducts;
                    Double ViVip = Convert.ToDouble(hdvivip.Value);
                    if (hdthanhvienFree.Value == "0")// giá bán cho thành viên Freee
                    {
                        Double Tong = 0;
                        // kiểm tra THƯỞNG MUA HÀNG còn tiền không mới tính nhé
                        Double TongTien = Convert.ToDouble(FormatMoneyDiemMuaHang(totalvnd.ToString()));
                        if (ViVip >= VipChietKhaus)
                        {
                            Tong = TongTien - VipChietKhaus;
                            pnvivip.Visible = true;
                            this.ltvivip.Text = VipChietKhau.ToString();
                            hdViVips.Value = "1";
                        }
                        else
                        {
                            Tong = TongTien;
                            this.ltvivip.Text = "0";
                            hdViVips.Value = "0";
                        }

                        if (Tong < 0)
                        {
                            System.Web.HttpContext.Current.Session["cart"] = null;
                            base.Response.Redirect("/Message.html");
                        }
                        this.ltTongcanthanhtoansaukhichietkhaudaily.Text = Tong.ToString();

                    }
                    else // giá cho đại lý
                    {
                        Double Tong = 0;
                        // kiểm tra THƯỞNG MUA HÀNG còn tiền không mới tính nhé
                        Double TongTien = Convert.ToDouble(Thanhtoansaukhichietkhauchodaily.ToString());
                        if (ViVip >= VipChietKhaus)
                        {
                            Tong = TongTien - VipChietKhaus;
                            pnvivip.Visible = true;
                            this.ltvivip.Text = VipChietKhau.ToString();
                            hdViVips.Value = "1";
                        }
                        else
                        {
                            Tong = TongTien;
                            this.ltvivip.Text = "0";
                            hdViVips.Value = "0";
                        }
                        if (Tong < 0)
                        {
                            System.Web.HttpContext.Current.Session["cart"] = null;
                            base.Response.Redirect("/Message.html");
                        }
                        this.ltTongcanthanhtoansaukhichietkhaudaily.Text = Tong.ToString();
                    }
                    lttangchothanhvien.Text = Free;
                    ltchietkhaudaily.Text = DaiLy;
                    hdtongtien.Value = ltTongcanthanhtoansaukhichietkhaudaily.Text;

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
                try
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
                                double GiaChietKhauDaiLy = Convert.ToDouble(dtcart.Rows[i]["GiaChietKhauDaiLy"].ToString());
                                double ChietKhau = Convert.ToDouble(dtcart.Rows[i]["ChietKhau"].ToString());

                                //double SoTien = (SoTienPhaiThanhToan);
                                // Nếu là đại lý

                                if (hdthanhvienFree.Value.ToString() == "1")
                                {
                                    SoTien = (Money) - GiaChietKhauDaiLy;
                                }
                                else
                                {
                                    SoTien = (Money);
                                }

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

                                //ViVip
                                if (Session["ViVip"] == null)
                                {
                                    ViVip = Convert.ToDouble(iiit.ViTangTienVip);
                                }
                                else
                                {
                                    ViVip = Convert.ToDouble(Session["ViVip"].ToString());
                                }

                                if (ViVip >= ChietKhau)
                                {
                                    Tong = ((ViVip) - (ChietKhau));
                                    TongCK = ChietKhau;
                                }
                                double Tongcong = SoTien - TongCK;
                                // 0 : Vi AFF
                                // 1: Ví Thương mại
                                if (ViAFF >= Tongcong)// trừ tiền ở ví hoa hồng aff
                                {
                                    double ConglaiCoin = ((ViAFF) - (Tongcong));
                                    Session["ViAFF"] = ConglaiCoin;
                                    Session["ViVip"] = Tong;

                                    TongTien = ConglaiCoin;
                                }
                                else if (ViHienTai >= Tongcong)// trừ tiền ví thương mại
                                {
                                    double ConglaiCoin = ((ViHienTai) - (Tongcong));
                                    Session["ViHienTai"] = ConglaiCoin;
                                    Session["ViVip"] = Tong;
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
                catch (Exception)
                { }

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
                        if (dtcart.Rows[i]["SanPhamChienLuoc"].ToString() == "1")
                        {
                            TongTienChienLuoc += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                        }
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
                        Double TangThanhVienFree = Convert.ToDouble(dtcart.Rows[i]["GiaThanhVienFree"].ToString());
                        Double ChietKhauChoDaiLy = Convert.ToDouble(dtcart.Rows[i]["GiaChietKhauDaiLy"].ToString());
                        Double ChietKhauVip = Convert.ToDouble(dtcart.Rows[i]["ChietKhau"].ToString());
                        Double TongTienThanhToan = 0;
                        // gia goc - gia ag
                        Double GiaAgNhanDuoc = ((Convert.ToDouble(dtcart.Rows[i]["GiaThanhVien"].ToString()) - Convert.ToDouble(dtcart.Rows[i]["GiaPhaiTraNCC"].ToString())) / 1000) * Convert.ToDouble(dtcart.Rows[i]["Quantity"].ToString());
                        Double ThanhToanNCC = (Convert.ToDouble(dtcart.Rows[i]["GiaPhaiTraNCC"].ToString()) * Convert.ToDouble(dtcart.Rows[i]["Quantity"].ToString())) / 1000;

                        string DemdichiaHH = "0";
                        if (hdthanhvienFree.Value == "0")
                        {
                            TongTienThanhToan = Convert.ToDouble(FormatMoneyDiemMuaHang(dtcart.Rows[i]["Money"].ToString()));

                            Double GiaThanhViens = Convert.ToDouble(dtcart.Rows[i]["GiaThanhVien"].ToString()) * Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                            DemdichiaHH = Commond.ThanhVienTongTien(dtcart.Rows[i]["Money"].ToString(), GiaThanhViens.ToString(), TangThanhVienFree.ToString());
                        }
                        else
                        {
                            TongTienThanhToan = Convert.ToDouble(FormatMoneyDiemMuaHang(dtcart.Rows[i]["Money"].ToString())) - Convert.ToDouble(ChietKhauChoDaiLy);
                            Double GiaThanhViens = Convert.ToDouble(dtcart.Rows[i]["GiaThanhVien"].ToString()) * Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                            DemdichiaHH = Commond.ThanhVienTongTien(dtcart.Rows[i]["Money"].ToString(), GiaThanhViens.ToString(), ChietKhauChoDaiLy.ToString());
                        }
                        double ViTangTienVips = 0;
                        List<Entity.users> it = Susers.Name_Text("select * from users where iuser_id=" + hdidthanhvien.Value + " ");
                        if (it.Count > 0)
                        {
                            ViTangTienVips = Convert.ToDouble(it[0].ViTangTienVip);
                        }

                        double Tongs = 0;
                        double TongCKs = 0;
                        if (ViTangTienVips >= ChietKhauVip)
                        {
                            if (hdViVips.Value == "1")
                            {
                                Tongs = ((ViTangTienVips) - (ChietKhauVip));
                                TongCKs = ChietKhauVip;// lấy ra dc chiết khấu
                            }
                        }
                        /// lấy điểm chiết khấu trừ vào điểm TongDiemDemDiChia
                        double TDemdichiaHH = Convert.ToDouble(DemdichiaHH) - Convert.ToDouble(TongCKs);
                        double TongTienThanhToans = Convert.ToDouble(TongTienThanhToan) - Convert.ToDouble(TongCKs);

                        if (TongTienThanhToans > 0)
                        {
                            #region MyRegion
                            string KQTrangThaiAgLang = ShowTrangThaiAgLang(dtcart.Rows[i]["PID"].ToString());
                            string hoahongs = HoaHongHienTaiLucMuaHang_News(KQTrangThaiAgLang, hdidthanhvien.Value);
                            oj.ID_Cart = int.Parse(cartid.ToString());
                            oj.ipid = int.Parse(dtcart.Rows[i]["PID"].ToString());
                            oj.Price = Convert.ToSingle(dtcart.Rows[i]["Price"].ToString());
                            oj.Quantity = Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                            oj.Money = Convert.ToSingle(dtcart.Rows[i]["Money"].ToString());
                            oj.Mausac = int.Parse(dtcart.Rows[i]["Mausac"].ToString());
                            oj.Kichco = int.Parse(dtcart.Rows[i]["Kichco"].ToString());
                            oj.GiaThanhVien = int.Parse(dtcart.Rows[i]["GiaThanhVien"].ToString());
                            oj.Diemcoin = Convert.ToSingle(dtcart.Rows[i]["TongDiemcoin"].ToString());

                            oj.HoaHongTheoLevel = int.Parse(hoahongs.ToString());

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

                            oj.TrangThaiAgLang = int.Parse(KQTrangThaiAgLang);
                            oj.TrangThaiNhaCungCap = 3;//Trạng thái nhà cung cấp,1: duyệt đơn, 2= hủy đơn, 3= chờ xử lý
                            oj.LyDoHuyHang = "";

                            oj.TrangThaiNguoiMuaHang = 3;//Trạng thái Người mua hàng,1: duyệt đơn, 2= hủy đơn, 3= chờ xử lý
                            oj.LyDoTraHang = "";
                            oj.TrangThaiKhieuKien = 0;
                            oj.SentMail = 0;
                            oj.NoiDungGiaoHang = "";
                            oj.TienTuViNao = 0;// 1 Ví AFF , 2 Ví thương mại

                            oj.TongTienThanhToan = TongTienThanhToans.ToString(); // TongTienThanhToan.ToString();
                            oj.TangThanhVienFree = TangThanhVienFree.ToString();
                            oj.ChietKhauChoDaiLy = ChietKhauChoDaiLy.ToString();
                            if (TDemdichiaHH > 0)
                            {
                                oj.TongDiemDemDiChia = TDemdichiaHH.ToString();// DemdichiaHH.ToString();
                            }
                            else
                            {
                                oj.TongDiemDemDiChia = "0";
                            }
                            oj.ThanhVienFree_DaiLy = int.Parse(hdcuahang.Value);
                            oj.CongDiemVechoAg = GiaAgNhanDuoc.ToString();
                            oj.ThanhToanNCC = ThanhToanNCC.ToString();
                            oj.ChietKhauVip = TongCKs.ToString();//ChietKhauVip.ToString();
                            oj.SanPhamChienLuoc = int.Parse(dtcart.Rows[i]["SanPhamChienLuoc"].ToString());
                            #endregion
                            int IDCartDetail = FCartDetail.Insert(oj);
                            if (IDCartDetail != 0)
                            {
                                try
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
                                        double ChietKhauVips = Convert.ToDouble(dtcart.Rows[i]["ChietKhau"].ToString());

                                        double Money = Convert.ToDouble(dtcart.Rows[i]["money"].ToString()) / 1000;
                                        double GiaChietKhauDaiLy = Convert.ToDouble(dtcart.Rows[i]["GiaChietKhauDaiLy"].ToString());

                                        double SoTienCoin = 0;
                                        if (hdthanhvienFree.Value.ToString() == "1")
                                        {
                                            SoTienCoin = (Money) - GiaChietKhauDaiLy;
                                        }
                                        else
                                        {
                                            SoTienCoin = (Money);
                                        }
                                        // Quy đổi VND ra số Coin
                                        // double SoTienPhaiThanhToanCoin = Convert.ToDouble(hdtongtien.Value);
                                        // double SoTienCoin = (SoTienPhaiThanhToanCoin);/// 1000;

                                        double Tong = 0;
                                        double TongCK = 0;
                                        if (ViTangTienVip >= ChietKhauVips)
                                        {
                                            if (hdViVips.Value == "1")
                                            {
                                                Tong = ((ViTangTienVip) - (ChietKhauVips));
                                                TongCK = ChietKhauVips;// lấy ra dc chiết khấu
                                            }
                                        }
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
                                            // Tạm thời trừ tiền và lưu vào bảng tạm
                                            if (hdViVips.Value == "1")
                                            {
                                                Susers.Name_Text("update users set ViTangTienVip='" + Tong + "'  where iuser_id=" + hdidthanhvien.Value + "");
                                            }
                                            double ConglaiCoin = ((TongVi) - (TongTiensss));
                                            if (LayTienTuViNao == 0)// AFF
                                            {
                                                // iiit.ViMuaHangAFF = ConglaiCoin.ToString();
                                                //db.SubmitChanges();
                                                //TienTuViNao 1 là ví aff
                                                Susers.Name_Text("update users set ViMuaHangAFF='" + ConglaiCoin + "'  where iuser_id=" + hdidthanhvien.Value + "");
                                                SCartDetail.Name_Text("update CartDetail set TienTuViNao=1  where ID=" + IDCartDetail + "");
                                            }
                                            else// Ví thương mại
                                            {
                                                // iiit.TongTienCoinDuocCap = ConglaiCoin.ToString();
                                                //db.SubmitChanges();
                                                Susers.Name_Text("update users set TongTienCoinDuocCap='" + ConglaiCoin + "'  where iuser_id=" + hdidthanhvien.Value + "");
                                                //TienTuViNao 2 là ví Thương mại
                                                SCartDetail.Name_Text("update CartDetail set TienTuViNao=2  where ID=" + IDCartDetail + "");
                                            }

                                            double TT1 = Convert.ToDouble(GiaNhap(dtcart.Rows[i]["PID"].ToString(), dtcart.Rows[i]["Quantity"].ToString()));
                                            double VSoTienNhaCCSeNhan = (TT1) / 1000;
                                            double SoTienNguoiMuaBiTru = (TongTiensss);/// 1000;

                                            ViTamMuaHang obi = new ViTamMuaHang();
                                            obi.IDCarts = cartid;
                                            obi.IDCartDetail = IDCartDetail;
                                            obi.IDSanPham = Convert.ToInt32(dtcart.Rows[i]["PID"].ToString());
                                            obi.IDThanhVienMua = Convert.ToInt32(hdidthanhvien.Value);
                                            obi.IDNhaCungCap = Convert.ToInt32(IDNhaCungCap);
                                            obi.SoTienNhaCCSeNhan = VSoTienNhaCCSeNhan.ToString();
                                            obi.SoTienNguoiMuaBiTru = SoTienNguoiMuaBiTru.ToString();
                                            obi.SoDiemThuong = dtcart.Rows[i]["TongDiemcoin"].ToString();
                                            obi.NgayCapNhat = DateTime.Now;
                                            obi.LayTienOVi = LayTienTuViNao;
                                            obi.NCCDuyet = 1;// 1 là thành viên đi mua hàng, 2 là nhà cc chấp nhận . sau 3 ngày ncc ko duyệt thì quyét trạng thái 1 và trả lại tiền cho người mua.
                                            obi.ChietKhauVip = TongCK.ToString();
                                            db.ViTamMuaHangs.InsertOnSubmit(obi);
                                            db.SubmitChanges();
                                        }
                                    }
                                    #endregion
                                }
                                catch (Exception)
                                { }
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
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session["cart"] = null;
                            base.Response.Redirect("/Message.html");
                        }
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
                strb.AppendLine("Chúc mừng đơn hàng <a href=\"http://aggroup365.com/account/orders/" + cartid.ToString() + "\" target=\"_blank\"><b>#" + cartid.ToString() + "</b></a> của bạn đã được đặt thành công. <br />");
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
                            if (MoreAll.MoreAll.GetCookies("Members") != "")
                            {
                                GioHangs.Cart_UpdateNumber_ThanhVienMuaTheoSoLuong(ref dtcart, pid, HttpContext.Current.Request.Form[pid].ToString().Trim(), "1");
                            }
                            else
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
                            if (MoreAll.MoreAll.GetCookies("Members") != "")
                            {
                                GioHangs.Cart_UpdateNumber_ThanhVienMuaTheoSoLuong(ref dtcart, pid, HttpContext.Current.Request.Form[pid].ToString().Trim(), "1");
                            }
                            else
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
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                GioHangs.Cart_UpdateNumber_ThanhVienMuaTheoSoLuong(ref dtcart, b.Value, Quantity.Text, "1");
            }
            else
            {
                SessionCarts.Cart_Updatequantity(ref dtcart, b.Value, Quantity.Text);
            }
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
        protected string HoaHongHienTaiLucMuaHang_News(string KieuBDS_SANPHAM, string IDThanhVien)
        {
            // <asp:ListItem Value="1">Kiểu Sản Phẩm</asp:ListItem>
            // <asp:ListItem Value="2">Kiểu Bất Động Sản</asp:ListItem>
            // lấy CapoLevelHoaHongs của thành viên F1

            double HoaHongs = 0;
            if (KieuBDS_SANPHAM == "1")// kiểu sản phẩm =1 sẽ đi tìm thành viên F1
            {
                user tables = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien));
                if (tables != null)
                {
                    if (tables.GioiThieu.ToString() != "0")
                    {
                        user vtable = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tables.GioiThieu.ToString()));
                        if (vtable != null)
                        {
                            string CapoLevelHoaHongs = CapoLevelHoaHong(vtable.LevelThanhVien.ToString());
                            HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                            //if (LogFile == "true")
                            //{
                            //    Library.WriteErrorLogCart("IDThanhVien:" + IDThanhVien + " - kiểu sản phẩm Lấy Hoa hồng F1 : HoaHongs : " + HoaHongs);
                            //}
                        }
                    }
                    else// Nếu lỗi thì lấy tạm hoa hồng của chính ng mua
                    {
                        user tablek = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien));
                        if (tablek != null)
                        {
                            string CapoLevelHoaHongs = CapoLevelHoaHong(tablek.LevelThanhVien.ToString());
                            HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                            //// ER1:
                            //if (LogFile == "true")
                            //{
                            //    Library.WriteErrorLogCart("ER1: IDThanhVien:" + IDThanhVien + " - kiểu sản phẩm Lấy Hoa hồng F1 : HoaHongs : " + HoaHongs);
                            //}
                        }
                    }
                }
            }
            else
            {
                // đi tìm thành viên nào là F1 và có trạng thái AgLAng=1 thì mới dc nhé
                user tables = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien));
                if (tables != null)
                {
                    if (tables.GioiThieu.ToString() != "0")
                    {
                        user vtable = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(TimThanhVienAG(tables.GioiThieu.ToString())));
                        if (vtable != null)
                        {
                            string CapoLevelHoaHongs = CapoLevelHoaHong(vtable.LevelThanhVien.ToString());
                            HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                            //if (LogFile == "true")
                            //{
                            //    Library.WriteErrorLogCart("IDThanhVien:" + IDThanhVien + " - kiểu BẤT ĐỘNG SẢN - Đi tìm trong hệ thống F1 đến .... n có thành viên nào là Agland thì lấy hoa hồng của ng đó F1 : HoaHongs : " + HoaHongs);
                            //}

                        }
                    }
                    else// Nếu lỗi thì lấy tạm hoa hồng của chính ng mua
                    {
                        user tablek = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien));
                        if (tablek != null)
                        {
                            string CapoLevelHoaHongs = CapoLevelHoaHong(tablek.LevelThanhVien.ToString());
                            HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                            // ER1:
                            //if (LogFile == "true")
                            //{
                            //    Library.WriteErrorLogCart("ER1: IDThanhVien:" + IDThanhVien + " - kiểu BẤT ĐỘNG SẢN - Đi tìm trong hệ thống F1 đến .... n có thành viên nào là Agland thì lấy hoa hồng của ng đó F1 : HoaHongs : " + HoaHongs);
                            //}
                        }
                    }
                }

            }
            return HoaHongs.ToString();
        }

        public string TimThanhVienAG(string id)
        {
            string str = "0";
            List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + " ");
            if (dt.Count > 0)
            {
                if (dt[0].ThanhVienAgLang.ToString() == "1")
                {
                    //if (LogFile == "true")
                    //{
                    //    Library.WriteErrorLogCart("Đã tìm thấy thành viên có trạng thái là AG Land : iuser_id : " + dt[0].iuser_id.ToString());
                    //}
                    return dt[0].iuser_id.ToString();
                }
                else
                {
                    str = dt[0].GioiThieu.ToString();
                    //if (LogFile == "true")
                    //{
                    //    Library.WriteErrorLogCart(" for .... n Tìm thành viên AG Land : GioiThieu : " + dt[0].iuser_id.ToString());
                    //}
                    return TimThanhVienAG(str);
                }
            }
            return str;
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

        protected void bntkichhoat_Click(object sender, EventArgs e)
        {
            IDMaDonTao = MoreAll.MoreAll.FormatDate_ID(DateTime.Now);

            string ThanhVienGioiThieu = "0";
            user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdidthanhvien.Value) && p.DuyetTienDanap == 0);
            if (iitem != null)
            {
                double Tiencoin = Convert.ToDouble(Commond.Setting("TienKichHoat"));
                double TienVND = (Tiencoin) * 1000;
                #region Nếu nạp tiền >=480 điển thì sẽ được kích hoạt thành viên ngay. Nếu không được thì vào admin kích hoạt nạp tiền
                double TCoin = Convert.ToDouble(Commond.Setting("TienKichHoat"));
                double TongViTien = 0;
                string Alet = "";
                double VIAAFFILIATE = Convert.ToDouble(iitem.VIAAFFILIATE);
                double TongTienCoinDuocCap = Convert.ToDouble(iitem.TongTienCoinDuocCap);

                string vinao = "0";
                if (VIAAFFILIATE >= Tiencoin)
                {
                    Alet = " Ví Quản lý ";
                    vinao = "1";
                    TongViTien = Convert.ToDouble(iitem.VIAAFFILIATE);
                }
                else if (TongTienCoinDuocCap >= Tiencoin)
                {
                    vinao = "2";
                    Alet = " Ví Thương Mại ";
                    TongViTien = Convert.ToDouble(iitem.TongTienCoinDuocCap);
                }

                if (TongViTien >= TCoin)
                {
                    #region Ví chuyên gia
                    try
                    {
                        user ChuyenGiga = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(SetVi.SetThanhVienChuyenGia()));
                        if (ChuyenGiga != null)
                        {
                            string IDBanChuyenGia = SetVi.SetThanhVienChuyenGia();
                            double HHChuyengia = Convert.ToDouble(Commond.Setting("txtbanchuyengianDangKy"));
                            double TienHH = Convert.ToDouble(Tiencoin * HHChuyengia) / 100;
                            if (HHChuyengia > 0)
                            {
                                ThemHoaHong("0", "400", "Hoa hồng quản lý - Ban Đào tạo - Chuyên gia (" + Tiencoin + ")", hdidthanhvien.Value, IDBanChuyenGia, HHChuyengia.ToString(), TienHH.ToString(), IDMaDonTao, "");
                            }
                        }
                    }
                    catch (Exception)
                    { }
                    #endregion

                    #region Ví doanh số đồng hưởng
                    try
                    {
                        string IDDongHuong = SetVi.SetViDongHuongDoanhSo();
                        double HHDH = Convert.ToDouble(Commond.Setting("txtdoanhsodonghuongDangKy"));
                        double TienHH = Convert.ToDouble(Tiencoin * HHDH) / 100;
                        if (HHDH > 0)
                        {
                            ThemHoaHong("0", "404", "Doanh số đồng hưởng (" + Tiencoin + ")", hdidthanhvien.Value, IDDongHuong, HHDH.ToString(), TienHH.ToString(), IDMaDonTao, "");
                        }
                    }
                    catch (Exception)
                    { }
                    #endregion


                    #region Hoa Hồng Quản lý
                    try
                    {
                        double thuongquanly = Convert.ToDouble(MoreAll.Other.Giatri("txtthuongquanlyThanhVien"));
                        if (!MoreAll.Other.Giatri("txtthuongquanlyThanhVien").Equals("0"))
                        {
                            double TongTien = (Tiencoin * thuongquanly) / 100;

                            ThemHoaHong("0", "406", "Hoa hồng Thưởng quản lý (" + Tiencoin + ")", hdidthanhvien.Value, SetVi.SetViThuongQuanLy(), thuongquanly.ToString(), TongTien.ToString(), IDMaDonTao, "");

                        }
                    }
                    catch (Exception)
                    { }
                    #endregion


                    //MDoanhSoDongHuong.DoanhSoDongHuongKichHoatThanhVien("480", hdidthanhvien.Value, IDMaDonTao.ToString(), "");

                    #region Thành viên và leader
                    List<Entity.users> dt = Susers.Name_Text("select * from users  where iuser_id=" + hdidthanhvien.Value + " ");//and ChiNhanh=0
                    if (dt.Count() > 0)
                    {
                        string Diemcoin = Tiencoin.ToString();
                        string IDThanhVien = hdidthanhvien.Value;
                        #region Hoa hồng cấp quản lý và F1

                        #region HHF1
                        List<Entity.users> F00 = Susers.Name_Text("select * from users  where iuser_id=" + IDThanhVien + " ");
                        if (F00.Count() > 0)
                        {
                            double TongTienNap = Tiencoin;
                            double HoaHongGioiThieuF1 = Convert.ToDouble(Commond.Setting("hoahonggttructiep"));
                            double HoaHongTrucTiep = (TongTienNap * HoaHongGioiThieuF1) / 100;
                            if (!F00[0].GioiThieu.Equals("0"))
                            {
                                #region Hoa Hồng cho người giới thiệu trực tiếp  F1 30%
                                if (HoaHongTrucTiep > 0)
                                {
                                    ThemHoaHong("0", "1", "Hoa hồng quản lý TT", IDThanhVien, F00[0].GioiThieu.ToString(), HoaHongGioiThieuF1.ToString(), HoaHongTrucTiep.ToString(), IDMaDonTao, "");
                                }
                                #endregion
                            }
                            List<Entity.users> F02 = Susers.Name_Text("select * from users  where iuser_id=" + F00[0].GioiThieu + " ");
                            if (F02.Count() > 0)
                            {
                                double HoaHongGioiThieuF2 = Convert.ToDouble(MoreAll.Other.Giatri("txtHHGTF1"));
                                double HoaHongF2 = (HoaHongTrucTiep * HoaHongGioiThieuF2) / 100;
                                if (!F02[0].GioiThieu.Equals("0"))
                                {
                                    if (HoaHongF2 > 0)
                                    {
                                        ThemHoaHong("0", "1", "Hoa hồng quản lý 1", IDThanhVien, F02[0].GioiThieu.ToString(), HoaHongGioiThieuF2.ToString(), HoaHongF2.ToString(), IDMaDonTao, "");
                                    }
                                }
                                List<Entity.users> F03 = Susers.Name_Text("select * from users  where iuser_id=" + F02[0].GioiThieu + " ");
                                if (F03.Count() > 0)
                                {
                                    double HoaHongGioiThieuF3 = Convert.ToDouble(MoreAll.Other.Giatri("txtHHGTF2"));
                                    double HoaHongF3 = (HoaHongF2 * HoaHongGioiThieuF3) / 100;
                                    if (!F03[0].GioiThieu.Equals("0"))
                                    {
                                        if (HoaHongF3 > 0)
                                        {
                                            ThemHoaHong("0", "1", "Hoa hồng quản lý 2", IDThanhVien, F03[0].GioiThieu.ToString(), HoaHongGioiThieuF3.ToString(), HoaHongF3.ToString(), IDMaDonTao, "");
                                        }
                                    }
                                    List<Entity.users> F04 = Susers.Name_Text("select * from users  where iuser_id=" + F03[0].GioiThieu + " ");
                                    if (F04.Count() > 0)
                                    {
                                        double HoaHongGioiThieuF4 = Convert.ToDouble(MoreAll.Other.Giatri("txtHHGTF3"));
                                        double HoaHongF4 = (HoaHongF3 * HoaHongGioiThieuF4) / 100;
                                        if (!F04[0].GioiThieu.Equals("0"))
                                        {
                                            if (HoaHongF4 > 0)
                                            {
                                                ThemHoaHong("0", "1", "Hoa hồng quản lý 3", IDThanhVien, F04[0].GioiThieu.ToString(), HoaHongGioiThieuF4.ToString(), HoaHongF4.ToString(), IDMaDonTao, "");
                                            }
                                        }
                                        List<Entity.users> F05 = Susers.Name_Text("select * from users  where iuser_id=" + F04[0].GioiThieu + " ");
                                        if (F05.Count() > 0)
                                        {
                                            double HoaHongGioiThieuF5 = Convert.ToDouble(MoreAll.Other.Giatri("txtHHGTF4"));
                                            double HoaHongF5 = (HoaHongF4 * HoaHongGioiThieuF5) / 100;
                                            if (!F05[0].GioiThieu.Equals("0"))
                                            {
                                                if (HoaHongF5 > 0)
                                                {
                                                    ThemHoaHong("0", "1", "Hoa hồng quản lý 4", IDThanhVien, F05[0].GioiThieu.ToString(), HoaHongGioiThieuF5.ToString(), HoaHongF5.ToString(), IDMaDonTao, "");
                                                }
                                            }
                                            List<Entity.users> F06 = Susers.Name_Text("select * from users  where iuser_id=" + F05[0].GioiThieu + " ");
                                            if (F06.Count() > 0)
                                            {
                                                double HoaHongGioiThieuF6 = Convert.ToDouble(MoreAll.Other.Giatri("txtHHGTF5"));
                                                double HoaHongF6 = (HoaHongF5 * HoaHongGioiThieuF6) / 100;
                                                if (!F06[0].GioiThieu.Equals("0"))
                                                {
                                                    if (HoaHongF6 > 0)
                                                    {
                                                        ThemHoaHong("0", "1", "Hoa hồng quản lý 5", IDThanhVien, F06[0].GioiThieu.ToString(), HoaHongGioiThieuF6.ToString(), HoaHongF6.ToString(), IDMaDonTao, "");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                        user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien));
                        if (table != null)
                        {
                            #region Hoa Hồng Gián tiếp F1
                            if (table.GioiThieu.ToString() != "0")
                            {
                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
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
                                        ThemHoaHong_ThuongLevel("0", "F1", "3", IDThanhVien, table.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(table.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                        #region Dừng nếu gặp lelvel5
                                        string leveeeel = TimLevelB(table.GioiThieu.ToString());
                                        if (leveeeel == "5")
                                        {
                                            Plevel = "45";
                                        }
                                        #endregion
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
                                if (tableTVTF2.GioiThieu.ToString() != "0")
                                {
                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                    // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                    try
                                    {
                                        if (ShowF2(tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                        {
                                            Dung = false;
                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                        }
                                        else
                                        {
                                            Dung = true;
                                            Plevel = Plevel + "," + ShowF2(tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                    ThemHoaHong_ThuongLevel("0", "F2", "3", IDThanhVien, tableTVTF2.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                }
                                            }
                                            else
                                            {
                                                ThemHoaHong_ThuongLevel("0", "F2", "3", IDThanhVien, tableTVTF2.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                            }
                                            #region Dừng nếu gặp lelvel5
                                            string leveeeel = TimLevelB(tableTVTF2.GioiThieu.ToString());
                                            if (leveeeel == "5")
                                            {
                                                Plevel = "45";
                                            }
                                            #endregion

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
                                    double TongDiemF3 = 0;
                                    if (tableTVTF3.GioiThieu.ToString() != "0")
                                    {
                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                        try
                                        {
                                            if (ShowF3(tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                            {
                                                Dung = false;
                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                            }
                                            else
                                            {
                                                Dung = true;
                                                Plevel = Plevel + "," + ShowF3(tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                        ThemHoaHong_ThuongLevel("0", "F3", "3", IDThanhVien, tableTVTF3.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                    }
                                                }
                                                else
                                                {
                                                    ThemHoaHong_ThuongLevel("0", "F3", "3", IDThanhVien, tableTVTF3.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                }
                                                #region Dừng nếu gặp lelvel5
                                                string leveeeel = TimLevelB(tableTVTF3.GioiThieu.ToString());
                                                if (leveeeel == "5")
                                                {
                                                    Plevel = "45";
                                                }
                                                #endregion
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
                                        if (tableTVTF4.GioiThieu.ToString() != "0")
                                        {
                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                            try
                                            {
                                                if (ShowF4(tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                {
                                                    Dung = false;
                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                }
                                                else
                                                {
                                                    Dung = true;
                                                    Plevel = Plevel + "," + ShowF4(tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                            ThemHoaHong_ThuongLevel("0", "F4", "3", IDThanhVien, tableTVTF4.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ThemHoaHong_ThuongLevel("0", "F4", "3", IDThanhVien, tableTVTF4.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                    }
                                                    #region Dừng nếu gặp lelvel5
                                                    string leveeeel = TimLevelB(tableTVTF4.GioiThieu.ToString());
                                                    if (leveeeel == "5")
                                                    {
                                                        Plevel = "45";
                                                    }
                                                    #endregion
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
                                            if (tableTVTF5.GioiThieu.ToString() != "0")
                                            {
                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                try
                                                {
                                                    if (ShowF5(tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                    {
                                                        Dung = false;
                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                    }
                                                    else
                                                    {
                                                        Dung = true;
                                                        Plevel = Plevel + "," + ShowF5(tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                ThemHoaHong_ThuongLevel("0", "F5", "3", IDThanhVien, tableTVTF5.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ThemHoaHong_ThuongLevel("0", "F5", "3", IDThanhVien, tableTVTF5.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                        }
                                                        #region Dừng nếu gặp lelvel5
                                                        string leveeeel = TimLevelB(tableTVTF5.GioiThieu.ToString());
                                                        if (leveeeel == "5")
                                                        {
                                                            Plevel = "45";
                                                        }
                                                        #endregion
                                                    }
                                                }
                                                catch (Exception)
                                                { }
                                                #endregion
                                            }

                                            #region Hoa Hồng Gián tiếp F6
                                            user tableTVTF6 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF5.GioiThieu.ToString()));
                                            if (tableTVTF6 != null)
                                            {
                                                if (tableTVTF6.GioiThieu.ToString() != "0")
                                                {
                                                    try
                                                    {
                                                        if (ShowFQRcode.ShowF6(tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                        {
                                                            Dung = false;
                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                        }
                                                        else
                                                        {
                                                            Dung = true;
                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF6(tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                    ThemHoaHong_ThuongLevel("0", "F6", "3", IDThanhVien, tableTVTF6.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ThemHoaHong_ThuongLevel("0", "F6", "3", IDThanhVien, tableTVTF6.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                            }
                                                            #region Dừng nếu gặp lelvel5
                                                            string leveeeel = TimLevelB(tableTVTF6.GioiThieu.ToString());
                                                            if (leveeeel == "5")
                                                            {
                                                                Plevel = "45";
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                    catch (Exception)
                                                    { }
                                                }
                                                #region Hoa Hồng Gián tiếp F7
                                                user tableTVTF7 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF6.GioiThieu.ToString()));
                                                if (tableTVTF7 != null)
                                                {
                                                    if (tableTVTF7.GioiThieu.ToString() != "0")
                                                    {
                                                        try
                                                        {
                                                            if (ShowFQRcode.ShowF7(tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                            {
                                                                Dung = false;
                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                            }
                                                            else
                                                            {
                                                                Dung = true;
                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF7(tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                        ThemHoaHong_ThuongLevel("0", "F7", "3", IDThanhVien, tableTVTF7.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ThemHoaHong_ThuongLevel("0", "F7", "3", IDThanhVien, tableTVTF7.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                }
                                                                #region Dừng nếu gặp lelvel5
                                                                string leveeeel = TimLevelB(tableTVTF7.GioiThieu.ToString());
                                                                if (leveeeel == "5")
                                                                {
                                                                    Plevel = "45";
                                                                }
                                                                #endregion
                                                            }
                                                        }
                                                        catch (Exception)
                                                        { }
                                                    }
                                                    #region Hoa Hồng Gián tiếp F8
                                                    user tableTVTF8 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF7.GioiThieu.ToString()));
                                                    if (tableTVTF8 != null)
                                                    {
                                                        if (tableTVTF8.GioiThieu.ToString() != "0")
                                                        {
                                                            try
                                                            {
                                                                if (ShowFQRcode.ShowF8(tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                {
                                                                    Dung = false;
                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                }
                                                                else
                                                                {
                                                                    Dung = true;
                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF8(tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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

                                                                            ThemHoaHong_ThuongLevel("0", "F8", "3", IDThanhVien, tableTVTF8.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ThemHoaHong_ThuongLevel("0", "F8", "3", IDThanhVien, tableTVTF8.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                    }

                                                                    #region Dừng nếu gặp lelvel5
                                                                    string leveeeel = TimLevelB(tableTVTF8.GioiThieu.ToString());
                                                                    if (leveeeel == "5")
                                                                    {
                                                                        Plevel = "45";
                                                                    }
                                                                    #endregion

                                                                }
                                                            }
                                                            catch (Exception)
                                                            { }
                                                        }

                                                        #region Hoa Hồng Gián tiếp F9
                                                        user tableTVTF9 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF8.GioiThieu.ToString()));
                                                        if (tableTVTF9 != null)
                                                        {
                                                            if (tableTVTF9.GioiThieu.ToString() != "0")
                                                            {
                                                                try
                                                                {
                                                                    if (ShowFQRcode.ShowF9(tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                    {
                                                                        Dung = false;
                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                    }
                                                                    else
                                                                    {
                                                                        Dung = true;
                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF9(tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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

                                                                                ThemHoaHong_ThuongLevel("0", "F9", "3", IDThanhVien, tableTVTF9.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            ThemHoaHong_ThuongLevel("0", "F9", "3", IDThanhVien, tableTVTF9.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                        }
                                                                        #region Dừng nếu gặp lelvel5
                                                                        string leveeeel = TimLevelB(tableTVTF9.GioiThieu.ToString());
                                                                        if (leveeeel == "5")
                                                                        {
                                                                            Plevel = "45";
                                                                        }
                                                                        #endregion
                                                                    }
                                                                }
                                                                catch (Exception)
                                                                { }
                                                            }
                                                            #region Hoa Hồng Gián tiếp F10
                                                            user tableTVTF10 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF9.GioiThieu.ToString()));
                                                            if (tableTVTF10 != null)
                                                            {
                                                                if (tableTVTF10.GioiThieu.ToString() != "0")
                                                                {
                                                                    try
                                                                    {
                                                                        if (ShowFQRcode.ShowF10(tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                        {
                                                                            Dung = false;
                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                        }
                                                                        else
                                                                        {
                                                                            Dung = true;
                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF10(tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                    ThemHoaHong_ThuongLevel("0", "F10", "3", IDThanhVien, tableTVTF10.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                ThemHoaHong_ThuongLevel("0", "F10", "3", IDThanhVien, tableTVTF10.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                            }
                                                                            #region Dừng nếu gặp lelvel5
                                                                            string leveeeel = TimLevelB(tableTVTF10.GioiThieu.ToString());
                                                                            if (leveeeel == "5")
                                                                            {
                                                                                Plevel = "45";
                                                                            }
                                                                            #endregion
                                                                        }
                                                                    }
                                                                    catch (Exception)
                                                                    { }
                                                                }
                                                                #region Hoa Hồng Gián tiếp F11
                                                                user tableTVTF11 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF10.GioiThieu.ToString()));
                                                                if (tableTVTF11 != null)
                                                                {
                                                                    if (tableTVTF11.GioiThieu.ToString() != "0")
                                                                    {
                                                                        try
                                                                        {
                                                                            if (ShowFQRcode.ShowF11(tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                            {
                                                                                Dung = false;
                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                            }
                                                                            else
                                                                            {
                                                                                Dung = true;
                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF11(tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                        ThemHoaHong_ThuongLevel("0", "F11", "3", IDThanhVien, tableTVTF11.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    ThemHoaHong_ThuongLevel("0", "F11", "3", IDThanhVien, tableTVTF11.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                }
                                                                                #region Dừng nếu gặp lelvel5
                                                                                string leveeeel = TimLevelB(tableTVTF11.GioiThieu.ToString());
                                                                                if (leveeeel == "5")
                                                                                {
                                                                                    Plevel = "45";
                                                                                }
                                                                                #endregion
                                                                            }
                                                                        }
                                                                        catch (Exception)
                                                                        { }
                                                                    }
                                                                    #region Hoa Hồng Gián tiếp F12
                                                                    user tableTVTF12 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF11.GioiThieu.ToString()));
                                                                    if (tableTVTF12 != null)
                                                                    {
                                                                        if (tableTVTF12.GioiThieu.ToString() != "0")
                                                                        {
                                                                            try
                                                                            {
                                                                                if (ShowFQRcode.ShowF12(tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                {
                                                                                    Dung = false;
                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                }
                                                                                else
                                                                                {
                                                                                    Dung = true;
                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF12(tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                            ThemHoaHong_ThuongLevel("0", "F12", "3", IDThanhVien, tableTVTF12.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        ThemHoaHong_ThuongLevel("0", "F12", "3", IDThanhVien, tableTVTF12.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                    }
                                                                                    #region Dừng nếu gặp lelvel5
                                                                                    string leveeeel = TimLevelB(tableTVTF12.GioiThieu.ToString());
                                                                                    if (leveeeel == "5")
                                                                                    {
                                                                                        Plevel = "45";
                                                                                    }
                                                                                    #endregion
                                                                                }
                                                                            }
                                                                            catch (Exception)
                                                                            { }
                                                                        }
                                                                        #region Hoa Hồng Gián tiếp tableTVTF13
                                                                        user tableTVTF13 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF12.GioiThieu.ToString()));
                                                                        if (tableTVTF13 != null)
                                                                        {
                                                                            if (tableTVTF13.GioiThieu.ToString() != "0")
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (ShowFQRcode.ShowF13(tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                    {
                                                                                        Dung = false;
                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Dung = true;
                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF13(tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                ThemHoaHong_ThuongLevel("0", "F13", "3", IDThanhVien, tableTVTF13.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            ThemHoaHong_ThuongLevel("0", "F13", "3", IDThanhVien, tableTVTF13.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                        }
                                                                                        #region Dừng nếu gặp lelvel5
                                                                                        string leveeeel = TimLevelB(tableTVTF13.GioiThieu.ToString());
                                                                                        if (leveeeel == "5")
                                                                                        {
                                                                                            Plevel = "45";
                                                                                        }
                                                                                        #endregion
                                                                                    }
                                                                                }
                                                                                catch (Exception)
                                                                                { }
                                                                            }
                                                                            #region Hoa Hồng Gián tiếp tableTVTF14
                                                                            user tableTVTF14 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF13.GioiThieu.ToString()));
                                                                            if (tableTVTF14 != null)
                                                                            {
                                                                                if (tableTVTF14.GioiThieu.ToString() != "0")
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        if (ShowFQRcode.ShowF14(tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                        {
                                                                                            Dung = false;
                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Dung = true;
                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF14(tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                    ThemHoaHong_ThuongLevel("0", "F14", "3", IDThanhVien, tableTVTF14.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                ThemHoaHong_ThuongLevel("0", "F14", "3", IDThanhVien, tableTVTF14.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                            }
                                                                                            #region Dừng nếu gặp lelvel5
                                                                                            string leveeeel = TimLevelB(tableTVTF14.GioiThieu.ToString());
                                                                                            if (leveeeel == "5")
                                                                                            {
                                                                                                Plevel = "45";
                                                                                            }
                                                                                            #endregion
                                                                                        }
                                                                                    }
                                                                                    catch (Exception)
                                                                                    { }
                                                                                }
                                                                                #region Hoa Hồng Gián tiếp tableTVTF15
                                                                                user tableTVTF15 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF14.GioiThieu.ToString()));
                                                                                if (tableTVTF15 != null)
                                                                                {
                                                                                    if (tableTVTF15.GioiThieu.ToString() != "0")
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            if (ShowFQRcode.ShowF15(tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                            {
                                                                                                Dung = false;
                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Dung = true;
                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF15(tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                        ThemHoaHong_ThuongLevel("0", "F15", "3", IDThanhVien, tableTVTF15.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    ThemHoaHong_ThuongLevel("0", "F15", "3", IDThanhVien, tableTVTF15.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                }
                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                string leveeeel = TimLevelB(tableTVTF15.GioiThieu.ToString());
                                                                                                if (leveeeel == "5")
                                                                                                {
                                                                                                    Plevel = "45";
                                                                                                }
                                                                                                #endregion
                                                                                            }
                                                                                        }
                                                                                        catch (Exception)
                                                                                        { }
                                                                                    }

                                                                                    #region Hoa Hồng Gián tiếp tableTVTF16
                                                                                    user tableTVTF16 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF15.GioiThieu.ToString()));
                                                                                    if (tableTVTF16 != null)
                                                                                    {
                                                                                        if (tableTVTF16.GioiThieu.ToString() != "0")
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                if (ShowFQRcode.ShowF16(tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                {
                                                                                                    Dung = false;
                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Dung = true;
                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF16(tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                            ThemHoaHong_ThuongLevel("0", "F16", "3", IDThanhVien, tableTVTF16.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        ThemHoaHong_ThuongLevel("0", "F16", "3", IDThanhVien, tableTVTF16.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                    }
                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                    string leveeeel = TimLevelB(tableTVTF16.GioiThieu.ToString());
                                                                                                    if (leveeeel == "5")
                                                                                                    {
                                                                                                        Plevel = "45";
                                                                                                    }
                                                                                                    #endregion
                                                                                                }
                                                                                            }
                                                                                            catch (Exception)
                                                                                            { }
                                                                                        }
                                                                                        #region Hoa Hồng Gián tiếp tableTVTF17
                                                                                        user tableTVTF17 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF16.GioiThieu.ToString()));
                                                                                        if (tableTVTF17 != null)
                                                                                        {
                                                                                            if (tableTVTF17.GioiThieu.ToString() != "0")
                                                                                            {
                                                                                                try
                                                                                                {
                                                                                                    if (ShowFQRcode.ShowF17(tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                    {
                                                                                                        Dung = false;
                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Dung = true;
                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF17(tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                ThemHoaHong_ThuongLevel("0", "F17", "3", IDThanhVien, tableTVTF17.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            ThemHoaHong_ThuongLevel("0", "F17", "3", IDThanhVien, tableTVTF17.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                        }
                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                        string leveeeel = TimLevelB(tableTVTF17.GioiThieu.ToString());
                                                                                                        if (leveeeel == "5")
                                                                                                        {
                                                                                                            Plevel = "45";
                                                                                                        }
                                                                                                        #endregion
                                                                                                    }
                                                                                                }
                                                                                                catch (Exception)
                                                                                                { }
                                                                                            }

                                                                                            #region Hoa Hồng Gián tiếp tableTVTF18
                                                                                            user tableTVTF18 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF17.GioiThieu.ToString()));
                                                                                            if (tableTVTF18 != null)
                                                                                            {
                                                                                                if (tableTVTF18.GioiThieu.ToString() != "0")
                                                                                                {
                                                                                                    try
                                                                                                    {
                                                                                                        if (ShowFQRcode.ShowF18(tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                        {
                                                                                                            Dung = false;
                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            Dung = true;
                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF18(tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                    ThemHoaHong_ThuongLevel("0", "F18", "3", IDThanhVien, tableTVTF18.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                ThemHoaHong_ThuongLevel("0", "F18", "3", IDThanhVien, tableTVTF18.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                            }
                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                            string leveeeel = TimLevelB(tableTVTF18.GioiThieu.ToString());
                                                                                                            if (leveeeel == "5")
                                                                                                            {
                                                                                                                Plevel = "45";
                                                                                                            }
                                                                                                            #endregion
                                                                                                        }
                                                                                                    }
                                                                                                    catch (Exception)
                                                                                                    { }
                                                                                                }

                                                                                                #region Hoa Hồng Gián tiếp tableTVTF19
                                                                                                user tableTVTF19 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF18.GioiThieu.ToString()));
                                                                                                if (tableTVTF19 != null)
                                                                                                {
                                                                                                    if (tableTVTF19.GioiThieu.ToString() != "0")
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (ShowFQRcode.ShowF19(tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                            {
                                                                                                                Dung = false;
                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                Dung = true;
                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF19(tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                        ThemHoaHong_ThuongLevel("0", "F19", "3", IDThanhVien, tableTVTF19.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    ThemHoaHong_ThuongLevel("0", "F19", "3", IDThanhVien, tableTVTF19.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                }
                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                string leveeeel = TimLevelB(tableTVTF19.GioiThieu.ToString());
                                                                                                                if (leveeeel == "5")
                                                                                                                {
                                                                                                                    Plevel = "45";
                                                                                                                }
                                                                                                                #endregion
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception)
                                                                                                        { }
                                                                                                    }
                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF20
                                                                                                    user tableTVTF20 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF19.GioiThieu.ToString()));
                                                                                                    if (tableTVTF20 != null)
                                                                                                    {
                                                                                                        if (tableTVTF20.GioiThieu.ToString() != "0")
                                                                                                        {
                                                                                                            try
                                                                                                            {
                                                                                                                if (ShowFQRcode.ShowF20(tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                {
                                                                                                                    Dung = false;
                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    Dung = true;
                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF20(tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                            ThemHoaHong_ThuongLevel("0", "F20", "3", IDThanhVien, tableTVTF20.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        ThemHoaHong_ThuongLevel("0", "F20", "3", IDThanhVien, tableTVTF20.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                    }
                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                    string leveeeel = TimLevelB(tableTVTF20.GioiThieu.ToString());
                                                                                                                    if (leveeeel == "5")
                                                                                                                    {
                                                                                                                        Plevel = "45";
                                                                                                                    }
                                                                                                                    #endregion
                                                                                                                }
                                                                                                            }
                                                                                                            catch (Exception)
                                                                                                            { }
                                                                                                        }
                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF21
                                                                                                        user tableTVTF21 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF20.GioiThieu.ToString()));
                                                                                                        if (tableTVTF21 != null)
                                                                                                        {
                                                                                                            if (tableTVTF21.GioiThieu.ToString() != "0")
                                                                                                            {
                                                                                                                try
                                                                                                                {
                                                                                                                    if (ShowFQRcode.ShowF21(tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                    {
                                                                                                                        Dung = false;
                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        Dung = true;
                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF21(tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                ThemHoaHong_ThuongLevel("0", "F21", "3", IDThanhVien, tableTVTF21.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            ThemHoaHong_ThuongLevel("0", "F21", "3", IDThanhVien, tableTVTF21.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                        }
                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                        string leveeeel = TimLevelB(tableTVTF21.GioiThieu.ToString());
                                                                                                                        if (leveeeel == "5")
                                                                                                                        {
                                                                                                                            Plevel = "45";
                                                                                                                        }
                                                                                                                        #endregion
                                                                                                                    }
                                                                                                                }
                                                                                                                catch (Exception)
                                                                                                                { }
                                                                                                            }
                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF22
                                                                                                            user tableTVTF22 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF21.GioiThieu.ToString()));
                                                                                                            if (tableTVTF22 != null)
                                                                                                            {
                                                                                                                if (tableTVTF22.GioiThieu.ToString() != "0")
                                                                                                                {
                                                                                                                    try
                                                                                                                    {
                                                                                                                        if (ShowFQRcode.ShowF22(tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                        {
                                                                                                                            Dung = false;
                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            Dung = true;
                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF22(tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F22", "3", IDThanhVien, tableTVTF22.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                }
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                ThemHoaHong_ThuongLevel("0", "F22", "3", IDThanhVien, tableTVTF22.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                            }
                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                            string leveeeel = TimLevelB(tableTVTF22.GioiThieu.ToString());
                                                                                                                            if (leveeeel == "5")
                                                                                                                            {
                                                                                                                                Plevel = "45";
                                                                                                                            }
                                                                                                                            #endregion
                                                                                                                        }
                                                                                                                    }
                                                                                                                    catch (Exception)
                                                                                                                    { }
                                                                                                                }
                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF23
                                                                                                                user tableTVTF23 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF22.GioiThieu.ToString()));
                                                                                                                if (tableTVTF23 != null)
                                                                                                                {
                                                                                                                    if (tableTVTF23.GioiThieu.ToString() != "0")
                                                                                                                    {
                                                                                                                        try
                                                                                                                        {
                                                                                                                            if (ShowFQRcode.ShowF23(tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                            {
                                                                                                                                Dung = false;
                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                Dung = true;
                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF23(tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F23", "3", IDThanhVien, tableTVTF23.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F23", "3", IDThanhVien, tableTVTF23.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                }

                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                string leveeeel = TimLevelB(tableTVTF23.GioiThieu.ToString());
                                                                                                                                if (leveeeel == "5")
                                                                                                                                {
                                                                                                                                    Plevel = "45";
                                                                                                                                }
                                                                                                                                #endregion
                                                                                                                            }
                                                                                                                        }
                                                                                                                        catch (Exception)
                                                                                                                        { }
                                                                                                                    }
                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF24
                                                                                                                    user tableTVTF24 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF23.GioiThieu.ToString()));
                                                                                                                    if (tableTVTF24 != null)
                                                                                                                    {
                                                                                                                        if (tableTVTF24.GioiThieu.ToString() != "0")
                                                                                                                        {
                                                                                                                            try
                                                                                                                            {
                                                                                                                                if (ShowFQRcode.ShowF24(tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                {
                                                                                                                                    Dung = false;
                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    Dung = true;
                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF24(tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F24", "3", IDThanhVien, tableTVTF24.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F24", "3", IDThanhVien, tableTVTF24.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                    }
                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                    string leveeeel = TimLevelB(tableTVTF24.GioiThieu.ToString());
                                                                                                                                    if (leveeeel == "5")
                                                                                                                                    {
                                                                                                                                        Plevel = "45";
                                                                                                                                    }
                                                                                                                                    #endregion
                                                                                                                                }
                                                                                                                            }
                                                                                                                            catch (Exception)
                                                                                                                            { }
                                                                                                                        }
                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF25
                                                                                                                        user tableTVTF25 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF24.GioiThieu.ToString()));
                                                                                                                        if (tableTVTF25 != null)
                                                                                                                        {
                                                                                                                            if (tableTVTF25.GioiThieu.ToString() != "0")
                                                                                                                            {
                                                                                                                                try
                                                                                                                                {
                                                                                                                                    if (ShowFQRcode.ShowF25(tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                    {
                                                                                                                                        Dung = false;
                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        Dung = true;
                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF25(tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F25", "3", IDThanhVien, tableTVTF25.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F25", "3", IDThanhVien, tableTVTF25.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                        }
                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                        string leveeeel = TimLevelB(tableTVTF25.GioiThieu.ToString());
                                                                                                                                        if (leveeeel == "5")
                                                                                                                                        {
                                                                                                                                            Plevel = "45";
                                                                                                                                        }
                                                                                                                                        #endregion
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                catch (Exception)
                                                                                                                                { }
                                                                                                                            }
                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF26
                                                                                                                            user tableTVTF26 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF25.GioiThieu.ToString()));
                                                                                                                            if (tableTVTF26 != null)
                                                                                                                            {
                                                                                                                                if (tableTVTF26.GioiThieu.ToString() != "0")
                                                                                                                                {
                                                                                                                                    try
                                                                                                                                    {
                                                                                                                                        if (ShowFQRcode.ShowF26(tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                        {
                                                                                                                                            Dung = false;
                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            Dung = true;
                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF26(tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F26", "3", IDThanhVien, tableTVTF26.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F26", "3", IDThanhVien, tableTVTF26.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                            }
                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                            string leveeeel = TimLevelB(tableTVTF26.GioiThieu.ToString());
                                                                                                                                            if (leveeeel == "5")
                                                                                                                                            {
                                                                                                                                                Plevel = "45";
                                                                                                                                            }
                                                                                                                                            #endregion
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    catch (Exception)
                                                                                                                                    { }
                                                                                                                                }

                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF27
                                                                                                                                user tableTVTF27 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF26.GioiThieu.ToString()));
                                                                                                                                if (tableTVTF27 != null)
                                                                                                                                {
                                                                                                                                    if (tableTVTF27.GioiThieu.ToString() != "0")
                                                                                                                                    {
                                                                                                                                        try
                                                                                                                                        {
                                                                                                                                            if (ShowFQRcode.ShowF27(tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                            {
                                                                                                                                                Dung = false;
                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                Dung = true;
                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF27(tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F27", "3", IDThanhVien, tableTVTF27.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F27", "3", IDThanhVien, tableTVTF27.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                }
                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                string leveeeel = TimLevelB(tableTVTF27.GioiThieu.ToString());
                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                {
                                                                                                                                                    Plevel = "45";
                                                                                                                                                }
                                                                                                                                                #endregion
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        catch (Exception)
                                                                                                                                        { }
                                                                                                                                    }
                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF28
                                                                                                                                    user tableTVTF28 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF27.GioiThieu.ToString()));
                                                                                                                                    if (tableTVTF28 != null)
                                                                                                                                    {
                                                                                                                                        if (tableTVTF28.GioiThieu.ToString() != "0")
                                                                                                                                        {
                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                if (ShowFQRcode.ShowF28(tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                {
                                                                                                                                                    Dung = false;
                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    Dung = true;
                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF28(tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F28", "3", IDThanhVien, tableTVTF28.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F28", "3", IDThanhVien, tableTVTF28.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                    }
                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF28.GioiThieu.ToString());
                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                    {
                                                                                                                                                        Plevel = "45";
                                                                                                                                                    }
                                                                                                                                                    #endregion
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            catch (Exception)
                                                                                                                                            { }
                                                                                                                                        }

                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF29
                                                                                                                                        user tableTVTF29 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF28.GioiThieu.ToString()));
                                                                                                                                        if (tableTVTF29 != null)
                                                                                                                                        {
                                                                                                                                            if (tableTVTF29.GioiThieu.ToString() != "0")
                                                                                                                                            {
                                                                                                                                                try
                                                                                                                                                {
                                                                                                                                                    if (ShowFQRcode.ShowF29(tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                    {
                                                                                                                                                        Dung = false;
                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        Dung = true;
                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF29(tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F29", "3", IDThanhVien, tableTVTF29.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F29", "3", IDThanhVien, tableTVTF29.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                        }
                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF29.GioiThieu.ToString());
                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                        {
                                                                                                                                                            Plevel = "45";
                                                                                                                                                        }
                                                                                                                                                        #endregion
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                catch (Exception)
                                                                                                                                                { }
                                                                                                                                            }
                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF30
                                                                                                                                            user tableTVTF30 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF29.GioiThieu.ToString()));
                                                                                                                                            if (tableTVTF30 != null)
                                                                                                                                            {
                                                                                                                                                if (tableTVTF30.GioiThieu.ToString() != "0")
                                                                                                                                                {
                                                                                                                                                    try
                                                                                                                                                    {
                                                                                                                                                        if (ShowFQRcode.ShowF30(tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                        {
                                                                                                                                                            Dung = false;
                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            Dung = true;
                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF30(tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F30", "3", IDThanhVien, tableTVTF30.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F30", "3", IDThanhVien, tableTVTF30.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                            }
                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF30.GioiThieu.ToString());
                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                            {
                                                                                                                                                                Plevel = "45";
                                                                                                                                                            }
                                                                                                                                                            #endregion
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    catch (Exception)
                                                                                                                                                    { }
                                                                                                                                                }

                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF31
                                                                                                                                                user tableTVTF31 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF30.GioiThieu.ToString()));
                                                                                                                                                if (tableTVTF31 != null)
                                                                                                                                                {
                                                                                                                                                    if (tableTVTF31.GioiThieu.ToString() != "0")
                                                                                                                                                    {
                                                                                                                                                        try
                                                                                                                                                        {
                                                                                                                                                            if (ShowFQRcode.ShowF31(tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                            {
                                                                                                                                                                Dung = false;
                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                Dung = true;
                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF31(tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F31", "3", IDThanhVien, tableTVTF31.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F31", "3", IDThanhVien, tableTVTF31.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                }
                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF31.GioiThieu.ToString());
                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                {
                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                }
                                                                                                                                                                #endregion
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        catch (Exception)
                                                                                                                                                        { }
                                                                                                                                                    }

                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF32
                                                                                                                                                    user tableTVTF32 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF31.GioiThieu.ToString()));
                                                                                                                                                    if (tableTVTF32 != null)
                                                                                                                                                    {
                                                                                                                                                        if (tableTVTF32.GioiThieu.ToString() != "0")
                                                                                                                                                        {
                                                                                                                                                            try
                                                                                                                                                            {
                                                                                                                                                                if (ShowFQRcode.ShowF32(tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                {
                                                                                                                                                                    Dung = false;
                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    Dung = true;
                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF32(tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F32", "3", IDThanhVien, tableTVTF32.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F32", "3", IDThanhVien, tableTVTF32.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                    }
                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF32.GioiThieu.ToString());
                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                    {
                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                    }
                                                                                                                                                                    #endregion
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            catch (Exception)
                                                                                                                                                            { }
                                                                                                                                                        }
                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF33
                                                                                                                                                        user tableTVTF33 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF32.GioiThieu.ToString()));
                                                                                                                                                        if (tableTVTF33 != null)
                                                                                                                                                        {
                                                                                                                                                            if (tableTVTF33.GioiThieu.ToString() != "0")
                                                                                                                                                            {
                                                                                                                                                                try
                                                                                                                                                                {
                                                                                                                                                                    if (ShowFQRcode.ShowF33(tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                    {
                                                                                                                                                                        Dung = false;
                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        Dung = true;
                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF33(tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F33", "3", IDThanhVien, tableTVTF33.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F33", "3", IDThanhVien, tableTVTF33.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                        }
                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF33.GioiThieu.ToString());
                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                        {
                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                        }
                                                                                                                                                                        #endregion
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                catch (Exception)
                                                                                                                                                                { }
                                                                                                                                                            }
                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF34
                                                                                                                                                            user tableTVTF34 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF33.GioiThieu.ToString()));
                                                                                                                                                            if (tableTVTF34 != null)
                                                                                                                                                            {
                                                                                                                                                                if (tableTVTF34.GioiThieu.ToString() != "0")
                                                                                                                                                                {
                                                                                                                                                                    try
                                                                                                                                                                    {
                                                                                                                                                                        if (ShowFQRcode.ShowF34(tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                        {
                                                                                                                                                                            Dung = false;
                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            Dung = true;
                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF34(tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F34", "3", IDThanhVien, tableTVTF34.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F34", "3", IDThanhVien, tableTVTF34.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                            }
                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF34.GioiThieu.ToString());
                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                            {
                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                            }
                                                                                                                                                                            #endregion
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    catch (Exception)
                                                                                                                                                                    { }
                                                                                                                                                                }
                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF35
                                                                                                                                                                user tableTVTF35 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF34.GioiThieu.ToString()));
                                                                                                                                                                if (tableTVTF35 != null)
                                                                                                                                                                {
                                                                                                                                                                    if (tableTVTF35.GioiThieu.ToString() != "0")
                                                                                                                                                                    {
                                                                                                                                                                        try
                                                                                                                                                                        {
                                                                                                                                                                            if (ShowFQRcode.ShowF35(tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                            {
                                                                                                                                                                                Dung = false;
                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                Dung = true;
                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF35(tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F35", "3", IDThanhVien, tableTVTF35.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F35", "3", IDThanhVien, tableTVTF35.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                }
                                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF35.GioiThieu.ToString());
                                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                                {
                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                }
                                                                                                                                                                                #endregion
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        catch (Exception)
                                                                                                                                                                        { }
                                                                                                                                                                    }
                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF36
                                                                                                                                                                    user tableTVTF36 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF35.GioiThieu.ToString()));
                                                                                                                                                                    if (tableTVTF36 != null)
                                                                                                                                                                    {
                                                                                                                                                                        if (tableTVTF36.GioiThieu.ToString() != "0")
                                                                                                                                                                        {
                                                                                                                                                                            try
                                                                                                                                                                            {
                                                                                                                                                                                if (ShowFQRcode.ShowF36(tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                {
                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF36(tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F36", "3", IDThanhVien, tableTVTF36.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F36", "3", IDThanhVien, tableTVTF36.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                    }
                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF36.GioiThieu.ToString());
                                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                                    {
                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                    }
                                                                                                                                                                                    #endregion
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            catch (Exception)
                                                                                                                                                                            { }
                                                                                                                                                                        }
                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF37
                                                                                                                                                                        user tableTVTF37 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF36.GioiThieu.ToString()));
                                                                                                                                                                        if (tableTVTF37 != null)
                                                                                                                                                                        {
                                                                                                                                                                            if (tableTVTF37.GioiThieu.ToString() != "0")
                                                                                                                                                                            {
                                                                                                                                                                                try
                                                                                                                                                                                {
                                                                                                                                                                                    if (ShowFQRcode.ShowF37(tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                    {
                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF37(tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F37", "3", IDThanhVien, tableTVTF37.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F37", "3", IDThanhVien, tableTVTF37.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                        }
                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF37.GioiThieu.ToString());
                                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                                        {
                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                        }
                                                                                                                                                                                        #endregion
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                { }
                                                                                                                                                                            }
                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF38
                                                                                                                                                                            user tableTVTF38 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF37.GioiThieu.ToString()));
                                                                                                                                                                            if (tableTVTF38 != null)
                                                                                                                                                                            {
                                                                                                                                                                                if (tableTVTF38.GioiThieu.ToString() != "0")
                                                                                                                                                                                {
                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        if (ShowFQRcode.ShowF38(tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                        {
                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF38(tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F38", "3", IDThanhVien, tableTVTF38.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F38", "3", IDThanhVien, tableTVTF38.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                            }
                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF38.GioiThieu.ToString());
                                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                                            {
                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                            }
                                                                                                                                                                                            #endregion
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                    { }
                                                                                                                                                                                }
                                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF39
                                                                                                                                                                                user tableTVTF39 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF38.GioiThieu.ToString()));
                                                                                                                                                                                if (tableTVTF39 != null)
                                                                                                                                                                                {
                                                                                                                                                                                    if (tableTVTF39.GioiThieu.ToString() != "0")
                                                                                                                                                                                    {
                                                                                                                                                                                        try
                                                                                                                                                                                        {
                                                                                                                                                                                            if (ShowFQRcode.ShowF39(tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                            {
                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF39(tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F39", "3", IDThanhVien, tableTVTF39.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F39", "3", IDThanhVien, tableTVTF39.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                }

                                                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF39.GioiThieu.ToString());
                                                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                                                {
                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                }
                                                                                                                                                                                                #endregion
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                        { }
                                                                                                                                                                                    }

                                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF40
                                                                                                                                                                                    user tableTVTF40 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF39.GioiThieu.ToString()));
                                                                                                                                                                                    if (tableTVTF40 != null)
                                                                                                                                                                                    {
                                                                                                                                                                                        if (tableTVTF40.GioiThieu.ToString() != "0")
                                                                                                                                                                                        {
                                                                                                                                                                                            try
                                                                                                                                                                                            {
                                                                                                                                                                                                if (ShowFQRcode.ShowF40(tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                {
                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF40(tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F40", "3", IDThanhVien, tableTVTF40.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F40", "3", IDThanhVien, tableTVTF40.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                    }
                                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF40.GioiThieu.ToString());
                                                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                    }
                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                            { }
                                                                                                                                                                                        }
                                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF41
                                                                                                                                                                                        user tableTVTF41 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF40.GioiThieu.ToString()));
                                                                                                                                                                                        if (tableTVTF41 != null)
                                                                                                                                                                                        {
                                                                                                                                                                                            if (tableTVTF41.GioiThieu.ToString() != "0")
                                                                                                                                                                                            {
                                                                                                                                                                                                try
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (ShowFQRcode.ShowF41(tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF41(tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F41", "3", IDThanhVien, tableTVTF41.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F41", "3", IDThanhVien, tableTVTF41.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                        }
                                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF41.GioiThieu.ToString());
                                                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                        }
                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                { }
                                                                                                                                                                                            }

                                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF42
                                                                                                                                                                                            user tableTVTF42 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF41.GioiThieu.ToString()));
                                                                                                                                                                                            if (tableTVTF42 != null)
                                                                                                                                                                                            {
                                                                                                                                                                                                if (tableTVTF42.GioiThieu.ToString() != "0")
                                                                                                                                                                                                {
                                                                                                                                                                                                    try
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (ShowFQRcode.ShowF42(tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF42(tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F42", "3", IDThanhVien, tableTVTF42.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F42", "3", IDThanhVien, tableTVTF42.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                            }
                                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF42.GioiThieu.ToString());
                                                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                            }
                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                    { }
                                                                                                                                                                                                }

                                                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF43
                                                                                                                                                                                                user tableTVTF43 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF42.GioiThieu.ToString()));
                                                                                                                                                                                                if (tableTVTF43 != null)
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (tableTVTF43.GioiThieu.ToString() != "0")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        try
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (ShowFQRcode.ShowF43(tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF43(tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F43", "3", IDThanhVien, tableTVTF43.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F43", "3", IDThanhVien, tableTVTF43.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                                }
                                                                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF43.GioiThieu.ToString());
                                                                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                }
                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                        { }
                                                                                                                                                                                                    }

                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF44
                                                                                                                                                                                                    user tableTVTF44 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF43.GioiThieu.ToString()));
                                                                                                                                                                                                    if (tableTVTF44 != null)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (tableTVTF44.GioiThieu.ToString() != "0")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            try
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (ShowFQRcode.ShowF44(tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF44(tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F44", "3", IDThanhVien, tableTVTF44.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F44", "3", IDThanhVien, tableTVTF44.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF44.GioiThieu.ToString());
                                                                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                            { }
                                                                                                                                                                                                        }

                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF45
                                                                                                                                                                                                        user tableTVTF45 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF44.GioiThieu.ToString()));
                                                                                                                                                                                                        if (tableTVTF45 != null)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (tableTVTF45.GioiThieu.ToString() != "0")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                try
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (ShowFQRcode.ShowF45(tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF45(tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F45", "3", IDThanhVien, tableTVTF45.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F45", "3", IDThanhVien, tableTVTF45.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF45.GioiThieu.ToString());
                                                                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                { }
                                                                                                                                                                                                            }

                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF46
                                                                                                                                                                                                            user tableTVTF46 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF45.GioiThieu.ToString()));
                                                                                                                                                                                                            if (tableTVTF46 != null)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (tableTVTF46.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    try
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (ShowFQRcode.ShowF46(tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF46(tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F46", "3", IDThanhVien, tableTVTF46.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F46", "3", IDThanhVien, tableTVTF46.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF46.GioiThieu.ToString());
                                                                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                    { }
                                                                                                                                                                                                                }

                                                                                                                                                                                                                #region Hoa Hồng Gián tiếp tableTVTF47
                                                                                                                                                                                                                user tableTVTF47 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF46.GioiThieu.ToString()));
                                                                                                                                                                                                                if (tableTVTF47 != null)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (tableTVTF47.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        try
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (ShowFQRcode.ShowF47(tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                                Plevel = Plevel + "," + ShowFQRcode.ShowF47(tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F47", "3", IDThanhVien, tableTVTF47.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F47", "3", IDThanhVien, tableTVTF47.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                string leveeeel = TimLevelB(tableTVTF47.GioiThieu.ToString());
                                                                                                                                                                                                                                if (leveeeel == "5")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                                        { }
                                                                                                                                                                                                                    }

                                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp tableTVTF48
                                                                                                                                                                                                                    user tableTVTF48 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF47.GioiThieu.ToString()));
                                                                                                                                                                                                                    if (tableTVTF48 != null)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (tableTVTF48.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            try
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (ShowFQRcode.ShowF48(tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                                    Plevel = Plevel + "," + ShowFQRcode.ShowF48(tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F48", "3", IDThanhVien, tableTVTF48.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel("0", "F48", "3", IDThanhVien, tableTVTF48.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                    string leveeeel = TimLevelB(tableTVTF48.GioiThieu.ToString());
                                                                                                                                                                                                                                    if (leveeeel == "5")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                                            { }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp tableTVTF49
                                                                                                                                                                                                                        user tableTVTF49 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF48.GioiThieu.ToString()));
                                                                                                                                                                                                                        if (tableTVTF49 != null)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (tableTVTF49.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                try
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (ShowFQRcode.ShowF49(tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                                        Plevel = Plevel + "," + ShowFQRcode.ShowF49(tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F49", "3", IDThanhVien, tableTVTF49.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel("0", "F49", "3", IDThanhVien, tableTVTF49.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                        string leveeeel = TimLevelB(tableTVTF49.GioiThieu.ToString());
                                                                                                                                                                                                                                        if (leveeeel == "5")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                                { }
                                                                                                                                                                                                                            }

                                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp tableTVTF50
                                                                                                                                                                                                                            user tableTVTF50 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF49.GioiThieu.ToString()));
                                                                                                                                                                                                                            if (tableTVTF50 != null)
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (tableTVTF50.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    try
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (ShowFQRcode.ShowF50(tableTVTF50.iuser_id.ToString(), tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString()) == "5")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                                            Plevel = Plevel + "," + ShowFQRcode.ShowF50(tableTVTF50.iuser_id.ToString(), tableTVTF49.iuser_id.ToString(), tableTVTF48.iuser_id.ToString(), tableTVTF47.iuser_id.ToString(), tableTVTF46.iuser_id.ToString(), tableTVTF45.iuser_id.ToString(), tableTVTF44.iuser_id.ToString(), tableTVTF43.iuser_id.ToString(), tableTVTF42.iuser_id.ToString(), tableTVTF41.iuser_id.ToString(), tableTVTF40.iuser_id.ToString(), tableTVTF39.iuser_id.ToString(), tableTVTF38.iuser_id.ToString(), tableTVTF37.iuser_id.ToString(), tableTVTF36.iuser_id.ToString(), tableTVTF35.iuser_id.ToString(), tableTVTF34.iuser_id.ToString(), tableTVTF33.iuser_id.ToString(), tableTVTF32.iuser_id.ToString(), tableTVTF31.iuser_id.ToString(), tableTVTF30.iuser_id.ToString(), tableTVTF29.iuser_id.ToString(), tableTVTF28.iuser_id.ToString(), tableTVTF27.iuser_id.ToString(), tableTVTF26.iuser_id.ToString(), tableTVTF25.iuser_id.ToString(), tableTVTF24.iuser_id.ToString(), tableTVTF23.iuser_id.ToString(), tableTVTF22.iuser_id.ToString(), tableTVTF21.iuser_id.ToString(), tableTVTF20.iuser_id.ToString(), tableTVTF19.iuser_id.ToString(), tableTVTF18.iuser_id.ToString(), tableTVTF17.iuser_id.ToString(), tableTVTF16.iuser_id.ToString(), tableTVTF15.iuser_id.ToString(), tableTVTF14.iuser_id.ToString(), tableTVTF13.iuser_id.ToString(), tableTVTF12.iuser_id.ToString(), tableTVTF11.iuser_id.ToString(), tableTVTF10.iuser_id.ToString(), tableTVTF9.iuser_id.ToString(), tableTVTF8.iuser_id.ToString(), tableTVTF7.iuser_id.ToString(), tableTVTF6.iuser_id.ToString(), tableTVTF5.iuser_id.ToString(), tableTVTF4.iuser_id.ToString(), tableTVTF3.iuser_id.ToString(), tableTVTF2.iuser_id.ToString(), IDThanhVien.ToString());
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
                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel("0", "F50", "3", IDThanhVien, tableTVTF50.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), TongLevel, IDMaDonTao, "0");
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel("0", "F50", "3", IDThanhVien, tableTVTF50.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), "0", IDMaDonTao, "0");
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            #region Dừng nếu gặp lelvel5
                                                                                                                                                                                                                                            string leveeeel = TimLevelB(tableTVTF50.GioiThieu.ToString());
                                                                                                                                                                                                                                            if (leveeeel == "5")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                                    { }
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
                        #endregion


                        ThanhVienGioiThieu = dt[0].GioiThieu.ToString();

                        #region Nếu là lead thì thưởng thêm 10% Từ B

                        if (!TimLeader(dt[0].GioiThieu).Equals("0"))
                        {
                            double HoaHongLeader = Convert.ToDouble(Commond.Setting("hoahonggtLeader"));
                            double LeadTongCoin = (Tiencoin * HoaHongLeader) / 100;
                            if (HoaHongLeader > 0)
                            {
                                ThemHoaHong("2", "Hoa Hồng Leader", hdidthanhvien.Value, TimLeader(dt[0].GioiThieu), HoaHongLeader.ToString(), LeadTongCoin.ToString(), IDMaDonTao);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region Chi  nhánh  ko được thưởng theo hình thức giới thiệu lên chỉ ăn 10 % phí đăng ký tài khoản mới trong mạng lưới của mình
                    List<Entity.users> dtchinhanh = Susers.Name_Text("select * from users  where iuser_id=" + hdidthanhvien.Value + "");// and ChiNhanh=1
                    if (dtchinhanh.Count() > 0)
                    {
                        #region Chi nhánh được hưởng 10%
                        if (!dtchinhanh[0].IDChiNhanh.Equals("0"))//&& dtchinhanh[0].DuyetTienDanap.ToString() != "0"
                        {
                            if (ShowIDChiNhanh(dtchinhanh[0].IDChiNhanh.ToString()) != "0" && !dtchinhanh[0].DuyetTienDanap.Equals("0"))
                            {
                                double HoaHongChiNhanh = Convert.ToDouble(Commond.Setting("hoahonggtchinhanh"));
                                double TongCoin = (Tiencoin * HoaHongChiNhanh) / 100;
                                if (HoaHongChiNhanh > 0)
                                {
                                    ThemHoaHong("5", "Hoa Hồng (Chi Nhánh)", hdidthanhvien.Value, ShowIDChiNhanh(dtchinhanh[0].IDChiNhanh.ToString()), HoaHongChiNhanh.ToString(), TongCoin.ToString(), IDMaDonTao);
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion

                    // Nếu người giới thiệu là leader thì sẽ được hưởng thêm 10% từ người giới thiệu từ mạng lưới cấp dưới của mình
                    // TH1:,TH2:,TH4:
                    #region kiểm tra nếu đã được kích hoạt rồi thì ko kích hoạt dc nữa
                    //kiểm tra nếu đã được kích hoạt rồi thì ko kích hoạt dc nữa
                    user dkkl = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdidthanhvien.Value));
                    if (dkkl != null)
                    {
                        if (dkkl.DuyetTienDanap == 0)
                        {
                            double TongSoCoinDaCos = 0;
                            if (vinao == "1")
                            {
                                TongSoCoinDaCos = Convert.ToDouble(dkkl.VIAAFFILIATE);
                            }
                            else if (vinao == "2")
                            {
                                TongSoCoinDaCos = Convert.ToDouble(dkkl.TongTienCoinDuocCap);
                            }
                            double TongTienNapVaos = Convert.ToDouble(Tiencoin.ToString());
                            double Conglais = 0;
                            // trừ tiền và coin đi
                            Conglais = ((TongSoCoinDaCos) - (TongTienNapVaos));
                            if (vinao == "1")
                            {
                                Susers.Name_Text("update users set DuyetTienDanap=1,TongTienDanapVND=" + TienVND.ToString() + ",TongTienDanapCoin=" + Tiencoin.ToString() + ",VIAAFFILIATE=" + Conglais.ToString() + "  where iuser_id=" + hdidthanhvien.Value + "");
                            }
                            else if (vinao == "2")
                            {
                                Susers.Name_Text("update users set DuyetTienDanap=1,TongTienDanapVND=" + TienVND.ToString() + ",TongTienDanapCoin=" + Tiencoin.ToString() + ",TongTienCoinDuocCap=" + Conglais.ToString() + "  where iuser_id=" + hdidthanhvien.Value + "");
                            }
                            LichSuGiaoDich("18", "Nạp tiền đăng ký thành viên " + Tiencoin + " ngàn cho thành viên Từ " + Alet + "", "0", hdidthanhvien.Value, "0", Commond.Setting("TienKichHoat").ToString());
                        }
                    }
                    #endregion

                    #region Nâng cấp level thành viên
                    NangLevel.UpDate_NangLevel(hdidthanhvien.Value);
                    #endregion

                    #region Vi Loi Nhuan sau khi da chia HH
                    //try
                    //{
                    var tongdiemdachia = db.S_TongDiemDaChia_DangKyThanhVien(int.Parse(hdidthanhvien.Value), Convert.ToInt64(IDMaDonTao)).ToList();
                    if (tongdiemdachia[0].sodiem >= 0)
                    {
                        Double TongDaChia = Convert.ToDouble(tongdiemdachia[0].sodiem.ToString());
                        Double TongDaChiaConlai = TongDaChia;// +ThanhTien;
                        Double TongCongs = Tiencoin - TongDaChiaConlai;
                        LoiNhuanDangKyThanhVien abln = new LoiNhuanDangKyThanhVien();
                        abln.IDThanhVienDangKy = int.Parse(hdidthanhvien.Value);
                        abln.IDThanhVienGioiThieu = int.Parse(ThanhVienGioiThieu);
                        abln.MoTa = "Lợi nhuận đăng ký thành viên";
                        abln.NgayTao = DateTime.Now;
                        abln.SoDiemNapVao = Tiencoin.ToString();
                        abln.SoDiemConLai = TongCongs.ToString();
                        abln.SoDiemDaChia = TongDaChiaConlai.ToString();
                        abln.MTreeIDThanhVienDangKy = Commond.ShowMTree(hdidthanhvien.Value.ToString());
                        abln.MTReIDThanhVienGioiThieu = Commond.ShowMTree(ThanhVienGioiThieu.ToString());
                        abln.IDMaDonTao = Convert.ToInt64(IDMaDonTao);
                        abln.IDChiNhanh = Convert.ToInt32(hdChiNhanh.Value);
                        abln.IDLeader = Convert.ToInt32(TimLeader(hdidthanhvien.Value.ToString()));
                        db.LoiNhuanDangKyThanhViens.InsertOnSubmit(abln);
                        db.SubmitChanges();
                    }
                    //else
                    //{
                    //    Double TongDaChia = Convert.ToDouble(0);
                    //    Double TongDaChiaConlai = ThanhTien;
                    //    Double TongCongs = Tiencoin - TongDaChiaConlai;
                    //    LoiNhuanDangKyThanhVien abln = new LoiNhuanDangKyThanhVien();
                    //    abln.IDThanhVienDangKy = int.Parse(hdidthanhvien.Value);
                    //    abln.IDThanhVienGioiThieu = int.Parse(ThanhVienGioiThieu);
                    //    abln.MoTa = "Lợi nhuận đăng ký thành viên";
                    //    abln.NgayTao = DateTime.Now;
                    //    abln.SoDiemNapVao = Tiencoin.ToString();
                    //    abln.SoDiemConLai = TongCongs.ToString();
                    //    abln.SoDiemDaChia = TongDaChiaConlai.ToString();
                    //    abln.MTreeIDThanhVienDangKy = Commond.ShowMTree(hdidthanhvien.Value.ToString());
                    //    abln.MTReIDThanhVienGioiThieu = Commond.ShowMTree(ThanhVienGioiThieu.ToString());
                    //    abln.IDMaDonTao = Convert.ToInt64(IDMaDonTao);
                    //    abln.IDChiNhanh = Convert.ToInt32(hdChiNhanh.Value);
                    //    abln.IDLeader = Convert.ToInt32(TimLeader(hdidthanhvien.Value.ToString()));
                    //    db.LoiNhuanDangKyThanhViens.InsertOnSubmit(abln);
                    //    db.SubmitChanges();
                    //}

                    //}
                    //catch (Exception)
                    //{ }
                    #endregion

                    #region Cập nhật ngày kích hoạt 1 năm để kiểm soát
                    Commond.SetLichSuKichHoat(hdidthanhvien.Value, "Kích Hoạt Bằng Ví");
                    #endregion

                    DataTable dtcart = (DataTable)Session["cart"];
                    if (Session["cart"] != null)
                    {
                        if (dtcart.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcart.Rows.Count; i++)
                            {
                                GioHangs.Cart_UpdateNumber_ThanhVienMuaTheoSoLuong(ref dtcart, dtcart.Rows[i]["PID"].ToString(), dtcart.Rows[i]["Quantity"].ToString(), "1");
                            }
                        }
                    }
                    Response.Write("<script type=\"text/javascript\">alert('Kích hoạt thành công.');window.location.href='/gio-hang.html';</script>");
                    return;
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Tài khoản của bạn không đủ điểm để kích hoạt. Số điểm cần để kích hoạt là " + Tiencoin + " điểm ');window.location.href='/gio-hang.html';</script>");
                }
                #endregion
            }
        }


        void ThemHoaHong(string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDMaDonTao)
        {
            #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
            Commond.CheckNgayHetHan(IDUserNguoiDuocHuong.ToString());
            #endregion

            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and DuyetTienDanap=1");
            if (iitem.Count > 0)
            {
                #region HoaHongThanhVien
                HoaHongThanhVien obj = new HoaHongThanhVien();
                obj.IDProducts = int.Parse("0");
                obj.IDType = int.Parse(IDType);
                obj.Type = Type;
                obj.IDThanhVien = int.Parse(IDThanhVien);
                obj.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
                obj.PhamTramHoaHong = PhamTramHoaHong;
                obj.SoCoin = SoCoin.ToString();
                obj.NgayTao = DateTime.Now;
                obj.TrangThai = 1;
                obj.NoiDung = "Hoa hồng đăng ký thành viên";
                //obj.IDCart = int.Parse(IDThanhVien);// ID đơn hàng lấy thành mã thành viên đăng ký
                obj.IDCart = Convert.ToInt64(IDMaDonTao);
                db.HoaHongThanhViens.InsertOnSubmit(obj);
                db.SubmitChanges();
                #endregion

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
                obl.NoiDung = "Hoa hồng đăng ký thành viên";
                obl.IDCart = Convert.ToInt64(IDMaDonTao);
                // obl.IDCart = int.Parse(IDThanhVien);// ID đơn hàng lấy thành mã thành viên đăng ký
                db.LichSuGiaoDiches.InsertOnSubmit(obl);
                db.SubmitChanges();
                #endregion
                CongTien(IDUserNguoiDuocHuong, SoCoin);
                //  CongTienViTienHHGioiThieu(IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, SoCoin);
            }
        }
        void CongTien(string IDUserNguoiDuocHuong, string SoCoin)
        {
            #region Cộng điểm theo hoa hồng coin vào bảng thành viên để tích điểm trong các trường hợp mua hàng cần
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and DuyetTienDanap=1");
            if (iitem.Count > 0)
            {
                double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViHoaHongMuaBan);
                double TongTienNapVao = Convert.ToDouble(SoCoin);
                double Conglai = 0;
                Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                Susers.Name_Text("update users set ViHoaHongMuaBan=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
            }
            #endregion
        }

        void CongTienViTienHHGioiThieu(string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
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
                List<Entity.users> truvi = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and HoTro=1");
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
                        ThemHoaHongThem_ViTienHHGioiThieu("31", "Hoa Hồng (Hỗ Trợ)", IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, TongTienNapVao.ToString());
                        //Mục 32 này làm để lưu lịch sử để sau này nhỡ có lỗi còn lục lại được là đã bị trừ ntn
                        ThemHoaHongThem_ViTienHHGioiThieu("32", "Hoa Hồng hỗ trợ (Bị trừ từ ví hoa hồng Hỗ Trợ)", IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, TongTienNapVao.ToString());
                    }
                }
                #endregion
            }
            #endregion
        }
        // Sẽ xóa đoạn code này khi ViTienHHGioiThieu=0 đồng nhé ThemHoaHongThem_ViTienHHGioiThieu
        void ThemHoaHongThem_ViTienHHGioiThieu(string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
        {
            #region HoaHongThanhVien
            HoaHongThanhVien obj = new HoaHongThanhVien();
            obj.IDProducts = int.Parse("0");
            obj.IDType = int.Parse(IDType);
            obj.Type = Type;
            obj.IDThanhVien = int.Parse(IDThanhVien);
            obj.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
            obj.PhamTramHoaHong = PhamTramHoaHong;
            obj.SoCoin = SoCoin.ToString();
            obj.NgayTao = DateTime.Now;
            obj.TrangThai = 1;
            obj.NoiDung = "Hoa hồng đăng ký thành viên";
            obj.IDCart = int.Parse("999");
            db.HoaHongThanhViens.InsertOnSubmit(obj);
            db.SubmitChanges();
            #endregion

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

        #region Kèm theo Hoa Hong
        void ThemHoaHong_ThuongLevel(string IDProducts, string ThuTu, string IDType, string IDThanhVien, string IDUserNguoiDuocHuong, string SoCoin, string LevelThanhVienA, string LevelThanhVienB, string IDCart, string Noidung)
        {
            // Library.WriteErrorLog("  LevelThanhVienA: " + LevelThanhVienA + " - LevelThanhVienB: " + LevelThanhVienB);
            if (TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()) != "0")
            {
                #region Hoa hồng gián tiếp khi giới thiệu được hưởng sau khi lấy level A - level B
                double SoPhanTram = Convert.ToDouble(TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()));
                double TongTien = Convert.ToDouble(SoCoin);
                double ThuongLevel = (TongTien * SoPhanTram) / 100;
                // Library.WriteErrorLog("  SoPhanTram: " + SoPhanTram + "  IDThanhVien: " + IDThanhVien + " IDUserNguoiDuocHuong: " + IDUserNguoiDuocHuong + " ThuongLevel: " + ThuongLevel);
                ThemHoaHong(IDProducts, IDType, "Hoa hồng quản lý (Cấp) " + ThuTu, IDThanhVien, IDUserNguoiDuocHuong, SoPhanTram.ToString(), ThuongLevel.ToString(), IDCart, Noidung);
                #endregion
            }
        }
        void ThemHoaHong(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDCart, string NoiDung)
        {
            #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
            Commond.CheckNgayHetHan(IDUserNguoiDuocHuong.ToString());
            #endregion

            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and DuyetTienDanap=1");
            if (iitem.Count > 0)
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

                CongTien(IDType, IDUserNguoiDuocHuong, SoCoin);
                //CongTien_ViTienHHGioiThieu(IDProducts, IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, SoCoin);
            }

        }
        void CongTien(string Type, string IDUserNguoiDuocHuong, string SoCoin)
        {
            #region Cộng điểm  theo hoa hồng coin vào bảng thành viên để tích điểm trong các trường hợp mua hàng cần
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and DuyetTienDanap=1");
            if (iitem.Count > 0)
            {
                if (Type == "400")
                {
                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                    double TongTienNapVao = Convert.ToDouble(SoCoin);
                    double Conglai = 0;
                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }
                else
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
        public string TinhDiemthuongGiantiep(string LevelA, string LevelB)
        {

            /// Riêng level kích hoạt 480 thì chỉ cách nhau có 2% trên 1 level nhé, chứ ko phải cách nhau 3 như sản phẩm
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
        //public string SetLevel(string id)
        //{
        //    List<Entity.Menu> dt = SMenu.Name_Text("select * from Menu where capp='" + More.LV + "' and Views=" + id + " ");
        //    if (dt.Count > 0)
        //    {
        //        return dt[0].Noidung1.ToString();
        //    }
        //    return "0";
        //}
        public string SetLevel(string Level)//// Riêng level kích hoạt 480 thì chỉ cách nhau có 2% trên 1 level nhé, chứ ko phải cách nhau 3 như sản phẩm
        {
            Double DauVao = Convert.ToDouble(Level);
            if (DauVao == 0)
            {
                return "0";
            }
            else if (DauVao == 1)
            {
                return "2";
            }
            else if (DauVao == 2)
            {
                return "4";
            }
            else if (DauVao == 3)
            {
                return "6";
            }
            else if (DauVao == 4)
            {
                return "8";
            }
            else if (DauVao == 5)
            {
                return "10";
            }
            return "0";
        }

        protected string TimLevelB(string ID)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            user iitems = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(ID.ToString()));
            if (iitems != null)
            {
                return iitems.LevelThanhVien.ToString();
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

        #region Tìm ra người giới thiệu gần nhất để cho Level
        protected string ShowF2(string IDF1, string IDF2)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " ");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF3(string IDF1, string IDF2, string IDF3)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " ");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + " ");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + " ");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF4(string IDF1, string IDF2, string IDF3, string IDF4)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " ");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + " ");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + " ");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf4 = Susers.Name_Text("select * from users  where iuser_id=" + IDF4 + " ");
            if (dtf4.Count > 0)
            {
                return dtf4[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF5(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " ");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + " ");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + " ");
            if (dtf5.Count > 0)
            {
                return dtf5[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        #endregion

        #endregion

    }
}