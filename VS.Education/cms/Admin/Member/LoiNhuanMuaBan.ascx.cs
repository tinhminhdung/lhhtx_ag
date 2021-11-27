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
    public partial class LoiNhuanMuaBan : System.Web.UI.UserControl
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
            return " and ( day(NgayTao)=" + Convert.ToDateTime(date).ToString("dd") + " and MONTH(NgayTao)=" + Convert.ToDateTime(date).ToString("MM") + "  and  year(NgayTao)=" + Convert.ToDateTime(date).ToString("yyyy") + " )";
        }
        public void LoadItems()
        {
            string sql = "";

            //if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            //{
            //    sql += LocDate_NgayThangNam(txtNgayThangNam.Text);
            //}
            //else if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && !string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            //{
            //    sql += " and ( NgayTao>='" + MoreAll.MoreAll.LocDate(txtNgayThangNam.Text) + "' and  NgayTao<='" + MoreAll.MoreAll.LocDate(txtDenNgayThangNam.Text) + "' )";
            //}

            if (Commond.Check(txtNgayThangNam.Text))
                fDate = Commond.ConvertStringToDate(txtNgayThangNam.Text, "dd/MM/yyyy");
            if (Commond.Check(txtDenNgayThangNam.Text))
                tDate = Commond.ConvertStringToDate(txtDenNgayThangNam.Text, "dd/MM/yyyy");

            if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND NgayTao IS NOT NULL AND ((DATEADD(dd,-31,NgayTao)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR NgayTao>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000') AND NgayTao <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999')";
            }
            else if (txtNgayThangNam.Text == "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND NgayTao IS NOT NULL AND NgayTao <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999'";
            }
            else if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text == "")
            {
                sql += " AND NgayTao IS NOT NULL AND (DATEADD(dd,-31,NgayTao)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR NgayTao>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000')";
            }

            if (txtkeyword.Text != "")
            {
                if (ddlkieuthanhvien.SelectedValue == "1")
                {
                    sql += " and IDThanhVienMua in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDThanhVienBan in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(ddlPage.SelectedValue);
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.ELoiNhuanMuaBan> iitem = SLoiNhuanMuaBan.CATEGORY_PHANTRANG1(sql);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();

                double TSoDiemGoc = 0.0;
                double TSoDiemDaChia = 0.0;
                double TSoDiemConLai = 0.0;
                for (int i = 0; i < iitem.Count; i++)
                {
                    TSoDiemGoc += Convert.ToDouble(iitem[i].SoDiemGoc.ToString());
                    TSoDiemDaChia += Convert.ToDouble(iitem[i].SoDiemDaChia.ToString());
                    TSoDiemConLai += Convert.ToDouble(iitem[i].SoDiemConLai.ToString());
                }
                lttongtien1.Text = TSoDiemGoc.ToString();
                ltdachia1.Text = TSoDiemDaChia.ToString();
                ltconlai1.Text = TSoDiemConLai.ToString();
            }
            List<Entity.ELoiNhuanMuaBan> dt = SLoiNhuanMuaBan.CATEGORY_PHANTRANG2(sql, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                double TSoDiemGoc = 0.0;
                double TSoDiemDaChia = 0.0;
                double TSoDiemConLai = 0.0;
                for (int i = 0; i < dt.Count; i++)
                {
                    TSoDiemGoc += Convert.ToDouble(dt[i].SoDiemGoc.ToString());
                    TSoDiemDaChia += Convert.ToDouble(dt[i].SoDiemDaChia.ToString());
                    TSoDiemConLai += Convert.ToDouble(dt[i].SoDiemConLai.ToString());
                }
                lttongtien.Text = TSoDiemGoc.ToString();
                ltdachia.Text = TSoDiemDaChia.ToString();
                ltconlai.Text = TSoDiemConLai.ToString();

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
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=LoiNhuan&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "", Tongsobanghi, pages);
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
        protected string ShowDaBanSanPham(string ID, string ID_Cart)
        {
            double tong = 0;
            // var dt = db.CartDetails.Where(x => x.ipid == int.Parse(ID) && ID_Cart ==(ID_Cart)).ToList();
            List<Entity.CartDetail> dt = SCartDetail.Name_Text("SELECT * FROM  CartDetail where ipid=" + ID + " and ID_Cart=" + ID_Cart + "");
            if (dt != null)
            {
                for (int i = 0; i < dt.Count(); i++)
                {
                    tong += Convert.ToDouble(dt[i].Quantity.ToString());
                }
                return tong.ToString();
            }
            return "0";
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
            Response.Redirect("/admin.aspx?u=LoiNhuan&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "");
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
            string Namefile = "LoiNhuanMuaBan_ " + DateTime.Now;
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
            sb.Append("  <th style=\"width:50px; text-align: center; height: 22px;\">");
            sb.Append("    <b>STT</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:520px; text-align: center;\">");
            sb.Append("    <b>Sản phẩm</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:40px; text-align: center;\">");
            sb.Append("    <b>Mã đơn hàng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; text-align: center;\">");
            sb.Append("    <b>Người Mua Hàng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; text-align: center;\">");
            sb.Append("    <b>Nhà cung cấp</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; text-align: center;\">");
            sb.Append("    <b>Số lượng bán</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; text-align: center;\">");
            sb.Append("    <b>Số điểm gốc</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; text-align: center;\">");
            sb.Append("    <b>Số điểm chia HH</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; text-align: center;\">");
            sb.Append("    <b>Số điểm còn lại</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:130px; text-align: center;\">");
            sb.Append("    <b>Ngày giao dịch</b>");
            sb.Append("  </th>");
            sb.Append("</tr>");

            string sql = "";

            if (Commond.Check(txtNgayThangNam.Text))
                fDate = Commond.ConvertStringToDate(txtNgayThangNam.Text, "dd/MM/yyyy");
            if (Commond.Check(txtDenNgayThangNam.Text))
                tDate = Commond.ConvertStringToDate(txtDenNgayThangNam.Text, "dd/MM/yyyy");

            if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND NgayTao IS NOT NULL AND ((DATEADD(dd,-31,NgayTao)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR NgayTao>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000') AND NgayTao <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999')";
            }
            else if (txtNgayThangNam.Text == "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND NgayTao IS NOT NULL AND NgayTao <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999'";
            }
            else if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text == "")
            {
                sql += " AND NgayTao IS NOT NULL AND (DATEADD(dd,-31,NgayTao)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR NgayTao>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000')";

            }
            //if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            //{
            //    sql += LocDate_NgayThangNam(txtNgayThangNam.Text);
            //}
            //else if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && !string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            //{
            //    sql += " and ( NgayTao>='" + MoreAll.MoreAll.LocDate(txtNgayThangNam.Text) + "' and  NgayTao<='" + MoreAll.MoreAll.LocDate(txtDenNgayThangNam.Text) + "' )";
            //}
            if (txtkeyword.Text != "")
            {
                if (ddlkieuthanhvien.SelectedValue == "1")
                {
                    sql += " and IDThanhVienMua in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDThanhVienBan in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }

            List<Entity.ELoiNhuanMuaBan> dt = SLoiNhuanMuaBan.Name_Text("SELECT *  FROM  LoiNhuanMuaBan  where 1=1 " + sql + " ORDER BY NgayTao DESC");
            if (dt.Count >= 1)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"text-align: center; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"text-align: center;\">" + ShowPro(item.IDSanPham.ToString(), item.MoTa.ToString()) + "</td>");
                    sb.Append("    <td style=\"text-align: center;\"><a target=\"_blank\" href=\"/admin.aspx?u=cartsNhanh&Code=" + item.IDDonHang.ToString() + "\">" + item.IDDonHang.ToString() + "</a></td>");
                    sb.Append("    <td style=\"text-align: center;\">" + ShowtThanhVien(item.IDThanhVienMua.ToString()) + "</td>");
                    sb.Append("    <td style=\"text-align: center;\">" + ShowtThanhVien(item.IDThanhVienBan.ToString()) + "</td>");
                    sb.Append("    <td style=\"text-align: center;\">" + ShowDaBanSanPham(item.IDSanPham.ToString(), item.IDDonHang.ToString()) + "</td>");
                    sb.Append("    <td style=\"text-align: center;\">" + item.SoDiemGoc + "</td>");
                    sb.Append("    <td style=\"text-align: center;\">" + item.SoDiemDaChia + "</td>");
                    sb.Append("    <td style=\"text-align: center;\">" + item.SoDiemConLai + "</td>");
                    sb.Append("    <td style=\"text-align: center;\">" + item.NgayTao + "</td>");
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