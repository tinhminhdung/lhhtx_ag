using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VS.E_Commerce.Application;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class DoiNhanhThanhVien : System.Web.UI.Page
    {
        string IDThanhVien = "";
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
            {
                if (MoreAll.MoreAll.GetCookies("UName") != "")
                {
                    IDThanhVien = Request["IDThanhVien"];
                    ltname.Text = ShowtThanhViens(IDThanhVien);
                }
                else
                {
                    Response.Redirect("/");
                }
              
            }
        }
        protected string ShowCapDuoi(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.Name_Text("select * from users  where iuser_id =" + id + "");
            if (dt.Count > 0)
            {
                foreach (Entity.users item in dt)
                {
                    str += item.iuser_id + "|" + ShowCapDuoi(item.GioiThieu.ToString());
                }
            }
            return str.ToString();
        }

        protected void btnguoigioithieu_Click(object sender, EventArgs e)
        {
            if (txtIDGioiThieu.Text.Length > 0 && IDThanhVien != "")
            {
                Susers.Name_Text("update users set GioiThieu=" + txtIDGioiThieu.Text + "  where iuser_id=" + IDThanhVien + "");
            }
            Susers.Name_Text("update users set MTree='" + ShowCapDuoi(IDThanhVien) + "'  where iuser_id=" + IDThanhVien + "");
            WebMsgBox.Show("Cập nhật thành công.");
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
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + " (" + dt[0].vuserun + ")  </span></a>";
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