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

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class MLichSuMuaDiem : System.Web.UI.UserControl
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
                if (!MoreAll.Other.Giatri("PageMuaDiemThanhVien").Equals(""))
                {
                    ddlPage.SelectedValue = MoreAll.Other.Giatri("PageMuaDiemThanhVien");
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

        public void LoadItems()
        {
            string sql = "";
            if (Request["nam"] != null && !Request["nam"].Equals("0"))
            {
                sql += " and year(NgayGui)=" + Request["nam"] + "";
            }
            if (Request["thang"] != null && !Request["thang"].Equals("0"))
            {
                sql += " and month(NgayGui)=" + Request["thang"] + "";
            }
            if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
            {
                sql += " and day(NgayGui)=" + Request["ngay"] + "";
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
            List<Entity.EMuaDiemThanhVien> iitem = SMuaDiemThanhVien.CATEGORY_PHANTRANG1(sql);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.EMuaDiemThanhVien> dt = SMuaDiemThanhVien.CATEGORY_PHANTRANG2(sql, (pages - 1), Tongsotrang);
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
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=MLichSuMuaDiem&kw=" + txtkeyword.Text + "&ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "", Tongsobanghi, pages);
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
                case "Delete":
                    MuaDiemThanhVien del = db.MuaDiemThanhViens.Where(s => s.ID == int.Parse(str2)).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        if (del.TrangThai == 0)
                        {
                            //#region Hoàn tiền cho thành viên
                            //user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(del.IDThanhVien.ToString()));
                            //if (iitem != null)
                            //{
                            //    double TongSoVNDDaCo = Convert.ToDouble(iitem.TongTienCongLai);
                            //    double TongTienNapVaoVND = Convert.ToDouble(del.SoTienCanRut);

                            //    // double TongSoCoinDaCo = Convert.ToDouble(iitem.TongTienCoinDuocCap);

                            //    double ConglaiVND = 0;
                            //    if (TongTienNapVaoVND.ToString() != "0")
                            //    {
                            //        ConglaiVND = ((TongSoVNDDaCo) + (TongTienNapVaoVND));
                            //        iitem.TongTienCongLai = ConglaiVND.ToString();
                            //    }
                            //    db.SubmitChanges();
                            //}
                            //#endregion
                        }
                        //  LichSuGiaoDich("0", "22", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Xóa : Rút tiền ", del.IDThanhVien.ToString(), del.IDThanhVien.ToString(), "0", del.SoCoin.ToString());
                        db.MuaDiemThanhViens.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        this.LoadItems();
                    }
                    return;
                //case "Huy":
                //    MuaDiemThanhVien abc = db.MuaDiemThanhViens.SingleOrDefault(p => p.ID == int.Parse(e.CommandArgument.ToString().Trim()));
                //    if (abc != null)
                //    {
                //        abc.TrangThai = 0;
                //        abc.NgayDuyet = DateTime.Now.ToString();
                //        abc.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                //        db.SubmitChanges();
                //        this.LoadItems();
                //        base.Response.Redirect(base.Request.Url.ToString().Trim());
                //    }
                //    return;
                case "Duyet":
                    MuaDiemThanhVien abcs = db.MuaDiemThanhViens.SingleOrDefault(p => p.ID == int.Parse(e.CommandArgument.ToString().Trim()));
                    abcs.TrangThai = 1;
                    abcs.NgayDuyet = DateTime.Now.ToString();
                    abcs.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                    db.SubmitChanges();
                    #region Cộng điểm coin vào bảng thành viên khi chuyển điểm
                    user congdiem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(abcs.IDThanhVien.ToString()));
                    if (congdiem != null)
                    {
                        double TongSoCoinDaCos = Convert.ToDouble(congdiem.TongTienCoinDuocCap);
                        double TongTienNapVaos = Convert.ToDouble(abcs.SoDiemCanMua.ToString());
                        double Conglais = 0;
                        Conglais = ((TongSoCoinDaCos) + (TongTienNapVaos));
                        Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglais.ToString() + " where iuser_id=" + abcs.IDThanhVien.ToString() + "");
                        LichSuGiaoDich("0", "23", "Admin (" + MoreAll.MoreAll.GetCookies("UName").ToString() + "): Duyệt mua điểm ", abcs.IDThanhVien.ToString(), abcs.IDThanhVien.ToString(), "0", abcs.SoDiemCanMua.ToString());
                    }
                    #endregion
                    this.LoadItems();
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
            }
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
        //                List<MuaDiemThanhVien> del = db.MuaDiemThanhViens.Where(s => s.ID == int.Parse(id.Value)).ToList();// xóa nhiều
        //                if (del != null)
        //                {
        //                    //LichSuGiaoDich("0", "22", "Admin:(" + MoreAll.MoreAll.GetCookies("UName").ToString() + ") Xóa : Rút tiền ", del[0].IDThanhVien.ToString(), del[0].IDThanhVien.ToString(), "0", del[0].SoCoin.ToString());
        //                    db.MuaDiemThanhViens.DeleteAllOnSubmit(del);
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
        protected string ShowtThanhVien(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count >= 1)
            {
                str += " Họ và tên:<span style=\"color: #444444; padding-left: 27px; font-weight: bold\"><span style=\" color:red\">";
                if (dt[0].vfname.ToString().Length > 0)
                {
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + "</span></a>";
                }
                str += "</span></span><br />";
                str += " Tên đăng nhập:<span style=\"color: #444444; padding-left: 27px; font-weight: bold\"><span style='color:red'>" + dt[0].vuserun + "</span></span><br />";
                str += " Số điện thoại:<span style=\"color: #444444; padding-left: 27px; font-weight: bold\"><span style='color:red'>" + dt[0].vphone + "</span></span><br />";
                str += " Email:<span style=\"color: #444444; padding-left: 27px; font-weight: bold\"><span style='color:red'>" + dt[0].vemail + "</span></span><br />";
                str += " Địa chỉ:<span style=\"color: #444444; padding-left: 27px; font-weight: bold\"><span style='color:red'>" + dt[0].vaddress + "</span></span><br />";
            }
            else
            {
                str = "Không tìm thấy thành viên";
            }
            return str;
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Update_setting("PageMuaDiemThanhVien", ddlPage.SelectedValue);
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
            Response.Redirect("/admin.aspx?u=MMuaDiemThanhVien&kw=" + txtkeyword.Text + "&ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "");
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
            return status.Equals("1");
        }
        protected void Lock_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn hủy thanh toán?')";
        }
        protected void Duyet_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn duyệt mua điểm của thành viên này?.')";
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
    }
}