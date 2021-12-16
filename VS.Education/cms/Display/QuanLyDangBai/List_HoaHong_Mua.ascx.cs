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
    public partial class List_HoaHong_Mua : System.Web.UI.UserControl
    {
        public int i = 1;
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["lang"] != null)
            {
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["lang"] = this.lang;
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            if (!base.IsPostBack)
            {
                #region UpdatePanel
                #endregion
                this.DropDownList3.Items.Clear();
                this.DropDownList3.Items.Insert(0, new ListItem("Tất cả các năm", "0"));
                for (int i = 2018; i < (DateTime.Now.Year + 1); i++)
                {
                    this.DropDownList3.Items.Add(new ListItem("Năm " + i.ToString(), i.ToString()));
                }
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.DropDownList3, "0");

                if (Request["ngay"] != null && !Request["ngay"].Equals(""))
                {
                    DropDownList1.SelectedValue = Request["ngay"];
                }
                if (Request["thang"] != null && !Request["thang"].Equals(""))
                {
                    DropDownList2.SelectedValue = Request["thang"];
                }
                if (Request["nam"] != null && !Request["nam"].Equals(""))
                {
                    DropDownList3.SelectedValue = Request["nam"];
                }
                if (Request["type"] != null && !Request["type"].Equals(""))
                {
                    ddlkieu.SelectedValue = Request["type"];
                }
            }


            ShowInfo();
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                LoadItems();
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
                    hdid.Value = table.iuser_id.ToString();

                }
            }
        }
        private void btn_link_cancel_Click(object sender, EventArgs e)
        {
            this.pn_list.Visible = true;
        }
        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xóa bài viết này ?')";
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        private void LoadItems()
        {
            string sql = "";
            if (Request["nam"] != null && !Request["nam"].Equals("0"))
            {
                sql += " and year(NgayTao)=" + Request["nam"] + "";
            }
            if (Request["thang"] != null && !Request["thang"].Equals("0"))
            {
                sql += " and month(NgayTao)=" + Request["thang"] + "";
            }
            if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
            {
                sql += " and day(NgayTao)=" + Request["ngay"] + "";
            }
            if (hdid.Value != "0")
            {
                sql += " and IDUserNguoiDuocHuong=" + hdid.Value + "";
            }
            if (ddlkieu.SelectedValue != "0")
            {
                sql += " and IDType=" + ddlkieu.SelectedValue + "";
            }
            //else
            //{
            //    sql += " and IDType in (6,7,71,72,73,74,75,76,77,78,79,9,13,30,55,300,302)";
            //}
            List<EHoaHongThanhVien> table = SHoaHongThanhVien.Name_Text("SELECT * from HoaHongThanhVien where 1=1 " + sql + " order by NgayTao desc");
            CollectionPager1.DataSource = table;
            CollectionPager1.BindToControl = rp_pagelist;
            CollectionPager1.MaxPages = 10000;
            CollectionPager1.PageSize = int.Parse("20");
            rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
            rp_pagelist.DataBind();
            if (!hdid.Value.Equals("0"))
            {
                double num = 0.0;
                double coin = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    if (table[i].TrangThai == 1)
                    {
                        coin += Convert.ToDouble(table[i].SoCoin.ToString());
                    }
                }
                ltCoin.Text = "Tổng điểm: " + coin.ToString();
            }
        }
        protected string ShowtThanhVien(string id)
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
                str += "</span><br>";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone;
                }
            }
            return str;
        }


        protected void ddlthanhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void bthienthi_Click(object sender, EventArgs e)
        {
            Show();
        }

        void Show()
        {
            Response.Redirect("/tich-diem-hoa-hong-mua.html?ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "&type=" + ddlkieu.SelectedValue + "");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void ddlkieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }
        public string ShowTrangThai(string enable)
        {
            if (enable.Trim().Equals("1"))
            {
                return "Hoa Hồng Quản Lý F ...";
            }
            else if (enable.Trim().Equals("2"))
            {
                return "Hoa Hồng Quản Lý Leader";
            }
            else if (enable.Trim().Equals("3"))
            {
                return "Hoa Hồng Quản Lý level ...";
            }
            else if (enable.Trim().Equals("5"))
            {
                return "Hoa Hồng Quản Lý Chi Nhánh";
            }
            else if (enable.Trim().Equals("31"))
            {
                return "Hoa Hồng (Hỗ Trợ)";
            }
            else if (enable.Trim().Equals("10"))
            {
                return "Hoa hồng (Giới Thiệu Nhà Cung Cấp)";
            }
            else if (enable.Trim().Equals("11"))
            {
                return "Hoa hồng (Gián tiếp Nhà Cung Cấp) ..";
            }
            else if (enable.Trim().Equals("13"))
            {
                return "Hoa hồng (Nhà Cung Cấp)";
            }
            else if (enable.Trim().Equals("12"))
            {
                return "Hoa Hồng (Giới Thiệu Chi Nhánh Nhà Cung Cấp)";
            }
            else if (enable.Trim().Equals("6"))
            {
                return "Hoa hồng Mua Hàng Trực Tiếp";
            }
            else if (enable.Trim().Equals("7"))
            {
                return "Hoa hồng gián tiếp giới thiệu 1";
            }
            else if (enable.Trim().Equals("71"))
            {
                return "Hoa hồng gián tiếp giới thiệu 2";
            }
            else if (enable.Trim().Equals("72"))
            {
                return "Hoa hồng gián tiếp giới thiệu 3";
            }
            else if (enable.Trim().Equals("73"))
            {
                return "Hoa hồng gián tiếp giới thiệu 4";
            }
            else if (enable.Trim().Equals("74"))
            {
                return "Hoa hồng gián tiếp giới thiệu 5";
            }
            else if (enable.Trim().Equals("75"))
            {
                return "Hoa hồng cấp quản lý 1";
            }
            else if (enable.Trim().Equals("76"))
            {
                return "Hoa hồng cấp quản lý 2";
            }
            else if (enable.Trim().Equals("77"))
            {
                return "Hoa hồng cấp quản lý 3";
            }
            else if (enable.Trim().Equals("78"))
            {
                return "Hoa hồng cấp quản lý 4";
            }
            else if (enable.Trim().Equals("79"))
            {
                return "Hoa hồng cấp quản lý 5";
            }

            else if (enable.Trim().Equals("8"))
            {
                return "Hoa Hồng giới thiệu Mua Hàng gián tiếp Thưởng thêm";
            }
            else if (enable.Trim().Equals("9"))
            {
                return "Hoa Hồng (Chi Nhánh Mua Hàng)";
            }
            else if (enable.Trim().Equals("30"))
            {
                return "Thanh toán tiền đơn hàng cho nhà Cung cấp";
            }
            else if (enable.Trim().Equals("12"))
            {
                return "Hoa Hồng (Chi Nhánh Bán Hàng)";
            }
            else if (enable.Trim().Equals("13"))
            {
                return "Hoa Hồng (Leader - Mua Hàng)";
            }
            else if (enable.Trim().Equals("55"))
            {
                return "Hoa hồng cấp quản lý 6 .. 50";
            }
            else if (enable.Trim().Equals("300"))
            {
                return "Thưởng vào ví mua hàng";
            }
            else if (enable.Trim().Equals("302"))
            {
                return "Điểm danh nhận thưởng";
            }
            else if (enable.Trim().Equals("200"))
            {
                return "Hoa hồng QRCode 1";
            }
            else if (enable.Trim().Equals("201"))
            {
                return "Hoa hồng QRCode 2";
            }
            else if (enable.Trim().Equals("202"))
            {
                return "Hoa hồng QRCode 3";
            }
            else if (enable.Trim().Equals("203"))
            {
                return "Hoa hồng QRCode 4";
            }
            else if (enable.Trim().Equals("204"))
            {
                return "Hoa hồng QRCode 5";
            }
            else if (enable.Trim().Equals("205"))
            {
                return "Hoa hồng QRCode Level 1";
            }
            else if (enable.Trim().Equals("206"))
            {
                return "Hoa hồng QRCode Level 2";
            }
            else if (enable.Trim().Equals("207"))
            {
                return "Hoa hồng QRCode Level 3";
            }
            else if (enable.Trim().Equals("208"))
            {
                return "Hoa hồng QRCode Level 4";
            }
            else if (enable.Trim().Equals("209"))
            {
                return "Hoa hồng QRCode Level 5";
            }
            else if (enable.Trim().Equals("210"))
            {
                return "Hoa hồng QRCode Level 6 .. 50";
            }
            else if (enable.Trim().Equals("211"))
            {
                return "Hoa Hồng QRCode (Chi Nhánh Mua Hàng)";
            }
            else if (enable.Trim().Equals("212"))
            {
                return "Hoa Hồng QRCode (Leader - Mua Hàng)";
            }
            else if (enable.Trim().Equals("213"))
            {
                return "Hoa Hồng QRCode cho người mua";
            }
            else if (enable.Trim().Equals("214"))
            {
                return "Thanh toán điểm QRCode cho người bán";
            }
            else if (enable.Trim().Equals("403"))
            {
                return "Hoa hồng  - Danh số đồng hưởng";
            }
            else if (enable.Trim().Equals("500"))
            {
                return "Hoa hồng Trực tiếp BĐS";
            }
            else if (enable.Trim().Equals("504"))
            {
                return "Hoa hồng gián tiếp BĐS";
            }
            else if (enable.Trim().Equals("501"))
            {
                return "Hoa hồng Văn phòng  - BĐS";
            }
            else if (enable.Trim().Equals("502"))
            {
                return "Hoa hồng Đồng hưởng - BĐS";
            }
            else if (enable.Trim().Equals("503"))
            {
                return "Hoa hồng Cấp bậc - BĐS";
            }
            return "";
        }
        protected string ShowPro(string id)
        {
            string str = "";
            if (id != "0")
            {
                List<Entity.Products> dt = SProducts.GetById(id);
                if (dt.Count >= 1)
                {
                    str = "<a class='mausp' href=\"/" + dt[0].TangName + "_sp" + dt[0].ipid + ".html\" target=\"_blank\">" + dt[0].Name + "</a>";
                }
            }
            return str;
        }
        protected void XuatExel_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (Request["nam"] != null && !Request["nam"].Equals("0"))
            {
                sql += " and year(NgayTao)=" + Request["nam"] + "";
            }
            if (Request["thang"] != null && !Request["thang"].Equals("0"))
            {
                sql += " and month(NgayTao)=" + Request["thang"] + "";
            }
            if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
            {
                sql += " and day(NgayTao)=" + Request["ngay"] + "";
            }
            if (hdid.Value != "0")
            {
                sql += " and IDUserNguoiDuocHuong=" + hdid.Value + "";
            }
            if (ddlkieu.SelectedValue != "0")
            {
                sql += " and IDType=" + ddlkieu.SelectedValue + "";
            }
            //else
            //{
            //    sql += " and IDType in (6,7,71,72,73,74,75,76,77,78,79,9,13,30,55)";
            //}
            List<EHoaHongThanhVien> dt = SHoaHongThanhVien.Name_Text("SELECT * from HoaHongThanhVien where 1=1 " + sql + " order by NgayTao desc");

            string Namefile = "Danhsachhoahong" + DateTime.Now;
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
            sb.Append("  <th style=\"width:520px; vertical-align:middle;\">");
            sb.Append("    <b>Kiểu Hoa Hồng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Người tham gia Đăng ký</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:400px; vertical-align:middle;\">");
            sb.Append("    <b>Người được hưởng giới thiệu</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>% Hoa Hồng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Điểm thưởng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Ngày tạo</b>");
            sb.Append("  </th>");
            //sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            //sb.Append("    <b>Trạng thái</b>");
            //sb.Append("  </th>");
            sb.Append("</tr>");
            if (dt.Count > 0)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + ShowTrangThai(item.IDType.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: center;\">" + Commond.EXLShowtThanhVien(item.IDThanhVien.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: center;\">" + Commond.EXLShowtThanhVien(item.IDUserNguoiDuocHuong.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + item.PhamTramHoaHong + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle; text-align: center;\">" + item.SoCoin + "</td>");
                    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: center;\">" + MoreAll.FormatDateTime.FormatDateFull(item.NgayTao) + "</td>");
                    //if (item.TrangThai.ToString() == "1")
                    //{
                    //    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: center;\">Đã duyệt</td>");
                    //}
                    //else
                    //{
                    //    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: center;\">Chưa duyệt</td>");
                    //}
                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
    }
}