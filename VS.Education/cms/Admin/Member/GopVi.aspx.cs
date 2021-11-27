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
using System.Text;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class GopVi : System.Web.UI.Page
    {
        string IDThanhVien = "";
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
            {
                IDThanhVien = Request["IDThanhVien"];
                var table = db.LichSuGopVis.Where(s => s.IDThanhVien == Convert.ToInt32(IDThanhVien)).ToList();
                if (table.Count > 0)
                {
                    rp_pagelist.DataSource = table;
                    rp_pagelist.DataBind();
                }
                else
                {
                    Response.Write("Chưa có dữ liệu (Lưu ý chỉ có thành viên trước ngày 14/ tháng 10/2020 mới có dữ liệu dồn ví nhé)");
                }
                var table2 = db.LichSuDonViTongs.Where(s => s.IDThanhVien == Convert.ToInt32(IDThanhVien)).ToList();
                if (table2.Count > 0)
                {
                    Repeater1.DataSource = table2;
                    Repeater1.DataBind();
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
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + " (Level: " + dt[0].LevelThanhVien + ")</span></a>";
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
    }
}