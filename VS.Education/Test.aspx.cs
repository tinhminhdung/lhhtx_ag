using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class Test : System.Web.UI.Page
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string pas = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["pas"] != null && !Request["pas"].Equals(""))
            {
                pas = Request["pas"].ToString();
            }
            if (!RegularExpressions.Password(pas) == true)
            {
                Response.Write("KO");
            }
            else
            {
                Response.Write("OK");
            }

            if (!base.IsPostBack)
            {
                //string MachineName1 = Environment.MachineName;
                //string MachineName2 = System.Net.Dns.GetHostName();
                //string MachineName3 = Request.ServerVariables["REMOTE_HOST"].ToString();
                //string MachineName4 = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

                //Response.Write("Computer Name 1: " + MachineName1 + "<br />");
                //Response.Write("Computer Name 2: " + MachineName2 + "<br />");
                //Response.Write("Computer Name 3: " + MachineName3 + "<br />");
                //Response.Write("Computer Name 4: " + MachineName4 + "<br />");

                //string strName = HttpContext.Current.User.Identity.Name.ToString();


                //Response.Write("Computer Name 5: " + strName + "<br />");
           

            }
        }
    }
}