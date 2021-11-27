using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.E_Commerce.cms.Display.Downloads
{
    public partial class Detail : System.Web.UI.UserControl
    {
        #region string
        string did = "-1";
        private string language = Captionlanguage.Language;
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        DatalinqDataContext db = new DatalinqDataContext();
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
            #region Requesthp
            if (Request["hp"] != null && !Request["hp"].Equals(""))
            {
                hp = Request["hp"].ToString();
            }
            iEmptyIndex = hp.IndexOf("?");
            if (iEmptyIndex != -1)
            {
                hp = hp.Substring(0, iEmptyIndex);
            }
            #endregion
            if (!IsPostBack)
            {
                #region Detail_ID
                Download dt = db.Downloads.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    lttitle.Text = dt.Title.ToString();
                    ltdesc.Text = dt.Brief.ToString();
                    ltcontent.Text = dt.Contents.ToString();
                    lttaifile.Text = "<a target=_blank href=\"/cms/display/Download/Defaul.aspx?id=" + dt.ID.ToString() + "\">Tải dữ liệu file</a>";
                }
                #endregion
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}