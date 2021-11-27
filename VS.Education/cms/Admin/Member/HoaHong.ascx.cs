﻿using System;
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
    public partial class HoaHong : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        string IDDonHang = "0";
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
                if (!Commond.Setting("PageHoaHong").Equals(""))
                {
                    ddlPage.SelectedValue = Commond.Setting("PageHoaHong");
                }

                if (Request["IDDonHang"] != null && !Request["IDDonHang"].Equals(""))
                {
                    IDDonHang = Request["IDDonHang"];
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
                if (Request["type"] != null && !Request["type"].Equals(""))
                {
                    ddlkieu.SelectedValue = Request["type"];
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
                    HoaHongThanhVien del = db.HoaHongThanhViens.Where(s => s.ID == int.Parse(str2)).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        LichSuGiaoDich(del.IDProducts.ToString(), "21", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Xóa : " + del.Type + " ", del.IDThanhVien.ToString(), del.IDUserNguoiDuocHuong.ToString(), del.PhamTramHoaHong.ToString(), del.SoCoin.ToString());
                        db.HoaHongThanhViens.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        this.LoadItems();
                        this.ltmsg.Text = "";
                    }
                    break;
            }
        }
        //private void LoadItems()
        //{
        //    string sql = "";
        //    sql += " and year(NgayTao)=" + DropDownList3.SelectedValue + "";
        //    if (Request["thang"] != null && !Request["thang"].Equals("0"))
        //    {
        //        sql += " and month(NgayTao)=" + Request["thang"] + "";
        //    }
        //    if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
        //    {
        //        sql += " and day(NgayTao)=" + Request["ngay"] + "";
        //    }
        //    if (ddlthanhvien.SelectedValue != "0")
        //    {
        //        sql += " and IDThanhVien=" + ddlthanhvien.SelectedValue + "";
        //    }
        //    if (ddlthanhvienhuong.SelectedValue != "0")
        //    {
        //        sql += " and IDUserNguoiDuocHuong=" + ddlthanhvienhuong.SelectedValue + "";
        //    }
        //    if (ddlkieu.SelectedValue != "0")
        //    {
        //        sql += " and IDType=" + ddlkieu.SelectedValue + "";
        //    }
        //    List<EHoaHongThanhVien> table = SHoaHongThanhVien.Name_Text("SELECT * from HoaHongThanhVien where 1=1 " + sql + "  order by NgayTao desc");

        //    CollectionPager1.DataSource = table;
        //    CollectionPager1.BindToControl = rp_pagelist;
        //    CollectionPager1.MaxPages = 10000;
        //    CollectionPager1.PageSize = int.Parse(ddlPage.SelectedValue);
        //    rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
        //    rp_pagelist.DataBind();
        //    if (!ddlthanhvien.SelectedValue.Equals("0"))
        //    {
        //        double num = 0.0;
        //        double coin = 0.0;
        //        for (int i = 0; i < table.Count; i++)
        //        {
        //            if (table[i].TrangThai == 1)
        //            {
        //                coin += Convert.ToDouble(table[i].SoCoin.ToString());
        //            }
        //        }
        //        ltCoin.Text = "Tổng điểm: " + coin.ToString();
        //    }
        //}
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
                    sql += " and IDThanhVien in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDUserNguoiDuocHuong in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }
            if (ddlkieu.SelectedValue != "0")
            {
                sql += " and IDType in (" + ddlkieu.SelectedValue + ")";
            }
            sql += " and IDType !=32";

            if (IDDonHang != "0")
            {
                sql += " and IDCart =" + IDDonHang + " ";//  and IDType!=30
            }

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(ddlPage.SelectedValue);
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.EHoaHongThanhVien> iitem = SHoaHongThanhVien.CATEGORY_PHANTRANG1(sql);
            if (iitem.Count() > 0)
            {
                double coin = 0.0;
                for (int i = 0; i < iitem.Count; i++)
                {
                    coin += Convert.ToDouble(iitem[i].SoCoin.ToString());
                }
                lttongtien.Text = coin.ToString();
                Tongsobanghi = iitem.Count();
            }
            List<Entity.EHoaHongThanhVien> dt = SHoaHongThanhVien.CATEGORY_PHANTRANG2(sql, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                double num = 0.0;
                double coin = 0.0;
                for (int i = 0; i < dt.Count; i++)
                {
                    coin += Convert.ToDouble(dt[i].SoCoin.ToString());
                }
                ltCoin.Text = coin.ToString();

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
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=HoaHong&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&type=" + ddlkieu.SelectedValue + "", Tongsobanghi, pages);
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
                    List<HoaHongThanhVien> del = db.HoaHongThanhViens.Where(s => s.ID == int.Parse(id.Value)).ToList();// xóa nhiều
                    if (del != null)
                    {
                        LichSuGiaoDich(del[0].IDProducts.ToString(), "21", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Xóa : " + del[0].Type + " ", del[0].IDThanhVien.ToString(), del[0].IDUserNguoiDuocHuong.ToString(), del[0].PhamTramHoaHong.ToString(), del[0].SoCoin.ToString());
                        db.HoaHongThanhViens.DeleteAllOnSubmit(del);
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
            Response.Redirect("/admin.aspx?u=HoaHong&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&type=" + ddlkieu.SelectedValue + "");
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
            string Namefile = "Lịch sử Hoa Hồng _ " + DateTime.Now;
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
            sb.Append("    <b>Loại lịch sử</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:520px; vertical-align:middle;\">");
            sb.Append("    <b>Mã đơn hàng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Người tham gia Đăng ký/<br />Người Mua Hàng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Người được hưởng giới thiệu/<br />Nhà cung cấp</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:40px; vertical-align:middle;\">");
            sb.Append("    <b>% Hoa Hồng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Số điểm</b>");
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
                    sql += " and IDThanhVien in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and IDUserNguoiDuocHuong in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
                }
            }
            if (ddlkieu.SelectedValue != "0")
            {
                sql += " and IDType in (" + ddlkieu.SelectedValue + ")";
            }
            sql += " and IDType !=32";

            List<Entity.EHoaHongThanhVien> dt = SHoaHongThanhVien.Name_Text("SELECT *  FROM  HoaHongThanhVien  where 1=1 " + sql + " ORDER BY NgayTao DESC");
            if (dt.Count >= 1)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.Type + "<br>" + ShowPro(item.IDProducts.ToString(), item.NoiDung.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\"><a target=\"_blank\" href=\"/admin.aspx?u=cartsNhanh&Code=" + item.IDCart.ToString() + "\">" + item.IDCart.ToString() + "</a></td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + ShowtThanhVien(item.IDThanhVien.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + ShowtThanhVien(item.IDUserNguoiDuocHuong.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: left;\">" + item.PhamTramHoaHong + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.SoCoin + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.NgayTao + "</td>");
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