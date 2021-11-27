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
    public partial class List_HoaHong_GioiThieu : System.Web.UI.UserControl
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
            else
            {
                sql += " and IDType in (1,2,3,5,31)";
            }
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
                    // if (table[i].TrangThai == 1)
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
            Response.Redirect("/tich-diem-hoa-hong-gioi-thieu.html?ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "&type=" + ddlkieu.SelectedValue + "");
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
                return "Hoa Hồng Quản Lý";
            }
            else if (enable.Trim().Equals("2"))
            {
                return "Hoa Hồng Quản Lý Leader";
            }
            else if (enable.Trim().Equals("3"))
            {
                return "Hoa Hồng Quản Lý ...";
            }
            else if (enable.Trim().Equals("5"))
            {
                return "Hoa Hồng Quản Lý Chi Nhánh";
            }
            else if (enable.Trim().Equals("31"))
            {
                return "Hoa Hồng (Hỗ Trợ)";
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
                    str = "<a href=\"/" + dt[0].TangName + ".html\" target=\"_blank\">" + dt[0].Name + "</a>";
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
            else
            {
                sql += " and IDType in (1,2,3,5,31)";
            }
            List<EHoaHongThanhVien> dt = SHoaHongThanhVien.Name_Text("SELECT * from HoaHongThanhVien where 1=1 " + sql + " order by NgayTao desc");

            string Namefile = "HoaHong_AFFILIATE" + DateTime.Now;
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
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Trạng thái</b>");
            sb.Append("  </th>");
            sb.Append("</tr>");
            if (dt.Count > 0)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + ShowTrangThai(item.IDType.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + ShowtThanhVien(item.IDThanhVien.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: left;\">" + ShowtThanhVien(item.IDUserNguoiDuocHuong.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.PhamTramHoaHong + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle; text-align: left;\">" + item.SoCoin + " điểm</td>");
                    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">" + MoreAll.FormatDateTime.FormatDateFull(item.NgayTao) + "</td>");
                    if (item.TrangThai.ToString() == "1")
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Đã duyệt</td>");
                    }
                    else
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Chưa duyệt</td>");
                    }
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