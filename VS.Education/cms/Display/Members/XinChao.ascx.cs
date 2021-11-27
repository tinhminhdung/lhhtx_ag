using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.Members
{
    public partial class XinChao : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lnkthoat_Click(object sender, EventArgs e)
        {
            MoreAll.MoreAll.SetCookie("Members", "", -1);
            Response.Redirect("/");
        }
    }
}