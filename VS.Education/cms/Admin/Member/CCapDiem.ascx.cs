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
using VS.E_Commerce.Application;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class CCapDiem : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        public string IDThanhVien = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
            {
                IDThanhVien = Request["IDThanhVien"];
                ltname.Text = this.ltshowten.Text = ShowtThanhViens(IDThanhVien);
            }
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
                this.Page.Form.Enctype = "multipart/form-data";
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btn_InsertUpdate);
                #endregion
                if (!Commond.Setting("PageChuyenDiem").Equals(""))
                {
                    ddlPage.SelectedValue = Commond.Setting("PageChuyenDiem");
                }
                if (MoreAll.MoreAll.GetCookie("URole") != null)
                {
                    string strArray = MoreAll.MoreAll.GetCookie("URole").ToString().Trim();
                    if (strArray.Length > 0)
                    {
                        if (strArray.Contains("|23"))
                        {
                            this.LoadItems();
                        }
                        else if (!strArray.Contains("|23"))
                        {
                            Response.Redirect("/admin.aspx");
                        }
                    }
                }
            }
        }
        protected void btn_InsertUpdate_Click(object sender, EventArgs e)
        {
            // try
            //{
            if (this.ltshowten.Text.Trim().Length < 0)
            {
                this.lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn"; return;
            }
            else if (txtSoCoin.Text.Trim() == "")
            {
                this.lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn"; return;
            }
            else
            {
                string sgrnlevel = hidLevel.Value;
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                CapDiemThanhVien obj = new CapDiemThanhVien();
                string str5 = this.hd_insertupdate.Value.Trim();
                if (str5 != null)
                {
                    if (!(str5 == "update"))
                    {
                        if (str5 == "insert")
                        {
                            string TextNhanDiem = "";
                            if (ddlvidiem.SelectedValue == "1")// ví tổng thương mại, 2 ví Aff
                            {
                                TextNhanDiem = "Cấp điểm vào ví thương mại";
                            }
                            else if (ddlvidiem.SelectedValue == "2")
                            {
                                TextNhanDiem = "Cấp điểm vào ví quản lý";
                            }
                            else if (ddlvidiem.SelectedValue == "5")
                            {
                                TextNhanDiem = "Cấp điểm vào THƯỞNG MUA HÀNG";
                            }

                            obj.IDNguoiCap = 0;
                            obj.IDNguoiNhanDiemCoin = int.Parse(IDThanhVien);
                            obj.SoDiemCoin = txtSoCoin.Text;
                            obj.NgayCap = DateTime.Now;
                            obj.MoTa = TextNhanDiem;
                            obj.NguoiTao = MoreAll.MoreAll.GetCookies("UName").ToString();
                            obj.TrangThai = Convert.ToInt16(chck_Enable.Checked ? "1" : "0");
                            obj.KieuVi = Convert.ToInt16(ddlvidiem.SelectedValue);//1 là ví tổng, 2 là ví Aff
                            db.CapDiemThanhViens.InsertOnSubmit(obj);
                            db.SubmitChanges();

                            #region Cộng điểm vào bảng thành viên TongTienCoinDuocCap
                            List<Entity.users> iitem = Susers.GET_BY_ID(IDThanhVien.ToString());
                            if (iitem.Count() > 0)
                            {
                                double SoCoin = Convert.ToDouble(txtSoCoin.Text);
                                if (ddlvidiem.SelectedValue == "1")// ví tổng
                                {
                                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                                    double TongTienNapVao = Convert.ToDouble(txtSoCoin.Text);
                                    double Conglai = 0;
                                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));

                                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + " where iuser_id=" + IDThanhVien.ToString() + "");
                                }
                                else if (ddlvidiem.SelectedValue == "2")// ví tổng
                                {
                                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].VIAAFFILIATE);
                                    double TongTienNapVao = Convert.ToDouble(txtSoCoin.Text);
                                    double Conglai = 0;
                                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                                    Susers.Name_Text("update users set VIAAFFILIATE=" + Conglai.ToString() + " where iuser_id=" + IDThanhVien.ToString() + "");
                                }
                                else if (ddlvidiem.SelectedValue == "5")// ví tổng
                                {
                                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViTangTienVip);
                                    double TongTienNapVao = Convert.ToDouble(txtSoCoin.Text);
                                    double Conglai = 0;
                                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                                    Susers.Name_Text("update users set ViTangTienVip=" + Conglai.ToString() + " where iuser_id=" + IDThanhVien.ToString() + "");
                                }
                                db.SubmitChanges();
                                LichSuGiaoDich("16", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Cấp điểm cho thành viên", "0", IDThanhVien.ToString(), "0", txtSoCoin.Text);
                            }
                            #endregion

                            Response.Write("<script type=\"text/javascript\">alert('Bạn đã cấp điểm thành công');window.location.href='" + Request.RawUrl.ToString() + "'; </script>");

                        }
                    }
                    else
                    {
                        obj.ID = Convert.ToInt16(hd_page_edit_id.Value);
                        obj.IDNguoiNhanDiemCoin = int.Parse(IDThanhVien);
                        obj.SoDiemCoin = txtSoCoin.Text;
                        obj.NgayCap = DateTime.Now;
                        // obj.MoTa = txtMota.Text;
                        obj.NguoiTao = MoreAll.MoreAll.GetCookies("UName").ToString();
                        obj.TrangThai = Convert.ToInt16(chck_Enable.Checked ? "1" : "0");
                        db.SubmitChanges();
                    }
                }
                this.LoadItems();
                this.pn_list.Visible = true;
                this.pn_insert.Visible = false;
                this.hd_insertupdate.Value = "";
                this.ltshowten.Text = "";
                this.hd_insertupdate.Value = "insert";
                this.hd_id.Value = "-1";
                this.hdFileName.Value = "";
                txtSoCoin.Text = "";
                this.lblmsg.Text = "";
                this.lbl_curpage.Text = "";
            }
            // }
            //catch (Exception) { }
        }

        void LichSuGiaoDich(string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin)
        {
            #region LichSuGiaoDich
            LichSuGiaoDich obl = new LichSuGiaoDich();
            obl.IDProducts = int.Parse("0");
            obl.IDType = int.Parse(IDType);
            obl.Type = Type;
            obl.IDThanhVien = int.Parse(IDThanhVien);
            obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
            obl.PhamTramHoaHong = PhamTramHoaHong;
            obl.SoCoin = SoCoin.ToString();
            obl.NgayTao = DateTime.Now;
            obl.NoiDung = "0";
            obl.IDCart = 0;
            db.LichSuGiaoDiches.InsertOnSubmit(obl);
            db.SubmitChanges();
            #endregion
        }
        private void btn_link_cancel_Click(object sender, EventArgs e)
        {
            this.pn_list.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.hd_insertupdate.Value = "";
            this.pn_list.Visible = true;
            this.pn_insert.Visible = false;
            this.hdFileName.Value = "";

            this.lblmsg.Text = "";

            hidLevel.Value = "";
            lbl_curpage.Text = "";

            txtSoCoin.Text = "";
            hidLevel.Value = "";
        }

        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xóa bài viết này ?')";
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
            this.hd_insertupdate.Value = "insert";
            this.hd_page_edit_id.Value = "-1";
            this.hdFileName.Value = "";
            txtSoCoin.Text = "";
            this.lblmsg.Text = "";
            this.ltshowten.Text = "";
            hidLevel.Value = "";

        }

        protected void rp_pagelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str = e.CommandName.Trim();
            string str2 = e.CommandArgument.ToString().Trim();
            string str4 = str;

            switch (e.CommandName)
            {
                #region EditDetail
                case "EditDetail":
                    CapDiemThanhVien table = db.CapDiemThanhViens.SingleOrDefault(p => p.ID == int.Parse(str2));
                    if (table != null)
                    {
                        this.pn_list.Visible = false;
                        this.pn_insert.Visible = true;
                        this.hd_insertupdate.Value = "update";
                        this.hd_page_edit_id.Value = str2.Trim();
                        this.hdid.Value = table.ID.ToString().Trim();
                        this.ltshowten.Text = ShowtThanhViens(table.IDNguoiNhanDiemCoin.ToString().Trim());
                        txtSoCoin.Text = table.SoDiemCoin.ToString().Trim();
                        // txtMota.Text = table.MoTa.ToString().Trim();
                        chck_Enable.Checked = (table.TrangThai == 1);
                    }
                    return;
                #endregion
                case "Delete":
                    CapDiemThanhVien del = db.CapDiemThanhViens.Where(s => s.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        double SoCoin = Convert.ToDouble(del.SoDiemCoin);
                        double SoTienQuyDoiVND = (SoCoin) * 1000;// Từ Coin quy ra tiền
                        LichSuGiaoDich("20", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Xóa Cấp điểm cho thành viên", "0", IDThanhVien.ToString(), "0", del.SoDiemCoin);

                        //#region Trừ điểm vào bảng thành viên  TongTienCoinDuocCap
                        //user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien.ToString()));
                        //if (iitem != null)
                        //{
                        //    double TongSoCoinDaCo = Convert.ToDouble(iitem.TongTienCoinDuocCap);
                        //    double TongTienNapVao = Convert.ToDouble(del.SoDiemCoin);
                        //    if (TongSoCoinDaCo >= TongTienNapVao)
                        //    {
                        //        double Conglai = 0;
                        //        Conglai = ((TongSoCoinDaCo) - (TongTienNapVao));
                        //        iitem.TongTienCoinDuocCap = Conglai.ToString();
                        //        db.SubmitChanges();
                        //    }
                        //    else
                        //    {
                        //        ltmsg.Text = "Lỗi không trừ được điểm";
                        //    }
                        //}
                        //#endregion
                        db.CapDiemThanhViens.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        this.LoadItems();
                    }
                    return;
                //case "ChangeStatus":
                //    string str3;
                //    str2 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                //    if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                //    { str3 = "0"; }
                //    else { str3 = "1"; }
                //    SMenu.UPDATESTATUS(str2, str3);
                //    this.LoadItems();
                //    return;
            }
        }

        private void LoadItems()
        {
            List<CapDiemThanhVien> table = db.CapDiemThanhViens.Where(s => s.IDNguoiNhanDiemCoin == int.Parse(IDThanhVien)).OrderByDescending(x => x.ID).ToList();
            CollectionPager1.DataSource = table;
            CollectionPager1.BindToControl = rp_pagelist;
            CollectionPager1.MaxPages = 10000;
            CollectionPager1.PageSize = int.Parse(ddlPage.SelectedValue);
            rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
            rp_pagelist.DataBind();
        }

        protected void btthemmoi_Click(object sender, EventArgs e)
        {
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
            this.hd_insertupdate.Value = "insert";
            this.hd_page_edit_id.Value = "-1";
            this.hdFileName.Value = "";
            txtSoCoin.Text = "";
            this.lblmsg.Text = "";
            hidLevel.Value = "";
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoreAll.MoreAll.Update_setting("PageChuyenDiem", ddlPage.SelectedValue);
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected string ShowtThanhVien(string id, string NguoiTao)
        {
            string str = "";
            if (id.ToString() == "0")
            {
                return "Admin - " + NguoiTao;
            }
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
            return str;
        }
        protected string ShowtThanhViens(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count >= 1)
            {
                str += "<span id=" + dt[0].iuser_id.ToString() + " style=\" color:red\">";
                if (dt[0].vfname.ToString().Length > 0)
                {
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + " (" + dt[0].vuserun + ")  </span></a>";
                }
                str += "</span><br>";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone;
                }
            }
            return str;
        }
    }
}