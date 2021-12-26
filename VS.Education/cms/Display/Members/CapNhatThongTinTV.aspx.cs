using Framework;
using MoreAll;
using QRCoder;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.Members
{
    public partial class CapNhatThongTinTV : System.Web.UI.Page
    {
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        private string IDMaDonTao = "1";
        string URL = "";
        protected bool Dung = false;
        string Plevel = "99999999999";
        string TongLevel = "0";
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
                    MoreAll.MoreAll.SetCookie("Members", U1, 5000);
                    MoreAll.MoreAll.SetCookie("MembersID", table[0].iuser_id.ToString(), 5000);
                    Response.Redirect("/cms/display/Members/CapNhatThongTinTV.aspx");
                }
            }

            URL = Request.RawUrl.ToString();
            if (!IsPostBack)
            {
                if (MoreAll.MoreAll.GetCookies("Members") != "")
                {
                    btnsave.Text = label("l_update");
                    ShowTinhThanh();
                    loadinformation();
                    LoadInfo();
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }
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
        private void loadinformation()
        {
            try
            {
                user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                if (table != null)
                {
                    hdid.Value = table.iuser_id.ToString();
                    hdChiNhanh.Value = table.IDChiNhanh.ToString();

                    this.txtname.Text = table.vfname.ToString();
                    this.txtaddress.Text = table.vaddress.ToString();
                    this.txtemail.Text = table.vemail.ToString();

                    this.txtbirthday.Text = ((DateTime)table.dbirthday).ToString("yyyy-MM-dd");
                    this.txtphone.Text = table.vphone.ToString();
                    this.ltloaithanhvien.Text = MoreAll.MoreAll.Kieuloai(table.Type.ToString());
                    this.kieuthanhvien.Text = MoreAll.MoreAll.Showlead(table.Leader.ToString());

                    this.txtsochungminhthu.Text = table.SoChungMinhThu;
                    this.txtnoicap.Text = table.NoiCapChungMinhThu;
                    this.txtngaycap.Text = table.NgayCapChungMinhThu;

                    txttenshop.Text = table.TenShop;
                    txtdiachikhohang.Text = table.DiaChiKhoHang;

                    WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddltinhthanh, table.TinhThanh.ToString());
                    WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlloaichungminhthu, table.LoaiChungMinh.ToString());

                    if (table.LevelThanhVien.ToString() == "0")
                    {
                        this.ltcapdo.Text = "<img style='width: 100px;' src=\"/Resources/images/1sao.png\" />";
                    }
                    else if (table.LevelThanhVien.ToString() == "1")
                    {
                        this.ltcapdo.Text = "<img style='width: 100px;' src=\"/Resources/images/2sao.png\" />";
                    }
                    else if (table.LevelThanhVien.ToString() == "2")
                    {
                        this.ltcapdo.Text = "<img style='width: 100px;' src=\"/Resources/images/3sao.png\" />";
                    }
                    else if (table.LevelThanhVien.ToString() == "3")
                    {
                        this.ltcapdo.Text = "<img style='width: 100px;' src=\"/Resources/images/4sao.png\" />";
                    }
                    else if (table.LevelThanhVien.ToString() == "4")
                    {
                        this.ltcapdo.Text = "<img style='width: 100px;' src=\"/Resources/images/5sao.png\" />";
                    }
                    else if (table.LevelThanhVien.ToString() == "5")
                    {
                        this.ltcapdo.Text = "<img style='width: 100px;' src=\"/Resources/images/6sao.png\" />";
                    }
                    if (table.Type.ToString() == "2")
                    {
                        Panel2.Visible = true;
                    }
                    else
                    {
                        Panel2.Visible = false;
                    }
                    if (table.DuyetTienDanap.ToString() == "1")
                    {
                        ltkickhoat.Text = "Đã kích hoạt";
                        btkichhoat.Visible = false;
                        ddlvitien.Visible = false;
                    }
                    else
                    {
                        btkichhoat.Visible = true;
                        ddlvitien.Visible = true;
                    }

                    if (table.TrangThaiThamGiaQRCode.ToString() == "1")
                    {
                        btqrcode.Visible = false;
                        Panel1.Visible = true;

                        this.txtchietkhauQRcode.Text = table.QRCodeChietKhauHH.ToString();
                        this.txthoahongnguoimuaQRcode.Text = table.QRCodeHHNguoiMua.ToString();
                        this.txthoahonghethongQRcode.Text = table.QRCodeHHHeThong.ToString();
                        ltQRcode.Text = "<img style='width: 200px;' src=\"" + table.AnhQRCode.ToString() + "\" />";
                    }
                    else
                    {
                        btqrcode.Visible = true;
                    }

                    if (table.ChiNhanh.ToString() != "0")
                    {
                        if (ShowTenChiNhanh(table.iuser_id.ToString()) != "0")
                        {
                            this.ltchinhanh.Text = "<img class='iconthongbao' src='/Resources/images/iconthongbao.jpg' /> Bạn là chi nhánh: " + ShowTenChiNhanh(table.iuser_id.ToString());
                        }
                        else
                        {
                            this.ltchinhanh.Text = "<img class='iconthongbao' src='/Resources/images/iconthongbao.jpg' /> Bạn không phải là chi nhánh";
                        }
                    }
                    else
                    {
                        this.ltchinhanh.Text = "<img class='iconthongbao' src='/Resources/images/iconthongbao.jpg' /> Bạn không phải là chi nhánh";
                    }
                    this.ltthuocchinhanh.Text = ShowChiNhanh(table.IDChiNhanh.ToString());
                    //   this.lttrangthainaptien.Text = table.TongTienDanapCoin.ToString();

                    if (!table.GioiThieu.Equals("0"))
                    {
                        this.txttennguoigioithieu.Text = ShowName(table.GioiThieu.ToString());
                        txttennguoigioithieu.Enabled = false;
                    }

                }
            }
            catch (Exception)
            { }
        }

        protected string ShowTenChiNhanh(string id)
        {
            string str = "0";
            List<Entity.Menu> dt = SMenu.Name_Text("select * from Menu where capp='" + More.DL + "' and Type=" + id + " ");
            if (dt.Count >= 1)
            {
                str = dt[0].Name;
            }
            return str;
        }
        protected string ShowName(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count >= 1)
            {
                str = dt[0].vuserun;
            }
            return str;
        }
        protected string ShowChiNhanh(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count >= 1)
            {
                str = dt[0].Name;
            }
            return str;
        }
        private void LoadInfo()
        {
            try
            {
                user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                if (table != null)
                {
                    if (table.vavatar.ToString().Length < 1)
                    {
                        this.ltimg.Text = "<img class='iconthongbao' src='/Uploads/avatar/no_avatar.png' class=admavatarimg>";
                    }
                    else
                    {
                        this.ltimg.Text = "<img class='iconthongbao' src='/Uploads/avatar/" + table.vavatar.ToString() + "' class=admavatarimg>";
                    }

                    if (table.GiayPhepKinhDoanh.ToString().Length < 1)
                    {
                        this.ltimggiayphep.Text = "";
                    }
                    else
                    {
                        this.ltimggiayphep.Text = "<img class='iconthongbao' src='/Uploads/DangKyKinhDoanh/" + table.GiayPhepKinhDoanh.ToString() + "' class=admavatarimg>";
                    }

                    if (table.AnhChungMinhThuTruoc.ToString().Length < 1)
                    {
                        this.lanhchungminhthutruoc.Text = "";
                    }
                    else
                    {
                        this.lanhchungminhthutruoc.Text = "<img class='iconthongbao' src='/Uploads/ChungMinhThu/" + table.AnhChungMinhThuTruoc.ToString() + "' class=admavatarimg>";
                    }

                    if (table.AnhChungMinhThuSau.ToString().Length < 1)
                    {
                        this.lanhchungminhthusau.Text = "";
                    }
                    else
                    {
                        this.lanhchungminhthusau.Text = "<img class='iconthongbao' src='/Uploads/ChungMinhThu/" + table.AnhChungMinhThuSau.ToString() + "' class=admavatarimg>";
                    }
                    this.hdimg.Value = table.vavatar.ToString();
                    this.hdchungminhthumattruoc.Value = table.AnhChungMinhThuTruoc.ToString();
                    this.hdchungminhthumatsau.Value = table.AnhChungMinhThuSau.ToString();
                    this.hdgiayphep.Value = table.GiayPhepKinhDoanh.ToString();
                }
            }
            catch (Exception)
            { }

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            if (this.flavatar.HasFile)
            {
                //if ((this.flavatar.PostedFile.ContentLength / 0x400) > 0x400)
                //{
                //    this.ltmsg.Text = "<img class='iconthongbao' src='/Resources/images/iconthongbao.jpg' /> Cập nhật với dung lượng 1 M";
                //    return;
                //}
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                string extension = Path.GetExtension(Path.GetFileName(this.flavatar.PostedFile.FileName));
                if (this.hdimg.Value.Length > 0)
                {
                    try
                    {
                        File.Delete(utlitities.APPL_PHYSICAL_PATH + "/Uploads/avatar/" + this.hdimg.Value);
                    }
                    catch (Exception)
                    {
                    }
                }
                string str = DateTime.Now.Ticks.ToString() + extension;
                this.hdimg.Value = str;
                try
                {
                    this.flavatar.PostedFile.SaveAs(utlitities.APPL_PHYSICAL_PATH + "/Uploads/avatar/" + str);
                }
                catch (Exception)
                {
                }
            }

            if (this.flileGiayDangKyKinhDoanh.HasFile)
            {
                //if ((this.flileGiayDangKyKinhDoanh.PostedFile.ContentLength / 0x400) > 0x400)
                //{
                //    this.ltimggiayphep.Text = "<img class='iconthongbao' src='/Resources/images/iconthongbao.jpg' /> Cập nhật với dung lượng 1 M";
                //    return;
                //}
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                string extension = Path.GetExtension(Path.GetFileName(this.flileGiayDangKyKinhDoanh.PostedFile.FileName));
                if (this.hdgiayphep.Value.Length > 0)
                {
                    try
                    {
                        File.Delete(utlitities.APPL_PHYSICAL_PATH + "/Uploads/DangKyKinhDoanh/" + this.hdgiayphep.Value);
                    }
                    catch (Exception)
                    {
                    }
                }
                string str = DateTime.Now.Ticks.ToString() + extension;
                this.hdgiayphep.Value = str;
                try
                {
                    this.flileGiayDangKyKinhDoanh.PostedFile.SaveAs(utlitities.APPL_PHYSICAL_PATH + "/Uploads/DangKyKinhDoanh/" + str);
                }
                catch (Exception)
                {
                }
            }


            if (this.flchungminhthutruoc.HasFile)
            {
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                string extension = Path.GetExtension(Path.GetFileName(this.flchungminhthutruoc.PostedFile.FileName));
                if (this.hdchungminhthumattruoc.Value.Length > 0)
                {
                    try
                    {
                        File.Delete(utlitities.APPL_PHYSICAL_PATH + "/Uploads/ChungMinhThu/" + this.hdchungminhthumattruoc.Value);
                    }
                    catch (Exception)
                    {
                    }
                }
                string str = DateTime.Now.Ticks.ToString() + extension;
                this.hdchungminhthumattruoc.Value = str;
                try
                {
                    this.flchungminhthutruoc.PostedFile.SaveAs(utlitities.APPL_PHYSICAL_PATH + "/Uploads/ChungMinhThu/" + str);
                }
                catch (Exception)
                {
                }
            }

            if (this.flchungminhthusau.HasFile)
            {
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                string extension = Path.GetExtension(Path.GetFileName(this.flchungminhthusau.PostedFile.FileName));
                if (this.hdchungminhthumatsau.Value.Length > 0)
                {
                    try
                    {
                        File.Delete(utlitities.APPL_PHYSICAL_PATH + "/Uploads/ChungMinhThu/" + this.hdchungminhthumatsau.Value);
                    }
                    catch (Exception)
                    {
                    }
                }
                string str = DateTime.Now.Ticks.ToString() + extension;
                this.hdchungminhthumatsau.Value = str;
                try
                {
                    this.flchungminhthusau.PostedFile.SaveAs(utlitities.APPL_PHYSICAL_PATH + "/Uploads/ChungMinhThu/" + str);
                }
                catch (Exception)
                {
                }
            }

            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                if (table != null)
                {
                    Entity.users obj = new Entity.users();
                    obj.vuserun = table.vuserun.ToString();
                    obj.vavatar = this.hdimg.Value;
                    obj.GiayPhepKinhDoanh = this.hdgiayphep.Value;
                    obj.AnhChungMinhThuTruoc = hdchungminhthumattruoc.Value;
                    obj.AnhChungMinhThuSau = hdchungminhthumatsau.Value;
                    obj.vavatartitle = "";
                    Susers.users_update_updateavatar(obj);
                }
            }
            this.ltmsg.Text = "";

            try
            {
                List<user> list = db.users.Where(s => s.vemail == txtemail.Text.Trim() && s.iuser_id != int.Parse(hdid.Value)).ToList();
                List<user> listDienThoai = db.users.Where(s => s.vphone == txtphone.Text.Trim() && s.iuser_id != int.Parse(hdid.Value)).ToList();
                if (listDienThoai.Count > 0)
                {
                    ltmsg.Text = "Điện thoại đã tồn tại trong hệ thống";
                    return;
                }
                else if (list.Count > 0)
                {
                    ltmsg.Text = "Email đã tồn tại trong hệ thống";
                    return;
                }
                user data = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
                data.iuser_id = int.Parse(hdid.Value);
                data.vuserun = MoreAll.MoreAll.GetCookies("Members").ToString();
                data.vfname = this.txtname.Text;
                data.vlname = this.txtname.Text;
                data.igender = 0;
                data.dbirthday = Convert.ToDateTime(this.txtbirthday.Text);
                data.vaddress = this.txtaddress.Text;
                data.vphone = this.txtphone.Text;
                data.vavatartitle = "";
                data.vemail = txtemail.Text;
                data.SoChungMinhThu = txtsochungminhthu.Text;
                data.NoiCapChungMinhThu = txtnoicap.Text;
                data.NgayCapChungMinhThu = txtngaycap.Text;
                data.LoaiChungMinh = int.Parse(ddlloaichungminhthu.SelectedValue);
                data.TrangThaiNhaCungCap = 0;
                data.TongSoSanPham = 0;
                data.ToiDongYCamKet = 0;
                data.TenShop = txttenshop.Text;
                data.DiaChiKhoHang = txtdiachikhohang.Text;
                data.MaSoDoanhNghiep = txtmasodoanhnghiep.Text;
                data.TinhThanh = int.Parse(ddltinhthanh.SelectedValue);
                db.SubmitChanges();
            }
            catch (Exception)
            {
            }
            ThemNguoiGioiThieu();
            ltmsg.Text += "<img class='iconthongbao' src='/Resources/images/iconthongbao.jpg' /> Thông tin đã được thay đổi";
            LoadInfo();
            loadinformation();
            if (ltmsg.Text.Contains("Thông tin đã được thay đổi"))
            {
                // ltmsg.Text += "<meta http-equiv=\"refresh\" content=\"1;url=" + Request.RawUrl.ToString() + "\">";
            }
        }
        void ThemNguoiGioiThieu()
        {
            if (txttennguoigioithieu.Text.Length > 0)
            {
                List<Entity.users> iEmail = Susers.Name_Text("select * from users  where (vuserun LIKE N'" + txttennguoigioithieu.Text + "' )  and iuser_id !=" + hdid.Value + " and DuyetTienDanap=1 ");
                if (iEmail.Count() == 0)
                {
                    ltmess.Text = "<img class='iconthongbao' src='/Resources/images/iconthongbao.jpg' /> Người giới thiệu không tồn tại hoặc chưa được kích hoạt trong hệ thống.";
                }
                else
                {
                    string Nguoigioithieu = "0";
                    string VTree = "0";

                    Nguoigioithieu = iEmail[0].iuser_id.ToString();
                    VTree = iEmail[0].MTree.ToString();

                    String mtree = "|0|";
                    if (Nguoigioithieu != "0")
                    {
                        mtree = VTree;
                    }
                    String mtrees = mtree + Nguoigioithieu + "|";
                    user data = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
                    data.iuser_id = int.Parse(hdid.Value);
                    data.GioiThieu = iEmail[0].iuser_id.ToString();
                    if (Nguoigioithieu == "0")
                    {
                        data.MTree = "|0|";
                    }
                    else
                    {
                        data.MTree = mtrees.Replace("|0|", "|");
                    }
                    db.SubmitChanges();
                    //ltmess.Text = "<img class='iconthongbao' src='/Resources/images/iconthongbao.jpg' /> Cập nhật thành công Người giới thiệu";
                }
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }

        protected void btkichhoat_Click(object sender, EventArgs e)
        {
            IDMaDonTao = MoreAll.MoreAll.FormatDate_ID(DateTime.Now);

            string ThanhVienGioiThieu = "0";
            user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value) && p.DuyetTienDanap == 0);
            if (iitem != null)
            {
                double Tiencoin = Convert.ToDouble(Commond.Setting("TienKichHoat"));
                double TienVND = (Tiencoin) * 1000;
                #region Nếu nạp tiền >=480 điển thì sẽ được kích hoạt thành viên ngay. Nếu không được thì vào admin kích hoạt nạp tiền
                double TCoin = Convert.ToDouble(Commond.Setting("TienKichHoat"));
                double TongViTien = 0;
                string Alet = "";
                if (ddlvitien.SelectedValue == "1")
                {
                    Alet = " Ví Quản lý ";
                    TongViTien = Convert.ToDouble(iitem.VIAAFFILIATE);
                }
                else if (ddlvitien.SelectedValue == "2")
                {
                    Alet = " Ví Thương Mại ";
                    TongViTien = Convert.ToDouble(iitem.TongTienCoinDuocCap);
                }

                //else if (ddlvitien.SelectedValue == "3")
                //{
                //    Alet = " Ví Mua Hàng ";
                //    TongViTien = Convert.ToDouble(iitem.ViMuaHangAFF);
                //}
                if (TongViTien >= TCoin)
                {
                    string Diemcoin = Commond.Setting("TienKichHoat");
                    string IDThanhVien = hdid.Value;
                    #region Thành viên và leader

                    #region Ví chuyên gia
                    try
                    {
                        user ChuyenGiga = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(SetVi.SetThanhVienChuyenGia()));
                        if (ChuyenGiga != null)
                        {
                            string IDBanChuyenGia = SetVi.SetThanhVienChuyenGia();
                            double HHChuyengia = Convert.ToDouble(Commond.Setting("txtbanchuyengianDangKy"));
                            double TienHH = Convert.ToDouble(480 * HHChuyengia) / 100;
                            ThemHoaHong("0", "400", "Hoa hồng quản lý - Ban Đào tạo - Chuyên gia ", IDThanhVien, IDBanChuyenGia, HHChuyengia.ToString(), TienHH.ToString(), IDMaDonTao, "");
                        }
                    }
                    catch (Exception)
                    { }
                    #endregion

                    List<Entity.users> dt = Susers.Name_Text("select * from users  where iuser_id=" + hdid.Value + " ");//and ChiNhanh=0
                    if (dt.Count() > 0)
                    {
                        //#region Thay đổi trạng thái ví hỗ trợ nếu hotro=1 là mới giới thiệu được thành viên mới xong thì mới được tính hoa hồng hỗ trợ sang ví tổng
                        //Susers.Name_Text("update users set HoTro=1 where iuser_id=" + dt[0].GioiThieu.ToString() + "");
                        //#endregion

                        //List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + dt[0].GioiThieu.ToString() + " ");
                        //if (F1.Count() > 0)
                        //{
                        //    if (!F1[0].GioiThieu.Equals("0") && F1[0].DuyetTienDanap.ToString() != "0")
                        //    {
                        //        #region Hoa Hồng cho người giới thiệu trực tiếp  F1 30%
                        //        double HoaHongTrucTiep = Convert.ToDouble(Commond.Setting("hoahonggttructiep"));
                        //        ThemHoaHong("1", "Hoa hồng quản lý 1", hdid.Value, dt[0].GioiThieu.ToString(), HoaHongTrucTiep.ToString(), HoaHongTrucTiep.ToString(), IDMaDonTao);
                        //        #endregion
                        //    }
                        //    #region Hoa hồng cho F2
                        //    List<Entity.users> F2 = Susers.Name_Text("select * from users  where iuser_id=" + dt[0].GioiThieu.ToString() + " ");
                        //    if (F2.Count() > 0)
                        //    {
                        //        double HHQuanLyF2 = Convert.ToDouble(Commond.Setting("HHQuanLyF2"));
                        //        if (!F2[0].GioiThieu.Equals("0") && F2[0].DuyetTienDanap.ToString() != "0")
                        //        {
                        //            ThemHoaHong("3", "Hoa hồng quản lý 2", hdid.Value, F2[0].GioiThieu.ToString(), HHQuanLyF2.ToString(), HHQuanLyF2.ToString(), IDMaDonTao);
                        //        }


                        //        #region Hoa hồng cho F3
                        //        List<Entity.users> F3 = Susers.Name_Text("select * from users  where iuser_id=" + F2[0].GioiThieu.ToString() + " ");
                        //        if (F3.Count() > 0)
                        //        {
                        //            double HHQuanLyF3 = Convert.ToDouble(Commond.Setting("HHQuanLyF3F4"));
                        //            if (!F3[0].GioiThieu.Equals("0") && F3[0].DuyetTienDanap.ToString() != "0")
                        //            {
                        //                ThemHoaHong("3", "Hoa hồng quản lý 3", hdid.Value, F3[0].GioiThieu.ToString(), HHQuanLyF3.ToString(), HHQuanLyF3.ToString(), IDMaDonTao);
                        //            }
                        //            #region Hoa hồng cho F4
                        //            List<Entity.users> F4 = Susers.Name_Text("select * from users  where iuser_id=" + F3[0].GioiThieu.ToString() + " ");
                        //            if (F4.Count() > 0)
                        //            {
                        //                double HHQuanLyF4 = Convert.ToDouble(Commond.Setting("HHQuanLyF3F4"));
                        //                if (!F4[0].GioiThieu.Equals("0") && F4[0].DuyetTienDanap.ToString() != "0")
                        //                {
                        //                    ThemHoaHong("3", "Hoa hồng quản lý 4", hdid.Value, F4[0].GioiThieu.ToString(), HHQuanLyF4.ToString(), HHQuanLyF4.ToString(), IDMaDonTao);
                        //                }
                        //                #region Hoa hồng cho F5
                        //                List<Entity.users> F5 = Susers.Name_Text("select * from users  where iuser_id=" + F4[0].GioiThieu.ToString() + " ");
                        //                if (F5.Count() > 0)
                        //                {
                        //                    double HHQuanLyF5 = Convert.ToDouble(Commond.Setting("HHQuanLyF3F4"));
                        //                    if (!F5[0].GioiThieu.Equals("0") && F5[0].DuyetTienDanap.ToString() != "0")
                        //                    {
                        //                        ThemHoaHong("3", "Hoa hồng quản lý 5", hdid.Value, F5[0].GioiThieu.ToString(), HHQuanLyF5.ToString(), HHQuanLyF5.ToString(), IDMaDonTao);
                        //                    }
                        //                    #region Hoa hồng cho F6
                        //                    List<Entity.users> F6 = Susers.Name_Text("select * from users  where iuser_id=" + F5[0].GioiThieu.ToString() + " ");
                        //                    if (F6.Count() > 0)
                        //                    {
                        //                        double HHQuanLyF6 = Convert.ToDouble(Commond.Setting("HHQuanLyF5F10"));
                        //                        if (!F6[0].GioiThieu.Equals("0") && F6[0].DuyetTienDanap.ToString() != "0")
                        //                        {
                        //                            ThemHoaHong("3", "Hoa hồng quản lý 6", hdid.Value, F6[0].GioiThieu.ToString(), HHQuanLyF6.ToString(), HHQuanLyF6.ToString(), IDMaDonTao);
                        //                        }
                        //                        #region Hoa hồng cho F7
                        //                        List<Entity.users> F7 = Susers.Name_Text("select * from users  where iuser_id=" + F6[0].GioiThieu.ToString() + " ");
                        //                        if (F7.Count() > 0)
                        //                        {
                        //                            double HHQuanLyF7 = Convert.ToDouble(Commond.Setting("HHQuanLyF5F10"));
                        //                            if (!F7[0].GioiThieu.Equals("0") && F7[0].DuyetTienDanap.ToString() != "0")
                        //                            {
                        //                                ThemHoaHong("3", "Hoa hồng quản lý 7", hdid.Value, F7[0].GioiThieu.ToString(), HHQuanLyF7.ToString(), HHQuanLyF7.ToString(), IDMaDonTao);
                        //                            }
                        //                            #region Hoa hồng cho F8
                        //                            List<Entity.users> F8 = Susers.Name_Text("select * from users  where iuser_id=" + F7[0].GioiThieu.ToString() + " ");
                        //                            if (F8.Count() > 0)
                        //                            {
                        //                                double HHQuanLyF8 = Convert.ToDouble(Commond.Setting("HHQuanLyF5F10"));
                        //                                if (!F8[0].GioiThieu.Equals("0") && F8[0].DuyetTienDanap.ToString() != "0")
                        //                                {
                        //                                    ThemHoaHong("3", "Hoa hồng quản lý 8", hdid.Value, F8[0].GioiThieu.ToString(), HHQuanLyF8.ToString(), HHQuanLyF8.ToString(), IDMaDonTao);
                        //                                }
                        //                                #region Hoa hồng cho F9
                        //                                List<Entity.users> F9 = Susers.Name_Text("select * from users  where iuser_id=" + F8[0].GioiThieu.ToString() + " ");
                        //                                if (F9.Count() > 0)
                        //                                {
                        //                                    double HHQuanLyF9 = Convert.ToDouble(Commond.Setting("HHQuanLyF5F10"));
                        //                                    if (!F9[0].GioiThieu.Equals("0") && F9[0].DuyetTienDanap.ToString() != "0")
                        //                                    {
                        //                                        ThemHoaHong("3", "Hoa hồng quản lý 9", hdid.Value, F9[0].GioiThieu.ToString(), HHQuanLyF9.ToString(), HHQuanLyF9.ToString(), IDMaDonTao);
                        //                                    }
                        //                                    #region Hoa hồng cho F10
                        //                                    List<Entity.users> F10 = Susers.Name_Text("select * from users  where iuser_id=" + F9[0].GioiThieu.ToString() + " ");
                        //                                    if (F10.Count() > 0)
                        //                                    {
                        //                                        double HHQuanLyF10 = Convert.ToDouble(Commond.Setting("HHQuanLyF5F10"));
                        //                                        if (!F10[0].GioiThieu.Equals("0") && F10[0].DuyetTienDanap.ToString() != "0")
                        //                                        {
                        //                                            ThemHoaHong("3", "Hoa hồng quản lý 10", hdid.Value, F10[0].GioiThieu.ToString(), HHQuanLyF10.ToString(), HHQuanLyF10.ToString(), IDMaDonTao);
                        //                                        }
                        //                                        #region Hoa hồng cho F11
                        //                                        List<Entity.users> F11 = Susers.Name_Text("select * from users  where iuser_id=" + F10[0].GioiThieu.ToString() + " ");
                        //                                        if (F11.Count() > 0)
                        //                                        {
                        //                                            double HHQuanLyF11 = Convert.ToDouble(Commond.Setting("HHQuanLyF11F15"));
                        //                                            if (!F11[0].GioiThieu.Equals("0") && F11[0].DuyetTienDanap.ToString() != "0")
                        //                                            {
                        //                                                ThemHoaHong("3", "Hoa hồng quản lý 11", hdid.Value, F11[0].GioiThieu.ToString(), HHQuanLyF11.ToString(), HHQuanLyF11.ToString(), IDMaDonTao);
                        //                                            }
                        //                                            #region Hoa hồng cho F12
                        //                                            List<Entity.users> F12 = Susers.Name_Text("select * from users  where iuser_id=" + F11[0].GioiThieu.ToString() + " ");
                        //                                            if (F12.Count() > 0)
                        //                                            {
                        //                                                double HHQuanLyF12 = Convert.ToDouble(Commond.Setting("HHQuanLyF11F15"));
                        //                                                if (!F12[0].GioiThieu.Equals("0") && F12[0].DuyetTienDanap.ToString() != "0")
                        //                                                {
                        //                                                    ThemHoaHong("3", "Hoa hồng quản lý 12", hdid.Value, F12[0].GioiThieu.ToString(), HHQuanLyF12.ToString(), HHQuanLyF12.ToString(), IDMaDonTao);
                        //                                                }
                        //                                                #region Hoa hồng cho F13
                        //                                                List<Entity.users> F13 = Susers.Name_Text("select * from users  where iuser_id=" + F12[0].GioiThieu.ToString() + " ");
                        //                                                if (F13.Count() > 0)
                        //                                                {
                        //                                                    double HHQuanLyF13 = Convert.ToDouble(Commond.Setting("HHQuanLyF11F15"));
                        //                                                    if (!F13[0].GioiThieu.Equals("0") && F13[0].DuyetTienDanap.ToString() != "0")
                        //                                                    {
                        //                                                        ThemHoaHong("3", "Hoa hồng quản lý 13", hdid.Value, F13[0].GioiThieu.ToString(), HHQuanLyF13.ToString(), HHQuanLyF13.ToString(), IDMaDonTao);
                        //                                                    }
                        //                                                    #region Hoa hồng cho F14
                        //                                                    List<Entity.users> F14 = Susers.Name_Text("select * from users  where iuser_id=" + F13[0].GioiThieu.ToString() + " ");
                        //                                                    if (F14.Count() > 0)
                        //                                                    {
                        //                                                        double HHQuanLyF14 = Convert.ToDouble(Commond.Setting("HHQuanLyF11F15"));
                        //                                                        if (!F14[0].GioiThieu.Equals("0") && F14[0].DuyetTienDanap.ToString() != "0")
                        //                                                        {
                        //                                                            ThemHoaHong("3", "Hoa hồng quản lý 14", hdid.Value, F14[0].GioiThieu.ToString(), HHQuanLyF14.ToString(), HHQuanLyF14.ToString(), IDMaDonTao);
                        //                                                        }
                        //                                                        #region Hoa hồng cho F15
                        //                                                        List<Entity.users> F15 = Susers.Name_Text("select * from users  where iuser_id=" + F14[0].GioiThieu.ToString() + " ");
                        //                                                        if (F15.Count() > 0)
                        //                                                        {
                        //                                                            double HHQuanLyF15 = Convert.ToDouble(Commond.Setting("HHQuanLyF11F15"));
                        //                                                            if (!F15[0].GioiThieu.Equals("0") && F15[0].DuyetTienDanap.ToString() != "0")
                        //                                                            {
                        //                                                                ThemHoaHong("3", "Hoa hồng quản lý 15", hdid.Value, F15[0].GioiThieu.ToString(), HHQuanLyF15.ToString(), HHQuanLyF15.ToString(), IDMaDonTao);
                        //                                                            }
                        //                                                        }
                        //                                                        #endregion
                        //                                                    }
                        //                                                    #endregion
                        //                                                }
                        //                                                #endregion
                        //                                            }
                        //                                            #endregion
                        //                                        }
                        //                                        #endregion
                        //                                    }
                        //                                    #endregion
                        //                                }
                        //                                #endregion
                        //                            }
                        //                            #endregion
                        //                        }
                        //                        #endregion
                        //                    }
                        //                    #endregion
                        //                }
                        //                #endregion
                        //            }
                        //            #endregion
                        //        }
                        //        #endregion
                        //    }
                        //    #endregion
                        //}

                        #region Hoa hồng cấp quản lý và F1
                        #region HHF1
                        List<Entity.users> F00 = Susers.Name_Text("select * from users  where iuser_id=" + IDThanhVien + " ");
                        if (F00.Count() > 0)
                        {
                            double TongTienNap = Convert.ToDouble("480");
                            double HoaHongGioiThieuF1 = Convert.ToDouble(Commond.Setting("hoahonggttructiep"));
                            double HoaHongTrucTiep = (TongTienNap * HoaHongGioiThieuF1) / 100;
                            if (!F00[0].GioiThieu.Equals("0"))
                            {
                                #region Hoa Hồng cho người giới thiệu trực tiếp  F1 30%
                                ThemHoaHong("0", "1", "Hoa hồng quản lý 1", IDThanhVien, F00[0].GioiThieu.ToString(), HoaHongGioiThieuF1.ToString(), HoaHongTrucTiep.ToString(), IDMaDonTao, "");
                                #endregion
                            }
                            List<Entity.users> F02 = Susers.Name_Text("select * from users  where iuser_id=" + F00[0].GioiThieu + " ");
                            if (F02.Count() > 0)
                            {
                                double HoaHongGioiThieuF2 = Convert.ToDouble(MoreAll.Other.Giatri("txtHHGTF2"));
                                double HoaHongF2 = (HoaHongTrucTiep * HoaHongGioiThieuF2) / 100;
                                if (!F02[0].GioiThieu.Equals("0"))
                                {
                                    ThemHoaHong("0", "1", "Hoa hồng quản lý 2", IDThanhVien, F02[0].GioiThieu.ToString(), HoaHongGioiThieuF2.ToString(), HoaHongF2.ToString(), IDMaDonTao, "");
                                }
                                List<Entity.users> F03 = Susers.Name_Text("select * from users  where iuser_id=" + F02[0].GioiThieu + " ");
                                if (F03.Count() > 0)
                                {
                                    double HoaHongGioiThieuF3 = Convert.ToDouble(MoreAll.Other.Giatri("txtHHGTF3"));
                                    double HoaHongF3 = (HoaHongF2 * HoaHongGioiThieuF3) / 100;
                                    if (!F03[0].GioiThieu.Equals("0"))
                                    {
                                        ThemHoaHong("0", "1", "Hoa hồng quản lý 3", IDThanhVien, F03[0].GioiThieu.ToString(), HoaHongGioiThieuF3.ToString(), HoaHongF3.ToString(), IDMaDonTao, "");
                                    }
                                    List<Entity.users> F04 = Susers.Name_Text("select * from users  where iuser_id=" + F03[0].GioiThieu + " ");
                                    if (F04.Count() > 0)
                                    {
                                        double HoaHongGioiThieuF4 = Convert.ToDouble(MoreAll.Other.Giatri("txtHHGTF4"));
                                        double HoaHongF4 = (HoaHongF3 * HoaHongGioiThieuF4) / 100;
                                        if (!F04[0].GioiThieu.Equals("0"))
                                        {
                                            ThemHoaHong("0", "1", "Hoa hồng quản lý 4", IDThanhVien, F04[0].GioiThieu.ToString(), HoaHongGioiThieuF4.ToString(), HoaHongF4.ToString(), IDMaDonTao, "");
                                        }
                                        List<Entity.users> F05 = Susers.Name_Text("select * from users  where iuser_id=" + F04[0].GioiThieu + " ");
                                        if (F05.Count() > 0)
                                        {
                                            double HoaHongGioiThieuF5 = Convert.ToDouble(MoreAll.Other.Giatri("txtHHGTF5"));
                                            double HoaHongF5 = (HoaHongF4 * HoaHongGioiThieuF5) / 100;
                                            if (!F05[0].GioiThieu.Equals("0"))
                                            {
                                                ThemHoaHong("0", "1", "Hoa hồng quản lý 5", IDThanhVien, F05[0].GioiThieu.ToString(), HoaHongGioiThieuF5.ToString(), HoaHongF5.ToString(), IDMaDonTao, "");
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
                            ThemHoaHong("2", "Hoa Hồng trực tiếp cho Leader (Giới Thiệu)", hdid.Value, TimLeader(dt[0].GioiThieu), HoaHongLeader.ToString(), LeadTongCoin.ToString(), IDMaDonTao);
                        }
                        #endregion

                    }
                    #endregion

                    #region Chi  nhánh  ko được thưởng theo hình thức giới thiệu lên chỉ ăn 10 % phí đăng ký tài khoản mới trong mạng lưới của mình
                    List<Entity.users> dtchinhanh = Susers.Name_Text("select * from users  where iuser_id=" + hdid.Value + "");// and ChiNhanh=1
                    if (dtchinhanh.Count() > 0)
                    {
                        #region Chi nhánh được hưởng 10%
                        if (!dtchinhanh[0].IDChiNhanh.Equals("0"))//&& dtchinhanh[0].DuyetTienDanap.ToString() != "0"
                        {
                            if (ShowIDChiNhanh(dtchinhanh[0].IDChiNhanh.ToString()) != "0" && !dtchinhanh[0].DuyetTienDanap.Equals("0"))
                            {
                                double HoaHongChiNhanh = Convert.ToDouble(Commond.Setting("hoahonggtchinhanh"));
                                double TongCoin = (Tiencoin * HoaHongChiNhanh) / 100;
                                ThemHoaHong("5", "Hoa Hồng (Giới Thiệu Chi Nhánh)", hdid.Value, ShowIDChiNhanh(dtchinhanh[0].IDChiNhanh.ToString()), HoaHongChiNhanh.ToString(), TongCoin.ToString(), IDMaDonTao);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    // Nếu người giới thiệu là leader thì sẽ được hưởng thêm 10% từ người giới thiệu từ mạng lưới cấp dưới của mình
                    // TH1:,TH2:,TH4:
                    #region kiểm tra nếu đã được kích hoạt rồi thì ko kích hoạt dc nữa
                    //kiểm tra nếu đã được kích hoạt rồi thì ko kích hoạt dc nữa
                    user dkkl = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
                    if (dkkl != null)
                    {
                        if (dkkl.DuyetTienDanap == 0)
                        {
                            double TongSoCoinDaCos = 0;
                            if (ddlvitien.SelectedValue == "1")
                            {
                                TongSoCoinDaCos = Convert.ToDouble(dkkl.VIAAFFILIATE);
                            }
                            else if (ddlvitien.SelectedValue == "2")
                            {
                                TongSoCoinDaCos = Convert.ToDouble(dkkl.TongTienCoinDuocCap);
                            }
                            double TongTienNapVaos = Convert.ToDouble(Tiencoin.ToString());
                            double Conglais = 0;
                            // trừ tiền và coin đi
                            Conglais = ((TongSoCoinDaCos) - (TongTienNapVaos));
                            if (ddlvitien.SelectedValue == "1")
                            {
                                Susers.Name_Text("update users set DuyetTienDanap=1,TongTienDanapVND=" + TienVND.ToString() + ",TongTienDanapCoin=" + Tiencoin.ToString() + ",VIAAFFILIATE=" + Conglais.ToString() + "  where iuser_id=" + hdid.Value + "");
                            }
                            else if (ddlvitien.SelectedValue == "2")
                            {
                                Susers.Name_Text("update users set DuyetTienDanap=1,TongTienDanapVND=" + TienVND.ToString() + ",TongTienDanapCoin=" + Tiencoin.ToString() + ",TongTienCoinDuocCap=" + Conglais.ToString() + "  where iuser_id=" + hdid.Value + "");
                            }
                            LichSuGiaoDich("18", "Nạp tiền đăng ký thành viên 480 ngàn cho thành viên Từ " + Alet + "", "0", hdid.Value, "0", Commond.Setting("TienKichHoat").ToString());
                        }
                    }
                    #endregion

                    #region Nâng cấp level thành viên
                    NangLevel.UpDate_NangLevel(hdid.Value);
                    #endregion

                    //#region ViHoaHongChuyenGia
                    ////  ViHoaHongChuyenGia
                    //// 1: Hoa hồng AFF
                    //// 2: Hoa hồng AgLand
                    //double PhanTram = Convert.ToDouble(Commond.Setting("txtAFFChuyengia"));
                    //double ThanhTien = (Tiencoin * PhanTram) / 100; // VD: 480*0,05=24 
                    //// ví chuyên gia AFF
                    //ViHoaHongChuyenGia obp = new ViHoaHongChuyenGia();
                    //obp.IDDonHang = Convert.ToInt64(IDMaDonTao);
                    //obp.IDThanhVien = Convert.ToInt64(Commond.SetThanhVienChuyenGia());
                    //obp.IDThanhVienMua_KichHoat = Convert.ToInt64(ThanhVienGioiThieu.ToString());
                    //obp.TongDiem = ThanhTien.ToString();
                    //obp.LoaiHoaHong = 1; //1: Hoa hồng AFF
                    //obp.NgayTao = DateTime.Now;
                    //obp.PhanTram = int.Parse(Commond.Setting("txtAFFChuyengia").ToString());
                    //db.ViHoaHongChuyenGias.InsertOnSubmit(obp);
                    //db.SubmitChanges();
                    //#endregion

                    #region Vi Loi Nhuan sau khi da chia HH
                    //try
                    //{
                    var tongdiemdachia = db.S_TongDiemDaChia_DangKyThanhVien(int.Parse(hdid.Value), Convert.ToInt64(IDMaDonTao)).ToList();
                    if (tongdiemdachia[0].sodiem >= 0)
                    {
                        Double TongDaChia = Convert.ToDouble(tongdiemdachia[0].sodiem.ToString());
                        Double TongDaChiaConlai = TongDaChia;// +ThanhTien;
                        Double TongCongs = Tiencoin - TongDaChiaConlai;
                        LoiNhuanDangKyThanhVien abln = new LoiNhuanDangKyThanhVien();
                        abln.IDThanhVienDangKy = int.Parse(hdid.Value);
                        abln.IDThanhVienGioiThieu = int.Parse(ThanhVienGioiThieu);
                        abln.MoTa = "Lợi nhuận đăng ký thành viên";
                        abln.NgayTao = DateTime.Now;
                        abln.SoDiemNapVao = Tiencoin.ToString();
                        abln.SoDiemConLai = TongCongs.ToString();
                        abln.SoDiemDaChia = TongDaChiaConlai.ToString();
                        abln.MTreeIDThanhVienDangKy = Commond.ShowMTree(hdid.Value.ToString());
                        abln.MTReIDThanhVienGioiThieu = Commond.ShowMTree(ThanhVienGioiThieu.ToString());
                        abln.IDMaDonTao = Convert.ToInt64(IDMaDonTao);
                        abln.IDChiNhanh = Convert.ToInt32(hdChiNhanh.Value);
                        abln.IDLeader = Convert.ToInt32(TimLeader(hdid.Value.ToString()));
                        db.LoiNhuanDangKyThanhViens.InsertOnSubmit(abln);
                        db.SubmitChanges();
                    }
                    //else
                    //{
                    //    Double TongDaChia = Convert.ToDouble(0);
                    //    Double TongDaChiaConlai = ThanhTien;
                    //    Double TongCongs = Tiencoin - TongDaChiaConlai;
                    //    LoiNhuanDangKyThanhVien abln = new LoiNhuanDangKyThanhVien();
                    //    abln.IDThanhVienDangKy = int.Parse(hdid.Value);
                    //    abln.IDThanhVienGioiThieu = int.Parse(ThanhVienGioiThieu);
                    //    abln.MoTa = "Lợi nhuận đăng ký thành viên";
                    //    abln.NgayTao = DateTime.Now;
                    //    abln.SoDiemNapVao = Tiencoin.ToString();
                    //    abln.SoDiemConLai = TongCongs.ToString();
                    //    abln.SoDiemDaChia = TongDaChiaConlai.ToString();
                    //    abln.MTreeIDThanhVienDangKy = Commond.ShowMTree(hdid.Value.ToString());
                    //    abln.MTReIDThanhVienGioiThieu = Commond.ShowMTree(ThanhVienGioiThieu.ToString());
                    //    abln.IDMaDonTao = Convert.ToInt64(IDMaDonTao);
                    //    abln.IDChiNhanh = Convert.ToInt32(hdChiNhanh.Value);
                    //    abln.IDLeader = Convert.ToInt32(TimLeader(hdid.Value.ToString()));
                    //    db.LoiNhuanDangKyThanhViens.InsertOnSubmit(abln);
                    //    db.SubmitChanges();
                    //}

                    //}
                    //catch (Exception)
                    //{ }
                    #endregion

                    Response.Write("<script type=\"text/javascript\">alert('Kích hoạt thành công.');window.location.href='/ho-so-thanh-vien.html';</script>");
                    return;
                }
                else
                {
                    ltkickhoat.Text = "<p style='color:red;font-weight: bold;'>" + Alet + " không đủ điểm để kích hoạt.</p>";
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

        protected void btqrcode_Click(object sender, EventArgs e)
        {
            user data = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
            if (data != null)
            {
                string code = "lienhiephoptac.vn/QRCode/" + data.vuserun.Trim();
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                imgBarCode.Height = 350;
                imgBarCode.Width = 350;
                string anh = "";
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        anh = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    // plQRCode.Controls.Add(imgBarCode);
                }
                data.iuser_id = int.Parse(hdid.Value);
                data.TrangThaiThamGiaQRCode = 1;
                data.AnhQRCode = anh.ToString();
                data.QRCodeHHNguoiMua = "60";
                data.QRCodeHHHeThong = "40";
                db.SubmitChanges();
            }
            Response.Redirect("/ho-so-thanh-vien.html");
        }

        protected void btluuhoahongQRCode_Click(object sender, EventArgs e)
        {
            if (Test(txtchietkhauQRcode.Text.Trim()) == true)
            {
                user data = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
                if (data != null)
                {
                    data.iuser_id = int.Parse(hdid.Value);
                    data.QRCodeChietKhauHH = txtchietkhauQRcode.Text.Trim();
                    // data.QRCodeHHNguoiMua = txthoahongnguoimuaQRcode.Text.Trim();
                    // data.QRCodeHHHeThong = txthoahonghethongQRcode.Text.Trim();
                    db.SubmitChanges();
                }

                #region LichSuQRCode
                LichSuQRCode obj = new LichSuQRCode();
                obj.IDThanhVien = int.Parse(hdid.Value);
                obj.ChietKhauHH = txtchietkhauQRcode.Text.Trim();
                obj.HHNGuoiMua = txthoahongnguoimuaQRcode.Text.Trim();
                obj.HHHeThong = txthoahonghethongQRcode.Text.Trim();
                obj.NguoiDuyet = "Thành viên : " + MoreAll.MoreAll.GetCookies("Members").ToString();
                obj.NgayDuyet = DateTime.Now;
                db.LichSuQRCodes.InsertOnSubmit(obj);
                db.SubmitChanges();
                #endregion

                Response.Write("<script type=\"text/javascript\">alert('Cập nhật hoa hồng thành công.');</script>");
                // làm them khi thay đổi giá trị hoa hồng theo ngày , ai thay đổi, admin hay khách hàng..lưu lịch sử
                Response.Redirect("/ho-so-thanh-vien.html");
            }
        }

        protected bool Test(string ChieKhau)
        {
            Double TongChieKhau = Convert.ToDouble(ChieKhau);

            Double Tong1 = Convert.ToDouble("0");
            Double Tong = Convert.ToDouble("100");

            if (TongChieKhau <= Tong1 || TongChieKhau >= Tong)
            {
                Response.Write("<script type=\"text/javascript\">alert('Giá trị nhập % chiết khấu trong khoảng từ 0 đến 99!');window.location.href='/ho-so-thanh-vien.html';</script>"); return false;
            }
            //if (TongNguoiMua <= Tong1 || TongNguoiMua >= Tong)
            //{
            //    Response.Write("<script type=\"text/javascript\">alert('Giá trị nhập % chiết khấu người mua trong khoảng từ 0 đến 99!');window.location.href='" + Request.Url.ToString().Trim() + "';</script>"); return false;
            //}
            //if (TongHeThong <= Tong1 || TongHeThong >= Tong)
            //{
            //    Response.Write("<script type=\"text/javascript\">alert('Giá trị nhập % chiết khấu hệ thống trong khoảng từ 0 đến 99 !');window.location.href='" + Request.Url.ToString().Trim() + "';</script>"); return false;
            //}
            return true;
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
                //Library.WriteErrorLog("  SoPhanTram: " + SoPhanTram + "  IDThanhVien: " + IDThanhVien + " IDUserNguoiDuocHuong: " + IDUserNguoiDuocHuong + " ThuongLevel: " + ThuongLevel);
                ThemHoaHong(IDProducts, IDType, "Hoa hồng quản lý Level " + ThuTu, IDThanhVien, IDUserNguoiDuocHuong, SoPhanTram.ToString(), ThuongLevel.ToString(), IDCart, Noidung);
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

        public string SetLevel(string Level)
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