using Entity;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class List_HoaHong_LaisuatAGLand : System.Web.UI.UserControl
    {
        public int i = 1;
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
                this.DropDownList3.Items.Clear();
                this.DropDownList3.Items.Insert(0, new ListItem("Tất cả các năm", "0"));
                for (int i = 2018; i < (DateTime.Now.Year + 1); i++)
                {
                    this.DropDownList3.Items.Add(new ListItem("Năm " + i.ToString(), i.ToString()));
                }
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.DropDownList3, "0");

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
            }

           

            ShowInfo();
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

        private void LoadItems()
        {
            string sql = "";
            if (Request["nam"] != null && !Request["nam"].Equals("0"))
            {
                sql += " and year(NgayNhan)=" + Request["nam"] + "";
            }
            if (Request["thang"] != null && !Request["thang"].Equals("0"))
            {
                sql += " and month(NgayNhan)=" + Request["thang"] + "";
            }
            if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
            {
                sql += " and day(NgayNhan)=" + Request["ngay"] + "";
            }
            if (hdid.Value != "0")
            {
                sql += " and IDThanhVienHuongHH=" + hdid.Value + "";
            }
            if (ddlkieu.SelectedValue != "0")
            {
                sql += " and KieuLaiSuat=" + ddlkieu.SelectedValue + "";
            }
            else
            {
                sql += " and KieuLaiSuat in (1,2)";
            }

            List<LaiSuatAGLANG> table = db.ExecuteQuery<LaiSuatAGLANG>(@"SELECT * from LaiSuatAGLANG where 1=1 " + sql + " order by NgayNhan desc").ToList();
            // List<ELaiSuatAGLANG> table = SLaiSuatAGLANG.Name_Text("SELECT * from LaiSuatAGLANG where 1=1 " + sql + " order by NgayNhan desc");

            CollectionPager1.DataSource = table;
            CollectionPager1.BindToControl = rp_pagelist;
            CollectionPager1.MaxPages = 10000;
            CollectionPager1.PageSize = int.Parse("20");
            rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
            rp_pagelist.DataBind();
            if (!hdid.Value.Equals("0"))
            {
                double coin = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    if (table[i].TrangThai == 1)
                    {
                        coin += Convert.ToDouble(table[i].LaiSuat.ToString());
                    }
                }
                ltCoin.Text = "Tổng điểm: " + coin.ToString();
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
                str += "</span><br>";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone;
                }
            }
            return str;
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
            Response.Redirect("/lai-suat-agland.html?ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "&type=" + ddlkieu.SelectedValue + "");
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
        public string ShowTrangThai(string enable)
        {
            if (enable.Trim().Equals("1"))
            {
                return "Hoa hồng lãi suất 28% / Năm";
            }
            else if (enable.Trim().Equals("2"))
            {
                return "Hoa hồng lãi suất 32% / Năm";
            }
            return "";
        }
        protected string ShowPro(string id)
        {
            string str = "";
            if (id != "0")
            {
                List<Entity.Products> dt = SProducts.GetById(id);
                if (dt.Count >= 1)
                {
                    str = "<a class='mausp' href=\"/" + dt[0].TangName + ".html\" target=\"_blank\">" + dt[0].Name + "</a>";
                }
            }
            return str;
        }
    }
}