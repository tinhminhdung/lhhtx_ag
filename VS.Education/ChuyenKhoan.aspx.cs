using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class ChuyenKhoan : System.Web.UI.Page
    {
        string key = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["key"] != null && !Request["key"].Equals(""))
            {
                key = Request["key"].ToString();
            }
            List<Entity.users> dt = Susers.Name_Text("select * from users where (vuserun ='" + key + "')");
            if (dt.Count >= 1)
            {
                Literal1.Text = dt[0].vfname.ToString();
                Literal2.Text = dt[0].vemail.ToString();
                Literal3.Text = dt[0].vphone.ToString();
            }
        }

       
    }
}