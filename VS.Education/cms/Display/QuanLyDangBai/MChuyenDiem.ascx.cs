using Framework;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class MChuyenDiem : System.Web.UI.UserControl
    {
        private static Random random = new Random();
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowInfo();
            if(!IsPostBack)
            {
                SetCapCha();
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
                    if (table.TatChucNang == 1)
                    {
                        Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này.');window.location.href='/vi-tien.html'; </script>");
                    }
                    hdid.Value = table.iuser_id.ToString();
                    this.ltsodiemhientai.Text = table.TongTienCoinDuocCap.ToString();
                   // ltAAFFILIATE.Text = table.VIAAFFILIATE.ToString();

                    this.lthovaten.Text = table.vfname.ToString();
                    this.ltemail.Text = table.vemail.ToString();
                    this.ltdienthoai.Text = table.vphone.ToString();

                    try
                    {
                        List<Entity.users> obj = Susers.Name_Text("SELECT *  FROM users WHERE ((MTree LIKE N'%|" + table.iuser_id + "|%'))  and iuser_id=" + table.iuser_id + " ");
                        if (obj.Count() <= 0)
                        {
                            string Cay = table.MTree + table.iuser_id + "|";
                            Susers.Name_Text("UPDATE [users] SET MTree='" + Cay + "' WHERE iuser_id =" + table.iuser_id + "");
                        }
                    }
                    catch (Exception)
                    { }

                    txtnguoinhan.Text = "";
                    txtmatkhau.Text = "";
                    txtsocoin.Text = "";
                    txtcapcha.Text = "";

                }
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                    string Nhap = txtcapcha.Text.Trim();
                if (Session["RandomCapCha"].ToString() != Nhap)
                {
                    ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\">Mã bảo vệ chưa chính xác</div>";
                }
                else
                {
                    // hiện đang lấy số điểm trong cấu hình . chứ ko lấy theo drop ở trên gioa diện nhé,
                    // sau này mà cần thì sửa tiếp là lấy theo drop vì nó liên quan đến tiền vnd
                    DatalinqDataContext db = new DatalinqDataContext();
                    if (this.txtnguoinhan.Text.Trim().Length < 1)
                    {
                        this.ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Vui lòng điền thông tin người nhận </div>";
                    }
                    else if (this.txtmatkhau.Text.Trim().Length < 1)
                    {
                        this.ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Vui lòng điền mật khẩu </div>";
                    }
                    else if (this.txtsocoin.Text.Trim().Length < 1)
                    {
                        this.ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Vui lòng điền số điểm cần chuyển </div>";
                    }
                    else if (ddlvicanchuyen.SelectedValue == "2" && ddlViNhanDiem.SelectedValue == "1")
                    {
                        this.ltmsg.Text = "";
                        Response.Write("<script type=\"text/javascript\">alert('Lưu ý: ví quản lý không thể chuyển vào ví thương mại.');</script>");
                    }
                    else if (ddlvicanchuyen.SelectedValue == "2" && ddlViNhanDiem.SelectedValue == "3")
                    {
                        this.ltmsg.Text = "";
                        Response.Write("<script type=\"text/javascript\">alert('Lưu ý: ví quản lý không thể chuyển vào ví mua hàng.');</script>");
                    }
                    else if (ddlvicanchuyen.SelectedValue == "1" && ddlViNhanDiem.SelectedValue == "3")
                    {
                        this.ltmsg.Text = "";
                        Response.Write("<script type=\"text/javascript\">alert('Lưu ý: ví thương mại không thể chuyển vào ví mua hàng.');</script>");
                    }
                    else if (ddlvicanchuyen.SelectedValue == "3" && ddlViNhanDiem.SelectedValue == "1")
                    {
                        this.ltmsg.Text = "";
                        Response.Write("<script type=\"text/javascript\">alert('Lưu ý: ví mua hàng không thể chuyển vào ví thương mại.');</script>");
                    }
                    //else if (ddlvicanchuyen.SelectedValue == "3" && ddlViNhanDiem.SelectedValue == "2")
                    //{
                    //    this.ltmsg.Text = "";
                    //    Response.Write("<script type=\"text/javascript\">alert('Lưu ý: ví mua hàng không thể chuyển vào ví quản lý.');</script>");
                    //}
                    else
                    {
                        #region Kiểm tra người nhận có thực ko
                        if (txtnguoinhan.Text.Length > 0)
                        {
                            var iEmail = db.S_Member_ChuyenDiem(txtnguoinhan.Text.Trim(), int.Parse(hdid.Value)).ToList();
                            if (iEmail.Count() == 0)
                            {
                                ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Không tìm thấy người nhận. </div>";
                            }
                            else
                            {
                                ThemMTreeNeuThieu(iEmail[0].MTree.ToString(), iEmail[0].iuser_id.ToString());
                                CapNhatIDNguoiGioiThieu(iEmail[0].iuser_id.ToString());
                                #region Kiểm tra mật khẩu
                                var item = db.S_Member_GetPAss(MoreAll.MoreAll.GetCookies("Members").ToString(), txtmatkhau.Text).ToList();
                                if (item[0].Tong.ToString() == "0")
                                {
                                    this.ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Mật khẩu hiện tại không đúng </div>";
                                }
                                else
                                {
                                    this.ltmsg.Text = "";
                                    // Ví AFF bị giới hạn chuyển 
                                    //1. Ko cho chuyển lên cập trên.
                                    //2. Chỉ cho chuyển trong chi nhánh của mình

                                    if (ddlvicanchuyen.SelectedValue == "3" && ddlViNhanDiem.SelectedValue == "3")
                                    {
                                        var itemv = db.S_Members_ChuyenDiem_Tree(int.Parse(hdid.Value), int.Parse(iEmail[0].iuser_id.ToString())).ToList();
                                        if (itemv[0].Tong <= 0)
                                        {
                                            ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Không tìm thấy người nhận nằm trong chi nhánh của bạn. </div>"; return;
                                        }
                                        else
                                        {
                                            double Tiencoin = Convert.ToDouble(txtsocoin.Text);
                                            #region Trừ điểm vào bảng thành viên TongTienCoinDuocCap
                                            user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
                                            if (iitem != null)
                                            {
                                                double ViHienTai = 0;
                                                ViHienTai = Convert.ToDouble(iitem.ViMuaHangAFF);
                                                double TongTienNapVao = Convert.ToDouble(txtsocoin.Text);
                                                if (ViHienTai >= TongTienNapVao)
                                                {
                                                    string TextCanChuyen = "";
                                                    string TextNhanDiem = "";
                                                    #region Lưu số tiền đã bị trừ khi chia cho thành viên khác xong
                                                    double Conglai = 0;
                                                    Conglai = ((ViHienTai) - (TongTienNapVao));
                                                    TextCanChuyen = "Ví mua hàng";
                                                    Susers.Name_Text("update users set ViMuaHangAFF=" + Conglai.ToString() + " where iuser_id=" + hdid.Value + "");
                                                    #endregion
                                                    TextNhanDiem = "Ví mua hàng";
                                                    LichSuGiaoDich("14", "Chuyển điểm cho thành viên trong chi nhánh", hdid.Value, iEmail[0].iuser_id.ToString(), "0", Tiencoin.ToString());
                                                    #region Bắt đầu tính các trường hợp khi thỏa mãn số tiền trong ví và số tiền sẽ nạp cho thành viên khác
                                                   
                                                    #region Thêm vào Bảng ChuyenDiemThanhVien
                                                    ChuyenDiemThanhVien obks = new ChuyenDiemThanhVien();
                                                    obks.IDNguoiCap = int.Parse(hdid.Value);
                                                    obks.IDNguoiNhan = int.Parse(iEmail[0].iuser_id.ToString());
                                                    obks.SoCoin = Tiencoin.ToString();
                                                    obks.NgayCap = DateTime.Now;
                                                    obks.MoTa = "Cấp điểm từ " + TextCanChuyen + " sang " + TextNhanDiem;
                                                    obks.ViChuyen = int.Parse(ddlvicanchuyen.SelectedValue);
                                                    obks.ViNhan = int.Parse(ddlViNhanDiem.SelectedValue);
                                                    obks.TrangThai = int.Parse("1");// trang thái 1 là chuyển trong hệ thống, 2 chuyển cho chính mình
                                                    db.ChuyenDiemThanhViens.InsertOnSubmit(obks);
                                                    db.SubmitChanges();
                                                    #endregion

                                                    #region Thêm vào Bảng CapDiemThanhVien
                                                    CapDiemThanhVien obkp = new CapDiemThanhVien();
                                                    obkp.IDNguoiCap = int.Parse(hdid.Value);
                                                    obkp.IDNguoiNhanDiemCoin = int.Parse(iEmail[0].iuser_id.ToString());
                                                    obkp.SoDiemCoin = Tiencoin.ToString();
                                                    obkp.NgayCap = DateTime.Now;
                                                    obkp.MoTa = "Cấp điểm từ " + TextCanChuyen + " sang " + TextNhanDiem;
                                                    obkp.NguoiTao = lthovaten.Text;
                                                    obkp.TrangThai = 1;
                                                    obkp.KieuVi = int.Parse(ddlvicanchuyen.SelectedValue);
                                                    db.CapDiemThanhViens.InsertOnSubmit(obkp);
                                                    db.SubmitChanges();
                                                    #endregion

                                                    #region Cộng điểm coin vào bảng thành viên khi chuyển điểm
                                                    user congdiem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(iEmail[0].iuser_id.ToString()));
                                                    if (congdiem != null)
                                                    {
                                                        double TongSoCoinDaCos = Convert.ToDouble(congdiem.ViMuaHangAFF);
                                                        double TongTienNapVaos = Convert.ToDouble(Tiencoin.ToString());
                                                        double Conglais = 0;
                                                        Conglais = ((TongSoCoinDaCos) + (TongTienNapVaos));
                                                        Susers.Name_Text("update users set ViMuaHangAFF=" + Conglais.ToString() + " where iuser_id=" + iEmail[0].iuser_id.ToString() + "");

                                                        LichSuGiaoDich("15", "Cấp điểm cho thành viên trong chi nhánh", hdid.Value, iEmail[0].iuser_id.ToString(), "0", Tiencoin.ToString());
                                                        txtnguoinhan.Text = "";
                                                        txtmatkhau.Text = "";
                                                    }
                                                    #endregion
                                                    ShowInfo();
                                                    Response.Write("<script type=\"text/javascript\">alert('Bạn đã chuyển điểm thành công');window.location.href='/chuyen-diem.html'; </script>");

                                                    return;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Hiện điểm của bạn không đủ để thực hiện chuyển điểm </div>";
                                                }
                                            }
                                            #endregion
                                        }

                                    }

                                   //ví mua hàng chuyển vào ví quản lý
                                    else if (ddlvicanchuyen.SelectedValue == "3" && ddlViNhanDiem.SelectedValue == "2")
                                    {
                                        var itemv = db.S_Members_ChuyenDiem_Tree(int.Parse(hdid.Value), int.Parse(iEmail[0].iuser_id.ToString())).ToList();
                                        if (itemv[0].Tong <= 0)
                                        {
                                            ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Không tìm thấy người nhận nằm trong chi nhánh của bạn. </div>"; return;
                                        }
                                        else
                                        {
                                            double Tiencoin = Convert.ToDouble(txtsocoin.Text);
                                            #region Trừ điểm vào bảng thành viên TongTienCoinDuocCap
                                            user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
                                            if (iitem != null)
                                            {
                                                double ViHienTai = 0;
                                                ViHienTai = Convert.ToDouble(iitem.ViMuaHangAFF);
                                                double TongTienNapVao = Convert.ToDouble(txtsocoin.Text);
                                                if (ViHienTai >= TongTienNapVao)
                                                {
                                                    string TextCanChuyen = "";
                                                    string TextNhanDiem = "";

                                                    #region Lưu số tiền đã bị trừ khi chia cho thành viên khác xong
                                                    double Conglai = 0;
                                                    Conglai = ((ViHienTai) - (TongTienNapVao));

                                                    TextCanChuyen = "Ví mua hàng";
                                                    Susers.Name_Text("update users set ViMuaHangAFF=" + Conglai.ToString() + " where iuser_id=" + hdid.Value + "");
                                                    #endregion

                                                    TextNhanDiem = "Ví Quản lý";
                                                    LichSuGiaoDich("14", "Chuyển điểm cho thành viên trong chi nhánh", hdid.Value, iEmail[0].iuser_id.ToString(), "0", Tiencoin.ToString());
                                                    
                                                    
                                                    #region Bắt đầu tính các trường hợp khi thỏa mãn số tiền trong ví và số tiền sẽ nạp cho thành viên khác
                                                    #region Thêm vào Bảng ChuyenDiemThanhVien
                                                    ChuyenDiemThanhVien obks = new ChuyenDiemThanhVien();
                                                    obks.IDNguoiCap = int.Parse(hdid.Value);
                                                    obks.IDNguoiNhan = int.Parse(iEmail[0].iuser_id.ToString());
                                                    obks.SoCoin = Tiencoin.ToString();
                                                    obks.NgayCap = DateTime.Now;
                                                    obks.MoTa = "Cấp điểm từ " + TextCanChuyen + " sang " + TextNhanDiem;
                                                    obks.ViChuyen = int.Parse(ddlvicanchuyen.SelectedValue);
                                                    obks.ViNhan = int.Parse(ddlViNhanDiem.SelectedValue);
                                                    obks.TrangThai = int.Parse("1");// trang thái 1 là chuyển trong hệ thống, 2 chuyển cho chính mình
                                                    db.ChuyenDiemThanhViens.InsertOnSubmit(obks);
                                                    db.SubmitChanges();
                                                    #endregion

                                                    #region Thêm vào Bảng CapDiemThanhVien
                                                    CapDiemThanhVien obkp = new CapDiemThanhVien();
                                                    obkp.IDNguoiCap = int.Parse(hdid.Value);
                                                    obkp.IDNguoiNhanDiemCoin = int.Parse(iEmail[0].iuser_id.ToString());
                                                    obkp.SoDiemCoin = Tiencoin.ToString();
                                                    obkp.NgayCap = DateTime.Now;
                                                    obkp.MoTa = "Cấp điểm từ " + TextCanChuyen + " sang " + TextNhanDiem;
                                                    obkp.NguoiTao = lthovaten.Text;
                                                    obkp.TrangThai = 1;
                                                    obkp.KieuVi = int.Parse(ddlvicanchuyen.SelectedValue);
                                                    db.CapDiemThanhViens.InsertOnSubmit(obkp);
                                                    db.SubmitChanges();
                                                    #endregion

                                                    #region Cộng điểm coin vào bảng thành viên khi chuyển điểm
                                                    user congdiem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(iEmail[0].iuser_id.ToString()));
                                                    if (congdiem != null)
                                                    {
                                                        double TongSoCoinDaCos = Convert.ToDouble(congdiem.VIAAFFILIATE);
                                                        double TongTienNapVaos = Convert.ToDouble(Tiencoin.ToString());
                                                        double Conglais = 0;
                                                        Conglais = ((TongSoCoinDaCos) + (TongTienNapVaos));
                                                        Susers.Name_Text("update users set VIAAFFILIATE=" + Conglais.ToString() + " where iuser_id=" + iEmail[0].iuser_id.ToString() + "");

                                                        LichSuGiaoDich("15", "Cấp điểm cho thành viên trong chi nhánh", hdid.Value, iEmail[0].iuser_id.ToString(), "0", Tiencoin.ToString());
                                                        txtnguoinhan.Text = "";
                                                        txtmatkhau.Text = "";
                                                    }
                                                    #endregion
                                                    ShowInfo();
                                                    Response.Write("<script type=\"text/javascript\">alert('Bạn đã chuyển điểm thành công');window.location.href='/chuyen-diem.html'; </script>");
                                                    return;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Hiện điểm của bạn không đủ để thực hiện chuyển điểm </div>";
                                                }
                                            }
                                            #endregion
                                        }

                                    }
                                    else if (ddlvicanchuyen.SelectedValue == "2" && ddlViNhanDiem.SelectedValue != "3")// ví AFF //Ví AFF bị giới hạn chuyển 
                                    {
                                        // Chỉ có thành viên trong hệ thống chi nhánh của mình mới được hưởng
                                        // dưới chuyern lên tr
                                        //var itemv = db.S_Members_ChuyenDiem_Tree(int.Parse(iEmail[0].iuser_id.ToString()), int.Parse(hdid.Value)).ToList();
                                        ///var itemv1 = db.S_Members_ChuyenDiem_Tree(int.Parse(hdid.Value), int.Parse(iEmail[0].iuser_id.ToString())).ToList();
                                        var itemv = db.S_Members_ChuyenDiem_Tree(int.Parse(hdid.Value), int.Parse(iEmail[0].iuser_id.ToString())).ToList();
                                        if (itemv[0].Tong <= 0)
                                        {
                                            ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Không tìm thấy người nhận nằm trong chi nhánh của bạn. </div>"; return;
                                        }
                                        else
                                        {

                                            double Tiencoin = Convert.ToDouble(txtsocoin.Text);
                                            #region Trừ điểm vào bảng thành viên TongTienCoinDuocCap
                                            user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
                                            if (iitem != null)
                                            {
                                                double ViHienTai = 0;
                                                if (ddlvicanchuyen.SelectedValue == "1")// ví tổng thương mại, 2 ví Aff
                                                {
                                                    ViHienTai = Convert.ToDouble(iitem.TongTienCoinDuocCap);
                                                }
                                                else
                                                {
                                                    ViHienTai = Convert.ToDouble(iitem.VIAAFFILIATE);
                                                }

                                                double TongTienNapVao = Convert.ToDouble(txtsocoin.Text);
                                                if (ViHienTai >= TongTienNapVao)
                                                {
                                                    //#region Lưu số tiền đã bị trừ khi chia cho thành viên khác xong
                                                    //double Conglai = 0;
                                                    //Conglai = ((ViHienTai) - (TongTienNapVao));
                                                    //iitem.TongTienCoinDuocCap = Conglai.ToString();
                                                    //db.SubmitChanges();
                                                    //#endregion

                                                    string TextCanChuyen = "";
                                                    string TextNhanDiem = "";
                                                    #region Lưu số tiền đã bị trừ khi chia cho thành viên khác xong
                                                    double Conglai = 0;
                                                    Conglai = ((ViHienTai) - (TongTienNapVao));
                                                    if (ddlvicanchuyen.SelectedValue == "1")// 1ví tổng thương mại, 2 ví Aff
                                                    {
                                                        TextCanChuyen = "Ví thương mại";
                                                        Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + " where iuser_id=" + hdid.Value + "");
                                                    }
                                                    else
                                                    {
                                                        TextCanChuyen = "Ví quản lý";
                                                        Susers.Name_Text("update users set VIAAFFILIATE=" + Conglai.ToString() + " where iuser_id=" + hdid.Value + "");
                                                    }
                                                    #endregion

                                                    if (ddlViNhanDiem.SelectedValue == "1")// ví tổng thương mại, 2 ví Aff
                                                    {
                                                        TextNhanDiem = "Ví thương mại";
                                                    }
                                                    else
                                                    {
                                                        TextNhanDiem = "Ví quản lý";
                                                    }

                                                    LichSuGiaoDich("14", "Chuyển điểm cho thành viên trong chi nhánh", hdid.Value, iEmail[0].iuser_id.ToString(), "0", Tiencoin.ToString());

                                                    #region Bắt đầu tính các trường hợp khi thỏa mãn số tiền trong ví và số tiền sẽ nạp cho thành viên khác
                                                    #region Thêm vào Bảng ChuyenDiemThanhVien
                                                    ChuyenDiemThanhVien obks = new ChuyenDiemThanhVien();
                                                    obks.IDNguoiCap = int.Parse(hdid.Value);
                                                    obks.IDNguoiNhan = int.Parse(iEmail[0].iuser_id.ToString());
                                                    obks.SoCoin = Tiencoin.ToString();
                                                    obks.NgayCap = DateTime.Now;
                                                    obks.MoTa = "Cấp điểm từ " + TextCanChuyen + " sang " + TextNhanDiem;
                                                    obks.ViChuyen = int.Parse(ddlvicanchuyen.SelectedValue);
                                                    obks.ViNhan = int.Parse(ddlViNhanDiem.SelectedValue);
                                                    obks.TrangThai = int.Parse("1");// trang thái 1 là chuyển trong hệ thống, 2 chuyển cho chính mình
                                                    db.ChuyenDiemThanhViens.InsertOnSubmit(obks);
                                                    db.SubmitChanges();
                                                    #endregion

                                                    #region Thêm vào Bảng CapDiemThanhVien
                                                    CapDiemThanhVien obkp = new CapDiemThanhVien();
                                                    obkp.IDNguoiCap = int.Parse(hdid.Value);
                                                    obkp.IDNguoiNhanDiemCoin = int.Parse(iEmail[0].iuser_id.ToString());
                                                    obkp.SoDiemCoin = Tiencoin.ToString();
                                                    obkp.NgayCap = DateTime.Now;
                                                    obkp.MoTa = "Cấp điểm từ " + TextCanChuyen + " sang " + TextNhanDiem;
                                                    obkp.NguoiTao = lthovaten.Text;
                                                    obkp.TrangThai = 1;
                                                    obkp.KieuVi = int.Parse(ddlvicanchuyen.SelectedValue);
                                                    db.CapDiemThanhViens.InsertOnSubmit(obkp);
                                                    db.SubmitChanges();
                                                    #endregion

                                                    #region Cộng điểm coin vào bảng thành viên khi chuyển điểm
                                                    user congdiem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(iEmail[0].iuser_id.ToString()));
                                                    if (congdiem != null)
                                                    {
                                                        if (ddlViNhanDiem.SelectedValue == "1")// ví tổng thương mại, 2 ví Aff
                                                        {
                                                            double TongSoCoinDaCos = Convert.ToDouble(congdiem.TongTienCoinDuocCap);
                                                            double TongTienNapVaos = Convert.ToDouble(Tiencoin.ToString());
                                                            double Conglais = 0;
                                                            Conglais = ((TongSoCoinDaCos) + (TongTienNapVaos));
                                                            Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglais.ToString() + " where iuser_id=" + iEmail[0].iuser_id.ToString() + "");
                                                        }
                                                        else
                                                        {
                                                            double TongSoCoinDaCos = Convert.ToDouble(congdiem.VIAAFFILIATE);
                                                            double TongTienNapVaos = Convert.ToDouble(Tiencoin.ToString());
                                                            double Conglais = 0;
                                                            Conglais = ((TongSoCoinDaCos) + (TongTienNapVaos));
                                                            Susers.Name_Text("update users set VIAAFFILIATE=" + Conglais.ToString() + " where iuser_id=" + iEmail[0].iuser_id.ToString() + "");
                                                        }
                                                        LichSuGiaoDich("15", "Cấp điểm cho thành viên trong chi nhánh", hdid.Value, iEmail[0].iuser_id.ToString(), "0", Tiencoin.ToString());
                                                        txtnguoinhan.Text = "";
                                                        txtmatkhau.Text = "";
                                                    }
                                                    #endregion
                                                    ShowInfo();
                                                    Response.Write("<script type=\"text/javascript\">alert('Bạn đã chuyển điểm thành công');window.location.href='/chuyen-diem.html'; </script>");
                                                    return;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Hiện điểm của bạn không đủ để thực hiện chuyển điểm </div>";
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    else if (ddlvicanchuyen.SelectedValue == "1" && ddlViNhanDiem.SelectedValue != "3") // Ví Thương mại vẫn chuyển cho nhau bình thường. ko bị giới hạn
                                    {
                                        double Tiencoin = Convert.ToDouble(txtsocoin.Text);
                                        #region Trừ điểm vào bảng thành viên TongTienCoinDuocCap
                                        user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdid.Value));
                                        if (iitem != null)
                                        {
                                            double ViHienTai = 0;
                                            if (ddlvicanchuyen.SelectedValue == "1")// ví tổng thương mại, 2 ví Aff
                                            {
                                                ViHienTai = Convert.ToDouble(iitem.TongTienCoinDuocCap);
                                            }
                                            else
                                            {
                                                ViHienTai = Convert.ToDouble(iitem.VIAAFFILIATE);
                                            }

                                            double TongTienNapVao = Convert.ToDouble(txtsocoin.Text);
                                            if (ViHienTai >= TongTienNapVao)
                                            {
                                                //#region Lưu số tiền đã bị trừ khi chia cho thành viên khác xong
                                                //double Conglai = 0;
                                                //Conglai = ((ViHienTai) - (TongTienNapVao));
                                                //iitem.TongTienCoinDuocCap = Conglai.ToString();
                                                //db.SubmitChanges();
                                                //#endregion

                                                string TextCanChuyen = "";
                                                string TextNhanDiem = "";
                                                #region Lưu số tiền đã bị trừ khi chia cho thành viên khác xong
                                                double Conglai = 0;
                                                Conglai = ((ViHienTai) - (TongTienNapVao));
                                                if (ddlvicanchuyen.SelectedValue == "1")// 1ví tổng thương mại, 2 ví Aff
                                                {
                                                    TextCanChuyen = "Ví thương mại";
                                                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + " where iuser_id=" + hdid.Value + "");
                                                }
                                                else
                                                {
                                                    TextCanChuyen = "Ví quản lý";
                                                    Susers.Name_Text("update users set VIAAFFILIATE=" + Conglai.ToString() + " where iuser_id=" + hdid.Value + "");
                                                }
                                                #endregion

                                                if (ddlViNhanDiem.SelectedValue == "1")// ví tổng thương mại, 2 ví Aff
                                                {
                                                    TextNhanDiem = "Ví thương mại";
                                                }
                                                else
                                                {
                                                    TextNhanDiem = "Ví quản lý";
                                                }

                                                LichSuGiaoDich("14", "Chuyển điểm cho thành viên trong chi nhánh", hdid.Value, iEmail[0].iuser_id.ToString(), "0", Tiencoin.ToString());

                                                #region Bắt đầu tính các trường hợp khi thỏa mãn số tiền trong ví và số tiền sẽ nạp cho thành viên khác
                                                #region Thêm vào Bảng ChuyenDiemThanhVien
                                                ChuyenDiemThanhVien obks = new ChuyenDiemThanhVien();
                                                obks.IDNguoiCap = int.Parse(hdid.Value);
                                                obks.IDNguoiNhan = int.Parse(iEmail[0].iuser_id.ToString());
                                                obks.SoCoin = Tiencoin.ToString();
                                                obks.NgayCap = DateTime.Now;
                                                obks.MoTa = "Cấp điểm từ " + TextCanChuyen + " sang " + TextNhanDiem;
                                                obks.ViChuyen = int.Parse(ddlvicanchuyen.SelectedValue);
                                                obks.ViNhan = int.Parse(ddlViNhanDiem.SelectedValue);
                                                obks.TrangThai = int.Parse("1");// trang thái 1 là chuyển trong hệ thống, 2 chuyển cho chính mình
                                                db.ChuyenDiemThanhViens.InsertOnSubmit(obks);
                                                db.SubmitChanges();
                                                #endregion

                                                #region Thêm vào Bảng CapDiemThanhVien
                                                CapDiemThanhVien obkp = new CapDiemThanhVien();
                                                obkp.IDNguoiCap = int.Parse(hdid.Value);
                                                obkp.IDNguoiNhanDiemCoin = int.Parse(iEmail[0].iuser_id.ToString());
                                                obkp.SoDiemCoin = Tiencoin.ToString();
                                                obkp.NgayCap = DateTime.Now;
                                                obkp.MoTa = "Cấp điểm từ " + TextCanChuyen + " sang " + TextNhanDiem;
                                                obkp.NguoiTao = lthovaten.Text;
                                                obkp.TrangThai = 1;
                                                obkp.KieuVi = int.Parse(ddlvicanchuyen.SelectedValue);
                                                db.CapDiemThanhViens.InsertOnSubmit(obkp);
                                                db.SubmitChanges();
                                                #endregion

                                                #region Cộng điểm coin vào bảng thành viên khi chuyển điểm
                                                user congdiem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(iEmail[0].iuser_id.ToString()));
                                                if (congdiem != null)
                                                {
                                                    if (ddlViNhanDiem.SelectedValue == "1")// ví tổng thương mại, 2 ví Aff
                                                    {
                                                        double TongSoCoinDaCos = Convert.ToDouble(congdiem.TongTienCoinDuocCap);
                                                        double TongTienNapVaos = Convert.ToDouble(Tiencoin.ToString());
                                                        double Conglais = 0;
                                                        Conglais = ((TongSoCoinDaCos) + (TongTienNapVaos));
                                                        Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglais.ToString() + " where iuser_id=" + iEmail[0].iuser_id.ToString() + "");
                                                    }
                                                    else
                                                    {
                                                        double TongSoCoinDaCos = Convert.ToDouble(congdiem.VIAAFFILIATE);
                                                        double TongTienNapVaos = Convert.ToDouble(Tiencoin.ToString());
                                                        double Conglais = 0;
                                                        Conglais = ((TongSoCoinDaCos) + (TongTienNapVaos));
                                                        Susers.Name_Text("update users set VIAAFFILIATE=" + Conglais.ToString() + " where iuser_id=" + iEmail[0].iuser_id.ToString() + "");
                                                    }
                                                    LichSuGiaoDich("15", "Cấp điểm cho thành viên trong chi nhánh", hdid.Value, iEmail[0].iuser_id.ToString(), "0", Tiencoin.ToString());
                                                    txtnguoinhan.Text = "";
                                                    txtmatkhau.Text = "";
                                                }
                                                #endregion
                                                ShowInfo();
                                                Response.Write("<script type=\"text/javascript\">alert('Bạn đã chuyển điểm thành công');window.location.href='/chuyen-diem.html'; </script>");
                                                return;
                                                #endregion
                                            }
                                            else
                                            {
                                                ltmsg.Text = "<div class=\"alert alert-danger\" role=\"alert\"> Hiện điểm của bạn không đủ để thực hiện chuyển điểm </div>";
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception) { }
            SetCapCha(); 
        }
        public void ThemMTreeNeuThieu(string MTree, string iuser_id)
        {
            try
            {
                List<Entity.users> obj = Susers.Name_Text("SELECT *  FROM users WHERE ((MTree LIKE N'%|" + iuser_id + "|%'))  and iuser_id=" + iuser_id + " ");
                if (obj.Count() <= 0)
                {
                    string Cay = MTree + iuser_id + "|";
                    Susers.Name_Text("UPDATE [users] SET MTree='" + Cay + "' WHERE iuser_id =" + iuser_id + "");
                }
            }
            catch (Exception)
            { }
        }
        public void CapNhatIDNguoiGioiThieu(string iuser_id)
        {
            List<Entity.users> obg = Susers.Name_Text("SELECT *  FROM users WHERE   iuser_id=" + iuser_id + " ");
            if (obg.Count() > 0)
            {
                List<Entity.users> obj = Susers.Name_Text("SELECT *  FROM users WHERE ((MTree LIKE N'%|" + obg[0].GioiThieu.ToString() + "|%'))  and iuser_id=" + iuser_id + " ");
                if (obj.Count() <= 0)
                {
                    List<Entity.users> obk = Susers.Name_Text("SELECT *  FROM users WHERE iuser_id=" + iuser_id + " ");
                    if (obk.Count() > 0)
                    {
                        string Cay = obk[0].MTree + obg[0].GioiThieu.ToString() + "|";
                        Susers.Name_Text("UPDATE [users] SET MTree='" + Cay + "' WHERE iuser_id =" + iuser_id + "");
                    }
                }
            }
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
            List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + "");
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
        protected string ShowThanhVien(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count >= 1)
            {
                str += "<span id=" + dt[0].iuser_id.ToString() + " style=\" color:red\">";
                if (dt[0].vfname.ToString().Length > 0)
                {
                    str += dt[0].vfname;
                }
                str += "</span>";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += " - " + dt[0].vphone;
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
    }
}