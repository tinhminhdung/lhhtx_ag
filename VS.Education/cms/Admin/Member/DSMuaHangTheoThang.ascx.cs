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
using System.Data.SqlClient;
using System.Data;
using Framwork;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class DSMuaHangTheoThang : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        public string IDThanhVien = "";
        DateTime fDate, tDate;
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
                #endregion

                if (!Commond.Setting("PageChuyenDiem").Equals(""))
                {
                    ddlPage.SelectedValue = Commond.Setting("PageChuyenDiem");
                }
                if (Request["Tu"] != null && !Request["Tu"].Equals(""))
                {
                    txtNgayThangNam.Text = Request["Tu"];
                }
                if (Request["Den"] != null && !Request["Den"].Equals(""))
                {
                    txtDenNgayThangNam.Text = Request["Den"];
                }
                this.LoadItems();
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
        private void LoadItems()
        {
            string sql = "";
            if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            {
                sql += LocDate_NgayThangNam(txtNgayThangNam.Text);
            }
            else if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && !string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            {
                sql += " and  ( pc.Create_Date>='" + MoreAll.MoreAll.LocDate(txtNgayThangNam.Text) + "' and pc.Create_Date<='" + MoreAll.MoreAll.LocDate(txtDenNgayThangNam.Text) + "' )";
            }

            if (Commond.Check(txtNgayThangNam.Text))
                fDate = Commond.ConvertStringToDate(txtNgayThangNam.Text, "dd/MM/yyyy");
            if (Commond.Check(txtDenNgayThangNam.Text))
                tDate = Commond.ConvertStringToDate(txtDenNgayThangNam.Text, "dd/MM/yyyy");

            if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND pc.Create_Date IS NOT NULL AND ((DATEADD(dd,-31,pc.Create_Date)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR pc.Create_Date>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000') AND pc.Create_Date <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999')";
            }
            else if (txtNgayThangNam.Text == "" && txtDenNgayThangNam.Text != "")
            {
                sql += " AND pc.Create_Date IS NOT NULL AND pc.Create_Date <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999'";
            }
            else if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text == "")
            {
                sql += " AND pc.Create_Date IS NOT NULL AND (DATEADD(dd,-31,pc.Create_Date)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR pc.Create_Date>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000')";
            }
            List<Entity.Carts> table = SCarts.Name_Text(@"SELECT pc.*,p.* FROM Carts AS pc left join CartDetail as p ON pc.ID = p.ID_Cart WHERE p.TrangThaiNhaCungCap=1 and TrangThaiNguoiMuaHang=1 " + sql + "").ToList();
            if (table.Count() > 0)
            {
                CollectionPager1.DataSource = table;
                CollectionPager1.BindToControl = rp_pagelist;
                CollectionPager1.MaxPages = 10000;
                CollectionPager1.PageSize = int.Parse(ddlPage.SelectedValue);
                rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
                rp_pagelist.DataBind();
            }
        }
        protected string SearchThanhVien(string keyword)
        {
            string str = "0";
            List<Entity.users> dt = Susers.Name_Text("select * from users where (vuserun like N'%" + keyword + "%')");
            if (dt.Count >= 1)
            {
                str = dt[0].iuser_id.ToString();
            }
            return str;
        }
        public static string LocDate_NgayThangNam(string date)
        {
            return "  and ( day(pc.Create_Date)=" + Convert.ToDateTime(date).ToString("dd") + " and MONTH(pc.Create_Date)=" + Convert.ToDateTime(date).ToString("MM") + "  and  year(pc.Create_Date)=" + Convert.ToDateTime(date).ToString("yyyy") + " )";
        }
        protected string SearchChitietSP()
        {
            string str = "0";
            List<Entity.CartDetail> dt = SCartDetail.Name_Text("select * from CartDetail where  TrangThaiNhaCungCap=1");
            if (dt.Count >= 1)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    str = str + "," + dt[i].ID_Cart.ToString();
                }
            }
            List<Entity.CartDetail> dt2 = SCartDetail.Name_Text("select * from CartDetail where  TrangThaiNguoiMuaHang =1");
            if (dt2.Count >= 1)
            {
                for (int i = 0; i < dt2.Count; i++)
                {
                    str = str + "," + dt2[i].ID_Cart.ToString();
                }
            }
            return str.Replace("0,", "");
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
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + " (Level: " + dt[0].LevelThanhVien + ")</span></a>";
                }
                str += "</span> ";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone;
                }
            }
            return str;
        }
        protected string ShowViTangTienVip(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count >= 1)
            {
                str += dt[0].ViTangTienVip;
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
        protected string NgayMuaHangGanNhat(string id)
        {
            string str = "";
            DataTable table3 = new DataTable();
            FCartDetail db = new FCartDetail();
            db.CartDetail_IDThanhViens(table3, id);
            if (table3.Rows.Count > 0)
            {
                return "<a style=' color:red' target='_blank' href='/admin.aspx?u=DetailCartNhanh&ID=" + table3.Rows[0]["ID_Cart"].ToString() + "'><b>Mã đơn hàng :#" + table3.Rows[0]["ID_Cart"].ToString() + "</b></a><br>" + table3.Rows[0]["Create_Date"].ToString();
            }
            return "";
        }
        protected string TongTien(string ID_Cart)
        {
            string str = "";
            Double Tong = 0;
            List<Entity.CartDetail> dt = SCartDetail.Name_Text("select * from CartDetail where  ID_Cart=" + ID_Cart + " ");
            if (dt.Count >= 1)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    Tong += dt[i].Money;
                }
            }
            return Tong.ToString();
        }
        void Show()
        {
            Response.Redirect("/admin.aspx?u=DSMuaHangTheoThang&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "");
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
    }
}