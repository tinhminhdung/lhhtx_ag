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
    public partial class ThongBaoXuLyDonHang : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
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
            if (!base.IsPostBack)
            {
                if (MoreAll.MoreAll.GetCookies("MembersID") != "")
                {
                    if (Commond.Setting("StatusNCC").Equals("1"))
                    {
                        ltthongbaotoanbonhacc.Text += ThongBaoToanBoNhaCC();
                    }
                    ltthongbao.Text += ThongBaoDuyetViTam();
                    // cho js xuất hiện popup và cho dạng sesion xuất hiện 1 lần
                    // so sánh trong cấu hình còn lại 5 ngày thì mới thông báo.....
                }
            }
        }
        protected string ShowTongThongBao()
        {
            string Tong1 = "";
            List<Entity.users> table = Susers.Name_Text("select * from users where vuserun=N'" + MoreAll.MoreAll.GetCookies("Members") + "' and istatus=1 ");
            if (table.Count > 0)
            {
                DatalinqDataContext db = new DatalinqDataContext();
                List<Notification> dt1 = db.Notifications.Where(s => s.IDThanhVienNhanThongBao == int.Parse(table[0].iuser_id.ToString()) && s.TrangThai == 0).ToList();
                if (dt1.Count > 0)
                {
                    Tong1 += " <div class=\"hotline-phone-ring-wrap\">";
                    Tong1 += "<div class=\"hotline-phone-ring\">";
                    Tong1 += "<div class=\"hotline-phone-ring-circle\"></div>";
                    Tong1 += "<div class=\"hotline-phone-ring-circle-fill\"></div>";
                    Tong1 += "<div class=\"hotline-phone-ring-img-circle\">";
                    Tong1 += "<a href=\"/tin-nhan.html\" class=\"pps-btn-img\">";
                    Tong1 += "<i class=\"fa fa-bell\" style=\" color:red\"></i>";
                    Tong1 += "</a>";
                    Tong1 += "</div>";
                    Tong1 += "</div>";
                    Tong1 += "</div>";
                }
            }
            return Tong1;
        }
        protected string ThongBaoDuyetViTam()
        {
            string str = "";
            // Tìm ví xem có giao dịch nào đủ điều kiện ko 
            List<ViTamMuaHang> List = db.ViTamMuaHangs.Where(s => s.IDThanhVienMua == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString())).ToList();
            if (List.Count > 0)
            {
                foreach (var item in List)
                {
                    // Chỉ có những đơn hàng người mua hàng chưa bấm nút chấp nhận thì mưới tự động duyệt, còn các trạng thái khiếu nại thì sẽ ko duyệt
                    DateTime NgayDuyetHang = Convert.ToDateTime(item.NgayCapNhat.ToString());
                    DateTime NgayXuLy = NgayDuyetHang.AddDays((double)Convert.ToInt32(MoreAll.Other.Giatri("Thongbaoduyethang")));
                    DateTime NgayHienTai = DateTime.Now;
                    if ((NgayHienTai >= NgayXuLy))
                    {
                        List<Entity.CartDetail> table = SCartDetail.Name_Text("select * from CartDetail  where ID=" + item.IDCartDetail.ToString() + " and TrangThaiKhieuKien=0 and TrangThaiNhaCungCap=1 and TrangThaiNguoiMuaHang=3");
                        if (table.Count > 0)
                        {
                            string bc = Request.Url.Authority + Request.RawUrl.ToString();
                            if (!bc.Contains("localhost"))
                            {
                                if (System.Web.HttpContext.Current.Session["ThongBaoDuyetViTam"] != "ThongBaoDuyetViTam")
                                {
                                    ltscript.Text += " <script> $(document).ready(function () { $('#myModal').modal('show'); }); </script>";
                                    System.Web.HttpContext.Current.Session["ThongBaoDuyetViTam"] = "ThongBaoDuyetViTam";
                                }
                            }
                            foreach (var item1 in table)
                            {
                                //Thongbaoduyethang
                                List<Entity.Carts> Giohang = SCarts.Name_Text("select * from Carts  where ID=" + item1.ID_Cart.ToString() + "");
                                if (Giohang.Count > 0)
                                {
                                    str += "<p><b>" + ShowName(item1.ipid.ToString()) + "</b> - Mã đơn hàng: <b style=\"color: red\"><a target=\"_blank\" href=\"/account/orders/" + Giohang[0].ID.ToString() + "\" style='color:red' >#" + Giohang[0].ID.ToString() + "</a></b> - Mua ngày: " + Giohang[0].Create_Date.ToString() + " - Số điểm thanh toán:" + item.SoTienNguoiMuaBiTru.ToString() + "</p>";
                                }
                            }
                        }
                    }
                }
            }
            return str;
        }

        protected string ThongBaoToanBoNhaCC()
        {
            string str = "";
            user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
            if (table != null)
            {
                if (table.Type.ToString() == "2")
                {
                    DateTime NgayHienThi = Convert.ToDateTime(MoreAll.Other.Giatri("NgayCapNhapHienThiTBNCC"));
                    DateTime NgayXuLy = NgayHienThi.AddDays((double)Convert.ToInt32(MoreAll.Other.Giatri("NgayHienThi")));
                    DateTime NgayHienTai = DateTime.Now;
                    if ((NgayXuLy >= NgayHienTai))
                    {
                        string bc = Request.Url.Authority + Request.RawUrl.ToString();
                        if (!bc.Contains("localhost"))
                        {
                            if (System.Web.HttpContext.Current.Session["TBTBNCC"] != "TBTBNCC")
                            {
                                ltscript.Text += " <script> $(document).ready(function () { $('#ThongBaoToanBoNCC').modal('show'); }); </script>";
                                System.Web.HttpContext.Current.Session["TBTBNCC"] = "TBTBNCC";
                            }
                        }
                        str += MoreAll.Other.Giatri("noiDungNCC");
                    }
                }
            }
            return str;
        }

        protected string ShowName(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += "Sản phẩm: <a style='color:red' target=\"_blank\" href=\"/" + dt[0].TangName + "_sp" + dt[0].ipid + ".html\">" + dt[0].Name.ToString() + "</a>";
            }
            return str.ToString();
        }

    }
}