using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class MLichSuServiceAgLand : System.Web.UI.UserControl
    {
        string ID = "0";
        public int i = 1;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["ID"] != null && !Request["ID"].Equals(""))
            {
                ID = Request["ID"];
            }

            if (!IsPostBack)
            {
                LoadItems();
            }
        }
        public void LoadItems()
        {
            List<LichSuThayDoiGoiLand> table = db.LichSuThayDoiGoiLands.Where(s => s.IDService == Convert.ToInt32(ID)).OrderByDescending(s => s.ID).ToList();
            if (table.Count > 0)
            {
                rp_pagelist.DataSource = table;
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