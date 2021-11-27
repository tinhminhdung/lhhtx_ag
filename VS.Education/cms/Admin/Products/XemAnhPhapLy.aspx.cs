using MoreAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Admin.Products
{
    public partial class XemAnhPhapLy : System.Web.UI.Page
    {
        private string id = "-1";
        string bReturn = "";
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
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
            if (Request["ID"] != null && !Request["ID"].Equals(""))
            {
                List<product> dt = db.products.Where(s => s.ipid == int.Parse(Request["ID"].ToString())).ToList();
                if (dt.Count > 0)
                {
                    if (dt[0].Noidung2.ToString().Length > 5)
                    {
                        string[] strArray = dt[0].Noidung2.ToString().Split(new char[] { ',' });
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            bReturn += "<img alt='" + dt[0].Name.ToString() + "'src=\"" + strArray[i].ToString() + "\"/><br/>";
                        }
                    }
                    ltshowimg.Text = bReturn;
                }
            }

        }
    }
}