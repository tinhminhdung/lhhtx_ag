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
    public partial class MLichSuQRCode : System.Web.UI.UserControl
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
            this.Page.Form.DefaultButton = lnksearch.UniqueID;
            if (!base.IsPostBack)
            {
                #region UpdatePanel
                #endregion
                if (!Commond.Setting("PageHoaHong").Equals(""))
                {
                    ddlPage.SelectedValue = Commond.Setting("PageHoaHong");
                }
                this.DropDownList3.Items.Clear();
                this.DropDownList3.Items.Insert(0, new ListItem("Tất cả các năm", "0"));
                for (int i = 2018; i < (DateTime.Now.Year + 1); i++)
                {
                    this.DropDownList3.Items.Add(new ListItem("Năm " + i.ToString(), i.ToString()));
                }
                //WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.DropDownList3, "0");
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.DropDownList3, DateTime.Now.Year.ToString());
                if (Request["kw"] != null && !Request["kw"].Equals(""))
                {
                    txtkeyword.Text = Request["kw"];
                }
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
        //protected void rp_pagelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    string str = e.CommandName.Trim();
        //    string str2 = e.CommandArgument.ToString().Trim();
        //    string str4 = str;

        //    switch (e.CommandName)
        //    {
        //        case "Delete":
        //            LichSuGiaoDich del = db.LichSuGiaoDiches.Where(s => s.ID == int.Parse(str2)).FirstOrDefault();// xóa 1
        //            if (del != null)
        //            {
        //                db.LichSuGiaoDiches.DeleteOnSubmit(del);
        //                db.SubmitChanges();
        //                this.LoadItems();
        //                this.ltmsg.Text = "";
        //            }
        //            return;
        //    }
        //}

        public void LoadItems()
        {
            string sql = "";
            if (Request["nam"] != null && !Request["nam"].Equals("0"))
            {
                sql += " and year(NgayDuyet)=" + Request["nam"] + "";
            }
            if (Request["thang"] != null && !Request["thang"].Equals("0"))
            {
                sql += " and month(NgayDuyet)=" + Request["thang"] + "";
            }
            if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
            {
                sql += " and day(NgayDuyet)=" + Request["ngay"] + "";
            }
            if (txtkeyword.Text != "")
            {
                sql += " and IDThanhVien in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
            }
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(ddlPage.SelectedValue);
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.ELichSuQRCode> iitem = SLichSuQRCode.CATEGORY_PHANTRANG1(sql);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.ELichSuQRCode> dt = SLichSuQRCode.CATEGORY_PHANTRANG2(sql, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
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
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=LichSuQRcode&kw=" + txtkeyword.Text + "&ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "", Tongsobanghi, pages);
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
        //protected void btxoa_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        for (int i = 0; i < rp_pagelist.Items.Count; i++)
        //        {
        //            CheckBox chk = (CheckBox)rp_pagelist.Items[i].FindControl("chkid");
        //            HiddenField id = (HiddenField)rp_pagelist.Items[i].FindControl("hiID");
        //            if (chk.Checked)
        //            {
        //                List<LichSuGiaoDich> del = db.LichSuGiaoDiches.Where(s => s.ID == int.Parse(id.Value)).ToList();// xóa nhiều
        //                if (del != null)
        //                {
        //                    db.LichSuGiaoDiches.DeleteAllOnSubmit(del);
        //                    db.SubmitChanges();
        //                }
        //            }
        //        }
        //        LoadItems();
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}


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
            Response.Redirect("/admin.aspx?u=LichSuQRcode&kw=" + txtkeyword.Text + "&ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "");
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
        protected void ddlthanhvienhuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
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
                str = "<span style='color: #fff; background: #d62c9a; text-transform: uppercase; border-radius: 3px; padding: 5px;'>Cấu hình áp dụng tất cả thành viên</span>";
            }
            return str;
        }
        protected string ShowtThanhVienType(string id)
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
                str += "</span> - ";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone;
                }
            }
            else
            {
                str = "<span style='color: #fff; background: #d62c9a; text-transform: uppercase; border-radius: 3px; padding: 5px;'>Cấu hình áp dụng tất cả thành viên</span>";
            }
            return str;
        }
       
        protected void lnksearch_Click(object sender, EventArgs e)
        {
            Show();
        }

        //protected void lnkxuatExel_Click(object sender, EventArgs e)
        //{
        //    string Namefile = "Lịch sử giao dịch _ " + DateTime.Now;
        //    Response.Clear();
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.AddHeader("content-disposition", "attachment;filename=" + Namefile + ".xls");
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application/ms-excel";
        //    Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    Response.Charset = "utf-8";
        //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        //    Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        //    sb.Append("<table border='1' bgcolor='#ffffff' bordercolor='#dedede' cellspacing='0' cellpadding='0' style='font-size:12px; font-family:Arial; background:white;'>");
        //    sb.Append("<tr>");
        //    sb.Append("  <th style=\"width:50px; vertical-align:middle; height: 22px;\">");
        //    sb.Append("    <b>STT</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:520px; vertical-align:middle;\">");
        //    sb.Append("    <b>Loại lịch sử</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
        //    sb.Append("    <b>Người tham gia Đăng ký/<br />Người Mua Hàng</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
        //    sb.Append("    <b>Người được hưởng giới thiệu/<br />Nhà cung cấp</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:40px; vertical-align:middle;\">");
        //    sb.Append("    <b>% Hoa Hồng</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
        //    sb.Append("    <b>Số điểm</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:90px; vertical-align:middle;\">");
        //    sb.Append("    <b>Ngày giao dịch</b>");
        //    sb.Append("  </th>");
        //    sb.Append("</tr>");

        //    string sql = "";
        //    if (Request["nam"] != null && !Request["nam"].Equals("0"))
        //    {
        //        sql += " and year(NgayTao)=" + Request["nam"] + "";
        //    }
        //    if (Request["thang"] != null && !Request["thang"].Equals("0"))
        //    {
        //        sql += " and month(NgayTao)=" + Request["thang"] + "";
        //    }
        //    if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
        //    {
        //        sql += " and day(NgayTao)=" + Request["ngay"] + "";
        //    }
        //    if (txtkeyword.Text != "")
        //    {
        //        if (ddlkieuthanhvien.SelectedValue == "1")
        //        {
        //            sql += " and IDThanhVien in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
        //        }
        //        else if (ddlkieuthanhvien.SelectedValue == "2")
        //        {
        //            sql += " and IDUserNguoiDuocHuong in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
        //        }
        //    }
        //    if (ddlkieu.SelectedValue != "0")
        //    {
        //        sql += " and IDType=" + ddlkieu.SelectedValue + "";
        //    }
        //    List<Entity.ELichSuGiaoDich> dt = SLichSuGiaoDich.Name_Text("SELECT * FROM  LichSuGiaoDich  where 1=1 " + sql + " ORDER BY NgayTao DESC");
        //    if (dt.Count() > 0)
        //    {
        //        int i = 1;
        //        foreach (var item in dt)
        //        {
        //            sb.Append("<tr>");
        //            sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
        //            sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.Type + "<br>" + ShowPro(item.IDProducts.ToString()) + "</td>");
        //            sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + ShowtThanhVien(item.IDThanhVien.ToString()) + "</td>");
        //            sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + ShowtThanhVien(item.IDUserNguoiDuocHuong.ToString()) + "</td>");
        //            sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: left;\">" + item.PhamTramHoaHong + "</td>");
        //            sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.SoCoin + "</td>");
        //            sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.NgayTao + "</td>");
        //            sb.Append("</tr>");
        //        }
        //    }

        //    sb.Append("</table>");
        //    Response.Write(sb.ToString());
        //    Response.Flush();
        //    Response.End();
        //}

    }
}