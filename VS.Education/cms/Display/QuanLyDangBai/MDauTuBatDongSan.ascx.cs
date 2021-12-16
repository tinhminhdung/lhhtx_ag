using MoreAll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class MDauTuBatDongSan : System.Web.UI.UserControl
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
                    hdid.Value = table.iuser_id.ToString();
                }
            }
        }
        protected void btrutien_Click(object sender, EventArgs e)
        {
            if (this.flAnh.HasFile)
            {
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                string extension = Path.GetExtension(Path.GetFileName(this.flAnh.PostedFile.FileName));
                if (this.hdNganhang.Value.Length > 0)
                {
                    try
                    {
                        File.Delete(utlitities.APPL_PHYSICAL_PATH + "/Uploads/ChuyenTien/" + this.hdNganhang.Value);
                    }
                    catch (Exception)
                    {
                    }
                }
                string str = DateTime.Now.Ticks.ToString() + extension;
                this.hdNganhang.Value = str;
                try
                {
                    this.flAnh.PostedFile.SaveAs(utlitities.APPL_PHYSICAL_PATH + "/Uploads/ChuyenTien/" + str);
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


            DauTuBatDongSan obj = new DauTuBatDongSan();
            obj.IDThanhVien = int.Parse(hdid.Value);
            obj.TongTienDauTu = txtsotiencanrut.Text.Replace(",", "").Replace(".", "");
            obj.HoVaTen = txthovaten.Text;
            obj.DiaChi = DiaChi.Text;
            obj.DienThoai = DienThoai.Text;
            obj.CMND = CMND.Text;
            obj.TenNganHang = txttennganhang.Text;
            obj.SoTaiKHoan = txtsotaikhoan.Text;
            obj.ChiNhanh = txtchinhanh.Text;
            obj.Anh = hdNganhang.Value;
            obj.GhiChu = txtghichu.Text;
            obj.TrangThai = 0;
            obj.NgayTao = DateTime.Now;
            obj.NgayDuyet = "";
            obj.NguoiDuyet = "";

            obj.CMNDTruoc = hdchungminhthumattruoc.Value;
            obj.CMNDSau = hdchungminhthumatsau.Value;

            db.DauTuBatDongSans.InsertOnSubmit(obj);
            db.SubmitChanges();

            ltmsg.Text = "<div class=\"thongbaos\">Bạn đã gửi thành công. Vui lòng đợi chúng tôi xử lý.</div>";

            txtsotiencanrut.Text = "";
            txttennganhang.Text = "";
            txthovaten.Text = "";
            txtsotaikhoan.Text = "";
            txtchinhanh.Text = "";
            DiaChi.Text = "";
            DienThoai.Text = "";
            txtghichu.Text = "";
            ShowInfo();
        }

        protected void bthuy_Click(object sender, EventArgs e)
        {
            txtsotiencanrut.Text = "";
            txttennganhang.Text = "";
            txthovaten.Text = "";
            txtsotaikhoan.Text = "";
            txtchinhanh.Text = "";
            DiaChi.Text = "";
            DienThoai.Text = "";
            txtghichu.Text = "";
            ltmsg.Text = "";
        }
    }
}