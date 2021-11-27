using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using Entity;
using Framework;
using System.Data;

namespace VS.E_Commerce.cms.Admin.WebAnalytics
{
    public partial class WebAnalytics : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;

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
                this.ddl_yearbyday.Items.Clear();
                this.ddl_yearbymonth.Items.Clear();
                for (int i = 2011; i < (DateTime.Now.Year + 1); i++)
                {
                    this.ddl_yearbyday.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    this.ddl_yearbymonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddl_yearbyday, DateTime.Now.Year.ToString());
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddl_yearbymonth, DateTime.Now.Year.ToString());
                this.btn_showbydate.Text = this.label("l_showreport");
                this.btn_showbymonth.Text = this.label("l_showreport");
                this.rd_bymonth.Text = this.label("lt_month");
                this.rd_byyear.Text = this.label("lt_year");
                this.rd_bydate.Text = this.label("lt_day");
            }
        }

        protected void btn_showbydate_Click(object sender, EventArgs e)
        {
            Fweb_statistic db = new Fweb_statistic();
            DataTable report = new DataTable();
            report = db.ByDate(this.ddl_month.SelectedValue.Trim(), this.ddl_yearbyday.SelectedValue.Trim());
            this.lt_reportbydate.Text = ReportByDate(report, "blue", "#FCFCFC", "red");
        }

        protected void btn_showbymonth_Click(object sender, EventArgs e)
        {
            Fweb_statistic db = new Fweb_statistic();
            DataTable report = new DataTable();
            report = db.ByMonth(this.ddl_yearbymonth.SelectedValue.Trim());
            this.lt_reportbymonth.Text = ReportByMonth(report, "blue", "#FCFCFC", "red");
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void rd_bydate_CheckedChanged(object sender, EventArgs e)
        {
            this.pn_bydate.Visible = true;
            this.pn_bymonth.Visible = false;
            this.pn_byyear.Visible = false;
        }

        protected void rd_bymonth_CheckedChanged(object sender, EventArgs e)
        {
            this.pn_bydate.Visible = false;
            this.pn_bymonth.Visible = true;
            this.pn_byyear.Visible = false;
        }

        protected void rd_byyear_CheckedChanged(object sender, EventArgs e)
        {
            this.pn_bydate.Visible = false;
            this.pn_bymonth.Visible = false;
            this.pn_byyear.Visible = true;
        }

        public static string ReportByDate(DataTable report, string Fcolor, string Bcolor, string Barcolor)
        {
            string str = "<table   border='0' width='100%' cellpadding='0' style='border-collapse: collapse; border-left-width: 1px; border-right: 1px solid #FFFFFF; border-top-width: 1px; border-bottom-width: 1px' ><tr height=0><td><font size=1>[NUM]</font></td></tr><tr  bgcolor=" + Barcolor + " height=[HEIGHT] style='border-left-width: 1px; border-right: 1px solid #FFFFFF; border-top-width: 1px; border-bottom-width: 1px'><td></td></tr><tr align=center height=10><td>[COL]</td></tr></table>";
            string str2 = "<table border='0' width='100%' cellpadding='0' style='border-collapse: collapse; border-left: 1px solid #C0C0C0; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #C0C0C0' id='table2'><tr><td style='border-left-style: solid; border-left-width: 1px; border-bottom-style: solid; border-bottom-width: 1px'> &nbsp;";
            str2 = str2 + "</td><td height=150 valign=bottom><table bgcolor=" + Bcolor + " height=0  border='0' cellpadding='0' style='border-collapse: collapse'><tr  valign=bottom>";
            string str3 = "";
            int num = 0;
            for (int i = 0; i < report.Rows.Count; i++)
            {
                int num3 = Convert.ToInt32(report.Rows[i]["numofvisit"].ToString());
                num += num3;
            }
            for (int j = 0; j < report.Rows.Count; j++)
            {
                str2 = str2 + "<td align=center width=20>";
                str3 = str;
                int num5 = Convert.ToInt32(report.Rows[j]["numofvisit"].ToString());
                int num6 = (num5 * 350) / num;
                str3 = str3.Replace("[HEIGHT]", num6.ToString()).Replace("[COL]", report.Rows[j]["date"].ToString()).Replace("[NUM]", report.Rows[j]["numofvisit"].ToString());
                str2 = str2 + str3 + "</td><td></td>";
            }
            return (str2 + "</tr></table></td>\t</tr></table>" + "<table class=all border='0' width='100%' cellspacing='0' cellpadding='0'><tr><td width=50%><b>Total</b>: [TOTAL]</td><td></td></tr></table>").Replace("[TOTAL]", num.ToString());
        }

        public static string ReportByMonth(DataTable report, string Fcolor, string Bcolor, string Barcolor)
        {
            string str = "<table  border='0' width='100%' cellpadding='0' style='border-collapse: collapse; border-left-width: 1px; border-right: 1px solid #FFFFFF; border-top-width: 1px; border-bottom-width: 1px'' id='table4'><tr height=0><td><font size=1>[NUM]</font></td></tr><tr  bgcolor=" + Barcolor + " height=[HEIGHT]><td></td></tr><tr align=center height=10><td>[COL]</td></tr></table>";
            string str2 = "<table border='0' width='100%' cellpadding='0' style='border-collapse: collapse; border-left: 1px solid #C0C0C0; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #C0C0C0' id='table2'><tr><td style='border-left-style: solid; border-left-width: 1px; border-bottom-style: solid; border-bottom-width: 1px'> &nbsp;";
            str2 = str2 + "</td><td><table bgcolor=" + Bcolor + " height=0 class=all border='0' cellpadding='0' style='border-collapse: collapse'><tr  valign=bottom>";
            string str3 = "1";
            int num = 0;
            for (int i = 0; i < report.Rows.Count; i++)
            {
                int num3 = Convert.ToInt32(report.Rows[i]["numofvisit"].ToString());
                num += num3;
            }
            for (int j = 0; j < report.Rows.Count; j++)
            {
                str2 = str2 + "<td align=center width=20>";
                str3 = str;
                int num5 = Convert.ToInt32(report.Rows[j]["numofvisit"].ToString());
                int num6 = (num5 * 350) / num;
                str3 = str3.Replace("[HEIGHT]", num6.ToString()).Replace("[COL]", report.Rows[j]["month"].ToString()).Replace("[NUM]", report.Rows[j]["numofvisit"].ToString());
                str2 = str2 + str3 + "</td><td></td>";
            }
            return (str2 + "</tr></table></td>\t</tr></table>" + "<table class=all border='0' width='100%' cellspacing='0' cellpadding='0'><tr><td width=50%><b>Total</b>: [TOTAL]</td><td></td></tr></table>").Replace("[TOTAL]", num.ToString());
        }
    }
}