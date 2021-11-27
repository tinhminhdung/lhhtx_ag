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
    public partial class LoiNhuanDangKyThanhVien : System.Web.UI.UserControl
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
                if (Request["chinhanh"] != null && !Request["chinhanh"].Equals(""))
                {
                    ddlchinhanh.SelectedValue = Request["chinhanh"];
                }
               
                if (Request["leader"] != null && !Request["leader"].Equals(""))
                {
                    ddlleader.SelectedValue = Request["leader"];
                }
                ShowLeader();
                ShowChiNhanh();
                this.LoadItems();
            }
        }
        protected void ShowLeader()
        {
            int str = 0;
            List<Entity.users> dt = Susers.Name_Text("select * from users  where Leader=1 ");
            for (int i = 0; i < dt.Count; i++)
            {
                ddlleader.Items.Insert(str, new ListItem(dt[i].vlname.ToString(), dt[i].iuser_id.ToString()));
            }
            this.ddlleader.Items.Insert(0, new ListItem("== Lọc theo Leader == ", "0"));
            this.ddlleader.DataBind();
        }
        protected void ShowChiNhanh()
        {
            int str = 0;
            List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.DL, this.lang, "-1", "1");
            for (int i = 0; i < dt.Count; i++)
            {
                ddlchinhanh.Items.Insert(str, new ListItem(dt[i].Name.ToString(), dt[i].ID.ToString()));
            }
            this.ddlchinhanh.Items.Insert(0, new ListItem("== Lọc theo chi nhánh == ", "0"));
            this.ddlchinhanh.DataBind();
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
                    sql += " and IDThanhVienDangKy in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDThanhVienGioiThieu in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }
            if (ddlchinhanh.SelectedValue != "0")
            {
                sql += " and IDChiNhanh =" + ddlchinhanh.SelectedValue + " ";
            }
            if (ddlleader.SelectedValue != "0")
            {
                sql += " and IDLeader =" + ddlleader.SelectedValue + " ";
            }


            
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(ddlPage.SelectedValue);
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.ELoiNhuanDangKyThanhVien> iitem = SLoiNhuanDangKyThanhVien.CATEGORY_PHANTRANG1(sql);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.ELoiNhuanDangKyThanhVien> dt = SLoiNhuanDangKyThanhVien.CATEGORY_PHANTRANG2(sql, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                double TSoDiemGoc = 0.0;
                double TSoDiemDaChia = 0.0;
                double TSoDiemConLai = 0.0;

                for (int i = 0; i < dt.Count; i++)
                {
                    TSoDiemGoc += Convert.ToDouble(dt[i].SoDiemNapVao.ToString());
                    TSoDiemDaChia += Convert.ToDouble(dt[i].SoDiemDaChia.ToString());
                    TSoDiemConLai += Convert.ToDouble(dt[i].SoDiemConLai.ToString());
                }
                ltCoin.Text += "<span style='margin-right: 13px;'>(Tổng điểm gốc: " + TSoDiemGoc.ToString() + ")</span>";
                ltCoin.Text += " - <span  style='margin-right: 13px;'> (Tổng điểm đã chia HH: " + TSoDiemDaChia.ToString() + ")</span>";
                ltCoin.Text += " - <span  style='margin-right: 13px;'>(Tổng điểm còn lại: " + TSoDiemConLai.ToString() + ")</span>";

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
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=LoiNhuanDangKy&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&ChiNhanh=" + ddlchinhanh.SelectedValue + "&leader=" + ddlleader.SelectedValue + "", Tongsobanghi, pages);
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
            Response.Redirect("/admin.aspx?u=LoiNhuanDangKy&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&ChiNhanh=" + ddlchinhanh.SelectedValue + "&leader=" + ddlleader.SelectedValue + "");
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

        protected void ddlchinhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void lnkxuatExel_Click(object sender, EventArgs e)
        {
            string Namefile = "LoiNhuanDangKyThanhVien_ " + DateTime.Now;
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
            sb.Append("    <b>Người Đăng Ký</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Người Giới Thiệu</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:40px; vertical-align:middle;\">");
            sb.Append("    <b>Số điểm gốc</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Số điểm chia HH</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Số điểm còn lại</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:90px; vertical-align:middle;\">");
            sb.Append("    <b>Ngày giao dịch</b>");
            sb.Append("  </th>");
            sb.Append("</tr>");

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
                    sql += " and IDThanhVienDangKy in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDThanhVienGioiThieu in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }
            if (ddlchinhanh.SelectedValue != "0")
            {
                sql += " and IDChiNhanh =" + ddlchinhanh.SelectedValue + " ";
            }
            if (ddlleader.SelectedValue != "0")
            {
                sql += " and IDLeader =" + ddlleader.SelectedValue + " ";
            }
            List<Entity.ELoiNhuanDangKyThanhVien> dt = SLoiNhuanDangKyThanhVien.Name_Text("SELECT *  FROM  LoiNhuanDangKyThanhVien  where 1=1 " + sql + " ORDER BY NgayTao DESC");
            if (dt.Count >= 1)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + ShowtThanhVien(item.IDThanhVienDangKy.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + ShowtThanhVien(item.IDThanhVienGioiThieu.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: left;\">" + item.SoDiemNapVao + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.SoDiemDaChia + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.SoDiemConLai + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.NgayTao + "</td>");
                    sb.Append("</tr>");
                }
            }

            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void ddlleader_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }
    }
}