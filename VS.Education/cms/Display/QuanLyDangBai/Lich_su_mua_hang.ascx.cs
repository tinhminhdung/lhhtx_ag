using Entity;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class Lich_su_mua_hang : System.Web.UI.UserControl
    {
        private string status = "1";
        public int i = 1;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Form.DefaultButton = btnshow.UniqueID;
            if (!base.IsPostBack)
            {
                if (Request["st"] != null && !Request["st"].Equals(""))
                {
                    ddlstatus.SelectedValue = Request["st"];
                }
            }
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
            string sql = "select * from Carts where IDThanhVien=" + hdid.Value + "";
            if (!ddlstatus.SelectedValue.Equals("-1"))
            {
                sql = sql + " and  Status=" + ddlstatus.SelectedValue + " ";
            }
            if (txtkeyword.Text.Length > 0)
            {
                sql = sql + " and  dbo.fuConvertToUnsign(Name) LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR Address LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR Phone LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR Email LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR Money LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "'";
            }
            sql = sql + " order by Create_Date desc";
            List<LCart> dt = db.ExecuteQuery<LCart>(@"" + sql + "").ToList();
            CollectionPager1.DataSource = dt;
            CollectionPager1.BindToControl = rp_items;
            CollectionPager1.MaxPages = 10000;
            CollectionPager1.PageSize = 30;
            rp_items.DataSource = CollectionPager1.DataSourcePaged;
            rp_items.DataBind();

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

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowProducts();
            Response.Redirect("/lich-su-mua-hang.html?st=" + ddlstatus.SelectedValue + "");
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            this.ShowProducts();
        }

        protected string ShowtrangthaiXL(string ID, string sql, string MauNenChuaOk, string MaunenOK)
        {
            string str = "";
            List<Entity.CartDetail> dtcart1 = SCartDetail.Name_Text("select * from CartDetail where ID_Cart=" + ID + " " + sql + "  and  IDThanhVien=" + hdid.Value + "");
            if (dtcart1.Count > 0)
            {
                str += "<a style='background:#" + MaunenOK + "; border-radius: 30px; color: #fff; display: inline-block; font-size: 14px; font-weight: bold; height: 25px; padding: 4px; text-align: center; width: 25px;'>" + dtcart1.Count + "</a> ";
            }
            else
            {
                str += "<a style='background:#" + MauNenChuaOk + "; border-radius: 30px; color: #fff; display: inline-block; font-size: 14px; font-weight: bold; height: 25px; padding: 4px; text-align: center; width: 25px;'>0</a> ";
            }
            return str;
        }

        protected string TongDonHang(string ID)
        {
            List<Entity.CartDetail> dtcart = SCartDetail.Name_Text("select * from CartDetail where ID_Cart=" + ID + "  and  IDThanhVien=" + hdid.Value + "");
            if (dtcart.Count > 0)
            {
                return "<a style='background: #ffa903;color: #fff; display: inline-block; font-size: 14px; font-weight: bold; height: 25px; padding: 4px; text-align: center; width: 25px;'>" + dtcart.Count().ToString() + "</a>";
            }
            return "<a style='background: #ffa903;color: #fff; display: inline-block; font-size: 14px; font-weight: bold; height: 25px; padding: 4px; text-align: center; width: 25px;'>0</a>";
        }

        protected void Export_Click(object sender, EventArgs e)
        {
            string Namefile = "LichSuMuaHang" + DateTime.Now;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=" + Namefile + ".xls");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            sb.Append("<table border='1' bgcolor='#ffffff' bordercolor='#dedede' cellspacing='0' cellpadding='0' style='font-size:12px; font-family:Arial; background:white;'>");
            sb.Append("<tr>");
            sb.Append("  <th style=\"width:50px; vertical-align:middle; height: 22px;\">");
            sb.Append("    <b>STT</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Họ và tên</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:400px; vertical-align:middle;\">");
            sb.Append("    <b>Địa chỉ</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Điện thoại</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Email</b>");
            sb.Append("  </th>");
       
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Ngày Mua</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:520px; vertical-align:middle;\" colspan=\"3\">");
            sb.Append("    <b>Ghi chú</b>");
            sb.Append("  </th>");
            sb.Append("  </tr>");

            string sql = "select * from Carts where IDThanhVien=" + hdid.Value + "";
            //if (!ddlstatus.SelectedValue.Equals("-1"))
            //{
            //    sql = sql + " and  Status=" + ddlstatus.SelectedValue + " ";
            //}
            //if (txtkeyword.Text.Length > 0)
            //{
            //    sql = sql + " and  dbo.fuConvertToUnsign(Name) LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR Address LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR Phone LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR Email LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR Money LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "'";
            //}
            sql = sql + " order by Create_Date desc";

            List<LCart> dt = db.ExecuteQuery<LCart>(@"" + sql + "").ToList();
            // List<Carts> dt = SCarts.p_cartslist_count(ddlstatus.SelectedValue, txtkeyword.Text);
            if (dt.Count > 0)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr style=' height: 22px;background:#f3f200'>");
                    sb.Append("    <td style=\"vertical-align:middle; height: 22px; text-align: center;\" colspan=\"8\">MÃ ĐƠN HÀNG: #" + item.ID.ToString() + "<td>");
                    sb.Append("  </tr>");

                    sb.Append("    <tr style='height: 45px;background:#f79646'>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + item.Name + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: center;\">" + item.Address + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: center;\">" + item.Phone + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + item.Email + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle; text-align: center;\">" + item.Create_Date + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: center;\" colspan=\"3\">" + item.Contents + "</td>");
                   

                    sb.Append("  </tr>");

                    List<Entity.CartDetail> table = SCartDetail.Detail_ID_Cart(item.ID.ToString());
                    if (table.Count > 0)
                    {
                        sb.Append("<tr>");
                        sb.Append("    <td style=\"vertical-align:middle; height: 22px; text-align: center;\" colspan=\"9\">");
                        sb.Append(" <table border='1' bgcolor='#ffffff' bordercolor='#ffa903'  cellspacing='0' cellpadding='0' style='font-size:12px; font-family:Arial; background:white;'>");
                        sb.Append("<tr style=\" background:#dcdcdc\">");
                        sb.Append("  <th style=\"width:50px; vertical-align:middle; height: 22px;\">");
                        sb.Append("    <b>STT</b>");
                        sb.Append("  </th>");
                        sb.Append("  <th style=\"width:130px; vertical-align:middle;\">");
                        sb.Append("    <b>Mã sản phẩm</b>");
                        sb.Append("  </th>");
                        sb.Append("  <th style=\"width:400px; vertical-align:middle;\">");
                        sb.Append("    <b>Tên sản phẩm</b>");
                        sb.Append("  </th>");
                        sb.Append("  <th style=\"width:186px; vertical-align:middle;\">");
                        sb.Append("    <b>Số lượng</b>");
                        sb.Append("  </th>");
                        sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
                        sb.Append("    <b>Đ.V tính</b>");
                        sb.Append("  </th>");
                        sb.Append("  <th style=\"width:160px; vertical-align:middle;\">");
                        sb.Append("    <b>Đơn giá</b>");
                        sb.Append("  </th>");
                        sb.Append("  <th style=\"width:160px; vertical-align:middle;\">");
                        sb.Append("    <b>Thành tiền</b>");
                        sb.Append("  </th>");

                        sb.Append("  <th style=\"width:520px; vertical-align:middle;\">");
                        sb.Append("    <b>Trạng thái Người Mua</b>");
                        sb.Append("  </th>");
                        sb.Append("  <th style=\"width:520px; vertical-align:middle;\">");
                        sb.Append("    <b>Trạng thái Nhà CC</b>");
                        sb.Append("  </th>");
                        sb.Append("  <th style=\"width:520px; vertical-align:middle;\">");
                        sb.Append("    <b>Trạng thái Khiếu kiện</b>");
                        sb.Append("  </th>");
                        sb.Append("  </tr>");
                        int j = 1;
                        foreach (var item1 in table)
                        {
                            sb.Append("    <tr style=''>");
                            sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + j++ + "</td>");
                            sb.Append("    <td style=\"width:130px; vertical-align:middle;text-align: center;\">" + Code(item1.ipid.ToString()) + "</td>");
                            sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + item1.Name + "</td>");
                            sb.Append("    <td style=\"width:186px; vertical-align:middle;text-align: center;\">" + item1.Quantity + "</td>");
                            sb.Append("    <td style=\"width:120px; vertical-align:middle;text-align: center;\">VNĐ</td>");
                            sb.Append("    <td style=\"width:160px; vertical-align:middle; text-align: center;\">" + FomatPrice(item1.Price.ToString()) + "</td>");
                            sb.Append("        <td style=\"width:160px; vertical-align:middle; text-align: center;\">" + FomatPrice(item1.Money.ToString()) + "</td>");
                            sb.Append("    <td style=\"width:160px; vertical-align:middle; text-align: center;\">" + TrangThaiNguoiMuaHang(item1.TrangThaiNguoiMuaHang.ToString()) + "</td>");
                            sb.Append("    <td style=\"width:160px; vertical-align:middle; text-align: center;\">" + TrangThaiNhaCungCap(item1.TrangThaiNhaCungCap.ToString()) + "</td>");
                            sb.Append("    <td style=\"width:160px; vertical-align:middle; text-align: center;\">" + TrangThaiNhaCungCap(item1.TrangThaiKhieuKien.ToString()) + "</td>");

                            sb.Append("  </tr>");
                        }
                        sb.Append(" </tr>");

                        //sb.Append("  <tr>");
                        //sb.Append(" <td style=\"vertical-align:middle; height: 22px; text-align: center;font-weight:bold\" colspan=\"3\">Tổng cộng</td>");
                        //sb.Append("<td style=\"width:200px; vertical-align:middle;\">" + Soluong(item.ID.ToString()) + "</td>");
                        //sb.Append(" <td style=\"vertical-align:middle;\" colspan=\"2\">&nbsp;</td>");
                        //sb.Append(" <td style=\"width:120px; vertical-align:middle; text-align: center;\">" + AllQuery.MorePro.Detail_Price(item.Money.ToString()) + "</td>");
                        //sb.Append(" </tr>");
                        //sb.Append("  <tr><td style=\"vertical-align:middle; height: 22px; text-align: center;font-weight:bold\" colspan=\"7\">" + ConvertSoRaChu(item.Money) + "</td>");
                        //sb.Append("  <tr><td style=\"vertical-align:middle; height: 22px; text-align: center;font-weight:bold\" colspan=\"7\"></td>");

                        sb.Append("    </table>");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr style=' height: 22px;'>");
                    sb.Append("    <td style=\"vertical-align:middle; height: 22px; text-align: center;\" colspan=\"9\"></td>");
                    sb.Append("  </tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
        public static string FomatPrice(string money)
        {
            try
            {
                if (money.Length > 0)
                {
                    double value = Convert.ToDouble(money.ToString());
                    double tong = (value / 1000);
                    return tong.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string TrangThaiKhieuKien(string enable)
        {
            if (enable.Trim().Equals("1"))
            {
                return "Khiếu kiện";
            }
            else if (enable.Trim().Equals("2"))
            {
                return "Chấp nhận Thanh Toán";
            }
            else if (enable.Trim().Equals("3"))
            {
                return "Hoàn Tiền";
            }
            return "";
        }
        public static string TrangThaiNhaCungCap(string enable)
        {
            if (enable.Trim().Equals("1"))
            {
                return "Chấp nhận";
            }
            else if (enable.Trim().Equals("2"))
            {
                return "Hủy đơn";
            }
            else if (enable.Trim().Equals("3"))
            {
                return "Chưa xử lý";
            }
            return "";
        }
        public static string TrangThaiNguoiMuaHang(string enable)
        {
            if (enable.Trim().Equals("1"))
            {
                return "Chấp nhận";
            }
            else if (enable.Trim().Equals("2"))
            {
                return "Trả lại";
            }
            else if (enable.Trim().Equals("3"))
            {
                return "Chưa xử lý";
            }
            return "";
        }

        protected string Code(string id)
        {
            string str = "";
            try
            {
                List<Entity.Products> dt = SProducts.GetById(id);
                if (dt.Count > 0)
                {
                    str += dt[0].Code.ToString();
                }
            }
            catch (Exception)
            { }
            return str.ToString();
        }
    }
}