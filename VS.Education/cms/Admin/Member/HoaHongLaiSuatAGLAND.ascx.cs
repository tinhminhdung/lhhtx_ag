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
    public partial class HoaHongLaiSuatAGLAND : System.Web.UI.UserControl
    {
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
                if (!Commond.Setting("PageHoaHong").Equals(""))
                {
                    ddlPage.SelectedValue = Commond.Setting("PageHoaHong");
                }
                if (Request["kw"] != null && !Request["kw"].Equals(""))
                {
                    txtkeyword.Text = Request["kw"];
                }
                if (Request["kieu"] != null && !Request["kieu"].Equals(""))
                {
                    ddlkieuthanhvien.SelectedValue = Request["kieu"];
                }
                if (Request["type"] != null && !Request["type"].Equals(""))
                {
                    ddlkieu.SelectedValue = Request["type"];
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
        protected void rp_pagelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str = e.CommandName.Trim();
            string str2 = e.CommandArgument.ToString().Trim();
            string str4 = str;

            switch (e.CommandName)
            {
                case "Delete":
                    LaiSuatAGLANG del = db.LaiSuatAGLANGs.Where(s => s.ID == int.Parse(str2)).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        // LichSuGiaoDich(del.IDSanPham.ToString(), "21", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Xóa : " + del.Type + " ", del.IDThanhVien.ToString(), del.IDUserNguoiDuocHuong.ToString(), del.PhamTramHoaHong.ToString(), del.SoCoin.ToString());
                        db.LaiSuatAGLANGs.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        this.LoadItems();
                        this.ltmsg.Text = "";
                    }
                    return;
            }
        }
        public static string LocDate_NgayThangNam(string date)
        {
            return " and ( day(NgayNhan)=" + Convert.ToDateTime(date).ToString("dd") + " and MONTH(NgayNhan)=" + Convert.ToDateTime(date).ToString("MM") + "  and  year(NgayNhan)=" + Convert.ToDateTime(date).ToString("yyyy") + " )";
        }
        public void LoadItems()
        {
            string sql = "";
            //if (Request["nam"] != null && !Request["nam"].Equals("0"))
            //{
            //    sql += " and year(NgayNhan)=" + Request["nam"] + "";
            //}
            //if (Request["thang"] != null && !Request["thang"].Equals("0"))
            //{
            //    sql += " and month(NgayNhan)=" + Request["thang"] + "";
            //}
            //if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
            //{
            //    sql += " and day(NgayNhan)=" + Request["ngay"] + "";
            //}
            if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            {
                sql += LocDate_NgayThangNam(txtNgayThangNam.Text);
            }
            else if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && !string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            {
                sql += " and ( NgayNhan>='" + MoreAll.MoreAll.LocDate(txtNgayThangNam.Text) + "' and  NgayNhan<='" + MoreAll.MoreAll.LocDate(txtDenNgayThangNam.Text) + "' )";
            }
            if (txtkeyword.Text != "")
            {
                if (ddlkieuthanhvien.SelectedValue == "1")
                {
                    sql += " and IDThanhVienBan in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDThanhVienHuongHH in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }
            if (ddlkieu.SelectedValue != "0")
            {
                sql += " and KieuLaiSuat=" + ddlkieu.SelectedValue + "";
            }

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(ddlPage.SelectedValue);
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.ELaiSuatAGLANG> iitem = SLaiSuatAGLANG.CATEGORY_PHANTRANG1(sql);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.ELaiSuatAGLANG> dt = SLaiSuatAGLANG.CATEGORY_PHANTRANG2(sql, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                double coin = 0.0;
                for (int i = 0; i < dt.Count; i++)
                {
                    coin += Convert.ToDouble(dt[i].LaiSuat.ToString());
                }
                ltCoin.Text = "Tổng điểm: " + coin.ToString();
                foreach (var item in dt)
                {
                    SLaiSuatAGLANG.Name_Text("update LaiSuatAGLANG set MTreeHuong='" + Commond.ShowMTrees(item.IDThanhVienHuongHH.ToString()) + "' where IDThanhVienHuongHH=" + item.IDThanhVienHuongHH.ToString() + "");
                }
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
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=HoaHongAGLANGD&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&type=" + ddlkieu.SelectedValue + "", Tongsobanghi, pages);
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
        protected void btxoa_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rp_pagelist.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)rp_pagelist.Items[i].FindControl("chkid");
                HiddenField id = (HiddenField)rp_pagelist.Items[i].FindControl("hiID");
                if (chk.Checked)
                {
                    List<LaiSuatAGLANG> del = db.LaiSuatAGLANGs.Where(s => s.ID == int.Parse(id.Value)).ToList();// xóa nhiều
                    if (del != null)
                    {
                        // LichSuGiaoDich(del[0].IDSanPham.ToString(), "21", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Xóa : " + del[0].Type + " ", del[0].IDThanhVien.ToString(), del[0].IDUserNguoiDuocHuong.ToString(), del[0].PhamTramHoaHong.ToString(), del[0].SoCoin.ToString());
                        db.LaiSuatAGLANGs.DeleteAllOnSubmit(del);
                        db.SubmitChanges();
                    }
                }
            }
            this.LoadItems();
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
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + "</span></a>";
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
            MoreAll.MoreAll.Update_setting("PageHoaHong", ddlPage.SelectedValue);
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
            Response.Redirect("/admin.aspx?u=HoaHongAGLANGD&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&type=" + ddlkieu.SelectedValue + "");
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

        protected string ShowPro(string id)
        {
            string str = "";
            if (id != "0")
            {
                List<Entity.Products> dt = SProducts.GetById(id);
                if (dt.Count >= 1)
                {
                    str = "<a href=\"/" + dt[0].TangName + "_sp" + dt[0].ipid + ".html\" target=\"_blank\">" + dt[0].Name + "</a>";
                }
            }
            return str;
        }
        protected void ddlthanhvienhuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }
        void LichSuGiaoDich(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
        {
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
            db.LichSuGiaoDiches.InsertOnSubmit(obl);
            db.SubmitChanges();
            #endregion
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
            string Namefile = "Danh_Sach_Mua_AG_LAND_" + DateTime.Now;
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
            sb.Append("  <th style=\"width:200px; vertical-align:middle;\">");
            sb.Append("    <b>Sản phẩm</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:200px; vertical-align:middle;\">");
            sb.Append("    <b>Thành viên bán</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:200px; vertical-align:middle;\">");
            sb.Append("    <b>Người được hưởng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:200px; vertical-align:middle;\">");
            sb.Append("    <b>Số tiền đầu tư</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:200px; vertical-align:middle;\">");
            sb.Append("    <b>Lãi suất</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:90px; vertical-align:middle;\">");
            sb.Append("    <b>Ngày tạo</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:90px; vertical-align:middle;\">");
            sb.Append("    <b>Đơn hàng</b>");
            sb.Append("  </th>");
            sb.Append("</tr>");

            string sql = "";
            //if (Request["nam"] != null && !Request["nam"].Equals("0"))
            //{
            //    sql += " and year(NgayNhan)=" + Request["nam"] + "";
            //}
            //if (Request["thang"] != null && !Request["thang"].Equals("0"))
            //{
            //    sql += " and month(NgayNhan)=" + Request["thang"] + "";
            //}
            //if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
            //{
            //    sql += " and day(NgayNhan)=" + Request["ngay"] + "";
            //}

            if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            {
                sql += LocDate_NgayThangNam(txtNgayThangNam.Text);
            }
            else if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && !string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            {
                sql += " and ( NgayNhan>='" + MoreAll.MoreAll.LocDate(txtNgayThangNam.Text) + "' and  NgayNhan<='" + MoreAll.MoreAll.LocDate(txtDenNgayThangNam.Text) + "' )";
            }
            if (txtkeyword.Text != "")
            {
                if (ddlkieuthanhvien.SelectedValue == "1")
                {
                    sql += " and IDThanhVienBan in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDThanhVienHuongHH in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }
            if (ddlkieu.SelectedValue != "0")
            {
                sql += " and KieuLaiSuat=" + ddlkieu.SelectedValue + "";
            }
            List<Entity.ELaiSuatAGLANG> dt = SLaiSuatAGLANG.Name_Text("SELECT *  FROM  LaiSuatAGLANG  where 1=1 " + sql + " ORDER BY NgayNhan DESC");
            if (dt.Count >= 1)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\" vertical-align:middle;text-align: center;\">" + ShowPro(item.IDSanPham.ToString()) + "</td>");
                    sb.Append("    <td style=\" vertical-align:middle; text-align: center;\">" + ShowtThanhVien(item.IDThanhVienBan.ToString()) + "</td>");
                    sb.Append("    <td style=\" vertical-align:middle; text-align: center;\">" + ShowtThanhVien(item.IDThanhVienHuongHH.ToString()) + "</td>");
                    sb.Append("    <td style=\" vertical-align:middle;text-align: center;\">" + AllQuery.MorePro.FormatMoney_NO(item.SoTienDauTu.ToString()) + " /  VND</td>");
                    sb.Append("    <td style=\" vertical-align:middle;text-align: center;\">" + item.LaiSuat + " / điểm</td>");
                    sb.Append("    <td style=\" vertical-align:middle; text-align: center;\">" + item.NgayThamGia.ToString() + "</td>");
                    sb.Append("    <td style=\" vertical-align:middle; text-align: center;\">" + item.IDCart.ToString() + "</td>");
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