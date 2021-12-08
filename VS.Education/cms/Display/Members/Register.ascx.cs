using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Framework;
using Services;
using Entity;

namespace VS.E_Commerce.cms.Display.Members
{
    public partial class Register : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
        private static Random random = new Random();
        DatalinqDataContext db = new DatalinqDataContext();
        string link = "0";
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
            if (Request["info"] != null && !Request["info"].Equals(""))
            {
                link = Request["info"].ToString();
            }
            if (!base.IsPostBack)
            {
                SetCapCha();
                if (link != "0")// trường hợp có coupon và không có link ?aff
                {
                    List<Entity.users> iEmail = Susers.Name_Text("select * from users  where vphone='" + link + "' and istatus=1");//and iuser_id !=" + MoreAll.MoreAll.GetCookies("MembersID") + " 
                    if (iEmail.Count > 0)
                    {
                        txtnguoigioithieu.Text = iEmail[0].vuserun.ToString();
                        ltgoithieu.Text = "Người giới thiệu: " + iEmail[0].vuserun.ToString();
                        txtnguoigioithieu.Style.Add("pointer-events", "none");
                        txtnguoigioithieu.Style.Add("opacity", "0.6");
                    }
                    //else
                    //{
                    //    ltgoithieu.Text = " Người giới thiệu không tồn tại trong hệ thống.";
                    //    return;
                    //}
                }
                ShowTinhThanh();
                ShowChiNhanh();
                new DataTable();
                this.btnregister.Text = this.label("l_register");
            }
        }
        public void SetCapCha()
        {
            string hash = RandomString(6);
            ltshowcapcha.Text = hash;
            Session["RandomCapCha"] = hash;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdsfghjklqwertyuiopzxcvbnm0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        protected void ShowTinhThanh()
        {
            int str = 0;
            var dt = db.Tinhthanhs.Where(s => s.capp == "TT" && s.Parent_ID == -1 && s.Lang == language).OrderByDescending(s => s.ID).ToList();
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
        #region Menu
        protected void ShowChiNhanh()
        {
            int str = 0;
            List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where capp='" + More.DL + "' and Lang='" + language + "'  and Parent_ID=-1 and Status=1 order by Orders desc");
            for (int i = 0; i < dt.Count; i++)
            {
                if (dt[i].Parent_ID.ToString() == "-1")
                {
                    ddlchinhanh.Items.Insert(str, new ListItem(dt[i].Name.ToString(), dt[i].ID.ToString()));
                }
            }
            this.ddlchinhanh.Items.Insert(0, new ListItem("== Chọn chi nhánh == ", "0"));
            this.ddlchinhanh.DataBind();
        }
        #endregion
        protected void btncancel_Click(object sender, EventArgs e)
        {
            this.txtemail.Text = "";
            this.txtlastname.Text = "";
           // this.txtusername.Text = "";
            this.txt_add.Text = "";
            this.txt_phone.Text = "";
            this.ltmsg.Text = "";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Nhap = txtcapcha.Text.Trim();
                if (Session["RandomCapCha"].ToString() != Nhap)
                {
                    ltmsg.Text = "Mã bảo vệ chưa chính xác";
                }
                else
                {
                    //  System.Threading.Thread.Sleep(1000);
                    Fusers item = new Fusers();
                    //if (item.Detailvuserun(this.txtusername.Text.Trim().ToLower()).Rows.Count > 0)
                    //{
                    //    this.ltmsg.Text = " Điện thoại bạn đăng ký đã được sử dụng bởi một tài khoản khác.!";
                    //}
                    //else 
                    if (item.Detailemail(this.txtemail.Text.Trim().ToLower()).Rows.Count > 0)
                    {
                        this.ltmsg.Text = label("login6");
                    }
                    //else if (ContainsUnicodeCharacter(this.txtusername.Text.Trim()))
                    //{
                    //    this.ltmsg.Text = label("login7");
                    //}
                    //else if (checkspace(this.txtusername.Text.Trim()))
                    //{
                    //    this.ltmsg.Text = label("login7");
                    //}
                    else if (item.Detailvphone(txt_phone.Text.Trim().ToLower()).Rows.Count > 0)
                    {
                        this.ltmsg.Text = " Điện thoại bạn đăng ký đã được sử dụng bởi một tài khoản khác.";
                    }
                    else if (!this.txtpassword.Text.Equals(this.txtxacnhanmatkhau.Text))
                    {
                        this.ltmsg.Text = "Mật khẩu không trùng hợp";
                    }
                    else if (!RegularExpressions.Password(txtxacnhanmatkhau.Text.Trim()) == true)
                    {
                        this.ltmsg.Text = "MẬT KHẨU BAO GỒM 8 KÝ TỰ : Các ký tự từ a đến z và các số từ 0 đến 9";
                        //this.ltmsg.Text = "MẬT KHẨU BAO GỒM 8 KÝ TỰ SAU <br />  - Bao gồm: ít nhất 1 chữ cái viết hoa.<br />  - Bao gồm: ít nhất 1 ký tự đặc biệt <br />  - Bao gồm: các ký tự chữ viết thường từ a đến z <br />  - Bao gồm: các số từ 0 đến 9";
                    }
                    else
                    {
                        string Nguoigioithieu = "0";
                        string VTree = "0";

                        if (txtnguoigioithieu.Text.Length > 0)
                        {
                            if (txtemail.Text.Trim() != txtnguoigioithieu.Text.Trim() || txt_phone.Text.Trim() != txtnguoigioithieu.Text.Trim())
                            {
                                List<Entity.users> iEmail = Susers.Name_Text("select * from users  where (vuserun LIKE N'" + txtnguoigioithieu.Text + "')");// and DuyetTienDanap=1/  //and iuser_id !=" + MoreAll.MoreAll.GetCookies("MembersID") + " 
                                if (iEmail.Count > 0)
                                {
                                    Nguoigioithieu = iEmail[0].iuser_id.ToString();
                                    try
                                    {
                                        VTree = iEmail[0].MTree.ToString();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                else
                                {
                                    ltmsg.Text = " Người giới thiệu không tồn tại hoặc chưa được kích hoạt trong hệ thống.";
                                    return;
                                }
                            }
                        }
                        String mtree = "|0|";
                        if (Nguoigioithieu != "0")
                        {
                            mtree = VTree;
                        }
                        // String mtrees = mtree + Nguoigioithieu + "|";
                        String mtrees = mtree;// +Nguoigioithieu + "|";
                        string validatekey = DateTime.Now.Ticks.ToString();
                        Entity.users obj = new Entity.users();
                        obj.vuserun = txt_phone.Text.Trim().ToLower();
                        obj.vuserpwd = this.txtpassword.Text;
                        obj.vfname = this.txtlastname.Text;
                        obj.vlname = this.txtlastname.Text;
                        obj.igender = int.Parse("0");
                        obj.dbirthday = DateTime.Now;
                        obj.vidcard = "0";
                        obj.vaddress = this.txt_add.Text;
                        obj.vphone = this.txt_phone.Text;
                        obj.vemail = this.txtemail.Text.Trim().ToLower();
                        obj.iregionid = int.Parse("0");
                        obj.vavatar = "";
                        obj.vavatartitle = "";
                        obj.dcreatedate = DateTime.Now;
                        obj.dlastvisited = DateTime.Now;
                        obj.vvalidatekey = validatekey;
                        obj.istatus = int.Parse("1");
                        obj.lang = language;

                        obj.Type = int.Parse(ddlloaitaikhoan.SelectedValue);// 1: kiểu thành viên , 2: hay leader
                        obj.IDChiNhanh = int.Parse(ddlchinhanh.SelectedValue);// thuộc chi nhánh nào?
                        obj.ChiNhanh = 0;// nâng cấp thành viên lên thành chi nhánh là 1
                        obj.GioiThieu = Nguoigioithieu;// txtnguoigioithieu.Text;//người giới thiệu 

                        obj.DuyetTienDanap = 1;
                        obj.TongTienDanapVND = "0";
                        obj.TongTienDanapCoin = "0";
                        obj.LevelThanhVien = 0;
                        obj.Leader = 0;
                        obj.TongTienCoinDuocCap = "0";
                        if (Nguoigioithieu == "0")
                        {
                            obj.MTree = "|0|";
                        }
                        else
                        {
                            obj.MTree = mtrees.Replace("|0|", "|");
                        }
                        obj.ViTienHHGioiThieu = "0";
                        obj.HoTro = 0;
                        obj.VIAAFFILIATE = "0";
                        obj.ViAgLang = "0";
                        obj.ThanhVienAgLang = 0;
                        obj.TienDangSoHuuBatDongSan = "0";
                        obj.Uutien = 0;
                        obj.ViUuTien = "0";

                        obj.ViQRCode = "0";
                        obj.TrangThaiThamGiaQRCode = 0;
                        obj.AnhQRCode = "";
                        obj.QRCodeChietKhauHH = "0";
                        obj.QRCodeHHNguoiMua = "0";
                        obj.QRCodeHHHeThong = "0";
                        obj.GiayPhepKinhDoanh = "";

                        obj.AnhChungMinhThuTruoc = "";
                        obj.AnhChungMinhThuSau = "";
                        obj.ViHoaHongMuaBan = "0";
                        obj.ViHoaHongAFF = "0";
                        obj.CauHinhDuyetDonTuDong = 0;
                        obj.TongSoSanPhamDaBan = 0;
                        obj.ViMuaHangAFF = "0";
                        obj.ViFMotAnTheoAgland = "0";
                        obj.ViTangTienVip = "0";
                        obj.TinhThanh = int.Parse(ddltinhthanh.SelectedValue);
                        obj.CuaHang = 0;
                        obj.TatChucNang = 0;
                        obj.TrangThaiThongBao = 0;
                        obj.DaBanDuocSanPham = 0;
                        obj.IDLuuTam = "0";
                        obj.TongTienDaMua = "0";
                        int ID = Fusers.INSERT2(obj);
                        string Cay = mtrees.Replace("|0|", "|") + ID + "|";
                        Susers.Name_Text("UPDATE [users] SET MTree='" + Cay + "' WHERE iuser_id =" + ID + "");

                        MultiView1.ActiveViewIndex = 1;
                        // Tạm thời khóa lại vì khách hàng đăng ký nhiều mail quá lên bị khóa mail lại

                        //string title = "Aggroupusa: Xác nhận tài khoản thành viên! ";
                        //string str4 = "https://" + HttpContext.Current.Request.Url.Host + "/confirm/" + validatekey + "";

                        //System.Text.StringBuilder strb = new System.Text.StringBuilder();
                        //strb.AppendLine("<p>Xin chào : " + txtlastname.Text + "</p>");
                        //strb.AppendLine("<p>Cảm ơn bạn đã đăng ký tài khoản, trên hệ webiste: aggroup365.com</p>");
                        //strb.AppendLine("<p>Bạn có thể đăng nhập bằng tài khoản trên.</p>");
                        //strb.AppendLine("<p><b>THÔNG TIN HỖ TRỢ :</b></p>");
                        //strb.AppendLine("<p><b>Hotline:</b> (+1)415.792.7729 - <b>");
                        //strb.AppendLine("<p><b>Website:</b> www.aggroup365.com </p>");

                        //string email = Email.email();
                        //string password = Email.password();
                        //int port = Convert.ToInt32(Email.port());
                        //string host = Email.host();

                        //MailUtilities.SendMail("Aggroupusa: Xác nhận tài khoản thành viên", email, password, txtemail.Text.Trim().ToLower(), host, Convert.ToInt32(port), title, strb.ToString());
                    }
                }
            }
            catch (Exception) { }
            SetCapCha();
        }
        protected bool checkspace(string text)
        {
            string[] arrtxt = text.Split(' ');
            if (arrtxt.Count() > 1)
            {
                return true;
            }
            return false;
        }
        public bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;
            return input.Any(c => c > MaxAnsiCode);
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }

    }
}