using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VS.E_Commerce.Application;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class DanhSachListViewThanhVien2 : System.Web.UI.UserControl
    {
        public int i = 1;
        string nav = "";
        string Loc = "";
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        string ID = MoreAll.MoreAll.GetCookies("MembersID").ToString();
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
            if (!base.IsPostBack)
            {
                if (Request["ID"] != null && !Request["ID"].Equals(""))
                {
                    ID = Request["ID"];
                    Loc += "&ID=" + ID;
                }


                List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users  WHERE iuser_id = " + ID + " ");
                if (dt.Count > 0)
                {
                    string Mtr = "|" + dt[0].MTree.ToString();
                    if (Mtr.Contains("|" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + "|"))
                    {
                        ltphanmuc.Text = LoadNav(int.Parse(ID.ToString()));
                        LoadItems();
                    }
                    else
                    {
                        WebMsgBox.Show("Bạn ko có quyền xem ID này. Vì ID này không nằm trong hệ thống của bạn");

                    }
                }
            }
        }
        public void LoadItems()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                int Tongsobanghi = 0;
                Int16 pages = 1;
                int Tongsotrang = int.Parse("10");
                if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
                {
                    pages = Convert.ToInt16(Request.QueryString["page"].Trim());
                }
                List<Entity.TongSo> iitem = Susers.ThanhVien_PHANTRANG1(ID, "1");
                if (iitem.Count() > 0)
                {
                    Tongsobanghi = iitem[0].Tong;
                }
                List<Entity.users> dt = Susers.ThanhVien_PHANTRANG2(ID, "1", (pages - 1), Tongsotrang);
                if (dt.Count >= 1)
                {
                    rp_items.DataSource = dt;
                    rp_items.DataBind();
                }
                if (Tongsobanghi % Tongsotrang > 0)
                {
                    Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
                }
                else
                {
                    Tongsobanghi = Tongsobanghi / Tongsotrang;
                }
                ltpage.Text = Commond.PhantrangAdmin("/Danh-sach-thanh-vien.html?ID=" + ID + "", Tongsobanghi, pages);
            }
        }
        private string LoadNav(int ID)
        {
            try
            {
                var item = db.users.FirstOrDefault(s => s.iuser_id == ID);
                if (item != null)
                {
                    nav = "<span> <i class=\"fa fa-angle-right\"></i> </span> <a href=\"/Danh-sach-thanh-vien.html?ID=" + item.iuser_id + "\">" + item.vfname + "</a>" + nav;
                    if (int.Parse(item.GioiThieu) >= int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()))
                    {
                        LoadNav(Convert.ToInt32(item.GioiThieu));
                    }
                }
            }
            catch (Exception)
            { }
            return nav;
        }

        public string TongThanhVien_Tree(string id)
        {
            string str = "0";
            try
            {
                var dt = db.S_Members_List_TongThanhVien_Tree(int.Parse(id)).ToList();
                if (dt.Count > 0)
                {
                    str = dt[0].Tong.ToString();
                }
            }
            catch (Exception)
            { }
            Double Tong = 0;
            if (str != "0")
            {
                Tong = Convert.ToDouble(str) - 1;
            }
            return Tong.ToString();
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
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count >= 1)
            {
                str = dt[0].Name;
            }
            return str;
        }

        protected void lnkxuatExel_Click(object sender, EventArgs e)
        {
            string Namefile = "DanhSachThanhVien" + DateTime.Now;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=" + Namefile + ".xls");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            sb.Append("<table border='1' bgcolor='#ffffff' bordercolor='#dedede' cellspacing='0' cellpadding='0' style='font-size:12px; font-family:Arial; background:white;'>");
            sb.Append("<tr>");
            sb.Append("  <th style=\"width:50px; vertical-align:middle; height: 22px;\">");
            sb.Append("    <b>STT</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:50px; vertical-align:middle;\">");
            sb.Append("    <b>ID</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:520px; vertical-align:middle;\">");
            sb.Append("    <b>Tên Đăng Nhập</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:50px; vertical-align:middle;\">");
            sb.Append("    <b>Level</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Họ và tên</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:400px; vertical-align:middle;\">");
            sb.Append("    <b>Địa chỉ</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Điện thoại</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Email</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Chi Nhánh</b>");
            sb.Append("  </th>");
            //sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            //sb.Append("    <b>AG Land</b>");
            //sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Kích Hoạt</b>");
            sb.Append("  </th>");
            //sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            //sb.Append("    <b>Ví AFF</b>");
            //sb.Append("  </th>");
            //sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            //sb.Append("    <b>Ví Thương mại</b>");
            //sb.Append("  </th>");
            //sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            //sb.Append("    <b>Ví ưu tiên</b>");
            //sb.Append("  </th>");

            sb.Append("</tr>");
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users as con WHERE ((MTree LIKE N'%|'+CONVERT(varchar," + MoreAll.MoreAll.GetCookies("MembersID") + ")+'|%')) ORDER BY iuser_id DESC");
            if (dt.Count > 0)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle;text-align: left;\"><a href=\"#" + item.iuser_id + "\">" + item.iuser_id + "</a></td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.vuserun + "</td>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle;text-align: left;\">" + item.LevelThanhVien + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + item.vfname + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: left;\">" + item.vaddress + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.vphone + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.vemail + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + Commond.Name(item.IDChiNhanh.ToString()) + "</td>");


                    //if (item.ThanhVienAgLang == 0)
                    //{
                    //    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\"></td>");
                    //}
                    //else
                    //{
                    //    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Là thành viên AG Land</td>");
                    //}
                    if (item.DuyetTienDanap == 0)
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Chưa kích hoạt</td>");
                    }
                    else
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Đã Kích Hoạt</td>");
                    }
                    //sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.VIAAFFILIATE + "</td>");
                   // sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.TongTienCoinDuocCap + "</td>");
                    //sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.ViUuTien + "</td>");
                    sb.Append("  </tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

    }
}