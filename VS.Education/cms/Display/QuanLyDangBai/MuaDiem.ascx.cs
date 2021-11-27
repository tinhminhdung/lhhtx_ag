using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class MuaDiem : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btmuadiem_Click(object sender, EventArgs e)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            if (this.txtsotiencanmua.Text.Trim().Length < 1)
            {
                this.ltmsg.Text = "<img src='/Resources/images/iconthongbao.jpg' /> Vui lòng điền thông tin số điểm cần mua";
            }
            else if (this.txtmatkhau.Text.Trim().Length < 1)
            {
                this.ltmsg.Text = "<img src='/Resources/images/iconthongbao.jpg' /> Vui lòng điền xác nhận mật khẩu";
            }
            else
            {
                // Kiểm tra mật khẩu
                var item = db.S_Member_GetPAss(MoreAll.MoreAll.GetCookies("Members").ToString(), txtmatkhau.Text).ToList();
                if (item[0].Tong.ToString() == "0")
                {
                    this.ltmsg.Text = "<img src='/Resources/images/iconthongbao.jpg' /> Mật khẩu hiện tại không đúng";
                    return;
                }
                else
                {
                    this.ltmsg.Text = "";
                    #region Thêm vào Bảng ChuyenDiemThanhVien
                    MuaDiemThanhVien obks = new MuaDiemThanhVien();
                    obks.IDThanhVien = int.Parse(MoreAll.MoreAll.GetCookies("MembersID"));
                    obks.SoDiemCanMua = int.Parse(txtsotiencanmua.Text);
                    obks.NgayGui = DateTime.Now;
                    obks.NgayDuyet = "";
                    obks.NguoiDuyet = "";
                    obks.GhiChu = txtghichu.Text;
                    obks.TrangThai = 0;
                    db.MuaDiemThanhViens.InsertOnSubmit(obks);
                    db.SubmitChanges();

                    this.txtsotiencanmua.Text = "";
                    this.ltmsg.Text = "";
                    this.txtghichu.Text = "";

                    #endregion
                    Response.Write("<script type=\"text/javascript\">alert('Bạn đã gửi yêu cầu thành công.');</script>");
                }
            }

        }
    }
}