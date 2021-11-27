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
using System.Configuration;
using TestWindowService;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class ServiceLaiSuatAGLAND : System.Web.UI.UserControl
    {
        string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
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
                if (Request["uutien"] != null && !Request["uutien"].Equals(""))
                {
                    ddluutien.SelectedValue = Request["uutien"];
                }
                if (Request["leader"] != null && !Request["leader"].Equals(""))
                {
                    ddlleader.SelectedValue = Request["leader"];
                }
                ShowLeader();
                this.LoadItems();
            }
        }
        protected bool Enable(string ID)
        {
            List<LichSuThayDoiGoiLand> table = db.LichSuThayDoiGoiLands.Where(s => s.IDService == Convert.ToInt32(ID)).OrderByDescending(s => s.ID).ToList();
            if (table.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
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
        private void btn_link_cancel_Click(object sender, EventArgs e)
        {
            this.pn_list.Visible = true;
        }
        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Lưu ý: Bạn đang xóa Service , nếu xóa đi thì các ngày sau sẽ không thể rơi hoa hồng AG LAND được nữa ?')";
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
                case "EditDetail":
                    Service_LaiSuatAGLANG table = db.Service_LaiSuatAGLANGs.Where(s => s.ID == int.Parse(str2)).FirstOrDefault();
                    if (table != null)
                    {
                        this.pn_list.Visible = false;
                        this.pn_insert.Visible = true;
                        this.hd_insertupdate.Value = "update";
                        this.hd_page_edit_id.Value = str2.Trim();
                        hdIDHuong.Value = table.IDThanhVienHuongHH.ToString().Trim();
                        hdidservice.Value = table.ID.ToString().Trim();
                        //this.txtTenthanhvien.Text = table.IDThanhVienHuongHH.ToString().Trim();
                        ltthongtin.Text = SearchThanhVienID(table.IDThanhVienHuongHH.ToString());
                    }
                    return;

                case "Delete":
                    Service_LaiSuatAGLANG del = db.Service_LaiSuatAGLANGs.Where(s => s.ID == int.Parse(str2)).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        //if (LogFile == "true")
                        //{
                        //    Library.WriteErrorLogAGLANG("Xóa AG Aland: " + str2 + " - IDThanhVienBan: " + del.IDThanhVienBan + "- IDThanhVienHuongHH: " + del.IDThanhVienHuongHH + "- IDThanhVienHuongHH: " + del.LaiSuat + " -  Người Xóa: " + MoreAll.MoreAll.GetCookies("UName").ToString());
                        //}

                        db.Service_LaiSuatAGLANGs.DeleteOnSubmit(del);
                        db.SubmitChanges();

                        this.LoadItems();
                        this.ltmsg.Text = "";
                    }
                    return;
            }
        }
        protected void txtTenthanhvien_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            ltthongtin.Text = SSearchThanhVien(Tieude.Text.Trim().Replace("&nbsp;", ""));
        }
        protected string SSearchThanhVien(string keyword)
        {
            List<Entity.users> dt = Susers.Name_Text("select * from users where (vuserun ='" + keyword + "')");
            if (dt.Count >= 1)
            {
                hdidthanhvien.Value = dt[0].iuser_id.ToString();
                return "(" + dt[0].vuserun.ToString() + ") - " + dt[0].vfname.ToString() + " - " + dt[0].vphone.ToString() + " - " + dt[0].vemail.ToString();
            }
            return "";
        }
        protected string SearchThanhVienID(string id)
        {
            List<Entity.users> dt = Susers.Name_Text("select * from users where iuser_id=" + id + " ");
            if (dt.Count >= 1)
            {
                txtTenthanhvien.Text = dt[0].vuserun.ToString();
                hdidthanhvien.Value = dt[0].iuser_id.ToString();
                return "(" + dt[0].vuserun.ToString() + ") - " + dt[0].vfname.ToString() + " - " + dt[0].vphone.ToString() + " - " + dt[0].vemail.ToString();
            }
            return "";
        }
        protected void btn_InsertUpdate_Click(object sender, EventArgs e)
        {
            if (this.txtTenthanhvien.Text.Trim().Length < 1)
            {
                this.lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn";
            }
            else if (hdidthanhvien.Value.Trim().Length < 1)
            {
                this.lblmsg.Text = "Tên thành viên chưa đúng.";
            }
            else
            {
                string sgrnlevel = hidLevel.Value;
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();

                string str5 = this.hd_insertupdate.Value.Trim();
                if (str5 != null)
                {
                    if (str5 == "update")
                    {
                        //hdIDHuong.Value
                        if (hdIDHuong.Value != hdidthanhvien.Value)
                        {
                            user ThanhVienCu = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdIDHuong.Value));
                            if (ThanhVienCu != null)
                            {
                                // Cập nhật thành viên mới
                                Service_LaiSuatAGLANG abc = db.Service_LaiSuatAGLANGs.SingleOrDefault(p => p.ID == Convert.ToInt32(hd_page_edit_id.Value));
                                abc.ID = Convert.ToInt32(hd_page_edit_id.Value);
                                abc.IDThanhVienHuongHH = Convert.ToInt32(hdidthanhvien.Value);
                                db.SubmitChanges();


                                Service_LaiSuatAGLANG table = db.Service_LaiSuatAGLANGs.Where(s => s.ID == int.Parse(hdidservice.Value)).FirstOrDefault();// xóa 1
                                if (table != null)
                                {
                                    string trangthai = "";
                                    if (this.chkcoppy.Checked)
                                    {
                                        trangthai = "Sao chép số cổ phần sang thành viên mới";
                                    }
                                    else
                                    {
                                        trangthai = "Không Sao chép cổ phần";
                                    }
                                    LichSuThayDoiGoiLand obj = new LichSuThayDoiGoiLand();
                                    obj.IDService = table.ID;
                                    obj.IDCart = table.IDCart;
                                    obj.IDThanhVienCu = int.Parse(hdIDHuong.Value);
                                    obj.IDThanhVienMoi = int.Parse(hdidthanhvien.Value);
                                    obj.NguoiTao = MoreAll.MoreAll.GetCookies("UName").ToString();
                                    obj.NgayTao = DateTime.Now;
                                    obj.TrangThai = trangthai;
                                    db.LichSuThayDoiGoiLands.InsertOnSubmit(obj);
                                    db.SubmitChanges();
                                }

                                if (this.chkcoppy.Checked)
                                {
                                    #region Sét cho thành viên là agland Mới
                                    user thanhvien = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdidthanhvien.Value));
                                    thanhvien.ThanhVienAgLang = 1;
                                    thanhvien.TienDangSoHuuBatDongSan = ThanhVienCu.TienDangSoHuuBatDongSan;
                                    db.SubmitChanges();
                                    #endregion

                                    #region Sét lại cho thành viên cũ
                                    user ThanhVC = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdIDHuong.Value));
                                    ThanhVC.ThanhVienAgLang = 0;
                                    ThanhVC.TienDangSoHuuBatDongSan = "0";
                                    db.SubmitChanges();
                                    #endregion
                                }
                                else
                                {
                                    #region Sét cho thành viên là agland Mới
                                    user thanhvien = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdidthanhvien.Value));
                                    thanhvien.ThanhVienAgLang = 1;
                                    db.SubmitChanges();
                                    #endregion

                                    //#region Sét lại cho thành viên cũ
                                    //user ThanhVC = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdIDHuong.Value));
                                    //ThanhVC.ThanhVienAgLang = 0;
                                    //db.SubmitChanges();
                                    //#endregion
                                }
                            }
                            this.LoadItems();
                            this.pn_list.Visible = true;
                            this.pn_insert.Visible = false;
                            this.hd_insertupdate.Value = "";
                            this.txtTenthanhvien.Text = "";
                            this.hd_insertupdate.Value = "insert";
                            this.hd_id.Value = "-1";
                            this.lblmsg.Text = "";
                            this.hdIDHuong.Value = "";
                            this.hdidservice.Value = "";
                        }
                        else
                        {
                            this.lblmsg.Text = "Không thể lưu . Ô nhập tên thành viên không thể trùng với thành viên cũ";
                        }
                    }
                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.hd_insertupdate.Value = "";
            this.pn_list.Visible = true;
            this.pn_insert.Visible = false;
            txtTenthanhvien.Text = "";
            this.lblmsg.Text = "";
            hidLevel.Value = "";
            lbl_curpage.Text = "";
            hidLevel.Value = "";
            this.hdIDHuong.Value = "";
            this.hdidservice.Value = "";
        }
        public static string LocDate_NgayThangNam(string date)
        {
            return " and ( day(NgayThamGia)=" + Convert.ToDateTime(date).ToString("dd") + " and MONTH(NgayThamGia)=" + Convert.ToDateTime(date).ToString("MM") + "  and  year(NgayThamGia)=" + Convert.ToDateTime(date).ToString("yyyy") + " )";
        }

        public static string ShowUuTienK()
        {
            string submn = "0";
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users  where Uutien=0 and ThanhVienAgLang=1");
            for (int i = 0; i < dt.Count; i++)
            {
                submn = submn + "," + dt[i].iuser_id.ToString();
            }
            return submn;
        }
        public static string ShowUuTien()
        {
            string submn = "0";
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users  where Uutien=1 and ThanhVienAgLang=1");
            for (int i = 0; i < dt.Count; i++)
            {
                submn = submn + "," + dt[i].iuser_id.ToString();

            }
            return submn;
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
                sql += " and ( NgayThamGia>='" + MoreAll.MoreAll.LocDate(txtNgayThangNam.Text) + "' and  NgayThamGia<='" + MoreAll.MoreAll.LocDate(txtDenNgayThangNam.Text) + "' )";
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
            if (ddluutien.SelectedValue == "0")
            {
                sql += " and IDThanhVienHuongHH in (" + ShowUuTienK() + ") ";
            }
            if (ddluutien.SelectedValue == "1")
            {
                sql += " and IDThanhVienHuongHH in (" + ShowUuTien() + ") ";
            }
            if (ddlleader.SelectedValue != "0")
            {
                sql += " and ((MTreeHuong LIKE N'%|" + ddlleader.SelectedValue + "|%')) ";
            }
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(ddlPage.SelectedValue);
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.EService_LaiSuatAGLANG> iitem = SService_LaiSuatAGLANG.CATEGORY_PHANTRANG1(sql);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.EService_LaiSuatAGLANG> dt = SService_LaiSuatAGLANG.CATEGORY_PHANTRANG2(sql, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                double coin = 0.0;
                double tongtien = 0.0;
                for (int i = 0; i < dt.Count; i++)
                {
                    coin += Convert.ToDouble(dt[i].LaiSuat.ToString());
                    tongtien += Convert.ToDouble(dt[i].SoTienDauTu.ToString());
                    ////////// ShowViAgLand(dt[i].IDThanhVienHuongHH.ToString());// sét lại ví aff bằng đúng số tiền hoa hồng trong lịch sử
                }
                ltCoin.Text = "Tổng điểm: " + coin.ToString();
                lttongtien.Text = "Tổng tiền đầu tư: " + AllQuery.MorePro.FormatMoney_NO(tongtien.ToString());

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
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=ServiceAGLANGD&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&type=" + ddlkieu.SelectedValue + "&uutien=" + ddluutien.SelectedValue + "&leader=" + ddlleader.SelectedValue + "", Tongsobanghi, pages);
        }

        protected string ShowViAgLand(string IDThanhVien)
        {
            string sql = "";
            List<Entity.ELaiSuatAGLANG> table = SLaiSuatAGLANG.Name_Text("select *  from LaiSuatAGLANG where IDThanhVienHuongHH=" + IDThanhVien + "  ");
            if (table.Count > 0)
            {
                double coin = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    coin += Convert.ToDouble(table[i].LaiSuat.ToString());
                }
                Susers.Name_Text("update users set ViAgLang=" + coin.ToString() + " where iuser_id=" + IDThanhVien + "");
            }
            return "";
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
                    List<Service_LaiSuatAGLANG> del = db.Service_LaiSuatAGLANGs.Where(s => s.ID == int.Parse(id.Value)).ToList();// xóa nhiều
                    if (del != null)
                    {
                        db.Service_LaiSuatAGLANGs.DeleteAllOnSubmit(del);
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
            Response.Redirect("/admin.aspx?u=ServiceAGLANGD&kw=" + txtkeyword.Text + "&kieu=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&type=" + ddlkieu.SelectedValue + "&uutien=" + ddluutien.SelectedValue + "&leader=" + ddlleader.SelectedValue + "");
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
            string Namefile = "Danh_sach_Thanh_Vien_Tham_Gia_AGLAND_ " + DateTime.Now;
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
            sb.Append("    <b>Sản phẩm</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Thành viên bán</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Người được hưởng</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Tiền đầu tư</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
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
                sql += " and ( NgayThamGia>='" + MoreAll.MoreAll.LocDate(txtNgayThangNam.Text) + "' and  NgayThamGia<='" + MoreAll.MoreAll.LocDate(txtDenNgayThangNam.Text) + "' )";
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
            if (ddluutien.SelectedValue != "0")
            {
                if (ddluutien.SelectedValue == "0")
                {
                    sql += ShowUuTienK();
                }
                if (ddluutien.SelectedValue == "1")
                {
                    sql += ShowUuTien();
                }
            }
            List<Entity.EService_LaiSuatAGLANG> dt = SService_LaiSuatAGLANG.Name_Text("SELECT *  FROM  Service_LaiSuatAGLANG  where 1=1 " + sql + " ORDER BY NgayNhan DESC");
            if (dt.Count >= 1)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + ShowPro(item.IDSanPham.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + Commond.ShowsThanhVien(item.IDThanhVienBan.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + Commond.ShowsThanhVien(item.IDThanhVienHuongHH.ToString()) + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + item.SoTienDauTu + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + item.LaiSuat + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: center;\">" + item.NgayThamGia.ToString() + "</td>");
                    sb.Append("    <td style=\" vertical-align:middle; text-align: center;\">" + item.IDCart.ToString() + "</td>");
                    sb.Append("</tr>");
                }
            }

            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void ddluutien_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }
    }
}