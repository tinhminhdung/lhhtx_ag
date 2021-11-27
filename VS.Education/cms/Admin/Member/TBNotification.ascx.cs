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
    public partial class TBNotification : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        public string IDThanhVien = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
            {
                IDThanhVien = Request["IDThanhVien"];
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
                if (!Commond.Setting("PageTB").Equals(""))
                {
                    ddlPage.SelectedValue = Commond.Setting("PageTB");
                }
                this.LoadItems();
            }
        }
        protected void btn_InsertUpdate_Click(object sender, EventArgs e)
        {
            // try
            //{
            if (this.txtnoidung.Text.Trim().Length < 1)
            {
                this.lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn";
            }
            else
            {
                string sgrnlevel = hidLevel.Value;
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                Notification obj = new Notification();
                string str5 = this.hd_insertupdate.Value.Trim();
                if (str5 != null)
                {
                    if (!(str5 == "update"))
                    {
                        if (str5 == "insert")
                        {
                            obj.IDThanhVienNhanThongBao = int.Parse(IDThanhVien);
                            obj.NgayTao = DateTime.Now;
                            obj.NoiDung = txtnoidung.Text;
                            obj.NguoiTao = MoreAll.MoreAll.GetCookies("UName").ToString();
                            obj.TrangThai = 0; // Convert.ToInt16(chck_Enable.Checked ? "1" : "0");

                            db.Notifications.InsertOnSubmit(obj);
                            db.SubmitChanges();

                        }
                    }
                    else
                    {
                        Notification abc = db.Notifications.SingleOrDefault(p => p.ID == int.Parse(hd_page_edit_id.Value));
                        abc.IDThanhVienNhanThongBao = int.Parse(IDThanhVien);
                        abc.NgayTao = DateTime.Now;
                        abc.NoiDung = txtnoidung.Text;
                        abc.NguoiTao = MoreAll.MoreAll.GetCookies("UName").ToString();
                        abc.TrangThai = 0;// Convert.ToInt16(chck_Enable.Checked ? "1" : "0");
                        db.SubmitChanges();
                    }
                }
                this.LoadItems();
                this.pn_list.Visible = true;
                this.pn_insert.Visible = false;
                this.hd_insertupdate.Value = "";
                this.txtnoidung.Text = "";
                this.hd_insertupdate.Value = "insert";
                this.hd_id.Value = "-1";
                this.hdFileName.Value = "";
                this.lblmsg.Text = "";
                this.lbl_curpage.Text = "";
            }
            // }
            //catch (Exception) { }
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
            txtnoidung.Text = "";
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
            txtnoidung.Text = "";
            this.lblmsg.Text = "";

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
                    Notification table = db.Notifications.SingleOrDefault(p => p.ID == int.Parse(str2));
                    if (table != null)
                    {
                        this.pn_list.Visible = false;
                        this.pn_insert.Visible = true;
                        this.hd_insertupdate.Value = "update";
                        this.hd_page_edit_id.Value = str2.Trim();
                        this.hdid.Value = table.ID.ToString().Trim();
                        txtnoidung.Text = table.NoiDung;
                        chck_Enable.Checked = (table.TrangThai == 1);
                    }
                    return;
                #endregion
                case "Delete":
                    Notification del = db.Notifications.Where(s => s.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        db.Notifications.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        this.LoadItems();
                    }
                    return;
            }
        }

        private void LoadItems()
        {
            List<Notification> table = db.Notifications.Where(s => s.IDThanhVienNhanThongBao == int.Parse(IDThanhVien)).OrderByDescending(x => x.ID).ToList();
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
            txtnoidung.Text = "";
            this.lblmsg.Text = "";
            hidLevel.Value = "";
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoreAll.MoreAll.Update_setting("PageTB", ddlPage.SelectedValue);
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
                    str += dt[0].vfname;
                }
                str += "</span> ";
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
                    str += dt[0].vfname;
                }
                str += "</span> ";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone;
                }
            }
            return str;
        }


    }
}