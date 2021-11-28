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
    public partial class MDauTuBatDongSan : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        DateTime fDate, tDate;
        private string IDMaDonTao = "1";
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

                List<Entity.users> dt = Susers.Name_Text("select * from users where (vphone ='" + Commond.Setting("NguoiBan") + "')");
                if (dt.Count >= 1)
                {
                    hdThanhVienBan.Value = dt[0].iuser_id.ToString();
                    ltthongtin1.Text = dt[0].vfname.ToString() + " - " + dt[0].vuserun.ToString();
                }
                if (!MoreAll.Other.Giatri("PageDautu").Equals(""))
                {
                    ddlPage.SelectedValue = MoreAll.Other.Giatri("PageDautu");
                }
                //this.DropDownList3.Items.Clear();
                //this.DropDownList3.Items.Insert(0, new ListItem("Tất cả các năm", "0"));
                //for (int i = 2018; i < (DateTime.Now.Year + 1); i++)
                //{
                //    this.DropDownList3.Items.Add(new ListItem("Năm " + i.ToString(), i.ToString()));
                //}
                ////WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.DropDownList3, "0");
                //WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.DropDownList3, DateTime.Now.Year.ToString());
                if (Request["kw"] != null && !Request["kw"].Equals(""))
                {
                    txtkeyword.Text = Request["kw"];
                }

                if (Request["Tu"] != null && !Request["Tu"].Equals(""))
                {
                    txtNgayThangNam.Text = Request["Tu"];
                }
                if (Request["Den"] != null && !Request["Den"].Equals(""))
                {
                    txtDenNgayThangNam.Text = Request["Den"];
                }

                if (Request["trangthai"] != null && !Request["trangthai"].Equals(""))
                {
                    ddltrangthai.SelectedValue = Request["trangthai"];
                }
                this.LoadItems();
            }
        }
        public static string LocDate_NgayThangNam(string date)
        {
            return " and ( day(NgayTao)=" + Convert.ToDateTime(date).ToString("dd") + " and MONTH(NgayTao)=" + Convert.ToDateTime(date).ToString("MM") + "  and  year(NgayTao)=" + Convert.ToDateTime(date).ToString("yyyy") + " )";
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
                sql += " and IDThanhVien in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
            }
            if (Request["trangthai"] != null && !Request["trangthai"].Equals("-1"))
            {
                sql += " and TrangThai=" + Request["trangthai"] + "";
            }

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse("20");
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<DauTuBatDongSan> iitem = db.ExecuteQuery<DauTuBatDongSan>(@"SELECT * FROM DauTuBatDongSan where 1=1 " + sql + " order by NgayTao desc").ToList();
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            int PageIndex = (pages - 1);
            int Tongpage = Tongsotrang;
            int StartRecord = PageIndex * Tongpage;
            int EndRecord = StartRecord + Tongpage + 1;
            List<DauTuBatDongSan> dt = db.ExecuteQuery<DauTuBatDongSan>(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY NgayTao DESC) AS rowindex ,*  FROM  DauTuBatDongSan  where 1=1 " + sql + " ) AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")").ToList();
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
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=Datu&kw=" + txtkeyword.Text + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&trangthai=" + ddltrangthai.SelectedValue + "", Tongsobanghi, pages);
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

        private void btn_link_cancel_Click(object sender, EventArgs e)
        {
            this.pn_list.Visible = true;
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
                //case "Delete":
                //    LichSuRutTien del = db.LichSuRutTiens.Where(s => s.ID == int.Parse(str2)).FirstOrDefault();// xóa 1
                //    if (del != null)
                //    {
                //        if (del.TrangThai == 0)
                //        {

                //        }
                //        LichSuGiaoDich("0", "22", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Xóa : Rút tiền ", del.IDThanhVien.ToString(), del.IDThanhVien.ToString(), "0", del.SoCoin.ToString());
                //        db.LichSuRutTiens.DeleteOnSubmit(del);
                //        db.SubmitChanges();
                //        this.LoadItems();
                //        this.ltmsg.Text = "";
                //    }
                //    return;
                case "Huy":
                    DauTuBatDongSan abc = db.DauTuBatDongSans.SingleOrDefault(p => p.ID == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (abc != null)
                    {
                        abc.TrangThai = 2;
                        abc.NgayDuyet = DateTime.Now.ToString();
                        abc.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                        db.SubmitChanges();
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                    }
                    return;
                case "ThanhToan":

                    DauTuBatDongSan abcs = db.DauTuBatDongSans.SingleOrDefault(p => p.ID == int.Parse(e.CommandArgument.ToString().Trim()));
                    abcs.TrangThai = 1;
                    abcs.NgayDuyet = DateTime.Now.ToString();
                    abcs.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                    db.SubmitChanges();

                    IDMaDonTao = MoreAll.MoreAll.FormatDate_IDQR(DateTime.Now);
                    if (abcs.IDThanhVien.ToString() != "0" && hdThanhVienBan.Value != "0")
                    {
                        Double TongTien = Convert.ToDouble(abcs.TongTienDauTu) / 1000;
                        SinhHHBatDongSan.HoaHongF(abcs.IDThanhVien.ToString(), hdThanhVienBan.Value, IDMaDonTao, TongTien.ToString());
                        this.LoadItems();
                        Response.Write("<script type=\"text/javascript\">alert('Duyệt thành công với mã đơn hàng #" + IDMaDonTao + ".');window.location.href='" + Request.RawUrl.ToString() + "'; </script>");
                    }
                    else
                    {
                        ltmsg.Text = "Vui lòng kiểm tra người bán ở cấu hình bất động sản, Vui lòng điền số điện thoại người bán";
                    }

                    return;
            }
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
                        List<DauTuBatDongSan> del = db.DauTuBatDongSans.Where(s => s.ID == int.Parse(id.Value)).ToList();// xóa nhiều
                        if (del != null)
                        {
                            // LichSuGiaoDich("0", "22", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Xóa : Rút tiền ", del[0].IDThanhVien.ToString(), del[0].IDThanhVien.ToString(), "0", del[0].SoCoin.ToString());
                            db.DauTuBatDongSans.DeleteAllOnSubmit(del);
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
                str += "</span>";
            }
            else
            {
                str = "Không tìm thấy thành viên";
            }
            return str;
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Update_setting("PageDautu", ddlPage.SelectedValue);
            Show();
        }
        public string Update_setting(string str, string Value)
        {
            Entity.Setting obj = new Entity.Setting();
            obj.Lang = "VIE";
            obj.Properties = str;
            obj.Value = Value.ToString();
            SSetting.UPDATE(obj);
            return "";
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
            Response.Redirect("/admin.aspx?u=Dautu&kw=" + txtkeyword.Text + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&trangthai=" + ddltrangthai.SelectedValue + "");
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
        protected bool EnableUnLock(string status)
        {
            return status.Equals("0");
        }
        protected bool EnableLock(string status)
        {
            return status.Equals("0");
        }
        protected void Lock_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn hủy thanh toán?')";
        }
        protected void Duyet_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn duyệt và muốn sinh hoa hồng bất động sản ?.')";
        }
        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xóa giao dịch này ? ')";//.Hệ thống hoàn lại số tiền cho thành viên? Nếu giao dịch chưa thành công.
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

        protected void lnkxuatExel_Click(object sender, EventArgs e)
        {
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

            if (txtkeyword.Text != "")
            {
                sql += " and IDThanhVien in (" + SearchThanhVien(txtkeyword.Text.Trim().Replace("&nbsp;", "")) + ") ";
            }
            if (Request["trangthai"] != null && !Request["trangthai"].Equals("-1"))
            {
                sql += " and TrangThai=" + Request["trangthai"] + "";
            }

            string Namefile = "DauTu" + DateTime.Now;
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
            sb.Append("    <b>Họ và tên thành viên</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:400px; vertical-align:middle;\">");
            sb.Append("    <b>Địa chỉ thành viên</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Điện thoại thành viên</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Email thành viên</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Số tiền cần rút</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Số điểm còn lại</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Tên ngân hàng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Họ và tên chủ thẻ</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Số tài khoản</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Chi nhánh</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Nội dung chuyển tiền</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Ghi chú</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Trạng thái</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Ngày tạo</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Ngày duyệt</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Người duyệt</b>");
            sb.Append("  </th>");
            sb.Append("</tr>");

            List<Entity.ELichSuRutTien> dt = SLichSuRutTien.Exel(sql);
            // List<Entity.ELichSuRutTien> dt = SLichSuRutTien.Name_Text("SELECT * FROM LichSuRutTien order by NgayDuyet desc ");
            if (dt.Count > 0)
            {
                int i = 1;
                foreach (var item in dt)
                {

                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    List<Entity.users> dtv = Susers.Name_Text("SELECT * FROM users  where iuser_id=" + item.IDThanhVien + "");
                    if (dtv.Count > 0)
                    {
                        sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + dtv[0].vfname + "</td>");
                        sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + dtv[0].vaddress + "</td>");
                        sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + dtv[0].vphone + "</td>");
                        sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + dtv[0].vemail + "</td>");
                    }

                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + item.SoTienCanRut + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: left;\">" + item.SoCoin + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.TenNganHang + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.HoVaTen + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.SoTaiKHoan + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.ChiNhanh + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.NoiDungChuyenTien + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.GhiChu + "</td>");
                    if (item.TrangThai == 0)
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Chưa duyệt</td>");
                    }
                    else
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Đã duyệt</td>");
                    }
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.NgayTao + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.NgayDuyet + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.NguoiDuyet + "</td>");
                    sb.Append("  </tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void txtNgayThangNam_TextChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void txtDenNgayThangNam_TextChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void ddltrangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }
    }
}