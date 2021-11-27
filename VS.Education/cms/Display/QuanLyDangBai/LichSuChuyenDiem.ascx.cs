using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class LichSuChuyenDiem : System.Web.UI.UserControl
    {
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (Request["st"] != null && !Request["st"].Equals(""))
                {
                    ddlstatus.SelectedValue = Request["st"];
                }
                if (Request["c"] != null && !Request["c"].Equals(""))
                {
                    ddlvicanchuyen.SelectedValue = Request["c"];
                }
                if (Request["n"] != null && !Request["n"].Equals(""))
                {
                    ddlViNhanDiem.SelectedValue = Request["n"];
                }

            }
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                ShowInfo();
            }
            else
            {
                Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }

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
        private void LoadItems()
        {
            if (hdid.Value.Count() > 0)
            {

                string sql = "";
                string sql1 = "";
                if (Request["st"] != null && !Request["st"].Equals("-1"))
                {
                    sql += " and TrangThai=" + ddlstatus.SelectedValue + " ";
                }
                if (ddlstatus.SelectedValue == "1")
                {
                    if (Request["c"] != null && !Request["c"].Equals("0"))
                    {
                        sql1 += " and ViChuyen=" + ddlvicanchuyen.SelectedValue + " ";
                    }
                    if (Request["n"] != null && !Request["n"].Equals("0"))
                    {
                        sql1 += " and ViNhan=" + ddlViNhanDiem.SelectedValue + " ";
                    }
                }
                else
                {
                    ddlvicanchuyen.Visible = false;
                    ddlViNhanDiem.Visible = false;
                    sql1 = "";
                }

                string sqls = "SELECT * from ChuyenDiemThanhVien where IDNguoiCap=" + hdid.Value + " " + sql + " " + sql1 + "  order by ID desc";
                List<ChuyenDiemThanhVien> table = db.ExecuteQuery<ChuyenDiemThanhVien>(@"" + sqls + "").ToList();


                //  List<ChuyenDiemThanhVien> table = db.ChuyenDiemThanhViens.Where(s => s.IDNguoiCap == int.Parse(hdid.Value)).OrderByDescending(s => s.NgayCap).ThenByDescending(s => s.ID).ToList();
                if (table.Count > 0)
                {
                    double coin = 0.0;
                    for (int i = 0; i < table.Count; i++)
                    {
                        coin += Convert.ToDouble(table[i].SoCoin.ToString());
                    }
                    ltCoin.Text = "Tổng điểm: " + coin.ToString();

                    CollectionPager1.DataSource = table;
                    CollectionPager1.BindToControl = rp_pagelist;
                    CollectionPager1.MaxPages = 10000;
                    CollectionPager1.PageSize = int.Parse("20");
                    rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
                    rp_pagelist.DataBind();
                }
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
                str += "</span> ";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone;
                }
            }
            return str;
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            Response.Redirect("/lich-su-chuyen-diem.html?st=" + ddlstatus.SelectedValue + "&c=" + ddlvicanchuyen.SelectedValue + "&n=" + ddlViNhanDiem.SelectedValue + "");
        }

        protected void ddlViNhanDiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            Response.Redirect("/lich-su-chuyen-diem.html?st=" + ddlstatus.SelectedValue + "&c=" + ddlvicanchuyen.SelectedValue + "&n=" + ddlViNhanDiem.SelectedValue + "");
        }

        protected void ddlvicanchuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            Response.Redirect("/lich-su-chuyen-diem.html?st=" + ddlstatus.SelectedValue + "&c=" + ddlvicanchuyen.SelectedValue + "&n=" + ddlViNhanDiem.SelectedValue + "");
        }
    }
}