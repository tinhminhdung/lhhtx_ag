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
    public partial class CheckTimKiemLichSuDangNhap : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        public string IDThanhVien = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
            {
                IDThanhVien = Request["IDThanhVien"];
                ltname.Text = ShowtThanhViens(IDThanhVien);
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
                #endregion
                if (!Commond.Setting("PageChuyenDiem").Equals(""))
                {
                    ddlPage.SelectedValue = Commond.Setting("PageChuyenDiem");
                }
                if (Request["kw"] != null && !Request["kw"].Equals(""))
                {
                    txtkeyword.Text = Request["kw"];
                }
                this.LoadItems();
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        private void LoadItems()
        {
            if (txtkeyword.Text != "")
            {
                List<HistoryLogin> table = db.HistoryLogins.OrderByDescending(x => x.ID).ToList();
                table = table.Where(s => s.IP.Contains(txtkeyword.Text.Trim().Replace("&nbsp;", ""))).ToList();
                CollectionPager1.DataSource = table;
                CollectionPager1.BindToControl = rp_pagelist;
                CollectionPager1.MaxPages = 10000;
                CollectionPager1.PageSize = int.Parse(ddlPage.SelectedValue);
                rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
                rp_pagelist.DataBind();
            }
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
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + " (" + dt[0].vuserun + ")</span></a>";
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

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin.aspx?u=checkIP&kw=" + txtkeyword.Text + "");
        }
    }
}