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
    public partial class MChuyenDiem : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        public string IDThanhVien = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
            {
                IDThanhVien = Request["IDThanhVien"];
                ltname.Text = ShowtThanhVien(IDThanhVien);
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
                    ChuyenDiemThanhVien del = db.ChuyenDiemThanhViens.Where(s => s.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        db.ChuyenDiemThanhViens.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        this.LoadItems();
                    }
                    return;
            }
        }

        private void LoadItems()
        {
            List<ChuyenDiemThanhVien> table = db.ChuyenDiemThanhViens.Where(s => s.IDNguoiCap == int.Parse(IDThanhVien)).ToList();
            CollectionPager1.DataSource = table;
            CollectionPager1.BindToControl = rp_pagelist;
            CollectionPager1.MaxPages = 10000;
            CollectionPager1.PageSize = int.Parse(ddlPage.SelectedValue);
            rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
            rp_pagelist.DataBind();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoreAll.MoreAll.Update_setting("PageChuyenDiem", ddlPage.SelectedValue);
            Response.Redirect(Request.RawUrl.ToString());
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
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + "-(" + dt[0].vuserun + ") - </span></a>";
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