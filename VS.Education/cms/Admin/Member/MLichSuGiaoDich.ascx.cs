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
    public partial class MLichSuGiaoDich : System.Web.UI.UserControl
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
                if (Request["kieu"] != null && !Request["kieu"].Equals(""))
                {
                    ddlkieuthanhvien.SelectedValue = Request["kieu"];
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
                    LichSuGiaoDich del = db.LichSuGiaoDiches.Where(s => s.ID == int.Parse(str2)).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        db.LichSuGiaoDiches.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        this.LoadItems();
                        this.ltmsg.Text = "";
                    }
                    return;
            }
        }
        #region Menu
        #endregion

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
        //    List<ELichSuGiaoDich> table = SLichSuGiaoDich.Name_Text("SELECT * from LichSuGiaoDich where 1=1 " + sql + "  order by NgayTao desc");

        //    CollectionPager1.DataSource = table;
        //    CollectionPager1.BindToControl = rp_pagelist;
        //    CollectionPager1.MaxPages = 10000;
        //    CollectionPager1.PageSize = int.Parse(ddlPage.SelectedValue);
        //    rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
        //    rp_pagelist.DataBind();
        //    //if (!ddlthanhvien.SelectedValue.Equals("0"))
        //    //{
        //    //    double num = 0.0;
        //    //    double coin = 0.0;
        //    //    for (int i = 0; i < table.Count; i++)
        //    //    {
        //    //        if (table[i].TrangThai == 1)
        //    //        {
        //    //            num += Convert.ToDouble(table[i].SoTienVND.ToString());
        //    //            coin += Convert.ToDouble(table[i].SoCoin.ToString());
        //    //        }
        //    //    }
        //    //    lttongtien.Text = "Tổng tiền: " + AllQuery.MorePro.FormatMoney_VND(num.ToString());
        //    //    lttongtienbangchu.Text = MoreAll.Hamdoisorachu.So_chu(Convert.ToDouble(num.ToString()));
        //    //    ltCoin.Text = "Tổng Coin: " + coin.ToString();
        //    //}
        //}
        public void LoadItems()
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
                sql += " and IDType=" + ddlkieu.SelectedValue + "";
            }

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(ddlPage.SelectedValue);
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.ELichSuGiaoDich> iitem = SLichSuGiaoDich.CATEGORY_PHANTRANG1(sql);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.ELichSuGiaoDich> dt = SLichSuGiaoDich.CATEGORY_PHANTRANG2(sql, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                double coin = 0.0;
                for (int i = 0; i < dt.Count; i++)
                {
                    coin += Convert.ToDouble(dt[i].SoCoin.ToString());
                }
                ltCoin.Text = "Tổng điểm: " + coin.ToString();


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
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=LichSuDG&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "&type=" + ddlkieu.SelectedValue + "", Tongsobanghi, pages);
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
            try
            {
                for (int i = 0; i < rp_pagelist.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rp_pagelist.Items[i].FindControl("chkid");
                    HiddenField id = (HiddenField)rp_pagelist.Items[i].FindControl("hiID");
                    if (chk.Checked)
                    {
                        List<LichSuGiaoDich> del = db.LichSuGiaoDiches.Where(s => s.ID == int.Parse(id.Value)).ToList();// xóa nhiều
                        if (del != null)
                        {
                            db.LichSuGiaoDiches.DeleteAllOnSubmit(del);
                            db.SubmitChanges();
                        }
                    }
                }
                LoadItems();
            }
            catch (Exception)
            {

            }
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
            Response.Redirect("/admin.aspx?u=LichSuDG&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "&type=" + ddlkieu.SelectedValue + "");
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
                str = "Không tìm thấy thành viên";
            }
            return str;
        }
        protected string ShowInfo(string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong)
        {
            string str = "";
            if (IDType == "16")
            {
                str = "<span class=\"BodderXanhs\"> Admin cộng điểm cho :" + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "17")
            {
                str = "<span class=\"BodderXanhs\"> Admin:(admin) Nạp tiền đăng ký thành viên 480 ngàn cho thành viên :" + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "18")
            {
                str = "<span class=\"BodderXanhs\"> Thành Viên " + ShowtThanhVienType(IDUserNguoiDuocHuong) + " - Kích hoạt Nạp tiền đăng ký thành viên 480 Coin </span><br />";
            }
            else if (IDType == "19")
            {
                str = "<span class=\"BodderXanhs\"> Thành Viên " + ShowtThanhVienType(IDUserNguoiDuocHuong) + " - Rút tiền </span><br />";
            }
            else if (IDType == "20")
            {
                str = "<span class=\"BodderXanhs\"> Admin: Xóa Cấp điểm cho thành viên:" + ShowtThanhVienType(IDUserNguoiDuocHuong) + " - Rút tiền </span><br />";
            }
            else if (IDType == "21")
            {
                str = "<span class=\"BodderXanhs\">" + Type + " <br />Của thành viên: " + ShowtThanhVienType(IDUserNguoiDuocHuong) + " </span><br />";
            }
            else if (IDType == "22")
            {
                str = "<span class=\"BodderXanhs\"> Admin: Xóa : Rút tiền  <br />Của thành viên: " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "14")
            {
                str = "<span class=\"BodderXanhs\">Chuyển điểm Từ thành viên : " + ShowtThanhVienType(IDThanhVien) + "<br /> Cho thành viên : " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "15")
            {
                str = "<span class=\"BodderXanhs\">Cộng điểm Từ thành viên: " + ShowtThanhVienType(IDThanhVien) + "<br /> Cho thành viên : " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "1" || IDType == "2" || IDType == "3" || IDType == "5")
            {
                str = "<span class=\"BodderXanhs\">Người tham gia Đăng ký " + ShowtThanhVienType(IDThanhVien) + "<br /> Người được hưởng giới thiệu: " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "6" || IDType == "7" || IDType == "71" || IDType == "72" || IDType == "73" || IDType == "74" || IDType == "75" || IDType == "76" || IDType == "77" || IDType == "78" || IDType == "79" || IDType == "8" || IDType == "9")
            {
                str = "<span class=\"BodderXanhs\">Người tham gia Mua Hàng" + ShowtThanhVienType(IDThanhVien) + "<br /> Người được hưởng : " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "10" || IDType == "11" || IDType == "12" || IDType == "13")
            {
                str = "<span class=\"BodderXanhs\">Người tham gia Mua Hàng" + ShowtThanhVienType(IDThanhVien) + "<br /> Nhà cung cấp : " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "30")
            {
                str = "<span class=\"BodderXanhs\">Người Mua Hàng" + ShowtThanhVienType(IDThanhVien) + "<br /> Nhà cung cấp : " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "31")
            {
                str = "<span class=\"BodderXanhs\">Người Mua Hàng" + ShowtThanhVienType(IDThanhVien) + "<br /> Nhà cung cấp : " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "32")
            {
                str = "<span class=\"BodderXanhs\">Người tham gia" + ShowtThanhVienType(IDThanhVien) + "<br /> Người được hưởng : " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "23")
            {
                str = "<span class=\"BodderXanhs\">Thành viên mua điểm: " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            else if (IDType == "80")
            {
                str = "<span class=\"BodderXanhs\">Người Bán : " + ShowtThanhVienType(IDThanhVien) + "<br /> Người hưởng lãi suất : " + ShowtThanhVienType(IDUserNguoiDuocHuong) + "</span><br />";
            }
            return str;
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            Show();
        }

        protected void lnkxuatExel_Click(object sender, EventArgs e)
        {
            string Namefile = "Lịch sử giao dịch _ " + DateTime.Now;
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
                sql += " and IDType=" + ddlkieu.SelectedValue + "";
            }
            List<Entity.ELichSuGiaoDich> dt = SLichSuGiaoDich.Name_Text("SELECT * FROM  LichSuGiaoDich  where 1=1 " + sql + " ORDER BY NgayTao DESC");
            if (dt.Count() > 0)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.Type + "<br>" + ShowPro(item.IDProducts.ToString(), item.NoiDung.ToString()) + "</td>");
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