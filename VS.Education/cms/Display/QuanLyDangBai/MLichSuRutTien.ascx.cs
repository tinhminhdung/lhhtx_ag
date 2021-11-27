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
    public partial class MLichSuRutTien : System.Web.UI.UserControl
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
                for (int i = 2018; i < (DateTime.Now.Year + 1); i++)
                {
                    this.DropDownList3.Items.Add(new ListItem("Năm " + i.ToString(), i.ToString()));
                }
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.DropDownList3, DateTime.Now.Year.ToString());

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
                user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
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

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        private void LoadItems()
        {

            string sql = "";
            sql += " and year(NgayTao)=" + DropDownList3.SelectedValue + "";
            if (Request["thang"] != null && !Request["thang"].Equals("0"))
            {
                sql += " and month(NgayTao)=" + Request["thang"] + "";
            }
            if (Request["ngay"] != null && !Request["ngay"].Equals("0"))
            {
                sql += " and day(NgayTao)=" + Request["ngay"] + "";
            }
            sql += " and IDThanhVien=" + hdid.Value + "";
            string sqls = "SELECT * from LichSuRutTien where 1=1 " + sql + "  order by NgayTao desc";
            List<LichSuRutTien> table = db.ExecuteQuery<LichSuRutTien>(@"" + sqls + "").ToList();
            if (table.Count > 0)
            {
                double coin = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    coin += Convert.ToDouble(table[i].SoTienCanRut.ToString());
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
                str += "</span>";
            }
            return str;
        }
        protected void bthienthi_Click(object sender, EventArgs e)
        {
            Show();
        }

        void Show()
        {
            Response.Redirect("/lich-su-rut-tien.html?ngay=" + DropDownList1.SelectedValue + "&thang=" + DropDownList2.SelectedValue + "&nam=" + DropDownList3.SelectedValue + "");
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

        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn Muốn hủy lệnh rút tiền ?')";
        }
        protected bool EnableLock(string status)
        {
            return status.Equals("0");
        }
        protected void rp_pagelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str = e.CommandName.Trim();
            string str2 = e.CommandArgument.ToString().Trim();
            string str4 = str;
            switch (e.CommandName)
            {
                case "Delete":

                    LichSuRutTien del = db.LichSuRutTiens.Where(s => s.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        #region Hoàn tiền cho thành viên
                        user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(del.IDThanhVien.ToString()));
                        if (iitem != null)
                        {
                            double TongSoVNDDaCo = Convert.ToDouble(iitem.TongTienCoinDuocCap);
                            double TongTienNapVaoVND = Convert.ToDouble(del.SoTienCanRut);
                            double ConglaiVND = 0;
                            if (TongTienNapVaoVND.ToString() != "0")
                            {
                                ConglaiVND = ((TongSoVNDDaCo) + (TongTienNapVaoVND));
                            }
                            Susers.Name_Text("update users set TongTienCoinDuocCap=" + ConglaiVND + "  where iuser_id=" + del.IDThanhVien.ToString() + "");
                        }
                        #endregion
                        db.LichSuRutTiens.DeleteOnSubmit(del);
                        db.SubmitChanges();
                    }
                    Show();
                    return;
            }
        }
    }
}