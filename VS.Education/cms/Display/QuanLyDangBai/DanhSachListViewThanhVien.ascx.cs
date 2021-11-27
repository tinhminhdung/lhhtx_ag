using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class DanhSachListViewThanhVien : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }
        private void ShowInfo()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                ltshow.Text = ShowDanhsachthanhvien(MoreAll.MoreAll.GetCookies("MembersID").ToString());
            }
        }
        protected string ShowDanhsachthanhvien(string MembersID)
        {
            string str = "";
            int strs = 0;
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM [users] where   iuser_id='" + MembersID + "'  and istatus=1 order by iuser_id asc");
            if (dt.Count > 0)
            {
                foreach (Entity.users item in dt)
                {
                    strs = strs + 1;
                    str += "<li> <span class='cha'>" + item.vfname.ToString() + " - " + item.vphone.ToString() + "  - " + item.vaddress.ToString() + ShowChiNhanh(item.ChiNhanh.ToString(), item.iuser_id.ToString()) + Showlead(item.Leader.ToString()) + ShowNhaCungCap(item.Type.ToString()) + " </span>" + SupN(item.iuser_id.ToString(), strs.ToString()) + "</li>";
                }
            }
            return str.ToString();
        }
        protected string SupN(string id, string strs)
        {
            string str = "";
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM [users]  where  GioiThieu=" + id + "  and istatus=1 order by iuser_id asc");
            if (dt.Count > 0)
            {
                str += "<ul>";
                foreach (Entity.users item in dt)
                {
                    strs = strs + 1;
                    str += "<li><span class='concap2'>" + item.vfname.ToString() + " - " + item.vphone.ToString() + "  - " + item.vaddress.ToString() + " </span> " + ShowChiNhanh(item.ChiNhanh.ToString(), item.iuser_id.ToString()) + Showlead(item.Leader.ToString()) + ShowNhaCungCap(item.Type.ToString()) + "" + SupN3(item.iuser_id.ToString(), strs.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        protected string SupN3(string id, string strs)
        {
            string str = "";
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM [users]  where  GioiThieu=" + id + "  and istatus=1 order by iuser_id asc");
            if (dt.Count > 0)
            {
                str += "<ul>";
                foreach (Entity.users item in dt)
                {
                    strs = strs + 1;
                    str += "<li><span class='concap3'>" + item.vfname.ToString() + " - " + item.vphone.ToString() + "  - " + item.vaddress.ToString() + " " + ShowChiNhanh(item.ChiNhanh.ToString(), item.iuser_id.ToString()) + Showlead(item.Leader.ToString()) + ShowNhaCungCap(item.Type.ToString()) + " </span>" + SupN4(item.iuser_id.ToString(), strs.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        protected string SupN4(string id, string strs)
        {
            string str = "";
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM [users]  where  GioiThieu=" + id + "  and istatus=1 order by iuser_id asc");
            if (dt.Count > 0)
            {
                str += "<ul>";
                foreach (Entity.users item in dt)
                {
                    strs = strs + 1;
                    str += "<li><span class='concap4'>" + item.vfname.ToString() + " - " + item.vphone.ToString() + "  - " + item.vaddress.ToString() + " " + ShowChiNhanh(item.ChiNhanh.ToString(), item.iuser_id.ToString()) + Showlead(item.Leader.ToString()) + ShowNhaCungCap(item.Type.ToString()) + "</span>" + SupN5(item.iuser_id.ToString(), strs.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        protected string SupN5(string id, string strs)
        {
            string str = "";
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM [users]  where  GioiThieu=" + id + "  and istatus=1 order by iuser_id asc");
            if (dt.Count > 0)
            {
                str += "<ul>";
                foreach (Entity.users item in dt)
                {
                    strs = strs + 1;
                    str += "<li><span class='concap5'>" + item.vfname.ToString() + " - " + item.vphone.ToString() + "  - " + item.vaddress.ToString() + " " + ShowChiNhanh(item.ChiNhanh.ToString(), item.iuser_id.ToString()) + Showlead(item.Leader.ToString()) + ShowNhaCungCap(item.Type.ToString()) + " </span>" + SupN6(item.iuser_id.ToString(), strs.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        protected string SupN6(string id, string strs)
        {
            string str = "";
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM [users]  where  GioiThieu=" + id + "  and istatus=1 order by iuser_id asc");
            if (dt.Count > 0)
            {
                str += "<ul>";
                foreach (Entity.users item in dt)
                {
                    strs = strs + 1;
                    str += "<li><span class='concap6'>" + item.vfname.ToString() + " - " + item.vphone.ToString() + "  - " + item.vaddress.ToString() + " " + ShowChiNhanh(item.ChiNhanh.ToString(), item.iuser_id.ToString()) + Showlead(item.Leader.ToString()) + "</span></li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        public string ShowTrangThai(string enable)
        {
            if (enable.Trim().Equals("0"))
            {
                return "Thành viên";
            }
            else if (enable.Trim().Equals("1"))
            {
                return "Đại lý cấp 1";
            }
            else if (enable.Trim().Equals("2"))
            {
                return "Đại lý cấp 2";
            }
            else if (enable.Trim().Equals("3"))
            {
                return "Đại lý cấp 3";
            }
            return "";
        }

        public string ShowChiNhanh(string enable, string id)
        {
            if (enable.Trim().Equals("1"))
            {
                return "<span class='BodderDo'>Chi Nhánh - " + ShowChiNhanh(id) + "</span> ";
            }
            return "";
        }
        protected string ShowChiNhanh(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM Menu where type=" + id + " ");
            if (dt.Count >= 1)
            {
                str += dt[0].Name;
            }
            return str;
        }
        protected string Showlead(string enable)
        {
            if (enable.Trim().Equals("1"))
            {
                return "<span class='BodderDo'>Lead</span>";
            }
            return "";
          //  return "<span class='BodderXanh'>Thành viên</span>";
        }

        protected string ShowNhaCungCap(string enable)
        {
            if (enable.Trim().Equals("1"))
            {
                return "<span class='BodderDo'>Thành viên</span>";
            }
            return "<span class='BodderXanh'>Cung Cấp</span>";
        }
    }
}