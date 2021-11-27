using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using System.IO;
using MoreAll;
using Entity;
using System.Text;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class MViTamMuaHang : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        DateTime fDate, tDate;
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
                if (!Commond.Setting("PageLoiNhuan").Equals(""))
                {
                    ddlPage.SelectedValue = Commond.Setting("PageLoiNhuan");
                }
                if (Request["kw"] != null && !Request["kw"].Equals(""))
                {
                    txtkeyword.Text = Request["kw"];
                }
                if (Request["kieu"] != null && !Request["kieu"].Equals(""))
                {
                    ddlkieuthanhvien.SelectedValue = Request["kieu"];
                }
                if (Request["Tu"] != null && !Request["Tu"].Equals(""))
                {
                    txtNgayThangNam.Text = Request["Tu"];
                }
                if (Request["Den"] != null && !Request["Den"].Equals(""))
                {
                    txtDenNgayThangNam.Text = Request["Den"];
                }
                if (Request["Code"] != null && !Request["Code"].Equals(""))
                {
                    txtkeywordCode.Text = Request["Code"];
                }
                this.LoadItems();
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

        public static string LocDate_NgayThangNam(string date)
        {
            return " and ( day(NgayCapNhat)=" + Convert.ToDateTime(date).ToString("dd") + " and MONTH(NgayCapNhat)=" + Convert.ToDateTime(date).ToString("MM") + "  and  year(NgayCapNhat)=" + Convert.ToDateTime(date).ToString("yyyy") + " )";
        }
        public void LoadItems()
        {
            string sql = "";

            if (Commond.Check(txtNgayThangNam.Text))
                fDate = Commond.ConvertStringToDate(txtNgayThangNam.Text, "dd/MM/yyyy");
            if (Commond.Check(txtDenNgayThangNam.Text))
                tDate = Commond.ConvertStringToDate(txtDenNgayThangNam.Text, "dd/MM/yyyy");

            if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND NgayCapNhat IS NOT NULL AND ((DATEADD(dd,-31,NgayCapNhat)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR NgayCapNhat>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000') AND NgayCapNhat <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999')";
            }
            else if (txtNgayThangNam.Text == "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND NgayCapNhat IS NOT NULL AND NgayCapNhat <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999'";
            }
            else if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text == "")
            {
                sql += " AND NgayCapNhat IS NOT NULL AND (DATEADD(dd,-31,NgayCapNhat)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR NgayCapNhat>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000')";
            }

            if (txtkeyword.Text != "")
            {
                if (ddlkieuthanhvien.SelectedValue == "1")
                {
                    sql += " and IDThanhVienMua in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDNhaCungCap in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }
            if (txtkeywordCode.Text != "")
            {
                if (MoreAll.RegularExpressions.IsValidInt(txtkeywordCode.Text.Trim()) == true)
                {
                    sql += " and IDCarts=" + txtkeywordCode.Text.Trim().Replace("&nbsp;", "") + " ";
                }
            }

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(ddlPage.SelectedValue);
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.EViTamMuaHang> iitem = SViTamMuaHang.CATEGORY_PHANTRANG1(sql);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.EViTamMuaHang> dt = SViTamMuaHang.CATEGORY_PHANTRANG2(sql, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                double TSoTienNhaCCSeNhan = 0.0;
                double TSoTienNguoiMuaBiTru = 0.0;
                double TSoDiemThuong = 0.0;

                for (int i = 0; i < dt.Count; i++)
                {
                    TSoTienNhaCCSeNhan += Convert.ToDouble(dt[i].SoTienNhaCCSeNhan.ToString());
                    TSoTienNguoiMuaBiTru += Convert.ToDouble(dt[i].SoTienNguoiMuaBiTru.ToString());
                    TSoDiemThuong += Convert.ToDouble(dt[i].SoDiemThuong.ToString());
                }
                ltCoin.Text += "<span style='margin-right: 13px;'>(Tổng điểm NCC sẽ nhận: " + TSoTienNhaCCSeNhan.ToString() + ")</span>";
                ltCoin.Text += " - <span  style='margin-right: 13px;'> (Tổng điểm người mua thanh toán: " + TSoTienNguoiMuaBiTru.ToString() + ")</span>";
                ltCoin.Text += " - <span  style='margin-right: 13px;'>(Tổng điểm sẽ đi chia: " + TSoDiemThuong.ToString() + ")</span>";

                rp_pagelist.DataSource = dt;
                rp_pagelist.DataBind();
            }

            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=ViTamMuaHang&kw=" + txtkeyword.Text + "&Code=" + txtkeywordCode.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "", Tongsobanghi, pages);
        }
        protected string SearchThanhVien(string keyword)
        {
            string str = "0";
            List<Entity.users> dt = Susers.Name_Text("select * from users where (vuserun like N'%" + keyword + "%')");
            if (dt.Count >= 1)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    str = str + "," + dt[i].iuser_id.ToString();
                }
            }
            return str.Replace("0,", "");
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
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + " (Level: " + dt[0].LevelThanhVien + ")</span></a>";
                }
                str += "</span><br>";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone;
                }
            }
            else
            {
                str = "Không tìm thấy thành viên";
            }
            return str;
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoreAll.MoreAll.Update_setting("PageLoiNhuan", ddlPage.SelectedValue);
            Show();
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
            Response.Redirect("/admin.aspx?u=ViTamMuaHang&kw=" + txtkeyword.Text + "&Code=" + txtkeywordCode.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "");
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

        protected string ShowPro(string id, string NoiDung)
        {
            string str = "";
            if (id != "0")
            {
                List<Entity.Products> dt = SProducts.GetById(id);
                if (dt.Count > 0)
                {
                    str = "<a href=\"/" + dt[0].TangName + "_sp" + dt[0].ipid + ".html\" target=\"_blank\">" + dt[0].Name + "</a>";
                }
                else
                {
                    return NoiDung;
                }
            }
            return str;
        }
        protected void ddlthanhvienhuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            Show();
        }
        protected void txtNgayThangNam_TextChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void txtDenNgayThangNam_TextChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void lnkxuatExel_Click(object sender, EventArgs e)
        {
            string Namefile = "ViTamMuaHang_ " + DateTime.Now;
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
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Sản phẩm</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Người mua</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Nhà cung cấp</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:40px; vertical-align:middle;\">");
            sb.Append("    <b>Số điểm NCC nhận</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Số điểm người mua thanh toán</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Số điểm chia hoa hồng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:90px; vertical-align:middle;\">");
            sb.Append("    <b>Ngày tạo</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:90px; vertical-align:middle;\">");
            sb.Append("    <b>Lấy ở ví nào?</b>");
            sb.Append("  </th>");
            sb.Append("</tr>");

            string sql = "";
            if (Commond.Check(txtNgayThangNam.Text))
                fDate = Commond.ConvertStringToDate(txtNgayThangNam.Text, "dd/MM/yyyy");
            if (Commond.Check(txtDenNgayThangNam.Text))
                tDate = Commond.ConvertStringToDate(txtDenNgayThangNam.Text, "dd/MM/yyyy");

            if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND NgayCapNhat IS NOT NULL AND ((DATEADD(dd,-31,NgayCapNhat)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR NgayCapNhat>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000') AND NgayCapNhat <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999')";
            }
            else if (txtNgayThangNam.Text == "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND NgayCapNhat IS NOT NULL AND NgayCapNhat <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999'";
            }
            else if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text == "")
            {
                sql += " AND NgayCapNhat IS NOT NULL AND (DATEADD(dd,-31,NgayCapNhat)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR NgayCapNhat>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000')";
            }
            if (txtkeyword.Text != "")
            {
                if (ddlkieuthanhvien.SelectedValue == "1")
                {
                    sql += " and IDThanhVienDangKy in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDThanhVienGioiThieu in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }

            List<Entity.EViTamMuaHang> dt = SViTamMuaHang.Name_Text("SELECT *  FROM  ViTamMuaHang  where 1=1 " + sql + " ORDER BY NgayCapNhat DESC");
            if (dt.Count >= 1)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("<td style=\"width:400px; vertical-align:middle; text-align: left;\">");
                    sb.Append("<div>" + ShowPro(item.IDSanPham.ToString(), "") + "</div>");
                    sb.Append("<div>Mã đơn hàng: <a target=\"_blank\" href=\"/admin.aspx?u=cartsNhanh&Code=" + item.IDCarts.ToString() + "\">" + item.IDCarts.ToString() + "</a></div>");
                    sb.Append("</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: center;\">" + Commond.EXLShowtThanhVien(item.IDThanhVienMua.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: center;\">" + Commond.EXLShowtThanhVien(item.IDNhaCungCap.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + item.SoTienNhaCCSeNhan + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + item.SoTienNguoiMuaBiTru + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + item.SoDiemThuong + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle; text-align: center;\">" + item.NgayCapNhat + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle; text-align: center;\">" + MoreAll.MoreAll.Vinao(item.LayTienOVi.ToString()) + "</td>");

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