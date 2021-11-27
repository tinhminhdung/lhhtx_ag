using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class LichSuCapDiem : System.Web.UI.UserControl
    {
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            { }
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
                    //if (table.DuyetTienDanap == 0)
                    //{
                    //    Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này. Yêu cầu kích hoạt tài khoản thành viên.');window.location.href='/vi-tien.html'; </script>");
                    //}

                    hdid.Value = table.iuser_id.ToString();
                }
            }
        }
        private void LoadItems()
        {
            if (hdid.Value.Count() > 0)
            {
                List<CapDiemThanhVien> table = db.CapDiemThanhViens.Where(s => s.IDNguoiNhanDiemCoin == int.Parse(hdid.Value)).OrderByDescending(x => x.ID).ToList();
                if (table.Count > 0)
                {
                    double coin = 0.0;
                    try
                    {
                        for (int i = 0; i < table.Count; i++)
                        {
                            coin += Convert.ToDouble(table[i].SoDiemCoin.ToString());
                        }
                    }
                    catch (Exception)
                    { }
                    ltCoin.Text = "Tổng điểm: " + coin.ToString();

                    CollectionPager1.DataSource = table;
                    CollectionPager1.BindToControl = rp_pagelist;
                    CollectionPager1.MaxPages = 10000;
                    CollectionPager1.PageSize = int.Parse("50");
                    rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
                    rp_pagelist.DataBind();
                }
            }
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
    }
}