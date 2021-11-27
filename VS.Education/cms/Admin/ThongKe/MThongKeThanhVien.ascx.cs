using Entity;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Admin.ThongKe
{
    public partial class MThongKeThanhVien : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (Request["chinhanh"] != null && !Request["chinhanh"].Equals(""))
                {
                    ddlchinhanh.SelectedValue = Request["chinhanh"];
                }
                if (Request["leader"] != null && !Request["leader"].Equals(""))
                {
                    ddlleader.SelectedValue = Request["leader"];
                }
                if (Request["AgLand"] != null && !Request["AgLand"].Equals(""))
                {
                    ddlAgLand.SelectedValue = Request["AgLand"];
                }
                if (Request["uutien"] != null && !Request["uutien"].Equals(""))
                {
                    ddluutien.SelectedValue = Request["uutien"];
                }
                if (Request["kieuthanhvien"] != null && !Request["kieuthanhvien"].Equals(""))
                {
                    ddlkieuthanhvien.SelectedValue = Request["kieuthanhvien"];
                }
                ShowLeader();
                ShowChiNhanh();
                LocTheoThang();
            }
        }
        protected void ShowLeader()
        {
            int str = 0;
            List<Entity.users> dt = Susers.Name_Text("select * from users  where Leader=1 ");
            for (int i = 0; i < dt.Count; i++)
            {
                ddlleader.Items.Insert(str, new ListItem(dt[i].vlname.ToString(), dt[i].iuser_id.ToString()));
            }
            this.ddlleader.Items.Insert(0, new ListItem("== Lọc theo Leader == ", "0"));
            this.ddlleader.DataBind();
        }
        protected void ShowChiNhanh()
        {
            int str = 0;
            List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.DL, this.lang, "-1", "1");
            for (int i = 0; i < dt.Count; i++)
            {
                ddlchinhanh.Items.Insert(str, new ListItem(dt[i].Name.ToString(), dt[i].ID.ToString()));
            }
            this.ddlchinhanh.Items.Insert(0, new ListItem("== Lọc theo chi nhánh == ", "0"));
            this.ddlchinhanh.DataBind();
        }

        public void LocTheoTuan()
        {
            string sql = "";
            if (ddlchinhanh.SelectedValue != "0")
            {
                sql += " and IDChiNhanh =" + ddlchinhanh.SelectedValue + " ";
            }
            if (ddlleader.SelectedValue != "0")
            {
                sql += " and ((MTree LIKE N'%|" + ddlleader.SelectedValue + "|%')) ";
            }
            if (ddlAgLand.SelectedValue != "-1")
            {
                sql += " and ThanhVienAgLang=" + ddlAgLand.SelectedValue + " ";
            }
            if (ddluutien.SelectedValue != "-1")
            {
                sql += " and Uutien=" + ddluutien.SelectedValue + " ";
            }
            if (ddlkieuthanhvien.SelectedValue != "-1")
            {
                if (ddlkieuthanhvien.SelectedValue == "1" || ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and Type=" + ddlkieuthanhvien.SelectedValue + " ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "3")
                {
                    sql += " and Type=2 and TongSoSanPham!=0";
                }
            }


            string lddl_month = ddl_month.SelectedValue.Trim();
            string lddl_yearbyday = ddl_yearbyday.SelectedValue.Trim();

            string str = "";
            str += "<script type=\"text/javascript\">";
            str += "    const dataSource = {";
            str += "        chart: {";
            str += "            caption: \"Thống kê thành viên đăng ký theo năm\",";
            str += "            yaxisname: \"\",";
            str += "            subcaption: \"" + lddl_month + "\",";
            str += "            showhovereffect: \"1\",";
            str += "            drawcrossline: \"1\",";
            str += "            plottooltext: \"<b style='color:red'>$dataValue</b> - $seriesName\",";
            str += "            theme: \"fusion\"";
            str += "        },";
            str += "        categories: [";
            str += "          {";
            str += "              category: [";

            //Category Theo Nhóm (Năm)
            List<ThanhVien> Thang = Name_Text("SELECT count(iuser_id) as numofvisit,day(dcreatedate) as date FROM users where  month(dcreatedate)=" + lddl_month + " and year(dcreatedate)=" + lddl_yearbyday + "  group by day(dcreatedate) order by day(dcreatedate) asc");
            if (Thang.Count > 0)
            {
                for (int i = 0; i < Thang.Count; i++)
                {
                    if (i < (Thang.Count - 1))
                    {
                        str += "{ label: \"" + i + "\" },";
                    }
                    else
                    {
                        str += "{ label: \"" + i + "\" }";
                    }
                }
            }


            str += "              ]";
            str += "          }";
            str += "        ],";

            // Đã kích hoạt
            List<ThanhVien> dt = Name_Text("SELECT count(iuser_id) as numofvisit,day(dcreatedate) as date FROM users  where  month(dcreatedate)=" + lddl_month + " and year(dcreatedate)=" + lddl_yearbyday + "  and DuyetTienDanap=1  " + sql + " group by day(dcreatedate) order by day(dcreatedate) asc");
            if (dt.Count > 0)
            {
                str += "        dataset: [";
                str += "          {";
                str += "              seriesname: \"Đã kích hoạt\",";
                str += "              data: [";
                for (int i = 0; i < dt.Count; i++)
                {
                    if (i < (dt.Count - 1))
                    {
                        str += "{ value: \"" + dt[i].numofvisit + "\" },";
                    }
                    else
                    {
                        str += "{ value: \"" + dt[i].numofvisit + "\" }";
                    }
                }
                str += "]";
                str += " },";
            }
            // Chưa kích hoạt
            List<ThanhVien> ChuaKichHoat = Name_Text("SELECT count(iuser_id) as numofvisit,day(dcreatedate) as date FROM users  where  month(dcreatedate)=" + lddl_month + " and year(dcreatedate)=" + lddl_yearbyday + "  and DuyetTienDanap=0  " + sql + " group by day(dcreatedate) order by day(dcreatedate) asc");
            if (ChuaKichHoat.Count > 0)
            {
                str += "          {";
                str += "              seriesname: \"Chưa kích hoạt\",";
                str += "              data: [";
                for (int i = 0; i < ChuaKichHoat.Count; i++)
                {
                    if (i < (ChuaKichHoat.Count - 1))
                    {
                        str += "{  value: \"" + ChuaKichHoat[i].numofvisit + "\" },";
                    }
                    else
                    {
                        str += "{ value: \"" + ChuaKichHoat[i].numofvisit + "\" }";
                    }
                }
                str += "]";
                str += "}";
                str += " ]";
                str += "};";
            }

            str += "    FusionCharts.ready(function() {";
            str += "        var myChart = new FusionCharts({";
            str += "            type: \"stackedcolumn2d\",";// msline  có thể đổi kiểu chỉ cần thay trường này thôi nhé,
            str += "            renderAt: \"chart-container\",";
            str += "            width: \"100%\",";
            str += "            height: \"100%\",";
            str += "            dataFormat: \"json\",";
            str += "            dataSource";
            str += "            }).render();";
            str += "    });";

            str += "</script>";

            lt_reportbydate.Text = str;
        }

        public void LocTheoThang()
        {
            string sql = "";
            if (ddlchinhanh.SelectedValue != "0")
            {
                sql += " and IDChiNhanh =" + ddlchinhanh.SelectedValue + " ";
            }
            if (ddlleader.SelectedValue != "0")
            {
                sql += " and ((MTree LIKE N'%|" + ddlleader.SelectedValue + "|%')) ";
            }
            if (ddlAgLand.SelectedValue != "-1")
            {
                sql += " and ThanhVienAgLang=" + ddlAgLand.SelectedValue + " ";
            }
            if (ddluutien.SelectedValue != "-1")
            {
                sql += " and Uutien=" + ddluutien.SelectedValue + " ";
            }
            if (ddlkieuthanhvien.SelectedValue != "-1")
            {
                if (ddlkieuthanhvien.SelectedValue == "1" || ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql += " and Type=" + ddlkieuthanhvien.SelectedValue + " ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "3")
                {
                    sql += " and Type=2 and TongSoSanPham!=0";
                }
            }


            string LocThang = ddl_yearbymonth.SelectedValue.Trim();
            string str = "";
            str += "<script type=\"text/javascript\">";
            str += "    const dataSource = {";
            str += "        chart: {";
            str += "            caption: \"Thống kê thành viên đăng ký theo năm\",";
            str += "            yaxisname: \"\",";
            str += "            subcaption: \"" + LocThang + "\",";
            str += "            showhovereffect: \"1\",";
            str += "            drawcrossline: \"1\",";
            str += "            plottooltext: \"<b style='color:red'>$dataValue</b> - $seriesName\",";
            str += "            theme: \"fusion\"";
            str += "        },";
            str += "        categories: [";
            str += "          {";
            str += "              category: [";

            //Category Theo Nhóm (Năm)
            List<ThanhVien> Thang = Name_Text("SELECT count(iuser_id) as numofvisit,month(dcreatedate) as month FROM users where  year(dcreatedate)=" + LocThang + " group by month(dcreatedate) order by month(dcreatedate) asc");
            if (Thang.Count > 0)
            {
                for (int i = 0; i < Thang.Count; i++)
                {
                    if (i < (Thang.Count - 1))
                    {
                        str += "{ label: \"Tháng: " + Thang[i].month + "\" },";
                    }
                    else
                    {
                        str += "{ label: \"Tháng: " + Thang[i].month + "\" }";
                    }
                }
            }


            str += "              ]";
            str += "          }";
            str += "        ],";

          ///Response.Write("SELECT count(iuser_id) as numofvisit,month(dcreatedate) as month FROM users where  year(dcreatedate)=" + LocThang + "  and DuyetTienDanap=1  " + sql + " group by month(dcreatedate) order by month(dcreatedate) asc");
            // Đã kích hoạt
            List<ThanhVien> dt = Name_Text("SELECT count(iuser_id) as numofvisit,month(dcreatedate) as month FROM users where  year(dcreatedate)=" + LocThang + "  and DuyetTienDanap=1  " + sql + " group by month(dcreatedate) order by month(dcreatedate) asc");
            if (dt.Count > 0)
            {
                str += "        dataset: [";
                str += "          {";
                str += "              seriesname: \"Đã kích hoạt\",";
                str += "              data: [";
                for (int i = 0; i < dt.Count; i++)
                {
                    if (i < (dt.Count - 1))
                    {
                        str += "{ value: \"" + dt[i].numofvisit + "\" },";
                    }
                    else
                    {
                        str += "{ value: \"" + dt[i].numofvisit + "\" }";
                    }
                }
                str += "]";
                str += " },";
            }
            // Chưa kích hoạt
            List<ThanhVien> ChuaKichHoat = Name_Text("SELECT count(iuser_id) as numofvisit,month(dcreatedate) as month FROM users where  year(dcreatedate)=" + LocThang + " and DuyetTienDanap=0  " + sql + " group by month(dcreatedate) order by month(dcreatedate) asc");
            if (ChuaKichHoat.Count > 0)
            {
                str += "          {";
                str += "              seriesname: \"Chưa kích hoạt\",";
                str += "              data: [";
                for (int i = 0; i < ChuaKichHoat.Count; i++)
                {
                    if (i < (ChuaKichHoat.Count - 1))
                    {
                        str += "{  value: \"" + ChuaKichHoat[i].numofvisit + "\" },";
                    }
                    else
                    {
                        str += "{ value: \"" + ChuaKichHoat[i].numofvisit + "\" }";
                    }
                }
                str += "]";
                str += "}";
              
            }

            str += " ]";
            str += "};";

            str += "    FusionCharts.ready(function() {";
            str += "        var myChart = new FusionCharts({";
            str += "            type: \"stackedcolumn2d\",";// msline  có thể đổi kiểu chỉ cần thay trường này thôi nhé,
            str += "            renderAt: \"chart-container\",";
            str += "            width: \"100%\",";
            str += "            height: \"100%\",";
            str += "            dataFormat: \"json\",";
            str += "            dataSource";
            str += "            }).render();";
            str += "    });";

            str += "</script>";

            lt_reportbymonth.Text = str;
        }
        public List<ThanhVien> Name_Text(string Name_Text)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(Name_Text, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<ThanhVien>(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        public class ThanhVien
        {
            public int numofvisit { get; set; }
            public int month { get; set; }
            public int date { get; set; }

        }

        // theo tháng/ Năm
        protected void btn_showbymonth_Click(object sender, EventArgs e)
        {
            LocTheoThang();
        }

        // theo tuần/ tháng
        protected void btn_showbydate_Click(object sender, EventArgs e)
        {
            LocTheoTuan();
        }

        protected void rd_bydate_CheckedChanged(object sender, EventArgs e)
        {
            this.pn_bydate.Visible = true;
            this.pn_bymonth.Visible = false;
        }

        protected void rd_bymonth_CheckedChanged(object sender, EventArgs e)
        {
            this.pn_bydate.Visible = false;
            this.pn_bymonth.Visible = true;
        }
    }
}