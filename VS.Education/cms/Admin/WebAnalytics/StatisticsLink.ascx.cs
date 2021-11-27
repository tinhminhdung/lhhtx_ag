using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.E_Commerce.cms.Admin.WebAnalytics
{
    public partial class StatisticsLink : System.Web.UI.UserControl
    {
        //private string lang = Captionlanguage.Language;
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (System.Web.HttpContext.Current.Session["lang"] != null)
        //    {
        //        this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
        //    }
        //    else
        //    {
        //        System.Web.HttpContext.Current.Session["lang"] = this.lang;
        //        this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
        //    }
        //    if (!base.IsPostBack)
        //    {
        //        this.ddl_yearbyday.Items.Clear();
        //        this.ddl_yearbymonth.Items.Clear();
        //        for (int i = 2011; i < (DateTime.Now.Year + 1); i++)
        //        {
        //            this.ddl_yearbyday.Items.Add(new ListItem(i.ToString(), i.ToString()));
        //            this.ddl_yearbymonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
        //        }
        //        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddl_yearbyday, DateTime.Now.Year.ToString());
        //        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddl_yearbymonth, DateTime.Now.Year.ToString());
        //        this.btn_showbydate.Text = this.label("l_showreport");
        //        this.btn_showbymonth.Text = this.label("l_showreport");
        //        this.rd_bymonth.Text = this.label("lt_month");
        //        this.rd_byyear.Text = this.label("lt_year");
        //        this.rd_bydate.Text = this.label("lt_day");
        //    }
        //    //List<Entity.StatisticsLink> dt = SStatisticsLink.Name_Text("SELECT * FROM [StatisticsLink]  order by Create_Date desc");
        //    //rpcates.DataSource = dt;
        //    //rpcates.DataBind();
        //}
        //protected List<Entity.StatisticsLink> NewProductInCate(string icid)
        //{
        //    return SStatisticsLink.GetTopProductInCategory(icid);
        //}
        //protected void btn_showbydate_Click(object sender, EventArgs e)
        //{
        //    string str = "";
        //    List<Entity.StatisticsLink> dt = SStatisticsLink.Name_Text("SELECT * FROM [StatisticsLink] where month(Create_Date)=" + ddl_month.SelectedValue + " and year(Create_Date)=" + ddl_yearbyday.SelectedValue + " order by Create_Date desc");
        //    //for (int i = 0; i < dt.Count; i++)
        //    //{
        //    //    str += dt[i].IP.ToString();
        //    //}
        //    //this.lt_reportbydate.Text = str.ToString();
        //   // lt_reportbydate.Text = "SELECT * FROM [StatisticsLink] where month(Create_Date)=" + ddl_month.SelectedValue + " and year(Create_Date)=" + ddl_yearbyday.SelectedValue + " order by Create_Date desc";
        //    rp_thangnam.DataSource = dt;
        //    rp_thangnam.DataBind();
        //}
        //protected void btn_showbymonth_Click(object sender, EventArgs e)
        //{
        //   // this.lt_reportbymonth.Text = "SELECT * FROM [StatisticsLink] where year(Create_Date)=" + ddl_yearbyday.SelectedValue + " order by Create_Date desc";
        //    string str = "";
        //    List<Entity.StatisticsLink> dt = SStatisticsLink.Name_Text("SELECT * FROM [StatisticsLink] where year(Create_Date)=" + ddl_yearbyday.SelectedValue + " order by Create_Date desc");
        //    //for (int i = 0; i < dt.Count; i++)
        //    //{
        //    //    str += dt[i].IP.ToString();
        //    //}
        //    //this.lt_reportbymonth.Text = str.ToString();

        //    reportbymonth.DataSource = dt;
        //    reportbymonth.DataBind();
        //}

        //protected string label(string id)
        //{
        //    return Captionlanguage.GetLabel(id, this.lang);
        //}

        //protected void rd_bydate_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.pn_bydate.Visible = true;
        //    this.pn_bymonth.Visible = false;
        //    this.pn_byyear.Visible = false;
        //}

        //protected void rd_bymonth_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.pn_bydate.Visible = false;
        //    this.pn_bymonth.Visible = true;
        //    this.pn_byyear.Visible = false;
        //}

        //protected void rd_byyear_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.pn_bydate.Visible = false;
        //    this.pn_bymonth.Visible = false;
        //    this.pn_byyear.Visible = true;
        //}
    }
}