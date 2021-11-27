using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.E_Commerce.cms.Display.GioiThieu
{
    public partial class GioiThieu : System.Web.UI.UserControl
    {
        #region string
        string tid = "-1";
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
        #endregion
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region #
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            #endregion
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
                #region MyRegion
                if (Request["tid"] == null)
                {
                    List<Entity.Gioithieu> dt = SGioithieu.GET_BY_ALL(language);
                    if (dt.Count > 0)
                    {
                        lttitle.Text = dt[0].Title;
                        ltdesc.Text = dt[0].Brief;
                        ltcontent.Text = dt[0].Contents;
                    }
                }
                else
                {
                    Gioithieu dt = db.Gioithieus.SingleOrDefault(p => p.TangName == hp);
                    if (dt != null)
                    {
                        tid = dt.ID.ToString();
                        lttitle.Text = dt.Title;
                        ltdesc.Text = dt.Brief;
                        ltcontent.Text = dt.Contents;

                    }

                    #region views
                    if (MoreAll.MoreAll.GetCookies("views").Equals("") || !MoreAll.MoreAll.GetCookies("views").Equals(this.tid))
                    {
                        SGioithieu.UPDAE_VIEW_TIMES(this.tid);
                        MoreAll.MoreAll.SetCookie("views", this.tid);
                    }
                    #endregion
                #endregion
                }
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}