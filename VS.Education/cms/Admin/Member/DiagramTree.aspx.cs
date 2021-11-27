using Framework;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class DiagramTree : System.Web.UI.Page
    {
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        string IDTHanhVien = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            if (Request["IDTHanhVien"] != null && !Request["IDTHanhVien"].Equals(""))
            {
                IDTHanhVien = Request["IDTHanhVien"];
                Fusers item = new Fusers();
                List<Entity.users> table = Susers.Name_Text("select * from users where  iuser_id=" + (IDTHanhVien.Trim().ToLower()) + " ");
                if (table.Count > 0)
                {
                    ltshow.Text = ShowDanhsachthanhvien(IDTHanhVien.ToString());
                    ltname.Text = table[0].vuserun + " - (Level:" + table[0].LevelThanhVien + ") - " + table[0].vfname + " - " + table[0].vphone;
                   // Response.Redirect("/cms/Admin/Member/DiagramTree.aspx");
                }
            }
        }

        protected string ShowDanhsachthanhvien(string MembersID)
        {
            string str = "";
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM [users] where   iuser_id='" + MembersID + "'  and istatus=1 order by iuser_id asc");
            if (dt.Count > 0)
            {
                foreach (Entity.users item in dt)
                {
                    str += "<li><code>" + item.vuserun.ToString() + "(Level:" + item.LevelThanhVien + ") <br /> " + item.vfname.ToString() + "</code>" + SupN3(item.iuser_id.ToString()) + "</li>";
                }
            }
            return str.ToString();
        }
        protected string SupN3(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM [users]  where  GioiThieu=" + id + "  and istatus=1 order by iuser_id asc");
            if (dt.Count > 0)
            {
                str += "<ul>";
                foreach (Entity.users item in dt)
                {
                    str += "<li><code>" + item.vuserun.ToString() + "(Level:" + item.LevelThanhVien + ") <br /> " + item.vfname.ToString() + "</code>" + SupN3(item.iuser_id.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }

    }
}